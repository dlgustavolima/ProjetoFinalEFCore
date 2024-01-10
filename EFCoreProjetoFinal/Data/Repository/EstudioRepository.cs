using EFCoreProjetoFinal.Data.Repository.GenericRepository;
using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository
{
    public class EstudioRepository : Repository<Estudio>, IEstudioRepository
    {
        public EstudioRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<List<Estudio>> BuscarEstudioPorJogoId(Guid id)
        {
            var estudio = await Db.EstudioJogo.Where(p => p.JogosId.Equals(id))
                .Include(p => p.Estudio)
                .Select(p => p.Estudio)
                .ToListAsync();

            return estudio;
        }
    }

    public interface IEstudioRepository : IRepository<Estudio>
    {
        Task<List<Estudio>> BuscarEstudioPorJogoId(Guid id);
    }
}
