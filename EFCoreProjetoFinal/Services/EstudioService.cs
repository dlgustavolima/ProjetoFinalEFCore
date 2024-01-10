using EFCoreProjetoFinal.Data.Repository;
using EFCoreProjetoFinal.Domain;

namespace EFCoreProjetoFinal.Services
{
    public class EstudioService : IEstudioService
    {
        private readonly IEstudioRepository _estudioRepository;

        public EstudioService(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        public async Task<Estudio> BuscarEstudio(Guid id)
        {
            return await _estudioRepository.GetByIdAsync(id);
        }

        public async Task<List<Estudio>> BuscarPlataformaPorJogoId(Guid jogoId)
        {
            return await _estudioRepository.BuscarEstudioPorJogoId(jogoId);
        }

        public async Task<string> AdicionarEstudio(Estudio estudio)
        {
            _estudioRepository.Add(estudio);
            var result = await _estudioRepository.SaveChanges();

            if (result)
            {
                var novoEstudio = await _estudioRepository.FirstOrDefaultAsync(p => p.Nome.Equals(estudio.Nome));
                return novoEstudio?.Id.ToString();
            }

            return null;
        }

        public async Task AtualizarEstudio(Estudio estudio)
        {
            _estudioRepository.Update(estudio);
            await _estudioRepository.SaveChanges();
        }


        public async Task DeletarEstudio(Estudio estudio)
        {
            _estudioRepository.Remove(estudio);
            await _estudioRepository.SaveChanges();
        }
    }

    public interface IEstudioService
    {
        Task<Estudio> BuscarEstudio(Guid id);

        Task<List<Estudio>> BuscarPlataformaPorJogoId(Guid jogoId);

        Task<string> AdicionarEstudio(Estudio estudio);

        Task AtualizarEstudio(Estudio estudio);

        Task DeletarEstudio(Estudio estudio);
    }

}
