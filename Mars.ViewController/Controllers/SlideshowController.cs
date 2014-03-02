using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class SlideshowController : MarsControllerBase
    {
        #region Constructor

        public SlideshowController(IReader<Slide> publiciteManager)
        {
            _publiciteManager = publiciteManager;
        }

        #endregion

        #region Attributes

        private readonly IReader<Slide> _publiciteManager;

        #endregion

        #region Methods

        // GET: /Slides/
        public ActionResult Index()
        {
            IEnumerable<Slide> publicites = _publiciteManager.Get();
            return View(publicites);
        }

        // GET: /Slides/Create
        public ActionResult Manage()
        {
            return View();
        }

        // POST: /Slides/Create
        [HttpPost]
        public ActionResult Manage(Slide publicite)
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

        // GET: /Slides/Edit/5
        public ActionResult Manage(int id)
        {
            Slide publicite = _publiciteManager.Get(id);
            return View(publicite);
        }

        // POST: /Slides/Edit/5
        [HttpPost]
        public ActionResult Manage(int id, Slide publicite)
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

        // GET: /Slides/Delete/5
        public ActionResult Delete(int id)
        {
            Slide publicite = _publiciteManager.Get(id);
            return RedirectToAction("Index");
        }

        // POST: /Slides/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Slide publicite)
        {
            try
            {
                // TODO: Add delete logic here
                _publiciteManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

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