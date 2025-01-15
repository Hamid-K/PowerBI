using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Azure.Core.Pipeline
{
	// Token: 0x02000091 RID: 145
	[NullableContext(1)]
	[Nullable(0)]
	internal class HttpWebRequestTransport : HttpPipelineTransport
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0000E0CA File Offset: 0x0000C2CA
		public HttpWebRequestTransport()
			: this(delegate(HttpWebRequest _)
			{
			})
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000E0F4 File Offset: 0x0000C2F4
		internal HttpWebRequestTransport(HttpPipelineTransportOptions options)
			: this(delegate(HttpWebRequest req)
			{
				HttpWebRequestTransport.ApplyOptionsToRequest(req, options);
			})
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0000E120 File Offset: 0x0000C320
		internal HttpWebRequestTransport(Action<HttpWebRequest> configureRequest)
		{
			this._configureRequest = configureRequest;
			IWebProxy webProxy;
			if (HttpEnvironmentProxy.TryCreate(out webProxy))
			{
				this._environmentProxy = webProxy;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000E14A File Offset: 0x0000C34A
		public override void Process(HttpMessage message)
		{
			this.ProcessInternal(message, false).EnsureCompleted();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000E15C File Offset: 0x0000C35C
		public override async ValueTask ProcessAsync(HttpMessage message)
		{
			await this.ProcessInternal(message, true).ConfigureAwait(false);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		private ValueTask ProcessInternal(HttpMessage message, bool async)
		{
			HttpWebRequestTransport.<ProcessInternal>d__8 <ProcessInternal>d__;
			<ProcessInternal>d__.<>t__builder = AsyncValueTaskMethodBuilder.Create();
			<ProcessInternal>d__.<>4__this = this;
			<ProcessInternal>d__.message = message;
			<ProcessInternal>d__.async = async;
			<ProcessInternal>d__.<>1__state = -1;
			<ProcessInternal>d__.<>t__builder.Start<HttpWebRequestTransport.<ProcessInternal>d__8>(ref <ProcessInternal>d__);
			return <ProcessInternal>d__.<>t__builder.Task;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		private HttpWebRequest CreateRequest(Request messageRequest)
		{
			HttpWebRequest httpWebRequest = WebRequest.CreateHttp(messageRequest.Uri.ToUri());
			httpWebRequest.Timeout = -1;
			httpWebRequest.ReadWriteTimeout = -1;
			httpWebRequest.AllowAutoRedirect = false;
			if (this._environmentProxy != null)
			{
				httpWebRequest.Proxy = this._environmentProxy;
			}
			httpWebRequest.ServicePoint.Expect100Continue = false;
			this._configureRequest(httpWebRequest);
			httpWebRequest.Method = messageRequest.Method.Method;
			foreach (HttpHeader httpHeader in messageRequest.Headers)
			{
				if (string.Equals(httpHeader.Name, HttpHeader.Names.ContentLength, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.ContentLength = long.Parse(httpHeader.Value, CultureInfo.InvariantCulture);
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.Host, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.Host = httpHeader.Value;
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.Date, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.Date = DateTime.Parse(httpHeader.Value, CultureInfo.InvariantCulture);
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.ContentType, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.ContentType = httpHeader.Value;
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.UserAgent, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.UserAgent = httpHeader.Value;
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.Accept, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.Accept = httpHeader.Value;
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.Referer, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.Referer = httpHeader.Value;
				}
				else if (string.Equals(httpHeader.Name, HttpHeader.Names.IfModifiedSince, StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.IfModifiedSince = DateTime.Parse(httpHeader.Value, CultureInfo.InvariantCulture);
				}
				else if (string.Equals(httpHeader.Name, "Expect", StringComparison.OrdinalIgnoreCase))
				{
					if (httpHeader.Value == "100-continue")
					{
						httpWebRequest.ServicePoint.Expect100Continue = true;
					}
					else
					{
						httpWebRequest.Expect = httpHeader.Value;
					}
				}
				else if (string.Equals(httpHeader.Name, "Transfer-Encoding", StringComparison.OrdinalIgnoreCase))
				{
					httpWebRequest.TransferEncoding = httpHeader.Value;
				}
				else
				{
					if (string.Equals(httpHeader.Name, HttpHeader.Names.Range, StringComparison.OrdinalIgnoreCase))
					{
						RangeHeaderValue rangeHeaderValue = RangeHeaderValue.Parse(httpHeader.Value);
						if (rangeHeaderValue.Unit != "bytes")
						{
							throw new InvalidOperationException("Only ranges with bytes unit supported.");
						}
						using (IEnumerator<RangeItemHeaderValue> enumerator2 = rangeHeaderValue.Ranges.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								RangeItemHeaderValue rangeItemHeaderValue = enumerator2.Current;
								if (rangeItemHeaderValue.From == null)
								{
									throw new InvalidOperationException("Only ranges with Offset supported.");
								}
								if (rangeItemHeaderValue.To == null)
								{
									httpWebRequest.AddRange(rangeItemHeaderValue.From.Value);
								}
								else
								{
									httpWebRequest.AddRange(rangeItemHeaderValue.From.Value, rangeItemHeaderValue.To.Value);
								}
							}
							continue;
						}
					}
					httpWebRequest.Headers.Add(httpHeader.Name, httpHeader.Value);
				}
			}
			long num;
			if (httpWebRequest.ContentLength == -1L && messageRequest.Content != null && messageRequest.Content.TryComputeLength(out num))
			{
				httpWebRequest.ContentLength = num;
			}
			if (httpWebRequest.ContentLength != -1L)
			{
				httpWebRequest.AllowWriteStreamBuffering = false;
			}
			return httpWebRequest;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		public override Request CreateRequest()
		{
			return new HttpWebRequestTransport.HttpWebRequestImplementation();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		private static void ApplyOptionsToRequest(HttpWebRequest request, HttpPipelineTransportOptions options)
		{
			if (options == null)
			{
				return;
			}
			if (options.ServerCertificateCustomValidationCallback != null)
			{
				request.ServerCertificateValidationCallback = (object request, X509Certificate certificate, X509Chain x509Chain, SslPolicyErrors sslPolicyErrors) => options.ServerCertificateCustomValidationCallback(new ServerCertificateCustomValidationArgs(new X509Certificate2(certificate), x509Chain, sslPolicyErrors));
			}
			foreach (X509Certificate2 x509Certificate in options.ClientCertificates)
			{
				request.ClientCertificates.Add(x509Certificate);
			}
		}

		// Token: 0x040001E0 RID: 480
		private readonly Action<HttpWebRequest> _configureRequest;

		// Token: 0x040001E1 RID: 481
		public static readonly HttpWebRequestTransport Shared = new HttpWebRequestTransport();

		// Token: 0x040001E2 RID: 482
		[Nullable(2)]
		private readonly IWebProxy _environmentProxy;

		// Token: 0x0200011E RID: 286
		[Nullable(0)]
		private sealed class HttpWebResponseImplementation : Response
		{
			// Token: 0x060007E6 RID: 2022 RVA: 0x0001D13A File Offset: 0x0001B33A
			public HttpWebResponseImplementation(string clientRequestId, HttpWebResponse webResponse)
			{
				this._webResponse = webResponse;
				this._originalContentStream = this._webResponse.GetResponseStream();
				this._contentStream = this._originalContentStream;
				this.ClientRequestId = clientRequestId;
			}

			// Token: 0x170001D7 RID: 471
			// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0001D16D File Offset: 0x0001B36D
			public override int Status
			{
				get
				{
					return (int)this._webResponse.StatusCode;
				}
			}

			// Token: 0x170001D8 RID: 472
			// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0001D17A File Offset: 0x0001B37A
			public override string ReasonPhrase
			{
				get
				{
					return this._webResponse.StatusDescription;
				}
			}

			// Token: 0x170001D9 RID: 473
			// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0001D187 File Offset: 0x0001B387
			// (set) Token: 0x060007EA RID: 2026 RVA: 0x0001D18F File Offset: 0x0001B38F
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
					this._originalContentStream = null;
					this._contentStream = value;
				}
			}

			// Token: 0x170001DA RID: 474
			// (get) Token: 0x060007EB RID: 2027 RVA: 0x0001D19F File Offset: 0x0001B39F
			// (set) Token: 0x060007EC RID: 2028 RVA: 0x0001D1A7 File Offset: 0x0001B3A7
			public override string ClientRequestId { get; set; }

			// Token: 0x060007ED RID: 2029 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
			public override void Dispose()
			{
				Response.DisposeStreamIfNotBuffered(ref this._originalContentStream);
				Response.DisposeStreamIfNotBuffered(ref this._contentStream);
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
			protected internal override bool TryGetHeader(string name, [Nullable(2)] [NotNullWhen(true)] out string value)
			{
				value = this._webResponse.Headers.Get(name);
				return value != null;
			}

			// Token: 0x060007EF RID: 2031 RVA: 0x0001D1E2 File Offset: 0x0001B3E2
			protected internal override bool TryGetHeaderValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values)
			{
				values = this._webResponse.Headers.GetValues(name);
				return values != null;
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x0001D1FC File Offset: 0x0001B3FC
			protected internal override bool ContainsHeader(string name)
			{
				return this._webResponse.Headers.Get(name) != null;
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x0001D212 File Offset: 0x0001B412
			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				foreach (string text in this._webResponse.Headers.AllKeys)
				{
					yield return new HttpHeader(text, this._webResponse.Headers.Get(text));
				}
				string[] array = null;
				yield break;
			}

			// Token: 0x04000438 RID: 1080
			private readonly HttpWebResponse _webResponse;

			// Token: 0x04000439 RID: 1081
			[Nullable(2)]
			private Stream _contentStream;

			// Token: 0x0400043A RID: 1082
			[Nullable(2)]
			private Stream _originalContentStream;
		}

		// Token: 0x0200011F RID: 287
		[Nullable(0)]
		private sealed class HttpWebRequestImplementation : Request
		{
			// Token: 0x060007F2 RID: 2034 RVA: 0x0001D222 File Offset: 0x0001B422
			public HttpWebRequestImplementation()
			{
				this.Method = RequestMethod.Get;
			}

			// Token: 0x060007F3 RID: 2035 RVA: 0x0001D240 File Offset: 0x0001B440
			protected internal override void SetHeader(string name, string value)
			{
				this._headers.SetHeader(name, value);
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x0001D24F File Offset: 0x0001B44F
			protected internal override void AddHeader(string name, string value)
			{
				this._headers.AddHeader(name, value);
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x0001D25E File Offset: 0x0001B45E
			protected internal override bool TryGetHeader(string name, out string value)
			{
				return this._headers.TryGetHeader(name, out value);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x0001D26D File Offset: 0x0001B46D
			protected internal override bool TryGetHeaderValues(string name, out IEnumerable<string> values)
			{
				return this._headers.TryGetHeaderValues(name, out values);
			}

			// Token: 0x060007F7 RID: 2039 RVA: 0x0001D27C File Offset: 0x0001B47C
			protected internal override bool ContainsHeader(string name)
			{
				IEnumerable<string> enumerable;
				return this._headers.TryGetHeaderValues(name, out enumerable);
			}

			// Token: 0x060007F8 RID: 2040 RVA: 0x0001D297 File Offset: 0x0001B497
			protected internal override bool RemoveHeader(string name)
			{
				return this._headers.RemoveHeader(name);
			}

			// Token: 0x060007F9 RID: 2041 RVA: 0x0001D2A5 File Offset: 0x0001B4A5
			protected internal override IEnumerable<HttpHeader> EnumerateHeaders()
			{
				return this._headers.EnumerateHeaders();
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x060007FA RID: 2042 RVA: 0x0001D2B4 File Offset: 0x0001B4B4
			// (set) Token: 0x060007FB RID: 2043 RVA: 0x0001D2E7 File Offset: 0x0001B4E7
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

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001D2FB File Offset: 0x0001B4FB
			// (set) Token: 0x060007FD RID: 2045 RVA: 0x0001D303 File Offset: 0x0001B503
			[Nullable(2)]
			public override RequestContent Content
			{
				[NullableContext(2)]
				get;
				[NullableContext(2)]
				set;
			}

			// Token: 0x060007FE RID: 2046 RVA: 0x0001D30C File Offset: 0x0001B50C
			public override void Dispose()
			{
				RequestContent content = this.Content;
				if (content != null)
				{
					this.Content = null;
					content.Dispose();
				}
			}

			// Token: 0x0400043C RID: 1084
			[Nullable(2)]
			private string _clientRequestId;

			// Token: 0x0400043D RID: 1085
			private readonly DictionaryHeaders _headers = new DictionaryHeaders();
		}
	}
}
