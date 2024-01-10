using EFCoreProjetoFinal.Data.Repository.GenericRepository;
using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository
{
    public class PlataformaRepository : Repository<Plataforma>, IPlataformaRepository
    {
        public PlataformaRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<List<Plataforma>> BuscarPlataformaPorJogoId(Guid id)
        {
            var plaformas = await Db.JogoPlataforma.Where(p => p.JogosId.Equals(id))
                .Include(p => p.Plataforma)
                .Select(p => p.Plataforma)
                .ToListAsync();

            return plaformas;
        }
    }

    public interface IPlataformaRepository : IRepository<Plataforma>
    {
        Task<List<Plataforma>> BuscarPlataformaPorJogoId(Guid id);
    }
}
