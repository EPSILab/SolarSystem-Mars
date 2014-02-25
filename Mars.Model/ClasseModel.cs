using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class ClasseModel : IReader<Classe>, IAvailable<Classe>
    {
        #region Attributes

        private readonly IClasseManager _proxy = new ClasseManagerClient();

        #endregion

        #region IReader methods

        public Classe Get(int code)
        {
            return _proxy.GetClasse(code);
        }

        public IList<Classe> Get()
        {
            return _proxy.GetClasses();
        }

        public int Add(Classe element, string username, string password)
        {
            return _proxy.AddClasse(element, username, password);
        }

        public void Edit(Classe element, string username, string password)
        {
            _proxy.EditClasse(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteClasse(code, username, password);
        }

        #endregion

        #region IAvailable methods

        public IList<Classe> GetAvailables()
        {
            return _proxy.GetClassesAvailables();
        }

        #endregion
    }
}