using System.Linq;
using Mars.Common;
using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Exceptions;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class NewsController : MarsControllerBase
    {
        #region Constructor

        public NewsController(IReaderLimit<News> model, IReader<Member> modelMember)
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

        #region Methods

        /// <summary>
        /// GET: /News/
        /// Get all news
        /// </summary>
        public ActionResult Index()
        {
            IEnumerable<News> newsList = _model.Get(0, 10);
            return View(newsList);
        }

        /// <summary>
        /// GET: /News/Manage/1
        /// Edit an existing news
        /// </summary>
        /// <param name="id">Id of the news to manage</param>
        public ActionResult Manage(int id = 0)
        {
            // Load members list
            IEnumerable<Member> members = _modelMembers.Get();
            ViewBag.Members = members.Select(m => new { m.Id, Name = string.Format("{0} {1} ({2})", m.LastName, m.FirstName, m.Campus.Place) });

            NewsViewModel vm;

            if (id == 0)
                vm = new NewsViewModel();
            else
            {
                News news = _model.Get(id);

                vm = new NewsViewModel(CRUDAction.Update)
                {
                    Id = news.Id,
                    IdAuthor = news.Member.Id,
                    ImageRemoteUrl = news.ImageUrl,
                    IsPublished = news.IsPublished,
                    Keywords = news.Keywords,
                    Text = news.Text,
                    ShortText = news.ShortText,
                    Title = news.Title,
                    Url = news.Url,
                    Date = news.DateTime.Date,
                    Time = new DateTime(0, 0, 0, news.DateTime.Hour, news.DateTime.Minute, news.DateTime.Second)
                };
            }

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
                    News news = new News
                    {
                        Id = vm.Id,
                        Keywords = vm.Keywords,
                        DateTime =
                            new DateTime(vm.Date.Year, vm.Date.Month, vm.Date.Day, vm.Time.Hour, vm.Time.Minute, 0),
                        Member = _modelMembers.Get(vm.IdAuthor),
                        IsPublished = vm.IsPublished,
                        ShortText = vm.ShortText,
                        Text = vm.Text,
                        Title = vm.Title,
                        Url = vm.Url
                    };

                    // Image management
                    if (file != null && file.ContentLength > 0) // Image is local
                    {
                        string imagePath = string.Format("../{0}/Images/News/{1}", ContentRessources.EPSILabUrl,
                            file.FileName);
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

                    switch (vm.Action)
                    {
                        case CRUDAction.Create:
                            //_model.Add(news, loginVM.Username, loginVM.PasswordCrypted);
                            break;
                        case CRUDAction.Update:
                            //_model.Edit(news, loginVM.Username, loginVM.PasswordCrypted);
                            break;
                    }

                    return RedirectToAction("Index");
                }

                throw new InvalidModelStateException();
            }
            catch (InvalidModelStateException ex)
            {
                ViewBag.ExceptionMessage = ex.DisplayMessage;
            }
            catch (Exception ex)
            {
                ViewBag.ExceptionMessage = ex.Message;
            }

            IEnumerable<Member> members = _modelMembers.Get();
            ViewBag.Members = members.Select(m => new { m.Id, Name = string.Format("{0} {1} ({2})", m.LastName, m.FirstName, m.Campus.Place) });

            return View(vm);
        }

        /// <summary>
        ///  GET: /News/Delete/1
        /// Delete an existing news
        /// </summary>
        /// <param name="id">News Id to delete</param>
        public ActionResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                _model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        #endregion
    }
}