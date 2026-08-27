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
            await CargarCatalogo();
        }

        private async Task CargarCatalogo()
        {
            _listaCompletaLibros = await _apiService.ObtenerLibrosAsync();

            if (_listaCompletaLibros == null || _listaCompletaLibros.Count == 0)
            {
                _listaCompletaLibros = ObtenerLibrosSimulados();
            }

            AplicarFiltros();
        }

        private void OnBuscarTextChanged(object sender, TextChangedEventArgs e)
        {
            AplicarFiltros();
        }

        private void OnFiltroDisponibilidadToggled(object sender, ToggledEventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            var searchBar = this.FindByName<SearchBar>("LibrosSearchBar");
            var switchDisp = this.FindByName<Switch>("DisponibilidadSwitch");
            var collectionView = this.FindByName<CollectionView>("LibrosCollectionView");

            string filtroTexto = searchBar?.Text?.ToLower() ?? "";
            bool soloDisponibles = switchDisp?.IsToggled ?? false;

            var resultado = _listaCompletaLibros.Where(l =>
                (string.IsNullOrWhiteSpace(filtroTexto) ||
                 l.Titulo.ToLower().Contains(filtroTexto) ||
                 l.Autor.ToLower().Contains(filtroTexto) ||
                 l.ISBN.Contains(filtroTexto)) &&
                (!soloDisponibles || l.StockDisponible > 0)
            ).ToList();

            if (collectionView != null)
            {
                collectionView.ItemsSource = resultado;
            }
        }

        // --- MÉTODOS REQUERIDOS POR EL XAML PARA RESOLVER ERRORES DE COMPILACIÓN ---

        private void OnBuscarLibroPressed(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private async void OnPerfilClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Perfil Digital 👤", "Credencial institucional del estudiante.", "OK");
        }

        private async void OnRecargarClicked(object sender, EventArgs e)
        {
            await CargarCatalogo();
        }

        private async void OnRefreshRequested(object sender, EventArgs e)
        {
            await CargarCatalogo();
        }

        private async void OnReservarBookClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton?.CommandParameter is Libro libro)
            {
                await Navigation.PushAsync(new DetalleLibroPage(libro));
            }
        }

        private async void OnVerDetalleClicked(object sender, EventArgs e)
        {
            var boton = sender as Button;
            if (boton?.CommandParameter is Libro libro)
            {
                await Navigation.PushAsync(new DetalleLibroPage(libro));
            }
        }

        private List<Libro> ObtenerLibrosSimulados()
        {
            return new List<Libro>
            {
                new Libro { LibroId = 1, Titulo = "Estructuras de Datos y Algoritmos", Autor = "Alfred Aho", ISBN = "978-0201100885", Categoria = "Ingeniería", UbicacionEstante = "Estante A-04", StockTotal = 3, StockDisponible = 3 },
                new Libro { LibroId = 2, Titulo = "Clean Code", Autor = "Robert C. Martin", ISBN = "978-0132350884", Categoria = "Software", UbicacionEstante = "Estante B-12", StockTotal = 2, StockDisponible = 1 }
            };
        }
    }
}