using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class NinjectFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IKernel _kernel;

        public NinjectFilterProvider(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            IEnumerable<Filter> filters = base.GetFilters(controllerContext, actionDescriptor);

            foreach (Filter filter in filters)
                _kernel.Inject(filter.Instance);

            return filters;
        }
    }
}