using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x02000038 RID: 56
	internal abstract class HttpHeaderValueLexer
	{
		// Token: 0x060001EA RID: 490 RVA: 0x000054C8 File Offset: 0x000036C8
		private HttpHeaderValueLexer(string httpHeaderName, string httpHeaderValue, string value, string originalText, int startIndexOfNextItem)
		{
			this.httpHeaderName = httpHeaderName;
			this.httpHeaderValue = httpHeaderValue;
			this.value = value;
			this.originalText = originalText;
			if (this.httpHeaderValue != null)
			{
				HttpUtils.SkipWhitespace(this.httpHeaderValue, ref startIndexOfNextItem);
			}
			this.startIndexOfNextItem = startIndexOfNextItem;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00005516 File Offset: 0x00003716
		internal string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000551E File Offset: 0x0000371E
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001ED RID: 493
		internal abstract HttpHeaderValueLexer.HttpHeaderValueItemType Type { get; }

		// Token: 0x060001EE RID: 494 RVA: 0x00005526 File Offset: 0x00003726
		internal static HttpHeaderValueLexer Create(string httpHeaderName, string httpHeaderValue)
		{
			return new HttpHeaderValueLexer.HttpHeaderStart(httpHeaderName, httpHeaderValue);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005530 File Offset: 0x00003730
		internal HttpHeaderValue ToHttpHeaderValue()
		{
			HttpHeaderValueLexer httpHeaderValueLexer = this;
			HttpHeaderValue httpHeaderValue = new HttpHeaderValue();
			while (httpHeaderValueLexer.Type != HttpHeaderValueLexer.HttpHeaderValueItemType.End)
			{
				httpHeaderValueLexer = httpHeaderValueLexer.ReadNext();
				if (httpHeaderValueLexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.Token)
				{
					HttpHeaderValueElement httpHeaderValueElement = HttpHeaderValueLexer.ReadHttpHeaderValueElement(ref httpHeaderValueLexer);
					if (!httpHeaderValue.ContainsKey(httpHeaderValueElement.Name))
					{
						httpHeaderValue.Add(httpHeaderValueElement.Name, httpHeaderValueElement);
					}
				}
			}
			return httpHeaderValue;
		}

		// Token: 0x060001F0 RID: 496
		internal abstract HttpHeaderValueLexer ReadNext();

		// Token: 0x060001F1 RID: 497 RVA: 0x00005584 File Offset: 0x00003784
		private static HttpHeaderValueElement ReadHttpHeaderValueElement(ref HttpHeaderValueLexer lexer)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>> { HttpHeaderValueLexer.ReadKeyValuePair(ref lexer) };
			while (lexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.ParameterSeparator)
			{
				lexer = lexer.ReadNext();
				list.Add(HttpHeaderValueLexer.ReadKeyValuePair(ref lexer));
			}
			return new HttpHeaderValueElement(list[0].Key, list[0].Value, list.Skip(1).ToArray<KeyValuePair<string, string>>());
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000055F4 File Offset: 0x000037F4
		private static KeyValuePair<string, string> ReadKeyValuePair(ref HttpHeaderValueLexer lexer)
		{
			string text = lexer.OriginalText;
			string text2 = null;
			lexer = lexer.ReadNext();
			if (lexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.ValueSeparator)
			{
				lexer = lexer.ReadNext();
				text2 = lexer.OriginalText;
				lexer = lexer.ReadNext();
			}
			return new KeyValuePair<string, string>(text, text2);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000563F File Offset: 0x0000383F
		private bool EndOfHeaderValue()
		{
			return this.startIndexOfNextItem == this.httpHeaderValue.Length;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005654 File Offset: 0x00003854
		private HttpHeaderValueLexer ReadNextTokenOrQuotedString()
		{
			int num = this.startIndexOfNextItem;
			bool flag;
			string text = HttpUtils.ReadTokenOrQuotedStringValue(this.httpHeaderName, this.httpHeaderValue, ref num, out flag, (string message) => new ODataException(message));
			if (num == this.startIndexOfNextItem)
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_FailedToReadTokenOrQuotedString(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem));
			}
			if (flag)
			{
				string text2 = this.httpHeaderValue.Substring(this.startIndexOfNextItem, num - this.startIndexOfNextItem);
				return new HttpHeaderValueLexer.HttpHeaderQuotedString(this.httpHeaderName, this.httpHeaderValue, text, text2, num);
			}
			return new HttpHeaderValueLexer.HttpHeaderToken(this.httpHeaderName, this.httpHeaderValue, text, num);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005710 File Offset: 0x00003910
		private HttpHeaderValueLexer.HttpHeaderToken ReadNextToken()
		{
			HttpHeaderValueLexer httpHeaderValueLexer = this.ReadNextTokenOrQuotedString();
			if (httpHeaderValueLexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString)
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem));
			}
			return (HttpHeaderValueLexer.HttpHeaderToken)httpHeaderValueLexer;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005758 File Offset: 0x00003958
		private HttpHeaderValueLexer.HttpHeaderSeparator ReadNextSeparator()
		{
			string text = this.httpHeaderValue.Substring(this.startIndexOfNextItem, 1);
			if (text != "," && text != ";" && text != "=")
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_UnrecognizedSeparator(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem, text));
			}
			return new HttpHeaderValueLexer.HttpHeaderSeparator(this.httpHeaderName, this.httpHeaderValue, text, this.startIndexOfNextItem + 1);
		}

		// Token: 0x04000098 RID: 152
		internal const string ElementSeparator = ",";

		// Token: 0x04000099 RID: 153
		internal const string ParameterSeparator = ";";

		// Token: 0x0400009A RID: 154
		internal const string ValueSeparator = "=";

		// Token: 0x0400009B RID: 155
		private readonly string httpHeaderName;

		// Token: 0x0400009C RID: 156
		private readonly string httpHeaderValue;

		// Token: 0x0400009D RID: 157
		private readonly int startIndexOfNextItem;

		// Token: 0x0400009E RID: 158
		private readonly string value;

		// Token: 0x0400009F RID: 159
		private readonly string originalText;

		// Token: 0x02000287 RID: 647
		internal enum HttpHeaderValueItemType
		{
			// Token: 0x04000BEF RID: 3055
			Start,
			// Token: 0x04000BF0 RID: 3056
			Token,
			// Token: 0x04000BF1 RID: 3057
			QuotedString,
			// Token: 0x04000BF2 RID: 3058
			ElementSeparator,
			// Token: 0x04000BF3 RID: 3059
			ParameterSeparator,
			// Token: 0x04000BF4 RID: 3060
			ValueSeparator,
			// Token: 0x04000BF5 RID: 3061
			End
		}

		// Token: 0x02000288 RID: 648
		private sealed class HttpHeaderStart : HttpHeaderValueLexer
		{
			// Token: 0x06001C42 RID: 7234 RVA: 0x0005614F File Offset: 0x0005434F
			internal HttpHeaderStart(string httpHeaderName, string httpHeaderValue)
				: base(httpHeaderName, httpHeaderValue, null, null, 0)
			{
			}

			// Token: 0x170005C6 RID: 1478
			// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00002390 File Offset: 0x00000590
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Start;
				}
			}

			// Token: 0x06001C44 RID: 7236 RVA: 0x0005615C File Offset: 0x0005435C
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (this.httpHeaderValue == null || base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextToken();
			}
		}

		// Token: 0x02000289 RID: 649
		private sealed class HttpHeaderToken : HttpHeaderValueLexer
		{
			// Token: 0x06001C45 RID: 7237 RVA: 0x0005617A File Offset: 0x0005437A
			internal HttpHeaderToken(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x170005C7 RID: 1479
			// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00002393 File Offset: 0x00000593
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Token;
				}
			}

			// Token: 0x06001C47 RID: 7239 RVA: 0x00056188 File Offset: 0x00054388
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextSeparator();
			}
		}

		// Token: 0x0200028A RID: 650
		private sealed class HttpHeaderQuotedString : HttpHeaderValueLexer
		{
			// Token: 0x06001C48 RID: 7240 RVA: 0x0005619E File Offset: 0x0005439E
			internal HttpHeaderQuotedString(string httpHeaderName, string httpHeaderValue, string value, string originalText, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, originalText, startIndexOfNextItem)
			{
			}

			// Token: 0x170005C8 RID: 1480
			// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00038940 File Offset: 0x00036B40
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString;
				}
			}

			// Token: 0x06001C4A RID: 7242 RVA: 0x000561B0 File Offset: 0x000543B0
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				HttpHeaderValueLexer.HttpHeaderSeparator httpHeaderSeparator = base.ReadNextSeparator();
				if (httpHeaderSeparator.Value == "," || httpHeaderSeparator.Value == ";")
				{
					return httpHeaderSeparator;
				}
				throw new ODataException(Strings.HttpHeaderValueLexer_InvalidSeparatorAfterQuotedString(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem, httpHeaderSeparator.Value));
			}
		}

		// Token: 0x0200028B RID: 651
		private sealed class HttpHeaderSeparator : HttpHeaderValueLexer
		{
			// Token: 0x06001C4B RID: 7243 RVA: 0x0005617A File Offset: 0x0005437A
			internal HttpHeaderSeparator(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x170005C9 RID: 1481
			// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00056220 File Offset: 0x00054420
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					string value = base.Value;
					if (value == ",")
					{
						return HttpHeaderValueLexer.HttpHeaderValueItemType.ElementSeparator;
					}
					if (!(value == ";"))
					{
						return HttpHeaderValueLexer.HttpHeaderValueItemType.ValueSeparator;
					}
					return HttpHeaderValueLexer.HttpHeaderValueItemType.ParameterSeparator;
				}
			}

			// Token: 0x06001C4D RID: 7245 RVA: 0x00056258 File Offset: 0x00054458
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (base.EndOfHeaderValue())
				{
					throw new ODataException(Strings.HttpHeaderValueLexer_EndOfFileAfterSeparator(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem, this.originalText));
				}
				if (base.Value == "=")
				{
					return base.ReadNextTokenOrQuotedString();
				}
				return base.ReadNextToken();
			}
		}

		// Token: 0x0200028C RID: 652
		private sealed class HttpHeaderEnd : HttpHeaderValueLexer
		{
			// Token: 0x06001C4E RID: 7246 RVA: 0x000562B4 File Offset: 0x000544B4
			private HttpHeaderEnd()
				: base(null, null, null, null, -1)
			{
			}

			// Token: 0x170005CA RID: 1482
			// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0003B90E File Offset: 0x00039B0E
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.End;
				}
			}

			// Token: 0x06001C50 RID: 7248 RVA: 0x0000360D File Offset: 0x0000180D
			internal override HttpHeaderValueLexer ReadNext()
			{
				return null;
			}

			// Token: 0x04000BF6 RID: 3062
			internal static readonly HttpHeaderValueLexer.HttpHeaderEnd Instance = new HttpHeaderValueLexer.HttpHeaderEnd();
		}
	}
}
