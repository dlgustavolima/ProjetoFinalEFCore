using EFCoreProjetoFinal.Models.Estudio;
using EFCoreProjetoFinal.Models.Genero;
using EFCoreProjetoFinal.Models.Plataforma;
using System.Collections.Generic;

namespace EFCoreProjetoFinal.Models.Jogo
{
    public class JogoViewModel
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public List<JogoPlataformaViewModel> Plataformas { get; set; }

        public List<JogoGeneroViewModel> Generos { get; set; }

        public List<JogoEstudioViewModel> Estudios { get; set; }

        public Guid CodigoAcessoId { get; set; }
    }

    public class JogoPlataformaViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
    }

    public class JogoGeneroViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
    }

    public class JogoEstudioViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Empresa { get; set; }
    }
}
