using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Exceptions;
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
    [WebserviceAuthorize(Role.Bureau)]
    public class SlidesController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public SlidesController(IReaderLimit<Slide> model, IConstants constants)
            : base(constants)
        {
            _model = model;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReaderLimit<Slide> _model;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Slide/
        /// GET: /Slide/Index
        /// GET: /Slide/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get Slide and tranform them in SlideViewModel
            IEnumerable<Slide> listSlide = _model.Get(id, _constants.ItemsNumber);
            IEnumerable<SlideViewModel> vm = listSlide.Select(slide => new SlideViewModel(slide));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Slide/Manage : Create a new slide
        /// GET: /Slide/Manage/5 : Edit an existing slide
        /// </summary>
        /// <param name="id">Slide's Id to manage. If Id = 0, it is a new slide</param>
        public ActionResult Manage(int id = 0)
        {
            SlideViewModel vm;

            // Create a slide
            if (id == 0)
                vm = new SlideViewModel();
            // Edit an existing slide
            else
            {
                Slide slide = _model.Get(id);
                vm = new SlideViewModel(slide);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Slide/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(SlideViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the slide to the server
                Slide slide = new Slide
                {
                    Id = vm.Id,
                    ImageUrl = vm.ImageRemoteUrl,
                    IsPublished = vm.IsPublished,
                    Name = vm.Name,
                    Presentation = vm.Presentation,
                    Url = vm.Url
                };

                // Image management
                SendImageToServer(vm, file, slide);

                // Save the slide
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(slide, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.SlideCreated;
                }
                else
                {
                    _model.Edit(slide, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.SlideUpdated;
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

        #region Delete methods

        /// <summary>
        ///  GET: /Slide/Delete/1
        /// Delete an existing slide
        /// </summary>
        /// <param name="id">Slide Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                // TODO: Uncomment line bellow
                //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.SlideDeleted });
            }
            catch (Exception ex)
            {
                return Json(new { id = 0, success = false, message = ex.Message });
            }
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Send Image to the server
        /// </summary>
        /// <param name="vm">SlideViewModel corresponding to the slide</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="slide">Slide that will get image</param>
        private void SendImageToServer(SlideViewModel vm, HttpPostedFileBase file, Slide slide)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/Slide/{0}", file.FileName);
                file.SaveAs(imagePath);
                slide.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                slide.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}