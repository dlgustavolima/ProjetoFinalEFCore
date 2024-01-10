using EFCoreProjetoFinal.Domain;
using EFCoreProjetoFinal.Models.Estudio;
using EFCoreProjetoFinal.Models.Plataforma;
using EFCoreProjetoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreProjetoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudioController : MainController
    {
        private readonly ILogger<EstudioController> _logger;
        private readonly IEstudioService _estudioService;
        private readonly IJogoService _jogoService;

        public EstudioController(ILogger<EstudioController> logger,
            IEstudioService estudioService,
            IJogoService jogoService)
        {
            _logger = logger;
            _estudioService = estudioService;
            _jogoService = jogoService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstudioViewModel>> Buscar(Guid id)
        {
            return CustomResponse(true, await ObterEstudio(id));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Adicionar(AdicionarEstudioViewModel estudio)
        {
            var response = await _estudioService.AdicionarEstudio(new Estudio
            {
                Nome = estudio.Nome,
                Empresa = estudio.Empresa
            });

            if (string.IsNullOrEmpty(response))
            {
                return CustomResponse(false, "Erro ao adicionar a novo estudio");
            }

            return CustomResponse(true, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<string>> Atualizar(Guid id, AtualizarEstudioViewModel estudio)
        {
            if (id != estudio.Id)
                return CustomResponse(false, "Os ids informados não são iguais!");

            var estudioAtualizacao = await ObterEstudio(estudio.Id);
            if (estudioAtualizacao == null)
                return CustomResponse(false, $"Não encontramos nenhum estudio com esse Id {id}");

            estudioAtualizacao.Nome = estudio.Nome;
            estudioAtualizacao.Empresa = estudio.Empresa;

            await _estudioService.AtualizarEstudio(new Estudio
            {
                Id = estudioAtualizacao.Id,
                Nome = estudioAtualizacao.Nome,
                Empresa = estudioAtualizacao.Empresa
            });

            return CustomResponse(true, "Estudio atualizado com sucesso");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> Deletar(Guid id)
        {
            var estudioDeletar = await ObterEstudio(id);
            if (estudioDeletar == null)
                return CustomResponse(false, $"Não encontramos nenhum estudio com esse Id {id}");

            await _estudioService.DeletarEstudio(new Estudio { Id = id });

            return CustomResponse(true, $"Estudio {estudioDeletar.Nome} removida com sucesso");
        }

        private async Task<EstudioViewModel> ObterEstudio(Guid id)
        {
            var jogos = new List<EstudioJogoViewModel>();
            var estudio = await _estudioService.BuscarEstudio(id);

            if (estudio == null) return new EstudioViewModel();

            var listaJogos = await _jogoService.BuscarJogoPorEstudio(id);

            foreach (var jogo in listaJogos)
            {
                jogos.Add(new EstudioJogoViewModel
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Descricao = jogo.Descricao
                });
            }

            return new EstudioViewModel()
            {
                Id = estudio.Id,
                Nome = estudio.Nome,
                Empresa = estudio.Empresa,
                Jogos = jogos
            };
        }
    }
}