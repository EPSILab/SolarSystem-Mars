using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class ProjectsController : MarsControllerBase
    {
        #region Constructor

        public ProjectsController(IReaderLimit<Project> projetManager)
        {
            _projetManager = projetManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Project> _projetManager;

        #endregion

        #region Methods

        // GET: /Projects/
        public ActionResult Index()
        {
            IEnumerable<Project> projets = _projetManager.Get(1, 10);
            return View(projets);
        }

        // GET: /Projects/Manage
        public ActionResult Manage()
        {
            return View();
        }

        // POST: /Projects/Create
        [HttpPost]
        public ActionResult Manage(Project projet)
        {
            try
            {
                // TODO: Add insert logic here
                _projetManager.Add(projet, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(projet);
            }
        }

        // GET: /Projects/Delete/5
        public ActionResult Delete(int id)
        {
            Project projet = _projetManager.Get(id);
            return View("Index");
        }

        #endregion
    }
}