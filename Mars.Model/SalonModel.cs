using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class SalonModel : IReaderLimit<Salon>
    {
        #region Attributes

        private readonly ISalonManager _proxy = new SalonManagerClient();

        #endregion

        #region IReaderLimit methods

        public Salon Get(int code)
        {
            return _proxy.GetSalon(code);
        }

        public IList<Salon> Get(int indexFirstResult, int numberOfResults)
        {
            return _proxy.GetSalons(indexFirstResult, numberOfResults);
        }

        public int Add(Salon element, string username, string password)
        {
            return _proxy.AddSalon(element, username, password);
        }

        public void Edit(Salon element, string username, string password)
        {
            _proxy.EditSalon(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteSalon(code, username, password);
        }

        #endregion
    }
}