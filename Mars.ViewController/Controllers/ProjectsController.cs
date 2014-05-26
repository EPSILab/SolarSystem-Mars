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
    [WebserviceAuthorize(Role.MemberActive, Role.Bureau)]
    public class ProjectsController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public ProjectsController(IReaderLimit<Project> model, IReader<Campus> modelCampuses, IConstants constants)
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
        private readonly IReaderLimit<Project> _model;

        /// <summary>
        /// Model for campuses
        /// </summary>
        private readonly IReader<Campus> _modelCampuses;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Project/
        /// GET: /Project/Index
        /// GET: /Project/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get Project and tranform them in ProjectViewModel
            IEnumerable<Project> listProject = _model.Get(id, _constants.ItemsNumber);
            IEnumerable<ProjectViewModel> vm = listProject.Select(project => new ProjectViewModel(project));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Project/Manage : Create a new project
        /// GET: /Project/Manage/5 : Edit an existing project
        /// </summary>
        /// <param name="id">Project's Id to manage. If Id = 0, it is a new project</param>
        public ActionResult Manage(int id = 0)
        {
            // Load campuses list
            IEnumerable<Campus> campusAvailables = _modelCampuses.Get();

            ProjectViewModel vm;

            // Create a project
            if (id == 0)
            {
                vm = new ProjectViewModel();
                ViewBag.Campuses = CreateSelectList(campusAvailables);
            }
            // Edit an existing project
            else
            {
                Project project = _model.Get(id);

                vm = new ProjectViewModel(project);
                ViewBag.Campuses = CreateSelectList(campusAvailables, project);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /Project/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(ProjectViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the project to the server
                Project project = new Project
                {
                    Id = vm.Id,
                    Campus = _modelCampuses.Get(vm.Id),
                    Description = vm.Description,
                    ImageUrl = vm.ImageRemoteUrl,
                    Progression = vm.Progression,
                    Name = vm.Name
                };

                // Image management
                SendImageToServer(vm, file, project);

                // Save the project
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(project, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.ProjectCreated;
                }
                else
                {
                    _model.Edit(project, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.ProjectUpdated;
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

            // Get the project currently selected by the user
            Project projectSelected = _model.Get(vm.Id);

            // Create the SelectList used with a DropDownList
            ViewBag.Campuses = CreateSelectList(campusAvailables, projectSelected);

            return View(vm);
        }

        #endregion

        #region Delete methods

        /// <summary>
        ///  GET: /Project/Delete/1
        /// Delete an existing project
        /// </summary>
        /// <param name="id">Project Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                // TODO: Uncomment line bellow
                //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.ProjectDeleted });
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
        /// <param name="project">Project currently selected - to define the default campus</param>
        /// <returns>Campuses list formatted</returns>
        private SelectList CreateSelectList(IEnumerable<Campus> campusAvailables, Project project = null)
        {
            // Get list of item (campus) to create the MemberList
            IList<SelectListItem> campusesItems = campusAvailables.Select(campus => new SelectListItem
            {
                Value = campus.Id.ToString("0"),
                Text = campus.Place
            }).ToList();

            SelectList campuses;

            if (project != null)
            {
                // Get the campus inside the SelectList if it exists
                SelectListItem campus = campusesItems.First(i => i.Value == project.Campus.Id.ToString("0"));
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
        /// <param name="vm">ProjectViewModel corresponding to the project</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="project">Project that will get image</param>
        private void SendImageToServer(ProjectViewModel vm, HttpPostedFileBase file, Project project)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/Project/{0}", file.FileName);
                file.SaveAs(imagePath);
                project.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                project.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}