using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Core
{
	// Token: 0x0200009E RID: 158
	internal static class HttpUtils
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00015400 File Offset: 0x00013600
		internal static IList<KeyValuePair<string, string>> ReadMimeType(string contentType, out string mediaTypeName, out string mediaTypeCharset)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				throw new ODataContentTypeException(Strings.HttpUtils_ContentTypeMissing);
			}
			IList<KeyValuePair<ODataMediaType, string>> list = HttpUtils.ReadMediaTypes(contentType);
			if (list.Count != 1)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_NoOrMoreThanOneContentTypeSpecified(contentType));
			}
			ODataMediaType key = list[0].Key;
			MediaTypeUtils.CheckMediaTypeForWildCards(key);
			mediaTypeName = key.FullTypeName;
			mediaTypeCharset = list[0].Value;
			if (key.Parameters == null)
			{
				return null;
			}
			return Enumerable.ToList<KeyValuePair<string, string>>(key.Parameters);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00015480 File Offset: 0x00013680
		internal static string BuildContentType(ODataMediaType mediaType, Encoding encoding)
		{
			return mediaType.ToText(encoding);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00015489 File Offset: 0x00013689
		internal static IList<KeyValuePair<ODataMediaType, string>> MediaTypesFromString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return HttpUtils.ReadMediaTypes(text);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001549B File Offset: 0x0001369B
		internal static bool CompareMediaTypeNames(string mediaTypeName1, string mediaTypeName2)
		{
			return string.Equals(mediaTypeName1, mediaTypeName2, 5);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000154A5 File Offset: 0x000136A5
		internal static bool CompareMediaTypeParameterNames(string parameterName1, string parameterName2)
		{
			return string.Equals(parameterName1, parameterName2, 5);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000154C0 File Offset: 0x000136C0
		internal static Encoding EncodingFromAcceptableCharsets(string acceptableCharsets, ODataMediaType mediaType, Encoding utf8Encoding, Encoding defaultEncoding)
		{
			Encoding encoding = null;
			if (!string.IsNullOrEmpty(acceptableCharsets))
			{
				HttpUtils.CharsetPart[] array = new List<HttpUtils.CharsetPart>(HttpUtils.AcceptCharsetParts(acceptableCharsets)).ToArray();
				KeyValuePair<int, HttpUtils.CharsetPart>[] array2 = array.StableSort((HttpUtils.CharsetPart x, HttpUtils.CharsetPart y) => y.Quality - x.Quality);
				foreach (KeyValuePair<int, HttpUtils.CharsetPart> keyValuePair in array2)
				{
					HttpUtils.CharsetPart value = keyValuePair.Value;
					if (value.Quality > 0)
					{
						if (string.Compare("utf-8", value.Charset, 5) == 0)
						{
							encoding = utf8Encoding;
							break;
						}
						encoding = HttpUtils.GetEncodingFromCharsetName(value.Charset);
						if (encoding != null)
						{
							break;
						}
					}
				}
			}
			if (encoding == null)
			{
				encoding = mediaType.SelectEncoding();
				if (encoding == null)
				{
					return defaultEncoding;
				}
			}
			return encoding;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00015580 File Offset: 0x00013780
		internal static void ReadQualityValue(string text, ref int textIndex, out int qualityValue)
		{
			char c = text.get_Chars(textIndex++);
			switch (c)
			{
			case '0':
				qualityValue = 0;
				break;
			case '1':
				qualityValue = 1;
				break;
			default:
				throw new ODataContentTypeException(Strings.HttpUtils_InvalidQualityValueStartChar(text, c));
			}
			if (textIndex < text.Length && text.get_Chars(textIndex) == '.')
			{
				textIndex++;
				int num = 1000;
				while (num > 1 && textIndex < text.Length)
				{
					char c2 = text.get_Chars(textIndex);
					int num2 = HttpUtils.DigitToInt32(c2);
					if (num2 < 0)
					{
						break;
					}
					textIndex++;
					num /= 10;
					qualityValue *= 10;
					qualityValue += num2;
				}
				qualityValue *= num;
				if (qualityValue > 1000)
				{
					throw new ODataContentTypeException(Strings.HttpUtils_InvalidQualityValue(qualityValue / 1000, text));
				}
			}
			else
			{
				qualityValue *= 1000;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00015664 File Offset: 0x00013864
		internal static void ValidateHttpMethod(string httpMethodString)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(httpMethodString, "httpMethodString");
			if (string.CompareOrdinal(httpMethodString, "GET") != 0 && string.CompareOrdinal(httpMethodString, "DELETE") != 0 && string.CompareOrdinal(httpMethodString, "PATCH") != 0 && string.CompareOrdinal(httpMethodString, "POST") != 0 && string.CompareOrdinal(httpMethodString, "PUT") != 0)
			{
				throw new ODataException(Strings.HttpUtils_InvalidHttpMethodString(httpMethodString));
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000156C9 File Offset: 0x000138C9
		internal static bool IsQueryMethod(string httpMethod)
		{
			return string.CompareOrdinal(httpMethod, "GET") == 0;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000156DC File Offset: 0x000138DC
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is a large switch on all the Http response codes; no complexity here.")]
		internal static string GetStatusMessage(int statusCode)
		{
			if (statusCode <= 206)
			{
				switch (statusCode)
				{
				case 100:
					return "Continue";
				case 101:
					return "Switching Protocols";
				default:
					switch (statusCode)
					{
					case 200:
						return "OK";
					case 201:
						return "Created";
					case 202:
						return "Accepted";
					case 203:
						return "Non-Authoritative Information";
					case 204:
						return "No Content";
					case 205:
						return "Reset Content";
					case 206:
						return "Partial Content";
					}
					break;
				}
			}
			else
			{
				switch (statusCode)
				{
				case 300:
					return "Multiple Choices";
				case 301:
					return "Moved Permanently";
				case 302:
					return "Found";
				case 303:
					return "See Other";
				case 304:
					return "Not Modified";
				case 305:
					return "Use Proxy";
				case 306:
					break;
				case 307:
					return "Temporary Redirect";
				default:
					switch (statusCode)
					{
					case 400:
						return "Bad Request";
					case 401:
						return "Unauthorized";
					case 402:
						return "Payment Required";
					case 403:
						return "Forbidden";
					case 404:
						return "Not Found";
					case 405:
						return "Method Not Allowed";
					case 406:
						return "Not Acceptable";
					case 407:
						return "Proxy Authentication Required";
					case 408:
						return "Request Time-out";
					case 409:
						return "Conflict";
					case 410:
						return "Gone";
					case 411:
						return "Length Required";
					case 412:
						return "Precondition Failed";
					case 413:
						return "Request Entity Too Large";
					case 414:
						return "Request-URI Too Large";
					case 415:
						return "Unsupported Media Type";
					case 416:
						return "Requested range not satisfiable";
					case 417:
						return "Expectation Failed";
					default:
						switch (statusCode)
						{
						case 500:
							return "Internal Server Error";
						case 501:
							return "Not Implemented";
						case 502:
							return "Bad Gateway";
						case 503:
							return "Service Unavailable";
						case 504:
							return "Gateway Time-out";
						case 505:
							return "HTTP Version not supported";
						}
						break;
					}
					break;
				}
			}
			return "Unknown Status Code";
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000158D0 File Offset: 0x00013AD0
		internal static Encoding GetEncodingFromCharsetName(string charsetName)
		{
			Encoding encoding;
			try
			{
				encoding = Encoding.GetEncoding(charsetName, new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
			catch (ArgumentException)
			{
				encoding = null;
			}
			return encoding;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00015908 File Offset: 0x00013B08
		internal static string ReadTokenOrQuotedStringValue(string headerName, string headerText, ref int textIndex, out bool isQuotedString, Func<string, Exception> createException)
		{
			StringBuilder stringBuilder = new StringBuilder();
			isQuotedString = false;
			if (textIndex < headerText.Length && headerText.get_Chars(textIndex) == '"')
			{
				textIndex++;
				isQuotedString = true;
			}
			char c = '\0';
			while (textIndex < headerText.Length)
			{
				c = headerText.get_Chars(textIndex);
				if (c == '\\' || c == '"')
				{
					if (!isQuotedString)
					{
						throw createException.Invoke(Strings.HttpUtils_EscapeCharWithoutQuotes(headerName, headerText, textIndex, c));
					}
					textIndex++;
					if (c == '"')
					{
						break;
					}
					if (textIndex >= headerText.Length)
					{
						throw createException.Invoke(Strings.HttpUtils_EscapeCharAtEnd(headerName, headerText, textIndex, c));
					}
					c = headerText.get_Chars(textIndex);
				}
				else
				{
					if (!isQuotedString && !HttpUtils.IsHttpToken(c))
					{
						break;
					}
					if (isQuotedString && !HttpUtils.IsValidInQuotedHeaderValue(c))
					{
						throw createException.Invoke(Strings.HttpUtils_InvalidCharacterInQuotedParameterValue(headerName, headerText, textIndex, c));
					}
				}
				stringBuilder.Append(c);
				textIndex++;
			}
			if (isQuotedString && c != '"')
			{
				throw createException.Invoke(Strings.HttpUtils_ClosingQuoteNotFound(headerName, headerText, textIndex));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00015A2A File Offset: 0x00013C2A
		internal static bool SkipWhitespace(string text, ref int textIndex)
		{
			while (textIndex < text.Length && char.IsWhiteSpace(text, textIndex))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00015D04 File Offset: 0x00013F04
		private static IEnumerable<HttpUtils.CharsetPart> AcceptCharsetParts(string headerValue)
		{
			bool commaRequired = false;
			int headerIndex = 0;
			while (headerIndex < headerValue.Length && !HttpUtils.SkipWhitespace(headerValue, ref headerIndex))
			{
				if (headerValue.get_Chars(headerIndex) == ',')
				{
					commaRequired = false;
					headerIndex++;
				}
				else
				{
					if (commaRequired)
					{
						throw new ODataContentTypeException(Strings.HttpUtils_MissingSeparatorBetweenCharsets(headerValue));
					}
					int headerStart = headerIndex;
					int headerNameEnd = headerStart;
					bool endReached = HttpUtils.ReadToken(headerValue, ref headerNameEnd);
					if (headerNameEnd == headerIndex)
					{
						throw new ODataContentTypeException(Strings.HttpUtils_InvalidCharsetName(headerValue));
					}
					int qualityValue;
					int headerEnd;
					if (endReached)
					{
						qualityValue = 1000;
						headerEnd = headerNameEnd;
					}
					else
					{
						char c = headerValue.get_Chars(headerNameEnd);
						if (!HttpUtils.IsHttpSeparator(c))
						{
							throw new ODataContentTypeException(Strings.HttpUtils_InvalidSeparatorBetweenCharsets(headerValue));
						}
						if (c == ';')
						{
							if (HttpUtils.ReadLiteral(headerValue, headerNameEnd, ";q="))
							{
								throw new ODataContentTypeException(Strings.HttpUtils_UnexpectedEndOfQValue(headerValue));
							}
							headerEnd = headerNameEnd + 3;
							HttpUtils.ReadQualityValue(headerValue, ref headerEnd, out qualityValue);
						}
						else
						{
							qualityValue = 1000;
							headerEnd = headerNameEnd;
						}
					}
					yield return new HttpUtils.CharsetPart(headerValue.Substring(headerStart, headerNameEnd - headerStart), qualityValue);
					commaRequired = true;
					headerIndex = headerEnd;
				}
			}
			yield break;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00015D24 File Offset: 0x00013F24
		private static IList<KeyValuePair<ODataMediaType, string>> ReadMediaTypes(string text)
		{
			List<KeyValuePair<string, string>> list = null;
			List<KeyValuePair<ODataMediaType, string>> list2 = new List<KeyValuePair<ODataMediaType, string>>();
			int num = 0;
			while (!HttpUtils.SkipWhitespace(text, ref num))
			{
				string text2;
				string text3;
				HttpUtils.ReadMediaTypeAndSubtype(text, ref num, out text2, out text3);
				string text4 = null;
				while (!HttpUtils.SkipWhitespace(text, ref num))
				{
					if (text.get_Chars(num) == ',')
					{
						num++;
						break;
					}
					if (text.get_Chars(num) != ';')
					{
						throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeRequiresSemicolonBeforeParameter(text));
					}
					num++;
					if (HttpUtils.SkipWhitespace(text, ref num))
					{
						break;
					}
					HttpUtils.ReadMediaTypeParameter(text, ref num, ref list, ref text4);
				}
				list2.Add(new KeyValuePair<ODataMediaType, string>(new ODataMediaType(text2, text3, list), text4));
				list = null;
			}
			return list2;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00015DC8 File Offset: 0x00013FC8
		private static void ReadMediaTypeParameter(string text, ref int textIndex, ref List<KeyValuePair<string, string>> parameters, ref string charset)
		{
			int num = textIndex;
			bool flag = HttpUtils.ReadToken(text, ref textIndex);
			string text2 = text.Substring(num, textIndex - num);
			if (text2.Length == 0)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeMissingParameterName);
			}
			if (flag)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeMissingParameterValue(text2));
			}
			if (text.get_Chars(textIndex) != '=')
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeMissingParameterValue(text2));
			}
			textIndex++;
			bool flag2;
			string text3 = HttpUtils.ReadTokenOrQuotedStringValue("Content-Type", text, ref textIndex, out flag2, (string message) => new ODataContentTypeException(message));
			if (HttpUtils.CompareMediaTypeParameterNames("charset", text2))
			{
				charset = text3;
				return;
			}
			if (parameters == null)
			{
				parameters = new List<KeyValuePair<string, string>>(1);
			}
			parameters.Add(new KeyValuePair<string, string>(text2, text3));
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00015E88 File Offset: 0x00014088
		private static void ReadMediaTypeAndSubtype(string mediaTypeName, ref int textIndex, out string type, out string subType)
		{
			int num = textIndex;
			if (HttpUtils.ReadToken(mediaTypeName, ref textIndex))
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeUnspecified(mediaTypeName));
			}
			if (mediaTypeName.get_Chars(textIndex) != '/')
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeRequiresSlash(mediaTypeName));
			}
			type = mediaTypeName.Substring(num, textIndex - num);
			textIndex++;
			int num2 = textIndex;
			HttpUtils.ReadToken(mediaTypeName, ref textIndex);
			if (textIndex == num2)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeRequiresSubType(mediaTypeName));
			}
			subType = mediaTypeName.Substring(num2, textIndex - num2);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00015F01 File Offset: 0x00014101
		private static bool IsHttpToken(char c)
		{
			return c < '\u007f' && c > '\u001f' && !HttpUtils.IsHttpSeparator(c);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00015F18 File Offset: 0x00014118
		private static bool IsValidInQuotedHeaderValue(char c)
		{
			return (c >= ' ' || c == ' ' || c == '\t') && c != '\u007f';
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00015F40 File Offset: 0x00014140
		private static bool IsHttpSeparator(char c)
		{
			return c == '(' || c == ')' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}' || c == ' ' || c == '\t';
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00015FAE File Offset: 0x000141AE
		private static bool ReadToken(string text, ref int textIndex)
		{
			while (textIndex < text.Length && HttpUtils.IsHttpToken(text.get_Chars(textIndex)))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00015FDB File Offset: 0x000141DB
		private static int DigitToInt32(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (HttpUtils.IsHttpElementSeparator(c))
			{
				return -1;
			}
			throw new ODataException(Strings.HttpUtils_CannotConvertCharToInt(c));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00016006 File Offset: 0x00014206
		private static bool IsHttpElementSeparator(char c)
		{
			return c == ',' || c == ' ' || c == '\t';
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00016019 File Offset: 0x00014219
		private static bool ReadLiteral(string text, int textIndex, string literal)
		{
			if (string.Compare(text, textIndex, literal, 0, literal.Length, 4) != 0)
			{
				throw new ODataException(Strings.HttpUtils_ExpectedLiteralNotFoundInString(literal, textIndex, text));
			}
			return textIndex + literal.Length == text.Length;
		}

		// Token: 0x0200009F RID: 159
		private struct CharsetPart
		{
			// Token: 0x06000607 RID: 1543 RVA: 0x00016050 File Offset: 0x00014250
			internal CharsetPart(string charset, int quality)
			{
				this.Charset = charset;
				this.Quality = quality;
			}

			// Token: 0x04000285 RID: 645
			internal readonly string Charset;

			// Token: 0x04000286 RID: 646
			internal readonly int Quality;
		}
	}
}
