using EFCoreProjetoFinal.Data.Functions;
using EFCoreProjetoFinal.Data.Repository.GenericRepository;
using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository
{
    public class CodigoAcessoRepository : Repository<CodigoAcesso>, ICodigoAcessoRepository
    {
        public CodigoAcessoRepository(ApplicationContext context) : base(context)
        {

        }

        public void DemonstracaoInsercaoShadowProperty(CodigoAcesso codigoAcesso)
        {
            Db.Entry(codigoAcesso).Property("UltimaAtualizacao").CurrentValue = DateTime.Now;
        }

        public void DemonstracaoQueryShadowProperty()
        {
            var codigo = Db.Codigos.Where(p => EF.Property<DateTime>(p, "UltimaAtualizacao") < DateTime.Now).ToList();
        }

        public void DemonstracaoFuncoesDateTime() 
        {
            Db.Codigos
                .Select(p => new 
                {
                    Dias = EF.Functions.DateDiffDay(DateTime.Now, p.DataAtivacao),
                    Mes = EF.Functions.DateDiffMonth(DateTime.Now, p.DataAtivacao),
                    Data = EF.Functions.DateFromParts(2024, 01, 01),
                    DataValida = EF.Functions.IsDate(p.Codigo) //Se é uma data
                });
        }

        public void DemonstracaoFuncaoLike()
        {
            var dados = Db.Codigos
                //.Where(p => EF.Functions.Like(p.Codigo, "Tes%"))
                .Where(p => EF.Functions.Like(p.Codigo, "Test[ei]%")) // Para procurar Teste ou Testi
                .ToList();
        }

        public void DemonstracaoFunctionLeft() 
        {
            var codigos = Db.Codigos
                .Select(p => MinhasFuncoes.Left(p.Codigo, 10))
                .ToList();
        }
    }

    public interface ICodigoAcessoRepository : IRepository<CodigoAcesso>
    {

    }
}
