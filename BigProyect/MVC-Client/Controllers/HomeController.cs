using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_Client.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC_Client.Controllers
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
            return View();
        }

        [HttpGet]
        public IActionResult Init()
        {
            string html = string.Empty;
            string url = @"http://localhost:27015/v1/Init";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            if (html == "El servicio ya esta inicializado")
                return View("Index","Error, el telescopio ya estaba iniciado");
            else
                return View("Index","El servicio se ha inicializado");
        }

        private static readonly HttpClient client = new HttpClient();
        //[HttpPost]
        public async Task<IActionResult> InfoAsync()
        {
            var values = new Dictionary<string, string>{};

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://localhost:27015/v1/Info", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString == "No esta iniciado el telescopio")
                return View("Index","Error, el telescopio no esta iniciado");
            else
            {
                var jsonOutput = JsonConvert.SerializeObject(responseString);
                return View("Index",jsonOutput.ToString());
            }
        }
        [HttpGet]
        public IActionResult Finish()
        {
            string html = string.Empty;
            string url = @"http://localhost:27015/v1/Finish";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            if (html == "Primero tiene que iniciar un servicio")
                return View("Index","Error, el telescopio no esta iniciado");
            else
                return View("Index","Se finalizo el proceso");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
