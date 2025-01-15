using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000C5 RID: 197
	[DebuggerDisplay("ExpressionLexer ({text} @ {textPos} [{token}]")]
	internal sealed class ExpressionLexer
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001003E File Offset: 0x0000E23E
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimeter)
			: this(expression, moveToFirstToken, useSemicolonDelimeter, false)
		{
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001004C File Offset: 0x0000E24C
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimeter, bool parsingFunctionParameters)
		{
			this.ignoreWhitespace = true;
			this.text = expression;
			this.textLen = this.text.Length;
			this.useSemicolonDelimeter = useSemicolonDelimeter;
			this.parsingFunctionParameters = parsingFunctionParameters;
			this.SetTextPos(0);
			if (moveToFirstToken)
			{
				this.NextToken();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001009E File Offset: 0x0000E29E
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x000100A6 File Offset: 0x0000E2A6
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000100AF File Offset: 0x0000E2AF
		internal string ExpressionText
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x000100B7 File Offset: 0x0000E2B7
		internal int Position
		{
			get
			{
				return this.token.Position;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x000100C4 File Offset: 0x0000E2C4
		private bool IsValidWhiteSpace
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && char.IsWhiteSpace(this.ch.Value);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00010114 File Offset: 0x0000E314
		private bool IsValidDigit
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && char.IsDigit(this.ch.Value);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00010164 File Offset: 0x0000E364
		private bool IsValidStartingCharForIdentifier
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && (char.IsLetter(this.ch.Value) || this.ch == '_' || this.ch == '$' || PlatformHelper.GetUnicodeCategory(this.ch.Value) == 9);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001020C File Offset: 0x0000E40C
		private bool IsValidNonStartingCharForIdentifier
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && (char.IsLetterOrDigit(this.ch.Value) || ExpressionLexer.AdditionalUnicodeCategoriesForIdentifier.Contains(PlatformHelper.GetUnicodeCategory(this.ch.Value)));
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001027C File Offset: 0x0000E47C
		internal bool TryPeekNextToken(out ExpressionToken resultToken, out Exception error)
		{
			int num = this.textPos;
			char? c = this.ch;
			int? num2 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
			char? c2 = ((num2 == null) ? default(char?) : new char?(this.ch.Value));
			ExpressionToken expressionToken = this.token;
			resultToken = this.NextTokenImplementation(out error);
			this.textPos = num;
			this.ch = c2;
			this.token = expressionToken;
			return error == null;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00010310 File Offset: 0x0000E510
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

		// Token: 0x060004CC RID: 1228 RVA: 0x00010330 File Offset: 0x0000E530
		internal string ReadDottedIdentifier(bool acceptStar)
		{
			this.ValidateToken(ExpressionTokenKind.Identifier);
			StringBuilder stringBuilder = null;
			string text = this.CurrentToken.Text;
			this.NextToken();
			while (this.CurrentToken.Kind == ExpressionTokenKind.Dot)
			{
				this.NextToken();
				if (this.CurrentToken.Kind != ExpressionTokenKind.Identifier)
				{
					if (this.CurrentToken.Kind != ExpressionTokenKind.Star)
					{
						throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.text));
					}
					if (!acceptStar || (this.PeekNextToken().Kind != ExpressionTokenKind.End && this.PeekNextToken().Kind != ExpressionTokenKind.Comma))
					{
						throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.text));
					}
				}
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

		// Token: 0x060004CD RID: 1229 RVA: 0x0001043C File Offset: 0x0000E63C
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

		// Token: 0x060004CE RID: 1230 RVA: 0x0001045C File Offset: 0x0000E65C
		internal bool ExpandIdentifierAsFunction()
		{
			ExpressionTokenKind kind = this.token.Kind;
			if (kind != ExpressionTokenKind.Identifier)
			{
				return false;
			}
			int num = this.textPos;
			char? c = this.ch;
			int? num2 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
			char? c2 = ((num2 == null) ? default(char?) : new char?(this.ch.Value));
			ExpressionToken expressionToken = this.token;
			bool flag = this.ignoreWhitespace;
			this.ignoreWhitespace = false;
			int position = this.token.Position;
			while (this.MoveNextWhenMatch(ExpressionTokenKind.Dot) && this.MoveNextWhenMatch(ExpressionTokenKind.Identifier))
			{
			}
			bool flag2 = this.CurrentToken.Kind == ExpressionTokenKind.Identifier && this.PeekNextToken().Kind == ExpressionTokenKind.OpenParen;
			if (flag2)
			{
				this.token.Text = this.text.Substring(position, this.textPos - position);
				this.token.Position = position;
			}
			else
			{
				this.textPos = num;
				this.ch = c2;
				this.token = expressionToken;
			}
			this.ignoreWhitespace = flag;
			return flag2;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001057E File Offset: 0x0000E77E
		internal void ValidateToken(ExpressionTokenKind t)
		{
			if (this.token.Kind != t)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.text));
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000105AA File Offset: 0x0000E7AA
		private static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000105B4 File Offset: 0x0000E7B4
		private ExpressionToken NextTokenImplementation(out Exception error)
		{
			error = null;
			if (this.ignoreWhitespace)
			{
				this.ParseWhitespace();
			}
			int num = this.textPos;
			char? c = this.ch;
			char valueOrDefault = c.GetValueOrDefault();
			ExpressionTokenKind expressionTokenKind;
			if (c != null)
			{
				if (valueOrDefault <= ':')
				{
					switch (valueOrDefault)
					{
					case '\'':
					{
						char value = this.ch.Value;
						do
						{
							this.AdvanceToNextOccuranceOf(value);
							if (this.textPos == this.textLen)
							{
								error = ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedStringLiteral(this.textPos, this.text));
							}
							this.NextChar();
						}
						while (this.ch != null && this.ch == value);
						expressionTokenKind = ExpressionTokenKind.StringLiteral;
						goto IL_03D8;
					}
					case '(':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.OpenParen;
						goto IL_03D8;
					case ')':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.CloseParen;
						goto IL_03D8;
					case '*':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Star;
						goto IL_03D8;
					case '+':
						break;
					case ',':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Comma;
						goto IL_03D8;
					case '-':
					{
						bool flag = this.textPos + 1 < this.textLen;
						if (flag && char.IsDigit(this.text.get_Chars(this.textPos + 1)))
						{
							this.NextChar();
							expressionTokenKind = this.ParseFromDigit();
							if (ExpressionLexerUtils.IsNumeric(expressionTokenKind))
							{
								goto IL_03D8;
							}
							this.SetTextPos(num);
						}
						else if (flag && this.text.get_Chars(num + 1) == "INF".get_Chars(0))
						{
							this.NextChar();
							this.ParseIdentifier();
							string text = this.text.Substring(num + 1, this.textPos - num - 1);
							if (ExpressionLexerUtils.IsInfinityLiteralDouble(text))
							{
								expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
								goto IL_03D8;
							}
							if (ExpressionLexerUtils.IsInfinityLiteralSingle(text))
							{
								expressionTokenKind = ExpressionTokenKind.SingleLiteral;
								goto IL_03D8;
							}
							this.SetTextPos(num);
						}
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Minus;
						goto IL_03D8;
					}
					case '.':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Dot;
						goto IL_03D8;
					case '/':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Slash;
						goto IL_03D8;
					default:
						if (valueOrDefault == ':')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Colon;
							goto IL_03D8;
						}
						break;
					}
				}
				else
				{
					switch (valueOrDefault)
					{
					case '=':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Equal;
						goto IL_03D8;
					case '>':
						break;
					case '?':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Question;
						goto IL_03D8;
					default:
						if (valueOrDefault == '[')
						{
							this.ParseBracketedExpression('[', ']');
							expressionTokenKind = ExpressionTokenKind.BracketedExpression;
							goto IL_03D8;
						}
						if (valueOrDefault == '{')
						{
							this.ParseBracketedExpression('{', '}');
							expressionTokenKind = ExpressionTokenKind.BracketedExpression;
							goto IL_03D8;
						}
						break;
					}
				}
			}
			if (this.IsValidWhiteSpace)
			{
				this.ParseWhitespace();
				expressionTokenKind = ExpressionTokenKind.Unknown;
			}
			else if (this.IsValidStartingCharForIdentifier)
			{
				this.ParseIdentifier();
				expressionTokenKind = ExpressionTokenKind.Identifier;
			}
			else if (this.IsValidDigit)
			{
				expressionTokenKind = this.ParseFromDigit();
			}
			else if (this.textPos == this.textLen)
			{
				expressionTokenKind = ExpressionTokenKind.End;
			}
			else if (this.useSemicolonDelimeter && this.ch == ';')
			{
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.SemiColon;
			}
			else if (this.parsingFunctionParameters && this.ch == '@')
			{
				this.NextChar();
				if (this.textPos == this.textLen)
				{
					error = ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.text));
					expressionTokenKind = ExpressionTokenKind.Unknown;
				}
				else if (!this.IsValidStartingCharForIdentifier)
				{
					error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.text));
					expressionTokenKind = ExpressionTokenKind.Unknown;
				}
				else
				{
					this.ParseIdentifier();
					expressionTokenKind = ExpressionTokenKind.ParameterAlias;
				}
			}
			else
			{
				error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.text));
				expressionTokenKind = ExpressionTokenKind.Unknown;
			}
			IL_03D8:
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.text.Substring(num, this.textPos - num);
			this.token.Position = num;
			this.HandleTypePrefixedLiterals();
			if (this.token.Kind == ExpressionTokenKind.Identifier)
			{
				if (ExpressionLexerUtils.IsInfinityOrNaNDouble(this.token.Text))
				{
					this.token.Kind = ExpressionTokenKind.DoubleLiteral;
				}
				else if (ExpressionLexerUtils.IsInfinityOrNanSingle(this.token.Text))
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

		// Token: 0x060004D2 RID: 1234 RVA: 0x00010A94 File Offset: 0x0000EC94
		private bool MoveNextWhenMatch(ExpressionTokenKind id)
		{
			if (id == this.PeekNextToken().Kind)
			{
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00010ABC File Offset: 0x0000ECBC
		private void HandleTypePrefixedLiterals()
		{
			ExpressionTokenKind expressionTokenKind = this.token.Kind;
			if (expressionTokenKind != ExpressionTokenKind.Identifier)
			{
				return;
			}
			if (!(this.ch == '\''))
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
			else if (string.Equals(text, "geometry", 5))
			{
				expressionTokenKind = ExpressionTokenKind.GeometryLiteral;
			}
			else
			{
				if (!this.parsingFunctionParameters || !string.Equals(text, "null", 5))
				{
					return;
				}
				expressionTokenKind = ExpressionTokenKind.NullLiteral;
			}
			int position = this.token.Position;
			do
			{
				this.NextChar();
			}
			while (this.ch != null && this.ch != '\'');
			char? c = this.ch;
			int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
			if (num == null)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedLiteral(this.textPos, this.text));
			}
			this.NextChar();
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.text.Substring(position, this.textPos - position);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00010C80 File Offset: 0x0000EE80
		private void NextChar()
		{
			if (this.textPos < this.textLen)
			{
				this.textPos++;
				if (this.textPos < this.textLen)
				{
					this.ch = new char?(this.text.get_Chars(this.textPos));
					return;
				}
			}
			this.ch = default(char?);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00010CE0 File Offset: 0x0000EEE0
		private ExpressionTokenKind ParseFromDigit()
		{
			char value = this.ch.Value;
			this.NextChar();
			ExpressionTokenKind expressionTokenKind;
			if ((value == '0' && this.ch == 'x') || this.ch == 'X')
			{
				expressionTokenKind = ExpressionTokenKind.BinaryLiteral;
				do
				{
					this.NextChar();
					if (this.ch == null)
					{
						break;
					}
				}
				while (UriPrimitiveTypeParser.IsCharHexDigit(this.ch.Value));
			}
			else
			{
				expressionTokenKind = ExpressionTokenKind.IntegerLiteral;
				while (this.IsValidDigit)
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
					while (this.IsValidDigit);
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
					while (this.IsValidDigit);
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

		// Token: 0x060004D6 RID: 1238 RVA: 0x00010F85 File Offset: 0x0000F185
		private void ParseWhitespace()
		{
			while (this.IsValidWhiteSpace)
			{
				this.NextChar();
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00010F98 File Offset: 0x0000F198
		private void ParseBracketedExpression(char startingCharacter, char endingCharacter)
		{
			int i = 1;
			this.NextChar();
			while (i > 0)
			{
				if (this.ch == '\'')
				{
					this.AdvanceToNextOccuranceOf('\'');
				}
				if (this.ch == startingCharacter)
				{
					i++;
				}
				else if (this.ch == endingCharacter)
				{
					i--;
				}
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				if (num == null)
				{
					throw new ODataException(Strings.ExpressionLexer_UnbalancedBracketExpression);
				}
				this.NextChar();
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00011070 File Offset: 0x0000F270
		private void AdvanceToNextOccuranceOf(char endingValue)
		{
			this.NextChar();
			while (this.ch != null && this.ch != endingValue)
			{
				this.NextChar();
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000110BA File Offset: 0x0000F2BA
		private void ParseIdentifier()
		{
			do
			{
				this.NextChar();
			}
			while (this.IsValidNonStartingCharForIdentifier);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000110CC File Offset: 0x0000F2CC
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.textLen) ? new char?(this.text.get_Chars(this.textPos)) : default(char?));
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00011115 File Offset: 0x0000F315
		private void ValidateDigit()
		{
			if (!this.IsValidDigit)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_DigitExpected(this.textPos, this.text));
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001113C File Offset: 0x0000F33C
		// Note: this type is marked as 'beforefieldinit'.
		static ExpressionLexer()
		{
			HashSet<UnicodeCategory> hashSet = new HashSet<UnicodeCategory>(new ExpressionLexer.UnicodeCategoryEqualityComparer());
			hashSet.Add(9);
			hashSet.Add(5);
			hashSet.Add(6);
			hashSet.Add(18);
			hashSet.Add(15);
			ExpressionLexer.AdditionalUnicodeCategoriesForIdentifier = hashSet;
		}

		// Token: 0x0400019E RID: 414
		private static readonly HashSet<UnicodeCategory> AdditionalUnicodeCategoriesForIdentifier;

		// Token: 0x0400019F RID: 415
		private readonly string text;

		// Token: 0x040001A0 RID: 416
		private readonly int textLen;

		// Token: 0x040001A1 RID: 417
		private readonly bool useSemicolonDelimeter;

		// Token: 0x040001A2 RID: 418
		private readonly bool parsingFunctionParameters;

		// Token: 0x040001A3 RID: 419
		private int textPos;

		// Token: 0x040001A4 RID: 420
		private char? ch;

		// Token: 0x040001A5 RID: 421
		private ExpressionToken token;

		// Token: 0x040001A6 RID: 422
		private bool ignoreWhitespace;

		// Token: 0x020000C6 RID: 198
		private sealed class UnicodeCategoryEqualityComparer : IEqualityComparer<UnicodeCategory>
		{
			// Token: 0x060004DD RID: 1245 RVA: 0x00011185 File Offset: 0x0000F385
			public bool Equals(UnicodeCategory x, UnicodeCategory y)
			{
				return x == y;
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x0001118B File Offset: 0x0000F38B
			public int GetHashCode(UnicodeCategory obj)
			{
				return obj;
			}
		}
	}
}
