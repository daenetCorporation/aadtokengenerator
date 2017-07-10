using System;
using System.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Extensions.Configuration.CommandLine;

namespace AddTokenGenDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Generating token...");

                CommandLineConfigurationProvider cmdLineConfig = new CommandLineConfigurationProvider(args);

                cmdLineConfig.Load();

                if (!cmdLineConfig.TryGet("clientId", out string clientId))
                    throw new ArgumentException("'clientId' argument must be specified.");

                if (!cmdLineConfig.TryGet("resource", out string resource))
                    throw new ArgumentException("'resource' argument must be specified");

                if (!cmdLineConfig.TryGet("redirectUri", out string redirectUri))
                    throw new ArgumentException("'redirectUri' argument must be specified");

                cmdLineConfig.TryGet("authority", out string authority);
                cmdLineConfig.TryGet("secret", out string secret);

                string token;
                if (string.IsNullOrEmpty(secret) == true)
                    token = createTokenFromClientCredentials(clientId, resource, redirectUri, authority);
                else
                {
                    token = createTokenFromClientSecret(secret, clientId, resource, redirectUri, authority);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(token);

                Console.WriteLine("Press Enter to exit the application.");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                if (e != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Acquiring token failed with the following error:" + Environment.NewLine + e);
                }
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Gets the AuthenticationContext
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        private static AuthenticationContext getAuthenticationContext(string authority)
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
        /// Creates token by using of grant_type=password&username.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="clientId"></param>
        /// <param name="resource"></param>
        /// <param name="redirectUri"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        private static string createTokenFromClientCredentials(string clientId, string resource, string redirectUri, string authority = null)
        {
            AuthenticationContext authContext = getAuthenticationContext(authority);

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
        private static string createTokenFromClientSecret(string secret, string clientId, string resource, string redirectUri, string authority = null)
        {
            AuthenticationContext authContext = getAuthenticationContext(authority);

            ClientCredential clientCred = new ClientCredential(clientId, secret);

            AuthenticationResult authResult =
                authContext.AcquireTokenAsync(resource, clientCred).Result;

            var aHeader = authResult.CreateAuthorizationHeader();

            return aHeader;
        }
    }
}
