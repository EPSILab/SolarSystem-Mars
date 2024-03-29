﻿using Ninject;
using SolarSystem.Mars.Model.Infrastructure;
using SolarSystem.Mars.ViewController.Controllers;
using SolarSystem.Mars.ViewController.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SolarSystem.Mars.ViewController.Infrastructure.Concrete
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        #region Constructor

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        #endregion

        #region Attributes

        private readonly IKernel _kernel;

        #endregion

        #region Methods

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // ViewController Injections
            _kernel.Bind<IAuthProvider>().To<WebserviceAuthProvider>();
            _kernel.Bind<IFilterProvider>().To<NinjectFilterProvider>().InSingletonScope();

            // Controller Injections
            _kernel.Bind<ErrorController>().ToSelf();

            // Model Module Load
            _kernel.Load<ModelModule>();
        }

        #endregion
    }
}