using System;
using System.Globalization;
using System.Net.Http.Properties;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000055 RID: 85
	internal class HttpStatusLineParser
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000B2A4 File Offset: 0x000094A4
		public HttpStatusLineParser(HttpUnsortedResponse httpResponse, int maxStatusLineSize)
		{
			if (maxStatusLineSize < 15)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxStatusLineSize", maxStatusLineSize, 15);
			}
			if (httpResponse == null)
			{
				throw Error.ArgumentNull("httpResponse");
			}
			this._httpResponse = httpResponse;
			this._maximumHeaderLength = maxStatusLineSize;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000B300 File Offset: 0x00009500
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
				parserState = HttpStatusLineParser.ParseStatusLine(buffer, bytesReady, ref bytesConsumed, ref this._statusLineState, this._maximumHeaderLength, ref this._totalBytesConsumed, this._currentToken, this._httpResponse);
			}
			catch (Exception)
			{
				parserState = ParserState.Invalid;
			}
			return parserState;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000B364 File Offset: 0x00009564
		private static ParserState ParseStatusLine(byte[] buffer, int bytesReady, ref int bytesConsumed, ref HttpStatusLineParser.HttpStatusLineState statusLineState, int maximumHeaderLength, ref int totalBytesConsumed, StringBuilder currentToken, HttpUnsortedResponse httpResponse)
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
			switch (statusLineState)
			{
			case HttpStatusLineParser.HttpStatusLineState.BeforeVersionNumbers:
			{
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 47)
				{
					if (buffer[bytesConsumed] < 33 || buffer[bytesConsumed] > 122)
					{
						parserState = ParserState.Invalid;
						goto IL_03F2;
					}
					num4 = bytesConsumed + 1;
					bytesConsumed = num4;
					if (num4 == num2)
					{
						string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
						currentToken.Append(@string);
						goto IL_03F2;
					}
				}
				if (bytesConsumed > num3)
				{
					string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string2);
				}
				string text = currentToken.ToString();
				if (string.CompareOrdinal("HTTP", text) != 0)
				{
					throw new FormatException(Error.Format(Resources.HttpInvalidVersion, new object[] { text, "HTTP" }));
				}
				currentToken.Clear();
				statusLineState = HttpStatusLineParser.HttpStatusLineState.MajorVersionNumber;
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					goto IL_03F2;
				}
				break;
			}
			case HttpStatusLineParser.HttpStatusLineState.MajorVersionNumber:
				break;
			case HttpStatusLineParser.HttpStatusLineState.MinorVersionNumber:
				goto IL_01AB;
			case HttpStatusLineParser.HttpStatusLineState.StatusCode:
				goto IL_0250;
			case HttpStatusLineParser.HttpStatusLineState.ReasonPhrase:
				goto IL_0344;
			case HttpStatusLineParser.HttpStatusLineState.AfterCarriageReturn:
				goto IL_03DE;
			default:
				goto IL_03F2;
			}
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 46)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					parserState = ParserState.Invalid;
					goto IL_03F2;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string3);
					goto IL_03F2;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string4);
			}
			currentToken.Append('.');
			statusLineState = HttpStatusLineParser.HttpStatusLineState.MinorVersionNumber;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03F2;
			}
			IL_01AB:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 32)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					parserState = ParserState.Invalid;
					goto IL_03F2;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string5 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string5);
					goto IL_03F2;
				}
			}
			if (bytesConsumed > num3)
			{
				string string6 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string6);
			}
			httpResponse.Version = Version.Parse(currentToken.ToString());
			currentToken.Clear();
			statusLineState = HttpStatusLineParser.HttpStatusLineState.StatusCode;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03F2;
			}
			IL_0250:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 32)
			{
				if (buffer[bytesConsumed] < 48 || buffer[bytesConsumed] > 57)
				{
					parserState = ParserState.Invalid;
					goto IL_03F2;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string7 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string7);
					goto IL_03F2;
				}
			}
			if (bytesConsumed > num3)
			{
				string string8 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string8);
			}
			int num5 = int.Parse(currentToken.ToString(), CultureInfo.InvariantCulture);
			if (num5 < 100 || num5 > 1000)
			{
				throw new FormatException(Error.Format(Resources.HttpInvalidStatusCode, new object[] { num5, 100, 1000 }));
			}
			httpResponse.StatusCode = (HttpStatusCode)num5;
			currentToken.Clear();
			statusLineState = HttpStatusLineParser.HttpStatusLineState.ReasonPhrase;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03F2;
			}
			IL_0344:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				if (buffer[bytesConsumed] < 32 || buffer[bytesConsumed] > 122)
				{
					parserState = ParserState.Invalid;
					goto IL_03F2;
				}
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if (num4 == num2)
				{
					string string9 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentToken.Append(string9);
					goto IL_03F2;
				}
			}
			if (bytesConsumed > num3)
			{
				string string10 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentToken.Append(string10);
			}
			httpResponse.ReasonPhrase = currentToken.ToString();
			currentToken.Clear();
			statusLineState = HttpStatusLineParser.HttpStatusLineState.AfterCarriageReturn;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if (num4 == num2)
			{
				goto IL_03F2;
			}
			IL_03DE:
			if (buffer[bytesConsumed] != 10)
			{
				parserState = ParserState.Invalid;
			}
			else
			{
				parserState = ParserState.Done;
				bytesConsumed++;
			}
			IL_03F2:
			totalBytesConsumed += bytesConsumed - num;
			return parserState;
		}

		// Token: 0x04000106 RID: 262
		internal const int MinStatusLineSize = 15;

		// Token: 0x04000107 RID: 263
		private const int DefaultTokenAllocation = 2048;

		// Token: 0x04000108 RID: 264
		private const int MaxStatusCode = 1000;

		// Token: 0x04000109 RID: 265
		private int _totalBytesConsumed;

		// Token: 0x0400010A RID: 266
		private int _maximumHeaderLength;

		// Token: 0x0400010B RID: 267
		private HttpStatusLineParser.HttpStatusLineState _statusLineState;

		// Token: 0x0400010C RID: 268
		private HttpUnsortedResponse _httpResponse;

		// Token: 0x0400010D RID: 269
		private StringBuilder _currentToken = new StringBuilder(2048);

		// Token: 0x02000086 RID: 134
		private enum HttpStatusLineState
		{
			// Token: 0x040001DF RID: 479
			BeforeVersionNumbers,
			// Token: 0x040001E0 RID: 480
			MajorVersionNumber,
			// Token: 0x040001E1 RID: 481
			MinorVersionNumber,
			// Token: 0x040001E2 RID: 482
			StatusCode,
			// Token: 0x040001E3 RID: 483
			ReasonPhrase,
			// Token: 0x040001E4 RID: 484
			AfterCarriageReturn
		}
	}
}
