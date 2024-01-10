using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository.Util
{
    //Essa classe é apenas para apresentar alguns metodos que podem ser uteis em algum momento
    public class UtilDataBaseRepository
    {
        protected readonly ApplicationContext Db;

        public UtilDataBaseRepository(ApplicationContext db)
        {
            Db = db;
        }

        //Esse metodo cria um banco de dados sem precisar de migrações
        public void EnsureCreated() 
        {
            Db.Database.EnsureCreated();
        }

        //Esse metodo apaga todo o banco de dados
        public void EnsureDeleted()
        {
            Db.Database.EnsureDeleted();
        }

        //Metodo para validar se consegue conectar no banco de dados
        public void HealthCheckBancoDeDados()
        {
            var cannConnect = Db.Database.CanConnect();
            if (cannConnect)
                Console.WriteLine("Conectado");
            else
                Console.WriteLine("Desconectado");
        }

        /* Metodo para abrir a conexão de forma manual
         * Fazendo de forma manual a conexão é aberta apenas uma vez fazendo o tempo diminuir consideravelmente
         * Então nessa demonstração, caso eu não abra a conexão manualmente o EF vai abrir 200x
         * Caso eu faça manualmente antes o EF vai abrir 1x só.
         * Muito util para fazer varias consultas de uma vez só
         */
        public void GerenciarEstadoDaConexao() 
        {
            int _count = 0;
            var time = System.Diagnostics.Stopwatch.StartNew();

            var conexao = Db.Database.GetDbConnection();

            conexao.StateChange += (_, __) => ++_count;

            conexao.Open();

            for (var i = 0; i < 200; i++)
            {
                Db.Codigos.AsNoTracking().Any();
            }

            time.Stop();
            var mensagem = $"Tempo: {time.Elapsed.ToString()}, Contador: {_count}";

            Console.WriteLine(mensagem);
        }

        public void MigracoesPendentes()
        {
            var migracoesPendentes = Db.Database.GetPendingMigrations();

            Console.WriteLine($"Total: {migracoesPendentes.Count()}");

            foreach (var migracao in migracoesPendentes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }

        public void AplicarMigracaoEmTempodeExecucao()
        {
            Db.Database.Migrate();
        }

        public void MigracoesJaAplicadas()
        {
            var migracoes = Db.Database.GetAppliedMigrations();

            Console.WriteLine($"Total: {migracoes.Count()}");

            foreach (var migracao in migracoes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }

        public void TodasMigracoes()
        {
            var migracoes = Db.Database.GetMigrations();

            Console.WriteLine($"Total: {migracoes.Count()}");

            foreach (var migracao in migracoes)
            {
                Console.WriteLine($"Migração: {migracao}");
            }
        }

        //Gera o script de todo o banco de dados
        public void ScriptGeralDoBancoDeDados()
        {
            var script = Db.Database.GenerateCreateScript();

            Console.WriteLine(script);
        }

    }
}
