using EFCoreProjetoFinal.Data.Repository;
using EFCoreProjetoFinal.Domain;

namespace EFCoreProjetoFinal.Services
{
    public class PlataformaService : IPlataformaService
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public PlataformaService(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        public async Task<Plataforma> BuscarPlataforma(Guid id)
        {
            return await _plataformaRepository.GetByIdAsync(id);
        }

        public async Task<List<Plataforma>> BuscarPlataformaPorJogoId(Guid jogoId) 
        {
            return await _plataformaRepository.BuscarPlataformaPorJogoId(jogoId);
        }

        public async Task<string> AdicionarPlataforma(Plataforma plataforma)
        {
            _plataformaRepository.Add(plataforma);
            var result = await _plataformaRepository.SaveChanges();

            if (result)
            {
                var novaPlataforma = await _plataformaRepository.FirstOrDefaultAsync(p => p.Nome.Equals(plataforma.Nome));
                return novaPlataforma?.Id.ToString();
            }

            return null;
        }

        public async Task AtualizarPlataforma(Plataforma plataforma)
        {
            _plataformaRepository.Update(plataforma);
            await _plataformaRepository.SaveChanges();
        }

        public async Task DeletarPlataforma(Plataforma plataforma)
        {
            _plataformaRepository.Remove(plataforma);
            await _plataformaRepository.SaveChanges();
        }
    }

    public interface IPlataformaService
    {
        Task<Plataforma> BuscarPlataforma(Guid id);

        Task<List<Plataforma>> BuscarPlataformaPorJogoId(Guid jogoId);

        Task<string> AdicionarPlataforma(Plataforma plataforma);

        Task AtualizarPlataforma(Plataforma plataforma);

        Task DeletarPlataforma(Plataforma plataforma);
    }
}
