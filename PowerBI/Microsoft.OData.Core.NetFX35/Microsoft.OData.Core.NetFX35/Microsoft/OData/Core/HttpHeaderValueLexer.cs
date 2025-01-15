using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Core
{
	// Token: 0x02000097 RID: 151
	internal abstract class HttpHeaderValueLexer
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x00014F48 File Offset: 0x00013148
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

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00014F96 File Offset: 0x00013196
		internal string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00014F9E File Offset: 0x0001319E
		internal string OriginalText
		{
			get
			{
				return this.originalText;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005D2 RID: 1490
		internal abstract HttpHeaderValueLexer.HttpHeaderValueItemType Type { get; }

		// Token: 0x060005D3 RID: 1491 RVA: 0x00014FA6 File Offset: 0x000131A6
		internal static HttpHeaderValueLexer Create(string httpHeaderName, string httpHeaderValue)
		{
			return new HttpHeaderValueLexer.HttpHeaderStart(httpHeaderName, httpHeaderValue);
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00014FB0 File Offset: 0x000131B0
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

		// Token: 0x060005D5 RID: 1493
		internal abstract HttpHeaderValueLexer ReadNext();

		// Token: 0x060005D6 RID: 1494 RVA: 0x00015004 File Offset: 0x00013204
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

		// Token: 0x060005D7 RID: 1495 RVA: 0x00015078 File Offset: 0x00013278
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

		// Token: 0x060005D8 RID: 1496 RVA: 0x000150C3 File Offset: 0x000132C3
		private bool EndOfHeaderValue()
		{
			return this.startIndexOfNextItem == this.httpHeaderValue.Length;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000150E0 File Offset: 0x000132E0
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

		// Token: 0x060005DA RID: 1498 RVA: 0x00015198 File Offset: 0x00013398
		private HttpHeaderValueLexer.HttpHeaderToken ReadNextToken()
		{
			HttpHeaderValueLexer httpHeaderValueLexer = this.ReadNextTokenOrQuotedString();
			if (httpHeaderValueLexer.Type == HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString)
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_TokenExpectedButFoundQuotedString(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem));
			}
			return (HttpHeaderValueLexer.HttpHeaderToken)httpHeaderValueLexer;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000151E0 File Offset: 0x000133E0
		private HttpHeaderValueLexer.HttpHeaderSeparator ReadNextSeparator()
		{
			string text = this.httpHeaderValue.Substring(this.startIndexOfNextItem, 1);
			if (text != "," && text != ";" && text != "=")
			{
				throw new ODataException(Strings.HttpHeaderValueLexer_UnrecognizedSeparator(this.httpHeaderName, this.httpHeaderValue, this.startIndexOfNextItem, text));
			}
			return new HttpHeaderValueLexer.HttpHeaderSeparator(this.httpHeaderName, this.httpHeaderValue, text, this.startIndexOfNextItem + 1);
		}

		// Token: 0x04000271 RID: 625
		internal const string ElementSeparator = ",";

		// Token: 0x04000272 RID: 626
		internal const string ParameterSeparator = ";";

		// Token: 0x04000273 RID: 627
		internal const string ValueSeparator = "=";

		// Token: 0x04000274 RID: 628
		private readonly string httpHeaderName;

		// Token: 0x04000275 RID: 629
		private readonly string httpHeaderValue;

		// Token: 0x04000276 RID: 630
		private readonly int startIndexOfNextItem;

		// Token: 0x04000277 RID: 631
		private readonly string value;

		// Token: 0x04000278 RID: 632
		private readonly string originalText;

		// Token: 0x02000098 RID: 152
		internal enum HttpHeaderValueItemType
		{
			// Token: 0x0400027B RID: 635
			Start,
			// Token: 0x0400027C RID: 636
			Token,
			// Token: 0x0400027D RID: 637
			QuotedString,
			// Token: 0x0400027E RID: 638
			ElementSeparator,
			// Token: 0x0400027F RID: 639
			ParameterSeparator,
			// Token: 0x04000280 RID: 640
			ValueSeparator,
			// Token: 0x04000281 RID: 641
			End
		}

		// Token: 0x02000099 RID: 153
		private sealed class HttpHeaderStart : HttpHeaderValueLexer
		{
			// Token: 0x060005DD RID: 1501 RVA: 0x00015264 File Offset: 0x00013464
			internal HttpHeaderStart(string httpHeaderName, string httpHeaderValue)
				: base(httpHeaderName, httpHeaderValue, null, null, 0)
			{
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060005DE RID: 1502 RVA: 0x00015271 File Offset: 0x00013471
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Start;
				}
			}

			// Token: 0x060005DF RID: 1503 RVA: 0x00015274 File Offset: 0x00013474
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (this.httpHeaderValue == null || base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextToken();
			}
		}

		// Token: 0x0200009A RID: 154
		private sealed class HttpHeaderToken : HttpHeaderValueLexer
		{
			// Token: 0x060005E0 RID: 1504 RVA: 0x00015292 File Offset: 0x00013492
			internal HttpHeaderToken(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060005E1 RID: 1505 RVA: 0x000152A0 File Offset: 0x000134A0
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.Token;
				}
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x000152A3 File Offset: 0x000134A3
			internal override HttpHeaderValueLexer ReadNext()
			{
				if (base.EndOfHeaderValue())
				{
					return HttpHeaderValueLexer.HttpHeaderEnd.Instance;
				}
				return base.ReadNextSeparator();
			}
		}

		// Token: 0x0200009B RID: 155
		private sealed class HttpHeaderQuotedString : HttpHeaderValueLexer
		{
			// Token: 0x060005E3 RID: 1507 RVA: 0x000152B9 File Offset: 0x000134B9
			internal HttpHeaderQuotedString(string httpHeaderName, string httpHeaderValue, string value, string originalText, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, originalText, startIndexOfNextItem)
			{
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000152C8 File Offset: 0x000134C8
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.QuotedString;
				}
			}

			// Token: 0x060005E5 RID: 1509 RVA: 0x000152CC File Offset: 0x000134CC
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

		// Token: 0x0200009C RID: 156
		private sealed class HttpHeaderSeparator : HttpHeaderValueLexer
		{
			// Token: 0x060005E6 RID: 1510 RVA: 0x0001533B File Offset: 0x0001353B
			internal HttpHeaderSeparator(string httpHeaderName, string httpHeaderValue, string value, int startIndexOfNextItem)
				: base(httpHeaderName, httpHeaderValue, value, value, startIndexOfNextItem)
			{
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001534C File Offset: 0x0001354C
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

			// Token: 0x060005E8 RID: 1512 RVA: 0x00015384 File Offset: 0x00013584
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

		// Token: 0x0200009D RID: 157
		private sealed class HttpHeaderEnd : HttpHeaderValueLexer
		{
			// Token: 0x060005E9 RID: 1513 RVA: 0x000153E0 File Offset: 0x000135E0
			private HttpHeaderEnd()
				: base(null, null, null, null, -1)
			{
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x060005EA RID: 1514 RVA: 0x000153ED File Offset: 0x000135ED
			internal override HttpHeaderValueLexer.HttpHeaderValueItemType Type
			{
				get
				{
					return HttpHeaderValueLexer.HttpHeaderValueItemType.End;
				}
			}

			// Token: 0x060005EB RID: 1515 RVA: 0x000153F0 File Offset: 0x000135F0
			internal override HttpHeaderValueLexer ReadNext()
			{
				return null;
			}

			// Token: 0x04000282 RID: 642
			internal static readonly HttpHeaderValueLexer.HttpHeaderEnd Instance = new HttpHeaderValueLexer.HttpHeaderEnd();
		}
	}
}
