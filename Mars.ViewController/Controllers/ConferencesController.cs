using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class ConferencesController : MarsControllerBase
    {
        #region Constructor

        public ConferencesController(IReaderLimit<Conference> conferenceManager)
        {
            _conferenceManager = conferenceManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Conference> _conferenceManager;

        #endregion

        #region Methods

        // GET: /Conferences/
        public ActionResult Index()
        {
            IEnumerable<Conference> conferences = _conferenceManager.Get(1, 10);
            return View(conferences);
        }

        // GET: /Conferences/Details/5
        public ActionResult Details(int id)
        {
            Conference conference = _conferenceManager.Get(id);
            return View(conference);
        }

        // GET: /Conferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Conferences/Create
        [HttpPost]
        public ActionResult Create(Conference conference)
        {
            try
            {
                // TODO: Add insert logic here
                _conferenceManager.Add(conference, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(conference);
            }
        }

        // GET: /Conferences/Edit/5
        public ActionResult Edit(int id)
        {
            Conference conference = _conferenceManager.Get(id);
            return View(conference);
        }

        // POST: /Conferences/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Conference conference)
        {
            try
            {
                // TODO: Add update logic here
                _conferenceManager.Edit(conference, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(conference);
            }
        }

        // GET: /Conferences/Delete/5
        public ActionResult Delete(int id)
        {
            Conference conference = _conferenceManager.Get(id);
            return View(conference);
        }

        // POST: /Conferences/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Conference conference)
        {
            try
            {
                // TODO: Add delete logic here
                _conferenceManager.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(conference);
            }
        }

        #endregion
    }
}