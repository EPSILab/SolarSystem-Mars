using SolarSystem.Mars.Model.Helpers;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    public class HomeController : MarsControllerBase
    {
        #region Constructor

        public HomeController(ILogin model, IAvailable<Promotion> modelPromotion, IReader<Campus> modelCampus)
        {
            _model = model;
            _modelPromotions = modelPromotion;
            _modelCampuses = modelCampus;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly ILogin _model;

        /// <summary>
        /// Model for promotions
        /// </summary>
        private readonly IAvailable<Promotion> _modelPromotions;

        /// <summary>
        /// Model for campuses
        /// </summary>
        private readonly IReader<Campus> _modelCampuses;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Home/
        /// </summary>
        public ActionResult Index()
        {
            if (AuthProvider.IsSignIn)
                return RedirectToAction("LogOn");

            if (AuthProvider.Error)
                ViewBag.ExceptionMessage = ErrorRessources.InvalidUsernameOrPassword;

            return View();
        }

        #endregion

        #region LogOn methods

        /// <summary>
        /// GET : /Home/LogOn
        /// </summary>
        [WebserviceAuthorize]
        public ViewResult LogOn()
        {
            LoginViewModel vm = AuthProvider.LoginViewModel;
            Member userConnected = _model.Login(vm.Username, vm.PasswordCrypted);
            Session.Add("CurrentUser", userConnected);
            return View();
        }

        /// <summary>
        /// POST : /Home/LogOn
        /// </summary>
        /// <param name="viewModel">Contains username and password</param>
        [HttpPost]
        public ActionResult LogOn(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
                AuthProvider.SignIn(viewModel);

            return RedirectToAction("Index");
        }

        #endregion

        #region Register methods

        /// <summary>
        ///  GET : /Home/Register
        /// </summary>

        public ActionResult Register()
        {
            // Get promotions and campuses for register
            ViewBag.Promotions = _modelPromotions.GetAvailables();
            ViewBag.Campuses = _modelCampuses.Get();

            MemberViewModel vm = new MemberViewModel();
            return View(vm);
        }

        /// <summary>
        /// POST : /Home/Register
        /// </summary>
        /// <param name="vm">Contains all informations about the new user</param>
        /// <param name="file">Member avatar uploaded</param>
        [HttpPost]
        public ActionResult Register(MemberViewModel vm, HttpPostedFileBase file)
        {
            // Get promotions and campuses
            IEnumerable<Promotion> promotions = _modelPromotions.GetAvailables();
            IEnumerable<Campus> campuses = _modelCampuses.Get();

            if (ModelState.IsValid)
            {
                Member member = new Member
                {
                    Campus = campuses.First(v => v.Id == vm.IdCampus),
                    CityFrom = vm.CityFrom,
                    EPSIEmail = vm.EPSIEmail,
                    FacebookUrl = vm.FacebookUrl,
                    FirstName = vm.FirstName,
                    GitHubUrl = vm.GitHubUrl,
                    LastName = vm.LastName,
                    LinkedInUrl = vm.LinkedInUrl,
                    PersonalEmail = vm.PersonalEmail,
                    PhoneNumber = vm.PhoneNumber,
                    Presentation = vm.Presentation,
                    Promotion = promotions.First(c => c.Id == vm.IdPromotion),
                    TwitterUrl = vm.TwitterUrl,
                    ViadeoUrl = vm.ViadeoUrl,
                    Username = vm.Username,
                    Website = vm.Website
                };

                // Image management
                string imagePath = string.Format("{0}/Images/Members/{1}", ContentRessources.EPSILabUrl, file.FileName);
                file.SaveAs(imagePath);
                member.ImageUrl = imagePath;

                // Password
                string password = PasswordEncoder.Encode(vm.Password);

                try
                {
                    _model.Register(member, password);
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex.Message;
                    return View(vm);
                }

                return Redirect(Url.Action("Index"));
            }

            ViewBag.Promotions = promotions;
            ViewBag.Campuses = campuses;
            return View(vm);
        }

        #endregion

        #region LostPassword regions

        /// <summary>
        ///  GET : /Home/LostPassword
        /// </summary>
        public ActionResult LostPassword()
        {
            return View();
        }

        /// <summary>
        ///  POST : /Home/LostPassword
        /// </summary>
        [HttpPost]
        public ActionResult LostPassword(LostPasswordViewModel vm)
        {
            try
            {
                _model.RequestLostPassword(vm.Username, vm.Email);
                return Json(new { success = true, message = MessagesResources.LostPasswordRequestSent });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region EditProfile regions

        /// <summary>
        /// GET : /Home/EditProfile
        /// </summary>
        [WebserviceAuthorize]
        public ActionResult EditProfile()
        {
            return View();
        }

        #endregion

        #region LogOut regions

        /// <summary>
        /// GET : /Home/LogOut
        /// </summary>
        [WebserviceAuthorize]
        public ActionResult LogOut()
        {
            AuthProvider.SignOut();
            return Redirect(Url.Action("Index"));
        }

        #endregion
    }
}