using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Exceptions;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize(Role.Bureau)]
    public class CampusesController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public CampusesController(IReader<Campus> model)
        {
            _model = model;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReader<Campus> _model;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Campus/
        /// GET: /Campus/Index
        /// </summary>
        public ActionResult Index()
        {
            // Get Campus and tranform them in CampusViewModel
            IEnumerable<Campus> listCampus = _model.Get();
            IEnumerable<CampusViewModel> vm = listCampus.Select(campus => new CampusViewModel(campus));

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Campus/Manage : Create a new campus
        /// GET: /Campus/Manage/5 : Edit an existing campus
        /// </summary>
        /// <param name="id">Campus's Id to manage. If Id = 0, it is a new campus</param>
        public ActionResult Manage(int id = 0)
        {
            CampusViewModel vm;

            // Create a campus
            if (id == 0)
                vm = new CampusViewModel();
            // Edit an existing campus
            else
            {
                Campus campus = _model.Get(id);
                vm = new CampusViewModel(campus);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Campus/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        [HttpPost]
        public ActionResult Manage(CampusViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the campus to the server
                Campus campus = new Campus
                {
                    Id = vm.Id,
                    ContactEmail = vm.ContactEmail,
                    Place = vm.Place
                };

                // Save the campus
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(campus, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.CampusCreated;
                }
                else
                {
                    _model.Edit(campus, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.CampusUpdated;
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