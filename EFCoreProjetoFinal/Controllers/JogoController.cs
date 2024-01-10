using EFCoreProjetoFinal.Domain;
using EFCoreProjetoFinal.Models.CodigoAcesso;
using EFCoreProjetoFinal.Models.Estudio;
using EFCoreProjetoFinal.Models.Jogo;
using EFCoreProjetoFinal.Models.Plataforma;
using EFCoreProjetoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreProjetoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogoController : MainController
    {
        private readonly ILogger<JogoController> _logger;
        private readonly IJogoService _jogoService;
        private readonly IEstudioService _estudioService;
        private readonly IGeneroService _generoService;
        private readonly IPlataformaService _plataformaService;

        public JogoController(ILogger<JogoController> logger,
            IJogoService jogoService,
            IEstudioService estudioService,
            IGeneroService generoService,
            IPlataformaService plataformaService)
        {
            _logger = logger;
            _jogoService = jogoService;
            _estudioService = estudioService;
            _generoService = generoService;
            _plataformaService = plataformaService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<JogoViewModel>> Buscar(Guid id)
        {
            return CustomResponse(true, await ObterJogo(id));
        }

        [HttpPost("Adicionar")]
        public async Task<ActionResult<string>> AdicionarJogo(AdicionarJogoViewModel jogo)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _jogoService.AdicionarJogo(new Jogo
            {
                Nome = jogo.Nome,
                Descricao = jogo.Descricao,
                PlataformaId = jogo.PlataformaId,
                GeneroId = jogo.GeneroId,
                EstudioId = jogo.EstudioId,
                CodigoAcessoId = jogo.CodigoAcessoId
            });

            return CustomResponse(true, result);
        }

        [HttpPost("AdicionarPlataformaParaJogo")]
        public async Task<ActionResult<string>> AdicionarPlataformaParaJogo(Guid jogoId, Guid plataformaId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _jogoService.AdicionarPlataformaParaJogo(jogoId, plataformaId);

            return CustomResponse(true, result);
        }

        [HttpPost("AdicionarEstudioParaJogo")]
        public async Task<ActionResult<string>> AdicionarEstudioParaJogo(Guid jogoId, Guid estudioId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _jogoService.AdicionarEstudioParaJogo(jogoId, estudioId);

            return CustomResponse(true, result);
        }

        [HttpPost("AdicionarGeneroParaJogo")]
        public async Task<ActionResult<string>> AdicionarGeneroParaJogo(Guid jogoId, Guid generoId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _jogoService.AdicionarGeneroParaJogo(jogoId, generoId);

            return CustomResponse(true, result);
        }

        private async Task<JogoViewModel> ObterJogo(Guid id)
        {
            var plataformas = new List<JogoPlataformaViewModel>();
            var generos = new List<JogoGeneroViewModel>();
            var estudios = new List<JogoEstudioViewModel>();

            var jogo = await _jogoService.BuscarJogo(id);

            if (jogo == null) return new JogoViewModel();

            var jogoResult = new JogoViewModel()
            {
                Nome = jogo.Nome,
                Descricao = jogo.Descricao,
                CodigoAcessoId = jogo.CodigoAcessoId
            };

            var plataformasResult = await _plataformaService.BuscarPlataformaPorJogoId(jogo.Id);

            foreach (var plataforma in plataformasResult)
            {
                plataformas.Add(new JogoPlataformaViewModel() 
                {
                    Id = plataforma.Id,
                    Nome = plataforma.Nome
                });
            }

            var estudiosResult = await _estudioService.BuscarPlataformaPorJogoId(jogo.Id);

            foreach (var estudio in estudiosResult)
            {
                estudios.Add(new JogoEstudioViewModel()
                {
                    Id = estudio.Id,
                    Nome = estudio.Nome,
                    Empresa = estudio.Empresa
                });
            }

            var generosResult = await _generoService.BuscarPlataformaPorJogoId(jogo.Id);

            foreach (var genero in generosResult)
            {
                generos.Add(new JogoGeneroViewModel()
                {
                    Id = genero.Id,
                    Nome = genero.Nome,
                });
            }

            jogoResult.Plataformas = plataformas;
            jogoResult.Estudios = estudios;
            jogoResult.Generos = generos;

            return jogoResult;
        }
    }
}