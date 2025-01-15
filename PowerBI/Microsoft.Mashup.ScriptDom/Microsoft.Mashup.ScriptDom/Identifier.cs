using System;
using System.Text;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x020000B8 RID: 184
	[Serializable]
	internal class Identifier : TSqlFragment
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public static string DecodeIdentifier(string identifier, out QuoteType quote)
		{
			if (identifier == null || identifier.Length <= 2)
			{
				quote = QuoteType.NotQuoted;
				return identifier;
			}
			if (identifier.Length == 0 || (identifier.get_Chars(0) != '[' && identifier.get_Chars(0) != '"'))
			{
				quote = QuoteType.NotQuoted;
				return identifier;
			}
			string text = identifier.Substring(1, identifier.Length - 2);
			if (identifier.get_Chars(0) == '[')
			{
				quote = QuoteType.SquareBracket;
				return text.Replace("]]", "]");
			}
			quote = QuoteType.DoubleQuote;
			return text.Replace("\"\"", "\"");
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000C828 File Offset: 0x0000AA28
		public static string EncodeIdentifier(string identifier)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(identifier.Replace("]", "]]"));
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C870 File Offset: 0x0000AA70
		public static string EncodeIdentifier(string identifier, QuoteType quoteType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			switch (quoteType)
			{
			case QuoteType.NotQuoted:
				return identifier;
			case QuoteType.SquareBracket:
				stringBuilder.Append("[");
				stringBuilder.Append(identifier.Replace("]", "]]"));
				stringBuilder.Append("]");
				break;
			case QuoteType.DoubleQuote:
				stringBuilder.Append("\"");
				stringBuilder.Append(identifier.Replace("\"", "\"\""));
				stringBuilder.Append("\"");
				break;
			default:
				throw new ArgumentOutOfRangeException("quoteType");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C90E File Offset: 0x0000AB0E
		internal void SetUnquotedIdentifier(string text)
		{
			this._value = text;
			this._quoteType = QuoteType.NotQuoted;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C91E File Offset: 0x0000AB1E
		internal void SetIdentifier(string text)
		{
			this._value = Identifier.DecodeIdentifier(text, out this._quoteType);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000C932 File Offset: 0x0000AB32
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000C93A File Offset: 0x0000AB3A
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000C943 File Offset: 0x0000AB43
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000C94B File Offset: 0x0000AB4B
		public QuoteType QuoteType
		{
			get
			{
				return this._quoteType;
			}
			set
			{
				this._quoteType = value;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C954 File Offset: 0x0000AB54
		public override void Accept(TSqlFragmentVisitor visitor)
		{
			if (visitor != null)
			{
				visitor.ExplicitVisit(this);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C960 File Offset: 0x0000AB60
		public override void AcceptChildren(TSqlFragmentVisitor visitor)
		{
			base.AcceptChildren(visitor);
		}

		// Token: 0x040005A1 RID: 1441
		private const string EscapedRSquareParen = "]]";

		// Token: 0x040005A2 RID: 1442
		private const string EscapedQuote = "\"\"";

		// Token: 0x040005A3 RID: 1443
		private const string Quote = "\"";

		// Token: 0x040005A4 RID: 1444
		private const char LSquareParenChar = '[';

		// Token: 0x040005A5 RID: 1445
		private const char RSquareParenChar = ']';

		// Token: 0x040005A6 RID: 1446
		private const char QuoteChar = '"';

		// Token: 0x040005A7 RID: 1447
		internal const int MaxIdentifierLength = 128;

		// Token: 0x040005A8 RID: 1448
		private string _value;

		// Token: 0x040005A9 RID: 1449
		private QuoteType _quoteType;
	}
}
