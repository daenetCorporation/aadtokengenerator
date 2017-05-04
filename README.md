# AAD Token Generatot tool
Creates OAuth tokens by using of Azure Active Directory.
Currentlly .NET Desktop version (*AddTokenGenDotNet*) works only. .NET Core version does not implement some token related core functionalities yet.

**Usage**
aadtokengendotnet.exe userName clientId resource redirectUri [optional authority]

userName,									clientId,							resource,								redirectUri,				authority = null
user1@YOURDOMAIN.onmicrosoft.com		328**CLIENTID**bf		https://YOURDOMAIN.onmicrosoft.com/YOURAPPNAME   http://netsummitnativeapp 3b0f78fd-01d5-4c43-a2ae-3a6f6b8cabe7

*Remarks*
If authority is null, token will be requested by AAD common endpoint.

**Example**
aadtokengendotnet.exe user1@YOURDOMAIN.onmicrosoft.com 328**CLIENTID**bf https://YOURDOMAIN.onmicrosoft.com/YOURAPPNAME http://netsummitnativeapp 3b0***be7

**Result example**
Genereting token...
Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6InowMzl6ZHNGdWl6cEJmQlZLMVRuMjVRSFlPMCIsImtpZCI6InowMzl6ZHNGdWl6cEJmQlZLMVRuMjVRSFlPMCJ9.eyJhdWQiOiJodHRwczovL2RhbWlyZG9icmljZGFlbmV0Lm9ubWljcm9zb2Z0LmNvbS9XZWJBcGlEZXNrdG9wIiwiaXNzIjoiaHR0cHM6Ly9zdHMud2luZG93cy5uZXQvM2IwZjc4ZmQtMDFkNS00YzQzLWEyYWUtM2E2ZjZiOGNhYmU3LyIsImlhdCI6MTQ5MzkzMjI3MywibmJmIjoxNDkzOTMyMjczLCJleHAiOjE0OTM5MzYxNzMsImFjciI6IjEiLCJhaW8iOiJZMlpnWUZpditkdnE5b0svL0JYQllyeVhkcjVoZG91N3hPRnVVMzdzYUFEVGhZeUhBdk1BIiwiYW1yIjpbInB3ZCJdLCJhcHBpZCI6IjMyOGYwYTBhLTNmMTktNDQ4Yy04ZjUzLWUyOTBmZWJhOThiZiIsImFwcGlkYWNyIjoiMCIsImVfZXhwIjoyNjI4MDAsImZhbWlseV9uYW1lIjoidGVzdCIsImdpdmVuX25hbWUiOiJ1c2VyMSIsImlwYWRkciI6IjIxNy4xMTAuOTAuNjAiLCJuYW1lIjoidXNlcjEiLCJvaWQiOiIzZWEwOGFkZi0yY2ZlLTQ2NWMtYTA4ZC0wNWRhZDQ2MmU4NzkiLCJwbGF0ZiI6IjMiLCJzY3AiOiJ1c2VyX2ltcGVyc29uYXRpb24iLCJzdWIiOiJaVnI4ekNhcDc1MHhvNU9ZRU5qa0FMYmdXelBLWEU5Y2xfZ2VMMS0xWURRIiwidGlkIjoiM2IwZjc4ZmQtMDFkNS00YzQzLWEyYWUtM2E2ZjZiOGNhYmU3IiwidW5pcXVlX25hbWUiOiJ1c2VyMUBkYW1pcmRvYnJpY2RhZW5ldC5vbm1pY3Jvc29mdC5jb20iLCJ1cG4iOiJ1c2VyMUBkYW1pcmRvYnJpY2RhZW5ldC5vbm1pY3Jvc29mdC5jb20iLCJ2ZXIiOiIxLjAifQ.behW4Wo9Lpxt4X19LpziQaRvzfxIAtwcVGs50wYLeLz-I65VbT5ZWuGTPPVQ77Jsi-NRDZB5akrtnM9JuN79Kpk3neSaGlIDV--_N8B3_HL-4YESbsffo1YXNgsJxlBZS6Cy7b1bqa3zOn6oIUkQlZqsXsJzjqy92UVXFnsm1KxHgf3jnNElk250UGUZB7CAxxqq7aG6dpVBQ1dCMHk5IMItWiN1NW67g642HkkZEc9v5HDjNwqZdW0054DjEcVgV_j5Y3DPRSFPoL2Uqejv7GlzOtPpZ-lJzouWnSy61R20Ud92o2WLQ9EZXTbTBWmEj181g7_Uj5EGT-i2uwwPsg