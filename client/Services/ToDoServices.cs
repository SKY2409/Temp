using System.Net.Http.Json;
using ToDoApp.Client.Models;

namespace ToDoApp.Client.Services
{
    public class ToDoServices
    {
        private readonly HttpClient _http;

        public ToDoServices(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            var items = await _http.GetFromJsonAsync<List<ToDoItem>>("api/todo");
            return items ?? new List<ToDoItem>();
        }

        public async Task<ToDoItem?> GetByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<ToDoItem>($"api/todo/{id}");
        }

        public async Task<ToDoItem?> CreateAsync(ToDoItem item)
        {
            var response = await _http.PostAsJsonAsync("api/todo", item);
            if (!response.IsSuccessStatusCode) return null;

            // Return the created ToDoItem (with the new ID)
            return await response.Content.ReadFromJsonAsync<ToDoItem>();
        }

        public async Task<bool> UpdateAsync(ToDoItem item)
        {
            var response = await _http.PutAsJsonAsync($"api/todo/{item.Id}", item);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/todo/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
