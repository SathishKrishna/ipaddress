using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IpAddress.Models;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.IO;

namespace IpAddress.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            var ipaddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4();
            var url = string.Format("http://api.ipstack.com/{0}?access_key={1}", ipaddress, "888a0e378148f270341506f198009bce");
            WebRequest request = WebRequest.Create(url);
            WebResponse webResponse = request.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);
            var response = streamReader.ReadToEnd();
            ViewData["Response"] = response;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
