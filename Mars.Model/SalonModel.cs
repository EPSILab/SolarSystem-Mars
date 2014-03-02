﻿using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class ShowModel : IReaderLimit<Show>
    {
        #region Attributes

        private readonly IShowManager _proxy = new ShowManagerClient();

        #endregion

        #region IReaderLimit methods

        public Show Get(int code)
        {
            return _proxy.GetShow(code);
        }

        public IList<Show> Get(int indexFirstResult, int numberOfResults)
        {
            return _proxy.GetShows(indexFirstResult, numberOfResults);
        }

        public int Add(Show element, string username, string password)
        {
            return _proxy.AddShow(element, username, password);
        }

        public void Edit(Show element, string username, string password)
        {
            _proxy.EditShow(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteShow(code, username, password);
        }

        #endregion
    }
}