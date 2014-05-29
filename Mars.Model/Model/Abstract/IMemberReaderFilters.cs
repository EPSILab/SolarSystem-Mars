using System.Collections.Generic;
using SolarSystem.Mars.Model.ManagersService;

namespace SolarSystem.Mars.Model.Model.Abstract
{
    public interface IMemberReaderFilters : IReaderFilters<Member, Campus>
    {
        IList<Member> GetInactives();
    }
}
