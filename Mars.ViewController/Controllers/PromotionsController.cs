using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Exceptions;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize(Role.Bureau)]
    public class PromotionsController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public PromotionsController(IReader<Promotion> model)
        {
            _model = model;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReader<Promotion> _model;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Promotion/
        /// GET: /Promotion/Index
        /// </summary>
        public ActionResult Index()
        {
            // Get Promotion and tranform them in PromotionViewModel
            IEnumerable<Promotion> listPromotion = _model.Get();
            IEnumerable<PromotionViewModel> vm = listPromotion.Select(promotion => new PromotionViewModel(promotion));

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Promotion/Manage : Create a new promotion
        /// GET: /Promotion/Manage/5 : Edit an existing promotion
        /// </summary>
        /// <param name="id">Promotion's Id to manage. If Id = 0, it is a new promotion</param>
        public ActionResult Manage(int id = 0)
        {
            PromotionViewModel vm;

            // Create a promotion
            if (id == 0)
                vm = new PromotionViewModel();
            // Edit an existing promotion
            else
            {
                Promotion promotion = _model.Get(id);
                vm = new PromotionViewModel(promotion);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Promotion/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        [HttpPost]
        public ActionResult Manage(PromotionViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the promotion to the server
                Promotion promotion = new Promotion
                {
                    Id = vm.Id,
                    GraduationYear = vm.GraduationYear,
                    Name = vm.Name,
                    StillPresent = vm.StillPresent
                };

                // Save the promotion
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(promotion, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.PromotionCreated;
                }
                else
                {
                    _model.Edit(promotion, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.PromotionUpdated;
                }

                return RedirectToAction("Index");
            }
            catch (InvalidModelStateException ex)
            {
                ViewBag.ErrorMessage = ex.DisplayMessage;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View(vm);
        }

        #endregion
    }
}