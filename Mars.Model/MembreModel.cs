using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class MembreModel : ILogin, IReaderFilters<Membre, Ville>
    {
        #region Attributes

        private readonly IMembreManager _proxy = new MembreManagerClient();

        #endregion

        #region ILogin methods

        public Membre Login(string username, string password)
        {
            return _proxy.Login(username, password);
        }

        public bool Exists(string username, string password)
        {
            return _proxy.ExistsMembre(username, password);
        }

        public bool Exists(string username)
        {
            return _proxy.ExistsUsername(username);
        }

        public int Register(Membre membre)
        {
            return _proxy.Register(membre);
        }

        public void RequestLostPassword(string username, string email)
        {
            _proxy.RequestLostPassword(username, email);
        }

        public void SetNewPasswordAfterLost(string username, string newPassword, string key)
        {
            _proxy.SetNewPasswordAfterLost(username, newPassword, key);
        }

        #endregion

        #region IReaderFilters methods

        public Membre Get(int code)
        {
            return _proxy.GetMembre(code);
        }

        public IList<Membre> Get()
        {
            return _proxy.GetMembresActives();
        }

        public IList<Membre> Get(Ville ville)
        {
            return _proxy.GetMembresBureau(ville);
        }

        public int Add(Membre element, string username, string password)
        {
            return _proxy.AddMembre(element, username, password);
        }

        public void Edit(Membre element, string username, string password)
        {
            _proxy.EditMembre(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteMembre(code, username, password);
        }

        #endregion
    }
}