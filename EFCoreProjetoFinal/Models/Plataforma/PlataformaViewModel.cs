using EFCoreProjetoFinal.Models.Jogo;

namespace EFCoreProjetoFinal.Models.Plataforma
{
    public class PlataformaViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public List<PlataformaJogoViewModel> Jogos { get; set; }
    }

    public class PlataformaJogoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

    }
}
