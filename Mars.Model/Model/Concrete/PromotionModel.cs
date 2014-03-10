using System.Collections.Generic;
using SolarSystem.Mars.Model.ManagersService;
using SolarSystem.Mars.Model.Model.Abstract;

namespace SolarSystem.Mars.Model.Model.Concrete
{
    class PromotionModel : IReader<Promotion>, IAvailable<Promotion>
    {
        #region Attributes

        private readonly IPromotionManager _proxy = new PromotionManagerClient();

        #endregion

        #region IReader methods

        public Promotion Get(int code)
        {
            return _proxy.GetPromotion(code);
        }

        public IList<Promotion> Get()
        {
            return _proxy.GetPromotions();
        }

        public int Add(Promotion element, string username, string password)
        {
            return _proxy.AddPromotion(element, username, password);
        }

        public void Edit(Promotion element, string username, string password)
        {
            _proxy.EditPromotion(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeletePromotion(code, username, password);
        }

        #endregion

        #region IAvailable methods

        public IList<Promotion> GetAvailables()
        {
            return _proxy.GetPromotionsAvailables();
        }

        #endregion
    }
}