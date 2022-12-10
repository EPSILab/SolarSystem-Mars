using Ninject;
using SolarSystem.Mars.Model.Model.Abstract;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Controllers
{
    public abstract class MarsControllerBase : Controller
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        protected MarsControllerBase()
        {
        }

        /// <summary>
        /// Constructor. Resolve parameters with NInject
        /// </summary>
        protected MarsControllerBase(IConstants constants)
        {
            _constants = constants;
        }

        #endregion

        #region Attributes

        /// <summary>
        /// Constants values
        /// </summary>
        protected readonly IConstants _constants;

        #endregion

        #region Properties

        [Inject]
        public IAuthProvider AuthProvider { get; set; }

        #endregion
    }
}