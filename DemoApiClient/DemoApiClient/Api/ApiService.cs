using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DemoApiClient.Models;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Newtonsoft.Json;

namespace DemoApiClient.Api
{
    public class ApiService
    {
        private readonly string uri = "http://localhost:5854/api/todo";

        public async Task<IEnumerable<TodoItem>> GetAllTodoItemsAsync()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(
                        await httpClient.GetStringAsync(uri)
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new List<TodoItem>();
                }
            }
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(long id)
        {
            string filterId = "/" + id;

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    return JsonConvert.DeserializeObject<TodoItem>(
                        await httpClient.GetStringAsync(uri + filterId)
                    );
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return new TodoItem();
                }
            }
        }
    }
}
