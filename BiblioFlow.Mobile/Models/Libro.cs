namespace BiblioFlow.Mobile.Models
{
    public class Libro
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string UbicacionEstante { get; set; } = string.Empty;
        public int StockTotal { get; set; }
        public int StockDisponible { get; set; }
        public string? PortadaURL { get; set; }
    }
}