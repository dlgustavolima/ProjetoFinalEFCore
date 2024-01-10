using EFCoreProjetoFinal.Data.Repository;
using EFCoreProjetoFinal.Domain;

namespace EFCoreProjetoFinal.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IPlataformaRepository _plataformaRepository;
        private readonly IEstudioRepository _estudioRepository;
        private readonly ICodigoAcessoRepository _codigoAcessoRepository;

        public JogoService(IJogoRepository jogoRepository,
                           IGeneroRepository generoRepository,
                           IPlataformaRepository plataformaRepository,
                           IEstudioRepository estudioRepository,
                           ICodigoAcessoRepository codigoAcessoRepository)
        {
            _jogoRepository = jogoRepository;
            _generoRepository = generoRepository;
            _plataformaRepository = plataformaRepository;
            _estudioRepository = estudioRepository;
            _codigoAcessoRepository = codigoAcessoRepository;
        }

        public async Task<Jogo> BuscarJogo(Guid id)
        {
            return await _jogoRepository.BuscarJogoPorId(id);
        }

        public async Task<List<Jogo>> BuscarJogoPorPlataforma(Guid id)
        {
            return await _jogoRepository.BuscarJogosPorPlataforma(id);
        }

        public async Task<List<Jogo>> BuscarJogoPorEstudio(Guid id)
        {
            return await _jogoRepository.BuscarJogosPorEstudio(id);
        }

        public async Task<List<Jogo>> BuscarJogoPorGenero(Guid id)
        {
            return await _jogoRepository.BuscarJogosPorGenero(id);
        }

        public async Task<string> AdicionarJogo(Jogo jogo)
        {
            var plataforma = await _plataformaRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.PlataformaId));
            if (plataforma == null) return $"Não encontramos nenhuma plataforma com esse id: {jogo.PlataformaId}";

            var genero = await _generoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.GeneroId));
            if (genero == null) return $"Não encontramos nenhum genero com esse id: {jogo.GeneroId}";

            var estudio = await _estudioRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.EstudioId));
            if (estudio == null) return $"Não encontramos nenhum estudio com esse id: {jogo.EstudioId}";

            var codigoAcesso = await _codigoAcessoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.CodigoAcessoId));
            if (codigoAcesso != null) codigoAcesso.ResgatarCodigo();
            else jogo.CodigoAcessoId = new Guid();

            await _jogoRepository.AdicionarJogo(jogo);

            var novoJogo = await _jogoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.Id));
            return novoJogo?.Id.ToString();
        }

        public async Task<string> AdicionarPlataformaParaJogo(Guid jogoId, Guid plataformaId)
        {
            var jogo = await _jogoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogoId));
            if (jogo == null) return $"Não encontramos nenhum jogo com esse id: {jogoId}";

            var plataforma = await _plataformaRepository.FirstOrDefaultAsync(p => p.Id.Equals(plataformaId));
            if (plataforma == null) return $"Não encontramos nenhuma plataforma com esse id: {plataformaId}";

            if (!await _jogoRepository.AdicionarPlataformaParaJogo(new JogoPlataforma()
            {
                JogosId = jogo.Id,
                PlataformaId = plataforma.Id
            })) 
                return $"Erro ao salvar plataforma: {plataforma.Nome} para jogo: {jogo.Nome}";

            return jogo.Id.ToString();
        }

        public async Task<string> AdicionarEstudioParaJogo(Guid jogoId, Guid estudioId)
        {
            var jogo = await _jogoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogoId));
            if (jogo == null) return $"Não encontramos nenhum jogo com esse id: {jogoId}";

            var estudio = await _estudioRepository.FirstOrDefaultAsync(p => p.Id.Equals(estudioId));
            if (estudio == null) return $"Não encontramos nenhum estudio com esse id: {estudioId}";

            if (!await _jogoRepository.AdicionarEstudioParaJogo(new EstudioJogo()
            {
                JogosId = jogo.Id,
                EstudioId = estudio.Id
            }))
                return $"Erro ao salvar estudio: {estudio.Nome} para jogo: {jogo.Nome}";

            return jogo.Id.ToString();
        }

        public async Task<string> AdicionarGeneroParaJogo(Guid jogoId, Guid generoId)
        {
            var jogo = await _jogoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogoId));
            if (jogo == null) return $"Não encontramos nenhum jogo com esse id: {jogoId}";

            var genero = await _generoRepository.FirstOrDefaultAsync(p => p.Id.Equals(generoId));
            if (genero == null) return $"Não encontramos nenhum genero com esse id: {generoId}";

            if (!await _jogoRepository.AdicionarGeneroParaJogo(new GeneroJogo()
            {
                JogosId = jogo.Id,
                GeneroId = genero.Id
            }))
                return $"Erro ao salvar genero: {genero.Nome} para jogo: {jogo.Nome}";

            return jogo.Id.ToString();
        }

        public async Task<string> AtualizarJogo(Jogo jogo)
        {
            var plataforma = await _plataformaRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.PlataformaId));
            if (plataforma == null) return $"Não encontramos nenhuma plataforma com esse id: {jogo.PlataformaId}";

            var genero = await _generoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.Id));
            if (genero == null) return $"Não encontramos nenhum genero com esse id: {jogo.GeneroId}";

            var estudio = await _estudioRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.EstudioId));
            if (estudio == null) return $"Não encontramos nenhum estudio com esse id: {jogo.EstudioId}";

            var codigoAcesso = await _codigoAcessoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.CodigoAcessoId));
            if (codigoAcesso != null) codigoAcesso.ResgatarCodigo();

            _jogoRepository.Update(jogo);
            var result = await _jogoRepository.SaveChanges();

            if (result)
            {
                var novoJogo = await _jogoRepository.FirstOrDefaultAsync(p => p.Id.Equals(jogo.Id));
                return novoJogo?.Id.ToString();
            }

            return null;
        }

        public async Task DeletarJogo(Jogo jogo)
        {
            _jogoRepository.Remove(jogo);
            await _estudioRepository.SaveChanges();
        }
    }

    public interface IJogoService
    {
        Task<Jogo> BuscarJogo(Guid id);

        Task<List<Jogo>> BuscarJogoPorPlataforma(Guid id);

        Task<List<Jogo>> BuscarJogoPorEstudio(Guid id);

        Task<List<Jogo>> BuscarJogoPorGenero(Guid id);

        Task<string> AdicionarJogo(Jogo genero);

        Task<string> AdicionarPlataformaParaJogo(Guid jogoId, Guid plataformaId);

        Task<string> AdicionarEstudioParaJogo(Guid jogoId, Guid estudioId);

        Task<string> AdicionarGeneroParaJogo(Guid jogoId, Guid generoId);

        Task<string> AtualizarJogo(Jogo genero);

        Task DeletarJogo(Jogo genero);
    }
}
