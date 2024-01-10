using EFCoreProjetoFinal.Domain;
using EFCoreProjetoFinal.Models.Genero;
using EFCoreProjetoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreProjetoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneroController : MainController
    {
        private readonly ILogger<GeneroController> _logger;
        private readonly IGeneroService _generoService;
        private readonly IJogoService _jogoService;

        public GeneroController(ILogger<GeneroController> logger,
            IGeneroService generoService,
            IJogoService jogoService)
        {
            _logger = logger;
            _generoService = generoService;
            _jogoService = jogoService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GeneroViewModel>> Buscar(Guid id)
        {
            return CustomResponse(true, await ObterGenero(id));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Adicionar(AdicionarGeneroViewModel genero)
        {
            var response = await _generoService.AdicionarGenero(new Genero
            {
                Nome = genero.Nome
            });

            if (string.IsNullOrEmpty(response))
            {
                return CustomResponse(false, "Erro ao adicionar a novo genero");
            }

            return CustomResponse(true, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<string>> Atualizar(Guid id, AtualizarGeneroViewModel genero)
        {
            if (id != genero.Id)
                return CustomResponse(false, "Os ids informados não são iguais!");

            var generoAtualizacao = await ObterGenero(genero.Id);
            if (generoAtualizacao == null)
                return CustomResponse(false, $"Não encontramos nenhum genero com esse Id {id}");

            generoAtualizacao.Nome = genero.Nome;

            await _generoService.AtualizarGenero(new Genero
            {
                Id = generoAtualizacao.Id,
                Nome = generoAtualizacao.Nome
            });

            return CustomResponse(true, "Genero atualizado com sucesso");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> Deletar(Guid id)
        {
            var generoDeletar = await ObterGenero(id);
            if (generoDeletar == null)
                return CustomResponse(false, $"Não encontramos nenhum genero com esse Id {id}");

            await _generoService.DeletarGenero(new Genero { Id = id });

            return CustomResponse(true, $"Genero {generoDeletar.Nome} removido com sucesso");
        }

        private async Task<GeneroViewModel> ObterGenero(Guid id)
        {
            var jogos = new List<GeneroJogoViewModel>();
            var genero = await _generoService.BuscarGenero(id);

            if (genero == null) return new GeneroViewModel();

            var listaJogos = await _jogoService.BuscarJogoPorGenero(id);

            foreach (var jogo in listaJogos)
            {
                jogos.Add(new GeneroJogoViewModel() 
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Descricao = jogo.Descricao
                });
            }

            return new GeneroViewModel()
            {
                Id = genero.Id,
                Nome = genero.Nome,
                Jogos = jogos
            };
        }
    }
}