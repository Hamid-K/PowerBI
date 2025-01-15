using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x020003F4 RID: 1012
	internal static class SharePointFile
	{
		// Token: 0x060022A7 RID: 8871 RVA: 0x000606EC File Offset: 0x0005E8EC
		public static string GetCsomFileUrl(IEngineHost host, string resourceKind, TextValue url, ResourceCredentialCollection credentials)
		{
			SharePointFile.ValidateFileUrl(url);
			UriBuilder uriBuilder = new UriBuilder(url.String);
			string text = uriBuilder.Path;
			int num = text.LastIndexOf('.');
			string text2 = text.Substring(num);
			string text3 = text2.Replace('.', '_');
			uriBuilder.Path = uriBuilder.Path.Replace(text2, text3) + "/_api/contextinfo";
			uriBuilder = new UriBuilder(SharePointFile.TryGetWebFullUrl(resourceKind, uriBuilder.Uri, credentials, host, false).TrimEnd(new char[] { '/' }));
			text = text.Replace("'", "''");
			string text4 = "_api/web/getfilebyserverrelativeurl('" + text + "')/$value";
			if (uriBuilder.Path == "/")
			{
				uriBuilder.Path = text4;
			}
			else
			{
				uriBuilder.Path = uriBuilder.Path.TrimEnd(new char[] { '/' }) + "/" + text4;
			}
			return uriBuilder.Uri.AbsoluteUri;
		}

		// Token: 0x060022A8 RID: 8872 RVA: 0x000607E4 File Offset: 0x0005E9E4
		public static bool TryGetFilePath(TextValue url, out string filePath)
		{
			string asString = url.AsString;
			if (asString.Contains("getfilebyserverrelativeurl"))
			{
				int num = asString.IndexOf("'", StringComparison.OrdinalIgnoreCase);
				int num2 = asString.LastIndexOf("'", StringComparison.OrdinalIgnoreCase);
				filePath = asString.Substring(num + 1, num2 - num - 1);
				return true;
			}
			filePath = null;
			return false;
		}

		// Token: 0x060022A9 RID: 8873 RVA: 0x00060838 File Offset: 0x0005EA38
		public static bool IsFile(IEngineHost host, string path)
		{
			bool isFile = false;
			SafeExceptions.IgnoreSafeExceptions(host, "Engine/SharePointFile/IsFile", delegate
			{
				isFile = FileHelper.GetFileExtension(path) != TextValue.Empty && new UriBuilder(path).Path != "/";
			});
			return isFile;
		}

		// Token: 0x060022AA RID: 8874 RVA: 0x00060878 File Offset: 0x0005EA78
		public static bool IsSharePointOnline(this ResourceCredentialCollection credentials)
		{
			if (credentials.Count == 1)
			{
				OAuthCredential oauthCredential = credentials[0] as OAuthCredential;
				string text;
				return oauthCredential != null && oauthCredential.Properties.TryGetValue("ProviderType", out text) && text.Equals("SharePointAAD", StringComparison.Ordinal);
			}
			return false;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x000608C4 File Offset: 0x0005EAC4
		private static void ValidateFileUrl(TextValue url)
		{
			Uri uri;
			if (Uri.TryCreate(url.AsString, UriKind.Absolute, out uri) && string.IsNullOrEmpty(uri.Query) && string.IsNullOrEmpty(uri.Fragment))
			{
				if (!uri.Segments.Any((string e) => e.Equals("_layouts", StringComparison.Ordinal)) && uri.AbsolutePath.LastIndexOf('.') != -1)
				{
					return;
				}
			}
			if (uri.Query == "?web=1")
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.SharePointInvalidFileUrlWebEqualsOne, url, null);
			}
			throw ValueException.NewDataFormatError<Message0>(Strings.SharePointInvalidFileUrl, url, null);
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x00060964 File Offset: 0x0005EB64
		private static string TryGetWebFullUrl(string resourceKind, Uri uri, ResourceCredentialCollection credentials, IEngineHost host, bool tokenRefreshed = false)
		{
			string webFullUrl;
			try
			{
				Request request = Request.Create(host, resourceKind, null, TextValue.New(uri.ToString()), null, null, null, SharePointFile.BuildRequestHeaders(), null, null, null, null, null, null);
				request.Method = "POST";
				request.ContentLength = 0L;
				using (Response response = request.GetResponse(credentials, null, false))
				{
					if (request.IsFailedStatusCode(response))
					{
						SharePointFile.GenerateErrorResponse(uri, host, request, response);
					}
					using (Stream responseStream = response.GetResponseStream())
					{
						webFullUrl = (new DataContractJsonSerializer(typeof(SharePointFile.SharePointContextInfo)).ReadObject(responseStream) as SharePointFile.SharePointContextInfo).WebFullUrl;
					}
				}
			}
			catch (Exception ex) when (ex is SerializationException || ex is ResponseException)
			{
				throw DataSourceException.NewDataSourceError<Message2>(host, Strings.RequestFailedWithoutStatusCode(resourceKind, uri), Resource.New(resourceKind, uri.AbsoluteUri), null, ex);
			}
			return webFullUrl;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x00060A70 File Offset: 0x0005EC70
		private static void GenerateErrorResponse(Uri uri, IEngineHost host, Request httpRequest, Response response)
		{
			IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			string text;
			if (SharePointUtil.TryExtractSharePointCorrelationID(((HttpResponse)response).HttpWebResponse, out text))
			{
				list.Add(new RecordKeyDefinition("SPRequestGuid", TextValue.New(text), TypeValue.Text));
			}
			throw DataSourceException.NewDataSourceError<Message3>(host, Strings.RequestFailedWithStatusCode("SharePoint", uri, response.StatusCode), Resource.New("SharePoint", uri.AbsoluteUri), list, null);
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x00060AE0 File Offset: 0x0005ECE0
		private static RecordValue BuildRequestHeaders()
		{
			return RecordValue.New(Keys.New("OData-MaxVersion", "Accept"), new Value[]
			{
				TextValue.New("4.0"),
				TextValue.New("application/json")
			});
		}

		// Token: 0x04000DBC RID: 3516
		public const string SpRequestGuid = "SPRequestGuid";

		// Token: 0x020003F5 RID: 1013
		[DataContract]
		private sealed class SharePointContextInfo
		{
			// Token: 0x17000E8A RID: 3722
			// (get) Token: 0x060022B0 RID: 8880 RVA: 0x00060B16 File Offset: 0x0005ED16
			// (set) Token: 0x060022B1 RID: 8881 RVA: 0x00060B1E File Offset: 0x0005ED1E
			[DataMember(Name = "WebFullUrl")]
			public string WebFullUrl { get; set; }
		}
	}
}
