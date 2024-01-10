namespace EFCoreProjetoFinal.Domain
{
    public class Plataforma : Entity
    {
        public string Nome { get; set; }

        public Guid JogoId { get; set; }

        public ICollection<JogoPlataforma> JogoPlataforma { get; set; }

    }
}
