using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.OData
{
	// Token: 0x02000039 RID: 57
	internal static class HttpUtils
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x000057DC File Offset: 0x000039DC
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
			return key.Parameters.ToList<KeyValuePair<string, string>>();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000585C File Offset: 0x00003A5C
		internal static string BuildContentType(ODataMediaType mediaType, Encoding encoding)
		{
			return mediaType.ToText(encoding);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005865 File Offset: 0x00003A65
		internal static IList<KeyValuePair<ODataMediaType, string>> MediaTypesFromString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return HttpUtils.ReadMediaTypes(text);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005877 File Offset: 0x00003A77
		internal static bool CompareMediaTypeNames(string mediaTypeName1, string mediaTypeName2)
		{
			return string.Equals(mediaTypeName1, mediaTypeName2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005881 File Offset: 0x00003A81
		internal static bool CompareMediaTypeParameterNames(string parameterName1, string parameterName2)
		{
			return string.Equals(parameterName1, parameterName2, StringComparison.OrdinalIgnoreCase) || (HttpUtils.IsMetadataParameter(parameterName1) && HttpUtils.IsMetadataParameter(parameterName2)) || (HttpUtils.IsStreamingParameter(parameterName1) && HttpUtils.IsStreamingParameter(parameterName2));
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000058AF File Offset: 0x00003AAF
		internal static bool IsMetadataParameter(string parameterName)
		{
			return string.Compare(parameterName, "odata.metadata", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(parameterName, "metadata", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000058D0 File Offset: 0x00003AD0
		internal static bool IsStreamingParameter(string parameterName)
		{
			return string.Compare(parameterName, "odata.streaming", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(parameterName, "streaming", StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000058F4 File Offset: 0x00003AF4
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
						if (string.Compare("utf-8", value.Charset, StringComparison.OrdinalIgnoreCase) == 0)
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

		// Token: 0x060001FF RID: 511 RVA: 0x000059B0 File Offset: 0x00003BB0
		internal static void ReadQualityValue(string text, ref int textIndex, out int qualityValue)
		{
			int num = textIndex;
			textIndex = num + 1;
			char c = text[num];
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
			if (textIndex < text.Length && text[textIndex] == '.')
			{
				textIndex++;
				int num2 = 1000;
				while (num2 > 1 && textIndex < text.Length)
				{
					char c2 = text[textIndex];
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

		// Token: 0x06000200 RID: 512 RVA: 0x00005A88 File Offset: 0x00003C88
		internal static void ValidateHttpMethod(string httpMethodString)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(httpMethodString, "httpMethodString");
			if (string.CompareOrdinal(httpMethodString, "GET") != 0 && string.CompareOrdinal(httpMethodString, "DELETE") != 0 && string.CompareOrdinal(httpMethodString, "PATCH") != 0 && string.CompareOrdinal(httpMethodString, "POST") != 0 && string.CompareOrdinal(httpMethodString, "PUT") != 0)
			{
				throw new ODataException(Strings.HttpUtils_InvalidHttpMethodString(httpMethodString));
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00005AED File Offset: 0x00003CED
		internal static bool IsQueryMethod(string httpMethod)
		{
			return string.CompareOrdinal(httpMethod, "GET") == 0;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00005B00 File Offset: 0x00003D00
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

		// Token: 0x06000203 RID: 515 RVA: 0x00005CF0 File Offset: 0x00003EF0
		internal static Encoding GetEncodingFromCharsetName(string charsetName)
		{
			Encoding encoding;
			try
			{
				encoding = Encoding.GetEncoding(charsetName);
			}
			catch (ArgumentException)
			{
				encoding = null;
			}
			return encoding;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00005D1C File Offset: 0x00003F1C
		internal static string ReadTokenOrQuotedStringValue(string headerName, string headerText, ref int textIndex, out bool isQuotedString, Func<string, Exception> createException)
		{
			StringBuilder stringBuilder = new StringBuilder();
			isQuotedString = false;
			if (textIndex < headerText.Length && headerText[textIndex] == '"')
			{
				textIndex++;
				isQuotedString = true;
			}
			char c = '\0';
			while (textIndex < headerText.Length)
			{
				c = headerText[textIndex];
				if (c == '\\' || c == '"')
				{
					if (!isQuotedString)
					{
						throw createException(Strings.HttpUtils_EscapeCharWithoutQuotes(headerName, headerText, textIndex, c));
					}
					textIndex++;
					if (c == '"')
					{
						break;
					}
					if (textIndex >= headerText.Length)
					{
						throw createException(Strings.HttpUtils_EscapeCharAtEnd(headerName, headerText, textIndex, c));
					}
					c = headerText[textIndex];
				}
				else
				{
					if (!isQuotedString && !HttpUtils.IsHttpToken(c))
					{
						break;
					}
					if (isQuotedString && !HttpUtils.IsValidInQuotedHeaderValue(c))
					{
						throw createException(Strings.HttpUtils_InvalidCharacterInQuotedParameterValue(headerName, headerText, textIndex, c));
					}
				}
				stringBuilder.Append(c);
				textIndex++;
			}
			if (isQuotedString && c != '"')
			{
				throw createException(Strings.HttpUtils_ClosingQuoteNotFound(headerName, headerText, textIndex));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005E3E File Offset: 0x0000403E
		internal static bool SkipWhitespace(string text, ref int textIndex)
		{
			while (textIndex < text.Length && char.IsWhiteSpace(text, textIndex))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005E66 File Offset: 0x00004066
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
				if (headerValue[i] == ',')
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
						char c = headerValue[num2];
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

		// Token: 0x06000207 RID: 519 RVA: 0x00005E78 File Offset: 0x00004078
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
					if (text[num] == ',')
					{
						num++;
						break;
					}
					if (text[num] != ';')
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

		// Token: 0x06000208 RID: 520 RVA: 0x00005F14 File Offset: 0x00004114
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
			if (text[textIndex] != '=')
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

		// Token: 0x06000209 RID: 521 RVA: 0x00005FD4 File Offset: 0x000041D4
		private static void ReadMediaTypeAndSubtype(string mediaTypeName, ref int textIndex, out string type, out string subType)
		{
			int num = textIndex;
			if (HttpUtils.ReadToken(mediaTypeName, ref textIndex))
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeUnspecified(mediaTypeName));
			}
			if (mediaTypeName[textIndex] != '/')
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

		// Token: 0x0600020A RID: 522 RVA: 0x0000604D File Offset: 0x0000424D
		private static bool IsHttpToken(char c)
		{
			return c < '\u007f' && c > '\u001f' && !HttpUtils.IsHttpSeparator(c);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006064 File Offset: 0x00004264
		private static bool IsValidInQuotedHeaderValue(char c)
		{
			return (c >= ' ' || c == ' ' || c == '\t') && c != '\u007f';
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000608C File Offset: 0x0000428C
		private static bool IsHttpSeparator(char c)
		{
			return c == '(' || c == ')' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}' || c == ' ' || c == '\t';
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000060FA File Offset: 0x000042FA
		private static bool ReadToken(string text, ref int textIndex)
		{
			while (textIndex < text.Length && HttpUtils.IsHttpToken(text[textIndex]))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006127 File Offset: 0x00004327
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

		// Token: 0x0600020F RID: 527 RVA: 0x00006152 File Offset: 0x00004352
		private static bool IsHttpElementSeparator(char c)
		{
			return c == ',' || c == ' ' || c == '\t';
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006165 File Offset: 0x00004365
		private static bool ReadLiteral(string text, int textIndex, string literal)
		{
			if (string.Compare(text, textIndex, literal, 0, literal.Length, StringComparison.Ordinal) != 0)
			{
				throw new ODataException(Strings.HttpUtils_ExpectedLiteralNotFoundInString(literal, textIndex, text));
			}
			return textIndex + literal.Length == text.Length;
		}

		// Token: 0x0200028E RID: 654
		private struct CharsetPart
		{
			// Token: 0x06001C55 RID: 7253 RVA: 0x000562E1 File Offset: 0x000544E1
			internal CharsetPart(string charset, int quality)
			{
				this.Charset = charset;
				this.Quality = quality;
			}

			// Token: 0x04000BF9 RID: 3065
			internal readonly string Charset;

			// Token: 0x04000BFA RID: 3066
			internal readonly int Quality;
		}
	}
}
