namespace EFCoreProjetoFinal.Domain
{
    public class Genero : Entity
    {
        public string Nome { get; set; }

        public Guid JogoId { get; set; }

        public ICollection<GeneroJogo> GeneroJogo { get; set; }

    }
}
