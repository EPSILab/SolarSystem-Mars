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
    [WebserviceAuthorize]
    public class NewsController : MarsControllerBase
    {
        #region Constructor

        /// <summary>
        /// Constructor. Parameters are resolved with NInject
        /// </summary>
        public NewsController(IReaderLimit<News> model, IReader<Member> modelMember, IConstants constants)
            : base(constants)
        {
            _model = model;
            _modelMembers = modelMember;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Main model
        /// </summary>
        private readonly IReaderLimit<News> _model;

        /// <summary>
        /// Model for members
        /// </summary>
        private readonly IReader<Member> _modelMembers;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /News/
        /// GET: /News/Index
        /// GET: /News/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get News and tranform them in NewsViewModel
            IEnumerable<News> listNews = _model.Get(id, _constants.ItemsNumber);
            IEnumerable<NewsViewModel> vm = listNews.Select(news => new NewsViewModel(news));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /News/Manage : Create a new news
        /// GET: /News/Manage/5 : Edit an existing news
        /// </summary>
        /// <param name="id">News's Id to manage. If Id = 0, it is a new news</param>
        public ActionResult Manage(int id = 0)
        {
            // Load members list
            IEnumerable<Member> membersAvailables = _modelMembers.Get();

            NewsViewModel vm;

            // Create a news
            if (id == 0)
            {
                vm = new NewsViewModel();
                ViewBag.Members = CreateSelectList(membersAvailables);
            }
            // Edit an existing news
            else
            {
                News news = _model.Get(id);

                vm = new NewsViewModel(news);
                ViewBag.Members = CreateSelectList(membersAvailables, news);
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /News/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(NewsViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the news to the server
                News news = new News
                {
                    Id = vm.Id,
                    Keywords = vm.Keywords,
                    DateTime = new DateTime(vm.Date.Year, vm.Date.Month, vm.Date.Day, vm.Time.Hour, vm.Time.Minute, 0),
                    Member = _modelMembers.Get(vm.AuthorId),
                    IsPublished = vm.IsPublished,
                    ShortText = vm.ShortText,
                    Text = vm.Text,
                    Title = vm.Title,
                    Url = vm.Url
                };

                // Image management
                SendImageToServer(vm, file, news);

                // Save the news
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                if (vm.Id == 0)
                {
                    _model.Add(news, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.NewsCreated;
                }
                else
                {
                    _model.Edit(news, loginVM.Username, loginVM.PasswordCrypted);
                    ViewBag.SuccessMessage = MessagesResources.NewsUpdated;
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

            // Load members list
            IEnumerable<Member> membersAvailables = _modelMembers.Get();

            // Get the news currently selected by the user
            News newsSelected = _model.Get(vm.Id);

            // Create the SelectList used with a DropDownList
            ViewBag.Members = CreateSelectList(membersAvailables, newsSelected);

            return View(vm);
        }

        #endregion

        #region Delete methods

        /// <summary>
        ///  GET: /News/Delete/1
        /// Delete an existing news
        /// </summary>
        /// <param name="id">News Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                //TODO: Uncomment line below
                //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.NewsDeleted });
            }
            catch (Exception ex)
            {
                return Json(new { id = 0, success = false, message = ex.Message });
            }
        }

        #endregion

        #region General Methods

        /// <summary>
        /// Transform a list of members to a selectlist
        /// </summary>
        /// <param name="membersAvailables">Convert Members into a MemberList - SelectList</param>
        /// <param name="news">News currently selected - to define the default author</param>
        /// <returns>Members list formatted</returns>
        private SelectList CreateSelectList(IEnumerable<Member> membersAvailables, News news = null)
        {
            // Get list of item (member) to create the MemberList
            IList<SelectListItem> membersItems = membersAvailables.Select(member => new SelectListItem
            {
                Value = member.Id.ToString("0"),
                Text = string.Format("{0} {1} ({2})", member.FirstName, member.LastName, member.Campus.Place)
            }).ToList();

            SelectList members;

            if (news != null)
            {
                // Get the author inside the SelectList if it exists
                SelectListItem author = membersItems.FirstOrDefault(i => i.Value == news.Member.Id.ToString("0"));

                // If it does not exist, adding it into the SelectList
                if (author == null)
                {
                    author = new SelectListItem
                    {
                        Value = news.Member.Id.ToString("0"),
                        Text = string.Format("{0} {1} ({2})", news.Member.FirstName, news.Member.LastName, news.Member.Campus.Place)
                    };

                    membersItems.Add(author);
                }

                members = new SelectList(membersItems, "Value", "Text", author);
            }
            else
            {
                members = new SelectList(membersItems, "Value", "Text");
            }

            return members;
        }

        /// <summary>
        /// Send Image to the server
        /// </summary>
        /// <param name="vm">NewsViewModel corresponding to the news</param>
        /// <param name="file">File - Image could be sent</param>
        /// <param name="news">News that will get image</param>
        private void SendImageToServer(NewsViewModel vm, HttpPostedFileBase file, News news)
        {
            // Image is local
            if (file != null && file.ContentLength > 0)
            {
                string imagePath = string.Format("../Images/News/{0}", file.FileName);
                file.SaveAs(imagePath);
                news.ImageUrl = imagePath;
            }
            // Image is remote
            else if (!string.IsNullOrWhiteSpace(vm.ImageRemoteUrl))
                news.ImageUrl = vm.ImageRemoteUrl;
            // No image given
            else
                throw new InvalidModelStateException(ErrorRessources.ImageRequired);
        }

        #endregion
    }
}