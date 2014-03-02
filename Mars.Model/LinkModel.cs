using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class LinkModel : IReader<Link>
    {
        #region Attributes

        private readonly ILinkManager _proxy = new LinkManagerClient();

        #endregion

        #region IReader methods

        public Link Get(int code)
        {
            return _proxy.GetLink(code);
        }

        public IList<Link> Get()
        {
            return _proxy.GetLinks();
        }

        public int Add(Link element, string username, string password)
        {
            return _proxy.AddLink(element, username, password);
        }

        public void Edit(Link element, string username, string password)
        {
            _proxy.EditLink(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteLink(code, username, password);
        }

        #endregion
    }
}