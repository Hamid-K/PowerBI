using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200157B RID: 5499
	public static class LiteralValue
	{
		// Token: 0x060088DC RID: 35036 RVA: 0x001CF790 File Offset: 0x001CD990
		public static LiteralKind GetKind(Value value)
		{
			if (value.IsLogical)
			{
				return LiteralKind.Logical;
			}
			if (value.IsNull)
			{
				return LiteralKind.Null;
			}
			if (value.IsNumber)
			{
				return LiteralKind.Number;
			}
			if (value.IsText)
			{
				return LiteralKind.Text;
			}
			return LiteralKind.Unknown;
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x001CF7BC File Offset: 0x001CD9BC
		private static bool TryGetPredefinedIdentifier(string text, int offset, int length, out Value value)
		{
			switch (length)
			{
			case 4:
				if (string.CompareOrdinal("#nan", 0, text, offset, length) == 0)
				{
					value = NumberValue.NaN;
					return true;
				}
				break;
			case 5:
				if (string.CompareOrdinal("#date", 0, text, offset, length) == 0)
				{
					value = Library.Date.date;
					return true;
				}
				if (string.CompareOrdinal("#time", 0, text, offset, length) == 0)
				{
					value = Library.Time.time;
					return true;
				}
				break;
			case 6:
				if (string.CompareOrdinal("#table", 0, text, offset, length) == 0)
				{
					value = TableModule.Table.Literal;
					return true;
				}
				break;
			case 7:
				if (string.CompareOrdinal("#binary", 0, text, offset, length) == 0)
				{
					value = Library.Binary.Literal;
					return true;
				}
				break;
			case 9:
				if (string.CompareOrdinal("#datetime", 0, text, offset, length) == 0)
				{
					value = Library.DateTime.datetime;
					return true;
				}
				if (string.CompareOrdinal("#duration", 0, text, offset, length) == 0)
				{
					value = Library.Duration.duration;
					return true;
				}
				if (string.CompareOrdinal("#infinity", 0, text, offset, length) == 0)
				{
					value = NumberValue.Infinity;
					return true;
				}
				break;
			case 13:
				if (string.CompareOrdinal("#datetimezone", 0, text, offset, length) == 0)
				{
					value = Library.DateTimeZone.datetimezone;
					return true;
				}
				break;
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x060088DE RID: 35038 RVA: 0x001CF8ED File Offset: 0x001CDAED
		private static bool TryGetDocumentedPredefinedIdentifier(string text, int offset, int length, out Value value)
		{
			if (LiteralValue.TryGetPredefinedIdentifier(text, offset, length, out value))
			{
				if (value.IsFunction)
				{
					value = value.AddHelp(text.Substring(offset, length), null);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060088DF RID: 35039 RVA: 0x001CF918 File Offset: 0x001CDB18
		public static bool TryParseSourceValue(string text, out Value value)
		{
			if (LiteralValue.TryParseValue(text, out value))
			{
				return true;
			}
			try
			{
				LiteralValue.TokenReader tokenReader = new LiteralValue.TokenReader(text);
				tokenReader.Read(TokenType.Bof);
				value = tokenReader.ReadLiteral();
				tokenReader.Read(TokenType.Eof);
				return true;
			}
			catch (ValueException)
			{
			}
			try
			{
				LiteralValue.TokenReader tokenReader2 = new LiteralValue.TokenReader(text);
				tokenReader2.Read(TokenType.Bof);
				Value value2 = tokenReader2.ReadLiteral();
				tokenReader2.Read(TokenType.LeftParen);
				List<Value> list = new List<Value>();
				do
				{
					list.Add(tokenReader2.ReadLiteral());
				}
				while (tokenReader2.TryRead(TokenType.Comma));
				tokenReader2.Read(TokenType.RightParen);
				tokenReader2.Read(TokenType.Eof);
				value = value2.AsFunction.Invoke(list.ToArray());
				return true;
			}
			catch (ValueException)
			{
			}
			value = null;
			return false;
		}

		// Token: 0x060088E0 RID: 35040 RVA: 0x001CF9E8 File Offset: 0x001CDBE8
		public static bool TryParseValue(string text, out Value value)
		{
			return LiteralValue.TryParseValue(text, 0, text.Length, out value);
		}

		// Token: 0x060088E1 RID: 35041 RVA: 0x001CF9F8 File Offset: 0x001CDBF8
		public static bool TryParseValue(StringSegment text, out Value value)
		{
			return LiteralValue.TryParseValue(text.String, text.Offset, text.Length, out value);
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x001CFA18 File Offset: 0x001CDC18
		public static bool TryParseValue(string text, int offset, int length, out Value value)
		{
			LiteralKind literalKind;
			if (!LiteralValue.TryParseKind(text, offset, length, out literalKind))
			{
				value = Value.Null;
				return false;
			}
			return LiteralValue.TryParseValue(text, offset, length, literalKind, out value);
		}

		// Token: 0x060088E3 RID: 35043 RVA: 0x001CFA44 File Offset: 0x001CDC44
		public static bool TryParseKind(StringSegment text, out LiteralKind kind)
		{
			return LiteralValue.TryParseKind(text.String, text.Offset, text.Length, out kind);
		}

		// Token: 0x060088E4 RID: 35044 RVA: 0x001CFA64 File Offset: 0x001CDC64
		public static bool TryParseKind(string text, int offset, int length, out LiteralKind kind)
		{
			if (length < 0 || length > text.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset < 0 || offset > text.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length > 0)
			{
				char c = text[offset];
				if ((c >= '0' && c <= '9') || c == '.')
				{
					kind = LiteralKind.Number;
					return true;
				}
				if (c == '"')
				{
					kind = LiteralKind.Text;
					return true;
				}
				if (length == 4 && string.CompareOrdinal("null", 0, text, offset, 4) == 0)
				{
					kind = LiteralKind.Null;
					return true;
				}
				if (length == 4 && string.CompareOrdinal("true", 0, text, offset, 4) == 0)
				{
					kind = LiteralKind.Logical;
					return true;
				}
				if (length == 5 && string.CompareOrdinal("false", 0, text, offset, 5) == 0)
				{
					kind = LiteralKind.Logical;
					return true;
				}
				if (c == '#')
				{
					kind = LiteralKind.PredefinedIdentifier;
					return true;
				}
			}
			kind = LiteralKind.Unknown;
			return false;
		}

		// Token: 0x060088E5 RID: 35045 RVA: 0x001CFB28 File Offset: 0x001CDD28
		private static bool TryParseValue(string text, int offset, int length, LiteralKind kind, out Value value)
		{
			if (length < 0 || length > text.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset < 0 || offset > text.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			switch (kind)
			{
			case LiteralKind.Logical:
				if (length == 4 && string.CompareOrdinal("true", 0, text, offset, 4) == 0)
				{
					value = LogicalValue.True;
					return true;
				}
				if (length == 5 && string.CompareOrdinal("false", 0, text, offset, 5) == 0)
				{
					value = LogicalValue.False;
					return true;
				}
				break;
			case LiteralKind.Number:
				return LiteralValue.TryGetNumberLiteralValue(text, offset, length, out value);
			case LiteralKind.Null:
				if (length == 4 && string.CompareOrdinal("null", 0, text, offset, 4) == 0)
				{
					value = Value.Null;
					return true;
				}
				break;
			case LiteralKind.PredefinedIdentifier:
				return LiteralValue.TryGetDocumentedPredefinedIdentifier(text, offset, length, out value);
			case LiteralKind.Text:
				return LiteralValue.TryGetStringLiteralValue(text, offset, length, out value);
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x060088E6 RID: 35046 RVA: 0x001CFC0C File Offset: 0x001CDE0C
		private static bool TryGetHexDigit(char ch, out uint digit)
		{
			if (ch >= '0' && ch <= '9')
			{
				digit = (uint)(ch - '0');
				return true;
			}
			if (ch >= 'A' && ch <= 'F')
			{
				digit = (uint)(ch - 'A' + '\n');
				return true;
			}
			if (ch >= 'a' && ch <= 'f')
			{
				digit = (uint)(ch - 'a' + '\n');
				return true;
			}
			digit = 0U;
			return false;
		}

		// Token: 0x060088E7 RID: 35047 RVA: 0x001CFC5C File Offset: 0x001CDE5C
		private static bool TryGetHexUlong(string text, int offset, int length, out ulong value)
		{
			if (length <= 8)
			{
				ulong num = 0UL;
				for (int i = 0; i < length; i++)
				{
					uint num2;
					if (!LiteralValue.TryGetHexDigit(text[offset + i], out num2))
					{
						value = 0UL;
						return false;
					}
					num = num * 16UL + (ulong)num2;
				}
				value = num;
				return true;
			}
			value = 0UL;
			return false;
		}

		// Token: 0x060088E8 RID: 35048 RVA: 0x001CFCA8 File Offset: 0x001CDEA8
		internal static bool TryGetEscapedCharacter(string text, int offset, int length, out ulong value)
		{
			switch (length)
			{
			case 1:
				if (text[offset] == '#')
				{
					value = 35UL;
					return true;
				}
				break;
			case 2:
				if (text[offset] == 'l' && text[offset + 1] == 'f')
				{
					value = 10UL;
					return true;
				}
				if (text[offset] == 'c' && text[offset + 1] == 'r')
				{
					value = 13UL;
					return true;
				}
				break;
			case 3:
				if (text[offset] == 't' && text[offset + 1] == 'a' && text[offset + 2] == 'b')
				{
					value = 9UL;
					return true;
				}
				break;
			case 4:
			{
				ulong num;
				if (LiteralValue.TryGetHexUlong(text, offset, length, out num))
				{
					value = (ulong)((ushort)num);
					return true;
				}
				break;
			}
			case 8:
			{
				ulong num2;
				if (LiteralValue.TryGetHexUlong(text, offset, length, out num2) && num2 <= 1114111UL && (num2 >= 65535UL || !char.IsSurrogate((char)num2)))
				{
					value = num2;
					return true;
				}
				break;
			}
			}
			value = 0UL;
			return false;
		}

		// Token: 0x060088E9 RID: 35049 RVA: 0x001CFDA8 File Offset: 0x001CDFA8
		private static bool TryGetEscapedCharacters(string text, int offset, int length, StringBuilder builder)
		{
			if (length <= 0)
			{
				return false;
			}
			int num = offset - 1;
			for (;;)
			{
				int num2 = num + 1;
				num = LiteralValue.GetIndexOf(text, num2, offset + length - num2, ',');
				ulong num3;
				if (!LiteralValue.TryGetEscapedCharacter(text, num2, num - num2, out num3))
				{
					break;
				}
				if (num3 > 65535UL)
				{
					builder.Append(char.ConvertFromUtf32((int)num3));
				}
				else
				{
					builder.Append((char)num3);
				}
				if (num >= offset + length)
				{
					goto Block_4;
				}
			}
			return false;
			Block_4:
			return num == offset + length;
		}

		// Token: 0x060088EA RID: 35050 RVA: 0x001CFE10 File Offset: 0x001CE010
		private static bool TryGetStringLiteralValue(string text, int offset, int length, out Value value)
		{
			string text2;
			if (LiteralValue.TryParseStringLiteral(text, offset, length, out text2))
			{
				value = TextValue.New(text2);
				return true;
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x060088EB RID: 35051 RVA: 0x001CFE3C File Offset: 0x001CE03C
		public static bool TryParseStringLiteral(string text, int offset, int length, out string value)
		{
			if (length < 0 || length > text.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (offset < 0 || offset > text.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (length >= 2 && text[offset] == '"' && text[offset + length - 1] == '"')
			{
				for (int i = 1; i < length - 1; i++)
				{
					if (text[offset + i] == '#' || text[offset + i] == '"')
					{
						return LiteralValue.TryGetEscapedString(text, offset + 1, length - 2, out value);
					}
				}
				value = text.Substring(offset + 1, length - 2);
				return true;
			}
			value = string.Empty;
			return false;
		}

		// Token: 0x060088EC RID: 35052 RVA: 0x001CFEE8 File Offset: 0x001CE0E8
		public static bool TryGetEscapedString(string text, int offset, int length, out string value)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < length)
			{
				char c = text[offset + i];
				if (c != '"')
				{
					if (c != '#')
					{
						stringBuilder.Append(c);
						i++;
					}
					else if (i + 1 >= length || text[offset + i + 1] != '(')
					{
						stringBuilder.Append(c);
						i++;
					}
					else
					{
						int num = offset + i + 2;
						int indexOf = LiteralValue.GetIndexOf(text, num, offset + length - num, ')');
						if (indexOf >= offset + length)
						{
							value = null;
							return false;
						}
						if (!LiteralValue.TryGetEscapedCharacters(text, num, indexOf - num, stringBuilder))
						{
							value = null;
							return false;
						}
						i = indexOf - offset + 1;
					}
				}
				else
				{
					if (i + 1 >= length || text[offset + i + 1] != '"')
					{
						value = null;
						return false;
					}
					stringBuilder.Append(c);
					i += 2;
				}
			}
			value = stringBuilder.ToString();
			return true;
		}

		// Token: 0x060088ED RID: 35053 RVA: 0x001CFFC0 File Offset: 0x001CE1C0
		private static int GetIndexOf(string text, int offset, int length, char target)
		{
			int num = text.IndexOf(target, offset, length);
			if (num >= 0)
			{
				return num;
			}
			return offset + length;
		}

		// Token: 0x060088EE RID: 35054 RVA: 0x001CFFE0 File Offset: 0x001CE1E0
		private static bool TryGetHexNumberValue(string text, int offset, int length, out Value value)
		{
			ulong num = 0UL;
			for (int i = 0; i < length; i++)
			{
				uint num2;
				if (!LiteralValue.TryGetHexDigit(text[offset + i], out num2))
				{
					value = Value.Null;
					return false;
				}
				num = num * 16UL + (ulong)num2;
			}
			value = NumberValue.New(num);
			return true;
		}

		// Token: 0x060088EF RID: 35055 RVA: 0x001D002C File Offset: 0x001CE22C
		private static bool TryGetNumberLiteralValue(string text, int offset, int length, out Value value)
		{
			if (length > 2)
			{
				char c = text[offset + 1];
				if (c == 'x' || c == 'X')
				{
					return LiteralValue.TryGetHexNumberValue(text, offset + 2, length - 2, out value);
				}
			}
			NumberValue numberValue;
			TypeValue typeValue;
			if (NumberValue.TryParse(text, offset, length, NumberStyles.Float, CultureInfo.InvariantCulture, out numberValue, out typeValue))
			{
				value = numberValue;
				return true;
			}
			value = Value.Null;
			return false;
		}

		// Token: 0x04004BAE RID: 19374
		public const string DurationConstructor = "#duration";

		// Token: 0x04004BAF RID: 19375
		public const string DateTimeZoneConstructor = "#datetimezone";

		// Token: 0x04004BB0 RID: 19376
		public const string DateTimeConstructor = "#datetime";

		// Token: 0x04004BB1 RID: 19377
		public const string DateConstructor = "#date";

		// Token: 0x04004BB2 RID: 19378
		public const string TimeConstructor = "#time";

		// Token: 0x04004BB3 RID: 19379
		public const string Infinity = "#infinity";

		// Token: 0x04004BB4 RID: 19380
		public const string NaN = "#nan";

		// Token: 0x04004BB5 RID: 19381
		public const string Table = "#table";

		// Token: 0x04004BB6 RID: 19382
		public const string Binary = "#binary";

		// Token: 0x0200157C RID: 5500
		private struct TokenReader
		{
			// Token: 0x060088F0 RID: 35056 RVA: 0x001D0084 File Offset: 0x001CE284
			public TokenReader(string text)
			{
				this.tokens = TextTokens.New(SegmentedString.New(text));
				this.index = 0;
			}

			// Token: 0x060088F1 RID: 35057 RVA: 0x001D009E File Offset: 0x001CE29E
			private TokenType GetTokenType(int index)
			{
				return this.tokens.GetType(this.tokens.GetToken(index));
			}

			// Token: 0x060088F2 RID: 35058 RVA: 0x001D00B7 File Offset: 0x001CE2B7
			public TokenType Peek()
			{
				return this.GetTokenType(this.index);
			}

			// Token: 0x060088F3 RID: 35059 RVA: 0x001D00C5 File Offset: 0x001CE2C5
			public void Read(TokenType tokenType)
			{
				if (!this.TryRead(tokenType))
				{
					throw ValueException.NewExpressionError("Expected token " + tokenType.ToString(), null, null);
				}
			}

			// Token: 0x060088F4 RID: 35060 RVA: 0x001D00EF File Offset: 0x001CE2EF
			public bool TryRead(TokenType tokenType)
			{
				this.SkipWhitespace();
				if (this.Peek() != tokenType)
				{
					return false;
				}
				this.index++;
				return true;
			}

			// Token: 0x060088F5 RID: 35061 RVA: 0x001D0114 File Offset: 0x001CE314
			public Value ReadLiteral()
			{
				this.SkipWhitespace();
				bool flag = this.TryRead(TokenType.Minus);
				if (!flag)
				{
					this.TryRead(TokenType.Plus);
				}
				StringSegment text = this.tokens.GetText(this.tokens.GetToken(this.index));
				this.Read(TokenType.Literal);
				Value value;
				if (!LiteralValue.TryParseValue(text, out value))
				{
					throw ValueException.NewExpressionError("Unable to interpret " + text.ToString() + " as a literal.", null, null);
				}
				if (!flag)
				{
					return value;
				}
				return value.Negate();
			}

			// Token: 0x060088F6 RID: 35062 RVA: 0x001D0197 File Offset: 0x001CE397
			private void SkipWhitespace()
			{
				while (this.GetTokenType(this.index) == TokenType.Whitespace)
				{
					this.index++;
				}
			}

			// Token: 0x04004BB7 RID: 19383
			private ITokens tokens;

			// Token: 0x04004BB8 RID: 19384
			private int index;
		}
	}
}
