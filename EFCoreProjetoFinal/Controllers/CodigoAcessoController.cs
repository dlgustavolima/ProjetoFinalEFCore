using EFCoreProjetoFinal.Domain;
using EFCoreProjetoFinal.Models.CodigoAcesso;
using EFCoreProjetoFinal.Models.Estudio;
using EFCoreProjetoFinal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreProjetoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodigoAcessoController : MainController
    {
        private readonly ILogger<CodigoAcessoController> _logger;
        private readonly ICodigoAcessoService _codigoAcessoService;

        public CodigoAcessoController(ILogger<CodigoAcessoController> logger,
            ICodigoAcessoService codigoAcessoService)
        {
            _logger = logger;
            _codigoAcessoService = codigoAcessoService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstudioViewModel>> Buscar(Guid id)
        {
            return CustomResponse(true, await ObterCodigoAcesso(id));
        }

        [HttpPost]
        public async Task<ActionResult<string>> Adicionar(AdicionarCodigoAcessoViewModel codigoAcesso)
        {
            var response = await _codigoAcessoService.AdicionarCodigoAcesso(new CodigoAcesso
            {
                Codigo = codigoAcesso.Codigo,
                Ativo = false,
                DataExpiracao = DateTime.Now.AddMonths(12)
            });

            if (string.IsNullOrEmpty(response))
            {
                return CustomResponse(false, "Erro ao adicionar a novo estudio");
            }

            return CustomResponse(true, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<string>> Atualizar(Guid id, AtualizarCodigoAcessoViewModel codigoAcesso)
        {
            if (id != codigoAcesso.Id)
                return CustomResponse(false, "Os ids informados não são iguais!");

            var codigoAcessoAtualizacao = await ObterCodigoAcesso(codigoAcesso.Id);
            if (codigoAcessoAtualizacao == null)
                return CustomResponse(false, $"Não encontramos nenhum codigo de acesso com esse Id {id}");

            codigoAcessoAtualizacao.JogoId = codigoAcesso.JogoId;

            await _codigoAcessoService.AtualizarCodigoAcesso(new CodigoAcesso
            {
                Id = codigoAcessoAtualizacao.Id,
                Ativo = codigoAcessoAtualizacao.Ativo,
                JogoId = codigoAcessoAtualizacao.JogoId
            });

            return CustomResponse(true, "Codigo de acesso atualizado com sucesso");
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<string>> Deletar(Guid id)
        {
            var codigoAcessoDeletar = await ObterCodigoAcesso(id);
            if (codigoAcessoDeletar == null)
                return CustomResponse(false, $"Não encontramos nenhum estudio com esse Id {id}");

            await _codigoAcessoService.DeletarCodigoAcesso(new CodigoAcesso { Id = id });

            return CustomResponse(true, $"Codigo de acesso {codigoAcessoDeletar.Codigo} removido com sucesso");
        }

        private async Task<CodigoAcessoViewModel> ObterCodigoAcesso(Guid id)
        {
            var codigoAcesso = await _codigoAcessoService.BuscarCodigoAcesso(id);

            if (codigoAcesso == null) return new CodigoAcessoViewModel();

            return new CodigoAcessoViewModel()
            {
                Id = codigoAcesso.Id,
                Codigo = codigoAcesso.Codigo,
                Ativo = codigoAcesso.Ativo,
                DataAtivacao = codigoAcesso.DataAtivacao,
                DataExpiracao = codigoAcesso.DataExpiracao,
                JogoId = codigoAcesso.JogoId
            };
        }
    }
}