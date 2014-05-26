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
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize(Role.Bureau)]
    public class LinksController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public LinksController(IReader<Link> model, IConstants constants)
            : base(constants)
        {
            _model = model;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReader<Link> _model;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Link/
        /// GET: /Link/Index
        /// GET: /Link/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get Link and tranform them in LinkViewModel
            IEnumerable<Link> listLink = (id == 0) ? _model.Get() : new List<Link> { _model.Get(id) };
            IEnumerable<LinkViewModel> vm = listLink.Select(link => new LinkViewModel(link));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Link/Manage : Create a new link
        /// GET: /Link/Manage/5 : Edit an existing link
        /// </summary>
        /// <param name="id">Link's Id to manage. If Id = 0, it is a new link</param>
        public ActionResult Manage(int id = 0)
        {
            LinkViewModel vm;

            // Create a link
            if (id == 0)
                vm = new LinkViewModel();
            // Edit an existing link
            else
            {
                Link link = _model.Get(id);
                vm = new LinkViewModel(link);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Link/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(LinkViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the link to the server
                Link link = new Link
                {
                    Id = vm.Id,
                    Description = vm.Description,
                    ImageUrl = vm.ImageRemoteUrl,
                    Label = vm.Label,
                    Order = vm.Order,
                    Url = vm.Url
                };

                // Image management
                SendImageToServer(vm, file, link);

                // Save the link
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(link, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.LinkCreated;
                }
                else
                {
                    _model.Edit(link, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.LinkUpdated;
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
        ///  GET: /Link/Delete/1
        /// Delete an existing link
        /// </summary>
        /// <param name="id">Link Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                // TODO: Uncomment line bellow
                //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.LinkDeleted });
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
        /// <param name="vm">LinkViewModel corresponding to the link</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="link">Link that will get image</param>
        private void SendImageToServer(LinkViewModel vm, HttpPostedFileBase file, Link link)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/Link/{0}", file.FileName);
                file.SaveAs(imagePath);
                link.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                link.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}