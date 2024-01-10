namespace EFCoreProjetoFinal.Models.CodigoAcesso
{
    public class CodigoAcessoViewModel
    {
        public Guid Id { get; set; }

        public string Codigo { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataAtivacao { get; set; }

        public DateTime DataExpiracao { get; set; }

        public Guid JogoId { get; set; }
    }
}
