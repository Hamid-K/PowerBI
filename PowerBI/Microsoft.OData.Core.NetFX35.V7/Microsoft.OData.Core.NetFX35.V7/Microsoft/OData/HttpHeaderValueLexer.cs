using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData
{
	// Token: 0x02000011 RID: 17
	internal abstract class HttpHeaderValueLexer
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002FB0 File Offset: 0x000011B0
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

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002FFE File Offset: 0x000011FE
		internal string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003006 File Offset: 0x00001206
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000069 RID: 105
		internal abstract HttpHeaderValueLexer.HttpHeaderValueItemType Type { get; }

		// Token: 0x0600006A RID: 106 RVA: 0x0000300E File Offset: 0x0000120E
		internal static HttpHeaderValueLexer Create(string httpHeaderName, string httpHeaderValue)
		{
			return new HttpHeaderValueLexer.HttpHeaderStart(httpHeaderName, httpHeaderValue);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003018 File Offset: 0x00001218
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

		// Token: 0x0600006C RID: 108
		internal abstract HttpHeaderValueLexer ReadNext();

		// Token: 0x0600006D RID: 109 RVA: 0x0000306C File Offset: 0x0000126C
		private static HttpHeaderValueElement ReadHttpHeaderValueElement(ref HttpHeaderValueLexer lexer)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(HttpHeaderValueLexer.ReadKeyValuePair(ref lexer));
			List<KeyValuePair<string, string>> list2 = list;
			while (lexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.ParameterSeparator)
			{
				lexer = lexer.ReadNext();
				list2.Add(HttpHeaderValueLexer.ReadKeyValuePair(ref lexer));
			}
			return new HttpHeaderValueElement(list2[0].Key, list2[0].Value, Enumerable.ToArray<KeyValuePair<string, string>>(Enumerable.Skip<KeyValuePair<string, string>>(list2, 1)));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030DC File Offset: 0x000012DC
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

		// Token: 0x0600006F RID: 111 RVA: 0x00003127 File Offset: 0x00001327
		private bool EndOfHeaderValue()
		{
			return this.startIndexOfNextItem == this.httpHeaderValue.Length;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000313C File Offset: 0x0000133C
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

		// Token: 0x06000071 RID: 113 RVA: 0x000031F8 File Offset: 0x000013F8
		private HttpHeaderValueLexer.HttpHeaderToken ReadNextToken()
		{
			HttpHeaderValueLexer httpHeaderValueLexer = this.ReadNextTokenOrQuotedString();
			if (httpHeaderValueLexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString)
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem));
			}
			return (HttpHeaderValueLexer.HttpHeaderToken)httpHeaderValueLexer;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003240 File Offset: 0x00001440
		private HttpHeaderValueLexer.HttpHeaderSeparator ReadNextSeparator()
		{
			string text = this.httpHeaderValue.Substring(this.startIndexOfNextItem, 1);
			if (text != "," && text != ";" && text != "=")
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_UnrecognizedSeparator(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem, text));
			}
			return new HttpHeaderValueLexer.HttpHeaderSeparator(this.httpHeaderName, this.httpHeaderValue, text, this.startIndexOfNextItem + 1);
		}

		// Token: 0x04000028 RID: 40
		internal const string ElementSeparator = ",";

		// Token: 0x04000029 RID: 41
		internal const string ParameterSeparator = ";";

		// Token: 0x0400002A RID: 42
		internal const string ValueSeparator = "=";

		// Token: 0x0400002B RID: 43
		private readonly string httpHeaderName;

		// Token: 0x0400002C RID: 44
		private readonly string httpHeaderValue;

		// Token: 0x0400002D RID: 45
		private readonly int startIndexOfNextItem;

		// Token: 0x0400002E RID: 46
		private readonly string value;

		// Token: 0x0400002F RID: 47
		private readonly string originalText;

		// Token: 0x02000239 RID: 569
		internal enum HttpHeaderValueItemType
		{
			// Token: 0x04000A8C RID: 2700
			Start,
			// Token: 0x04000A8D RID: 2701
			Token,
			// Token: 0x04000A8E RID: 2702
			QuotedString,
			// Token: 0x04000A8F RID: 2703
			ElementSeparator,
			// Token: 0x04000A90 RID: 2704
			ParameterSeparator,
			// Token: 0x04000A91 RID: 2705
			ValueSeparator,
			// Token: 0x04000A92 RID: 2706
			End
		}

		// Token: 0x0200023A RID: 570
		private sealed class HttpHeaderStart : HttpHeaderValueLexer
		{
			// Token: 0x060016ED RID: 5869 RVA: 0x00046675 File Offset: 0x00044875
			internal HttpHeaderStart(string httpHeaderName, string httpHeaderValue)
				: base(httpHeaderName, httpHeaderValue, null, null, 0)
			{
			}

			// Token: 0x17000526 RID: 1318
			// (get) Token: 0x060016EE RID: 5870 RVA: 0x00002500 File Offset: 0x00000700
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Start;
				}
			}

			// Token: 0x060016EF RID: 5871 RVA: 0x00046682 File Offset: 0x00044882
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (this.httpHeaderValue == null || base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextToken();
			}
		}

		// Token: 0x0200023B RID: 571
		private sealed class HttpHeaderToken : HttpHeaderValueLexer
		{
			// Token: 0x060016F0 RID: 5872 RVA: 0x000466A0 File Offset: 0x000448A0
			internal HttpHeaderToken(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x17000527 RID: 1319
			// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00002503 File Offset: 0x00000703
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Token;
				}
			}

			// Token: 0x060016F2 RID: 5874 RVA: 0x000466AE File Offset: 0x000448AE
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextSeparator();
			}
		}

		// Token: 0x0200023C RID: 572
		private sealed class HttpHeaderQuotedString : HttpHeaderValueLexer
		{
			// Token: 0x060016F3 RID: 5875 RVA: 0x000466C4 File Offset: 0x000448C4
			internal HttpHeaderQuotedString(string httpHeaderName, string httpHeaderValue, string value, string originalText, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, originalText, startIndexOfNextItem)
			{
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0002900C File Offset: 0x0002720C
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString;
				}
			}

			// Token: 0x060016F5 RID: 5877 RVA: 0x000466D4 File Offset: 0x000448D4
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

		// Token: 0x0200023D RID: 573
		private sealed class HttpHeaderSeparator : HttpHeaderValueLexer
		{
			// Token: 0x060016F6 RID: 5878 RVA: 0x000466A0 File Offset: 0x000448A0
			internal HttpHeaderSeparator(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00046744 File Offset: 0x00044944
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

			// Token: 0x060016F8 RID: 5880 RVA: 0x0004677C File Offset: 0x0004497C
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

		// Token: 0x0200023E RID: 574
		private sealed class HttpHeaderEnd : HttpHeaderValueLexer
		{
			// Token: 0x060016F9 RID: 5881 RVA: 0x000467D8 File Offset: 0x000449D8
			private HttpHeaderEnd()
				: base(null, null, null, null, -1)
			{
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x060016FA RID: 5882 RVA: 0x0002B862 File Offset: 0x00029A62
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.End;
				}
			}

			// Token: 0x060016FB RID: 5883 RVA: 0x0000B41B File Offset: 0x0000961B
			internal override HttpHeaderValueLexer ReadNext()
			{
				return null;
			}

			// Token: 0x04000A93 RID: 2707
			internal static readonly HttpHeaderValueLexer.HttpHeaderEnd Instance = new HttpHeaderValueLexer.HttpHeaderEnd();
		}
	}
}
