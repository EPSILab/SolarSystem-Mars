using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;
using System.ComponentModel.DataAnnotations;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    /// <summary>
    /// View-model for promotions creation or updating page
    /// </summary>
    public class PromotionViewModel
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public PromotionViewModel()
        {
            StillPresent = true;
        }

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="promotion">Entity to transform</param>
        public PromotionViewModel(Promotion promotion)
        {
            Id = promotion.Id;
            Name = promotion.Name;
            GraduationYear = promotion.GraduationYear;
            StillPresent = promotion.StillPresent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameRequired")]
        [MinLength(5, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameMinLength")]
        [MaxLength(100, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "NameMaxLength")]
        public string Name { get; set; }

        /// <summary>
        /// Graduation year
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "GraduationYear")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "GraduationYearRequired")]
        [MinLength(4, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "GraduationYearLength")]
        [MaxLength(4, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "GraduationYearLength")]
        public int GraduationYear { get; set; }

        /// <summary>
        /// Is promotion still present ?
        /// </summary>
        [Display(ResourceType = typeof(ContentRessources), Name = "StillPresent")]
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StillPresentRequired")]
        public bool StillPresent { get; set; }

        #endregion
    }
}