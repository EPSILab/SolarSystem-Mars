using SolarSystem.Mars.Model.Helpers;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Exceptions;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

namespace SolarSystem.Mars.ViewController.Controllers
{
    public class HomeController : MarsControllerBase
    {
        #region Constructor

        public HomeController(ILogin model, IReader<Member> modelMember, IAvailable<Promotion> modelPromotion, IReader<Campus> modelCampus)
        {
            _model = model;
            _modelMember = modelMember;
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
        /// 
        /// </summary>
        private readonly IReader<Member> _modelMember;

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

        #region anonymous methods

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

            RegisterViewModel vm = new RegisterViewModel();
            return View(vm);
        }

        /// <summary>
        /// POST : /Home/Register
        /// </summary>
        /// <param name="vm">Contains all informations about the new user</param>
        /// <param name="file">Member avatar uploaded</param>
        [HttpPost]
        public ActionResult Register(RegisterViewModel vm, HttpPostedFileBase file)
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
                    Website = vm.Website,
                    Active = vm.IsActive,
                    Role = Role.Inactive,
                    Status = "Membre",
                    ImageUrl = vm.ImageRemoteUrl
                };

                // Image management
                SendImageToServer(vm, file, member);

                // Password
                string password = PasswordEncoder.Encode(vm.Password);

                try
                {
                    _model.Register(member, password);
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex.Message;
                    ViewBag.Promotions = promotions;
                    ViewBag.Campuses = campuses;

                    return View(vm);
                }

                return Redirect(Url.Action("Index"));
            }

            ViewBag.Promotions = promotions;
            ViewBag.Campuses = campuses;
            return View(vm);
        }


        #region LostPassword methods

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
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                _model.RequestLostPassword(vm.Username, vm.Email);
                return Json(new { success = true, message = MessagesResources.LostPasswordRequestSent });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #endregion

        #endregion

        #region logged in methods

        #region ResetPassword methods

        /// <summary>
        ///  GET : /Home/ResetPassword
        /// </summary>
        [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
        public ActionResult ResetPassword()
        {
            return View();
        }

        /// <summary>
        ///  POST : /Home/ResetPassword
        /// </summary>
        [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
        [HttpPost]
        public ActionResult ResetPassword(ResetPasswordViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                _model.SetNewPasswordAfterLost(vm.Username, vm.Password, vm.Key);
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

        #region EditProfile methods

        /// <summary>
        /// GET : /Home/EditProfile
        /// </summary>
        [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
        public ActionResult EditProfile()
        {
            // Get promotions and campuses to edit profile
            ViewBag.Promotions = _modelPromotions.GetAvailables();
            ViewBag.Campuses = _modelCampuses.Get();

            Member member = _modelMember.Get().FirstOrDefault(m => m.Username == AuthProvider.LoginViewModel.Username);
            MemberViewModel viewModel = new MemberViewModel(member);

            return View(viewModel);
        }

        /// <summary>
        /// POST : /Home/EditProfile
        /// </summary>
        /// <param name="vm">Contains all informations about the user</param>
        /// <param name="file">Member avatar uploaded</param>
        [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
        [HttpPost]
        public ActionResult EditProfile(MemberViewModel vm, HttpPostedFileBase file)
        {
            // Get promotions and campuses
            IEnumerable<Promotion> promotions = _modelPromotions.GetAvailables();
            IEnumerable<Campus> campuses = _modelCampuses.Get();

            if (ModelState.IsValid)
            {
                Member member = new Member
                {
                    Id = vm.Id,
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
                    Website = vm.Website,
                    Active = vm.IsActive,
                    ImageUrl = vm.ImageRemoteUrl
                };

                // Image management
                SendImageToServer(vm, file, member);

                try
                {
                    _model.Edit(member, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex.Message;
                    ViewBag.Promotions = promotions;
                    ViewBag.Campuses = campuses;

                    return View(vm);
                }

                return Redirect(Url.Action("Index"));
            }

            ViewBag.Promotions = promotions;
            ViewBag.Campuses = campuses;
            return View(vm);
        }

        #endregion

        #region EditPassword methods

        [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
        public ActionResult EditPassword()
        {
            return View(new EditPasswordViewModel());
        }

        [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
        [HttpPost]
        public ActionResult EditPassword(EditPasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Member member = _modelMember.Get().FirstOrDefault(m => m.Username == AuthProvider.LoginViewModel.Username);

                    if (member == null)
                        throw new ArgumentNullException("member");

                    // Crypted Old Password
                    string oldPassword = PasswordEncoder.Encode(vm.OldPassword);

                    // Crypted New Password
                    string newPassword = PasswordEncoder.Encode(vm.NewPassword);

                    // Crypted Confirm New Password
                    string confirmNewPassword = PasswordEncoder.Encode(vm.ConfirmNewPassword);

                    if (oldPassword != AuthProvider.LoginViewModel.PasswordCrypted || newPassword != confirmNewPassword)
                        return View(vm);

                    // Set new password
                    _model.ChangePassword(member.Username, oldPassword, vm.NewPassword);
                    AuthProvider.LoginViewModel.PasswordNonCrypted = vm.NewPassword;
                }
                catch (Exception ex)
                {
                    ViewBag.Exception = ex.Message;

                    return View(vm);
                }

                return Redirect(Url.Action("Index"));
            }

            return View(vm);
        }

        #endregion

        #region LogOut methods

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

        #endregion

        #endregion

        #region General Methods

        /// <summary>
        /// Send Image to the server
        /// </summary>
        /// <param name="vm">MemberViewModel corresponding to the member</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="member">Member that will get image</param>
        private void SendImageToServer(MemberViewModel vm, HttpPostedFileBase file, Member member)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/Conference/{0}", file.FileName);
                file.SaveAs(imagePath);
                member.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                member.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}