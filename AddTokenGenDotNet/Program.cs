using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.Configuration.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

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

                string secret = null;
                string userName;
                if (!cmdLineConfig.TryGet("userName", out userName))
                {
                    if (!cmdLineConfig.TryGet("secret", out secret))
                    {
                        throw new Exception("'userName' or 'secret' argument must be specified.");
                    }
                }

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

                string token;

                if(String.IsNullOrEmpty(userName) == false)
                    token = createTokenFromUserNamePwd(userName, clientId, resource, redirectUri, authority);
                else
                    token = createTokenFromClientCredentials(secret, clientId, resource, redirectUri, authority);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(token);
            }
            catch (Exception e)
            {
               
                if (e != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e);
                }
                Console.ResetColor();
                Console.ReadLine();
            }


            //var token = createToken(args[0], args[1], args[2], args[3], args.Length == 5 ? args[4] : null);

            //Console.WriteLine(token);
        }


        private static AuthenticationContext getContext(string authority)
        {
            var aadUrl = ConfigurationManager.AppSettings["AadAuthorityUrl"];

            AuthenticationContext authContext;

            if (authority != null)
                authContext = new AuthenticationContext($"{aadUrl}/{authority}");
            else
                authContext = new AuthenticationContext($"{aadUrl}/common");

            return authContext;
        }


        /// <summary>
        /// Creates token by using of grant_type=pasword.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="clientId"></param>
        /// <param name="resource"></param>
        /// <param name="redirectUri"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        private static string createTokenFromUserNamePwd(string userName, string clientId, string resource, string redirectUri, string authority = null)
        {
            AuthenticationContext authContext = getContext(authority);

            //UserCredential uc = new UserCredential(userName);

            AuthenticationResult authResult =
                authContext.AcquireTokenAsync(resource,
                clientId,
                new Uri(redirectUri), new PlatformParameters(PromptBehavior.Auto)).Result;

            var aHeader = authResult.CreateAuthorizationHeader();

            return aHeader;
        }



        /// <summary>
        /// Creates token by using of grant_type=client_credentials.
        /// </summary>
        private static string createTokenFromClientCredentials(string secret, string clientId, string resource, string redirectUri, string authority = null)
        {
            AuthenticationContext authContext = getContext(authority);
            
            ClientCredential clientCred = new ClientCredential(clientId, secret);
     
            AuthenticationResult authResult =
                authContext.AcquireTokenAsync(resource, clientCred).Result;

            var aHeader = authResult.CreateAuthorizationHeader();

            return aHeader;
        }
    }
}
