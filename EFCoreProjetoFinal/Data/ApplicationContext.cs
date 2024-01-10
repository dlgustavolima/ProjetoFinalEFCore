using EFCoreProjetoFinal.Data.Functions;
using EFCoreProjetoFinal.Data.Interceptors;
using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Reflection;

namespace EFCoreProjetoFinal.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Jogo> Jogos { get; set; }

        public DbSet<Plataforma> Plataformas { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Estudio> Estudios { get; set; }

        public DbSet<CodigoAcesso> Codigos { get; set; }

        public DbSet<EstudioJogo> EstudioJogo { get; set; }

        public DbSet<GeneroJogo> GeneroJogo { get; set; }

        public DbSet<JogoPlataforma> JogoPlataforma { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(p =>
                     p.CommandTimeout(120) //Habilitar o timeout padrão para todos os metodos
                    .EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null)) //Substitui o Retry Polly (numero de tentativas, tempo para tentar novamente, lista de erros transients (o proprio EF já tem alguns por padrão, mas você pode personalizar se preferir))
                .LogTo(Console.WriteLine, LogLevel.Debug, Microsoft.EntityFrameworkCore.Diagnostics.DbContextLoggerOptions.LocalTime) //Log + LogLevel + ContextOptions
                .EnableSensitiveDataLogging() //Habilitar ver os dados sensiveis das queries
                .EnableDetailedErrors() //Habilita ver os erros detalhados
                .AddInterceptors(new InterceptadorDeComandos()) //Interceptador de comandos (aplicando with(nolock) em todas as queries)
                .AddInterceptors(new InterceptadorPersistencia()); //Interceptador de persistencia (aplicando log para todas as transações)


            /* Evita que você precise ficar colocando .AsNoTrackingWithIdentityResolution() em todas as queries
             * O AsNoTrackingWithIdentityResolution é recomendado apenas para uso leitura
             * Caso você vá utilizar a leitura e alteração para as entities, não utilize "AsNoTrackingWithIdentityResolution"
             * Uma explicação melhor é que o Tracking o sistema mantem na memoria as entities e cada altereção o sistema já sabe o que houve, então é necessario apenas dar o SaveChanges() para salvar no DB
             * O AsNoTrackingWithIdentityResolution não mantem na memoria, então qualquer alteração você precisa informar o banco o que houve, então é necessario utilizar o Update() antes do SaveChanges()
             * Um artigo muito bom que pode ajudar a entender https://www.c-sharpcorner.com/article/maximizing-performance-in-entity-framework-co-tracking-vs-no-tracking/
             */
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Buscando todas as propriedades que possuam coluna string e inserindo varchar(100) caso tenha esquecido de colocar no Mapping
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

            #region Apenas para consulta, pode comentar se quiser

            //Filtro global, todas as consultas irão respeitar também essa condição.
            //Caso queira ignorar no momento da consulta é só colocar o IgnoreQueryFilter()
            modelBuilder.Entity<CodigoAcesso>().HasQueryFilter(p => p.Ativo.Equals(true));

            /* Para definir se as queries vão aceitar Case Sensitive e Acentuação
             * O penultimo parametro CI é caso você não queira Case Sensitive, caso queira é só mudar para CS
             * Mesma coisa para o ultimo parametro que é o AI, caso queira que ele aceita acentuação é só deixar AI, caso queira sem acentuação é só mudar para AS]
             * Para todas as Entity
             */
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            //Para uma Propriedade de uma entity especifica
            modelBuilder.Entity<CodigoAcesso>().Property(p => p.Codigo).UseCollation("SQL_Latin1_General_CP1_CS_AS");

            //Caso você queira que a contagem de algum campo comece dos 1000 pra frente (ou qualquer numero que desejar), esse metodo pode ser util.
            modelBuilder.HasSequence<int>("MinhaSequencia", "sequencias")
                .StartsAt(1000) // Começa apartir de 1000
                .IncrementsBy(1) // Acrescenta 1 por 1
                .HasMin(1000) // Caso você queira um minimo
                .HasMax(10000) // Caso você queira um maximo
                .IsCyclic(); // Caso você queira que ele volte a contar do 0 quando atingir o valor maximo

            //Para aplicar a sequencia acima
            modelBuilder.Entity<CodigoAcesso>()
                .Property(p => p.Id)
                .HasDefaultValue("NEXT VALUE FOR sequencias.MinhaSequencia");

            //Metodo para criar Index
            modelBuilder.Entity<CodigoAcesso>()
                .HasIndex(p => new { p.Id, p.Ativo})
                .HasDatabaseName("idx_meu_indice_composto")
                .HasFilter("Ativo IS NOT NULL")
                .IsUnique();

            /* Caso queira criar uma propriedade somente no banco de dados e que não seja relacionada com sua entity
             * No Repository dessa entity tem a demonstação de como fazer uma query para essa propriedade
             */
            modelBuilder.Entity<CodigoAcesso>().Property<DateTime>("UltimaAtualizacao");

            #region Demonstração de Functions

            modelBuilder
                .HasDbFunction(_minhaFuncao)
                .HasName("LEFT")
                .IsBuiltIn();

            modelBuilder
                .HasDbFunction(_letrasMaiusculas)
                .HasName("ConverterParaLetrasMaiusculas")
                .HasSchema("dbo");

            modelBuilder
                .HasDbFunction(_dateDiff)
                .HasName("DATEDIFF")
                .HasTranslation(p =>
                {
                    var argumentos = p.ToList();

                    var contante = (SqlConstantExpression)argumentos[0];
                    argumentos[0] = new SqlFragmentExpression(contante.Value.ToString());

                    return new SqlFunctionExpression("DATEDIFF", argumentos, false, new[] { false, false, false }, typeof(int), null);

                })
                .IsBuiltIn();

            #endregion

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        private static MethodInfo _minhaFuncao = typeof(MinhasFuncoes)
            .GetRuntimeMethod("Left", new[] { typeof(string), typeof(int) });

        private static MethodInfo _letrasMaiusculas = typeof(MinhasFuncoes)
                    .GetRuntimeMethod(nameof(MinhasFuncoes.LetrasMaiusculas), new[] { typeof(string) });

        private static MethodInfo _dateDiff = typeof(MinhasFuncoes)
                    .GetRuntimeMethod(nameof(MinhasFuncoes.DateDiff), new[] { typeof(string), typeof(DateTime), typeof(DateTime) });
    }
}
