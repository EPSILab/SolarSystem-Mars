using Ninject;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    public abstract class MarsControllerBase : Controller
    {
        [Inject]
        public IAuthProvider AuthProvider { get; set; }
    }
}