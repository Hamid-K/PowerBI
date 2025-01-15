using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000018 RID: 24
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentMessageExtensions
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000353C File Offset: 0x0000173C
		public static bool IsHttpRequestMessageContent(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			bool flag;
			try
			{
				flag = HttpMessageContent.ValidateHttpMessageContent(content, true, false);
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000357C File Offset: 0x0000177C
		public static bool IsHttpResponseMessageContent(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			bool flag;
			try
			{
				flag = HttpMessageContent.ValidateHttpMessageContent(content, false, false);
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000035BC File Offset: 0x000017BC
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content)
		{
			return content.ReadAsHttpRequestMessageAsync("http", 32768);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000035CE File Offset: 0x000017CE
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpRequestMessageAsync("http", 32768, cancellationToken);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000035E1 File Offset: 0x000017E1
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, 32768);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000035EF File Offset: 0x000017EF
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, 32768, cancellationToken);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000035FE File Offset: 0x000017FE
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, bufferSize, 16384);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000360D File Offset: 0x0000180D
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, bufferSize, 16384, cancellationToken);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000361D File Offset: 0x0000181D
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize, int maxHeaderSize)
		{
			return content.ReadAsHttpRequestMessageAsync(uriScheme, bufferSize, maxHeaderSize, CancellationToken.None);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003630 File Offset: 0x00001830
		public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, string uriScheme, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (uriScheme == null)
			{
				throw Error.ArgumentNull("uriScheme");
			}
			if (!Uri.CheckSchemeName(uriScheme))
			{
				throw Error.Argument("uriScheme", Resources.HttpMessageParserInvalidUriScheme, new object[]
				{
					uriScheme,
					typeof(Uri).Name
				});
			}
			if (bufferSize < 256)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 256);
			}
			if (maxHeaderSize < 2)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxHeaderSize", maxHeaderSize, 2);
			}
			HttpMessageContent.ValidateHttpMessageContent(content, true, true);
			return content.ReadAsHttpRequestMessageAsyncCore(uriScheme, bufferSize, maxHeaderSize, cancellationToken);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000036E0 File Offset: 0x000018E0
		private static async Task<HttpRequestMessage> ReadAsHttpRequestMessageAsyncCore(this HttpContent content, string uriScheme, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stream stream2 = await content.ReadAsStreamAsync();
			Stream stream = stream2;
			HttpUnsortedRequest httpRequest = new HttpUnsortedRequest();
			HttpRequestHeaderParser parser = new HttpRequestHeaderParser(httpRequest, 2048, maxHeaderSize);
			byte[] buffer = new byte[bufferSize];
			int num = 0;
			int headerConsumed = 0;
			for (;;)
			{
				try
				{
					num = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
				}
				catch (Exception ex)
				{
					throw new IOException(Resources.HttpMessageErrorReading, ex);
				}
				ParserState parserState;
				try
				{
					parserState = parser.ParseBuffer(buffer, num, ref headerConsumed);
				}
				catch (Exception)
				{
					parserState = ParserState.Invalid;
				}
				if (parserState == ParserState.Done)
				{
					break;
				}
				if (parserState != ParserState.NeedMoreData)
				{
					goto Block_4;
				}
				if (num == 0)
				{
					goto Block_5;
				}
			}
			return HttpContentMessageExtensions.CreateHttpRequestMessage(uriScheme, httpRequest, stream, num - headerConsumed);
			Block_4:
			throw Error.InvalidOperation(Resources.HttpMessageParserError, new object[] { headerConsumed, buffer });
			Block_5:
			throw new IOException(Resources.ReadAsHttpMessageUnexpectedTermination);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003746 File Offset: 0x00001946
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content)
		{
			return content.ReadAsHttpResponseMessageAsync(32768);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003753 File Offset: 0x00001953
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpResponseMessageAsync(32768, cancellationToken);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003761 File Offset: 0x00001961
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize)
		{
			return content.ReadAsHttpResponseMessageAsync(bufferSize, 16384);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000376F File Offset: 0x0000196F
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize, CancellationToken cancellationToken)
		{
			return content.ReadAsHttpResponseMessageAsync(bufferSize, 16384, cancellationToken);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000377E File Offset: 0x0000197E
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize, int maxHeaderSize)
		{
			return content.ReadAsHttpResponseMessageAsync(bufferSize, maxHeaderSize, CancellationToken.None);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003790 File Offset: 0x00001990
		public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (bufferSize < 256)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 256);
			}
			if (maxHeaderSize < 2)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxHeaderSize", maxHeaderSize, 2);
			}
			HttpMessageContent.ValidateHttpMessageContent(content, false, true);
			return content.ReadAsHttpResponseMessageAsyncCore(bufferSize, maxHeaderSize, cancellationToken);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000037FC File Offset: 0x000019FC
		private static async Task<HttpResponseMessage> ReadAsHttpResponseMessageAsyncCore(this HttpContent content, int bufferSize, int maxHeaderSize, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stream stream2 = await content.ReadAsStreamAsync();
			Stream stream = stream2;
			HttpUnsortedResponse httpResponse = new HttpUnsortedResponse();
			HttpResponseHeaderParser parser = new HttpResponseHeaderParser(httpResponse, 2048, maxHeaderSize);
			byte[] buffer = new byte[bufferSize];
			int num = 0;
			int headerConsumed = 0;
			for (;;)
			{
				try
				{
					num = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
				}
				catch (Exception ex)
				{
					throw new IOException(Resources.HttpMessageErrorReading, ex);
				}
				ParserState parserState;
				try
				{
					parserState = parser.ParseBuffer(buffer, num, ref headerConsumed);
				}
				catch (Exception)
				{
					parserState = ParserState.Invalid;
				}
				if (parserState == ParserState.Done)
				{
					break;
				}
				if (parserState != ParserState.NeedMoreData)
				{
					goto Block_4;
				}
				if (num == 0)
				{
					goto Block_5;
				}
			}
			return HttpContentMessageExtensions.CreateHttpResponseMessage(httpResponse, stream, num - headerConsumed);
			Block_4:
			throw Error.InvalidOperation(Resources.HttpMessageParserError, new object[] { headerConsumed, buffer });
			Block_5:
			throw new IOException(Resources.ReadAsHttpMessageUnexpectedTermination);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000385C File Offset: 0x00001A5C
		private static Uri CreateRequestUri(string uriScheme, HttpUnsortedRequest httpRequest)
		{
			IEnumerable<string> enumerable;
			if (!httpRequest.HttpHeaders.TryGetValues("Host", out enumerable))
			{
				throw Error.InvalidOperation(Resources.HttpMessageParserInvalidHostCount, new object[] { "Host", 0 });
			}
			int num = enumerable.Count<string>();
			if (num != 1)
			{
				throw Error.InvalidOperation(Resources.HttpMessageParserInvalidHostCount, new object[] { "Host", num });
			}
			return new Uri(string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[]
			{
				uriScheme,
				enumerable.ElementAt(0),
				httpRequest.RequestUri
			}));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003900 File Offset: 0x00001B00
		private static HttpContent CreateHeaderFields(HttpHeaders source, HttpHeaders destination, Stream contentStream, int rewind)
		{
			HttpContentHeaders httpContentHeaders = null;
			HttpContent httpContent = null;
			foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in source)
			{
				if (!destination.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value))
				{
					if (httpContentHeaders == null)
					{
						httpContentHeaders = FormattingUtilities.CreateEmptyContentHeaders();
					}
					httpContentHeaders.TryAddWithoutValidation(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (httpContentHeaders != null)
			{
				if (!contentStream.CanSeek)
				{
					throw Error.InvalidOperation(Resources.HttpMessageContentStreamMustBeSeekable, new object[]
					{
						"ContentReadStream",
						FormattingUtilities.HttpResponseMessageType.Name
					});
				}
				contentStream.Seek((long)(0 - rewind), SeekOrigin.Current);
				httpContent = new StreamContent(contentStream);
				httpContentHeaders.CopyTo(httpContent.Headers);
			}
			return httpContent;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000039CC File Offset: 0x00001BCC
		private static HttpRequestMessage CreateHttpRequestMessage(string uriScheme, HttpUnsortedRequest httpRequest, Stream contentStream, int rewind)
		{
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
			httpRequestMessage.Method = httpRequest.Method;
			httpRequestMessage.RequestUri = HttpContentMessageExtensions.CreateRequestUri(uriScheme, httpRequest);
			httpRequestMessage.Version = httpRequest.Version;
			httpRequestMessage.Content = HttpContentMessageExtensions.CreateHeaderFields(httpRequest.HttpHeaders, httpRequestMessage.Headers, contentStream, rewind);
			return httpRequestMessage;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A20 File Offset: 0x00001C20
		private static HttpResponseMessage CreateHttpResponseMessage(HttpUnsortedResponse httpResponse, Stream contentStream, int rewind)
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
			httpResponseMessage.Version = httpResponse.Version;
			httpResponseMessage.StatusCode = httpResponse.StatusCode;
			httpResponseMessage.ReasonPhrase = httpResponse.ReasonPhrase;
			httpResponseMessage.Content = HttpContentMessageExtensions.CreateHeaderFields(httpResponse.HttpHeaders, httpResponseMessage.Headers, contentStream, rewind);
			return httpResponseMessage;
		}

		// Token: 0x04000032 RID: 50
		private const int MinBufferSize = 256;

		// Token: 0x04000033 RID: 51
		private const int DefaultBufferSize = 32768;
	}
}
