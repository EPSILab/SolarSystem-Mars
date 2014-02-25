using System;
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
            Bind<IReader<Classe>>().To<ClasseModel>().InSingletonScope();
            Bind<IAvailable<Classe>>().To<ClasseModel>().InSingletonScope();
            Bind<IReaderLimit<Conference>>().To<ConferenceModel>().InSingletonScope();
            Bind<IReader<Lien>>().To<LienModel>().InSingletonScope();
            Bind<IReaderFilters<Membre, Ville>>().To<MembreModel>().InSingletonScope();
            Bind<IReader<Membre>>().To<MembreModel>().InSingletonScope();
            Bind<ILogin>().To<MembreModel>().InSingletonScope();
            Bind<IReaderLimit<News>>().To<NewsModel>().InSingletonScope();
            Bind<IReaderLimit<Projet>>().To<ProjetModel>().InSingletonScope();
            Bind<IReader<Publicite>>().To<PubliciteModel>().InSingletonScope();
            Bind<IReaderLimit<Salon>>().To<SalonModel>().InSingletonScope();
            Bind<IReader<Ville>>().To<VilleModel>().InSingletonScope();
        }
    }
}
