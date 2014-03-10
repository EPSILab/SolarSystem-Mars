using System.Globalization;
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

        public NewsController(IReaderLimit<News> model, IReader<Member> modelMember, IConstants constants)
        {
            _model = model;
            _modelMembers = modelMember;
            _constants = constants;
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

        /// <summary>
        /// Constants values
        /// </summary>
        private readonly IConstants _constants;

        #endregion

        #region Index methods

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
        /// GET: /News/Manage/1
        /// Edit an existing news
        /// </summary>
        /// <param name="id">Id of the news to manage</param>
        public ActionResult Manage(int id = 0)
        {
            // Load members list
            IEnumerable<Member> membersAvailables = _modelMembers.Get();
            NewsViewModel vm;
            SelectListItem author = null;

            IList<SelectListItem> membersItems = membersAvailables.Select(member => new SelectListItem
            {
                Value = member.Id.ToString(),
                Text = string.Format("{0} {1} ({2})", member.FirstName, member.LastName, member.Campus.Place)
            }).ToList();


            if (id == 0)
                vm = new NewsViewModel();
            else
            {
                News news = _model.Get(id);
                vm = new NewsViewModel(news);

                author = membersItems.FirstOrDefault(i => i.Value == news.Member.Id.ToString());

                if (author == null)
                {
                    author = new SelectListItem
                    {
                        Value = news.Member.Id.ToString(),
                        Text = string.Format("{0} {1} ({2})", news.Member.FirstName, news.Member.LastName, news.Member.Campus.Place)
                    };

                    membersItems.Add(author);
                }
            }

            var membersList = new SelectList(membersItems, "Value", "Text", author);
            ViewBag.Members = membersList;

            return View(vm);
        }

        /// <summary>
        /// POST: /News/Manage
        /// When the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page view-model</param>
        /// <param name="file">Image to send on the FTP server</param>
        [HttpPost]
        public ActionResult Manage(NewsViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Prepare to send the news to the server
                    var news = new News
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
                    if (file != null && file.ContentLength > 0) // Image is local
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

                    // Save the news
                    LoginViewModel loginVM = AuthProvider.LoginViewModel;

                    if (vm.Id == 0)
                    {
                        _model.Add(news, loginVM.Username, loginVM.PasswordCrypted);
                        ViewData["successMessage"] = MessagesResources.NewsCreated;
                    }
                    else
                    {
                        _model.Edit(news, loginVM.Username, loginVM.PasswordCrypted);
                        ViewData["successMessage"] = MessagesResources.NewsUpdated;
                    }

                    return RedirectToAction("Index");
                }

                throw new InvalidModelStateException();
            }
            catch (InvalidModelStateException ex)
            {
                ViewData["errorMessage"] = ex.DisplayMessage;
            }
            catch (Exception ex)
            {
                ViewData["errorMessage"] = ex.Message;
            }

            IEnumerable<Member> membersAvailables = _modelMembers.Get();

            IList<SelectListItem> membersItems = membersAvailables.Select(member => new SelectListItem
            {
                Value = member.Id.ToString(),
                Text = string.Format("{0} {1} ({2})", member.FirstName, member.LastName, member.Campus.Place)
            }).ToList();

            SelectListItem author = membersItems.FirstOrDefault(i => int.Parse(i.Value) == vm.AuthorId);

            if (author == null)
            {
                News news = _model.Get(vm.Id);

                author = new SelectListItem
                {
                    Value = news.Member.Id.ToString(),
                    Text = string.Format("{0} {1} ({2})", news.Member.FirstName, news.Member.LastName, news.Member.Campus.Place)
                };

                membersItems.Add(author);
            }

            var membersList = new SelectList(membersItems, "Value", "Text", author);
            ViewBag.Members = membersList;

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
                // TODO: Décommenter les lignes suivantes
                // LoginViewModel loginVM = AuthProvider.LoginViewModel;
                // _model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.NewsDeleted });
            }
            catch (Exception ex)
            {
                return Json(new { id = 0, success = false, message = ex.Message });
            }
        }

        #endregion
    }
}