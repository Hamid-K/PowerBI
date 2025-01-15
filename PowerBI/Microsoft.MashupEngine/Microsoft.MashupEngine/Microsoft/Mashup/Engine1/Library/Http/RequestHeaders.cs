using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Xml;
using Microsoft.Data.OData;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Http
{
	// Token: 0x02000A77 RID: 2679
	public abstract class RequestHeaders
	{
		// Token: 0x06004B2B RID: 19243 RVA: 0x000F9AE8 File Offset: 0x000F7CE8
		public static void ThrowIfHeaderNotAllowed(RecordValue headers)
		{
			foreach (string text in headers.Keys)
			{
				if (!RequestHeaders.AllowedHeadersList.Contains(text))
				{
					string text2 = string.Join(", ", RequestHeaders.AllowedHeadersList.OrderBy((string s) => s).ToArray<string>());
					throw ValueException.NewExpressionError<Message2>(Strings.WebContentsWithUnapprovedHeaders(text, text2), null, null);
				}
			}
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x000F9B8C File Offset: 0x000F7D8C
		public static bool IsGzip(string contentEncoding)
		{
			return contentEncoding != null && contentEncoding.IndexOf("gzip", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x000F9BA5 File Offset: 0x000F7DA5
		public static bool IsDeflate(string contentEncoding)
		{
			return contentEncoding != null && contentEncoding.IndexOf("deflate", StringComparison.OrdinalIgnoreCase) != -1;
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x000F9BBE File Offset: 0x000F7DBE
		protected RequestHeaders(RequestHeaders.IWebRequest webRequest)
		{
			this.webRequest = webRequest;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x000F9BDD File Offset: 0x000F7DDD
		protected RequestHeaders(WebRequest webRequest)
		{
			this.webRequest = new RequestHeaders.WebRequestWrapper(webRequest);
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x000F9C01 File Offset: 0x000F7E01
		protected RequestHeaders(IODataRequestMessage requestMessage)
		{
			this.webRequest = new RequestHeaders.ODataRequestWrapper(requestMessage);
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x000F9C25 File Offset: 0x000F7E25
		protected void AddWebHeader(RequestHeaders.WebHeader webHeader)
		{
			this.webHeaders.Add(webHeader.Name, webHeader);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x000F9C3C File Offset: 0x000F7E3C
		private void ApplyHeader(string name, Value value)
		{
			RequestHeaders.WebHeader webHeader;
			if (this.webHeaders.TryGetValue(name, out webHeader))
			{
				webHeader.ApplyHeaderValue(value);
				return;
			}
			try
			{
				this.webRequest.AddHeaderValue(name, value.AsString);
			}
			catch (ArgumentException ex)
			{
				throw FileErrors.HandleException(ex, value);
			}
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x000F9C90 File Offset: 0x000F7E90
		internal virtual void ApplyHeaders(Value headers)
		{
			if (headers != null && !headers.IsNull)
			{
				foreach (string text in headers.AsRecord.Keys)
				{
					Value value = headers[text];
					this.ApplyHeader(text, value);
				}
			}
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x000F9CFC File Offset: 0x000F7EFC
		internal static RequestHeaders Create(IODataRequestMessage odataRequestMessage)
		{
			return new RequestHeaders.HttpHeaders(new RequestHeaders.ODataRequestWrapper(odataRequestMessage));
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x000F9D0C File Offset: 0x000F7F0C
		internal static RequestHeaders Create(WebRequest webRequest)
		{
			FileWebRequest fileWebRequest = webRequest as FileWebRequest;
			if (fileWebRequest != null)
			{
				return new RequestHeaders.FileHeaders(fileWebRequest);
			}
			FtpWebRequest ftpWebRequest = webRequest as FtpWebRequest;
			if (ftpWebRequest != null)
			{
				return new RequestHeaders.FtpHeaders(ftpWebRequest);
			}
			MashupHttpWebRequest mashupHttpWebRequest = webRequest as MashupHttpWebRequest;
			if (mashupHttpWebRequest != null)
			{
				return new RequestHeaders.HttpHeaders(new RequestHeaders.HttpRequestWrapper(mashupHttpWebRequest));
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x000F9D60 File Offset: 0x000F7F60
		internal static RecordValue GetHeaders(WebHeaderCollection webHeaders)
		{
			return RequestHeaders.GetHeaders(webHeaders, null);
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x000F9D6C File Offset: 0x000F7F6C
		internal static RecordValue GetHeaders(WebHeaderCollection webHeaders, string encoding)
		{
			NameObjectCollectionBase.KeysCollection keys = webHeaders.Keys;
			int count = keys.Count;
			KeysBuilder keysBuilder = new KeysBuilder(count);
			List<Value> list = new List<Value>(count);
			for (int i = 0; i < count; i++)
			{
				keysBuilder.Add(keys[i]);
				list.Add(TextValue.New(webHeaders[i]));
			}
			if (!string.IsNullOrEmpty(encoding) && keysBuilder.IndexOf("Content-Encoding") == -1)
			{
				keysBuilder.Add("Content-Encoding");
				list.Add(TextValue.New(encoding));
			}
			return RecordValue.New(keysBuilder.ToKeys(), list.ToArray());
		}

		// Token: 0x040027CF RID: 10191
		public const string HttpAccept = "Accept";

		// Token: 0x040027D0 RID: 10192
		public const string HttpAcceptCharset = "Accept-Charset";

		// Token: 0x040027D1 RID: 10193
		public const string HttpAcceptEncoding = "Accept-Encoding";

		// Token: 0x040027D2 RID: 10194
		public const string HttpAcceptLanguage = "Accept-Language";

		// Token: 0x040027D3 RID: 10195
		public const string HttpAcceptRanges = "Accept-Ranges";

		// Token: 0x040027D4 RID: 10196
		public const string HttpAge = "Age";

		// Token: 0x040027D5 RID: 10197
		public const string HttpAllow = "Allow";

		// Token: 0x040027D6 RID: 10198
		public const string HttpAllowAutoRedirect = "Allow-Auto-Redirect";

		// Token: 0x040027D7 RID: 10199
		public const string HttpAuthorization = "Authorization";

		// Token: 0x040027D8 RID: 10200
		public const string HttpAutomaticDecompression = "Automatic-Decompression";

		// Token: 0x040027D9 RID: 10201
		public const string HttpCacheControl = "Cache-Control";

		// Token: 0x040027DA RID: 10202
		public const string HttpClose = "Close";

		// Token: 0x040027DB RID: 10203
		public const string HttpConnection = "Connection";

		// Token: 0x040027DC RID: 10204
		public const string HttpContentEncoding = "Content-Encoding";

		// Token: 0x040027DD RID: 10205
		public const string HttpContentLanguage = "Content-Language";

		// Token: 0x040027DE RID: 10206
		public const string HttpContentLength = "Content-Length";

		// Token: 0x040027DF RID: 10207
		public const string HttpContentLocation = "Content-Location";

		// Token: 0x040027E0 RID: 10208
		public const string HttpContentRange = "Content-Range";

		// Token: 0x040027E1 RID: 10209
		public const string HttpContentType = "Content-Type";

		// Token: 0x040027E2 RID: 10210
		public const string HttpDate = "Date";

		// Token: 0x040027E3 RID: 10211
		public const string HttpETag = "ETag";

		// Token: 0x040027E4 RID: 10212
		public const string HttpExpect = "Expect";

		// Token: 0x040027E5 RID: 10213
		public const string HttpExpires = "Expires";

		// Token: 0x040027E6 RID: 10214
		public const string HttpIfMatch = "If-Match";

		// Token: 0x040027E7 RID: 10215
		public const string HttpIfModifiedSince = "If-Modified-Since";

		// Token: 0x040027E8 RID: 10216
		public const string HttpIfNoneMatch = "If-None-Match";

		// Token: 0x040027E9 RID: 10217
		public const string HttpKeepAlive = "Keep-Alive";

		// Token: 0x040027EA RID: 10218
		public const string HttpLastModified = "LastModified";

		// Token: 0x040027EB RID: 10219
		public const string HttpLocation = "Location";

		// Token: 0x040027EC RID: 10220
		public const string HttpPipelined = "Pipelined";

		// Token: 0x040027ED RID: 10221
		public const string HttpPrefer = "Prefer";

		// Token: 0x040027EE RID: 10222
		public const string HttpProxyAuthenticate = "Proxy-Authenticate";

		// Token: 0x040027EF RID: 10223
		public const string HttpRange = "Range";

		// Token: 0x040027F0 RID: 10224
		public const string HttpReferer = "Referer";

		// Token: 0x040027F1 RID: 10225
		public const string HttpRetryAfter = "Retry-After";

		// Token: 0x040027F2 RID: 10226
		public const string HttpServer = "Server";

		// Token: 0x040027F3 RID: 10227
		public const string HttpMicrosoftSharePointTeamServices = "MicrosoftSharePointTeamServices";

		// Token: 0x040027F4 RID: 10228
		public const string HttpTransferEncoding = "Transfer-Encoding";

		// Token: 0x040027F5 RID: 10229
		public const string HttpUserAgent = "User-Agent";

		// Token: 0x040027F6 RID: 10230
		public const string HttpVary = "Vary";

		// Token: 0x040027F7 RID: 10231
		public const string HttpWarning = "Warning";

		// Token: 0x040027F8 RID: 10232
		public const string HttpWwwAuthenticate = "WWW-Authenticate";

		// Token: 0x040027F9 RID: 10233
		public const string HttpXMethod = "X-HTTP-Method";

		// Token: 0x040027FA RID: 10234
		public const string HttpXmsBlobContentType = "x-ms-blob-content-type";

		// Token: 0x040027FB RID: 10235
		public const string HttpXmsBlobType = "x-ms-blob-type";

		// Token: 0x040027FC RID: 10236
		public const string HttpXmsCallerCapacityId = "x-ms-caller-capacity-id";

		// Token: 0x040027FD RID: 10237
		public const string HttpXmsClientRequestId = "x-ms-client-request-id";

		// Token: 0x040027FE RID: 10238
		public const string HttpXmsContentTypeKey = "x-ms-content-type";

		// Token: 0x040027FF RID: 10239
		public const string HttpXmsContinuation = "x-ms-continuation";

		// Token: 0x04002800 RID: 10240
		public const string HttpXmsDate = "x-ms-date";

		// Token: 0x04002801 RID: 10241
		public const string HttpXmsErrorCode = "x-ms-error-code";

		// Token: 0x04002802 RID: 10242
		public const string HttpXmsParentActivityId = "x-ms-parent-activity-id";

		// Token: 0x04002803 RID: 10243
		public const string HttpXmsRange = "x-ms-range";

		// Token: 0x04002804 RID: 10244
		public const string HttpXmsRenameSource = "x-ms-rename-source";

		// Token: 0x04002805 RID: 10245
		public const string HttpXmsRequestId = "x-ms-request-id";

		// Token: 0x04002806 RID: 10246
		public const string HttpXmsRootActivityId = "x-ms-root-activity-id";

		// Token: 0x04002807 RID: 10247
		public const string HttpXmsS2sActorAuthorization = "x-ms-s2s-actor-authorization";

		// Token: 0x04002808 RID: 10248
		public const string HttpXmsSourceIfMatch = "x-ms-source-if-match";

		// Token: 0x04002809 RID: 10249
		public const string HttpXmsSrcCapacityId = "x-ms-src-capacity-id";

		// Token: 0x0400280A RID: 10250
		public const string HttpXmsVersion = "x-ms-version";

		// Token: 0x0400280B RID: 10251
		public static readonly HashSet<string> AllowedHeadersList = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Accept", "Accept-Charset", "Accept-Encoding", "Accept-Language", "Cache-Control", "Content-Type", "If-Modified-Since", "Prefer", "Range", "Referer" };

		// Token: 0x0400280C RID: 10252
		public static readonly IList<string> CompressionTypes = new string[] { "gzip", "deflate" };

		// Token: 0x0400280D RID: 10253
		private const string DefaultAcceptValue = "*/*";

		// Token: 0x0400280E RID: 10254
		public const string DefaultUserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";

		// Token: 0x0400280F RID: 10255
		public static readonly RecordValue DefaultUserAgentHeader = RecordValue.New(Keys.New("User-Agent"), new Value[] { TextValue.New("Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)") });

		// Token: 0x04002810 RID: 10256
		private readonly Dictionary<string, RequestHeaders.WebHeader> webHeaders = new Dictionary<string, RequestHeaders.WebHeader>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04002811 RID: 10257
		private readonly RequestHeaders.IWebRequest webRequest;

		// Token: 0x02000A78 RID: 2680
		private sealed class FileHeaders : RequestHeaders
		{
			// Token: 0x06004B39 RID: 19257 RVA: 0x000F9EE2 File Offset: 0x000F80E2
			public FileHeaders(FileWebRequest fileRequest)
				: base(fileRequest)
			{
				base.AddWebHeader(new RequestHeaders.FileHeaders.ContentTypeHeader(fileRequest));
			}

			// Token: 0x02000A79 RID: 2681
			private sealed class ContentTypeHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B3A RID: 19258 RVA: 0x000F9EF7 File Offset: 0x000F80F7
				public ContentTypeHeader(FileWebRequest fileRequest)
					: base("Content-Type")
				{
					this.fileRequest = fileRequest;
				}

				// Token: 0x06004B3B RID: 19259 RVA: 0x000F9F0B File Offset: 0x000F810B
				public override void ApplyHeaderValue(Value value)
				{
					this.fileRequest.ContentType = value.AsString;
				}

				// Token: 0x04002812 RID: 10258
				private readonly FileWebRequest fileRequest;
			}
		}

		// Token: 0x02000A7A RID: 2682
		private sealed class FtpHeaders : RequestHeaders
		{
			// Token: 0x06004B3C RID: 19260 RVA: 0x000F9F1E File Offset: 0x000F811E
			public FtpHeaders(FtpWebRequest ftpRequest)
				: base(ftpRequest)
			{
			}
		}

		// Token: 0x02000A7B RID: 2683
		private sealed class HttpHeaders : RequestHeaders
		{
			// Token: 0x06004B3D RID: 19261 RVA: 0x000F9F28 File Offset: 0x000F8128
			public HttpHeaders(RequestHeaders.IHttpWebRequest httpWebRequest)
				: base(httpWebRequest)
			{
				this.httpWebRequest = httpWebRequest;
				base.AddWebHeader(new RequestHeaders.HttpHeaders.AcceptHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.AcceptEncodingHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.AllowAutoRedirectHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.AutomaticDecompressionHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.ConnectionHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.ContentTypeHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.ExpectHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.IfModifiedSinceHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.PipelinedHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.RangeHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.RefererHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.TransferEncodingHeader(httpWebRequest));
				base.AddWebHeader(new RequestHeaders.HttpHeaders.UserAgentHeader(httpWebRequest));
			}

			// Token: 0x06004B3E RID: 19262 RVA: 0x000F9FE0 File Offset: 0x000F81E0
			internal override void ApplyHeaders(Value headers)
			{
				base.ApplyHeaders(headers);
				if (this.httpWebRequest.Accept == null)
				{
					this.httpWebRequest.Accept = "*/*";
				}
				if (this.httpWebRequest.UserAgent == null)
				{
					this.httpWebRequest.UserAgent = "Microsoft.Data.Mashup (https://go.microsoft.com/fwlink/?LinkID=304225)";
				}
			}

			// Token: 0x04002813 RID: 10259
			private readonly RequestHeaders.IHttpWebRequest httpWebRequest;

			// Token: 0x02000A7C RID: 2684
			private sealed class AcceptHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B3F RID: 19263 RVA: 0x000FA02E File Offset: 0x000F822E
				public AcceptHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Accept")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B40 RID: 19264 RVA: 0x000FA042 File Offset: 0x000F8242
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.Accept = value.AsString;
				}

				// Token: 0x04002814 RID: 10260
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A7D RID: 2685
			private sealed class AcceptEncodingHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B41 RID: 19265 RVA: 0x000FA055 File Offset: 0x000F8255
				public AcceptEncodingHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Accept-Encoding")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B42 RID: 19266 RVA: 0x000FA06C File Offset: 0x000F826C
				public override void ApplyHeaderValue(Value value)
				{
					string[] array = value.AsString.Split(new char[] { ',' });
					for (int i = 0; i < array.Length; i++)
					{
						string coding = array[i];
						if (!RequestHeaders.HttpHeaders.AcceptEncodingHeader.knownMethods.Any((string method) => coding.IndexOf(method, StringComparison.OrdinalIgnoreCase) != -1))
						{
							throw ValueException.NewDataSourceError<Message1>(Strings.UnsupportedHeader(string.Join(", ", RequestHeaders.HttpHeaders.AcceptEncodingHeader.knownMethods)), RecordValue.New(RequestHeaders.HttpHeaders.AcceptEncodingHeader.unsupportedErrorKeys, new Value[]
							{
								TextValue.New("Accept-Encoding"),
								TextValue.New(coding)
							}), null);
						}
					}
					this.headersWrapper.AddHeaderValue("Accept-Encoding", value.AsString);
				}

				// Token: 0x04002815 RID: 10261
				private static readonly string[] knownMethods = new string[] { "gzip", "deflate", "identity" };

				// Token: 0x04002816 RID: 10262
				private static readonly Keys unsupportedErrorKeys = Keys.New("Header", "Value");

				// Token: 0x04002817 RID: 10263
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A7F RID: 2687
			private sealed class AllowAutoRedirectHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B46 RID: 19270 RVA: 0x000FA16F File Offset: 0x000F836F
				public AllowAutoRedirectHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Allow-Auto-Redirect")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B47 RID: 19271 RVA: 0x000FA183 File Offset: 0x000F8383
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.AllowAutoRedirect = value.AsBoolean;
				}

				// Token: 0x04002819 RID: 10265
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A80 RID: 2688
			private sealed class AutomaticDecompressionHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B48 RID: 19272 RVA: 0x000FA196 File Offset: 0x000F8396
				public AutomaticDecompressionHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Automatic-Decompression")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B49 RID: 19273 RVA: 0x000FA1AA File Offset: 0x000F83AA
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.AutomaticDecompression = (DecompressionMethods)value.AsInteger32;
				}

				// Token: 0x0400281A RID: 10266
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A81 RID: 2689
			private sealed class ConnectionHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B4A RID: 19274 RVA: 0x000FA1BD File Offset: 0x000F83BD
				public ConnectionHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Connection")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B4B RID: 19275 RVA: 0x000FA1D4 File Offset: 0x000F83D4
				public override void ApplyHeaderValue(Value value)
				{
					string asString = value.AsString;
					if (asString.Equals("Keep-Alive", StringComparison.OrdinalIgnoreCase))
					{
						this.headersWrapper.KeepAlive = true;
						return;
					}
					if (asString.Equals("Close", StringComparison.OrdinalIgnoreCase))
					{
						this.headersWrapper.KeepAlive = false;
						return;
					}
					this.headersWrapper.Connection = asString;
				}

				// Token: 0x0400281B RID: 10267
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A82 RID: 2690
			private sealed class ContentTypeHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B4C RID: 19276 RVA: 0x000FA22A File Offset: 0x000F842A
				public ContentTypeHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Content-Type")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B4D RID: 19277 RVA: 0x000FA23E File Offset: 0x000F843E
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.ContentType = value.AsString;
				}

				// Token: 0x0400281C RID: 10268
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A83 RID: 2691
			private sealed class ExpectHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B4E RID: 19278 RVA: 0x000FA251 File Offset: 0x000F8451
				public ExpectHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Expect")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B4F RID: 19279 RVA: 0x000FA265 File Offset: 0x000F8465
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.Expect = value.AsString;
				}

				// Token: 0x0400281D RID: 10269
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A84 RID: 2692
			private sealed class IfModifiedSinceHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B50 RID: 19280 RVA: 0x000FA278 File Offset: 0x000F8478
				public IfModifiedSinceHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("If-Modified-Since")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B51 RID: 19281 RVA: 0x000FA28C File Offset: 0x000F848C
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.IfModifiedSince = value.AsDateTime.AsClrDateTime;
				}

				// Token: 0x0400281E RID: 10270
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A85 RID: 2693
			private sealed class PipelinedHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B52 RID: 19282 RVA: 0x000FA2A4 File Offset: 0x000F84A4
				public PipelinedHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Pipelined")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B53 RID: 19283 RVA: 0x000FA2B8 File Offset: 0x000F84B8
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.Pipelined = value.AsBoolean;
				}

				// Token: 0x0400281F RID: 10271
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A86 RID: 2694
			private sealed class RangeHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B54 RID: 19284 RVA: 0x000FA2CB File Offset: 0x000F84CB
				public RangeHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Range")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B55 RID: 19285 RVA: 0x000FA2DF File Offset: 0x000F84DF
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.SetRange(value.AsString);
				}

				// Token: 0x04002820 RID: 10272
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A87 RID: 2695
			private sealed class RefererHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B56 RID: 19286 RVA: 0x000FA2F2 File Offset: 0x000F84F2
				public RefererHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Referer")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B57 RID: 19287 RVA: 0x000FA306 File Offset: 0x000F8506
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.Referer = value.AsString;
				}

				// Token: 0x04002821 RID: 10273
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A88 RID: 2696
			private sealed class TransferEncodingHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B58 RID: 19288 RVA: 0x000FA319 File Offset: 0x000F8519
				public TransferEncodingHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("Transfer-Encoding")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B59 RID: 19289 RVA: 0x000FA330 File Offset: 0x000F8530
				public override void ApplyHeaderValue(Value value)
				{
					string asString = value.AsString;
					this.headersWrapper.SendChunked = true;
					if (!asString.Equals("Chunked", StringComparison.OrdinalIgnoreCase))
					{
						this.headersWrapper.TransferEncoding = asString;
					}
				}

				// Token: 0x04002822 RID: 10274
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}

			// Token: 0x02000A89 RID: 2697
			private sealed class UserAgentHeader : RequestHeaders.WebHeader
			{
				// Token: 0x06004B5A RID: 19290 RVA: 0x000FA36A File Offset: 0x000F856A
				public UserAgentHeader(RequestHeaders.IHttpWebRequest headersWrapper)
					: base("User-Agent")
				{
					this.headersWrapper = headersWrapper;
				}

				// Token: 0x06004B5B RID: 19291 RVA: 0x000FA37E File Offset: 0x000F857E
				public override void ApplyHeaderValue(Value value)
				{
					this.headersWrapper.UserAgent = value.AsString;
				}

				// Token: 0x04002823 RID: 10275
				private readonly RequestHeaders.IHttpWebRequest headersWrapper;
			}
		}

		// Token: 0x02000A8A RID: 2698
		protected abstract class WebHeader
		{
			// Token: 0x06004B5C RID: 19292 RVA: 0x000FA391 File Offset: 0x000F8591
			protected WebHeader(string name)
			{
				this.name = name;
			}

			// Token: 0x170017B5 RID: 6069
			// (get) Token: 0x06004B5D RID: 19293 RVA: 0x000FA3A0 File Offset: 0x000F85A0
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x06004B5E RID: 19294
			public abstract void ApplyHeaderValue(Value value);

			// Token: 0x04002824 RID: 10276
			private readonly string name;
		}

		// Token: 0x02000A8B RID: 2699
		protected interface IWebRequest
		{
			// Token: 0x06004B5F RID: 19295
			void AddHeaderValue(string name, string value);
		}

		// Token: 0x02000A8C RID: 2700
		protected class WebRequestWrapper : RequestHeaders.IWebRequest
		{
			// Token: 0x06004B60 RID: 19296 RVA: 0x000FA3A8 File Offset: 0x000F85A8
			public WebRequestWrapper(WebRequest webRequest)
			{
				this.webRequest = webRequest;
			}

			// Token: 0x06004B61 RID: 19297 RVA: 0x000FA3B7 File Offset: 0x000F85B7
			public void AddHeaderValue(string name, string value)
			{
				this.webRequest.Headers.Add(name, value);
			}

			// Token: 0x04002825 RID: 10277
			private readonly WebRequest webRequest;
		}

		// Token: 0x02000A8D RID: 2701
		private interface IHttpWebRequest : RequestHeaders.IWebRequest
		{
			// Token: 0x170017B6 RID: 6070
			// (get) Token: 0x06004B62 RID: 19298
			// (set) Token: 0x06004B63 RID: 19299
			string Accept { get; set; }

			// Token: 0x170017B7 RID: 6071
			// (get) Token: 0x06004B64 RID: 19300
			// (set) Token: 0x06004B65 RID: 19301
			string ContentType { get; set; }

			// Token: 0x170017B8 RID: 6072
			// (get) Token: 0x06004B66 RID: 19302
			// (set) Token: 0x06004B67 RID: 19303
			string UserAgent { get; set; }

			// Token: 0x170017B9 RID: 6073
			// (get) Token: 0x06004B68 RID: 19304
			// (set) Token: 0x06004B69 RID: 19305
			bool AllowAutoRedirect { get; set; }

			// Token: 0x170017BA RID: 6074
			// (get) Token: 0x06004B6A RID: 19306
			// (set) Token: 0x06004B6B RID: 19307
			DecompressionMethods AutomaticDecompression { get; set; }

			// Token: 0x170017BB RID: 6075
			// (get) Token: 0x06004B6C RID: 19308
			// (set) Token: 0x06004B6D RID: 19309
			bool KeepAlive { get; set; }

			// Token: 0x170017BC RID: 6076
			// (get) Token: 0x06004B6E RID: 19310
			// (set) Token: 0x06004B6F RID: 19311
			string Connection { get; set; }

			// Token: 0x170017BD RID: 6077
			// (get) Token: 0x06004B70 RID: 19312
			// (set) Token: 0x06004B71 RID: 19313
			string Expect { get; set; }

			// Token: 0x170017BE RID: 6078
			// (get) Token: 0x06004B72 RID: 19314
			// (set) Token: 0x06004B73 RID: 19315
			bool Pipelined { get; set; }

			// Token: 0x170017BF RID: 6079
			// (get) Token: 0x06004B74 RID: 19316
			// (set) Token: 0x06004B75 RID: 19317
			string Referer { get; set; }

			// Token: 0x170017C0 RID: 6080
			// (get) Token: 0x06004B76 RID: 19318
			// (set) Token: 0x06004B77 RID: 19319
			bool SendChunked { get; set; }

			// Token: 0x170017C1 RID: 6081
			// (get) Token: 0x06004B78 RID: 19320
			// (set) Token: 0x06004B79 RID: 19321
			string TransferEncoding { get; set; }

			// Token: 0x170017C2 RID: 6082
			// (get) Token: 0x06004B7A RID: 19322
			// (set) Token: 0x06004B7B RID: 19323
			DateTime IfModifiedSince { get; set; }

			// Token: 0x06004B7C RID: 19324
			void SetRange(string range);
		}

		// Token: 0x02000A8E RID: 2702
		private class ODataRequestWrapper : RequestHeaders.IHttpWebRequest, RequestHeaders.IWebRequest
		{
			// Token: 0x06004B7D RID: 19325 RVA: 0x000FA3CB File Offset: 0x000F85CB
			public ODataRequestWrapper(IODataRequestMessage requestMessage)
			{
				this.odataRequestMessage = requestMessage;
			}

			// Token: 0x06004B7E RID: 19326 RVA: 0x000FA3DA File Offset: 0x000F85DA
			public void AddHeaderValue(string name, string value)
			{
				this.odataRequestMessage.SetHeader(name, value);
			}

			// Token: 0x170017C3 RID: 6083
			// (get) Token: 0x06004B7F RID: 19327 RVA: 0x000FA3E9 File Offset: 0x000F85E9
			// (set) Token: 0x06004B80 RID: 19328 RVA: 0x000FA3F6 File Offset: 0x000F85F6
			public string Accept
			{
				get
				{
					return this.GetHeader("Accept");
				}
				set
				{
					this.SetHeader("Accept", value);
				}
			}

			// Token: 0x170017C4 RID: 6084
			// (get) Token: 0x06004B81 RID: 19329 RVA: 0x000FA404 File Offset: 0x000F8604
			// (set) Token: 0x06004B82 RID: 19330 RVA: 0x000FA411 File Offset: 0x000F8611
			public string ContentType
			{
				get
				{
					return this.GetHeader("Content-Type");
				}
				set
				{
					this.SetHeader("Content-Type", value);
				}
			}

			// Token: 0x170017C5 RID: 6085
			// (get) Token: 0x06004B83 RID: 19331 RVA: 0x000FA41F File Offset: 0x000F861F
			// (set) Token: 0x06004B84 RID: 19332 RVA: 0x000FA42C File Offset: 0x000F862C
			public string UserAgent
			{
				get
				{
					return this.GetHeader("User-Agent");
				}
				set
				{
					this.SetHeader("User-Agent", value);
				}
			}

			// Token: 0x170017C6 RID: 6086
			// (get) Token: 0x06004B85 RID: 19333 RVA: 0x000FA43A File Offset: 0x000F863A
			// (set) Token: 0x06004B86 RID: 19334 RVA: 0x000FA447 File Offset: 0x000F8647
			public bool AllowAutoRedirect
			{
				get
				{
					return this.GetBooleanHeader("Allow-Auto-Redirect");
				}
				set
				{
					this.SetBooleanHeader("Allow-Auto-Redirect", value);
				}
			}

			// Token: 0x170017C7 RID: 6087
			// (get) Token: 0x06004B87 RID: 19335 RVA: 0x00002105 File Offset: 0x00000305
			// (set) Token: 0x06004B88 RID: 19336 RVA: 0x000033E7 File Offset: 0x000015E7
			public DecompressionMethods AutomaticDecompression
			{
				get
				{
					return DecompressionMethods.None;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170017C8 RID: 6088
			// (get) Token: 0x06004B89 RID: 19337 RVA: 0x000FA455 File Offset: 0x000F8655
			// (set) Token: 0x06004B8A RID: 19338 RVA: 0x000FA462 File Offset: 0x000F8662
			public bool KeepAlive
			{
				get
				{
					return this.GetBooleanHeader("Keep-Alive");
				}
				set
				{
					this.SetBooleanHeader("Keep-Alive", value);
				}
			}

			// Token: 0x170017C9 RID: 6089
			// (get) Token: 0x06004B8B RID: 19339 RVA: 0x000FA470 File Offset: 0x000F8670
			// (set) Token: 0x06004B8C RID: 19340 RVA: 0x000FA47D File Offset: 0x000F867D
			public string Connection
			{
				get
				{
					return this.GetHeader("Connection");
				}
				set
				{
					this.SetHeader("Connection", value);
				}
			}

			// Token: 0x170017CA RID: 6090
			// (get) Token: 0x06004B8D RID: 19341 RVA: 0x000FA48B File Offset: 0x000F868B
			// (set) Token: 0x06004B8E RID: 19342 RVA: 0x000FA498 File Offset: 0x000F8698
			public string Expect
			{
				get
				{
					return this.GetHeader("Expect");
				}
				set
				{
					this.SetHeader("Expect", value);
				}
			}

			// Token: 0x170017CB RID: 6091
			// (get) Token: 0x06004B8F RID: 19343 RVA: 0x000FA4A6 File Offset: 0x000F86A6
			// (set) Token: 0x06004B90 RID: 19344 RVA: 0x000FA4B3 File Offset: 0x000F86B3
			public bool Pipelined
			{
				get
				{
					return this.GetBooleanHeader("Pipelined");
				}
				set
				{
					this.SetBooleanHeader("Pipelined", value);
				}
			}

			// Token: 0x170017CC RID: 6092
			// (get) Token: 0x06004B91 RID: 19345 RVA: 0x000FA4C1 File Offset: 0x000F86C1
			// (set) Token: 0x06004B92 RID: 19346 RVA: 0x000FA4CE File Offset: 0x000F86CE
			public string Referer
			{
				get
				{
					return this.GetHeader("Referer");
				}
				set
				{
					this.SetHeader("Referer", value);
				}
			}

			// Token: 0x170017CD RID: 6093
			// (get) Token: 0x06004B93 RID: 19347 RVA: 0x00002105 File Offset: 0x00000305
			// (set) Token: 0x06004B94 RID: 19348 RVA: 0x000033E7 File Offset: 0x000015E7
			public bool SendChunked
			{
				get
				{
					return false;
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170017CE RID: 6094
			// (get) Token: 0x06004B95 RID: 19349 RVA: 0x000FA4DC File Offset: 0x000F86DC
			// (set) Token: 0x06004B96 RID: 19350 RVA: 0x000FA4E9 File Offset: 0x000F86E9
			public string TransferEncoding
			{
				get
				{
					return this.GetHeader("Transfer-Encoding");
				}
				set
				{
					this.SetHeader("Transfer-Encoding", value);
				}
			}

			// Token: 0x170017CF RID: 6095
			// (get) Token: 0x06004B97 RID: 19351 RVA: 0x000FA4F7 File Offset: 0x000F86F7
			// (set) Token: 0x06004B98 RID: 19352 RVA: 0x000FA50A File Offset: 0x000F870A
			public DateTime IfModifiedSince
			{
				get
				{
					return XmlConvert.ToDateTime(this.GetHeader("If-Modified-Since"), XmlDateTimeSerializationMode.RoundtripKind);
				}
				set
				{
					this.SetHeader("If-Modified-Since", XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind));
				}
			}

			// Token: 0x06004B99 RID: 19353 RVA: 0x000033E7 File Offset: 0x000015E7
			public void SetRange(string range)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06004B9A RID: 19354 RVA: 0x000FA3DA File Offset: 0x000F85DA
			private void SetHeader(string name, string value)
			{
				this.odataRequestMessage.SetHeader(name, value);
			}

			// Token: 0x06004B9B RID: 19355 RVA: 0x000FA51E File Offset: 0x000F871E
			private string GetHeader(string name)
			{
				return this.odataRequestMessage.GetHeader(name);
			}

			// Token: 0x06004B9C RID: 19356 RVA: 0x000FA52C File Offset: 0x000F872C
			private void SetBooleanHeader(string name, bool value)
			{
				this.odataRequestMessage.SetHeader(name, Convert.ToString(value, CultureInfo.InvariantCulture));
			}

			// Token: 0x06004B9D RID: 19357 RVA: 0x000FA545 File Offset: 0x000F8745
			private bool GetBooleanHeader(string name)
			{
				return Convert.ToBoolean(this.odataRequestMessage.GetHeader(name), CultureInfo.InvariantCulture);
			}

			// Token: 0x04002826 RID: 10278
			private readonly IODataRequestMessage odataRequestMessage;
		}

		// Token: 0x02000A8F RID: 2703
		private class HttpRequestWrapper : RequestHeaders.WebRequestWrapper, RequestHeaders.IHttpWebRequest, RequestHeaders.IWebRequest
		{
			// Token: 0x06004B9E RID: 19358 RVA: 0x000FA55D File Offset: 0x000F875D
			public HttpRequestWrapper(MashupHttpWebRequest httpWebRequest)
				: base(httpWebRequest)
			{
				this.httpWebRequest = httpWebRequest;
			}

			// Token: 0x170017D0 RID: 6096
			// (get) Token: 0x06004B9F RID: 19359 RVA: 0x000FA56D File Offset: 0x000F876D
			// (set) Token: 0x06004BA0 RID: 19360 RVA: 0x000FA57A File Offset: 0x000F877A
			public string Accept
			{
				get
				{
					return this.httpWebRequest.Accept;
				}
				set
				{
					this.httpWebRequest.Accept = value;
				}
			}

			// Token: 0x170017D1 RID: 6097
			// (get) Token: 0x06004BA1 RID: 19361 RVA: 0x000FA588 File Offset: 0x000F8788
			// (set) Token: 0x06004BA2 RID: 19362 RVA: 0x000FA595 File Offset: 0x000F8795
			public string ContentType
			{
				get
				{
					return this.httpWebRequest.ContentType;
				}
				set
				{
					this.httpWebRequest.ContentType = value;
				}
			}

			// Token: 0x170017D2 RID: 6098
			// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x000FA5A3 File Offset: 0x000F87A3
			// (set) Token: 0x06004BA4 RID: 19364 RVA: 0x000FA5B0 File Offset: 0x000F87B0
			public string UserAgent
			{
				get
				{
					return this.httpWebRequest.UserAgent;
				}
				set
				{
					this.httpWebRequest.UserAgent = value;
				}
			}

			// Token: 0x170017D3 RID: 6099
			// (get) Token: 0x06004BA5 RID: 19365 RVA: 0x000FA5BE File Offset: 0x000F87BE
			// (set) Token: 0x06004BA6 RID: 19366 RVA: 0x000FA5CB File Offset: 0x000F87CB
			public bool AllowAutoRedirect
			{
				get
				{
					return this.httpWebRequest.AllowAutoRedirect;
				}
				set
				{
					this.httpWebRequest.AllowAutoRedirect = value;
				}
			}

			// Token: 0x170017D4 RID: 6100
			// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x000FA5D9 File Offset: 0x000F87D9
			// (set) Token: 0x06004BA8 RID: 19368 RVA: 0x000FA5E6 File Offset: 0x000F87E6
			public DecompressionMethods AutomaticDecompression
			{
				get
				{
					return this.httpWebRequest.AutomaticDecompression;
				}
				set
				{
					this.httpWebRequest.AutomaticDecompression = value;
				}
			}

			// Token: 0x170017D5 RID: 6101
			// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x000FA5F4 File Offset: 0x000F87F4
			// (set) Token: 0x06004BAA RID: 19370 RVA: 0x000FA601 File Offset: 0x000F8801
			public bool KeepAlive
			{
				get
				{
					return this.httpWebRequest.KeepAlive;
				}
				set
				{
					this.httpWebRequest.KeepAlive = value;
				}
			}

			// Token: 0x170017D6 RID: 6102
			// (get) Token: 0x06004BAB RID: 19371 RVA: 0x000FA60F File Offset: 0x000F880F
			// (set) Token: 0x06004BAC RID: 19372 RVA: 0x000FA61C File Offset: 0x000F881C
			public string Connection
			{
				get
				{
					return this.httpWebRequest.Connection;
				}
				set
				{
					this.httpWebRequest.Connection = value;
				}
			}

			// Token: 0x170017D7 RID: 6103
			// (get) Token: 0x06004BAD RID: 19373 RVA: 0x000FA62A File Offset: 0x000F882A
			// (set) Token: 0x06004BAE RID: 19374 RVA: 0x000FA638 File Offset: 0x000F8838
			public string Expect
			{
				get
				{
					return this.httpWebRequest.Expect;
				}
				set
				{
					try
					{
						this.httpWebRequest.Expect = value;
					}
					catch (ArgumentException ex)
					{
						throw ValueException.NewDataSourceError(ex.Message, TextValue.New(ex.Message), ex);
					}
				}
			}

			// Token: 0x170017D8 RID: 6104
			// (get) Token: 0x06004BAF RID: 19375 RVA: 0x000FA67C File Offset: 0x000F887C
			// (set) Token: 0x06004BB0 RID: 19376 RVA: 0x000FA689 File Offset: 0x000F8889
			public bool Pipelined
			{
				get
				{
					return this.httpWebRequest.Pipelined;
				}
				set
				{
					this.httpWebRequest.Pipelined = value;
				}
			}

			// Token: 0x170017D9 RID: 6105
			// (get) Token: 0x06004BB1 RID: 19377 RVA: 0x000FA697 File Offset: 0x000F8897
			// (set) Token: 0x06004BB2 RID: 19378 RVA: 0x000FA6A4 File Offset: 0x000F88A4
			public string Referer
			{
				get
				{
					return this.httpWebRequest.Referer;
				}
				set
				{
					this.httpWebRequest.Referer = value;
				}
			}

			// Token: 0x170017DA RID: 6106
			// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x000FA6B2 File Offset: 0x000F88B2
			// (set) Token: 0x06004BB4 RID: 19380 RVA: 0x000FA6BF File Offset: 0x000F88BF
			public bool SendChunked
			{
				get
				{
					return this.httpWebRequest.SendChunked;
				}
				set
				{
					this.httpWebRequest.SendChunked = value;
				}
			}

			// Token: 0x170017DB RID: 6107
			// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x000FA6CD File Offset: 0x000F88CD
			// (set) Token: 0x06004BB6 RID: 19382 RVA: 0x000FA6DA File Offset: 0x000F88DA
			public string TransferEncoding
			{
				get
				{
					return this.httpWebRequest.TransferEncoding;
				}
				set
				{
					this.httpWebRequest.TransferEncoding = value;
				}
			}

			// Token: 0x170017DC RID: 6108
			// (get) Token: 0x06004BB7 RID: 19383 RVA: 0x000FA6E8 File Offset: 0x000F88E8
			// (set) Token: 0x06004BB8 RID: 19384 RVA: 0x000FA6F5 File Offset: 0x000F88F5
			public DateTime IfModifiedSince
			{
				get
				{
					return this.httpWebRequest.IfModifiedSince;
				}
				set
				{
					this.httpWebRequest.IfModifiedSince = value;
				}
			}

			// Token: 0x06004BB9 RID: 19385 RVA: 0x000FA703 File Offset: 0x000F8903
			public void SetRange(string range)
			{
				RequestHeaders.HttpRequestWrapper.addWithoutValidate.Invoke(this.httpWebRequest.Headers, new object[] { "Range", range });
			}

			// Token: 0x04002827 RID: 10279
			private static readonly MethodInfo addWithoutValidate = typeof(WebHeaderCollection).GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);

			// Token: 0x04002828 RID: 10280
			private readonly MashupHttpWebRequest httpWebRequest;
		}
	}
}
