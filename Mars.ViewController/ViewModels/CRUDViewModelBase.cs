using Mars.Common;

namespace SolarSystem.Mars.ViewController.ViewModels
{
    public abstract class CRUDViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">Action for the view-model</param>
        protected CRUDViewModelBase(CRUDAction action = CRUDAction.Create)
        {
            Action = action;
        }

        /// <summary>
        /// Action for the view-model
        /// </summary>
        public CRUDAction Action { get; private set; }
    }
}