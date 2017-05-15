using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace AadTokenGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Genereting token...");

            var token = createToken(args[0], args[1], args[2], args[3], args[4]);

            Console.WriteLine(token);
        }

        private static string createToken(string userName, string clientId, string resource, string redirectUri, string authority = null)
        {
            AuthenticationContext authContext;

            if (authority == null)
                authContext = new AuthenticationContext(string.Format("https://login.microsoftonline.com/{0}", authority));
            else
                authContext = new AuthenticationContext("https://login.microsoftonline.com/common");

            UserCredential uc = new UserCredential(userName);

            AuthenticationResult result2 =
                authContext.AcquireTokenAsync(resource,
                clientId,
                new Uri(redirectUri), null).Result;

                var aHeader = result2.CreateAuthorizationHeader();

            return aHeader;
        }
    }
}
