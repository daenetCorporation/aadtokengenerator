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
            CommandLineConfigurationProvider cmdLineConfig = new CommandLineConfigurationProvider(args);
            cmdLineConfig.Load();

            string userName;
            if(!cmdLineConfig.TryGet("userName", out userName))
            throw new Exception("'userName' argument must be specified.");

            string clientId;
            if (!cmdLineConfig.TryGet("clientId", out clientId))
                throw new Exception("'clientId' argument must be specified.");

            string resource;
            if (!cmdLineConfig.TryGet("resource", out resource))
                throw new Exception("'resource' argument must be specified");

            string redirectUri;
            if (!cmdLineConfig.TryGet("redirectUri", out redirectUri))
                throw new Exception("'redirectUri' argument must be specified");

            string authority;
            cmdLineConfig.TryGet("authority", out authority);

            var token = createToken(userName, clientId, resource, redirectUri, authority);

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
