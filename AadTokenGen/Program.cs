using Microsoft.Extensions.Configuration.CommandLine;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;

namespace AadTokenGen
{
    class Program
    {
        /// <summary>
        /// NOT WORKING RIGHT NOW!!!!
        /// </summary>
        /// <param name="args"></param>
        // WARNING: Input Arguments are currentlly not validated!
        // Enter arguments exactly in expected order.
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

        // NOT Used. Test for MSAL lib.
        //protected async void createTokenByMsal()
        //{
        //    Microsoft.Identity.Client.PublicClientApplication myApp = new Microsoft.Identity.Client.PublicClientApplication("328f0a0a-3f19-448c-8f53-e290feba98bf");
        //    //// let's see if we have a user in our belly already
        //    //try
        //    //{
        //    //    ClientAuthentication
        //    //    AuthenticationResult ar =
        //    //        await App.PCA.AcquireTokenSilentAsync(App.Scopes, App.PCA.Users.FirstOrDefault());
        //    //    RefreshUserData(ar.AccessToken);
        //    //    btnSignInSignOut.Text = "Sign out";
        //    //}
        //    //catch
        //    //{
        //    //    // doesn't matter, we go in interactive more
        //    //    btnSignInSignOut.Text = "Sign in";
        //    //}
        //}

    }
}
