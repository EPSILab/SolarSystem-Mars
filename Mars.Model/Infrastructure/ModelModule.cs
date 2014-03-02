﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Modules;
using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;

namespace SolarSystem.Mars.Model.Infrastructure
{
    public class ModelModule : NinjectModule
    {
        public override void Load()
        {
            // Model Injections
            Bind<IReader<Promotion>>().To<PromotionModel>().InSingletonScope();
            Bind<IAvailable<Promotion>>().To<PromotionModel>().InSingletonScope();
            Bind<IReaderLimit<Conference>>().To<ConferenceModel>().InSingletonScope();
            Bind<IReader<Link>>().To<LinkModel>().InSingletonScope();
            Bind<IReaderFilters<Member, Campus>>().To<MemberModel>().InSingletonScope();
            Bind<IReader<Member>>().To<MemberModel>().InSingletonScope();
            Bind<ILogin>().To<MemberModel>().InSingletonScope();
            Bind<IReaderLimit<News>>().To<NewsModel>().InSingletonScope();
            Bind<IReaderLimit<Project>>().To<ProjectModel>().InSingletonScope();
            Bind<IReader<Slide>>().To<SlideModel>().InSingletonScope();
            Bind<IReaderLimit<Show>>().To<ShowModel>().InSingletonScope();
            Bind<IReader<Campus>>().To<CampusModel>().InSingletonScope();
        }
    }
}
