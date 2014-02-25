using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class ProjetsController : MarsControllerBase
    {
        #region Constructor

        public ProjetsController(IReaderLimit<Projet> projetManager)
        {
            _projetManager = projetManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Projet> _projetManager;

        #endregion

        #region Methods

        // GET: /Projets/
        public ActionResult Index()
        {
            IEnumerable<Projet> projets = _projetManager.Get(1, 10);
            return View(projets);
        }

        // GET: /Projets/Details/5
        public ActionResult Details(int id)
        {
            Projet projet = _projetManager.Get(id);
            return View(projet);
        }

        // GET: /Projets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Projets/Create

        [HttpPost]
        public ActionResult Create(Projet projet)
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

        // GET: /Projets/Edit/5
        public ActionResult Edit(int id)
        {
            Projet projet = _projetManager.Get(id);
            return View(projet);
        }

        // POST: /Projets/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Projet projet)
        {
            try
            {
                // TODO: Add update logic here
                _projetManager.Edit(projet, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(projet);
            }
        }

        // GET: /Projets/Delete/5
        public ActionResult Delete(int id)
        {
            Projet projet = _projetManager.Get(id);
            return View(projet);
        }

        // POST: /Projets/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Projet projet)
        {
            try
            {
                // TODO: Add delete logic here
                _projetManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(projet);
            }
        }

        #endregion
    }
}