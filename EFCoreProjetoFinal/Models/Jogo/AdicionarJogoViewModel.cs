using EFCoreProjetoFinal.Models.Estudio;
using EFCoreProjetoFinal.Models.Genero;
using EFCoreProjetoFinal.Models.Plataforma;
using System.ComponentModel.DataAnnotations;

namespace EFCoreProjetoFinal.Models.Jogo
{
    public class AdicionarJogoViewModel
    {
        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        public Guid PlataformaId { get; set; }

        [Required]
        public Guid GeneroId { get; set; }

        [Required]
        public Guid EstudioId { get; set; }

        public Guid CodigoAcessoId { get; set; }
    }
}
