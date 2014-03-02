using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class ShowsController : MarsControllerBase
    {
        #region Constructor

        public ShowsController(IReaderLimit<Show> salonManager)
        {
            _salonManager = salonManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Show> _salonManager;

        #endregion

        #region Methods

        // GET: /Shows/
        public ActionResult Index()
        {
            IEnumerable<Show> salons = _salonManager.Get(1, 10);
            return View(salons);
        }

        // GET: /Shows/Create
        public ActionResult Manage()
        {
            return View();
        }

        // GET: /Shows/Edit/5
        public ActionResult Manage(int id)
        {
            Show salon = _salonManager.Get(id);
            return View(salon);
        }

        // POST: /Shows/Create
        [HttpPost]
        public ActionResult Manage(Show salon)
        {
            try
            {
                // TODO: Add insert logic here
                _salonManager.Add(salon, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(salon);
            }
        }
        
        // POST: /Shows/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Show salon)
        {
            try
            {
                // TODO: Add delete logic here
                _salonManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        #endregion
    }
}