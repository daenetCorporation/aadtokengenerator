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
