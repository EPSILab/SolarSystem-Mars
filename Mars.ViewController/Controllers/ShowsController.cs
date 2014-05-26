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
    [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
    public class ShowsController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public ShowsController(IReaderLimit<Show> model, IConstants constants)
            : base(constants)
        {
            _model = model;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReaderLimit<Show> _model;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Show/
        /// GET: /Show/Index
        /// GET: /Show/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get Show and tranform them in ShowViewModel
            IEnumerable<Show> listShow = _model.Get(id, _constants.ItemsNumber);
            IEnumerable<ShowViewModel> vm = listShow.Select(show => new ShowViewModel(show, AuthProvider));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Show/Manage : Create a new show
        /// GET: /Show/Manage/5 : Edit an existing show
        /// </summary>
        /// <param name="id">Show's Id to manage. If Id = 0, it is a new show</param>
        public ActionResult Manage(int id = 0)
        {
            ShowViewModel vm;

            // Create a show
            if (id == 0)
                vm = new ShowViewModel();
            // Edit an existing show
            else
            {
                Show show = _model.Get(id);
                vm = new ShowViewModel(show, AuthProvider);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Show/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(ShowViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the show to the server
                Show show = new Show
                {
                    Id = vm.Id,
                    Description = vm.Description,
                    End_DateTime = new DateTime(vm.EndDate.Year, vm.EndDate.Month, vm.EndDate.Day, vm.EndTime.Hour, vm.EndTime.Minute, 0),
                    ImageUrl = vm.ImageRemoteUrl,
                    IsPublished = vm.IsPublished,
                    Name = vm.Name,
                    Place = vm.Place,
                    Start_DateTime = new DateTime(vm.StartDate.Year, vm.StartDate.Month, vm.StartDate.Day, vm.StartTime.Hour, vm.StartTime.Minute, 0),
                    Url = vm.Url
                };

                // Image management
                SendImageToServer(vm, file, show);

                // Save the show
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(show, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.ShowCreated;
                }
                else
                {
                    _model.Edit(show, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.ShowUpdated;
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
        ///  GET: /Show/Delete/1
        /// Delete an existing show
        /// </summary>
        /// <param name="id">Show Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                Show show = _model.Get(id);
                if (show != null)
                {
                    LoginViewModel loginVM = AuthProvider.LoginViewModel;

                    if (loginVM.Role == Role.Bureau)
                    {
                        //TODO: Uncomment line below
                        //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                        return Json(new { id, success = true, message = MessagesResources.ShowDeleted });
                    }

                    return Json(new { id, success = false, message = MessagesResources.UnauthorizedRight });
                }

                return Json(new { id, success = false, message = MessagesResources.ShowInexistant });
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
        /// <param name="vm">ShowViewModel corresponding to the show</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="show">Show that will get image</param>
        private void SendImageToServer(ShowViewModel vm, HttpPostedFileBase file, Show show)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/Show/{0}", file.FileName);
                file.SaveAs(imagePath);
                show.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                show.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}