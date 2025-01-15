using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000FD RID: 253
	[DebuggerDisplay("ExpressionLexer ({text} @ {textPos} [{token}])")]
	internal class ExpressionLexer
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001F75F File Offset: 0x0001D95F
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimeter)
			: this(expression, moveToFirstToken, useSemicolonDelimeter, false)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001F76C File Offset: 0x0001D96C
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimeter, bool parsingFunctionParameters)
		{
			this.ignoreWhitespace = true;
			this.Text = expression;
			this.TextLen = this.Text.Length;
			this.useSemicolonDelimeter = useSemicolonDelimeter;
			this.parsingFunctionParameters = parsingFunctionParameters;
			this.SetTextPos(0);
			if (moveToFirstToken)
			{
				this.NextToken();
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001F7BE File Offset: 0x0001D9BE
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0001F7C6 File Offset: 0x0001D9C6
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

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0001F7CF File Offset: 0x0001D9CF
		internal string ExpressionText
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0001F7D7 File Offset: 0x0001D9D7
		internal int Position
		{
			get
			{
				return this.token.Position;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0001F7E4 File Offset: 0x0001D9E4
		protected bool IsValidWhiteSpace
		{
			get
			{
				return this.ch != null && char.IsWhiteSpace(this.ch.Value);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0001F805 File Offset: 0x0001DA05
		private bool IsValidDigit
		{
			get
			{
				return this.ch != null && char.IsDigit(this.ch.Value);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0001F828 File Offset: 0x0001DA28
		private bool IsValidStartingCharForIdentifier
		{
			get
			{
				if (this.ch != null)
				{
					if (!char.IsLetter(this.ch.Value))
					{
						char? c = this.ch;
						if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 95))
						{
							c = this.ch;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 36))
							{
								return PlatformHelper.GetUnicodeCategory(this.ch.Value) == 9;
							}
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0001F8F4 File Offset: 0x0001DAF4
		private bool IsValidNonStartingCharForIdentifier
		{
			get
			{
				return this.ch != null && (char.IsLetterOrDigit(this.ch.Value) || ExpressionLexer.AdditionalUnicodeCategoriesForIdentifier.Contains(PlatformHelper.GetUnicodeCategory(this.ch.Value)));
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001F934 File Offset: 0x0001DB34
		internal bool TryPeekNextToken(out ExpressionToken resultToken, out Exception error)
		{
			int num = this.textPos;
			char? c = this.ch;
			ExpressionToken expressionToken = this.token;
			resultToken = this.NextTokenImplementation(out error);
			this.textPos = num;
			this.ch = c;
			this.token = expressionToken;
			return error == null;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001F980 File Offset: 0x0001DB80
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

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		internal string ReadDottedIdentifier(bool acceptStar)
		{
			this.ValidateToken(ExpressionTokenKind.Identifier);
			StringBuilder stringBuilder = null;
			string text = this.CurrentToken.Text;
			this.NextToken();
			while (this.CurrentToken.Kind == ExpressionTokenKind.Dot)
			{
				this.NextToken();
				if (this.CurrentToken.Kind != ExpressionTokenKind.Identifier && this.CurrentToken.Kind != ExpressionTokenKind.QuotedLiteral)
				{
					if (this.CurrentToken.Kind != ExpressionTokenKind.Star)
					{
						throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
					}
					if (!acceptStar || (this.PeekNextToken().Kind != ExpressionTokenKind.End && this.PeekNextToken().Kind != ExpressionTokenKind.Comma))
					{
						throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
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

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001FABC File Offset: 0x0001DCBC
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

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001FADC File Offset: 0x0001DCDC
		internal bool ExpandIdentifierAsFunction()
		{
			ExpressionTokenKind kind = this.token.Kind;
			if (kind != ExpressionTokenKind.Identifier)
			{
				return false;
			}
			int num = this.textPos;
			char? c = this.ch;
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
				this.token.Text = this.Text.Substring(position, this.textPos - position);
				this.token.Position = position;
			}
			else
			{
				this.textPos = num;
				this.ch = c;
				this.token = expressionToken;
			}
			this.ignoreWhitespace = flag;
			return flag2;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		internal void ValidateToken(ExpressionTokenKind t)
		{
			if (this.token.Kind != t)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		internal string AdvanceThroughExpandOption()
		{
			int num = this.textPos;
			for (;;)
			{
				char? c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 39)
				{
					this.AdvanceToNextOccuranceOf('\'');
				}
				c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 40)
				{
					this.NextChar();
					this.AdvanceThroughBalancedParentheticalExpression();
				}
				else
				{
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 59)
					{
						break;
					}
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 41)
					{
						break;
					}
					if (this.ch == null)
					{
						goto Block_12;
					}
					this.NextChar();
				}
			}
			string text = this.Text.Substring(num, this.textPos - num);
			goto IL_016B;
			Block_12:
			text = this.Text.Substring(num);
			IL_016B:
			this.NextToken();
			return text;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001FD60 File Offset: 0x0001DF60
		internal string AdvanceThroughBalancedParentheticalExpression()
		{
			int position = this.Position;
			this.AdvanceThroughBalancedExpression('(', ')');
			return this.Text.Substring(position, this.textPos - position);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001F734 File Offset: 0x0001D934
		protected static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001FD94 File Offset: 0x0001DF94
		protected void NextChar()
		{
			if (this.textPos < this.TextLen)
			{
				this.textPos++;
				if (this.textPos < this.TextLen)
				{
					this.ch = new char?(this.Text.get_Chars(this.textPos));
					return;
				}
			}
			this.ch = default(char?);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001FDF4 File Offset: 0x0001DFF4
		protected void ParseWhitespace()
		{
			while (this.IsValidWhiteSpace)
			{
				this.NextChar();
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001FE08 File Offset: 0x0001E008
		protected void AdvanceToNextOccuranceOf(char endingValue)
		{
			this.NextChar();
			while (this.ch != null)
			{
				char? c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != (int)endingValue))
				{
					break;
				}
				this.NextChar();
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001FE74 File Offset: 0x0001E074
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This parser method is all about the switch statement and would be harder to maintain if it were broken up.")]
		protected virtual ExpressionToken NextTokenImplementation(out Exception error)
		{
			error = null;
			if (this.ignoreWhitespace)
			{
				this.ParseWhitespace();
			}
			int num = this.textPos;
			char? c = this.ch;
			ExpressionTokenKind expressionTokenKind;
			if (c != null)
			{
				char valueOrDefault = c.GetValueOrDefault();
				if (valueOrDefault <= '=')
				{
					switch (valueOrDefault)
					{
					case '\'':
					{
						char value = this.ch.Value;
						do
						{
							this.AdvanceToNextOccuranceOf(value);
							if (this.textPos == this.TextLen)
							{
								error = ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedStringLiteral(this.textPos, this.Text));
							}
							this.NextChar();
						}
						while (this.ch != null && this.ch.Value == value);
						expressionTokenKind = ExpressionTokenKind.StringLiteral;
						goto IL_0469;
					}
					case '(':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.OpenParen;
						goto IL_0469;
					case ')':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.CloseParen;
						goto IL_0469;
					case '*':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Star;
						goto IL_0469;
					case '+':
						break;
					case ',':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Comma;
						goto IL_0469;
					case '-':
					{
						bool flag = this.textPos + 1 < this.TextLen;
						if (flag && char.IsDigit(this.Text.get_Chars(this.textPos + 1)))
						{
							expressionTokenKind = this.ParseFromDigit();
							if (ExpressionLexerUtils.IsNumeric(expressionTokenKind))
							{
								goto IL_0469;
							}
							this.SetTextPos(num);
						}
						else if (flag && this.Text.get_Chars(num + 1) == "INF".get_Chars(0))
						{
							this.NextChar();
							this.ParseIdentifier();
							string text = this.Text.Substring(num + 1, this.textPos - num - 1);
							if (ExpressionLexerUtils.IsInfinityLiteralDouble(text))
							{
								expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
								goto IL_0469;
							}
							if (ExpressionLexerUtils.IsInfinityLiteralSingle(text))
							{
								expressionTokenKind = ExpressionTokenKind.SingleLiteral;
								goto IL_0469;
							}
							this.SetTextPos(num);
						}
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Minus;
						goto IL_0469;
					}
					case '.':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Dot;
						goto IL_0469;
					case '/':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Slash;
						goto IL_0469;
					default:
						if (valueOrDefault == ':')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Colon;
							goto IL_0469;
						}
						if (valueOrDefault == '=')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Equal;
							goto IL_0469;
						}
						break;
					}
				}
				else
				{
					if (valueOrDefault == '?')
					{
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Question;
						goto IL_0469;
					}
					if (valueOrDefault == '[')
					{
						this.NextChar();
						this.AdvanceThroughBalancedExpression('[', ']');
						expressionTokenKind = ExpressionTokenKind.BracketedExpression;
						goto IL_0469;
					}
					if (valueOrDefault == '{')
					{
						this.NextChar();
						this.AdvanceThroughBalancedExpression('{', '}');
						expressionTokenKind = ExpressionTokenKind.BracedExpression;
						goto IL_0469;
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
				char? c2 = this.ch;
				if (((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : default(int?)) == 45 && this.TryParseGuid(num))
				{
					expressionTokenKind = ExpressionTokenKind.GuidLiteral;
				}
				else
				{
					expressionTokenKind = ExpressionTokenKind.Identifier;
				}
			}
			else if (this.IsValidDigit)
			{
				expressionTokenKind = this.ParseFromDigit();
			}
			else if (this.textPos == this.TextLen)
			{
				expressionTokenKind = ExpressionTokenKind.End;
			}
			else
			{
				if (this.useSemicolonDelimeter)
				{
					char? c2 = this.ch;
					if (((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : default(int?)) == 59)
					{
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.SemiColon;
						goto IL_0469;
					}
				}
				if (this.parsingFunctionParameters)
				{
					char? c2 = this.ch;
					if (((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : default(int?)) == 64)
					{
						this.NextChar();
						if (this.textPos == this.TextLen)
						{
							error = ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
							expressionTokenKind = ExpressionTokenKind.Unknown;
							goto IL_0469;
						}
						if (!this.IsValidStartingCharForIdentifier)
						{
							error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.Text));
							expressionTokenKind = ExpressionTokenKind.Unknown;
							goto IL_0469;
						}
						this.ParseIdentifier();
						expressionTokenKind = ExpressionTokenKind.ParameterAlias;
						goto IL_0469;
					}
				}
				error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.Text));
				expressionTokenKind = ExpressionTokenKind.Unknown;
			}
			IL_0469:
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.Text.Substring(num, this.textPos - num);
			this.token.Position = num;
			this.HandleTypePrefixedLiterals();
			return this.token;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00020330 File Offset: 0x0001E530
		private bool MoveNextWhenMatch(ExpressionTokenKind id)
		{
			ExpressionToken expressionToken = this.PeekNextToken();
			if (id == expressionToken.Kind)
			{
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00020358 File Offset: 0x0001E558
		private void HandleTypePrefixedLiterals()
		{
			if (this.token.Kind != ExpressionTokenKind.Identifier)
			{
				return;
			}
			char? c = this.ch;
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 39)
			{
				IEdmTypeReference edmTypeByCustomLiteralPrefix = CustomUriLiteralPrefixes.GetEdmTypeByCustomLiteralPrefix(this.token.Text);
				if (edmTypeByCustomLiteralPrefix != null)
				{
					this.token.SetCustomEdmTypeLiteral(edmTypeByCustomLiteralPrefix);
				}
				else
				{
					this.token.Kind = this.GetBuiltInTypesLiteralPrefixWithQuotedValue(this.token.Text);
				}
				this.HandleQuotedValues();
				return;
			}
			ExpressionTokenKind? builtInTypesLiteralPrefix = ExpressionLexer.GetBuiltInTypesLiteralPrefix(this.token.Text);
			if (builtInTypesLiteralPrefix != null)
			{
				this.token.Kind = builtInTypesLiteralPrefix.Value;
			}
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002042C File Offset: 0x0001E62C
		private void HandleQuotedValues()
		{
			int position = this.token.Position;
			for (;;)
			{
				this.NextChar();
				char? c;
				if (this.ch != null)
				{
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != 39)
					{
						continue;
					}
				}
				if (this.ch == null)
				{
					break;
				}
				this.NextChar();
				if (this.ch == null)
				{
					goto IL_00E5;
				}
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 39))
				{
					goto IL_00E5;
				}
			}
			throw ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedLiteral(this.textPos, this.Text));
			IL_00E5:
			this.token.Text = this.Text.Substring(position, this.textPos - position);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00020540 File Offset: 0x0001E740
		private static ExpressionTokenKind? GetBuiltInTypesLiteralPrefix(string tokenText)
		{
			if (ExpressionLexerUtils.IsInfinityOrNaNDouble(tokenText))
			{
				return new ExpressionTokenKind?(ExpressionTokenKind.DoubleLiteral);
			}
			if (ExpressionLexerUtils.IsInfinityOrNanSingle(tokenText))
			{
				return new ExpressionTokenKind?(ExpressionTokenKind.SingleLiteral);
			}
			if (tokenText == "true" || tokenText == "false")
			{
				return new ExpressionTokenKind?(ExpressionTokenKind.BooleanLiteral);
			}
			if (tokenText == "null")
			{
				return new ExpressionTokenKind?(ExpressionTokenKind.NullLiteral);
			}
			return default(ExpressionTokenKind?);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x000205AC File Offset: 0x0001E7AC
		private ExpressionTokenKind GetBuiltInTypesLiteralPrefixWithQuotedValue(string tokenText)
		{
			if (string.Equals(tokenText, "duration", 5))
			{
				return ExpressionTokenKind.DurationLiteral;
			}
			if (string.Equals(tokenText, "binary", 5))
			{
				return ExpressionTokenKind.BinaryLiteral;
			}
			if (string.Equals(tokenText, "geography", 5))
			{
				return ExpressionTokenKind.GeographyLiteral;
			}
			if (string.Equals(tokenText, "geometry", 5))
			{
				return ExpressionTokenKind.GeometryLiteral;
			}
			if (string.Equals(tokenText, "null", 5))
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
			}
			return ExpressionTokenKind.QuotedLiteral;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002062C File Offset: 0x0001E82C
		private ExpressionTokenKind ParseFromDigit()
		{
			int num = this.textPos;
			char value = this.ch.Value;
			this.NextChar();
			char? c;
			ExpressionTokenKind expressionTokenKind;
			if (value == '0')
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 120))
				{
					c = this.ch;
					if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 88))
					{
						goto IL_00DB;
					}
				}
				expressionTokenKind = ExpressionTokenKind.BinaryLiteral;
				do
				{
					this.NextChar();
					if (this.ch == null)
					{
						break;
					}
				}
				while (UriParserHelper.IsCharHexDigit(this.ch.Value));
				return expressionTokenKind;
			}
			IL_00DB:
			expressionTokenKind = ExpressionTokenKind.IntegerLiteral;
			while (this.IsValidDigit)
			{
				this.NextChar();
			}
			c = this.ch;
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 45)
			{
				if (this.TryParseDate(num))
				{
					return ExpressionTokenKind.DateLiteral;
				}
				if (this.TryParseDateTimeoffset(num))
				{
					return ExpressionTokenKind.DateTimeOffsetLiteral;
				}
				if (this.TryParseGuid(num))
				{
					return ExpressionTokenKind.GuidLiteral;
				}
			}
			c = this.ch;
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 58 && this.TryParseTimeOfDay(num))
			{
				return ExpressionTokenKind.TimeOfDayLiteral;
			}
			if (this.ch != null && char.IsLetter(this.ch.Value) && this.TryParseGuid(num))
			{
				return ExpressionTokenKind.GuidLiteral;
			}
			c = this.ch;
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 46)
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
			c = this.ch;
			if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 69))
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 101))
				{
					goto IL_036E;
				}
			}
			expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
			this.NextChar();
			c = this.ch;
			if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 43))
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 45))
				{
					goto IL_035A;
				}
			}
			this.NextChar();
			IL_035A:
			this.ValidateDigit();
			do
			{
				this.NextChar();
			}
			while (this.IsValidDigit);
			IL_036E:
			c = this.ch;
			if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 77))
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 109))
				{
					c = this.ch;
					if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 100))
					{
						c = this.ch;
						if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 68))
						{
							c = this.ch;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 76))
							{
								c = this.ch;
								if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 108))
								{
									c = this.ch;
									if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 102))
									{
										c = this.ch;
										if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 70))
										{
											string text = this.Text.Substring(num, this.textPos - num);
											return ExpressionLexer.MakeBestGuessOnNoSuffixStr(text, expressionTokenKind);
										}
									}
									expressionTokenKind = ExpressionTokenKind.SingleLiteral;
									this.NextChar();
									return expressionTokenKind;
								}
							}
							expressionTokenKind = ExpressionTokenKind.Int64Literal;
							this.NextChar();
							return expressionTokenKind;
						}
					}
					expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
					this.NextChar();
					return expressionTokenKind;
				}
			}
			expressionTokenKind = ExpressionTokenKind.DecimalLiteral;
			this.NextChar();
			return expressionTokenKind;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00020C24 File Offset: 0x0001EE24
		private bool TryParseGuid(int tokenPos)
		{
			int num = this.textPos;
			string text = this.ParseLiteral(tokenPos);
			Guid guid;
			if (UriUtils.TryUriStringToGuid(text, out guid))
			{
				return true;
			}
			this.textPos = num;
			this.ch = new char?(this.Text.get_Chars(num));
			return false;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00020C6C File Offset: 0x0001EE6C
		private bool TryParseDateTimeoffset(int tokenPos)
		{
			int num = this.textPos;
			string text = this.ParseLiteral(tokenPos);
			DateTimeOffset dateTimeOffset;
			if (UriUtils.ConvertUriStringToDateTimeOffset(text, out dateTimeOffset))
			{
				return true;
			}
			this.textPos = num;
			this.ch = new char?(this.Text.get_Chars(num));
			return false;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		private bool TryParseDate(int tokenPos)
		{
			int num = this.textPos;
			string text = this.ParseLiteral(tokenPos);
			Date date;
			if (UriUtils.TryUriStringToDate(text, out date))
			{
				return true;
			}
			this.textPos = num;
			this.ch = new char?(this.Text.get_Chars(num));
			return false;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00020CFC File Offset: 0x0001EEFC
		private bool TryParseTimeOfDay(int tokenPos)
		{
			int num = this.textPos;
			string text = this.ParseLiteral(tokenPos);
			TimeOfDay timeOfDay;
			if (UriUtils.TryUriStringToTimeOfDay(text, out timeOfDay))
			{
				return true;
			}
			this.textPos = num;
			this.ch = new char?(this.Text.get_Chars(num));
			return false;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00020D44 File Offset: 0x0001EF44
		private string ParseLiteral(int tokenPos)
		{
			char? c;
			do
			{
				this.NextChar();
				if (this.ch == null)
				{
					break;
				}
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != 44))
				{
					break;
				}
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != 41))
				{
					break;
				}
				c = this.ch;
			}
			while (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) != 32);
			if (this.ch == null)
			{
				this.NextChar();
			}
			return this.Text.Substring(tokenPos, this.textPos - tokenPos);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00020E68 File Offset: 0x0001F068
		private static ExpressionTokenKind MakeBestGuessOnNoSuffixStr(string numericStr, ExpressionTokenKind guessedKind)
		{
			int num = 0;
			long num2 = 0L;
			float num3 = 0f;
			double num4 = 0.0;
			decimal num5 = default(decimal);
			if (guessedKind == ExpressionTokenKind.IntegerLiteral)
			{
				if (int.TryParse(numericStr, 7, CultureInfo.InvariantCulture, ref num))
				{
					return ExpressionTokenKind.IntegerLiteral;
				}
				if (long.TryParse(numericStr, 7, CultureInfo.InvariantCulture, ref num2))
				{
					return ExpressionTokenKind.Int64Literal;
				}
			}
			bool flag = float.TryParse(numericStr, 167, CultureInfo.InvariantCulture, ref num3);
			bool flag2 = double.TryParse(numericStr, 167, CultureInfo.InvariantCulture, ref num4);
			bool flag3 = decimal.TryParse(numericStr, 39, CultureInfo.InvariantCulture, ref num5);
			if (flag2 && flag3)
			{
				decimal num6;
				bool flag4 = decimal.TryParse(num4.ToString("R", CultureInfo.InvariantCulture), 167, CultureInfo.InvariantCulture, ref num6);
				decimal num7;
				bool flag5 = decimal.TryParse(num4.ToString("N29", CultureInfo.InvariantCulture), 111, CultureInfo.InvariantCulture, ref num7);
				if ((flag4 && num6 != num5) || (!flag4 && flag5 && num7 != num5))
				{
					return ExpressionTokenKind.DecimalLiteral;
				}
			}
			if (flag && flag2 && double.Parse(num3.ToString("R", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture) != num4)
			{
				return ExpressionTokenKind.DoubleLiteral;
			}
			if (flag)
			{
				return ExpressionTokenKind.SingleLiteral;
			}
			if (flag2)
			{
				return ExpressionTokenKind.DoubleLiteral;
			}
			throw new ODataException(Strings.ExpressionLexer_InvalidNumericString(numericStr));
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		private void AdvanceThroughBalancedExpression(char startingCharacter, char endingCharacter)
		{
			int i = 1;
			while (i > 0)
			{
				char? c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == 39)
				{
					this.AdvanceToNextOccuranceOf('\'');
				}
				c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == (int)startingCharacter)
				{
					i++;
				}
				else
				{
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?)) == (int)endingCharacter)
					{
						i--;
					}
				}
				if (this.ch == null)
				{
					throw new ODataException(Strings.ExpressionLexer_UnbalancedBracketExpression);
				}
				this.NextChar();
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000210B7 File Offset: 0x0001F2B7
		private void ParseIdentifier()
		{
			do
			{
				this.NextChar();
			}
			while (this.IsValidNonStartingCharForIdentifier);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000210C8 File Offset: 0x0001F2C8
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.TextLen) ? new char?(this.Text.get_Chars(this.textPos)) : default(char?));
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00021111 File Offset: 0x0001F311
		private void ValidateDigit()
		{
			if (!this.IsValidDigit)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_DigitExpected(this.textPos, this.Text));
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00021137 File Offset: 0x0001F337
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

		// Token: 0x0400069F RID: 1695
		protected readonly string Text;

		// Token: 0x040006A0 RID: 1696
		protected readonly int TextLen;

		// Token: 0x040006A1 RID: 1697
		protected int textPos;

		// Token: 0x040006A2 RID: 1698
		protected char? ch;

		// Token: 0x040006A3 RID: 1699
		protected ExpressionToken token;

		// Token: 0x040006A4 RID: 1700
		private static readonly HashSet<UnicodeCategory> AdditionalUnicodeCategoriesForIdentifier;

		// Token: 0x040006A5 RID: 1701
		private readonly bool useSemicolonDelimeter;

		// Token: 0x040006A6 RID: 1702
		private readonly bool parsingFunctionParameters;

		// Token: 0x040006A7 RID: 1703
		private bool ignoreWhitespace;

		// Token: 0x020002B5 RID: 693
		private sealed class UnicodeCategoryEqualityComparer : IEqualityComparer<UnicodeCategory>
		{
			// Token: 0x0600188E RID: 6286 RVA: 0x000489F7 File Offset: 0x00046BF7
			public bool Equals(UnicodeCategory x, UnicodeCategory y)
			{
				return x == y;
			}

			// Token: 0x0600188F RID: 6287 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
			public int GetHashCode(UnicodeCategory obj)
			{
				return obj;
			}
		}
	}
}
