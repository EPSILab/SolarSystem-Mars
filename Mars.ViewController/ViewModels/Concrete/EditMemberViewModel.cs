using System.ComponentModel.DataAnnotations;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels.Abstract;

namespace SolarSystem.Mars.ViewController.ViewModels.Concrete
{
    public class EditMemberViewModel : MemberViewModel, IEditMemberViewModel
    {
        #region Constructors

        /// <summary>
        /// Build a view-model from an entity
        /// </summary>
        /// <param name="member">Entity to transform</param>
        public EditMemberViewModel(Member member) : base(member)
        {
            Role = member.Role;
            Status = member.Status;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Role
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "RoleRequired")]
        public Role Role { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StatusRequired")]
        [MinLength(4, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StatusMinLength")]
        [MaxLength(40, ErrorMessageResourceType = typeof(ErrorRessources), ErrorMessageResourceName = "StatusMaxLength")]
        public string Status { get; set; }

        #endregion
    }
}