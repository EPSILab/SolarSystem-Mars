using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class PubliciteModel : IReader<Publicite>
    {
        #region Attributes

        private readonly IPubliciteManager _proxy = new PubliciteManagerClient();

        #endregion

        #region IReader methods

        public Publicite Get(int code)
        {
            return _proxy.GetPublicite(code);
        }

        public IList<Publicite> Get()
        {
            return _proxy.GetPublicites();
        }

        public int Add(Publicite element, string username, string password)
        {
            return _proxy.AddPublicite(element, username, password);
        }

        public void Edit(Publicite element, string username, string password)
        {
            _proxy.EditPublicite(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeletePublicite(code, username, password);
        }

        #endregion
    }
}