namespace EFCoreProjetoFinal.Models.Genero
{
    public class GeneroViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public Guid JogoId { get; set; }

        public List<GeneroJogoViewModel> Jogos { get; set; }
    }

    public class GeneroJogoViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

    }
}
