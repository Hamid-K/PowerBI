using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x02000287 RID: 647
	internal static class HttpUtils
	{
		// Token: 0x06001457 RID: 5207 RVA: 0x0004A9AC File Offset: 0x00048BAC
		internal static IList<KeyValuePair<string, string>> ReadMimeType(string contentType, out string mediaTypeName, out string mediaTypeCharset)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				throw new ODataContentTypeException(Strings.HttpUtils_ContentTypeMissing);
			}
			IList<KeyValuePair<MediaType, string>> list = HttpUtils.ReadMediaTypes(contentType);
			if (list.Count != 1)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_NoOrMoreThanOneContentTypeSpecified(contentType));
			}
			MediaType key = list[0].Key;
			MediaTypeUtils.CheckMediaTypeForWildCards(key);
			mediaTypeName = key.FullTypeName;
			mediaTypeCharset = list[0].Value;
			return key.Parameters;
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004AA1D File Offset: 0x00048C1D
		internal static string BuildContentType(MediaType mediaType, Encoding encoding)
		{
			return mediaType.ToText(encoding);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0004AA26 File Offset: 0x00048C26
		internal static IList<KeyValuePair<MediaType, string>> MediaTypesFromString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return HttpUtils.ReadMediaTypes(text);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004AA38 File Offset: 0x00048C38
		internal static bool CompareMediaTypeNames(string mediaTypeName1, string mediaTypeName2)
		{
			return string.Equals(mediaTypeName1, mediaTypeName2, 5);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004AA42 File Offset: 0x00048C42
		internal static bool CompareMediaTypeParameterNames(string parameterName1, string parameterName2)
		{
			return string.Equals(parameterName1, parameterName2, 5);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004AA60 File Offset: 0x00048C60
		internal static Encoding EncodingFromAcceptableCharsets(string acceptableCharsets, MediaType mediaType, Encoding utf8Encoding, Encoding defaultEncoding)
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

		// Token: 0x0600145D RID: 5213 RVA: 0x0004AB20 File Offset: 0x00048D20
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

		// Token: 0x0600145E RID: 5214 RVA: 0x0004AC04 File Offset: 0x00048E04
		internal static void ValidateHttpMethod(string httpMethodString)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(httpMethodString, "httpMethodString");
			if (string.CompareOrdinal(httpMethodString, "GET") != 0 && string.CompareOrdinal(httpMethodString, "DELETE") != 0 && string.CompareOrdinal(httpMethodString, "MERGE") != 0 && string.CompareOrdinal(httpMethodString, "PATCH") != 0 && string.CompareOrdinal(httpMethodString, "POST") != 0 && string.CompareOrdinal(httpMethodString, "PUT") != 0)
			{
				throw new ODataException(Strings.HttpUtils_InvalidHttpMethodString(httpMethodString));
			}
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004AC76 File Offset: 0x00048E76
		internal static bool IsQueryMethod(string httpMethod)
		{
			return string.CompareOrdinal(httpMethod, "GET") == 0;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004AC88 File Offset: 0x00048E88
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

		// Token: 0x06001461 RID: 5217 RVA: 0x0004AE7C File Offset: 0x0004907C
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

		// Token: 0x06001462 RID: 5218 RVA: 0x0004AEB4 File Offset: 0x000490B4
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

		// Token: 0x06001463 RID: 5219 RVA: 0x0004AFD6 File Offset: 0x000491D6
		internal static bool SkipWhitespace(string text, ref int textIndex)
		{
			while (textIndex < text.Length && char.IsWhiteSpace(text, textIndex))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004B2B0 File Offset: 0x000494B0
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

		// Token: 0x06001465 RID: 5221 RVA: 0x0004B2D0 File Offset: 0x000494D0
		private static IList<KeyValuePair<MediaType, string>> ReadMediaTypes(string text)
		{
			List<KeyValuePair<string, string>> list = null;
			List<KeyValuePair<MediaType, string>> list2 = new List<KeyValuePair<MediaType, string>>();
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
				list2.Add(new KeyValuePair<MediaType, string>(new MediaType(text2, text3, (list == null) ? null : list.ToArray()), text4));
				list = null;
			}
			return list2;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0004B384 File Offset: 0x00049584
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

		// Token: 0x06001467 RID: 5223 RVA: 0x0004B444 File Offset: 0x00049644
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

		// Token: 0x06001468 RID: 5224 RVA: 0x0004B4BD File Offset: 0x000496BD
		private static bool IsHttpToken(char c)
		{
			return c < '\u007f' && c > '\u001f' && !HttpUtils.IsHttpSeparator(c);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0004B4D4 File Offset: 0x000496D4
		private static bool IsValidInQuotedHeaderValue(char c)
		{
			return (c >= ' ' || c == ' ' || c == '\t') && c != '\u007f';
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0004B4FC File Offset: 0x000496FC
		private static bool IsHttpSeparator(char c)
		{
			return c == '(' || c == ')' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}' || c == ' ' || c == '\t';
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0004B56A File Offset: 0x0004976A
		private static bool ReadToken(string text, ref int textIndex)
		{
			while (textIndex < text.Length && HttpUtils.IsHttpToken(text.get_Chars(textIndex)))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0004B597 File Offset: 0x00049797
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

		// Token: 0x0600146D RID: 5229 RVA: 0x0004B5C2 File Offset: 0x000497C2
		private static bool IsHttpElementSeparator(char c)
		{
			return c == ',' || c == ' ' || c == '\t';
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0004B5D5 File Offset: 0x000497D5
		private static bool ReadLiteral(string text, int textIndex, string literal)
		{
			if (string.Compare(text, textIndex, literal, 0, literal.Length, 4) != 0)
			{
				throw new ODataException(Strings.HttpUtils_ExpectedLiteralNotFoundInString(literal, textIndex, text));
			}
			return textIndex + literal.Length == text.Length;
		}

		// Token: 0x02000288 RID: 648
		private struct CharsetPart
		{
			// Token: 0x06001471 RID: 5233 RVA: 0x0004B60C File Offset: 0x0004980C
			internal CharsetPart(string charset, int quality)
			{
				this.Charset = charset;
				this.Quality = quality;
			}

			// Token: 0x04000845 RID: 2117
			internal readonly string Charset;

			// Token: 0x04000846 RID: 2118
			internal readonly int Quality;
		}
	}
}
