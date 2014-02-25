using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class ProjetModel : IReaderLimit<Projet>
    {
        #region Attributes

        private readonly IProjetManager _proxy = new ProjetManagerClient();

        #endregion

        #region IReaderLimit methods

        public Projet Get(int code)
        {
            return _proxy.GetProjet(code);
        }

        public IList<Projet> Get(int indexFirstResult, int numberOfResults)
        {
            return _proxy.GetProjets(indexFirstResult, numberOfResults);
        }

        public int Add(Projet element, string username, string password)
        {
            return _proxy.AddProjet(element, username, password);
        }

        public void Edit(Projet element, string username, string password)
        {
            _proxy.EditProjet(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteProjet(code, username, password);
        }

        #endregion
    }
}