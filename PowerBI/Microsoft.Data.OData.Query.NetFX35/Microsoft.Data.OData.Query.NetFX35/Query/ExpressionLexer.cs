using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200003E RID: 62
	[DebuggerDisplay("ExpressionLexer ({text} @ {textPos} [{token}]")]
	internal class ExpressionLexer
	{
		// Token: 0x06000172 RID: 370 RVA: 0x000089BC File Offset: 0x00006BBC
		internal ExpressionLexer(string expression, bool moveToFirstToken)
		{
			this.text = expression;
			this.textLen = this.text.Length;
			this.SetTextPos(0);
			if (moveToFirstToken)
			{
				this.NextToken();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000089ED File Offset: 0x00006BED
		// (set) Token: 0x06000174 RID: 372 RVA: 0x000089F5 File Offset: 0x00006BF5
		internal ExpressionToken CurrentToken
		{
			get
			{
				return this.token;
			}
			set
			{
				this.token = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000089FE File Offset: 0x00006BFE
		internal string ExpressionText
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00008A06 File Offset: 0x00006C06
		internal int Position
		{
			get
			{
				return this.token.Position;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008A13 File Offset: 0x00006C13
		internal static bool IsNumeric(ExpressionTokenKind id)
		{
			return id == ExpressionTokenKind.IntegerLiteral || id == ExpressionTokenKind.DecimalLiteral || id == ExpressionTokenKind.DoubleLiteral || id == ExpressionTokenKind.Int64Literal || id == ExpressionTokenKind.SingleLiteral;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008A2E File Offset: 0x00006C2E
		internal static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008A38 File Offset: 0x00006C38
		internal bool TryPeekNextToken(out ExpressionToken resultToken, out Exception error)
		{
			int num = this.textPos;
			char c = this.ch;
			ExpressionToken expressionToken = this.token;
			resultToken = this.NextTokenImplementation(out error);
			this.textPos = num;
			this.ch = c;
			this.token = expressionToken;
			return error == null;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008A84 File Offset: 0x00006C84
		internal ExpressionToken NextToken()
		{
			Exception ex = null;
			ExpressionToken expressionToken = this.NextTokenImplementation(out ex);
			if (ex != null)
			{
				throw ex;
			}
			return expressionToken;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008AA4 File Offset: 0x00006CA4
		internal string ReadDottedIdentifier()
		{
			this.ValidateToken(ExpressionTokenKind.Identifier);
			StringBuilder stringBuilder = null;
			string text = this.CurrentToken.Text;
			this.NextToken();
			while (this.CurrentToken.Kind == ExpressionTokenKind.Dot)
			{
				this.NextToken();
				this.ValidateToken(ExpressionTokenKind.Identifier);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(text, text.Length + 1 + this.CurrentToken.Text.Length);
				}
				stringBuilder.Append('.');
				stringBuilder.Append(this.CurrentToken.Text);
				this.NextToken();
			}
			if (stringBuilder != null)
			{
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008B3C File Offset: 0x00006D3C
		internal ExpressionToken PeekNextToken()
		{
			ExpressionToken expressionToken;
			Exception ex;
			this.TryPeekNextToken(out expressionToken, out ex);
			if (ex != null)
			{
				throw ex;
			}
			return expressionToken;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008B5A File Offset: 0x00006D5A
		internal void ValidateToken(ExpressionTokenKind t)
		{
			if (this.token.Kind != t)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.text));
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008B88 File Offset: 0x00006D88
		private static bool IsInfinityOrNaNDouble(string tokenText)
		{
			if (tokenText.Length == 3)
			{
				if (tokenText.get_Chars(0) == "INF".get_Chars(0))
				{
					return ExpressionLexer.IsInfinityLiteralDouble(tokenText);
				}
				if (tokenText.get_Chars(0) == "NaN".get_Chars(0))
				{
					return string.CompareOrdinal(tokenText, 0, "NaN", 0, 3) == 0;
				}
			}
			return false;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008BE0 File Offset: 0x00006DE0
		private static bool IsInfinityLiteralDouble(string text)
		{
			return string.CompareOrdinal(text, 0, "INF", 0, text.Length) == 0;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008BF8 File Offset: 0x00006DF8
		private static bool IsInfinityOrNanSingle(string tokenText)
		{
			if (tokenText.Length == 4)
			{
				if (tokenText.get_Chars(0) == "INF".get_Chars(0))
				{
					return ExpressionLexer.IsInfinityLiteralSingle(tokenText);
				}
				if (tokenText.get_Chars(0) == "NaN".get_Chars(0))
				{
					return (tokenText.get_Chars(3) == 'f' || tokenText.get_Chars(3) == 'F') && string.CompareOrdinal(tokenText, 0, "NaN", 0, 3) == 0;
				}
			}
			return false;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00008C68 File Offset: 0x00006E68
		private static bool IsInfinityLiteralSingle(string text)
		{
			return text.Length == 4 && (text.get_Chars(3) == 'f' || text.get_Chars(3) == 'F') && string.CompareOrdinal(text, 0, "INF", 0, 3) == 0;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008C9C File Offset: 0x00006E9C
		private ExpressionToken NextTokenImplementation(out Exception error)
		{
			error = null;
			while (char.IsWhiteSpace(this.ch))
			{
				this.NextChar();
			}
			int num = this.textPos;
			char c = this.ch;
			ExpressionTokenKind expressionTokenKind;
			switch (c)
			{
			case '\'':
			{
				char c2 = this.ch;
				do
				{
					this.NextChar();
					while (this.textPos < this.textLen && this.ch != c2)
					{
						this.NextChar();
					}
					if (this.textPos == this.textLen)
					{
						error = ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedStringLiteral(this.textPos, this.text));
					}
					this.NextChar();
				}
				while (this.ch == c2);
				expressionTokenKind = ExpressionTokenKind.StringLiteral;
				goto IL_0283;
			}
			case '(':
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.OpenParen;
				goto IL_0283;
			case ')':
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.CloseParen;
				goto IL_0283;
			case '*':
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.Star;
				goto IL_0283;
			case '+':
				break;
			case ',':
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.Comma;
				goto IL_0283;
			case '-':
			{
				bool flag = this.textPos + 1 < this.textLen;
				if (flag && char.IsDigit(this.text.get_Chars(this.textPos + 1)))
				{
					this.NextChar();
					expressionTokenKind = this.ParseFromDigit();
					if (ExpressionLexer.IsNumeric(expressionTokenKind))
					{
						goto IL_0283;
					}
					this.SetTextPos(num);
				}
				else if (flag && this.text.get_Chars(num + 1) == "INF".get_Chars(0))
				{
					this.NextChar();
					this.ParseIdentifier();
					string text = this.text.Substring(num + 1, this.textPos - num - 1);
					if (ExpressionLexer.IsInfinityLiteralDouble(text))
					{
						expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
						goto IL_0283;
					}
					if (ExpressionLexer.IsInfinityLiteralSingle(text))
					{
						expressionTokenKind = ExpressionTokenKind.SingleLiteral;
						goto IL_0283;
					}
					this.SetTextPos(num);
				}
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.Minus;
				goto IL_0283;
			}
			case '.':
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.Dot;
				goto IL_0283;
			case '/':
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.Slash;
				goto IL_0283;
			default:
				switch (c)
				{
				case '=':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Equal;
					goto IL_0283;
				case '?':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Question;
					goto IL_0283;
				}
				break;
			}
			if (char.IsLetter(this.ch) || this.ch == '_')
			{
				this.ParseIdentifier();
				expressionTokenKind = ExpressionTokenKind.Identifier;
			}
			else if (char.IsDigit(this.ch))
			{
				expressionTokenKind = this.ParseFromDigit();
			}
			else if (this.textPos == this.textLen)
			{
				expressionTokenKind = ExpressionTokenKind.End;
			}
			else
			{
				error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.text));
				expressionTokenKind = ExpressionTokenKind.Unknown;
			}
			IL_0283:
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.text.Substring(num, this.textPos - num);
			this.token.Position = num;
			this.HandleTypePrefixedLiterals();
			if (this.token.Kind == ExpressionTokenKind.Identifier)
			{
				if (ExpressionLexer.IsInfinityOrNaNDouble(this.token.Text))
				{
					this.token.Kind = ExpressionTokenKind.DoubleLiteral;
				}
				else if (ExpressionLexer.IsInfinityOrNanSingle(this.token.Text))
				{
					this.token.Kind = ExpressionTokenKind.SingleLiteral;
				}
				else if (this.token.Text == "true" || this.token.Text == "false")
				{
					this.token.Kind = ExpressionTokenKind.BooleanLiteral;
				}
				else if (this.token.Text == "null")
				{
					this.token.Kind = ExpressionTokenKind.NullLiteral;
				}
			}
			return this.token;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009024 File Offset: 0x00007224
		private void HandleTypePrefixedLiterals()
		{
			ExpressionTokenKind expressionTokenKind = this.token.Kind;
			if (expressionTokenKind != ExpressionTokenKind.Identifier)
			{
				return;
			}
			if (this.ch != '\'')
			{
				return;
			}
			string text = this.token.Text;
			if (string.Equals(text, "datetime", 5))
			{
				expressionTokenKind = ExpressionTokenKind.DateTimeLiteral;
			}
			else if (string.Equals(text, "datetimeoffset", 5))
			{
				expressionTokenKind = ExpressionTokenKind.DateTimeOffsetLiteral;
			}
			else if (string.Equals(text, "time", 5))
			{
				expressionTokenKind = ExpressionTokenKind.TimeLiteral;
			}
			else if (string.Equals(text, "guid", 5))
			{
				expressionTokenKind = ExpressionTokenKind.GuidLiteral;
			}
			else if (string.Equals(text, "binary", 5) || string.Equals(text, "X", 5))
			{
				expressionTokenKind = ExpressionTokenKind.BinaryLiteral;
			}
			else if (string.Equals(text, "geography", 5))
			{
				expressionTokenKind = ExpressionTokenKind.GeographyLiteral;
			}
			else
			{
				if (!string.Equals(text, "geometry", 5))
				{
					return;
				}
				expressionTokenKind = ExpressionTokenKind.GeometryLiteral;
			}
			int position = this.token.Position;
			do
			{
				this.NextChar();
			}
			while (this.ch != '\0' && this.ch != '\'');
			if (this.ch == '\0')
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedLiteral(this.textPos, this.text));
			}
			this.NextChar();
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.text.Substring(position, this.textPos - position);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00009170 File Offset: 0x00007370
		private void NextChar()
		{
			if (this.textPos < this.textLen)
			{
				this.textPos++;
			}
			this.ch = ((this.textPos < this.textLen) ? this.text.get_Chars(this.textPos) : '\0');
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000091C4 File Offset: 0x000073C4
		private ExpressionTokenKind ParseFromDigit()
		{
			char c = this.ch;
			this.NextChar();
			ExpressionTokenKind expressionTokenKind;
			if ((c == '0' && this.ch == 'x') || this.ch == 'X')
			{
				expressionTokenKind = ExpressionTokenKind.BinaryLiteral;
				do
				{
					this.NextChar();
				}
				while (UriPrimitiveTypeParser.IsCharHexDigit(this.ch));
			}
			else
			{
				expressionTokenKind = ExpressionTokenKind.IntegerLiteral;
				while (char.IsDigit(this.ch))
				{
					this.NextChar();
				}
				if (this.ch == '.')
				{
					expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
					this.NextChar();
					this.ValidateDigit();
					do
					{
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
				}
				if (this.ch == 'E' || this.ch == 'e')
				{
					expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
					this.NextChar();
					if (this.ch == '+' || this.ch == '-')
					{
						this.NextChar();
					}
					this.ValidateDigit();
					do
					{
						this.NextChar();
					}
					while (char.IsDigit(this.ch));
				}
				if (this.ch == 'M' || this.ch == 'm')
				{
					expressionTokenKind = ExpressionTokenKind.DecimalLiteral;
					this.NextChar();
				}
				else if (this.ch == 'd' || this.ch == 'D')
				{
					expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
					this.NextChar();
				}
				else if (this.ch == 'L' || this.ch == 'l')
				{
					expressionTokenKind = ExpressionTokenKind.Int64Literal;
					this.NextChar();
				}
				else if (this.ch == 'f' || this.ch == 'F')
				{
					expressionTokenKind = ExpressionTokenKind.SingleLiteral;
					this.NextChar();
				}
			}
			return expressionTokenKind;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000931F File Offset: 0x0000751F
		private void ParseIdentifier()
		{
			do
			{
				this.NextChar();
			}
			while (char.IsLetterOrDigit(this.ch) || this.ch == '_');
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000933E File Offset: 0x0000753E
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.textLen) ? this.text.get_Chars(this.textPos) : '\0');
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000936F File Offset: 0x0000756F
		private void ValidateDigit()
		{
			if (!char.IsDigit(this.ch))
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_DigitExpected(this.textPos, this.text));
			}
		}

		// Token: 0x04000183 RID: 387
		private const char SingleSuffixLower = 'f';

		// Token: 0x04000184 RID: 388
		private const char SingleSuffixUpper = 'F';

		// Token: 0x04000185 RID: 389
		private readonly string text;

		// Token: 0x04000186 RID: 390
		private readonly int textLen;

		// Token: 0x04000187 RID: 391
		private int textPos;

		// Token: 0x04000188 RID: 392
		private char ch;

		// Token: 0x04000189 RID: 393
		private ExpressionToken token;
	}
}
