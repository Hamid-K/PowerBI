using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData
{
	// Token: 0x02000123 RID: 291
	internal abstract class HttpHeaderValueLexer
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x0001976C File Offset: 0x0001796C
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x000197BA File Offset: 0x000179BA
		internal string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x000197C2 File Offset: 0x000179C2
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007A8 RID: 1960
		internal abstract HttpHeaderValueLexer.HttpHeaderValueItemType Type { get; }

		// Token: 0x060007A9 RID: 1961 RVA: 0x000197CA File Offset: 0x000179CA
		internal static HttpHeaderValueLexer Create(string httpHeaderName, string httpHeaderValue)
		{
			return new HttpHeaderValueLexer.HttpHeaderStart(httpHeaderName, httpHeaderValue);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000197D4 File Offset: 0x000179D4
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

		// Token: 0x060007AB RID: 1963
		internal abstract HttpHeaderValueLexer ReadNext();

		// Token: 0x060007AC RID: 1964 RVA: 0x00019828 File Offset: 0x00017A28
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

		// Token: 0x060007AD RID: 1965 RVA: 0x0001989C File Offset: 0x00017A9C
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

		// Token: 0x060007AE RID: 1966 RVA: 0x000198E7 File Offset: 0x00017AE7
		private bool EndOfHeaderValue()
		{
			return this.startIndexOfNextItem == this.httpHeaderValue.Length;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00019904 File Offset: 0x00017B04
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

		// Token: 0x060007B0 RID: 1968 RVA: 0x000199BC File Offset: 0x00017BBC
		private HttpHeaderValueLexer.HttpHeaderToken ReadNextToken()
		{
			HttpHeaderValueLexer httpHeaderValueLexer = this.ReadNextTokenOrQuotedString();
			if (httpHeaderValueLexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString)
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem));
			}
			return (HttpHeaderValueLexer.HttpHeaderToken)httpHeaderValueLexer;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00019A04 File Offset: 0x00017C04
		private HttpHeaderValueLexer.HttpHeaderSeparator ReadNextSeparator()
		{
			string text = this.httpHeaderValue.Substring(this.startIndexOfNextItem, 1);
			if (text != "," && text != ";" && text != "=")
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_UnrecognizedSeparator(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem, text));
			}
			return new HttpHeaderValueLexer.HttpHeaderSeparator(this.httpHeaderName, this.httpHeaderValue, text, this.startIndexOfNextItem + 1);
		}

		// Token: 0x040002ED RID: 749
		internal const string ElementSeparator = ",";

		// Token: 0x040002EE RID: 750
		internal const string ParameterSeparator = ";";

		// Token: 0x040002EF RID: 751
		internal const string ValueSeparator = "=";

		// Token: 0x040002F0 RID: 752
		private readonly string httpHeaderName;

		// Token: 0x040002F1 RID: 753
		private readonly string httpHeaderValue;

		// Token: 0x040002F2 RID: 754
		private readonly int startIndexOfNextItem;

		// Token: 0x040002F3 RID: 755
		private readonly string value;

		// Token: 0x040002F4 RID: 756
		private readonly string originalText;

		// Token: 0x02000124 RID: 292
		internal enum HttpHeaderValueItemType
		{
			// Token: 0x040002F7 RID: 759
			Start,
			// Token: 0x040002F8 RID: 760
			Token,
			// Token: 0x040002F9 RID: 761
			QuotedString,
			// Token: 0x040002FA RID: 762
			ElementSeparator,
			// Token: 0x040002FB RID: 763
			ParameterSeparator,
			// Token: 0x040002FC RID: 764
			ValueSeparator,
			// Token: 0x040002FD RID: 765
			End
		}

		// Token: 0x02000125 RID: 293
		private sealed class HttpHeaderStart : HttpHeaderValueLexer
		{
			// Token: 0x060007B3 RID: 1971 RVA: 0x00019A88 File Offset: 0x00017C88
			internal HttpHeaderStart(string httpHeaderName, string httpHeaderValue)
				: base(httpHeaderName, httpHeaderValue, null, null, 0)
			{
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00019A95 File Offset: 0x00017C95
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Start;
				}
			}

			// Token: 0x060007B5 RID: 1973 RVA: 0x00019A98 File Offset: 0x00017C98
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (this.httpHeaderValue == null || base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextToken();
			}
		}

		// Token: 0x02000126 RID: 294
		private sealed class HttpHeaderToken : HttpHeaderValueLexer
		{
			// Token: 0x060007B6 RID: 1974 RVA: 0x00019AB6 File Offset: 0x00017CB6
			internal HttpHeaderToken(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00019AC4 File Offset: 0x00017CC4
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Token;
				}
			}

			// Token: 0x060007B8 RID: 1976 RVA: 0x00019AC7 File Offset: 0x00017CC7
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextSeparator();
			}
		}

		// Token: 0x02000127 RID: 295
		private sealed class HttpHeaderQuotedString : HttpHeaderValueLexer
		{
			// Token: 0x060007B9 RID: 1977 RVA: 0x00019ADD File Offset: 0x00017CDD
			internal HttpHeaderQuotedString(string httpHeaderName, string httpHeaderValue, string value, string originalText, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, originalText, startIndexOfNextItem)
			{
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x060007BA RID: 1978 RVA: 0x00019AEC File Offset: 0x00017CEC
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString;
				}
			}

			// Token: 0x060007BB RID: 1979 RVA: 0x00019AF0 File Offset: 0x00017CF0
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

		// Token: 0x02000128 RID: 296
		private sealed class HttpHeaderSeparator : HttpHeaderValueLexer
		{
			// Token: 0x060007BC RID: 1980 RVA: 0x00019B5F File Offset: 0x00017D5F
			internal HttpHeaderSeparator(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060007BD RID: 1981 RVA: 0x00019B70 File Offset: 0x00017D70
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					string value;
					if ((value = base.Value) != null)
					{
						if (value == ",")
						{
							return HttpHeaderValueLexer.HttpHeaderValueItemType.ElementSeparator;
						}
						if (value == ";")
						{
							return HttpHeaderValueLexer.HttpHeaderValueItemType.ParameterSeparator;
						}
					}
					return HttpHeaderValueLexer.HttpHeaderValueItemType.ValueSeparator;
				}
			}

			// Token: 0x060007BE RID: 1982 RVA: 0x00019BA8 File Offset: 0x00017DA8
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

		// Token: 0x02000129 RID: 297
		private sealed class HttpHeaderEnd : HttpHeaderValueLexer
		{
			// Token: 0x060007BF RID: 1983 RVA: 0x00019C04 File Offset: 0x00017E04
			private HttpHeaderEnd()
				: base(null, null, null, null, -1)
			{
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00019C11 File Offset: 0x00017E11
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.End;
				}
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x00019C14 File Offset: 0x00017E14
			internal override HttpHeaderValueLexer ReadNext()
			{
				return null;
			}

			// Token: 0x040002FE RID: 766
			internal static readonly HttpHeaderValueLexer.HttpHeaderEnd Instance = new HttpHeaderValueLexer.HttpHeaderEnd();
		}
	}
}
