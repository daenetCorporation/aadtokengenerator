using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace AadTokenGen
{
    class Program
    {
        // WARNING: Input Arguments are currentlly not validated!
        // Enter arguments exactly in expected order.
        static void Main(string[] args)
        {
            Console.WriteLine("Genereting token...");

            // TODO..
            //CommandLineConfigurationProvider cmdLineConfig = new CommandLineConfigurationProvider(args);
            //cmdLineConfig.Load();
            //string userName;
            //cmdLineConfig.TryGet("userName", out userName);

            //string clientId;
            //if (!cmdLineConfig.TryGet("clientId", out clientId))
            //    throw new Exception("'clientId' argument must be specified.");


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

            string aHeader;

            if (userName == null)
            {
                AuthenticationResult result =
                     authContext.AcquireTokenAsync(resource,
                     clientId,
                     new Uri(redirectUri), null).Result;

                aHeader = result.CreateAuthorizationHeader();              
            }
            else
            {
                AuthenticationResult result =
                     authContext.AcquireTokenAsync(resource, clientId,
                     new UserCredential(userName)).Result;

                aHeader = result.CreateAuthorizationHeader();
            }

            return aHeader;
        }
    }
}
