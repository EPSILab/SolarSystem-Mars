using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class ClassesController : MarsControllerBase
    {
        #region Constructor

        public ClassesController(IReader<Classe> classeManager)
        {
            _classeManager = classeManager;
        }

        #endregion

        #region Attributes

        private readonly IReader<Classe> _classeManager;

        #endregion

        #region Methods

        // GET: /Classes/
        public ActionResult Index()
        {
            IEnumerable<Classe> classes = _classeManager.Get();
            return View(classes);
        }

        // GET: /Classes/Details/5
        public ActionResult Details(int id)
        {
            Classe classe = _classeManager.Get(id);
            return View(classe);
        }

        // GET: /Classes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Classes/Create
        [HttpPost]
        public ActionResult Create(Classe classe)
        {
            try
            {
                // TODO: Add insert logic here
                _classeManager.Add(classe, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(classe);
            }
        }

        // GET: /Classes/Edit/5
        public ActionResult Edit(int id)
        {
            Classe classe = _classeManager.Get(id);
            return View(classe);
        }

        // POST: /Classes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Classe classe)
        {
            try
            {
                // TODO: Add update logic here
                _classeManager.Edit(classe, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(classe);
            }
        }

        // GET: /Classes/Delete/5
        public ActionResult Delete(int id)
        {
            Classe classe = _classeManager.Get(id);
            return View(classe);
        }

        // POST: /Classes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Classe classe)
        {
            try
            {
                // TODO: Add delete logic here
                _classeManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(classe);
            }
        }

        #endregion
    }
}