using System.Linq;
using Mars.Common;
using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
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

        public NewsController(IReaderLimit<News> modelNews, IReader<Member> modelMember)
        {
            _model = modelNews;
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
            ViewBag.Members = members.Select(m => new { Id = m.Id, Name = string.Format("{0} {1} - ({2}", m.FirstName, m.LastName, m.Campus.Place ) });

            NewsViewModel vm;

            if (id == 0)
                vm = new NewsViewModel();
            else
            {
                News news = _model.Get(id);

                vm = new NewsViewModel(CRUDAction.Update)
                {
                    Id = news.Id,
                    AuthorId = news.Member.Id,
                    ImageUrl = news.ImageUrl,
                    IsPublished = news.IsPublished,
                    Keywords = news.Keywords,
                    Text = news.Text,
                    ShortText = news.ShortText,
                    Title = news.Title,
                    Url = news.Url
                };
            }

            return View(vm);
        }

        /// <summary>
        /// POST: /News/Manage
        /// When the user has clicked on the OK button
        /// </summary>
        /// <param name="newsVM">Page view-model</param>
        /// <param name="file">Image to send on the FTP server</param>
        [HttpPost]
        public ActionResult Manage(NewsViewModel newsVM, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Prepare to send the news to the server
                    News news = new News
                    {
                        Id = newsVM.Id,
                        Keywords = newsVM.Keywords,
                        DateTime = newsVM.DateTime,
                        Member = _modelMembers.Get(newsVM.AuthorId),
                        IsPublished = newsVM.IsPublished,
                        ShortText = newsVM.ShortText,
                        Text = newsVM.Text,
                        Title = newsVM.Title,
                        Url = newsVM.Url
                    };

                    // Image management
                    string imagePath = string.Format("{0}/Images/News/{1}", ContentRessources.EPSILabUrl, file.FileName);
                    file.SaveAs(imagePath);
                    news.ImageUrl = imagePath;

                    // Save the news
                    LoginViewModel loginVM = AuthProvider.LoginViewModel;

                    switch (newsVM.Action)
                    {
                        case CRUDAction.Create:
                            _model.Add(news, loginVM.Username, loginVM.PasswordCrypted);
                            break;
                        case CRUDAction.Update:
                            _model.Edit(news, loginVM.Username, loginVM.PasswordCrypted);
                            break;
                    }

                    return RedirectToAction("Index");
                }

                return View(newsVM);
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex.Message;
                return View(newsVM);
            }
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