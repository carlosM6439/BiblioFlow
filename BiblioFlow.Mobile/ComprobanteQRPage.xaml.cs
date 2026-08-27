using BiblioFlow.Mobile.Models;

namespace BiblioFlow.Mobile
{
    public partial class ComprobanteQRPage : ContentPage
    {
        public ComprobanteQRPage(Libro libro, string fecha, string hora, string codigo)
        {
            InitializeComponent();

            CodigoReservaLabel.Text = $"Código: {codigo}";
            DetalleFechaHoraLabel.Text = $"Libro: {libro.Titulo}\nRecogida: {fecha} a las {hora} hrs\nUbicación: {libro.UbicacionEstante}";

            string qrData = $"BIBLIOFLOW|{codigo}|{libro.LibroId}|{fecha}|{hora}";
            QrImageView.Source = $"https://api.qrserver.com/v1/create-qr-code/?size=200x200&data={Uri.EscapeDataString(qrData)}";
        }

        private async void OnGuardarQRClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Éxito 💾", "Comprobante guardado en tu dispositivo.", "OK");
        }

        private async void OnVolverInicioClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}