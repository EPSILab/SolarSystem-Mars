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
        public NewsViewModel(CRUDAction action = CRUDAction.Create)
            : base(action) { }

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
        /// Author's Id
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "AuthorRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "AuthorRequired")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Date and time
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "DateTimeRequired")]
        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "DateTimeRequired")]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImageRequired")]
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png | jpg | jpeg | gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// Search keywords
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Is the news published
        /// </summary>
        public bool IsPublished { get; set; }

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
        [AllowHtml]
        public string Text { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "UrlRequired")]
        public string Url { get; set; }

        #endregion
    }
}