using BiblioFlow.Mobile.Models;
using BiblioFlow.Mobile.Services;

namespace BiblioFlow.Mobile
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;
        private List<Libro> _listaCompletaLibros = new();

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarCatalogoLibros();
        }

        private async Task CargarCatalogoLibros()
        {
            MainRefreshView.IsRefreshing = true;
            _listaCompletaLibros = await _apiService.ObtenerLibrosAsync();

            // Si la API falla o devuelve lista vacía, cargamos datos simulados de respaldo para pruebas
            if (_listaCompletaLibros == null || _listaCompletaLibros.Count == 0)
            {
                _listaCompletaLibros = ObtenerLibrosSimulados();
            }

            LibrosCollectionView.ItemsSource = _listaCompletaLibros;
            MainRefreshView.IsRefreshing = false;
        }

        private async void OnBuscarLibroPressed(object sender, EventArgs e)
        {
            FiltrarLibros(LibrosSearchBar.Text);
        }

        private void OnBuscarTextChanged(object sender, TextChangedEventArgs e)
        {
            FiltrarLibros(e.NewTextValue);
        }

        private void FiltrarLibros(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
            {
                LibrosCollectionView.ItemsSource = _listaCompletaLibros;
            }
            else
            {
                var resultado = _listaCompletaLibros.Where(l =>
                    l.Titulo.ToLower().Contains(filtro.ToLower()) ||
                    l.Autor.ToLower().Contains(filtro.ToLower()) ||
                    l.ISBN.Contains(filtro)).ToList();

                LibrosCollectionView.ItemsSource = resultado;
            }
        }

        private async void OnReservarBookClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var libro = button?.CommandParameter as Libro;

            if (libro == null) return;

            if (libro.StockDisponible <= 0)
            {
                await DisplayAlert("Agotado", $"El libro '{libro.Titulo}' no cuenta con ejemplares disponibles actualmente.", "OK");
                return;
            }

            bool confirmar = await DisplayAlert("Confirmar Reserva",
                $"¿Deseas apartar el libro '{libro.Titulo}'?\n\nUbicación: {libro.UbicacionEstante}\nTienes 24 horas para recogerlo en mostrador.",
                "Sí, Reservar", "Cancelar");

            if (confirmar)
            {
                libro.StockDisponible -= 1;
                // Forzar refresco de la lista visual
                LibrosCollectionView.ItemsSource = null;
                LibrosCollectionView.ItemsSource = _listaCompletaLibros;

                await DisplayAlert("Reserva Exitosa 🎉",
                    $"Se ha generado tu apartado para '{libro.Titulo}'.\nCódigo de Reserva: RES-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                    "Ver Comprobante QR");
            }
        }

        private async void OnPerfilClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Perfil de Usuario", "Usuario Activo: Carlos Mendoza\nMatrícula: 20240001\nPréstamos Activos: 1", "Cerrar");
        }

        private async void OnRecargarClicked(object sender, EventArgs e)
        {
            await CargarCatalogoLibros();
        }

        private async void OnRefreshRequested(object sender, EventArgs e)
        {
            await CargarCatalogoLibros();
        }

        // Datos de respaldo por si el emulador pierde comunicación con el localhost
        private List<Libro> ObtenerLibrosSimulados()
        {
            return new List<Libro>
            {
                new Libro { LibroId = 1, Titulo = "Estructuras de Datos y Algoritmos", Autor = "Alfred Aho", ISBN = "978-0201100885", Categoria = "Ingeniería", UbicacionEstante = "Estante A-04", StockTotal = 3, StockDisponible = 3 },
                new Libro { LibroId = 2, Titulo = "Clean Code", Autor = "Robert C. Martin", ISBN = "978-0132350884", Categoria = "Software", UbicacionEstante = "Estante B-12", StockTotal = 2, StockDisponible = 1 },
                new Libro { LibroId = 3, Titulo = "Sistemas Operativos Modernos", Autor = "Andrew S. Tanenbaum", ISBN = "978-0136006633", Categoria = "Sistemas", UbicacionEstante = "Estante C-01", StockTotal = 5, StockDisponible = 4 }
            };
        }
    }
}