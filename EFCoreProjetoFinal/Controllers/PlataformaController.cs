using EFCoreProjetoFinal.Domain;
using EFCoreProjetoFinal.Models.Jogo;
using EFCoreProjetoFinal.Models.Plataforma;
using EFCoreProjetoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreProjetoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlataformaController : MainController
    {
        private readonly ILogger<PlataformaController> _logger;
        private readonly IPlataformaService _plataformaService;
        private readonly IJogoService _jogoService;

        public PlataformaController(ILogger<PlataformaController> logger,
            IPlataformaService plataformaService,
            IJogoService jogoService)
        {
            _logger = logger;
            _plataformaService = plataformaService;
            _jogoService = jogoService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PlataformaViewModel>> Buscar(Guid id)
        {
            return CustomResponse(true, await ObterPlataforma(id));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Adicionar(AdicionarPlataformaViewModel plataforma)
        {
            var response = await _plataformaService.AdicionarPlataforma(new Plataforma
            {
                Nome = plataforma.Nome
            });

            if (string.IsNullOrEmpty(response))
            {
                return CustomResponse(false, "Erro ao adicionar a nova plataforma");
            }

            return CustomResponse(true, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<string>> Atualizar(Guid id, AtualizarPlataformaViewModel plataforma)
        {
            if (id != plataforma.Id)
                return CustomResponse(false, "Os ids informados não são iguais!");

            var plataformaAtualizacao = await ObterPlataforma(plataforma.Id);
            if (plataformaAtualizacao == null)
                return CustomResponse(false, $"Não encontramos nenhuma plataforma com esse Id {id}");

            plataformaAtualizacao.Nome = plataforma.Nome;

            await _plataformaService.AtualizarPlataforma(new Plataforma
            {
                Id = plataformaAtualizacao.Id,
                Nome = plataformaAtualizacao.Nome
            });

            return CustomResponse(true, "Plataforma atualizada com sucesso");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> Deletar(Guid id)
        {
            var produtoDeletar = await ObterPlataforma(id);
            if (produtoDeletar == null)
                return CustomResponse(false, $"Não encontramos nenhuma plataforma com esse Id {id}");

            await _plataformaService.DeletarPlataforma(new Plataforma { Id = id });

            return CustomResponse(true, $"Plataforma {produtoDeletar.Nome} removida com sucesso");
        }

        private async Task<PlataformaViewModel> ObterPlataforma(Guid id)
        {
            var jogos = new List<PlataformaJogoViewModel>();
            var plataforma = await _plataformaService.BuscarPlataforma(id);

            if (plataforma == null) return new PlataformaViewModel();

            var listaJogos = await _jogoService.BuscarJogoPorPlataforma(id);

            foreach (var jogo in listaJogos)
            {
                jogos.Add(new PlataformaJogoViewModel()
                {
                    Id = jogo.Id,
                    Nome = jogo.Nome,
                    Descricao = jogo.Descricao
                });
            }

            return new PlataformaViewModel()
            {
                Id = plataforma.Id,
                Nome = plataforma.Nome,
                Jogos = jogos
            };
        }
    }
}