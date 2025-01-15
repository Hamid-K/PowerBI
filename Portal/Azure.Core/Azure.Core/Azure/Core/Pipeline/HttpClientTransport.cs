using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000089 RID: 137
	[NullableContext(1)]
	[Nullable(0)]
	public class HttpClientTransport : HttpPipelineTransport, IDisposable
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000CF84 File Offset: 0x0000B184
		internal HttpClient Client { get; }

		// Token: 0x06000459 RID: 1113 RVA: 0x0000CF8C File Offset: 0x0000B18C
		public HttpClientTransport()
			: this(HttpClientTransport.CreateDefaultClient(null))
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000CF9A File Offset: 0x0000B19A
		[NullableContext(2)]
		internal HttpClientTransport(HttpPipelineTransportOptions options = null)
			: this(HttpClientTransport.CreateDefaultClient(options))
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public HttpClientTransport(HttpMessageHandler messageHandler)
		{
			HttpClient httpClient = new HttpClient(messageHandler);
			if (httpClient == null)
			{
				throw new ArgumentNullException("messageHandler");
			}
			this.Client = httpClient;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000CFCB File Offset: 0x0000B1CB
		public HttpClientTransport(HttpClient client)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			this.Client = client;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000CFE9 File Offset: 0x0000B1E9
		public sealed override Request CreateRequest()
		{
			return new HttpClientTransport.PipelineRequest();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000CFF0 File Offset: 0x0000B1F0
		public override void Process(HttpMessage message)
		{
			this.ProcessAsync(message).AsTask().GetAwaiter()
				.GetResult();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000D019 File Offset: 0x0000B219
		public override ValueTask ProcessAsync(HttpMessage message)
		{
			return this.ProcessAsync(message, true);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000D024 File Offset: 0x0000B224
		private async ValueTask ProcessAsync(HttpMessage message, bool async)
		{
			using (HttpRequestMessage httpRequest = HttpClientTransport.BuildRequestMessage(message))
			{
				HttpClientTransport.SetPropertiesOrOptions<HttpMessage>(httpRequest, "MessageForServerCertificateCallback", message);
				Stream contentStream = null;
				message.ClearResponse();
				HttpResponseMessage responseMessage;
				try
				{
					HttpResponseMessage httpResponseMessage = await this.Client.SendAsync(httpRequest, 1, message.CancellationToken).ConfigureAwait(false);
					responseMessage = httpResponseMessage;
					if (responseMessage.Content != null)
					{
						contentStream = await responseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
					}
				}
				catch (OperationCanceledException ex) when (CancellationHelper.ShouldWrapInOperationCanceledException(ex, message.CancellationToken))
				{
					throw CancellationHelper.CreateOperationCanceledException(ex, message.CancellationToken, null);
				}
				catch (HttpRequestException ex2)
				{
					throw new RequestFailedException(ex2.Message, ex2);
				}
				message.Response = new HttpClientTransport.PipelineResponse(message.Request.ClientRequestId, responseMessage, contentStream);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000D06F File Offset: 0x0000B26F
		private static HttpClient CreateDefaultClient([Nullable(2)] HttpPipelineTransportOptions options = null)
		{
			HttpMessageHandler httpMessageHandler = HttpClientTransport.CreateDefaultHandler(options);
			HttpClientTransport.SetProxySettings(httpMessageHandler);
			ServicePointHelpers.SetLimits(httpMessageHandler);
			return new HttpClient(httpMessageHandler)
			{
				Timeout = Timeout.InfiniteTimeSpan
			};
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000D093 File Offset: 0x0000B293
		private static HttpMessageHandler CreateDefaultHandler([Nullable(2)] HttpPipelineTransportOptions options = null)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return new HttpClientHandler();
			}
			return HttpClientTransport.ApplyOptionsToHandler(new HttpClientHandler
			{
				AllowAutoRedirect = false,
				UseCookies = HttpClientTransport.UseCookies()
			}, options);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		private static void SetProxySettings(HttpMessageHandler messageHandler)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return;
			}
			IWebProxy webProxy;
			if (HttpEnvironmentProxy.TryCreate(out webProxy))
			{
				HttpClientHandler httpClientHandler = messageHandler as HttpClientHandler;
				if (httpClientHandler != null)
				{
					httpClientHandler.Proxy = webProxy;
				}
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000D105 File Offset: 0x0000B305
		private static HttpRequestMessage BuildRequestMessage(HttpMessage message)
		{
			HttpClientTransport.PipelineRequest pipelineRequest = message.Request as HttpClientTransport.PipelineRequest;
			if (pipelineRequest == null)
			{
				throw new InvalidOperationException("the request is not compatible with the transport");
			}
			return pipelineRequest.BuildRequestMessage(message.CancellationToken);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000D12C File Offset: 0x0000B32C
		internal static bool TryGetHeader(HttpHeaders headers, [Nullable(2)] HttpContent content, string name, [Nullable(2)] [NotNullWhen(true)] out string value)
		{
			IEnumerable<string> enumerable;
			if (HttpClientTransport.TryGetHeader(headers, content, name, out enumerable))
			{
				value = HttpClientTransport.JoinHeaderValues(enumerable);
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000D153 File Offset: 0x0000B353
		internal static bool TryGetHeader(HttpHeaders headers, [Nullable(2)] HttpContent content, string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values)
		{
			return headers.TryGetValues(name, ref values) || (content != null && content.Headers.TryGetValues(name, ref values));
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000D173 File Offset: 0x0000B373
		internal static IEnumerable<HttpHeader> GetHeaders(HttpHeaders headers, [Nullable(2)] HttpContent content)
		{
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
			{
				yield return new HttpHeader(keyValuePair.Key, HttpClientTransport.JoinHeaderValues(keyValuePair.Value));
			}
			IEnumerator<KeyValuePair<string, IEnumerable<string>>> enumerator = null;
			if (content != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair2 in content.Headers)
				{
					yield return new HttpHeader(keyValuePair2.Key, HttpClientTransport.JoinHeaderValues(keyValuePair2.Value));
				}
				enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000D18C File Offset: 0x0000B38C
		internal static bool RemoveHeader(HttpHeaders headers, [Nullable(2)] HttpContent content, string name)
		{
			IEnumerable<string> enumerable;
			return (headers.TryGetValues(name, ref enumerable) && headers.Remove(name)) || (content != null && content.Headers.TryGetValues(name, ref enumerable) && content.Headers.Remove(name));
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000D1D0 File Offset: 0x0000B3D0
		internal static bool ContainsHeader(HttpHeaders headers, [Nullable(2)] HttpContent content, string name)
		{
			IEnumerable<string> enumerable;
			return headers.TryGetValues(name, ref enumerable) || (content != null && content.Headers.TryGetValues(name, ref enumerable));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000D1FD File Offset: 0x0000B3FD
		private static string JoinHeaderValues(IEnumerable<string> values)
		{
			return string.Join(",", values);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000D20C File Offset: 0x0000B40C
		private static HttpClientHandler ApplyOptionsToHandler(HttpClientHandler httpHandler, [Nullable(2)] HttpPipelineTransportOptions options)
		{
			if (options == null || RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
			{
				return httpHandler;
			}
			if (options.ServerCertificateCustomValidationCallback != null)
			{
				httpHandler.ServerCertificateCustomValidationCallback = (HttpRequestMessage _, X509Certificate2 certificate2, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => options.ServerCertificateCustomValidationCallback(new ServerCertificateCustomValidationArgs(certificate2, x509Chain, sslPolicyErrors));
			}
			foreach (X509Certificate2 x509Certificate in options.ClientCertificates)
			{
				httpHandler.ClientCertificates.Add(x509Certificate);
			}
			return httpHandler;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		public void Dispose()
		{
			if (this != HttpClientTransport.Shared)
			{
				this.Client.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000D2C7 File Offset: 0x0000B4C7
		private static void SetPropertiesOrOptions<[Nullable(2)] T>(HttpRequestMessage httpRequest, string name, T value)
		{
			httpRequest.Properties[name] = value;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000D2DB File Offset: 0x0000B4DB
		private static bool UseCookies()
		{
			return AppContextSwitchHelper.GetConfigValue("Azure.Core.Pipeline.HttpClientTransport.EnableCookies", "AZURE_CORE_HTTPCLIENT_ENABLE_COOKIES");
		}

		// Token: 0x040001CC RID: 460
		internal const string MessageForServerCertificateCallback = "MessageForServerCertificateCallback";

		// Token: 0x040001CE RID: 462
		public static readonly HttpClientTransport Shared = new HttpClientTransport();

		// Token: 0x02000114 RID: 276
		[Nullable(0)]
		private sealed class PipelineRequest : Request
		{
			// Token: 0x060007B1 RID: 1969 RVA: 0x0001C192 File Offset: 0x0001A392
			public PipelineRequest()
			{
				this.Method = RequestMethod.Get;
				this._headers = new ArrayBackedPropertyBag<HttpClientTransport.PipelineRequest.IgnoreCaseString, object>();
			}

			// Token: 0x170001CF RID: 463
			// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0001C1B0 File Offset: 0x0001A3B0
			// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0001C1E3 File Offset: 0x0001A3E3
			public override string ClientRequestId
			{
				get
				{
					string text;
					if ((text = this._clientRequestId) == null)
					{
						text = (this._clientRequestId = Guid.NewGuid().ToString());
					}
					return text;
				}
				set
				{
					Argument.AssertNotNull<string>(value, "value");
					this._clientRequestId = value;
				}
			}

			// Token: 0x060007B4 RID: 1972 RVA: 0x0001C1F7 File Offset: 0x0001A3F7
			protected internal override void SetHeader(string name, string value)
			{
				this._headers.Set(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name), value);
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x0001C20C File Offset: 0x0001A40C
			protected internal override void AddHeader(string name, string value)
			{
				object obj;
				if (this._headers.TryAdd(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name), value, out obj))
				{
					return;
				}
				string text = obj as string;
				if (text != null)
				{
					this._headers.Set(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name), new List<string> { text, value });
					return;
				}
				List<string> list = obj as List<string>;
				if (list == null)
				{
					return;
				}
				list.Add(value);
			}

			// Token: 0x060007B6 RID: 1974 RVA: 0x0001C274 File Offset: 0x0001A474
			protected internal override bool TryGetHeader(string name, [Nullable(2)] [NotNullWhen(true)] out string value)
			{
				object obj;
				if (this._headers.TryGetValue(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name), out obj))
				{
					value = HttpClientTransport.PipelineRequest.GetHttpHeaderValue(name, obj);
					return true;
				}
				value = null;
				return false;
			}

			// Token: 0x060007B7 RID: 1975 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
			protected internal override bool TryGetHeaderValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values)
			{
				object obj;
				if (this._headers.TryGetValue(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name), out obj))
				{
					string text = obj as string;
					IEnumerable<string> enumerable;
					if (text == null)
					{
						List<string> list = obj as List<string>;
						if (list == null)
						{
							throw new InvalidOperationException(string.Format("Unexpected type for header {0}: {1}", name, obj.GetType()));
						}
						enumerable = list;
					}
					else
					{
						enumerable = new string[] { text };
					}
					values = enumerable;
					return true;
				}
				values = null;
				return false;
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x0001C314 File Offset: 0x0001A514
			protected internal override bool ContainsHeader(string name)
			{
				object obj;
				return this._headers.TryGetValue(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name), out obj);
			}

			// Token: 0x060007B9 RID: 1977 RVA: 0x0001C334 File Offset: 0x0001A534
			protected internal override bool RemoveHeader(string name)
			{
				return this._headers.TryRemove(new HttpClientTransport.PipelineRequest.IgnoreCaseString(name));
			}

			// Token: 0x060007BA RID: 1978 RVA: 0x0001C347 File Offset: 0x0001A547
			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				int num;
				for (int i = 0; i < this._headers.Count; i = num + 1)
				{
					HttpClientTransport.PipelineRequest.IgnoreCaseString ignoreCaseString;
					object obj;
					this._headers.GetAt(i, out ignoreCaseString, out obj);
					yield return new HttpHeader(ignoreCaseString, HttpClientTransport.PipelineRequest.GetHttpHeaderValue(ignoreCaseString, obj));
					num = i;
				}
				yield break;
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x0001C358 File Offset: 0x0001A558
			public HttpRequestMessage BuildRequestMessage(CancellationToken cancellation)
			{
				HttpMethod httpMethod = HttpClientTransport.PipelineRequest.ToHttpClientMethod(this.Method);
				Uri uri = this.Uri.ToUri();
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(httpMethod, uri);
				HttpClientTransport.PipelineRequest.PipelineContentAdapter pipelineContentAdapter = ((this.Content != null) ? new HttpClientTransport.PipelineRequest.PipelineContentAdapter(this.Content, cancellation) : null);
				httpRequestMessage.Content = pipelineContentAdapter;
				httpRequestMessage.Headers.ExpectContinue = new bool?(false);
				for (int i = 0; i < this._headers.Count; i++)
				{
					HttpClientTransport.PipelineRequest.IgnoreCaseString ignoreCaseString;
					object obj;
					this._headers.GetAt(i, out ignoreCaseString, out obj);
					string text = obj as string;
					AuthenticationHeaderValue authenticationHeaderValue;
					if (text == null)
					{
						List<string> list = obj as List<string>;
						if (list != null)
						{
							if (!httpRequestMessage.Headers.TryAddWithoutValidation(ignoreCaseString, list) && pipelineContentAdapter != null && !pipelineContentAdapter.Headers.TryAddWithoutValidation(ignoreCaseString, list))
							{
								throw new InvalidOperationException(string.Format("Unable to add header {0} to header collection.", ignoreCaseString));
							}
						}
					}
					else if (ignoreCaseString == HttpHeader.Names.Authorization && AuthenticationHeaderValue.TryParse(text, ref authenticationHeaderValue))
					{
						httpRequestMessage.Headers.Authorization = authenticationHeaderValue;
					}
					else if (!httpRequestMessage.Headers.TryAddWithoutValidation(ignoreCaseString, text) && pipelineContentAdapter != null && !pipelineContentAdapter.Headers.TryAddWithoutValidation(ignoreCaseString, text))
					{
						throw new InvalidOperationException(string.Format("Unable to add header {0} to header collection.", ignoreCaseString));
					}
				}
				HttpClientTransport.PipelineRequest.AddPropertiesForBlazor(httpRequestMessage);
				return httpRequestMessage;
			}

			// Token: 0x060007BC RID: 1980 RVA: 0x0001C4C1 File Offset: 0x0001A6C1
			private static void AddPropertiesForBlazor(HttpRequestMessage currentRequest)
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")))
				{
					HttpClientTransport.SetPropertiesOrOptions<Dictionary<string, object>>(currentRequest, "WebAssemblyFetchOptions", new Dictionary<string, object> { { "cache", "no-store" } });
					HttpClientTransport.SetPropertiesOrOptions<bool>(currentRequest, "WebAssemblyEnableStreamingResponse", true);
				}
			}

			// Token: 0x060007BD RID: 1981 RVA: 0x0001C500 File Offset: 0x0001A700
			private static string GetHttpHeaderValue(string headerName, object value)
			{
				string text = value as string;
				string text2;
				if (text == null)
				{
					List<string> list = value as List<string>;
					if (list == null)
					{
						throw new InvalidOperationException(string.Format("Unexpected type for header {0}: {1}", headerName, (value != null) ? value.GetType() : null));
					}
					text2 = string.Join(",", list);
				}
				else
				{
					text2 = text;
				}
				return text2;
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x0001C554 File Offset: 0x0001A754
			public override void Dispose()
			{
				this._headers.Dispose();
				RequestContent content = this.Content;
				if (content != null)
				{
					this.Content = null;
					content.Dispose();
				}
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x0001C584 File Offset: 0x0001A784
			public override string ToString()
			{
				return this.BuildRequestMessage(default(CancellationToken)).ToString();
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
			private static HttpMethod ToHttpClientMethod(RequestMethod requestMethod)
			{
				string method = requestMethod.Method;
				if (method.Length == 3)
				{
					if (string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Get;
					}
					if (string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Put;
					}
				}
				else if (method.Length == 4)
				{
					if (string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Post;
					}
					if (string.Equals(method, "HEAD", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Head;
					}
				}
				else
				{
					if (string.Equals(method, "PATCH", StringComparison.OrdinalIgnoreCase))
					{
						return HttpClientTransport.PipelineRequest.s_patch;
					}
					if (string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase))
					{
						return HttpMethod.Delete;
					}
				}
				return new HttpMethod(method);
			}

			// Token: 0x04000407 RID: 1031
			[Nullable(2)]
			private string _clientRequestId;

			// Token: 0x04000408 RID: 1032
			[Nullable(new byte[] { 0, 1 })]
			private ArrayBackedPropertyBag<HttpClientTransport.PipelineRequest.IgnoreCaseString, object> _headers;

			// Token: 0x04000409 RID: 1033
			private static readonly HttpMethod s_patch = new HttpMethod("PATCH");

			// Token: 0x0200016B RID: 363
			[Nullable(0)]
			private readonly struct IgnoreCaseString : IEquatable<HttpClientTransport.PipelineRequest.IgnoreCaseString>
			{
				// Token: 0x06000924 RID: 2340 RVA: 0x0002359A File Offset: 0x0002179A
				public IgnoreCaseString(string value)
				{
					this._value = value;
				}

				// Token: 0x06000925 RID: 2341 RVA: 0x000235A3 File Offset: 0x000217A3
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public bool Equals(HttpClientTransport.PipelineRequest.IgnoreCaseString other)
				{
					return string.Equals(this._value, other._value, StringComparison.OrdinalIgnoreCase);
				}

				// Token: 0x06000926 RID: 2342 RVA: 0x000235B8 File Offset: 0x000217B8
				[NullableContext(2)]
				public override bool Equals(object obj)
				{
					if (obj is HttpClientTransport.PipelineRequest.IgnoreCaseString)
					{
						HttpClientTransport.PipelineRequest.IgnoreCaseString ignoreCaseString = (HttpClientTransport.PipelineRequest.IgnoreCaseString)obj;
						return this.Equals(ignoreCaseString);
					}
					return false;
				}

				// Token: 0x06000927 RID: 2343 RVA: 0x000235DD File Offset: 0x000217DD
				public override int GetHashCode()
				{
					return this._value.GetHashCode();
				}

				// Token: 0x06000928 RID: 2344 RVA: 0x000235EA File Offset: 0x000217EA
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public static bool operator ==(HttpClientTransport.PipelineRequest.IgnoreCaseString left, HttpClientTransport.PipelineRequest.IgnoreCaseString right)
				{
					return left.Equals(right);
				}

				// Token: 0x06000929 RID: 2345 RVA: 0x000235F4 File Offset: 0x000217F4
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public static bool operator !=(HttpClientTransport.PipelineRequest.IgnoreCaseString left, HttpClientTransport.PipelineRequest.IgnoreCaseString right)
				{
					return !left.Equals(right);
				}

				// Token: 0x0600092A RID: 2346 RVA: 0x00023601 File Offset: 0x00021801
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				public static implicit operator string(HttpClientTransport.PipelineRequest.IgnoreCaseString ics)
				{
					return ics._value;
				}

				// Token: 0x04000575 RID: 1397
				private readonly string _value;
			}

			// Token: 0x0200016C RID: 364
			[Nullable(0)]
			private sealed class PipelineContentAdapter : HttpContent
			{
				// Token: 0x0600092B RID: 2347 RVA: 0x00023609 File Offset: 0x00021809
				public PipelineContentAdapter(RequestContent pipelineContent, CancellationToken cancellationToken)
				{
					this._pipelineContent = pipelineContent;
					this._cancellationToken = cancellationToken;
				}

				// Token: 0x0600092C RID: 2348 RVA: 0x00023620 File Offset: 0x00021820
				protected override async Task SerializeToStreamAsync(Stream stream, [Nullable(2)] TransportContext context)
				{
					await this._pipelineContent.WriteToAsync(stream, this._cancellationToken).ConfigureAwait(false);
				}

				// Token: 0x0600092D RID: 2349 RVA: 0x0002366B File Offset: 0x0002186B
				protected override bool TryComputeLength(out long length)
				{
					return this._pipelineContent.TryComputeLength(out length);
				}

				// Token: 0x04000576 RID: 1398
				private readonly RequestContent _pipelineContent;

				// Token: 0x04000577 RID: 1399
				private readonly CancellationToken _cancellationToken;
			}
		}

		// Token: 0x02000115 RID: 277
		[Nullable(0)]
		private sealed class PipelineResponse : Response
		{
			// Token: 0x060007C2 RID: 1986 RVA: 0x0001C660 File Offset: 0x0001A860
			public PipelineResponse(string requestId, HttpResponseMessage responseMessage, [Nullable(2)] Stream contentStream)
			{
				if (requestId == null)
				{
					throw new ArgumentNullException("requestId");
				}
				this.ClientRequestId = requestId;
				if (responseMessage == null)
				{
					throw new ArgumentNullException("responseMessage");
				}
				this._responseMessage = responseMessage;
				this._contentStream = contentStream;
				this._responseContent = this._responseMessage.Content;
			}

			// Token: 0x170001D0 RID: 464
			// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001C6B7 File Offset: 0x0001A8B7
			public override int Status
			{
				get
				{
					return (int)this._responseMessage.StatusCode;
				}
			}

			// Token: 0x170001D1 RID: 465
			// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001C6C4 File Offset: 0x0001A8C4
			public override string ReasonPhrase
			{
				get
				{
					return this._responseMessage.ReasonPhrase ?? string.Empty;
				}
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0001C6DA File Offset: 0x0001A8DA
			// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0001C6E2 File Offset: 0x0001A8E2
			[Nullable(2)]
			public override Stream ContentStream
			{
				[NullableContext(2)]
				get
				{
					return this._contentStream;
				}
				[NullableContext(2)]
				set
				{
					this._responseMessage.Content = null;
					this._contentStream = value;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0001C6F7 File Offset: 0x0001A8F7
			// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0001C6FF File Offset: 0x0001A8FF
			public override string ClientRequestId { get; set; }

			// Token: 0x060007C9 RID: 1993 RVA: 0x0001C708 File Offset: 0x0001A908
			protected internal override bool TryGetHeader(string name, [Nullable(2)] [NotNullWhen(true)] out string value)
			{
				return HttpClientTransport.TryGetHeader(this._responseMessage.Headers, this._responseContent, name, out value);
			}

			// Token: 0x060007CA RID: 1994 RVA: 0x0001C722 File Offset: 0x0001A922
			protected internal override bool TryGetHeaderValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values)
			{
				return HttpClientTransport.TryGetHeader(this._responseMessage.Headers, this._responseContent, name, out values);
			}

			// Token: 0x060007CB RID: 1995 RVA: 0x0001C73C File Offset: 0x0001A93C
			protected internal override bool ContainsHeader(string name)
			{
				return HttpClientTransport.ContainsHeader(this._responseMessage.Headers, this._responseContent, name);
			}

			// Token: 0x060007CC RID: 1996 RVA: 0x0001C755 File Offset: 0x0001A955
			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				return HttpClientTransport.GetHeaders(this._responseMessage.Headers, this._responseContent);
			}

			// Token: 0x060007CD RID: 1997 RVA: 0x0001C76D File Offset: 0x0001A96D
			public override void Dispose()
			{
				HttpResponseMessage responseMessage = this._responseMessage;
				if (responseMessage != null)
				{
					responseMessage.Dispose();
				}
				Response.DisposeStreamIfNotBuffered(ref this._contentStream);
			}

			// Token: 0x060007CE RID: 1998 RVA: 0x0001C78B File Offset: 0x0001A98B
			public override string ToString()
			{
				return this._responseMessage.ToString();
			}

			// Token: 0x0400040A RID: 1034
			private readonly HttpResponseMessage _responseMessage;

			// Token: 0x0400040B RID: 1035
			private readonly HttpContent _responseContent;

			// Token: 0x0400040C RID: 1036
			[Nullable(2)]
			private Stream _contentStream;
		}
	}
}
