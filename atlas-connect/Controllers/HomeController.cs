using System;
using System.Dynamic;
using System.Web;
using Atlassian.Connect;
using Atlassian.Connect.Jwt;

namespace atlas_connect.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        public ActionResult InstalledCallback()
        {
            SecretKeyPersister.SaveSecretKey(Request);

            return Content(String.Empty);
        }

        [JwtAuthentication]
        public ActionResult Index()
        {
            var client = Request.CreateConnectHttpClient("com.example.myaddon");

            var response = client.GetAsync("rest/api/latest/project").Result;
            var results = response.Content.ReadAsStringAsync().Result;

            dynamic model = new ExpandoObject();
            model.projects = results;
            return View(model);
        }

        [HttpGet]
        public ActionResult Descriptor()
        {
            var descriptor = new ConnectDescriptor()
            {
                name = "Hello World",
                description = "Atlassian Connect add-on",
                key = "com.example.myaddon",
                vendor = new ConnectDescriptorVendor()
                {
                    name = "Example, Inc.",
                    url = "http://example.com"
                },
                authentication = new
                {
                    type = "jwt"
                },
                lifecycle = new
                {
                    installed = "/installed"
                },
                modules = new
                {
                    generalPages = new[]
                    {
                        new
                        {
                            url = "/helloworld.html",
                            key = "hello-world",
                            location = "system.top.navigation.bar",
                            name = new
                            {
                                value = "Greeting"
                            }
                        }
                    }
                }
            };

            descriptor.SetBaseUrlFromRequest(Request);
            descriptor.scopes.Add("READ");

            return Json(descriptor, JsonRequestBehavior.AllowGet);
        }
    }
}