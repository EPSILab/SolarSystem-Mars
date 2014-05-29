using System;
using System.Linq;
using System.Web;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Exceptions;
using SolarSystem.Mars.ViewController.Infrastructure.Concrete;
using System.Collections.Generic;
using System.Web.Mvc;
using SolarSystem.Mars.ViewController.Resources;
using SolarSystem.Mars.ViewController.ViewModels.Concrete;

namespace SolarSystem.Mars.ViewController.Controllers
{
    [WebserviceAuthorize(Role.Bureau)]
    public class MembersController : MarsControllerBase
    {
        #region Constructor

        public MembersController(IReader<Member> model, IMemberReaderFilters memberModel, IReader<Campus> campusModel, IReader<Promotion> promotionModel, IConstants constants)
            : base(constants)
        {
            _model = model;
            _memberModel = memberModel;
            _campusModel = campusModel;
            _promotionModel = promotionModel;
        }

        #endregion

        #region Attributes

        private readonly IReader<Member> _model;
        private readonly IMemberReaderFilters _memberModel;
        private readonly IReader<Campus> _campusModel;
        private readonly IReader<Promotion> _promotionModel;

        #endregion

        #region Index methods

        /// <summary>
        /// GET: /Member/
        /// GET: /Member/Index
        /// GET: /Member/Index/10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Index(int id = 0)
        {
            // Get Member and tranform them in MemberViewModel
            IEnumerable<Member> listMember = _model.Get().Skip(id).Take(_constants.ItemsNumber);
            IEnumerable<MemberViewModel> vm = listMember.Select(member => new MemberViewModel(member));

            // Send Id and ItemsNumber for navigation
            ViewBag.Id = id;
            ViewBag.ItemsNumber = _constants.ItemsNumber;

            return View(vm);
        }

        #endregion

        #region Valid methods

        /// <summary>
        /// GET: /Member/Valid : valid members
        /// </summary>
        /// <returns></returns>
        public ActionResult Valid()
        {
            IEnumerable<Member> inactiveMembers = _memberModel.GetInactives();
            IEnumerable<MemberViewModel> listVM = inactiveMembers.Select(member => new MemberViewModel(member));

            return View(listVM);
        }

        /// <summary>
        /// GET: /Member/Valid : Valid a member
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Valid(MemberViewModel vm)
        {
            Member inactiveCurrentMember = _model.Get(vm.Id);

            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // active the member
                inactiveCurrentMember.Active = true;

                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                _model.Edit(inactiveCurrentMember, loginVM.Username, loginVM.PasswordCrypted);

                // TODO : Send email notification

                return Json(new { vm.Id, success = true, message = MessagesResources.MemberUpdated });
            }
            catch (InvalidModelStateException ex)
            {
                return Json(new { id = 0, success = false, message = ex.DisplayMessage });
            }
            catch (Exception ex)
            {
                return Json(new { id = 0, success = false, message = ex.Message });
            }
        }

        #endregion

        #region Manage methods

        /// <summary>
        /// GET: /Member/Manage/5 : Edit an existing member
        /// </summary>
        /// <param name="id">Member's Id to manage.</param>
        public ActionResult Manage(int id)
        {
            // Get promotions and campuses for register
            ViewBag.Promotions = _promotionModel.Get();
            ViewBag.Campuses = _campusModel.Get();

            // Edit an existing member
            Member member = _model.Get(id);
            MemberViewModel vm = new MemberViewModel(member);

            return View(vm);
        }

        /// <summary>
        /// POST: /Member/Manage
        /// Raised when the user has clicked on the OK button
        /// </summary>
        /// <param name="vm">Page ViewModel</param>
        /// <param name="file">Image to send on the server</param>
        [HttpPost]
        public ActionResult Manage(MemberViewModel vm, HttpPostedFileBase file)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new InvalidModelStateException();

                // Prepare to send the member to the server
                Member member = new Member
                {
                    Id = vm.Id,
                    CityFrom = vm.CityFrom,
                    EPSIEmail = vm.EPSIEmail,
                    GitHubUrl = vm.GitHubUrl,
                    LastName = vm.LastName,
                    PersonalEmail = vm.PersonalEmail,
                    PhoneNumber = vm.PhoneNumber,
                    TwitterUrl = vm.TwitterUrl,
                    Username = vm.Username,
                    ViadeoUrl = vm.ViadeoUrl,
                    Website = vm.Website,
                    FirstName = vm.FirstName,
                    Presentation = vm.Presentation,
                    Active = vm.IsActive,
                    Campus = _campusModel.Get(vm.IdCampus),
                    FacebookUrl = vm.FacebookUrl,
                    LinkedInUrl = vm.LinkedInUrl,
                    Promotion = _promotionModel.Get(vm.IdPromotion)
                };

                // Save the member
                LoginViewModel loginVM = AuthProvider.LoginViewModel;

                _model.Edit(member, loginVM.Username, loginVM.PasswordCrypted);
                ViewBag.SuccessMessage = MessagesResources.MemberUpdated;

                return RedirectToAction("Index");
            }
            catch (InvalidModelStateException ex)
            {
                ViewBag.ErrorMessage = ex.DisplayMessage;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            // Get promotions and campuses for register
            ViewBag.Promotions = _promotionModel.Get();
            ViewBag.Campuses = _campusModel.Get();

            return View(vm);
        }

        #endregion

        #region Delete methods

        /// <summary>
        ///  GET: /Member/Delete/1
        /// Delete an existing member
        /// </summary>
        /// <param name="id">Member Id to delete</param>
        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                LoginViewModel loginVM = AuthProvider.LoginViewModel;
                // TODO: Uncomment line bellow
                //_model.Delete(id, loginVM.Username, loginVM.PasswordCrypted);

                return Json(new { id, success = true, message = MessagesResources.MemberDeleted });
            }
            catch (Exception ex)
            {
                return Json(new { id = 0, success = false, message = ex.Message });
            }
        }

        #endregion
    }
}