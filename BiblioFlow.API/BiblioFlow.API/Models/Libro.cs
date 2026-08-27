using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiblioFlow.API.Models
{
    [Table("libros")]
    public class Libro
    {
        [Key]
        [Column("libroid")]
        public int LibroId { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Column("autor")]
        public string Autor { get; set; } = string.Empty;

        [Column("isbn")]
        public string ISBN { get; set; } = string.Empty;

        [Column("categoria")]
        public string Categoria { get; set; } = string.Empty;

        [Column("ubicacionestante")]
        public string UbicacionEstante { get; set; } = string.Empty;

        [Column("stocktotal")]
        public int StockTotal { get; set; }

        [Column("stockdisponible")]
        public int StockDisponible { get; set; }

        [Column("portadaurl")]
        public string? PortadaURL { get; set; }
    }
}