using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Results;
using Microsoft.AspNet.OData.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData;

namespace Microsoft.AspNet.OData.Batch
{
	// Token: 0x020001CD RID: 461
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class ODataBatchHttpRequestMessageExtensions
	{
		// Token: 0x06000F39 RID: 3897 RVA: 0x0003E868 File Offset: 0x0003CA68
		public static Guid? GetODataBatchId(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			object obj;
			if (request.Properties.TryGetValue("BatchId", out obj))
			{
				return new Guid?((Guid)obj);
			}
			return null;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003E8AC File Offset: 0x0003CAAC
		public static void SetODataBatchId(this HttpRequestMessage request, Guid batchId)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties["BatchId"] = batchId;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0003E8D4 File Offset: 0x0003CAD4
		public static Guid? GetODataChangeSetId(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			object obj;
			if (request.Properties.TryGetValue("ChangesetId", out obj))
			{
				return new Guid?((Guid)obj);
			}
			return null;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003E918 File Offset: 0x0003CB18
		public static void SetODataChangeSetId(this HttpRequestMessage request, Guid changeSetId)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties["ChangesetId"] = changeSetId;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0003E940 File Offset: 0x0003CB40
		public static string GetODataContentId(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			object obj;
			if (request.Properties.TryGetValue("ContentId", out obj))
			{
				return (string)obj;
			}
			return null;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0003E977 File Offset: 0x0003CB77
		public static void SetODataContentId(this HttpRequestMessage request, string contentId)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties["ContentId"] = contentId;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003E998 File Offset: 0x0003CB98
		public static IDictionary<string, string> GetODataContentIdMapping(this HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			object obj;
			if (request.Properties.TryGetValue("ContentIdMapping", out obj))
			{
				return obj as IDictionary<string, string>;
			}
			return null;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003E9CF File Offset: 0x0003CBCF
		public static void SetODataContentIdMapping(this HttpRequestMessage request, IDictionary<string, string> contentIdMapping)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			request.Properties["ContentIdMapping"] = contentIdMapping;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003E9F0 File Offset: 0x0003CBF0
		internal static Task<HttpResponseMessage> CreateODataBatchResponseAsync(this HttpRequestMessage request, IEnumerable<ODataBatchResponseItem> responses, ODataMessageQuotas messageQuotas)
		{
			ODataVersion odataResponseVersion = ResultHelpers.GetODataResponseVersion(request);
			IServiceProvider requestContainer = request.GetRequestContainer();
			ODataMessageWriterSettings requiredService = ServiceProviderServiceExtensions.GetRequiredService<ODataMessageWriterSettings>(requestContainer);
			requiredService.Version = new ODataVersion?(odataResponseVersion);
			requiredService.MessageQuotas = messageQuotas;
			MediaTypeHeaderValue mediaTypeHeaderValue = null;
			if (request.Headers.Accept.Any((MediaTypeWithQualityHeaderValue t) => t.MediaType.Equals("multipart/mixed", StringComparison.OrdinalIgnoreCase)))
			{
				mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(string.Format(CultureInfo.InvariantCulture, "multipart/mixed;boundary=batchresponse_{0}", new object[] { Guid.NewGuid() }));
			}
			else if (request.Headers.Accept.Any((MediaTypeWithQualityHeaderValue t) => t.MediaType.Equals("application/json", StringComparison.OrdinalIgnoreCase)))
			{
				mediaTypeHeaderValue = MediaTypeHeaderValue.Parse("application/json");
			}
			HttpResponseMessage httpResponseMessage = HttpRequestMessageExtensions.CreateResponse(request, HttpStatusCode.OK);
			httpResponseMessage.Content = new ODataBatchContent(responses, requestContainer, mediaTypeHeaderValue);
			return Task.FromResult<HttpResponseMessage>(httpResponseMessage);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003EADC File Offset: 0x0003CCDC
		internal static void ValidateODataBatchRequest(this HttpRequestMessage request)
		{
			if (request.Content == null)
			{
				throw new HttpResponseException(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.BadRequest, SRResources.BatchRequestMissingContent));
			}
			MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
			if (contentType == null)
			{
				throw new HttpResponseException(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.BadRequest, SRResources.BatchRequestMissingContentType));
			}
			bool flag = string.Equals(contentType.MediaType, "multipart/mixed", StringComparison.OrdinalIgnoreCase);
			bool flag2 = string.Equals(contentType.MediaType, "application/json", StringComparison.OrdinalIgnoreCase);
			if (!flag && !flag2)
			{
				throw new HttpResponseException(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.BadRequest, Error.Format(SRResources.BatchRequestInvalidMediaType, new object[] { "multipart/mixed", "application/json" })));
			}
			if (flag)
			{
				NameValueHeaderValue nameValueHeaderValue = contentType.Parameters.FirstOrDefault((NameValueHeaderValue p) => string.Equals(p.Name, "boundary", StringComparison.OrdinalIgnoreCase));
				if (nameValueHeaderValue == null || string.IsNullOrEmpty(nameValueHeaderValue.Value))
				{
					throw new HttpResponseException(HttpRequestMessageExtensions.CreateErrorResponse(request, HttpStatusCode.BadRequest, SRResources.BatchRequestMissingBoundary));
				}
			}
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003EBE0 File Offset: 0x0003CDE0
		internal static Uri GetODataBatchBaseUri(this HttpRequestMessage request, string oDataRouteName)
		{
			if (oDataRouteName == null)
			{
				return new Uri(request.RequestUri, new Uri("/", UriKind.Relative));
			}
			UrlHelper urlHelper = HttpRequestMessageExtensions.GetUrlHelper(request) ?? new UrlHelper(request);
			HttpRouteValueDictionary httpRouteValueDictionary = new HttpRouteValueDictionary();
			httpRouteValueDictionary.Add(ODataRouteConstants.ODataPath, string.Empty);
			string text = urlHelper.Link(oDataRouteName, httpRouteValueDictionary);
			if (text == null)
			{
				throw new InvalidOperationException(SRResources.UnableToDetermineBaseUrl);
			}
			return new Uri(text);
		}

		// Token: 0x0400043C RID: 1084
		private const string BatchIdKey = "BatchId";

		// Token: 0x0400043D RID: 1085
		private const string ChangeSetIdKey = "ChangesetId";

		// Token: 0x0400043E RID: 1086
		private const string ContentIdKey = "ContentId";

		// Token: 0x0400043F RID: 1087
		private const string ContentIdMappingKey = "ContentIdMapping";

		// Token: 0x04000440 RID: 1088
		private const string BatchMediaTypeMime = "multipart/mixed";

		// Token: 0x04000441 RID: 1089
		private const string BatchMediaTypeJson = "application/json";

		// Token: 0x04000442 RID: 1090
		private const string Boundary = "boundary";
	}
}
