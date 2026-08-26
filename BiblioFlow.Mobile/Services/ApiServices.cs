using System.Net.Http.Json;
using BiblioFlow.Mobile.Models;

namespace BiblioFlow.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://10.0.2.2:5158/api/libros";

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
                Console.WriteLine($"Error al consultar API: {ex.Message}");
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
            catch
            {
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
            catch
            {
                return false;
            }
        }
    }
}