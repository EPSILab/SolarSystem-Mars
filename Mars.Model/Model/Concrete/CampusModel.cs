using System.Collections.Generic;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;

namespace SolarSystem.Mars.Model.Model.Concrete
{
    class CampusModel : IReader<Campus>
    {
        #region Attributes

        private readonly ICampusManager _proxy = new CampusManagerClient();

        #endregion

        #region IReader methods

        public Campus Get(int code)
        {
            return _proxy.GetCampus(code);
        }

        public IList<Campus> Get()
        {
            return _proxy.GetCampuses();
        }

        public int Add(Campus element, string username, string password)
        {
            return _proxy.AddCampus(element, username, password);
        }

        public void Edit(Campus element, string username, string password)
        {
            _proxy.EditCampus(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteCampus(code, username, password);
        }

        #endregion
    }
}