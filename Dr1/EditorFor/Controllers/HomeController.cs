using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EditorFor.Models;
using Enum = EditorFor.Models.Enum;

namespace EditorFor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var testModel = new TestModel
            {
                IntProperty = 1,
                LongProperty = 2,
                BoolProperty = true,
                StringProperty = "Строка",
                EnumProperty = Enum.Option3,
                ClassProperty = new NestedClass
                {
                    NestedIntProperty = 9,
                    NestedStringProperty = "Ещё строка"
                }
            };
            return View(testModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}