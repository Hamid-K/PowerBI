using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013C RID: 316
	[DebuggerDisplay("ExpressionLexer ({text} @ {textPos} [{token}])")]
	internal class ExpressionLexer
	{
		// Token: 0x0600107C RID: 4220 RVA: 0x0002CC47 File Offset: 0x0002AE47
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimiter)
			: this(expression, moveToFirstToken, useSemicolonDelimiter, false)
		{
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0002CC54 File Offset: 0x0002AE54
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimiter, bool parsingFunctionParameters)
		{
			this.ignoreWhitespace = true;
			this.Text = expression;
			this.TextLen = this.Text.Length;
			this.useSemicolonDelimiter = useSemicolonDelimiter;
			this.parsingFunctionParameters = parsingFunctionParameters;
			this.SetTextPos(0);
			if (moveToFirstToken)
			{
				this.NextToken();
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x0002CCA6 File Offset: 0x0002AEA6
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x0002CCAE File Offset: 0x0002AEAE
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

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0002CCB7 File Offset: 0x0002AEB7
		internal string ExpressionText
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x0002CCBF File Offset: 0x0002AEBF
		internal int Position
		{
			get
			{
				return this.token.Position;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x0002CCCC File Offset: 0x0002AECC
		protected bool IsValidWhiteSpace
		{
			get
			{
				return this.ch != null && char.IsWhiteSpace(this.ch.Value);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x0002CCED File Offset: 0x0002AEED
		private bool IsValidDigit
		{
			get
			{
				return this.ch != null && char.IsDigit(this.ch.Value);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x0002CD10 File Offset: 0x0002AF10
		private bool IsValidStartingCharForIdentifier
		{
			get
			{
				if (this.ch != null)
				{
					if (!char.IsLetter(this.ch.Value))
					{
						char? c = this.ch;
						if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 95))
						{
							c = this.ch;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 36))
							{
								return PlatformHelper.GetUnicodeCategory(this.ch.Value) == UnicodeCategory.LetterNumber;
							}
						}
					}
					return true;
				}
				return false;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x0002CDDC File Offset: 0x0002AFDC
		private bool IsValidNonStartingCharForIdentifier
		{
			get
			{
				return this.ch != null && (char.IsLetterOrDigit(this.ch.Value) || ExpressionLexer.AdditionalUnicodeCategoriesForIdentifier.Contains(PlatformHelper.GetUnicodeCategory(this.ch.Value)));
			}
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0002CE1C File Offset: 0x0002B01C
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

		// Token: 0x06001087 RID: 4231 RVA: 0x0002CE68 File Offset: 0x0002B068
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

		// Token: 0x06001088 RID: 4232 RVA: 0x0002CE88 File Offset: 0x0002B088
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

		// Token: 0x06001089 RID: 4233 RVA: 0x0002CFA4 File Offset: 0x0002B1A4
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

		// Token: 0x0600108A RID: 4234 RVA: 0x0002CFC4 File Offset: 0x0002B1C4
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
			bool flag2 = false;
			if (this.CurrentToken.Kind == ExpressionTokenKind.Identifier)
			{
				ExpressionTokenKind kind2 = this.PeekNextToken().Kind;
				flag2 = kind2 == ExpressionTokenKind.OpenParen || (kind2 == ExpressionTokenKind.ParenthesesExpression && this.CurrentToken.Text == "in");
			}
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

		// Token: 0x0600108B RID: 4235 RVA: 0x0002D0C1 File Offset: 0x0002B2C1
		internal void ValidateToken(ExpressionTokenKind t)
		{
			if (this.token.Kind != t)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
			}
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x0002D0F0 File Offset: 0x0002B2F0
		internal string AdvanceThroughExpandOption()
		{
			int num = this.textPos;
			for (;;)
			{
				char? c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 39)
				{
					this.AdvanceToNextOccuranceOf('\'');
				}
				c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 40)
				{
					this.NextChar();
					this.AdvanceThroughBalancedParentheticalExpression();
				}
				else
				{
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 59)
					{
						break;
					}
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 41)
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

		// Token: 0x0600108D RID: 4237 RVA: 0x0002D270 File Offset: 0x0002B470
		internal string AdvanceThroughBalancedParentheticalExpression()
		{
			int position = this.Position;
			this.AdvanceThroughBalancedExpression('(', ')');
			return this.Text.Substring(position, this.textPos - position);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0002D2A4 File Offset: 0x0002B4A4
		internal ExpressionLexer.ExpressionLexerPosition SnapshotPosition()
		{
			return new ExpressionLexer.ExpressionLexerPosition(this, new int?(this.textPos), new ExpressionToken?(this.token));
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
		internal void RestorePosition(ExpressionLexer.ExpressionLexerPosition position)
		{
			if (position.TextPos != null)
			{
				this.SetTextPos(position.TextPos.Value);
			}
			if (position.Token != null)
			{
				this.token = position.Token.Value;
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0002CC1C File Offset: 0x0002AE1C
		protected static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0002D31C File Offset: 0x0002B51C
		protected void NextChar()
		{
			if (this.textPos < this.TextLen)
			{
				this.textPos++;
				if (this.textPos < this.TextLen)
				{
					this.ch = new char?(this.Text[this.textPos]);
					return;
				}
			}
			this.ch = null;
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0002D37C File Offset: 0x0002B57C
		protected void ParseWhitespace()
		{
			while (this.IsValidWhiteSpace)
			{
				this.NextChar();
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0002D390 File Offset: 0x0002B590
		protected void AdvanceToNextOccuranceOf(char endingValue)
		{
			this.NextChar();
			while (this.ch != null)
			{
				char? c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) != (int)endingValue))
				{
					break;
				}
				this.NextChar();
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0002D3FC File Offset: 0x0002B5FC
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
						goto IL_04CF;
					}
					case '(':
						if (this.CurrentToken.Text == "in")
						{
							this.NextChar();
							this.AdvanceThroughBalancedExpression('(', ')');
							expressionTokenKind = ExpressionTokenKind.ParenthesesExpression;
							goto IL_04CF;
						}
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.OpenParen;
						goto IL_04CF;
					case ')':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.CloseParen;
						goto IL_04CF;
					case '*':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Star;
						goto IL_04CF;
					case '+':
						break;
					case ',':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Comma;
						goto IL_04CF;
					case '-':
					{
						bool flag = this.textPos + 1 < this.TextLen;
						if (flag && char.IsDigit(this.Text[this.textPos + 1]))
						{
							expressionTokenKind = this.ParseFromDigit();
							if (ExpressionLexerUtils.IsNumeric(expressionTokenKind))
							{
								goto IL_04CF;
							}
							this.SetTextPos(num);
						}
						else if (flag && this.Text[num + 1] == "INF"[0])
						{
							this.NextChar();
							this.ParseIdentifier(false);
							string text = this.Text.Substring(num + 1, this.textPos - num - 1);
							if (ExpressionLexerUtils.IsInfinityLiteralDouble(text))
							{
								expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
								goto IL_04CF;
							}
							if (ExpressionLexerUtils.IsInfinityLiteralSingle(text))
							{
								expressionTokenKind = ExpressionTokenKind.SingleLiteral;
								goto IL_04CF;
							}
							this.SetTextPos(num);
						}
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Minus;
						goto IL_04CF;
					}
					case '.':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Dot;
						goto IL_04CF;
					case '/':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Slash;
						goto IL_04CF;
					default:
						if (valueOrDefault == ':')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Colon;
							goto IL_04CF;
						}
						if (valueOrDefault == '=')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Equal;
							goto IL_04CF;
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
						goto IL_04CF;
					}
					if (valueOrDefault == '[')
					{
						this.NextChar();
						this.AdvanceThroughBalancedExpression('[', ']');
						expressionTokenKind = ExpressionTokenKind.BracketedExpression;
						goto IL_04CF;
					}
					if (valueOrDefault == '{')
					{
						this.NextChar();
						this.AdvanceThroughBalancedExpression('{', '}');
						expressionTokenKind = ExpressionTokenKind.BracedExpression;
						goto IL_04CF;
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
				this.ParseIdentifier(false);
				char? c2 = this.ch;
				if (((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null) == 45 && this.TryParseGuid(num))
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
				char? c2;
				if (this.useSemicolonDelimiter)
				{
					c2 = this.ch;
					if (((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null) == 59)
					{
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.SemiColon;
						goto IL_04CF;
					}
				}
				c2 = this.ch;
				if (((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null) == 64)
				{
					this.NextChar();
					if (this.textPos == this.TextLen)
					{
						error = ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
						expressionTokenKind = ExpressionTokenKind.Unknown;
					}
					else if (!this.IsValidStartingCharForIdentifier)
					{
						error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.Text));
						expressionTokenKind = ExpressionTokenKind.Unknown;
					}
					else
					{
						int num2 = this.textPos;
						this.ParseIdentifier(true);
						string text2 = this.ExpressionText.Substring(num2, this.textPos - num2);
						expressionTokenKind = ((this.parsingFunctionParameters && !text2.Contains(".")) ? ExpressionTokenKind.ParameterAlias : ExpressionTokenKind.Identifier);
					}
				}
				else
				{
					error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.Text));
					expressionTokenKind = ExpressionTokenKind.Unknown;
				}
			}
			IL_04CF:
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.Text.Substring(num, this.textPos - num);
			this.token.Position = num;
			this.HandleTypePrefixedLiterals();
			return this.token;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0002D91C File Offset: 0x0002BB1C
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

		// Token: 0x06001096 RID: 4246 RVA: 0x0002D944 File Offset: 0x0002BB44
		private void HandleTypePrefixedLiterals()
		{
			if (this.token.Kind != ExpressionTokenKind.Identifier)
			{
				return;
			}
			char? c = this.ch;
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 39)
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

		// Token: 0x06001097 RID: 4247 RVA: 0x0002DA18 File Offset: 0x0002BC18
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
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) != 39)
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
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 39))
				{
					goto IL_00E5;
				}
			}
			throw ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedLiteral(this.textPos, this.Text));
			IL_00E5:
			this.token.Text = this.Text.Substring(position, this.textPos - position);
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0002DB2C File Offset: 0x0002BD2C
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
			return null;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0002DB98 File Offset: 0x0002BD98
		private ExpressionTokenKind GetBuiltInTypesLiteralPrefixWithQuotedValue(string tokenText)
		{
			if (string.Equals(tokenText, "duration", StringComparison.OrdinalIgnoreCase))
			{
				return ExpressionTokenKind.DurationLiteral;
			}
			if (string.Equals(tokenText, "binary", StringComparison.OrdinalIgnoreCase))
			{
				return ExpressionTokenKind.BinaryLiteral;
			}
			if (string.Equals(tokenText, "geography", StringComparison.OrdinalIgnoreCase))
			{
				return ExpressionTokenKind.GeographyLiteral;
			}
			if (string.Equals(tokenText, "geometry", StringComparison.OrdinalIgnoreCase))
			{
				return ExpressionTokenKind.GeometryLiteral;
			}
			if (string.Equals(tokenText, "null", StringComparison.OrdinalIgnoreCase))
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
			}
			return ExpressionTokenKind.QuotedLiteral;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0002DC18 File Offset: 0x0002BE18
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
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 120))
				{
					c = this.ch;
					if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 88))
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
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 45)
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
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 58 && this.TryParseTimeOfDay(num))
			{
				return ExpressionTokenKind.TimeOfDayLiteral;
			}
			if (this.ch != null && char.IsLetter(this.ch.Value) && this.TryParseGuid(num))
			{
				return ExpressionTokenKind.GuidLiteral;
			}
			c = this.ch;
			if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 46)
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
			if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 69))
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 101))
				{
					goto IL_036E;
				}
			}
			expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
			this.NextChar();
			c = this.ch;
			if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 43))
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 45))
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
			if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 77))
			{
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 109))
				{
					c = this.ch;
					if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 100))
					{
						c = this.ch;
						if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 68))
						{
							c = this.ch;
							if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 76))
							{
								c = this.ch;
								if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 108))
								{
									c = this.ch;
									if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 102))
									{
										c = this.ch;
										if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 70))
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

		// Token: 0x0600109B RID: 4251 RVA: 0x0002E210 File Offset: 0x0002C410
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
			this.ch = new char?(this.Text[num]);
			return false;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002E258 File Offset: 0x0002C458
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
			this.ch = new char?(this.Text[num]);
			return false;
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
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
			this.ch = new char?(this.Text[num]);
			return false;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002E2E8 File Offset: 0x0002C4E8
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
			this.ch = new char?(this.Text[num]);
			return false;
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002E330 File Offset: 0x0002C530
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
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) != 44))
				{
					break;
				}
				c = this.ch;
				if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) != 41))
				{
					break;
				}
				c = this.ch;
			}
			while (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) != 32);
			if (this.ch == null)
			{
				this.NextChar();
			}
			return this.Text.Substring(tokenPos, this.textPos - tokenPos);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0002E454 File Offset: 0x0002C654
		private static ExpressionTokenKind MakeBestGuessOnNoSuffixStr(string numericStr, ExpressionTokenKind guessedKind)
		{
			int num = 0;
			long num2 = 0L;
			float num3 = 0f;
			double num4 = 0.0;
			decimal num5 = 0m;
			if (guessedKind == ExpressionTokenKind.IntegerLiteral)
			{
				if (int.TryParse(numericStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					return ExpressionTokenKind.IntegerLiteral;
				}
				if (long.TryParse(numericStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
				{
					return ExpressionTokenKind.Int64Literal;
				}
			}
			bool flag = float.TryParse(numericStr, NumberStyles.Float, CultureInfo.InvariantCulture, out num3);
			bool flag2 = double.TryParse(numericStr, NumberStyles.Float, CultureInfo.InvariantCulture, out num4);
			bool flag3 = decimal.TryParse(numericStr, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out num5);
			if (flag2 && flag3)
			{
				decimal num6;
				bool flag4 = decimal.TryParse(num4.ToString("R", CultureInfo.InvariantCulture), NumberStyles.Float, CultureInfo.InvariantCulture, out num6);
				decimal num7;
				bool flag5 = decimal.TryParse(num4.ToString("N29", CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out num7);
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

		// Token: 0x060010A1 RID: 4257 RVA: 0x0002E594 File Offset: 0x0002C794
		private void AdvanceThroughBalancedExpression(char startingCharacter, char endingCharacter)
		{
			int i = 1;
			while (i > 0)
			{
				char? c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 39)
				{
					this.AdvanceToNextOccuranceOf('\'');
				}
				c = this.ch;
				if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == (int)startingCharacter)
				{
					i++;
				}
				else
				{
					c = this.ch;
					if (((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == (int)endingCharacter)
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

		// Token: 0x060010A2 RID: 4258 RVA: 0x0002E6A4 File Offset: 0x0002C8A4
		private void ParseIdentifier(bool includingDots = false)
		{
			for (;;)
			{
				this.NextChar();
				if (!this.IsValidNonStartingCharForIdentifier)
				{
					if (!includingDots)
					{
						break;
					}
					char? c = this.ch;
					if (!(((c != null) ? new int?((int)c.GetValueOrDefault()) : null) == 46))
					{
						break;
					}
				}
			}
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0002E704 File Offset: 0x0002C904
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.TextLen) ? new char?(this.Text[this.textPos]) : null);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0002E74D File Offset: 0x0002C94D
		private void ValidateDigit()
		{
			if (!this.IsValidDigit)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_DigitExpected(this.textPos, this.Text));
			}
		}

		// Token: 0x040007B7 RID: 1975
		protected readonly string Text;

		// Token: 0x040007B8 RID: 1976
		protected readonly int TextLen;

		// Token: 0x040007B9 RID: 1977
		protected int textPos;

		// Token: 0x040007BA RID: 1978
		protected char? ch;

		// Token: 0x040007BB RID: 1979
		protected ExpressionToken token;

		// Token: 0x040007BC RID: 1980
		private static readonly HashSet<UnicodeCategory> AdditionalUnicodeCategoriesForIdentifier = new HashSet<UnicodeCategory>(new ExpressionLexer.UnicodeCategoryEqualityComparer())
		{
			UnicodeCategory.LetterNumber,
			UnicodeCategory.NonSpacingMark,
			UnicodeCategory.SpacingCombiningMark,
			UnicodeCategory.ConnectorPunctuation,
			UnicodeCategory.Format
		};

		// Token: 0x040007BD RID: 1981
		private readonly bool useSemicolonDelimiter;

		// Token: 0x040007BE RID: 1982
		private readonly bool parsingFunctionParameters;

		// Token: 0x040007BF RID: 1983
		private bool ignoreWhitespace;

		// Token: 0x0200038D RID: 909
		internal class ExpressionLexerPosition
		{
			// Token: 0x06001F71 RID: 8049 RVA: 0x0005A695 File Offset: 0x00058895
			public ExpressionLexerPosition(ExpressionLexer lexer, int? textPos, ExpressionToken? token)
			{
				this.Lexer = lexer;
				this.TextPos = textPos;
				this.Token = token;
			}

			// Token: 0x17000635 RID: 1589
			// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0005A6B2 File Offset: 0x000588B2
			// (set) Token: 0x06001F73 RID: 8051 RVA: 0x0005A6BA File Offset: 0x000588BA
			public ExpressionLexer Lexer { get; private set; }

			// Token: 0x17000636 RID: 1590
			// (get) Token: 0x06001F74 RID: 8052 RVA: 0x0005A6C3 File Offset: 0x000588C3
			// (set) Token: 0x06001F75 RID: 8053 RVA: 0x0005A6CB File Offset: 0x000588CB
			public int? TextPos { get; private set; }

			// Token: 0x17000637 RID: 1591
			// (get) Token: 0x06001F76 RID: 8054 RVA: 0x0005A6D4 File Offset: 0x000588D4
			// (set) Token: 0x06001F77 RID: 8055 RVA: 0x0005A6DC File Offset: 0x000588DC
			public ExpressionToken? Token { get; private set; }
		}

		// Token: 0x0200038E RID: 910
		private sealed class UnicodeCategoryEqualityComparer : IEqualityComparer<UnicodeCategory>
		{
			// Token: 0x06001F78 RID: 8056 RVA: 0x0005A6E5 File Offset: 0x000588E5
			public bool Equals(UnicodeCategory x, UnicodeCategory y)
			{
				return x == y;
			}

			// Token: 0x06001F79 RID: 8057 RVA: 0x00011BDC File Offset: 0x0000FDDC
			public int GetHashCode(UnicodeCategory obj)
			{
				return (int)obj;
			}
		}
	}
}
