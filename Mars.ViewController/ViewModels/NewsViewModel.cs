using Mars.Common;
using SolarSystem.Mars.ViewController.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for news creation or updating page
    /// </summary>
    public class NewsViewModel : CRUDViewModelBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Action for the view-model</param>
        public NewsViewModel(CRUDAction action)
            : base(action)
        {
            Date = Time = DateTime.Now;
            IsPublished = true;
        }

        public NewsViewModel()
            : this(CRUDAction.Create)
        {
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
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TitleRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TitleMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TitleMaxLength")]
        public string Title { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Author's Id
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "AuthorRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "AuthorRequired")]
        public int IdAuthor { get; set; }

        /// <summary>
        /// Date and time
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "DateRequired")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "DateRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Date and time
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TimeRequired")]
        [DataType(DataType.Time, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "TimeRequired")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [DataType(DataType.ImageUrl, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png | jpg | jpeg | gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions", ErrorMessage = null)]
        public string ImageRemoteUrl { get; set; }

        /// <summary>
        /// Search keywords
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "KeywordsRequired")]
        public string Keywords { get; set; }

        /// <summary>
        /// Short text
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ShortTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ShortTextMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ShortTextMaxLength")]
        public string ShortText { get; set; }

        /// <summary>
        /// Text
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextMinLength")]
        [AllowHtml]
        public string Text { get; set; }

        /// <summary>
        /// Is the news published
        /// </summary>
        public bool IsPublished { get; set; }

        #endregion
    }
}