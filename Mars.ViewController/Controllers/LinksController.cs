using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class LinksController : MarsControllerBase
    {
        #region Constructor

        public LinksController(IReaderLimit<Link> lienManager)
        {
            _lienManager = lienManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Link> _lienManager;

        #endregion

        #region Methods

        // GET: /Links/
        public ActionResult Index()
        {
            IEnumerable<Link> liens = _lienManager.Get(1, 10);
            return View(liens);
        }

        // GET: /Links/Manage
        public ActionResult Manage()
        {
            return View();
        }

        // POST: /Link/Manage
        [HttpPost]
        public ActionResult Manage(Link lien)
        {
            try
            {
                // TODO: Add insert logic here
                _lienManager.Add(lien, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(lien);
            }
        }

        // GET: /Links/Edit/5
        public ActionResult Manage(int id)
        {
            Link lien = _lienManager.Get(id);
            return View(lien);
        }

        // POST: /Link/Edit/5
        [HttpPost]
        public ActionResult Manage(int id, Link lien)
        {
            try
            {
                // TODO: Add update logic here
                _lienManager.Edit(lien, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(lien);
            }
        }

        // GET: /Links/Delete/5
        public ActionResult Delete(int id)
        {
            Link lien = _lienManager.Get(id);
            return RedirectToAction("Index");
        }

        // POST: /Link/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Link lien)
        {
            try
            {
                // TODO: Add delete logic here
                _lienManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

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