using SolarSystem.Mars.Model.Interfaces;
using SolarSystem.Mars.Model.ManagersService;
using System.Collections.Generic;

namespace SolarSystem.Mars.Model
{
    class MemberModel : ILogin, IReaderFilters<Member, Campus>
    {
        #region Attributes

        private readonly IMemberManager _proxy = new MemberManagerClient();

        #endregion

        #region ILogin methods

        public Member Login(string username, string password)
        {
            return _proxy.Login(username, password);
        }

        public bool Exists(string username, string password)
        {
            return _proxy.ExistsMember(username, password);
        }

        public bool Exists(string username)
        {
            return _proxy.ExistsUsername(username);
        }

        public int Register(Member member, string newPassword)
        {
            return _proxy.Register(member, newPassword);
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

        public Member Get(int code)
        {
            return _proxy.GetMember(code);
        }

        public IList<Member> Get()
        {
            return _proxy.GetMembersActives();
        }

        public IList<Member> Get(Campus campus)
        {
            return _proxy.GetMembersBureau(campus);
        }

        public int Add(Member element, string username, string password)
        {
            return _proxy.AddMember(element, username, password);
        }

        public void Edit(Member element, string username, string password)
        {
            _proxy.EditMember(element, username, password);
        }

        public void Delete(int code, string username, string password)
        {
            _proxy.DeleteMember(code, username, password);
        }

        #endregion
    }
}