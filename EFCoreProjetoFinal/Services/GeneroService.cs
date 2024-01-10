using EFCoreProjetoFinal.Data.Repository;
using EFCoreProjetoFinal.Domain;

namespace EFCoreProjetoFinal.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<Genero> BuscarGenero(Guid id)
        {
            return await _generoRepository.GetByIdAsync(id);
        }

        public async Task<List<Genero>> BuscarPlataformaPorJogoId(Guid jogoId)
        {
            return await _generoRepository.BuscarEstudioPorJogoId(jogoId);
        }

        public async Task<string> AdicionarGenero(Genero genero)
        {
            _generoRepository.Add(genero);
            var result = await _generoRepository.SaveChanges();

            if (result)
            {
                var novoGenero = await _generoRepository.FirstOrDefaultAsync(p => p.Nome.Equals(genero.Nome));
                return novoGenero?.Id.ToString();
            }

            return null;
        }

        public async Task AtualizarGenero(Genero genero)
        {
            _generoRepository.Update(genero);
            await _generoRepository.SaveChanges();
        }

        public async Task DeletarGenero(Genero genero)
        {
            _generoRepository.Remove(genero);
            await _generoRepository.SaveChanges();
        }
    }

    public interface IGeneroService
    {
        Task<Genero> BuscarGenero(Guid id);

        Task<List<Genero>> BuscarPlataformaPorJogoId(Guid jogoId);

        Task<string> AdicionarGenero(Genero genero);

        Task AtualizarGenero(Genero genero);

        Task DeletarGenero(Genero genero);
    }

}
