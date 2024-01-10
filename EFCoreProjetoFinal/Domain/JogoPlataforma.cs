namespace EFCoreProjetoFinal.Domain
{
    public class JogoPlataforma
    {
        public Guid JogosId { get; set; }

        public Jogo Jogo { get; set; }

        public Guid PlataformaId { get; set; }

        public Plataforma Plataforma { get; set; }
    }
}
