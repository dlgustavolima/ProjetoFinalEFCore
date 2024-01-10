using EFCoreProjetoFinal.Data.Repository.GenericRepository;
using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCoreProjetoFinal.Data.Repository
{
    public class JogoRepository : Repository<Jogo>, IJogoRepository
    {
        public JogoRepository(ApplicationContext context) : base(context)
        {

        }

        public async Task<Jogo> BuscarJogoPorId(Guid id)
        {
            var jogo = await Db.Jogos.Where(p => p.Id.Equals(id))
                .Include(p => p.EstudioJogo.Where(p => p.JogosId.Equals(id)))
                .Include(p => p.JogoPlataforma.Where(p => p.JogosId.Equals(id)))
                .Include(p => p.GeneroJogo.Where(p => p.JogosId.Equals(id)))
                .FirstOrDefaultAsync();

            return jogo;
        }

        public async Task<List<Jogo>> BuscarJogosPorPlataforma(Guid plataformaId)
        {
            return await Db.JogoPlataforma
                .Include(p => p.Jogo)
                .Where(p => p.PlataformaId.Equals(plataformaId))
                .Select(p => p.Jogo)
                .ToListAsync();
        }

        public async Task<List<Jogo>> BuscarJogosPorEstudio(Guid estudioId)
        {
            return await Db.EstudioJogo
                .Include(p => p.Jogo)
                .Where(p => p.EstudioId.Equals(estudioId))
                .Select(p => p.Jogo)
                .ToListAsync();
        }

        public async Task<List<Jogo>> BuscarJogosPorGenero(Guid generoId)
        {
            return await Db.GeneroJogo
                .Include(p => p.Jogo)
                .Where(p => p.GeneroId.Equals(generoId))
                .Select(p => p.Jogo)
                .ToListAsync();
        }

        public async Task AdicionarJogo(Jogo jogo)
        {
            Db.JogoPlataforma.Add(new JogoPlataforma
            {
                JogosId = jogo.Id,
                PlataformaId = jogo.PlataformaId,
            });

            Db.GeneroJogo.Add(new GeneroJogo
            {
                JogosId = jogo.Id,
                GeneroId = jogo.GeneroId
            });

            Db.EstudioJogo.Add(new EstudioJogo
            {
                JogosId = jogo.Id,
                EstudioId = jogo.EstudioId
            });

            Db.Jogos.Add(jogo);

            await Db.SaveChangesAsync();
        }

        public async Task<bool> AdicionarPlataformaParaJogo(JogoPlataforma jogoPlataforma)
        {
            Db.JogoPlataforma.Add(new JogoPlataforma
            {
                JogosId = jogoPlataforma.JogosId,
                PlataformaId = jogoPlataforma.PlataformaId,
            });

            return await Db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> AdicionarEstudioParaJogo(EstudioJogo estudioJogo)
        {
            Db.EstudioJogo.Add(new EstudioJogo
            {
                JogosId = estudioJogo.JogosId,
                EstudioId = estudioJogo.EstudioId,
            });

            return await Db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> AdicionarGeneroParaJogo(GeneroJogo generoJogo)
        {
            Db.GeneroJogo.Add(new GeneroJogo
            {
                JogosId = generoJogo.JogosId,
                GeneroId = generoJogo.GeneroId,
            });

            return await Db.SaveChangesAsync() > 0 ? true : false;
        }
    }

    public interface IJogoRepository : IRepository<Jogo>
    {
        Task<Jogo> BuscarJogoPorId(Guid id);
        Task<List<Jogo>> BuscarJogosPorPlataforma(Guid plataformaId);
        Task<List<Jogo>> BuscarJogosPorEstudio(Guid estudioId);
        Task<List<Jogo>> BuscarJogosPorGenero(Guid generoId);
        Task AdicionarJogo(Jogo jogo);
        Task<bool> AdicionarPlataformaParaJogo(JogoPlataforma jogo);
        Task<bool> AdicionarEstudioParaJogo(EstudioJogo jogo);
        Task<bool> AdicionarGeneroParaJogo(GeneroJogo generoJogo);
    }
}
