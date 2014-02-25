using System.Web;
using Microsoft.Ajax.Utilities;
using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SolarSystem.Mars.ViewController.ViewModels;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize]
    public class NewsController : MarsControllerBase
    {
        #region Constructor

        public NewsController(IReaderLimit<News> modelNews, IReader<Membre> modelMembre)
        {
            _modelNews = modelNews;
            _modelMembre = modelMembre;
        }

        #endregion

        #region Attributes

        private readonly IReaderLimit<News> _modelNews;
        private readonly IReader<Membre> _modelMembre;

        #endregion

        #region Methods

        // GET: /News/
        public ActionResult Index()
        {
            IEnumerable<News> newsList = _modelNews.Get(0, 10);
            return View(newsList);
        }

        // GET: /News/Details/5
        public ActionResult Details(int id)
        {
            News news = _modelNews.Get(id);
            return View(news);
        }

        // GET: /News/Create
        public ActionResult Create()
        {
            ViewBag.Membres = _modelMembre.Get();

            return View(new NewsViewModel());
        }

        // POST: /News/Create
        [HttpPost]
        public ActionResult Create(NewsViewModel newsViewModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var news = new News
                    {
                        Mots_Cles = newsViewModel.Keywords,
                        Date_Heure = DateTime.Now,
                        Membre = _modelMembre.Get(newsViewModel.AuthorId),
                        Publiee = newsViewModel.IsPublished,
                        Texte_Court = newsViewModel.ShortText,
                        Texte_Long = newsViewModel.LongText,
                        Titre = newsViewModel.Title,
                        URL = newsViewModel.Url
                    };

                    if (file != null && file.ContentLength > 0)
                    {
                        string imagePath = string.Format("http://www.epsilab.net/Images/News/{0}", file.FileName);
                        file.SaveAs(imagePath);
                        news.Image = imagePath;
                    }

                    _modelNews.Add(news, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                    return RedirectToAction("Index");
                }
                return View(newsViewModel);
            }
            catch
            {
                return View(newsViewModel);
            }
        }

        // GET: /News/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Membres = _modelMembre.Get();

            News news = _modelNews.Get(id);
            var newsViewModel = new NewsViewModel
            {
                NewsId = news.Code_News,
                AuthorId = news.Membre.Code_Membre,
                ImagePath = news.Image,
                IsPublished = news.Publiee,
                Keywords = news.Mots_Cles,
                LongText = news.Texte_Long,
                ShortText = news.Texte_Court,
                Title = news.Titre,
                Url = news.URL
            };

            return View(newsViewModel);
        }


        // POST: /News/Edit/5
        [HttpPost]
        public ActionResult Edit(NewsViewModel newsViewModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var news = _modelNews.Get(newsViewModel.NewsId);

                    if (news != null)
                    {
                        news.Mots_Cles = newsViewModel.Keywords;
                        news.Date_Heure = DateTime.Now;
                        news.Membre = _modelMembre.Get(newsViewModel.AuthorId);
                        news.Publiee = newsViewModel.IsPublished;
                        news.Texte_Court = newsViewModel.ShortText;
                        news.Texte_Long = newsViewModel.LongText;
                        news.Titre = newsViewModel.Title;
                        news.URL = newsViewModel.Url;

                        if (file != null && file.ContentLength > 0)
                        {
                            string imagePath = string.Format("http://www.epsilab.net/Images/News/{0}", file.FileName);
                            file.SaveAs(imagePath);
                            news.Image = imagePath;
                        }

                        _modelNews.Edit(news, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);
                    }

                    return RedirectToAction("Index");
                }
                return View(newsViewModel);
            }
            catch
            {
                return View(newsViewModel);
            }
        }

        // GET: /News/Delete/5
        public ActionResult Delete(int id)
        {
            News news = _modelNews.Get(id);
            return View(news);
        }

        // POST: /News/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, News news)
        {
            try
            {
                _modelNews.Delete(id, AuthProvider.LoginViewModel.Username, AuthProvider.LoginViewModel.PasswordCrypted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(news);
            }
        }

        #endregion
    }
}