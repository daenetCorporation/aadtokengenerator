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
            //string authority = "3b0f78fd-01d5-4c43-a2ae-3a6f6b8cabe7"; // tenant
                                                                       // string resource = "http://iottsystems/mywerkservice.de"; // "027d4e8d-338a-4dfa-85da-269a43849137" This is the client id of the service.
                                                                       //string clientId = "7315c7e4-473f-4734-8f34-c2e408dd2aed";
                                                                       //string redirectUri = "https://localhost:63803/";

            AuthenticationContext authContext;

            //http://www.cloudidentity.com/blog/2014/07/08/using-adal-net-to-authenticate-users-via-usernamepassword/

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