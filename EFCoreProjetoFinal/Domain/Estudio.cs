namespace EFCoreProjetoFinal.Domain
{
    public class Estudio : Entity
    {
        public string Nome { get; set; }

        public string Empresa { get; set; }

        public Guid JogoId { get; set; }

        public ICollection<EstudioJogo> EstudioJogo { get; set; }
    }
}
