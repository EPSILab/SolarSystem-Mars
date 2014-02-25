using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class SalonsController : MarsControllerBase
    {
        #region Constructor

        public SalonsController(IReaderLimit<Salon> salonManager)
        {
            _salonManager = salonManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Salon> _salonManager;

        #endregion

        #region Methods

        // GET: /Salons/
        public ActionResult Index()
        {
            IEnumerable<Salon> salons = _salonManager.Get(1, 10);
            return View(salons);
        }

        // GET: /Salons/Details/5
        public ActionResult Details(int id)
        {
            Salon salon = _salonManager.Get(id);
            return View(salon);
        }

        // GET: /Salons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Salons/Create
        [HttpPost]
        public ActionResult Create(Salon salon)
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

        // GET: /Salons/Edit/5
        public ActionResult Edit(int id)
        {
            Salon salon = _salonManager.Get(id);
            return View(salon);
        }

        // POST: /Salons/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Salon salon)
        {
            try
            {
                // TODO: Add update logic here
                _salonManager.Edit(salon, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(salon);
            }
        }

        // GET: /Salons/Delete/5
        public ActionResult Delete(int id)
        {
            Salon salon = _salonManager.Get(id);
            return View(salon);
        }

        // POST: /Salons/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Salon salon)
        {
            try
            {
                // TODO: Add delete logic here
                _salonManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(salon);
            }
        }

        #endregion
    }
}