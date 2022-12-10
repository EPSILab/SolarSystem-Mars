namespace SolarSystem.Mars.Model.Helpers
{
    public static class PasswordEncoder
    {
        public static string Encode(string word)
        {
            return MD5HasherHelper.Hash(string.Format("epsilab_{0}", word));
        }
    }
}