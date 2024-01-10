namespace EFCoreProjetoFinal.Domain
{
    public class GeneroJogo
    {
        public Guid GeneroId { get; set; }

        public Genero Genero { get; set; }

        public Guid JogosId { get; set; }

        public Jogo Jogo { get; set; }
    }
}
