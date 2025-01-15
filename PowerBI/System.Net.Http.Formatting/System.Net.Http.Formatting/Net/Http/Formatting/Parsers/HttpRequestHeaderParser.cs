using System;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000052 RID: 82
	internal class HttpRequestHeaderParser
	{
		// Token: 0x0600032A RID: 810 RVA: 0x0000AC02 File Offset: 0x00008E02
		public HttpRequestHeaderParser(HttpUnsortedRequest httpRequest)
			: this(httpRequest, 2048, 16384)
		{
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000AC18 File Offset: 0x00008E18
		public HttpRequestHeaderParser(HttpUnsortedRequest httpRequest, int maxRequestLineSize, int maxHeaderSize)
		{
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this._httpRequest = httpRequest;
			this._requestLineParser = new HttpRequestLineParser(this._httpRequest, maxRequestLineSize);
			this._headerParser = new InternetMessageFormatHeaderParser(this._httpRequest.HttpHeaders, maxHeaderSize);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState parserState = ParserState.NeedMoreData;
			ParserState parserState2 = ParserState.NeedMoreData;
			HttpRequestHeaderParser.HttpRequestState requestStatus = this._requestStatus;
			if (requestStatus != HttpRequestHeaderParser.HttpRequestState.RequestLine)
			{
				if (requestStatus != HttpRequestHeaderParser.HttpRequestState.RequestHeaders)
				{
					return parserState;
				}
			}
			else
			{
				try
				{
					parserState2 = this._requestLineParser.ParseBuffer(buffer, bytesReady, ref bytesConsumed);
				}
				catch (Exception)
				{
					parserState2 = ParserState.Invalid;
				}
				if (parserState2 == ParserState.Done)
				{
					this._requestStatus = HttpRequestHeaderParser.HttpRequestState.RequestHeaders;
					parserState2 = ParserState.NeedMoreData;
				}
				else
				{
					if (parserState2 != ParserState.NeedMoreData)
					{
						return parserState2;
					}
					return parserState;
				}
			}
			if (bytesConsumed < bytesReady)
			{
				try
				{
					parserState2 = this._headerParser.ParseBuffer(buffer, bytesReady, ref bytesConsumed);
				}
				catch (Exception)
				{
					parserState2 = ParserState.Invalid;
				}
				if (parserState2 == ParserState.Done)
				{
					parserState = parserState2;
				}
				else if (parserState2 != ParserState.NeedMoreData)
				{
					parserState = parserState2;
				}
			}
			return parserState;
		}

		// Token: 0x040000F3 RID: 243
		internal const int DefaultMaxRequestLineSize = 2048;

		// Token: 0x040000F4 RID: 244
		internal const int DefaultMaxHeaderSize = 16384;

		// Token: 0x040000F5 RID: 245
		private HttpUnsortedRequest _httpRequest;

		// Token: 0x040000F6 RID: 246
		private HttpRequestHeaderParser.HttpRequestState _requestStatus;

		// Token: 0x040000F7 RID: 247
		private HttpRequestLineParser _requestLineParser;

		// Token: 0x040000F8 RID: 248
		private InternetMessageFormatHeaderParser _headerParser;

		// Token: 0x02000083 RID: 131
		private enum HttpRequestState
		{
			// Token: 0x040001D2 RID: 466
			RequestLine,
			// Token: 0x040001D3 RID: 467
			RequestHeaders
		}
	}
}
