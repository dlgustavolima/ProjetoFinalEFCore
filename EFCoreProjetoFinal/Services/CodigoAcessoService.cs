using EFCoreProjetoFinal.Data;
using EFCoreProjetoFinal.Data.Repository;
using EFCoreProjetoFinal.Data.Repository.GenericRepository;
using EFCoreProjetoFinal.Domain;

namespace EFCoreProjetoFinal.Services
{
    public class CodigoAcessoService : ICodigoAcessoService
    {
        private readonly ICodigoAcessoRepository _codigoAcessoRepository;
        private readonly IJogoRepository _jogoRepository;

        public CodigoAcessoService(ICodigoAcessoRepository codigoAcessoRepository, IJogoRepository jogoRepository)
        {
            _codigoAcessoRepository = codigoAcessoRepository;
            _jogoRepository = jogoRepository;
        }

        public async Task<CodigoAcesso> BuscarCodigoAcesso(Guid id)
        {
            return await _codigoAcessoRepository.GetByIdAsync(id);
        }

        public async Task<string> AdicionarCodigoAcesso(CodigoAcesso codigoAcesso)
        {
            _codigoAcessoRepository.Add(codigoAcesso);
            var result = await _codigoAcessoRepository.SaveChanges();

            if (result)
            {
                var novoCodigoAcesso = await _codigoAcessoRepository.FirstOrDefaultAsync(p => p.Codigo.Equals(codigoAcesso.Codigo));
                return novoCodigoAcesso?.Id.ToString();
            }

            return null;
        }

        public async Task AtualizarCodigoAcesso(CodigoAcesso codigoAcesso)
        {
            var jogo = await _jogoRepository.GetByIdAsync(codigoAcesso.JogoId);
            if (jogo == null) return;

            _codigoAcessoRepository.Update(codigoAcesso);
            await _codigoAcessoRepository.SaveChanges();
        }

        public async Task DeletarCodigoAcesso(CodigoAcesso codigoAcesso)
        {
            _codigoAcessoRepository.Remove(codigoAcesso);
            await _codigoAcessoRepository.SaveChanges();
        }
    }

    public interface ICodigoAcessoService
    {
        Task<CodigoAcesso> BuscarCodigoAcesso(Guid id);

        Task<string> AdicionarCodigoAcesso(CodigoAcesso codigoAcesso);

        Task AtualizarCodigoAcesso(CodigoAcesso codigoAcesso);

        Task DeletarCodigoAcesso(CodigoAcesso codigoAcesso);
    }
}
