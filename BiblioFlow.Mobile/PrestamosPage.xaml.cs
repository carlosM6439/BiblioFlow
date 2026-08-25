namespace BiblioFlow.Mobile
{
    public partial class PrestamosPage : ContentPage
    {
        public PrestamosPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CargarPrestamos();
        }

        private void CargarPrestamos()
        {
            // Datos simulados de préstamos activos del usuario
            var prestamos = new List<PrestamoItem>
            {
                new PrestamoItem { Titulo = "Estructuras de Datos y Algoritmos", Autor = "Alfred Aho", FechaPrestamo = "18/05/2026", FechaVencimiento = "25/05/2026" },
                new PrestamoItem { Titulo = "Clean Code", Autor = "Robert C. Martin", FechaPrestamo = "20/05/2026", FechaVencimiento = "27/05/2026" }
            };

            PrestamosCollectionView.ItemsSource = prestamos;
        }

        private async void OnRenovarClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var item = btn?.CommandParameter as PrestamoItem;
            if (item != null)
            {
                await DisplayAlert("Renovación Exitosa", $"El préstamo de '{item.Titulo}' se ha extendido por 7 días más.", "OK");
            }
        }
    }

    public class PrestamoItem
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string FechaPrestamo { get; set; } = string.Empty;
        public string FechaVencimiento { get; set; } = string.Empty;
    }
}