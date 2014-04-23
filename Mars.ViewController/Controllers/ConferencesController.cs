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
    public class ConferencesController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public ConferencesController(IReaderLimit<Conference> model, IReader<Campus> modelCampuses, IConstants constants)
            : base(constants)
        {
            _model = model;
            _modelCampuses = modelCampuses;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReaderLimit<Conference> _model;

        /// <summary>
        /// Model for campuses
        /// </summary>
        private readonly IReader<Campus> _modelCampuses;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Conference/
        /// GET: /Conference/Index
        /// GET: /Conference/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get Conference and tranform them in ConferenceViewModel
            IEnumerable<Conference> listConference = _model.Get(id, _constants.ItemsNumber);
            IEnumerable<ConferenceViewModel> vm = listConference.Select(conference => new ConferenceViewModel(conference));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Conference/Manage : Create a new conference
        /// GET: /Conference/Manage/5 : Edit an existing conference
        /// </summary>
        /// <param name="id">Conference's Id to manage. If Id = 0, it is a new conference</param>
        public ActionResult Manage(int id = 0)
        {
            // Load campuses list
            IEnumerable<Campus> campusAvailables = _modelCampuses.Get();

            ConferenceViewModel vm;

            // Create a conference
            if (id == 0)
            {
                vm = new ConferenceViewModel();
                ViewBag.Campuses = CreateSelectList(campusAvailables);
            }
            // Edit an existing conference
            else
            {
                Conference conference = _model.Get(id);

                vm = new ConferenceViewModel(conference);
                ViewBag.Campuses = CreateSelectList(campusAvailables, conference);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Conference/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(ConferenceViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the conference to the server
                Conference conference = new Conference
                {
                    Id = vm.Id,
                    Campus = _modelCampuses.Get(vm.Id),
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
                SendImageToServer(vm, file, conference);

                // Save the conference
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(conference, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.ConferenceCreated;
                }
                else
                {
                    _model.Edit(conference, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.ConferenceUpdated;
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

            // Load campuses list
            IEnumerable<Campus> campusAvailables = _modelCampuses.Get();

            // Get the conference currently selected by the user
            Conference conferenceSelected = _model.Get(vm.Id);

            // Create the SelectList used with a DropDownList
            ViewBag.Campuses = CreateSelectList(campusAvailables, conferenceSelected);

            return View(vm);
        }

        #endregion

        #region Delete methods

        /// <summary>
        ///  GET: /Conference/Delete/1
        /// Delete an existing conference
        /// </summary>
        /// <param name="id">Conference Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                // TODO: Uncomment line bellow
                //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.ConferenceDeleted });
            }
            catch (Exception ex)
            {
                return Json(new { id = 0, success = false, message = ex.Message });
            }
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Transform a list of campuses to a selectlist
        /// </summary>
        /// <param name="campusAvailables">Convert Campuses into a MemberList - SelectList</param>
        /// <param name="conference">Conference currently selected - to define the default campus</param>
        /// <returns>Campuses list formatted</returns>
        private SelectList CreateSelectList(IEnumerable<Campus> campusAvailables, Conference conference = null)
        {
            // Get list of item (campus) to create the MemberList
            IList<SelectListItem> campusesItems = campusAvailables.Select(campus => new SelectListItem
            {
                Value = campus.Id.ToString("0"),
                Text = campus.Place
            }).ToList();

            SelectList campuses;

            if (conference != null)
            {
                // Get the campus inside the SelectList if it exists
                SelectListItem campus = campusesItems.First(i => i.Value == conference.Campus.Id.ToString("0"));
                campuses = new SelectList(campusesItems, "Value", "Text", campus);
            }
            else
            {
                campuses = new SelectList(campusesItems, "Value", "Text");
            }

            return campuses;
        }

        /// <summary>
        /// Send Image to the server
        /// </summary>
        /// <param name="vm">ConferenceViewModel corresponding to the conference</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="conference">Conference that will get image</param>
        private void SendImageToServer(ConferenceViewModel vm, HttpPostedFileBase file, Conference conference)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/Conference/{0}", file.FileName);
                file.SaveAs(imagePath);
                conference.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                conference.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}