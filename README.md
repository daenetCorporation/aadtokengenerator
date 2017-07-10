# AAD Token Generator tool
Creates OAuth tokens by using of Azure Active Directory.
Currentlly .NET Desktop version (*AadTokenGenDotNet*) is supported only. .NET Core version does not implement some token related core functionalities yet.

## Usage
````
aadtokengendotnet.exe clientId resource redirectUri [optional authority] [optional secret]
````

*Remarks*
If authority is null, token will be requested by AAD common endpoint.
If secret is null, sign-in dialog window will be showed for user credential input.

##  Creating Token with client credentials
````
aadtokengendotnet.exe --clientId=YOURCLIENTID --resource=https://YOURDOMAIN.onmicrosoft.com/YOURAPPNAME --redirectUri=http://netsummitnativeapp
````

## Creating token with client secret
*Client secret scenario*
````
aadTokenGenDotNet.exe --secret=YOURSECRETKEY --clientId=YOURCLIENTID --resource=https://YOURDOMAIN.onmicrosoft.de/YOURAPPNAME --redirectUri=http://netsummitnativeapp
````

## Token output
````
Genereting token...
Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzIOGFkZi0yY2ZlLTQ2yc29uYXRpb24iLCJzdWIiOiJaVnI4ekNhcDc1MHhvNU9ZRU5qa0FMYmdXelBLWEU5Y2xfZ2VMMS0xWURRIiwidGlkIjoiM2IwZjc4ZmQtMDFkNS00YzQzLWEyYWUtM2E2ZjZiOGNhYmU3IiwidW5pcXVlX25hbWUiOiJ1c2VyMUBkYW1pcmRvYnJpY2RhZW5ldC5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJ1c2VyMUBkYW1pcmRvYnJpY2RhZW5ldC5vbm1pY3Jvc29mdC5jb20iLCJ2ZXIiOiIxLjAifQ.behW4Wo9Lpxt4X19LpziQaRvzfxIAtwcVGs50wYLeLz-I65VbT5ZWuGTPPVQ77Jsi-NRDZB5akrtnM9JuN79Kpk3neSaGlIDV--_N8B3_HL-4YESbsffo1YXNgsJxlBZS6Cy7b1bqa3zOn6oIUkQlZqsXsJzjqy92UVXFnsm1KxHgf3jnNElk250UGUZB7CAxxqq7aG6dpVBQ1dCMHk5IMItWiN1NW67g642HkkZEc9v5HDjNwqZdW0054DjEcVgV_j5Y3DPRSFPoL2Uqejv7GlzOtPpZ-lJzouWnSy61R20Ud92o2WLQ9EZXTbTBWmEj181g7_Uj5EGT-i2uwwPsg
````
