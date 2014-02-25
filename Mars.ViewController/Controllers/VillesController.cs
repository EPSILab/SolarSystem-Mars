using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class VillesController : MarsControllerBase
    {
        #region Constructor

        public VillesController(IReader<Ville> villeManager)
        {
            _villeManager = villeManager;
        }

        #endregion

        #region Attributes

        private readonly IReader<Ville> _villeManager;

        #endregion

        #region Methods

        // GET: /Villes/
        public ActionResult Index()
        {
            IEnumerable<Ville> villes = _villeManager.Get();
            return View(villes);
        }

        // GET: /Villes/Details/5
        public ActionResult Details(int id)
        {
            Ville ville = _villeManager.Get(id);
            return View(ville);
        }

        // GET: /Villes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Villes/Create
        [HttpPost]
        public ActionResult Create(Ville ville)
        {
            try
            {
                // TODO: Add insert logic here
                _villeManager.Add(ville, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ville);
            }
        }

        // GET: /Villes/Edit/5
        public ActionResult Edit(int id)
        {
            Ville ville = _villeManager.Get(id);
            return View(ville);
        }

        // POST: /Villes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Ville ville)
        {
            try
            {
                // TODO: Add update logic here
                _villeManager.Edit(ville, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ville);
            }
        }

        // GET: /Villes/Delete/5
        public ActionResult Delete(int id)
        {
            Ville ville = _villeManager.Get(id);
            return View(ville);
        }

        // POST: /Villes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Ville ville)
        {
            try
            {
                // TODO: Add delete logic here
                _villeManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(ville);
            }
        }

        #endregion
    }
}