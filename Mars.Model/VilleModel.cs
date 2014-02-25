using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class VilleModel : IReader<Ville>
    {
        #region Attributes

        private readonly IVilleManager _proxy = new VilleManagerClient();

        #endregion

        #region IReader methods

        public Ville Get(int code)
        {
            return _proxy.GetVille(code);
        }

        public IList<Ville> Get()
        {
            return _proxy.GetVilles();
        }

        public int Add(Ville element, string username, string password)
        {
            return _proxy.AddVille(element, username, password);
        }

        public void Edit(Ville element, string username, string password)
        {
            _proxy.EditVille(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteVille(code, username, password);
        }

        #endregion
    }
}