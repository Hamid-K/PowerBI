using System;
using System.IO;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000405 RID: 1029
	internal static class SharePointServerVersion
	{
		// Token: 0x06002317 RID: 8983 RVA: 0x00062848 File Offset: 0x00060A48
		public static SharePointApiVersion GetServerVersion(IEngineHost host, string url, ResourceCredentialCollection credentials, bool isFilesDeepEnumeration)
		{
			UriBuilder uriBuilder = new UriBuilder(new Uri(url).GetLeftPart(UriPartial.Authority));
			uriBuilder.Path = "/_vti_pvt/service.cnf";
			try
			{
				MashupHttpWebRequest mashupHttpWebRequest = (MashupHttpWebRequest)host.CreateWebRequest(credentials.Resource, uriBuilder.Uri);
				HttpResourceCredentialDispatcher.ApplyCredentialsToRequest(mashupHttpWebRequest, credentials, host, null);
				mashupHttpWebRequest.Accept = "text/plain";
				mashupHttpWebRequest.Method = "GET";
				using (WebResponse response = mashupHttpWebRequest.GetResponse())
				{
					return SharePointServerVersion.TryGetVersion(response, isFilesDeepEnumeration);
				}
			}
			catch (WebException ex)
			{
				if (ex.Response != null)
				{
					return SharePointServerVersion.TryGetVersion(ex.Response, isFilesDeepEnumeration);
				}
			}
			return SharePointApiVersion.SP14;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000628FC File Offset: 0x00060AFC
		private static SharePointApiVersion TryGetVersion(WebResponse response, bool isFilesDeepEnumeration)
		{
			using (Stream responseStream = response.GetResponseStream())
			{
				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					string text;
					while ((text = streamReader.ReadLine()) != null)
					{
						if ((text.StartsWith("vti_extenderversion:SR|15", StringComparison.OrdinalIgnoreCase) && !isFilesDeepEnumeration) || text.StartsWith("vti_extenderversion:SR|16", StringComparison.OrdinalIgnoreCase))
						{
							return SharePointApiVersion.SP15;
						}
					}
				}
			}
			return SharePointApiVersion.SP14;
		}
	}
}
