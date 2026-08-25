using System.Net.Http.Json;
using BiblioFlow.Mobile.Models;

namespace BiblioFlow.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Libro>> ObtenerLibrosAsync()
        {
            try
            {
                // En el emulador de Android se usa 10.0.2.2 para hacer referencia al localhost de la PC
                // Cambia 5158 por el puerto HTTP que te dio tu consola de la API
                string url = "http://10.0.2.2:5158/api/libros";

                var libros = await _httpClient.GetFromJsonAsync<List<Libro>>(url);
                return libros ?? new List<Libro>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar a la API: {ex.Message}");
                return new List<Libro>();
            }
        }
    }
}