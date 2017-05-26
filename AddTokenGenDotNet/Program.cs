using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.Configuration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddTokenGenDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Genereting token...");


                CommandLineConfigurationProvider cmdLineConfig = new CommandLineConfigurationProvider(args);

                cmdLineConfig.Load();

                string userName;
                if (!cmdLineConfig.TryGet("userName", out userName))
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


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(token);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ReadKey();
            }


            //var token = createToken(args[0], args[1], args[2], args[3], args.Length == 5 ? args[4] : null);

            //Console.WriteLine(token);
        }

        private static string createToken(string userName, string clientId, string resource, string redirectUri, string authority = null)
        {
            AuthenticationContext authContext;

            //http://www.cloudidentity.com/blog/2014/07/08/using-adal-net-to-authenticate-users-via-usernamepassword/

            if (authority != null)
                authContext = new AuthenticationContext(string.Format("https://login.microsoftonline.com/{0}", authority));
            else
                authContext = new AuthenticationContext("https://login.microsoftonline.com/common");

            UserCredential uc = new UserCredential(userName);

            AuthenticationResult result2 =
                authContext.AcquireTokenAsync(resource,
                clientId,
                new Uri(redirectUri), new PlatformParameters(PromptBehavior.Always)).Result;

            var aHeader = result2.CreateAuthorizationHeader();

            return aHeader;
        }
    }
}
