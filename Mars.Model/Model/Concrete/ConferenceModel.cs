using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class ConferenceModel : IReaderLimit<Conference>
    {
        #region Attributes

        private readonly IConferenceManager _proxy = new ConferenceManagerClient();

        #endregion

        #region IReaderLimit methods

        public Conference Get(int code)
        {
            return _proxy.GetConference(code);
        }

        public IList<Conference> Get(int indexFirstResult, int numberOfResults)
        {
            return _proxy.GetConferences(indexFirstResult, numberOfResults);
        }

        #endregion

        #region IManager methods

        public int Add(Conference element, string username, string password)
        {
            return _proxy.AddConference(element, username, password);
        }

        public void Edit(Conference element, string username, string password)
        {
            _proxy.EditConference(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteConference(code, username, password);
        }

        #endregion
    }
}