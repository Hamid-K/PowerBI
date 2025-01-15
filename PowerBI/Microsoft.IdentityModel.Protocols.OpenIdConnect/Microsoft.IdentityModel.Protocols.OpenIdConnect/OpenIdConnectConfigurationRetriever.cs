using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Json;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.IdentityModel.Protocols.OpenIdConnect
{
	// Token: 0x02000004 RID: 4
	public class OpenIdConnectConfigurationRetriever : IConfigurationRetriever<OpenIdConnectConfiguration>
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002751 File Offset: 0x00000951
		public static Task<OpenIdConnectConfiguration> GetAsync(string address, CancellationToken cancel)
		{
			return OpenIdConnectConfigurationRetriever.GetAsync(address, new HttpDocumentRetriever(), cancel);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000275F File Offset: 0x0000095F
		public static Task<OpenIdConnectConfiguration> GetAsync(string address, HttpClient httpClient, CancellationToken cancel)
		{
			return OpenIdConnectConfigurationRetriever.GetAsync(address, new HttpDocumentRetriever(httpClient), cancel);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000276E File Offset: 0x0000096E
		Task<OpenIdConnectConfiguration> IConfigurationRetriever<OpenIdConnectConfiguration>.GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
		{
			return OpenIdConnectConfigurationRetriever.GetAsync(address, retriever, cancel);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002778 File Offset: 0x00000978
		public static async Task<OpenIdConnectConfiguration> GetAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
		{
			if (string.IsNullOrWhiteSpace(address))
			{
				throw LogHelper.LogArgumentNullException("address");
			}
			if (retriever == null)
			{
				throw LogHelper.LogArgumentNullException("retriever");
			}
			string text = await retriever.GetDocumentAsync(address, cancel).ConfigureAwait(false);
			LogHelper.LogVerbose("IDX21811: Deserializing the string: '{0}' obtained from metadata endpoint into openIdConnectConfiguration object.", new object[] { text });
			OpenIdConnectConfiguration openIdConnectConfiguration = JsonConvert.DeserializeObject<OpenIdConnectConfiguration>(text);
			if (!string.IsNullOrEmpty(openIdConnectConfiguration.JwksUri))
			{
				LogHelper.LogVerbose("IDX21812: Retrieving json web keys from: '{0}'.", new object[] { openIdConnectConfiguration.JwksUri });
				string text2 = await retriever.GetDocumentAsync(openIdConnectConfiguration.JwksUri, cancel).ConfigureAwait(false);
				LogHelper.LogVerbose("IDX21813: Deserializing json web keys: '{0}'.", new object[] { openIdConnectConfiguration.JwksUri });
				openIdConnectConfiguration.JsonWebKeySet = JsonConvert.DeserializeObject<JsonWebKeySet>(text2);
				foreach (SecurityKey securityKey in openIdConnectConfiguration.JsonWebKeySet.GetSigningKeys())
				{
					openIdConnectConfiguration.SigningKeys.Add(securityKey);
				}
			}
			return openIdConnectConfiguration;
		}
	}
}
