using System;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000053 RID: 83
	internal class HttpRequestLineParser
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000AD10 File Offset: 0x00008F10
		public HttpRequestLineParser(HttpUnsortedRequest httpRequest, int maxRequestLineSize)
		{
			if (maxRequestLineSize < 14)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxRequestLineSize", maxRequestLineSize, 14);
			}
			if (httpRequest == null)
			{
				throw Error.ArgumentNull("httpRequest");
			}
			this._httpRequest = httpRequest;
			this._maximumHeaderLength = maxRequestLineSize;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AD6C File Offset: 0x00008F6C
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState parserState = ParserState.NeedMoreData;
			if (bytesConsumed >= bytesReady)
			{
				return parserState;
			}
			try
			{
				parserState = HttpRequestLineParser.ParseRequestLine(buffer, bytesReady, ref bytesConsumed, ref this._requestLineState, this._maximumHeaderLength, ref this._totalBytesConsumed, this._currentToken, this._httpRequest);
			}
			catch (Exception)
			{
				parserState = ParserState.Invalid;
			}
			return parserState;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		private static ParserState ParseRequestLine(byte[] buffer, int bytesReady, ref int bytesConsumed, ref HttpRequestLineParser.HttpRequestLineState requestLineState, int maximumHeaderLength, ref int totalBytesConsumed, StringBuilder currentToken, HttpUnsortedRequest httpRequest)
		{
			int num = bytesConsumed;
			ParserState parserState = ParserState.DataTooBig;
			int num2 = ((maximumHeaderLength <= 0) ? int.MaxValue : (maximumHeaderLength - totalBytesConsumed + bytesConsumed));
			if (bytesReady < num2)
			{
				parserState = ParserState.NeedMoreData;
				num2 = bytesReady;
			}
			int num3;
			int num4;
			switch (requestLineState)
			{
			case HttpRequestLineParser.HttpRequestLineState.RequestMethod:
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 32)
				{
					if (buffer[bytesConsumed] < 33 || buffer[bytesConsumed] > 122)
					{
						parserState = ParserState.Invalid;
						goto IL_03AF;
					}
					num4 = bytesConsumed + 1;
					bytesConsumed = num4;
					if (num4 == num2)
					{
						string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
						currentToken.Append(@string);
						goto IL_03AF;
					}
				}
				if (bytesConsumed > num3)
				{
					string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string2);
				}
				httpRequest.Method = new HttpMethod(currentToken.ToString());
				currentToken.Clear();
				requestLineState = HttpRequestLineParser.HttpRequestLineState.RequestUri;
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					goto IL_03AF;
				}
				break;
			case HttpRequestLineParser.HttpRequestLineState.RequestUri:
				break;
			case HttpRequestLineParser.HttpRequestLineState.BeforeVersionNumbers:
				goto IL_019C;
			case HttpRequestLineParser.HttpRequestLineState.MajorVersionNumber:
				goto IL_0268;
			case HttpRequestLineParser.HttpRequestLineState.MinorVersionNumber:
				goto IL_02FC;
			case HttpRequestLineParser.HttpRequestLineState.AfterCarriageReturn:
				goto IL_039B;
			default:
				goto IL_03AF;
			}
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 32)
			{
				if (buffer[bytesConsumed] == 13)
				{
					parserState = ParserState.Invalid;
					goto IL_03AF;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string3);
					goto IL_03AF;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string4);
			}
			if (currentToken.Length == 0)
			{
				throw new FormatException(Resources.HttpMessageParserEmptyUri);
			}
			httpRequest.RequestUri = currentToken.ToString();
			currentToken.Clear();
			requestLineState = HttpRequestLineParser.HttpRequestLineState.BeforeVersionNumbers;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03AF;
			}
			IL_019C:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 47)
			{
				if (buffer[bytesConsumed] < 33 || buffer[bytesConsumed] > 122)
				{
					parserState = ParserState.Invalid;
					goto IL_03AF;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string5 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string5);
					goto IL_03AF;
				}
			}
			if (bytesConsumed > num3)
			{
				string string6 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string6);
			}
			string text = currentToken.ToString();
			if (string.CompareOrdinal("HTTP", text) != 0)
			{
				throw new FormatException(Error.Format(Resources.HttpInvalidVersion, new object[] { text, "HTTP" }));
			}
			currentToken.Clear();
			requestLineState = HttpRequestLineParser.HttpRequestLineState.MajorVersionNumber;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03AF;
			}
			IL_0268:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 46)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					parserState = ParserState.Invalid;
					goto IL_03AF;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string7 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string7);
					goto IL_03AF;
				}
			}
			if (bytesConsumed > num3)
			{
				string string8 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string8);
			}
			currentToken.Append('.');
			requestLineState = HttpRequestLineParser.HttpRequestLineState.MinorVersionNumber;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03AF;
			}
			IL_02FC:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					parserState = ParserState.Invalid;
					goto IL_03AF;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string9 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string9);
					goto IL_03AF;
				}
			}
			if (bytesConsumed > num3)
			{
				string string10 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string10);
			}
			httpRequest.Version = Version.Parse(currentToken.ToString());
			currentToken.Clear();
			requestLineState = HttpRequestLineParser.HttpRequestLineState.AfterCarriageReturn;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03AF;
			}
			IL_039B:
			if (buffer[bytesConsumed] != 10)
			{
				parserState = ParserState.Invalid;
			}
			else
			{
				parserState = ParserState.Done;
				bytesConsumed++;
			}
			IL_03AF:
			totalBytesConsumed += bytesConsumed - num;
			return parserState;
		}

		// Token: 0x040000F9 RID: 249
		internal const int MinRequestLineSize = 14;

		// Token: 0x040000FA RID: 250
		private const int DefaultTokenAllocation = 2048;

		// Token: 0x040000FB RID: 251
		private int _totalBytesConsumed;

		// Token: 0x040000FC RID: 252
		private int _maximumHeaderLength;

		// Token: 0x040000FD RID: 253
		private HttpRequestLineParser.HttpRequestLineState _requestLineState;

		// Token: 0x040000FE RID: 254
		private HttpUnsortedRequest _httpRequest;

		// Token: 0x040000FF RID: 255
		private StringBuilder _currentToken = new StringBuilder(2048);

		// Token: 0x02000084 RID: 132
		private enum HttpRequestLineState
		{
			// Token: 0x040001D5 RID: 469
			RequestMethod,
			// Token: 0x040001D6 RID: 470
			RequestUri,
			// Token: 0x040001D7 RID: 471
			BeforeVersionNumbers,
			// Token: 0x040001D8 RID: 472
			MajorVersionNumber,
			// Token: 0x040001D9 RID: 473
			MinorVersionNumber,
			// Token: 0x040001DA RID: 474
			AfterCarriageReturn
		}
	}
}
