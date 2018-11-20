using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DemoApiClient.Api;
using Microsoft.AspNetCore.Mvc;
using DemoApiClient.Models;

namespace DemoApiClient.Controllers
{
    public class HomeController : Controller
    {
        private ApiService service = new ApiService();
        private Api Api = new Api();


        public async Task<IActionResult> Index()
        {
            ViewBag.APIMethod = await Api.GetApiStatus();
            
            return View("Index", await service.GetAllTodoItemsAsync());
        }

        public async Task<IActionResult> About(long id)
        {
            return View("About", await service.GetTodoItemByIdAsync(id));
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
