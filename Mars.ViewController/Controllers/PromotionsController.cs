using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class PromotionsController : MarsControllerBase
    {
        #region Constructor

        public PromotionsController(IReader<Promotion> promotionManager)
        {
            _promotionManager = promotionManager;
        }

        #endregion

        #region Attributes

        private readonly IReader<Promotion> _promotionManager;

        #endregion

        #region Methods

        // GET: /Promotions/
        public ActionResult Index()
        {
            IEnumerable<Promotion> promotions = _promotionManager.Get();
            return View(promotions);
        }

        // GET: /Promotions/Create
        public ActionResult Manage()
        {
            return View();
        }

        // GET: /Promotions/Edit/5
        public ActionResult Manage(int id)
        {
            Promotion promotion = _promotionManager.Get(id);
            return View(promotion);
        }

        // POST: /Promotions/Create
        [HttpPost]
        public ActionResult Manage(Promotion promotion)
        {
            try
            {
                // TODO: Add insert logic here
                _promotionManager.Add(promotion, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(promotion);
            }
        }

        // GET: /Promotions/Delete/5
        public ActionResult Delete(int id)
        {
            Promotion promotion = _promotionManager.Get(id);
            return View("Index");
        }

        #endregion
    }
}