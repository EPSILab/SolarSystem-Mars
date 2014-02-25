using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    public class NewsViewModel
    {
        public int NewsId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "TitleRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "TitleMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "TitleMaxLength")]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "AuthorRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "AuthorRequired")]
        public int AuthorId { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ImageRequired")]
        [DataType(DataType.Url, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png | jpg | jpeg | gif", ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ImagePathExtensions")]
        public string ImagePath { get; set; }

        public string Keywords { get; set; }

        public bool IsPublished { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ShortTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ShortTextMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "ShortTextMaxLength")]
        public string ShortText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "LongTextRequired")]
        [AllowHtml]
        public string LongText { get; set; }

        [Required(ErrorMessageResourceType = typeof(ErrorMessages), ErrorMessageResourceName = "UrlRequired")]
        public string Url { get; set; }
    }
}