using System.Collections.Generic;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;

namespace SolarSystem.Mars.Model.Model.Concrete
{
    class NewsModel : IReaderLimit<News>
    {
        #region Attributes

        private readonly INewsManager _proxy = new NewsManagerClient();

        #endregion

        #region IReader methods

        public News Get(int code)
        {
            return _proxy.GetNews(code);
        }

        public IList<News> Get(int indexFirstResult, int numberOfResults)
        {
            return _proxy.GetListNewsLimited(indexFirstResult, numberOfResults);
        }

        public int Add(News element, string username, string password)
        {
            return _proxy.AddNews(element, username, password);
        }

        public void Edit(News element, string username, string password)
        {
            _proxy.EditNews(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteNews(code, username, password);
        }

        #endregion
    }
}