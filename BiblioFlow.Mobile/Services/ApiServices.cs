using System.Net.Http.Json;
using BiblioFlow.Mobile.Models;

namespace BiblioFlow.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        // IP local de tu PC para conexión con dispositivo móvil físico
        private const string BaseUrl = "http://192.168.100.193:5000/api/libros";

        public ApiService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<Libro>> ObtenerLibrosAsync()
        {
            try
            {
                var libros = await _httpClient.GetFromJsonAsync<List<Libro>>(BaseUrl);
                return libros ?? new List<Libro>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar libros: {ex.Message}");
                return new List<Libro>();
            }
        }

        public async Task<bool> ReservarLibroAsync(int libroId)
        {
            try
            {
                var response = await _httpClient.PostAsync($"{BaseUrl}/reservar/{libroId}", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al reservar en API: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DevolverLibroAsync(int libroId)
        {
            try
            {
                var response = await _httpClient.PutAsync($"{BaseUrl}/devolver/{libroId}", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al devolver en API: {ex.Message}");
                return false;
            }
        }
    }
}