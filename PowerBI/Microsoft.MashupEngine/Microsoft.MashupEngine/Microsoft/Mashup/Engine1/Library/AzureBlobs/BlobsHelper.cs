using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Xml;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EF3 RID: 3827
	internal static class BlobsHelper
	{
		// Token: 0x0600657F RID: 25983 RVA: 0x0015C754 File Offset: 0x0015A954
		public static Request CreateRequest(IEngineHost engineHost, IResource resource, TextValue url, Value query = null, Value headers = null, Value content = null)
		{
			TextValue textValue = BlobsHelper.DetermineVersionToUse(engineHost);
			return AzureBaseHelper.CreateRequest(engineHost, resource, url, textValue, query, headers, content, false);
		}

		// Token: 0x06006580 RID: 25984 RVA: 0x0015C778 File Offset: 0x0015A978
		public static void CheckResponseForErrors(IEngineHost engineHost, XmlDocument responseDocument, IResource resource)
		{
			if (responseDocument.GetElementsByTagName("Error").Count > 0)
			{
				string innerText = responseDocument.GetElementsByTagName("Error")[0].ChildNodes[0].InnerText;
				throw DataSourceException.NewDataSourceError(engineHost, string.Format(CultureInfo.CurrentCulture, "{0}. {1}", innerText, responseDocument.GetElementsByTagName("Error")[0].ChildNodes[1].InnerText), resource, null, null);
			}
		}

		// Token: 0x06006581 RID: 25985 RVA: 0x0015C7F8 File Offset: 0x0015A9F8
		public static bool TryCreateSecurityException(Request request, WebException e, out ResourceSecurityException resourceSecurityException)
		{
			MashupHttpWebResponse mashupHttpWebResponse = e.Response as MashupHttpWebResponse;
			if (mashupHttpWebResponse != null && e.Status == WebExceptionStatus.ProtocolError && mashupHttpWebResponse.StatusCode == HttpStatusCode.Forbidden)
			{
				resourceSecurityException = DataSourceException.NewAccessAuthorizationError(request.Host, request.RequestResource, null, null, null);
				return true;
			}
			return Request.TryCreateSecurityException(request, e, out resourceSecurityException);
		}

		// Token: 0x06006582 RID: 25986 RVA: 0x0015C84C File Offset: 0x0015AA4C
		public static TextValue FormatEtag(string etag)
		{
			if (string.IsNullOrEmpty(etag))
			{
				return TextValue.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(etag.Length + 2);
			stringBuilder.Append('"');
			stringBuilder.Append(etag);
			stringBuilder.Append('"');
			return TextValue.New(stringBuilder.ToString());
		}

		// Token: 0x06006583 RID: 25987 RVA: 0x0015C898 File Offset: 0x0015AA98
		private static TextValue DetermineVersionToUse(IEngineHost engineHost)
		{
			object obj;
			if (engineHost.TryGetConfigurationProperty("MashupFlight_UseAzureBlobVersion2021", out obj))
			{
				bool? flag = obj as bool?;
				if (flag != null && flag.Value)
				{
					return BlobsHelper.October2021Version;
				}
			}
			return BlobsHelper.November2017Version;
		}

		// Token: 0x0400379D RID: 14237
		public static readonly TextValue November2017Version = TextValue.New("2017-11-09");

		// Token: 0x0400379E RID: 14238
		public static readonly TextValue October2021Version = TextValue.New("2021-10-04");

		// Token: 0x0400379F RID: 14239
		public const string NextMarkerKey = "NextMarker";

		// Token: 0x040037A0 RID: 14240
		public const string ContainerKey = "Container";

		// Token: 0x040037A1 RID: 14241
		public const string CompQueryKey = "comp";

		// Token: 0x040037A2 RID: 14242
		public const string ListQueryKey = "list";

		// Token: 0x040037A3 RID: 14243
		public const string MarkerKey = "marker";

		// Token: 0x040037A4 RID: 14244
		public const string PrefixKey = "prefix";

		// Token: 0x040037A5 RID: 14245
		public const string MaxResultsKey = "maxresults";

		// Token: 0x040037A6 RID: 14246
		public const string BlobKey = "Blob";

		// Token: 0x040037A7 RID: 14247
		public const string BlockIdKey = "blockid";

		// Token: 0x040037A8 RID: 14248
		public const string BlockListTypeKey = "blocklisttype";

		// Token: 0x040037A9 RID: 14249
		public const string RestypeKey = "restype";

		// Token: 0x040037AA RID: 14250
		public const string ContainerTypeKey = "container";

		// Token: 0x040037AB RID: 14251
		public const string PropertiesKey = "Properties";

		// Token: 0x040037AC RID: 14252
		public const string MaxResultsQueryKey = "maxResults";

		// Token: 0x040037AD RID: 14253
		public const string ETagXmlTag = "Etag";

		// Token: 0x040037AE RID: 14254
		public const string ContentEncodingTag = "Content-Encoding";

		// Token: 0x040037AF RID: 14255
		private const string ErrorKey = "Error";

		// Token: 0x040037B0 RID: 14256
		public static readonly Keys BlobRecordKeys = Keys.New(new string[] { "Name", "Url", "Last-Modified", "Content-Length", "Content-Type", "FolderPath", "etag", "Content-Encoding" });

		// Token: 0x040037B1 RID: 14257
		public static readonly Keys ListEntryAttributeKeys = Keys.New("Size");

		// Token: 0x040037B2 RID: 14258
		public static readonly Keys QueryKeys = Keys.New("comp");

		// Token: 0x040037B3 RID: 14259
		public static readonly RecordValue ListDirectoryQuery = RecordValue.New(BlobsHelper.QueryKeys, new Value[] { TextValue.New("list") });

		// Token: 0x040037B4 RID: 14260
		public static readonly RecordValue ListBlobs = RecordValue.New(Keys.New("comp", "restype"), new Value[]
		{
			TextValue.New("list"),
			TextValue.New("container")
		});
	}
}
