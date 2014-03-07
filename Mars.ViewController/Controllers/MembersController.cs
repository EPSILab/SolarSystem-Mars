using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class MembersController : MarsControllerBase
    {
        #region Constructor

        public MembersController(IReaderLimit<Member> membreManager)
        {
            _membreManager = membreManager;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<Member> _membreManager;

        #endregion

        #region Methods

        // GET: /Members/
        public ViewResult Index()
        {
            IEnumerable<Member> membres = _membreManager.Get(1, 20);
            return View(membres);
        }

        // GET: /Members/Details/5
        public ActionResult Details(int id)
        {
            Member membre = _membreManager.Get(id);
            return View(membre);
        }

        // GET : /Members/Valid
        public ActionResult Valid()
        {
            return View();
        }

        // GET: /Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Members/Create
        [HttpPost]
        public ActionResult Create(Member membre)
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

        // GET: /Members/Edit/5
        public ActionResult Edit(int id)
        {
            Member membre = _membreManager.Get(id);
            return View(membre);
        }

        // POST: /Members/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Member membre)
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

        // GET: /Members/Delete/5
        public ActionResult Delete(int id)
        {
            Member membre = _membreManager.Get(id);
            return View(membre);
        }

        // POST: /Members/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Member membre)
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