using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200001B RID: 27
	public class HttpMessageContent : HttpContent
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003C64 File Offset: 0x00001E64
		public HttpMessageContent(HttpRequestMessage httpRequest)
		{
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this.HttpRequestMessage = httpRequest;
			base.Headers.ContentType = new MediaTypeHeaderValue("application/http");
			base.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("msgtype", "request"));
			this.InitializeStreamTask();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003CCC File Offset: 0x00001ECC
		public HttpMessageContent(HttpResponseMessage httpResponse)
		{
			if (httpResponse == null)
			{
				throw Error.ArgumentNull("httpResponse");
			}
			this.HttpResponseMessage = httpResponse;
			base.Headers.ContentType = new MediaTypeHeaderValue("application/http");
			base.Headers.ContentType.Parameters.Add(new NameValueHeaderValue("msgtype", "response"));
			this.InitializeStreamTask();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003D33 File Offset: 0x00001F33
		private HttpContent Content
		{
			get
			{
				if (this.HttpRequestMessage == null)
				{
					return this.HttpResponseMessage.Content;
				}
				return this.HttpRequestMessage.Content;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003D54 File Offset: 0x00001F54
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003D5C File Offset: 0x00001F5C
		public HttpRequestMessage HttpRequestMessage { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003D65 File Offset: 0x00001F65
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003D6D File Offset: 0x00001F6D
		public HttpResponseMessage HttpResponseMessage { get; private set; }

		// Token: 0x060000BD RID: 189 RVA: 0x00003D76 File Offset: 0x00001F76
		private void InitializeStreamTask()
		{
			this._streamTask = new Lazy<Task<Stream>>(delegate
			{
				if (this.Content != null)
				{
					return this.Content.ReadAsStreamAsync();
				}
				return null;
			});
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003D90 File Offset: 0x00001F90
		internal static bool ValidateHttpMessageContent(HttpContent content, bool isRequest, bool throwOnError)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			MediaTypeHeaderValue contentType = content.Headers.ContentType;
			if (contentType != null)
			{
				if (!contentType.MediaType.Equals("application/http", StringComparison.OrdinalIgnoreCase))
				{
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.HttpMessageInvalidMediaType, new object[]
						{
							FormattingUtilities.HttpContentType.Name,
							isRequest ? "application/http; msgtype=request" : "application/http; msgtype=response"
						});
					}
					return false;
				}
				else
				{
					foreach (NameValueHeaderValue nameValueHeaderValue in contentType.Parameters)
					{
						if (nameValueHeaderValue.Name.Equals("msgtype", StringComparison.OrdinalIgnoreCase))
						{
							if (FormattingUtilities.UnquoteToken(nameValueHeaderValue.Value).Equals(isRequest ? "request" : "response", StringComparison.OrdinalIgnoreCase))
							{
								return true;
							}
							if (throwOnError)
							{
								throw Error.Argument("content", Resources.HttpMessageInvalidMediaType, new object[]
								{
									FormattingUtilities.HttpContentType.Name,
									isRequest ? "application/http; msgtype=request" : "application/http; msgtype=response"
								});
							}
							return false;
						}
					}
				}
			}
			if (throwOnError)
			{
				throw Error.Argument("content", Resources.HttpMessageInvalidMediaType, new object[]
				{
					FormattingUtilities.HttpContentType.Name,
					isRequest ? "application/http; msgtype=request" : "application/http; msgtype=response"
				});
			}
			return false;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003EF8 File Offset: 0x000020F8
		protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			if (stream == null)
			{
				throw Error.ArgumentNull("stream");
			}
			byte[] array = this.SerializeHeader();
			await stream.WriteAsync(array, 0, array.Length);
			if (this.Content != null)
			{
				this.ValidateStreamForReading(await this._streamTask.Value);
				await this.Content.CopyToAsync(stream);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003F48 File Offset: 0x00002148
		protected override bool TryComputeLength(out long length)
		{
			bool flag = this._streamTask.Value != null;
			length = 0L;
			if (flag)
			{
				Stream stream;
				if (!this._streamTask.Value.TryGetResult(out stream) || stream == null || !stream.CanSeek)
				{
					length = -1L;
					return false;
				}
				length = stream.Length;
			}
			byte[] array = this.SerializeHeader();
			length += (long)array.Length;
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003FA7 File Offset: 0x000021A7
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.HttpRequestMessage != null)
				{
					this.HttpRequestMessage.Dispose();
					this.HttpRequestMessage = null;
				}
				if (this.HttpResponseMessage != null)
				{
					this.HttpResponseMessage.Dispose();
					this.HttpResponseMessage = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003FE8 File Offset: 0x000021E8
		private static void SerializeRequestLine(StringBuilder message, HttpRequestMessage httpRequest)
		{
			message.Append(httpRequest.Method + " ");
			message.Append(httpRequest.RequestUri.PathAndQuery + " ");
			message.Append("HTTP/" + ((httpRequest.Version != null) ? httpRequest.Version.ToString(2) : "1.1") + "\r\n");
			if (httpRequest.Headers.Host == null)
			{
				message.Append("Host: " + httpRequest.RequestUri.Authority + "\r\n");
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004090 File Offset: 0x00002290
		private static void SerializeStatusLine(StringBuilder message, HttpResponseMessage httpResponse)
		{
			message.Append("HTTP/" + ((httpResponse.Version != null) ? httpResponse.Version.ToString(2) : "1.1") + " ");
			message.Append((int)httpResponse.StatusCode + " ");
			message.Append(httpResponse.ReasonPhrase + "\r\n");
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004108 File Offset: 0x00002308
		private static void SerializeHeaderFields(StringBuilder message, HttpHeaders headers)
		{
			if (headers != null)
			{
				foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in headers)
				{
					if (HttpMessageContent._singleValueHeaderFields.Contains(keyValuePair.Key))
					{
						using (IEnumerator<string> enumerator2 = keyValuePair.Value.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								string text = enumerator2.Current;
								message.Append(keyValuePair.Key + ": " + text + "\r\n");
							}
							continue;
						}
					}
					if (HttpMessageContent._spaceSeparatedValueHeaderFields.Contains(keyValuePair.Key))
					{
						message.Append(keyValuePair.Key + ": " + string.Join(" ", keyValuePair.Value) + "\r\n");
					}
					else
					{
						message.Append(keyValuePair.Key + ": " + string.Join(", ", keyValuePair.Value) + "\r\n");
					}
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004230 File Offset: 0x00002430
		private byte[] SerializeHeader()
		{
			StringBuilder stringBuilder = new StringBuilder(2048);
			HttpHeaders httpHeaders;
			HttpContent httpContent;
			if (this.HttpRequestMessage != null)
			{
				HttpMessageContent.SerializeRequestLine(stringBuilder, this.HttpRequestMessage);
				httpHeaders = this.HttpRequestMessage.Headers;
				httpContent = this.HttpRequestMessage.Content;
			}
			else
			{
				HttpMessageContent.SerializeStatusLine(stringBuilder, this.HttpResponseMessage);
				httpHeaders = this.HttpResponseMessage.Headers;
				httpContent = this.HttpResponseMessage.Content;
			}
			HttpMessageContent.SerializeHeaderFields(stringBuilder, httpHeaders);
			if (httpContent != null)
			{
				HttpMessageContent.SerializeHeaderFields(stringBuilder, httpContent.Headers);
			}
			stringBuilder.Append("\r\n");
			return Encoding.UTF8.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000042D0 File Offset: 0x000024D0
		private void ValidateStreamForReading(Stream stream)
		{
			if (this._contentConsumed)
			{
				if (stream == null || !stream.CanRead)
				{
					throw Error.InvalidOperation(Resources.HttpMessageContentAlreadyRead, new object[]
					{
						FormattingUtilities.HttpContentType.Name,
						(this.HttpRequestMessage != null) ? FormattingUtilities.HttpRequestMessageType.Name : FormattingUtilities.HttpResponseMessageType.Name
					});
				}
				stream.Position = 0L;
			}
			this._contentConsumed = true;
		}

		// Token: 0x04000036 RID: 54
		private const string SP = " ";

		// Token: 0x04000037 RID: 55
		private const string ColonSP = ": ";

		// Token: 0x04000038 RID: 56
		private const string CRLF = "\r\n";

		// Token: 0x04000039 RID: 57
		private const string CommaSeparator = ", ";

		// Token: 0x0400003A RID: 58
		private const int DefaultHeaderAllocation = 2048;

		// Token: 0x0400003B RID: 59
		private const string DefaultMediaType = "application/http";

		// Token: 0x0400003C RID: 60
		private const string MsgTypeParameter = "msgtype";

		// Token: 0x0400003D RID: 61
		private const string DefaultRequestMsgType = "request";

		// Token: 0x0400003E RID: 62
		private const string DefaultResponseMsgType = "response";

		// Token: 0x0400003F RID: 63
		private const string DefaultRequestMediaType = "application/http; msgtype=request";

		// Token: 0x04000040 RID: 64
		private const string DefaultResponseMediaType = "application/http; msgtype=response";

		// Token: 0x04000041 RID: 65
		private static readonly HashSet<string> _singleValueHeaderFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Cookie", "Set-Cookie", "X-Powered-By" };

		// Token: 0x04000042 RID: 66
		private static readonly HashSet<string> _spaceSeparatedValueHeaderFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "User-Agent" };

		// Token: 0x04000043 RID: 67
		private bool _contentConsumed;

		// Token: 0x04000044 RID: 68
		private Lazy<Task<Stream>> _streamTask;
	}
}
