using System;
using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using SolarSystem.Mars.ViewController.Resources;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    /// <summary>
    /// View-model for projects creation or updating page
    /// </summary>
    public class ProjectViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProjectViewModel()
        {
            CanUpdate = true;
            CanDelete = true;
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="project">Entity to transform</param>
        /// <param name="authProvider"></param>
        public ProjectViewModel(Project project, IAuthProvider authProvider)
        {
            Id = project.Id;
            CampusId = project.Campus.Id;
            CampusName = project.Campus.Place;
            Description = project.Description;
            ImageRemoteUrl = project.ImageUrl;
            Name = project.Name;

            CanUpdate = true;
            CanDelete = (authProvider.LoginViewModel.Role == Role.Bureau);
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
        [Display(ResourceType = typeof(ContentRessources), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameMaxLength")]
        public string Name { get; set; }

        /// <summary>
        /// Start date
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Progression")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ProgressionRequired")]
        public int Progression { get; set; }

        /// <summary>
        /// Campus Id
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Campus")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "CampusRequired")]
        [Range(1, Int32.MaxValue, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "CampusRequired")]
        public int CampusId { get; set; }

        /// <summary>
        /// Campus name
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Campus")]
        public string CampusName { get; set; }

        /// <summary>
        /// Image URL
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Image")]
        [DataType(DataType.ImageUrl, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathFormat")]
        [FileExtensions(Extensions = "png, jpg, jpeg, gif", ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "ImagePathExtensions", ErrorMessage = null)]
        public string ImageRemoteUrl { get; set; }

        /// <summary>
        /// Short text
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Text")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "LongTextMinLength")]
        public string Description { get; set; }

        /// <summary>
        /// Specify if the user can update the project or not
        /// </summary>
        public bool CanUpdate { get; set; }

        /// <summary>
        /// Specify if the user can delete the project or not
        /// </summary>
        public bool CanDelete { get; set; }

        #endregion
    }
}