using System.Collections.Generic;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;

namespace SolarSystem.Mars.Model.Model.Concrete
{
    class SlideModel : IReader<Slide>
    {
        #region Attributes

        private readonly ISlideManager _proxy = new SlideManagerClient();

        #endregion

        #region IReader methods

        public Slide Get(int code)
        {
            return _proxy.GetSlide(code);
        }

        public IList<Slide> Get()
        {
            return _proxy.GetSlides();
        }

        public int Add(Slide element, string username, string password)
        {
            return _proxy.AddSlide(element, username, password);
        }

        public void Edit(Slide element, string username, string password)
        {
            _proxy.EditSlide(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteSlide(code, username, password);
        }

        #endregion
    }
}