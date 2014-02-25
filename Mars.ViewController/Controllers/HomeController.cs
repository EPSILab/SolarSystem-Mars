using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using SolarSystem.Mars.Model;
using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Helpers;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.ViewModels;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    public class HomeController : MarsControllerBase
    {
        #region Constructor

        public HomeController(ILogin model, IAvailable<Classe> modelClasse, IReader<Ville> modelVille)
        {
            _model = model;
            _modelClasse = modelClasse;
            _modelVille = modelVille;
        }

        #endregion

        #region Attributes

        private readonly ILogin _model;
        private readonly IAvailable<Classe> _modelClasse;
        private readonly IReader<Ville> _modelVille;

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
            Membre userConnected = _model.Login(AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);
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
            ViewBag.Classes = _modelClasse.GetAvailables();
            ViewBag.Villes = _modelVille.Get();

            var vm = new RegisterViewModel();
            return View(vm);
        }

        /// <summary>
        /// POST : /Home/Register
        /// </summary>
        /// <param name="viewModel">Contains all informations about the new user</param>
        [HttpPost]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            IEnumerable<Classe> classes = _modelClasse.GetAvailables();
            IEnumerable<Ville> villes = _modelVille.Get();

            if (!ModelState.IsValid)
            {
                ViewBag.Classes = classes;
                ViewBag.Villes = villes;

                return View();
            }
            else
            {
                var membre = new Membre
                {
                    Classe = classes.First(c => c.Code_Classe == viewModel.Code_Classe),
                    Email_EPSI = viewModel.EmailEPSI,
                    Email_perso = viewModel.EmailPerso,
                    Mot_de_passe = PasswordEncoderHelper.Encode(viewModel.Password),
                    Motivations = viewModel.Motivations,
                    Nom = viewModel.Surname,
                    Prenom = viewModel.FirstName,
                    Presentation = viewModel.Presentation,
                    Pseudo = viewModel.Username,
                    Site_web = viewModel.WebsiteUrl,
                    Telephone_mobile = viewModel.PhoneNumber,
                    URL_Facebook = viewModel.FacebookUrl,
                    URL_LinkedIn = viewModel.LinkedInUrl,
                    URL_Twitter = viewModel.TwitterUrl,
                    URL_Viadeo = viewModel.ViadeoUrl,
                    Ville = villes.First(v => v.Code_Ville == viewModel.Code_Ville),
                    Ville_origine = viewModel.City
                };

                //membre.Image

                _model.Register(membre);

                return Redirect(Url.Action("Index")); 
            }
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