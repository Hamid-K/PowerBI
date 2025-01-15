using System;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000054 RID: 84
	internal class HttpResponseHeaderParser
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000B198 File Offset: 0x00009398
		public HttpResponseHeaderParser(HttpUnsortedResponse httpResponse)
			: this(httpResponse, 2048, 16384)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B1AC File Offset: 0x000093AC
		public HttpResponseHeaderParser(HttpUnsortedResponse httpResponse, int maxResponseLineSize, int maxHeaderSize)
		{
			if (httpResponse == null)
			{
				throw Error.ArgumentNull("httpResponse");
			}
			this._httpResponse = httpResponse;
			this._statusLineParser = new HttpStatusLineParser(this._httpResponse, maxResponseLineSize);
			this._headerParser = new InternetMessageFormatHeaderParser(this._httpResponse.HttpHeaders, maxHeaderSize);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B200 File Offset: 0x00009400
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState parserState = ParserState.NeedMoreData;
			ParserState parserState2 = ParserState.NeedMoreData;
			HttpResponseHeaderParser.HttpResponseState responseStatus = this._responseStatus;
			if (responseStatus != HttpResponseHeaderParser.HttpResponseState.StatusLine)
			{
				if (responseStatus != HttpResponseHeaderParser.HttpResponseState.ResponseHeaders)
				{
					return parserState;
				}
			}
			else
			{
				try
				{
					parserState2 = this._statusLineParser.ParseBuffer(buffer, bytesReady, ref bytesConsumed);
				}
				catch (Exception)
				{
					parserState2 = ParserState.Invalid;
				}
				if (parserState2 == ParserState.Done)
				{
					this._responseStatus = HttpResponseHeaderParser.HttpResponseState.ResponseHeaders;
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

		// Token: 0x04000100 RID: 256
		internal const int DefaultMaxStatusLineSize = 2048;

		// Token: 0x04000101 RID: 257
		internal const int DefaultMaxHeaderSize = 16384;

		// Token: 0x04000102 RID: 258
		private HttpUnsortedResponse _httpResponse;

		// Token: 0x04000103 RID: 259
		private HttpResponseHeaderParser.HttpResponseState _responseStatus;

		// Token: 0x04000104 RID: 260
		private HttpStatusLineParser _statusLineParser;

		// Token: 0x04000105 RID: 261
		private InternetMessageFormatHeaderParser _headerParser;

		// Token: 0x02000085 RID: 133
		private enum HttpResponseState
		{
			// Token: 0x040001DC RID: 476
			StatusLine,
			// Token: 0x040001DD RID: 477
			ResponseHeaders
		}
	}
}
