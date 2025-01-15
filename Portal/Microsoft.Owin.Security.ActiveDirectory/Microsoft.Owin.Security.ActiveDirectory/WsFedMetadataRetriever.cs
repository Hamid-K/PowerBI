using System;
using System.IO;
using System.Net.Http;
using System.Xml;
using Microsoft.IdentityModel.Protocols.WsFederation;

namespace Microsoft.Owin.Security.ActiveDirectory
{
	// Token: 0x02000006 RID: 6
	internal static class WsFedMetadataRetriever
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002364 File Offset: 0x00000564
		public static IssuerSigningKeys GetSigningKeys(string metadataEndpoint, TimeSpan backchannelTimeout, HttpMessageHandler backchannelHttpHandler)
		{
			IssuerSigningKeys issuerSigningKeys;
			using (HttpClient metadataRequest = new HttpClient(backchannelHttpHandler, false))
			{
				metadataRequest.Timeout = backchannelTimeout;
				using (HttpResponseMessage metadataResponse = metadataRequest.GetAsync(metadataEndpoint).Result)
				{
					metadataResponse.EnsureSuccessStatusCode();
					Stream metadataStream = metadataResponse.Content.ReadAsStreamAsync().Result;
					using (XmlReader metaDataReader = XmlReader.Create(metadataStream, WsFedMetadataRetriever.SafeSettings))
					{
						WsFederationMetadataSerializer serializer = new WsFederationMetadataSerializer();
						WsFederationConfiguration wsFederationConfiguration = serializer.ReadMetadata(metaDataReader);
						issuerSigningKeys = new IssuerSigningKeys
						{
							Issuer = wsFederationConfiguration.Issuer,
							Keys = wsFederationConfiguration.SigningKeys
						};
					}
				}
			}
			return issuerSigningKeys;
		}

		// Token: 0x0400001D RID: 29
		private static readonly XmlReaderSettings SafeSettings = new XmlReaderSettings
		{
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit,
			ValidationType = ValidationType.None
		};
	}
}
