using EFCoreProjetoFinal.Data.Repository.GenericRepository;
using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository
{
    public class GeneroRepository : Repository<Genero>, IGeneroRepository
    {
        public GeneroRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<List<Genero>> BuscarEstudioPorJogoId(Guid id)
        {
            var genero = await Db.GeneroJogo.Where(p => p.JogosId.Equals(id))
                .Include(p => p.Genero)
                .Select(p => p.Genero)
                .ToListAsync();

            return genero;
        }
    }

    public interface IGeneroRepository : IRepository<Genero>
    {
        Task<List<Genero>> BuscarEstudioPorJogoId(Guid id);
    }
}
