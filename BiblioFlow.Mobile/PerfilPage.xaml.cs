namespace BiblioFlow.Mobile
{
    public partial class PerfilPage : ContentPage
    {
        public PerfilPage()
        {
            InitializeComponent();
        }

        private async void OnCerrarSesionClicked(object sender, EventArgs e)
        {
            bool confirmar = await DisplayAlert("Cerrar Sesión", "¿Estás seguro de que deseas salir de tu cuenta?", "Sí, Salir", "Cancelar");

            if (confirmar)
            {
                // Limpiar credenciales y regresar al estado inicial
                await DisplayAlert("Sesión Finalizada", "Has cerrado sesión correctamente.", "OK");

                // Redirigir a la pestaña principal (Catálogo)
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
    }
}