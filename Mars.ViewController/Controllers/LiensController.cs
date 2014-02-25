using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class LiensController : MarsControllerBase
    {
        #region Constructor

        public LiensController(IReaderLimit<Lien> lienManager)
        {
            _lienManager = lienManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Lien> _lienManager;

        #endregion

        #region Methods

        // GET: /Liens/
        public ActionResult Index()
        {
            IEnumerable<Lien> liens = _lienManager.Get(1, 10);
            return View(liens);
        }

        // GET: /Liens/Details/5
        public ActionResult Details(int id)
        {
            Lien lien = _lienManager.Get(id);
            return View(lien);
        }

        // GET: /Liens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Lien/Create
        [HttpPost]
        public ActionResult Create(Lien lien)
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

        // GET: /Liens/Edit/5
        public ActionResult Edit(int id)
        {
            Lien lien = _lienManager.Get(id);
            return View(lien);
        }

        // POST: /Lien/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Lien lien)
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

        // GET: /Liens/Delete/5
        public ActionResult Delete(int id)
        {
            Lien lien = _lienManager.Get(id);
            return View(lien);
        }

        // POST: /Lien/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Lien lien)
        {
            try
            {
                // TODO: Add delete logic here
                _lienManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(lien);
            }
        }

        #endregion
    }
}