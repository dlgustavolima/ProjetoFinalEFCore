namespace EFCoreProjetoFinal.Domain
{
    public class EstudioJogo
    {
        public Guid EstudioId { get; set; }

        public Estudio Estudio { get; set; }

        public Guid JogosId { get; set; }

        public Jogo Jogo { get; set; }
    }
}
