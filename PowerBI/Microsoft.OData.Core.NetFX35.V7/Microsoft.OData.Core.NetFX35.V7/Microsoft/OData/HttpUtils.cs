using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000012 RID: 18
	internal static class HttpUtils
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000032C4 File Offset: 0x000014C4
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

		// Token: 0x06000074 RID: 116 RVA: 0x00003344 File Offset: 0x00001544
		internal static string BuildContentType(ODataMediaType mediaType, Encoding encoding)
		{
			return mediaType.ToText(encoding);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000334D File Offset: 0x0000154D
		internal static IList<KeyValuePair<ODataMediaType, string>> MediaTypesFromString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return HttpUtils.ReadMediaTypes(text);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000335F File Offset: 0x0000155F
		internal static bool CompareMediaTypeNames(string mediaTypeName1, string mediaTypeName2)
		{
			return string.Equals(mediaTypeName1, mediaTypeName2, 5);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000335F File Offset: 0x0000155F
		internal static bool CompareMediaTypeParameterNames(string parameterName1, string parameterName2)
		{
			return string.Equals(parameterName1, parameterName2, 5);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000336C File Offset: 0x0000156C
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

		// Token: 0x06000079 RID: 121 RVA: 0x00003428 File Offset: 0x00001628
		internal static void ReadQualityValue(string text, ref int textIndex, out int qualityValue)
		{
			int num = textIndex;
			textIndex = num + 1;
			char c = text.get_Chars(num);
			if (c != '0')
			{
				if (c != '1')
				{
					throw new ODataContentTypeException(Strings.HttpUtils_InvalidQualityValueStartChar(text, c));
				}
				qualityValue = 1;
			}
			else
			{
				qualityValue = 0;
			}
			if (textIndex < text.Length && text.get_Chars(textIndex) == '.')
			{
				textIndex++;
				int num2 = 1000;
				while (num2 > 1 && textIndex < text.Length)
				{
					char c2 = text.get_Chars(textIndex);
					int num3 = HttpUtils.DigitToInt32(c2);
					if (num3 < 0)
					{
						break;
					}
					textIndex++;
					num2 /= 10;
					qualityValue *= 10;
					qualityValue += num3;
				}
				qualityValue *= num2;
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

		// Token: 0x0600007A RID: 122 RVA: 0x00003500 File Offset: 0x00001700
		internal static void ValidateHttpMethod(string httpMethodString)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(httpMethodString, "httpMethodString");
			if (string.CompareOrdinal(httpMethodString, "GET") != 0 && string.CompareOrdinal(httpMethodString, "DELETE") != 0 && string.CompareOrdinal(httpMethodString, "PATCH") != 0 && string.CompareOrdinal(httpMethodString, "POST") != 0 && string.CompareOrdinal(httpMethodString, "PUT") != 0)
			{
				throw new ODataException(Strings.HttpUtils_InvalidHttpMethodString(httpMethodString));
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003565 File Offset: 0x00001765
		internal static bool IsQueryMethod(string httpMethod)
		{
			return string.CompareOrdinal(httpMethod, "GET") == 0;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003578 File Offset: 0x00001778
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is a large switch on all the Http response codes; no complexity here.")]
		internal static string GetStatusMessage(int statusCode)
		{
			if (statusCode <= 206)
			{
				if (statusCode == 100)
				{
					return "Continue";
				}
				if (statusCode == 101)
				{
					return "Switching Protocols";
				}
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

		// Token: 0x0600007D RID: 125 RVA: 0x00003768 File Offset: 0x00001968
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

		// Token: 0x0600007E RID: 126 RVA: 0x000037A0 File Offset: 0x000019A0
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

		// Token: 0x0600007F RID: 127 RVA: 0x000038C2 File Offset: 0x00001AC2
		internal static bool SkipWhitespace(string text, ref int textIndex)
		{
			while (textIndex < text.Length && char.IsWhiteSpace(text, textIndex))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000038EA File Offset: 0x00001AEA
		private static IEnumerable<HttpUtils.CharsetPart> AcceptCharsetParts(string headerValue)
		{
			bool flag = false;
			int i = 0;
			while (i < headerValue.Length)
			{
				if (HttpUtils.SkipWhitespace(headerValue, ref i))
				{
					yield break;
				}
				if (headerValue.get_Chars(i) == ',')
				{
					flag = false;
					i++;
				}
				else
				{
					if (flag)
					{
						throw new ODataContentTypeException(Strings.HttpUtils_MissingSeparatorBetweenCharsets(headerValue));
					}
					int num = i;
					int num2 = num;
					bool flag2 = HttpUtils.ReadToken(headerValue, ref num2);
					if (num2 == i)
					{
						throw new ODataContentTypeException(Strings.HttpUtils_InvalidCharsetName(headerValue));
					}
					int qualityValue;
					int headerEnd;
					if (flag2)
					{
						qualityValue = 1000;
						headerEnd = num2;
					}
					else
					{
						char c = headerValue.get_Chars(num2);
						if (!HttpUtils.IsHttpSeparator(c))
						{
							throw new ODataContentTypeException(Strings.HttpUtils_InvalidSeparatorBetweenCharsets(headerValue));
						}
						if (c == ';')
						{
							if (HttpUtils.ReadLiteral(headerValue, num2, ";q="))
							{
								throw new ODataContentTypeException(Strings.HttpUtils_UnexpectedEndOfQValue(headerValue));
							}
							headerEnd = num2 + 3;
							HttpUtils.ReadQualityValue(headerValue, ref headerEnd, out qualityValue);
						}
						else
						{
							qualityValue = 1000;
							headerEnd = num2;
						}
					}
					yield return new HttpUtils.CharsetPart(headerValue.Substring(num, num2 - num), qualityValue);
					flag = true;
					i = headerEnd;
				}
			}
			yield break;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038FC File Offset: 0x00001AFC
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

		// Token: 0x06000082 RID: 130 RVA: 0x00003998 File Offset: 0x00001B98
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

		// Token: 0x06000083 RID: 131 RVA: 0x00003A58 File Offset: 0x00001C58
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

		// Token: 0x06000084 RID: 132 RVA: 0x00003AD1 File Offset: 0x00001CD1
		private static bool IsHttpToken(char c)
		{
			return c < '\u007f' && c > '\u001f' && !HttpUtils.IsHttpSeparator(c);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003AE8 File Offset: 0x00001CE8
		private static bool IsValidInQuotedHeaderValue(char c)
		{
			return (c >= ' ' || c == ' ' || c == '\t') && c != '\u007f';
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003B10 File Offset: 0x00001D10
		private static bool IsHttpSeparator(char c)
		{
			return c == '(' || c == ')' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}' || c == ' ' || c == '\t';
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003B7E File Offset: 0x00001D7E
		private static bool ReadToken(string text, ref int textIndex)
		{
			while (textIndex < text.Length && HttpUtils.IsHttpToken(text.get_Chars(textIndex)))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003BAB File Offset: 0x00001DAB
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

		// Token: 0x06000089 RID: 137 RVA: 0x00003BD6 File Offset: 0x00001DD6
		private static bool IsHttpElementSeparator(char c)
		{
			return c == ',' || c == ' ' || c == '\t';
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003BE9 File Offset: 0x00001DE9
		private static bool ReadLiteral(string text, int textIndex, string literal)
		{
			if (string.Compare(text, textIndex, literal, 0, literal.Length, 4) != 0)
			{
				throw new ODataException(Strings.HttpUtils_ExpectedLiteralNotFoundInString(literal, textIndex, text));
			}
			return textIndex + literal.Length == text.Length;
		}

		// Token: 0x02000240 RID: 576
		private struct CharsetPart
		{
			// Token: 0x06001700 RID: 5888 RVA: 0x00046805 File Offset: 0x00044A05
			internal CharsetPart(string charset, int quality)
			{
				this.Charset = charset;
				this.Quality = quality;
			}

			// Token: 0x04000A96 RID: 2710
			internal readonly string Charset;

			// Token: 0x04000A97 RID: 2711
			internal readonly int Quality;
		}
	}
}
