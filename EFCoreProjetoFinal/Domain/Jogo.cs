namespace EFCoreProjetoFinal.Domain
{
    public class Jogo : Entity
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public ICollection<JogoPlataforma> JogoPlataforma { get; set; } = new List<JogoPlataforma>();

        public Guid PlataformaId { get; set; }

        public ICollection<GeneroJogo> GeneroJogo { get; set; } = new List<GeneroJogo>();

        public Guid GeneroId { get; set; }

        public ICollection<EstudioJogo> EstudioJogo { get; set; } = new List<EstudioJogo>();

        public Guid EstudioId { get; set; }

        public CodigoAcesso CodigoAcesso { get; set; }

        public Guid CodigoAcessoId { get; set; }
    }
}
