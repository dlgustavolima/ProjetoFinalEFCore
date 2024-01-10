namespace EFCoreProjetoFinal.Domain
{
    public class CodigoAcesso : Entity
    {
        public string Codigo { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataAtivacao { get; set; }

        public DateTime DataExpiracao { get; set; }

        public Guid JogoId { get; set; }

        public ICollection<Jogo> Jogos { get; set; }

        public void ResgatarCodigo()
        {
            Ativo = true;
            DataAtivacao = DateTime.Now;
        }
    }
}
