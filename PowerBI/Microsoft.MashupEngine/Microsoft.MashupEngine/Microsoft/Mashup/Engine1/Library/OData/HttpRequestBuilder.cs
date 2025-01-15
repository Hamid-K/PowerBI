using System;
using System.Diagnostics;
using System.Net;
using Microsoft.Data.OData;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.OData.V3;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000724 RID: 1828
	internal static class HttpRequestBuilder
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x000AD710 File Offset: 0x000AB910
		public static MashupHttpWebRequest BuildWebRequest(HttpResource resource, Uri serviceUri, Uri uri, Value headers, ResourceCredentialCollection credentials, ODataSettingsBase settings, string contentType, IEngineHost host, ODataUserSettings userSettings, ODataServerVersion serverVersion)
		{
			headers = HttpRequestBuilder.AppendODataActivityIdHeaderIfTracingEnabled(headers, host, resource.Resource);
			uri = userSettings.ApplyQueryOptions(uri);
			bool flag = false;
			MashupHttpWebRequest mashupHttpWebRequest;
			if (serviceUri != null && userSettings.EnableBatch && HttpRequestBuilder.IsUriTooLarge(uri, userSettings.MaxUriLength))
			{
				Uri uri2 = new Uri(serviceUri, "$batch");
				uri2 = HttpRequestBuilder.ApplyCredentialsToUri(uri2, credentials, userSettings, host);
				mashupHttpWebRequest = (MashupHttpWebRequest)host.CreateWebRequest(resource.Resource, uri2);
				mashupHttpWebRequest.Method = "POST";
				flag = true;
			}
			else
			{
				uri = HttpRequestBuilder.ApplyCredentialsToUri(uri, credentials, userSettings, host);
				mashupHttpWebRequest = (MashupHttpWebRequest)host.CreateWebRequest(resource.Resource, uri);
				RequestHeaders.Create(mashupHttpWebRequest).ApplyHeaders(headers);
				mashupHttpWebRequest.Method = "GET";
				mashupHttpWebRequest.Accept = contentType;
			}
			mashupHttpWebRequest.AllowAutoRedirect = false;
			mashupHttpWebRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
			mashupHttpWebRequest.AdjustForCompression();
			if (settings.ApplyCredentials)
			{
				HttpServices.ApplyCredentials(mashupHttpWebRequest, resource.Kind, uri, credentials, host, userSettings.OAuthResource);
			}
			mashupHttpWebRequest.Timeout = (int)Math.Round(userSettings.Timeout.TotalMilliseconds);
			if (mashupHttpWebRequest.ReadWriteTimeout < mashupHttpWebRequest.Timeout)
			{
				mashupHttpWebRequest.ReadWriteTimeout = mashupHttpWebRequest.Timeout;
			}
			if (flag)
			{
				HttpRequestBuilder.CreateSingleBatchedODataQuery(mashupHttpWebRequest, uri, headers, contentType, serverVersion);
			}
			return mashupHttpWebRequest;
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000AD854 File Offset: 0x000ABA54
		private static Value AppendODataActivityIdHeaderIfTracingEnabled(Value headers, IEngineHost host, IResource resource)
		{
			if (!headers.AsRecord.Keys.Contains("x-ms-client-request-id"))
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/OData/Request", TraceEventType.Information, resource))
				{
					if (hostTrace.VerboseEnabled())
					{
						string text = Guid.NewGuid().ToString();
						hostTrace.Add("ODataActivityID", text, false);
						RecordValue recordValue = RecordValue.New(Keys.New("x-ms-client-request-id"), new Value[] { TextValue.New(text) });
						headers = headers.Concatenate(recordValue);
					}
				}
			}
			return headers;
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x000AD8F4 File Offset: 0x000ABAF4
		private static Uri ApplyCredentialsToUri(Uri uri, ResourceCredentialCollection credentials, ODataUserSettings userSettings, IEngineHost host)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			HttpResourceCredentialDispatcher.ApplyCredentialsToUri(uriBuilder, userSettings.ApiKeyName, credentials, host);
			uri = uriBuilder.Uri;
			return uri;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000AD913 File Offset: 0x000ABB13
		private static bool IsUriTooLarge(Uri uri, int maxUriLength)
		{
			return uri.AbsoluteUri.Length > maxUriLength;
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000AD924 File Offset: 0x000ABB24
		private static void CreateSingleBatchedODataQuery(MashupHttpWebRequest httpWebRequest, Uri uri, Value innerRequestHeaders, string contentType, ODataServerVersion version)
		{
			if (version == ODataServerVersion.V4)
			{
				throw new NotImplementedException();
			}
			using (ODataMessageWriter odataMessageWriter = new ODataMessageWriter(new ODataRequestMessage(httpWebRequest)))
			{
				ODataBatchWriter odataBatchWriter = odataMessageWriter.CreateODataBatchWriter();
				odataBatchWriter.WriteStartBatch();
				ODataBatchOperationRequestMessage odataBatchOperationRequestMessage = odataBatchWriter.CreateOperationRequestMessage("GET", uri);
				Value value = RecordValue.New(Keys.New("Accept"), new Value[] { TextValue.New(contentType) });
				Value value2 = (innerRequestHeaders.IsRecord ? innerRequestHeaders.Concatenate(value) : value);
				RequestHeaders.Create(odataBatchOperationRequestMessage).ApplyHeaders(value2);
				odataBatchWriter.WriteEndBatch();
			}
		}
	}
}
