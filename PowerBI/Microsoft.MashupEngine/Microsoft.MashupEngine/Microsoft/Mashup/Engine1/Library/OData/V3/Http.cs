using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008B4 RID: 2228
	internal static class Http
	{
		// Token: 0x06003FB8 RID: 16312 RVA: 0x000D355C File Offset: 0x000D175C
		public static HttpResponseData GetResult(Uri uri, ODataEnvironment environment)
		{
			return Http.GetResponse(environment.HttpResource, environment.ServiceUri, uri, environment.Headers, environment.Credentials, environment.Settings.ProposedResultContentTypes, true, environment.Host, environment.Settings, environment.UserSettings);
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000D35A8 File Offset: 0x000D17A8
		private static HttpResponseData GetResponse(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, string contentType, bool throwOnBadRequest, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			return HttpResponseHandler.GetResponseStream(resource, serviceUri, uri, headers, credentials, contentType, throwOnBadRequest, false, host, settings, userSettings, settings.ServerVersion, true);
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x000D35D4 File Offset: 0x000D17D4
		public static HttpResponseData GetAnyResult(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings)
		{
			return Http.GetResponse(resource, serviceUri, uri, headers, credentials, settings.ProposedServiceDocumentContentTypes, false, host, settings, userSettings);
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000D35FC File Offset: 0x000D17FC
		public static string GetScalarResponse(Uri uri, ODataEnvironment environment)
		{
			string text;
			using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(Http.GetResponse(environment.HttpResource, environment.ServiceUri, uri, environment.Headers, environment.Credentials, "text/plain", true, environment.Host, environment.Settings, environment.UserSettings)))
			{
				using (StreamReader streamReader = new StreamReader(odataResponseMessage.GetStream()))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000D368C File Offset: 0x000D188C
		public static IEdmModel GetServiceMetadataDocument(HttpResource resource, Uri uri, Value headers, ResourceCredentialCollection credentials, IEngineHost host, ODataSettings settings, ODataUserSettings userSettings, out bool isSharePoint)
		{
			IEdmModel edmModel;
			try
			{
				using (ODataResponseMessage odataResponseMessage = new ODataResponseMessage(Http.GetResponse(resource, null, uri, headers, credentials, "application/xml", true, host, settings, userSettings)))
				{
					isSharePoint = odataResponseMessage.Headers.Any((KeyValuePair<string, string> kvp) => kvp.Key.Equals("MicrosoftSharePointTeamServices"));
					using (ODataMessageReader odataMessageReader = new ODataMessageReader(odataResponseMessage, ODataResponse.DefaultReaderSettings))
					{
						edmModel = odataMessageReader.ReadMetadataDocument();
					}
				}
			}
			catch (WebException ex)
			{
				MashupHttpWebResponse mashupHttpWebResponse = ex.Response as MashupHttpWebResponse;
				if (mashupHttpWebResponse != null && mashupHttpWebResponse.StatusCode == HttpStatusCode.NotFound)
				{
					throw ODataCommonErrors.MissingMetadataDocument(ex);
				}
				throw ODataCommonErrors.RequestFailed(host, ex, uri, resource);
			}
			catch (ODataException ex2)
			{
				throw ODataCommonErrors.InvalidMetadataDocument(host, ex2, resource);
			}
			return edmModel;
		}

		// Token: 0x0400216B RID: 8555
		private const string SharePointOnlineHeader = "MicrosoftSharePointTeamServices";
	}
}
