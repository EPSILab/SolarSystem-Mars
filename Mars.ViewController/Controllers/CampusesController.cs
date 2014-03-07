using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class CampusesController : MarsControllerBase
    {
        #region Constructor

        public CampusesController(IReader<Campus> villeManager)
        {
            _villeManager = villeManager;
        }

        #endregion

        #region Attributes

        private readonly IReader<Campus> _villeManager;

        #endregion

        #region Methods

        /// <summary>
        /// GET: /Campuses/
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /Campuses/Manage
        /// </summary>
        public ActionResult Manage(int id)
        {
            return View();
        }

        // POST: /Campuses/Create
        [HttpPost]
        public ActionResult Create(Campus ville)
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

        // GET: /Campuses/Edit/5
        public ActionResult Edit(int id)
        {
            Campus ville = _villeManager.Get(id);
            return View(ville);
        }

        // POST: /Campuses/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Campus ville)
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

        // GET: /Campuses/Delete/5
        public ActionResult Delete(int id)
        {
            Campus ville = _villeManager.Get(id);
            return View(ville);
        }

        // POST: /Campuses/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Campus ville)
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