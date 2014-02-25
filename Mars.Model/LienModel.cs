using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class LienModel : IReader<Lien>
    {
        #region Attributes

        private readonly ILienManager _proxy = new LienManagerClient();

        #endregion

        #region IReader methods

        public Lien Get(int code)
        {
            return _proxy.GetLien(code);
        }

        public IList<Lien> Get()
        {
            return _proxy.GetLiens();
        }

        public int Add(Lien element, string username, string password)
        {
            return _proxy.AddLien(element, username, password);
        }

        public void Edit(Lien element, string username, string password)
        {
            _proxy.EditLien(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteLien(code, username, password);
        }

        #endregion
    }
}