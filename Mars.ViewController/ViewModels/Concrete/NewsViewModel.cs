using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels.Abstract;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    /// <summary>
    /// View-model for news creation or updating page
    /// </summary>
    public class NewsViewModel : INewsViewModel
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public NewsViewModel()
        {
            Date = Time = DateTime.Now;
            IsPublished = true;
            CanUpdate = true;
            CanDelete = true;
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="news">Entity to transform</param>
        /// <param name="authProvider"></param>
        public NewsViewModel(News news, IAuthProvider authProvider)
        {
            Id = news.Id;

            AuthorId = news.Member.Id;
            AuthorName = string.Format("{0} {1}", news.Member.FirstName, news.Member.LastName);

            ImageRemoteUrl = news.ImageUrl;

            IsPublished = news.IsPublished;
            CanUpdate = CanDelete = (authProvider.LoginViewModel.Role == Role.Bureau || news.Member.Username == authProvider.LoginViewModel.Username);

            Keywords = news.Keywords;
            Text = news.Text;
            ShortText = news.ShortText;
            Title = news.Title;
            Url = news.Url;

            Date = news.DateTime.Date;
            Time = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, news.DateTime.Hour, news.DateTime.Minute, news.DateTime.Second);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "NewsTitle")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TitleRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TitleMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TitleMaxLength")]
        public string Title { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "URL")]
        public string Url { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "DateTime")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "DateRequired")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "DateRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TimeRequired")]
        [DataType(DataType.Time, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TimeRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Author's Id
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Author")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "AuthorRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "AuthorRequired")]
        public int AuthorId { get; set; }

        [Display(ResourceType = typeof(ContentRessources), Name = "Author")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions", ErrorMessage = null)]
        public string ImageRemoteUrl { get; set; }

        /// <summary>
        /// Search keywords
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Keywords")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "KeywordsRequired")]
        public string Keywords { get; set; }

        /// <summary>
        /// Short text
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "ShortText")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ShortTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ShortTextMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ShortTextMaxLength")]
        public string ShortText { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Text")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextMinLength")]
        [AllowHtml]
        public string Text { get; set; }

        /// <summary>
        /// Is the news published
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "IsPublished", ShortName = "IsPublished")]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Specify if the user can update the news or not
        /// </summary>
        public bool CanUpdate { get; set; }

        /// <summary>
        /// Specify if the user can delete the news or not
        /// </summary>
        public bool CanDelete { get; set; }

        #endregion
    }
}