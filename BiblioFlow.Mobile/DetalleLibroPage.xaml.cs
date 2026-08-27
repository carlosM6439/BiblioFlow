using BiblioFlow.Mobile.Models;
using BiblioFlow.Mobile.Services;

namespace BiblioFlow.Mobile
{
    public partial class DetalleLibroPage : ContentPage
    {
        public Libro LibroSeleccionado { get; set; }
        private readonly ApiService _apiService;

        public DetalleLibroPage(Libro libro)
        {
            InitializeComponent();
            LibroSeleccionado = libro;
            _apiService = new ApiService();

            TituloLabel.Text = libro.Titulo;
            AutorLabel.Text = $"Autor: {libro.Autor}";
            IsbnLabel.Text = $"ISBN: {libro.ISBN}";
            UbicacionEstanteLabel.Text = libro.UbicacionEstante;
        }

        private async void OnConfirmarReservaClicked(object sender, EventArgs e)
        {
            bool exitoApi = await _apiService.ReservarLibroAsync(LibroSeleccionado.LibroId);

            if (!exitoApi)
            {
                await DisplayAlert("Error", "No se pudo actualizar el stock en la base de datos.", "OK");
                return;
            }

            DateTime fechaSeleccionada = RecogidaDatePicker.Date ?? DateTime.Now;
            TimeSpan horaSeleccionada = RecogidaTimePicker.Time ?? TimeSpan.FromHours(10);

            string fechaStr = fechaSeleccionada.ToString("dd/MM/yyyy");
            string horaStr = horaSeleccionada.ToString(@"hh\:mm");
            string codigoReserva = $"RES-{Guid.NewGuid().ToString()[..6].ToUpper()}";

            await Navigation.PushAsync(new ComprobanteQRPage(LibroSeleccionado, fechaStr, horaStr, codigoReserva));
        }
    }
}