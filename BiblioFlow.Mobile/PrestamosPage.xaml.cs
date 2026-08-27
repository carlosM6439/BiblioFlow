using BiblioFlow.Mobile.Models;
using BiblioFlow.Mobile.Services;

namespace BiblioFlow.Mobile
{
    public partial class PrestamosPage : ContentPage
    {
        private readonly ApiService _apiService;
        private static List<PrestamoItem> _listaPrestamos = new();

        public PrestamosPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarPrestamos();
        }

        private void CargarPrestamos()
        {
            if (_listaPrestamos.Count == 0)
            {
                _listaPrestamos = new List<PrestamoItem>
                {
                    new PrestamoItem { LibroId = 1, Titulo = "Estructuras de Datos y Algoritmos", Autor = "Alfred Aho", UbicacionEstante = "Estante A-04", FechaVencimiento = "28/08/2026" },
                    new PrestamoItem { LibroId = 2, Titulo = "Clean Code", Autor = "Robert C. Martin", UbicacionEstante = "Estante B-12", FechaVencimiento = "30/08/2026" }
                };
            }

            PrestamosCollectionView.ItemsSource = null;
            PrestamosCollectionView.ItemsSource = _listaPrestamos;
        }

        private async void OnDevolverReservaClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            var item = boton?.CommandParameter as PrestamoItem;

            if (item == null) return;

            bool confirmar = await DisplayAlert("Devolver / Cancelar",
                $"¿Deseas devolver el libro '{item.Titulo}' y liberar su reserva en biblioteca?",
                "Sí, Devolver", "No");

            if (confirmar)
            {
                bool exitoApi = await _apiService.DevolverLibroAsync(item.LibroId);

                if (exitoApi)
                {
                    _listaPrestamos.Remove(item);
                    PrestamosCollectionView.ItemsSource = null;
                    PrestamosCollectionView.ItemsSource = _listaPrestamos;

                    try { HapticFeedback.Default.Perform(HapticFeedbackType.LongPress); } catch { }

                    await DisplayAlert("Éxito 🎉", $"El ejemplar de '{item.Titulo}' fue devuelto y el stock se actualizó en PostgreSQL.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo actualizar la base de datos.", "OK");
                }
            }
        }

        private async void OnRenovarClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var item = btn?.CommandParameter as PrestamoItem;
            if (item != null)
            {
                await DisplayAlert("Renovado 🔄", $"El préstamo de '{item.Titulo}' se ha extendido por 7 días más.", "OK");
            }
        }
    }

    public class PrestamoItem
    {
        public int LibroId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string UbicacionEstante { get; set; } = string.Empty;
        public string FechaVencimiento { get; set; } = string.Empty;
    }
}