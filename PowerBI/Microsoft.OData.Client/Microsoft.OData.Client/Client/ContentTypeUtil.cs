using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.OData.Client
{
	// Token: 0x02000013 RID: 19
	internal static class ContentTypeUtil
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003E03 File Offset: 0x00002003
		internal static Encoding FallbackEncoding
		{
			get
			{
				return ContentTypeUtil.EncodingUtf8NoPreamble;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003E0A File Offset: 0x0000200A
		private static Encoding MissingEncoding
		{
			get
			{
				return Encoding.GetEncoding("ISO-8859-1", new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003E20 File Offset: 0x00002020
		internal static ContentTypeUtil.MediaParameter[] ReadContentType(string contentType, out string mime)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_ContentTypeMissing);
			}
			ContentTypeUtil.MediaType mediaType = ContentTypeUtil.ReadMediaType(contentType);
			mime = mediaType.MimeType;
			return mediaType.Parameters;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003E5C File Offset: 0x0000205C
		internal static string WriteContentType(string mimeType, ContentTypeUtil.MediaParameter[] parameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(mimeType);
			foreach (ContentTypeUtil.MediaParameter mediaParameter in parameters)
			{
				stringBuilder.Append(';');
				stringBuilder.Append(mediaParameter.Name);
				stringBuilder.Append("=");
				stringBuilder.Append(mediaParameter.GetOriginalValue());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003EC0 File Offset: 0x000020C0
		internal static ContentTypeUtil.MediaParameter[] ReadContentType(string contentType, out string mime, out Encoding encoding)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_ContentTypeMissing);
			}
			ContentTypeUtil.MediaType mediaType = ContentTypeUtil.ReadMediaType(contentType);
			mime = mediaType.MimeType;
			encoding = mediaType.SelectEncoding();
			return mediaType.Parameters;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003F04 File Offset: 0x00002104
		private static Encoding EncodingFromName(string name)
		{
			if (name == null)
			{
				return ContentTypeUtil.MissingEncoding;
			}
			name = name.Trim();
			if (name.Length == 0)
			{
				return ContentTypeUtil.MissingEncoding;
			}
			Encoding encoding;
			try
			{
				encoding = Encoding.GetEncoding(name);
			}
			catch (ArgumentException)
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_EncodingNotSupported(name));
			}
			return encoding;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003F60 File Offset: 0x00002160
		private static void ReadMediaTypeAndSubtype(string text, ref int textIndex, out string type, out string subType)
		{
			int num = textIndex;
			if (ContentTypeUtil.ReadToken(text, ref textIndex))
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_MediaTypeUnspecified);
			}
			if (text[textIndex] != '/')
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_MediaTypeRequiresSlash);
			}
			type = text.Substring(num, textIndex - num);
			textIndex++;
			int num2 = textIndex;
			ContentTypeUtil.ReadToken(text, ref textIndex);
			if (textIndex == num2)
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_MediaTypeRequiresSubType);
			}
			subType = text.Substring(num2, textIndex - num2);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003FE8 File Offset: 0x000021E8
		private static ContentTypeUtil.MediaType ReadMediaType(string text)
		{
			int num = 0;
			string text2;
			string text3;
			ContentTypeUtil.ReadMediaTypeAndSubtype(text, ref num, out text2, out text3);
			ContentTypeUtil.MediaParameter[] array = null;
			while (!ContentTypeUtil.SkipWhitespace(text, ref num))
			{
				if (text[num] != ';')
				{
					throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_MediaTypeRequiresSemicolonBeforeParameter);
				}
				num++;
				if (ContentTypeUtil.SkipWhitespace(text, ref num))
				{
					break;
				}
				ContentTypeUtil.ReadMediaTypeParameter(text, ref num, ref array);
			}
			return new ContentTypeUtil.MediaType(text2, text3, array);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000404C File Offset: 0x0000224C
		private static bool ReadToken(string text, ref int textIndex)
		{
			while (textIndex < text.Length && ContentTypeUtil.IsHttpToken(text[textIndex]))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004079 File Offset: 0x00002279
		private static bool SkipWhitespace(string text, ref int textIndex)
		{
			while (textIndex < text.Length && char.IsWhiteSpace(text, textIndex))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000040A4 File Offset: 0x000022A4
		private static void ReadMediaTypeParameter(string text, ref int textIndex, ref ContentTypeUtil.MediaParameter[] parameters)
		{
			int num = textIndex;
			if (ContentTypeUtil.ReadToken(text, ref textIndex))
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_MediaTypeMissingValue);
			}
			string text2 = text.Substring(num, textIndex - num);
			if (text[textIndex] != '=')
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_MediaTypeMissingValue);
			}
			textIndex++;
			ContentTypeUtil.MediaParameter mediaParameter = ContentTypeUtil.ReadQuotedParameterValue(text2, text, ref textIndex);
			if (parameters == null)
			{
				parameters = new ContentTypeUtil.MediaParameter[1];
			}
			else
			{
				ContentTypeUtil.MediaParameter[] array = new ContentTypeUtil.MediaParameter[parameters.Length + 1];
				Array.Copy(parameters, array, parameters.Length);
				parameters = array;
			}
			parameters[parameters.Length - 1] = mediaParameter;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004138 File Offset: 0x00002338
		private static ContentTypeUtil.MediaParameter ReadQuotedParameterValue(string parameterName, string headerText, ref int textIndex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			if (textIndex < headerText.Length && headerText[textIndex] == '"')
			{
				textIndex++;
				flag2 = true;
				flag = true;
			}
			while (textIndex < headerText.Length)
			{
				char c = headerText[textIndex];
				if (c == '\\' || c == '"')
				{
					if (!flag2)
					{
						throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_EscapeCharWithoutQuotes(parameterName));
					}
					textIndex++;
					if (c == '"')
					{
						flag2 = false;
						break;
					}
					if (textIndex >= headerText.Length)
					{
						throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_EscapeCharAtEnd(parameterName));
					}
					c = headerText[textIndex];
				}
				else if (!ContentTypeUtil.IsHttpToken(c))
				{
					break;
				}
				stringBuilder.Append(c);
				textIndex++;
			}
			if (flag2)
			{
				throw Error.HttpHeaderFailure(400, Strings.HttpProcessUtility_ClosingQuoteNotFound(parameterName));
			}
			return new ContentTypeUtil.MediaParameter(parameterName, stringBuilder.ToString(), flag);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004214 File Offset: 0x00002414
		private static bool IsHttpSeparator(char c)
		{
			return c == '(' || c == ')' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}' || c == ' ' || c == '\t';
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004282 File Offset: 0x00002482
		private static bool IsHttpToken(char c)
		{
			return c < '\u007f' && c > '\u001f' && !ContentTypeUtil.IsHttpSeparator(c);
		}

		// Token: 0x04000030 RID: 48
		internal static readonly UTF8Encoding EncodingUtf8NoPreamble = new UTF8Encoding(false, true);

		// Token: 0x02000147 RID: 327
		internal class MediaParameter
		{
			// Token: 0x06000CEB RID: 3307 RVA: 0x0002D87E File Offset: 0x0002BA7E
			public MediaParameter(string name, string value, bool isQuoted)
			{
				this.Name = name;
				this.Value = value;
				this.IsQuoted = isQuoted;
			}

			// Token: 0x17000338 RID: 824
			// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002D89B File Offset: 0x0002BA9B
			// (set) Token: 0x06000CED RID: 3309 RVA: 0x0002D8A3 File Offset: 0x0002BAA3
			public string Name { get; private set; }

			// Token: 0x17000339 RID: 825
			// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002D8AC File Offset: 0x0002BAAC
			// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0002D8B4 File Offset: 0x0002BAB4
			public string Value { get; private set; }

			// Token: 0x1700033A RID: 826
			// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002D8BD File Offset: 0x0002BABD
			// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0002D8C5 File Offset: 0x0002BAC5
			private bool IsQuoted { get; set; }

			// Token: 0x06000CF2 RID: 3314 RVA: 0x0002D8CE File Offset: 0x0002BACE
			public string GetOriginalValue()
			{
				if (!this.IsQuoted)
				{
					return this.Value;
				}
				return "\"" + this.Value + "\"";
			}
		}

		// Token: 0x02000148 RID: 328
		[DebuggerDisplay("MediaType [{type}/{subType}]")]
		private sealed class MediaType
		{
			// Token: 0x06000CF3 RID: 3315 RVA: 0x0002D8F4 File Offset: 0x0002BAF4
			internal MediaType(string type, string subType, ContentTypeUtil.MediaParameter[] parameters)
			{
				this.type = type;
				this.subType = subType;
				this.parameters = parameters;
			}

			// Token: 0x1700033B RID: 827
			// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002D911 File Offset: 0x0002BB11
			internal string MimeType
			{
				get
				{
					return this.type + "/" + this.subType;
				}
			}

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0002D929 File Offset: 0x0002BB29
			internal ContentTypeUtil.MediaParameter[] Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x06000CF6 RID: 3318 RVA: 0x0002D934 File Offset: 0x0002BB34
			internal Encoding SelectEncoding()
			{
				if (this.parameters != null)
				{
					foreach (ContentTypeUtil.MediaParameter mediaParameter in this.parameters)
					{
						if (string.Equals(mediaParameter.Name, "charset", StringComparison.OrdinalIgnoreCase))
						{
							string text = mediaParameter.Value.Trim();
							if (text.Length > 0)
							{
								return ContentTypeUtil.EncodingFromName(mediaParameter.Value);
							}
						}
					}
				}
				if (string.Equals(this.type, "text", StringComparison.OrdinalIgnoreCase))
				{
					if (string.Equals(this.subType, "xml", StringComparison.OrdinalIgnoreCase))
					{
						return null;
					}
					return ContentTypeUtil.MissingEncoding;
				}
				else
				{
					if (string.Equals(this.type, "application", StringComparison.OrdinalIgnoreCase) && string.Equals(this.subType, "json", StringComparison.OrdinalIgnoreCase))
					{
						return ContentTypeUtil.FallbackEncoding;
					}
					return null;
				}
			}

			// Token: 0x040006BB RID: 1723
			private readonly ContentTypeUtil.MediaParameter[] parameters;

			// Token: 0x040006BC RID: 1724
			private readonly string subType;

			// Token: 0x040006BD RID: 1725
			private readonly string type;
		}
	}
}
