using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class MembresController : MarsControllerBase
    {
        #region Constructor

        public MembresController(IReaderLimit<Membre> membreManager)
        {
            _membreManager = membreManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Membre> _membreManager;

        #endregion

        #region Methods

        // GET: /Membres/
        public ViewResult Index()
        {
            IEnumerable<Membre> membres = _membreManager.Get(1, 20);
            return View(membres);
        }

        // GET: /Membres/Details/5
        public ActionResult Details(int id)
        {
            Membre membre = _membreManager.Get(id);
            return View(membre);
        }

        // GET : /Membres/Valid
        public ActionResult Valid()
        {
            return View();
        }

        // GET: /Membres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Membres/Create
        [HttpPost]
        public ActionResult Create(Membre membre)
        {
            try
            {
                // TODO: Add insert logic here
                _membreManager.Add(membre, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(membre);
            }
        }

        // GET: /Membres/Edit/5
        public ActionResult Edit(int id)
        {
            Membre membre = _membreManager.Get(id);
            return View(membre);
        }

        // POST: /Membres/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Membre membre)
        {
            try
            {
                // TODO: Add update logic here
                _membreManager.Edit(membre, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(membre);
            }
        }

        // GET: /Membres/Delete/5
        public ActionResult Delete(int id)
        {
            Membre membre = _membreManager.Get(id);
            return View(membre);
        }

        // POST: /Membres/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Membre membre)
        {
            try
            {
                // TODO: Add delete logic here
                _membreManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(membre);
            }
        }

        #endregion
    }
}