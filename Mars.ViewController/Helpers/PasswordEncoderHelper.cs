namespace SolarSystem.Mars.ViewController.Helpers
{
    static class PasswordEncoderHelper
    {
        public static string Encode(string word)
        {
            return MD5HasherHelper.Hash(string.Format("epsilab_{0}", word));
        }
    }
}