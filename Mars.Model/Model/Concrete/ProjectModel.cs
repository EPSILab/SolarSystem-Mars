using System.Collections.Generic;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;

namespace SolarSystem.Mars.Model.Model.Concrete
{
    class ProjectModel : IReaderLimit<Project>
    {
        #region Attributes

        private readonly IProjectManager _proxy = new ProjectManagerClient();

        #endregion

        #region IReaderLimit methods

        public Project Get(int code)
        {
            return _proxy.GetProject(code);
        }

        public IList<Project> Get(int indexFirstResult, int numberOfResults)
        {
            return _proxy.GetProjects(indexFirstResult, numberOfResults);
        }

        public int Add(Project element, string username, string password)
        {
            return _proxy.AddProject(element, username, password);
        }

        public void Edit(Project element, string username, string password)
        {
            _proxy.EditProject(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteProject(code, username, password);
        }

        #endregion
    }
}