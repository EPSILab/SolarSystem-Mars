using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class PublicitesController : MarsControllerBase
    {
        #region Constructor

        public PublicitesController(IReader<Publicite> publiciteManager)
        {
            _publiciteManager = publiciteManager;
        }

        #endregion

        #region Attributes

        private readonly IReader<Publicite> _publiciteManager;

        #endregion

        #region Methods

        // GET: /Publicites/
        public ActionResult Index()
        {
            IEnumerable<Publicite> publicites = _publiciteManager.Get();
            return View(publicites);
        }

        // GET: /Publicites/Details/5
        public ActionResult Details(int id)
        {
            Publicite publicite = _publiciteManager.Get(id);
            return View(publicite);
        }

        // GET: /Publicites/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Publicites/Create
        [HttpPost]
        public ActionResult Create(Publicite publicite)
        {
            try
            {
                // TODO: Add insert logic here
                _publiciteManager.Add(publicite, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(publicite);
            }
        }

        // GET: /Publicites/Edit/5
        public ActionResult Edit(int id)
        {
            Publicite publicite = _publiciteManager.Get(id);
            return View(publicite);
        }

        // POST: /Publicites/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Publicite publicite)
        {
            try
            {
                // TODO: Add update logic here
                _publiciteManager.Edit(publicite, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(publicite);
            }
        }

        // GET: /Publicites/Delete/5
        public ActionResult Delete(int id)
        {
            Publicite publicite = _publiciteManager.Get(id);
            return View(publicite);
        }

        // POST: /Publicites/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Publicite publicite)
        {
            try
            {
                // TODO: Add delete logic here
                _publiciteManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(publicite);
            }
        }

        #endregion
    }
}