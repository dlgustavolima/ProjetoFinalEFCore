using EFCoreProjetoFinal.Models.Jogo;

namespace EFCoreProjetoFinal.Models.Estudio
{
    public class EstudioViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Empresa { get; set; }

        public List<EstudioJogoViewModel> Jogos { get; set; }
    }

    public class EstudioJogoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

    }
}
