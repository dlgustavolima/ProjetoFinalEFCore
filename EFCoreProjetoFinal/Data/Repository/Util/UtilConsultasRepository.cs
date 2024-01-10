using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository.Util
{
    public class UtilConsultasRepository
    {
        protected readonly ApplicationContext Db;

        public UtilConsultasRepository(ApplicationContext db)
        {
            Db = db;
        }

        //Criar comandos SQL sem depender do LINQ
        public void ExecuteSQL()
        {
            // Primeira Opcao
            using (var cmd = Db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "SELECT 1";
                cmd.ExecuteNonQuery();
            }

            // Segunda Opcao
            var nome = "Dead Space";
            Db.Database.ExecuteSqlRaw("select * from Jogos where Nome = {0}", nome);

            //Terceira Opcao
            Db.Database.ExecuteSqlInterpolated($"select * from Jogos where Nome = {nome}");
        }

        public void ConsultaProjetadas()
        {
            Db.Codigos
                .Where(p => p.Codigo.Equals("Teste"))
                .Select(p => new { p.Codigo, p.DataExpiracao, Jogos = p.Jogos.Select(j => new { j.Nome, j.Descricao }) })
                .ToList();
        }

        public void ConsultaParametrizada()
        {
            var id = new SqlParameter
            {
                Value = "e7ffe6f9-d4aa-4e14-9710-0c857c74c793",
                DbType = System.Data.DbType.Guid
            };

            var codigos = Db.Codigos
                .FromSqlRaw("select * from Codigos with(nolock) where Id = {0}", id)
                .ToList();

            foreach (var codigo in codigos)
            {
                Console.WriteLine(codigo);
            }
        }

        public void Consulta1NN1()
        {
            //Utiliza left join
            var Jogos = Db.Jogos
                .Include(p => p.CodigoAcesso)
                .ToList();

            //Utiliza inner join
            var Codigo = Db.Codigos
                .Include(p=>p.Jogos)
                .ToList();
        }

        public void DivisaoDeConsulta()
        {
            var Jogos = Db.Jogos
                .Include(p => p.CodigoAcesso)
                .AsSplitQuery()
                .ToList();
        }

    }
}
