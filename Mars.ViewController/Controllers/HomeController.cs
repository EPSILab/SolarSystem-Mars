using System;
using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Helpers;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels;
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

        #region Methods

        /// <summary>
        /// GET: /Home/
        /// </summary>
        public ActionResult Index()
        {
            if (AuthProvider.IsSignIn)
                return Redirect(Url.Action("LogOn"));

            return View();
        }

        /// <summary>
        /// GET : /Home/LogOn
        /// </summary>
        [WebserviceAuthorize]
        public ViewResult LogOn()
        {
            LoginViewModel vm = AuthProvider.LoginViewModel;
            Member userConnected = _model.Login(vm.Username, vm.PasswordCrypted);
            return View(userConnected);
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

            return Redirect(Url.Action("Index"));
        }

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
                string password = PasswordEncoderHelper.Encode(vm.Password);

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

        /// <summary>
        ///  GET : /Home/LostPassword
        /// </summary>
        public ActionResult LostPassword()
        {
            return View();
        }

        /// <summary>
        /// GET : /Home/EditProfile
        /// </summary>
        [WebserviceAuthorize]
        public ActionResult EditProfile()
        {
            return View();
        }

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