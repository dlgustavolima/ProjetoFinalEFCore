using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository.Util
{
    public class UtilStoreProceduresRepository
    {
        protected readonly ApplicationContext Db;

        public UtilStoreProceduresRepository(ApplicationContext db)
        {
            Db = db;
        }

        public void CriarStoredProcedure()
        {
            var criarCodigo = @"
            CREATE OR ALTER PROCEDURE CriarCodigo
                @Codigo VARCHAR(50),
                @Ativo bit
            AS
            BEGIN
                INSERT INTO 
                    CodigoAcesso(Descricao, Ativo) 
                    VALUES (@Codigo, @Ativo)
            END        
            ";

            Db.Database.ExecuteSqlRaw(criarCodigo);
        }

        public void InserirDadosViaProcedure()
        {
            Db.Database.ExecuteSqlRaw("execute CriarCodigo @p0, @p1", "Criar codigo via procedure", true);
        }

        public void CriarStoredProcedureDeConsulta()
        {
            var criarCodigo = @"
            CREATE OR ALTER PROCEDURE GetCodigo
                @Codigo VARCHAR(50)
            AS
            BEGIN
                SELECT * FROM CodigoAcesso Where Codigo Like @Codigo + '%'
            END        
            ";

            Db.Database.ExecuteSqlRaw(criarCodigo);
        }

        public void ConsultaViaProcedure()
        {
            var dep = new SqlParameter("@Codigo", "Consultar codigo via procedure");

            var codigos = Db.Codigos
                .FromSqlInterpolated($"EXECUTE GetCodigo {dep}")
                .ToList();
        }

    }
}
