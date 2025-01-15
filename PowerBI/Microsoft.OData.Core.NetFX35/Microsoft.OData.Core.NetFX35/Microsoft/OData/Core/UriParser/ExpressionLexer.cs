using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Microsoft.OData.Core.UriParser.Parsers.UriParsers;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser
{
	// Token: 0x020001E0 RID: 480
	[DebuggerDisplay("ExpressionLexer ({text} @ {textPos} [{token}])")]
	internal class ExpressionLexer
	{
		// Token: 0x06001186 RID: 4486 RVA: 0x0003E576 File Offset: 0x0003C776
		internal ExpressionLexer(string expression, bool moveToFirstToken, bool useSemicolonDelimeter)
			: this(expression, moveToFirstToken, useSemicolonDelimeter, false)
		{
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0003E584 File Offset: 0x0003C784
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0003E5D6 File Offset: 0x0003C7D6
		// (set) Token: 0x06001189 RID: 4489 RVA: 0x0003E5DE File Offset: 0x0003C7DE
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

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0003E5E7 File Offset: 0x0003C7E7
		internal string ExpressionText
		{
			get
			{
				return this.Text;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0003E5EF File Offset: 0x0003C7EF
		internal int Position
		{
			get
			{
				return this.token.Position;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x0003E5FC File Offset: 0x0003C7FC
		protected bool IsValidWhiteSpace
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && char.IsWhiteSpace(this.ch.Value);
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0003E64C File Offset: 0x0003C84C
		private bool IsValidDigit
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && char.IsDigit(this.ch.Value);
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0003E69C File Offset: 0x0003C89C
		private bool IsValidStartingCharForIdentifier
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && (char.IsLetter(this.ch.Value) || this.ch == '_' || this.ch == '$' || PlatformHelper.GetUnicodeCategory(this.ch.Value) == 9);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x0003E744 File Offset: 0x0003C944
		private bool IsValidNonStartingCharForIdentifier
		{
			get
			{
				char? c = this.ch;
				int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
				return num != null && (char.IsLetterOrDigit(this.ch.Value) || ExpressionLexer.AdditionalUnicodeCategoriesForIdentifier.Contains(PlatformHelper.GetUnicodeCategory(this.ch.Value)));
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0003E7B4 File Offset: 0x0003C9B4
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

		// Token: 0x06001191 RID: 4497 RVA: 0x0003E800 File Offset: 0x0003CA00
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

		// Token: 0x06001192 RID: 4498 RVA: 0x0003E820 File Offset: 0x0003CA20
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

		// Token: 0x06001193 RID: 4499 RVA: 0x0003E93C File Offset: 0x0003CB3C
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

		// Token: 0x06001194 RID: 4500 RVA: 0x0003E95C File Offset: 0x0003CB5C
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

		// Token: 0x06001195 RID: 4501 RVA: 0x0003EA34 File Offset: 0x0003CC34
		internal void ValidateToken(ExpressionTokenKind t)
		{
			if (this.token.Kind != t)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_SyntaxError(this.textPos, this.Text));
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003EA60 File Offset: 0x0003CC60
		internal string AdvanceThroughExpandOption()
		{
			int num = this.textPos;
			for (;;)
			{
				if (this.ch == '\'')
				{
					this.AdvanceToNextOccuranceOf('\'');
				}
				if (this.ch == '(')
				{
					this.NextChar();
					this.AdvanceThroughBalancedParentheticalExpression();
				}
				else
				{
					if (this.ch == ';' || this.ch == ')')
					{
						break;
					}
					char? c = this.ch;
					int? num2 = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
					if (num2 == null)
					{
						goto Block_9;
					}
					this.NextChar();
				}
			}
			string text = this.Text.Substring(num, this.textPos - num);
			goto IL_00FD;
			Block_9:
			text = this.Text.Substring(num);
			IL_00FD:
			this.NextToken();
			return text;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0003EB74 File Offset: 0x0003CD74
		internal string AdvanceThroughBalancedParentheticalExpression()
		{
			int position = this.Position;
			this.AdvanceThroughBalancedExpression('(', ')');
			return this.Text.Substring(position, this.textPos - position);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0003EBA8 File Offset: 0x0003CDA8
		protected static Exception ParseError(string message)
		{
			return new ODataException(message);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
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

		// Token: 0x0600119A RID: 4506 RVA: 0x0003EC10 File Offset: 0x0003CE10
		protected void ParseWhitespace()
		{
			while (this.IsValidWhiteSpace)
			{
				this.NextChar();
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003EC24 File Offset: 0x0003CE24
		protected void AdvanceToNextOccuranceOf(char endingValue)
		{
			this.NextChar();
			while (this.ch != null && this.ch != endingValue)
			{
				this.NextChar();
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003EC70 File Offset: 0x0003CE70
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
							if (this.textPos == this.TextLen)
							{
								error = ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedStringLiteral(this.textPos, this.Text));
							}
							this.NextChar();
						}
						while (this.ch != null && this.ch.Value == value);
						expressionTokenKind = ExpressionTokenKind.StringLiteral;
						goto IL_03FA;
					}
					case '(':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.OpenParen;
						goto IL_03FA;
					case ')':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.CloseParen;
						goto IL_03FA;
					case '*':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Star;
						goto IL_03FA;
					case '+':
						break;
					case ',':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Comma;
						goto IL_03FA;
					case '-':
					{
						bool flag = this.textPos + 1 < this.TextLen;
						if (flag && char.IsDigit(this.Text.get_Chars(this.textPos + 1)))
						{
							expressionTokenKind = this.ParseFromDigit();
							if (ExpressionLexerUtils.IsNumeric(expressionTokenKind))
							{
								goto IL_03FA;
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
								goto IL_03FA;
							}
							if (ExpressionLexerUtils.IsInfinityLiteralSingle(text))
							{
								expressionTokenKind = ExpressionTokenKind.SingleLiteral;
								goto IL_03FA;
							}
							this.SetTextPos(num);
						}
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Minus;
						goto IL_03FA;
					}
					case '.':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Dot;
						goto IL_03FA;
					case '/':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Slash;
						goto IL_03FA;
					default:
						if (valueOrDefault == ':')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Colon;
							goto IL_03FA;
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
						goto IL_03FA;
					case '>':
						break;
					case '?':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Question;
						goto IL_03FA;
					default:
						if (valueOrDefault == '[')
						{
							this.NextChar();
							this.AdvanceThroughBalancedExpression('[', ']');
							expressionTokenKind = ExpressionTokenKind.BracketedExpression;
							goto IL_03FA;
						}
						if (valueOrDefault == '{')
						{
							this.NextChar();
							this.AdvanceThroughBalancedExpression('{', '}');
							expressionTokenKind = ExpressionTokenKind.BracketedExpression;
							goto IL_03FA;
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
				if (this.ch == '-' && this.TryParseGuid(num))
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
			else if (this.useSemicolonDelimeter && this.ch == ';')
			{
				this.NextChar();
				expressionTokenKind = ExpressionTokenKind.SemiColon;
			}
			else if (this.parsingFunctionParameters && this.ch == '@')
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
					this.ParseIdentifier();
					expressionTokenKind = ExpressionTokenKind.ParameterAlias;
				}
			}
			else
			{
				error = ExpressionLexer.ParseError(Strings.ExpressionLexer_InvalidCharacter(this.ch, this.textPos, this.Text));
				expressionTokenKind = ExpressionTokenKind.Unknown;
			}
			IL_03FA:
			this.token.Kind = expressionTokenKind;
			this.token.Text = this.Text.Substring(num, this.textPos - num);
			this.token.Position = num;
			this.HandleTypePrefixedLiterals();
			return this.token;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0003F0BC File Offset: 0x0003D2BC
		private bool MoveNextWhenMatch(ExpressionTokenKind id)
		{
			if (id == this.PeekNextToken().Kind)
			{
				this.NextToken();
				return true;
			}
			return false;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003F0E4 File Offset: 0x0003D2E4
		private void HandleTypePrefixedLiterals()
		{
			if (this.token.Kind != ExpressionTokenKind.Identifier)
			{
				return;
			}
			if (this.ch == '\'')
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

		// Token: 0x0600119F RID: 4511 RVA: 0x0003F190 File Offset: 0x0003D390
		private void HandleQuotedValues()
		{
			int position = this.token.Position;
			for (;;)
			{
				this.NextChar();
				if (this.ch == null || !(this.ch != '\''))
				{
					char? c = this.ch;
					int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
					if (num == null)
					{
						break;
					}
					this.NextChar();
					if (this.ch == null || !(this.ch == '\''))
					{
						goto IL_00C3;
					}
				}
			}
			throw ExpressionLexer.ParseError(Strings.ExpressionLexer_UnterminatedLiteral(this.textPos, this.Text));
			IL_00C3:
			this.token.Text = this.Text.Substring(position, this.textPos - position);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0003F280 File Offset: 0x0003D480
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

		// Token: 0x060011A1 RID: 4513 RVA: 0x0003F2EC File Offset: 0x0003D4EC
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

		// Token: 0x060011A2 RID: 4514 RVA: 0x0003F36C File Offset: 0x0003D56C
		private ExpressionTokenKind ParseFromDigit()
		{
			int num = this.textPos;
			char value = this.ch.Value;
			this.NextChar();
			ExpressionTokenKind expressionTokenKind;
			if (value == '0' && (this.ch == 'x' || this.ch == 'X'))
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
				while (UriParserHelper.IsCharHexDigit(this.ch.Value));
			}
			else
			{
				expressionTokenKind = ExpressionTokenKind.IntegerLiteral;
				while (this.IsValidDigit)
				{
					this.NextChar();
				}
				if (this.ch == '-')
				{
					if (this.TryParseDateTimeoffset(num))
					{
						return ExpressionTokenKind.DateTimeOffsetLiteral;
					}
					if (this.TryParseGuid(num))
					{
						return ExpressionTokenKind.GuidLiteral;
					}
				}
				if (this.ch == ':' && this.TryParseTimeOfDay(num))
				{
					return ExpressionTokenKind.TimeOfDayLiteral;
				}
				if (this.ch != null && char.IsLetter(this.ch.Value) && this.TryParseGuid(num))
				{
					return ExpressionTokenKind.GuidLiteral;
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
				else
				{
					string text = this.Text.Substring(num, this.textPos - num);
					expressionTokenKind = ExpressionLexer.MakeBestGuessOnNoSuffixStr(text, expressionTokenKind);
				}
			}
			return expressionTokenKind;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003F6C8 File Offset: 0x0003D8C8
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

		// Token: 0x060011A4 RID: 4516 RVA: 0x0003F710 File Offset: 0x0003D910
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

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003F758 File Offset: 0x0003D958
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

		// Token: 0x060011A6 RID: 4518 RVA: 0x0003F7A0 File Offset: 0x0003D9A0
		private string ParseLiteral(int tokenPos)
		{
			do
			{
				this.NextChar();
			}
			while (this.ch != null && this.ch != ',' && this.ch != ')' && this.ch != ' ');
			char? c = this.ch;
			int? num = ((c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?));
			if (num == null)
			{
				this.NextChar();
			}
			return this.Text.Substring(tokenPos, this.textPos - tokenPos);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0003F874 File Offset: 0x0003DA74
		private static ExpressionTokenKind MakeBestGuessOnNoSuffixStr(string numericStr, ExpressionTokenKind guessedKind)
		{
			int num = 0;
			long num2 = 0L;
			float num3 = 0f;
			double num4 = 0.0;
			decimal num5 = 0m;
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

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003F9B4 File Offset: 0x0003DBB4
		private void AdvanceThroughBalancedExpression(char startingCharacter, char endingCharacter)
		{
			int i = 1;
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

		// Token: 0x060011A9 RID: 4521 RVA: 0x0003FA85 File Offset: 0x0003DC85
		private void ParseIdentifier()
		{
			do
			{
				this.NextChar();
			}
			while (this.IsValidNonStartingCharForIdentifier);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0003FA98 File Offset: 0x0003DC98
		private void SetTextPos(int pos)
		{
			this.textPos = pos;
			this.ch = ((this.textPos < this.TextLen) ? new char?(this.Text.get_Chars(this.textPos)) : default(char?));
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0003FAE1 File Offset: 0x0003DCE1
		private void ValidateDigit()
		{
			if (!this.IsValidDigit)
			{
				throw ExpressionLexer.ParseError(Strings.ExpressionLexer_DigitExpected(this.textPos, this.Text));
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0003FB08 File Offset: 0x0003DD08
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

		// Token: 0x04000798 RID: 1944
		protected readonly string Text;

		// Token: 0x04000799 RID: 1945
		protected readonly int TextLen;

		// Token: 0x0400079A RID: 1946
		protected int textPos;

		// Token: 0x0400079B RID: 1947
		protected char? ch;

		// Token: 0x0400079C RID: 1948
		protected ExpressionToken token;

		// Token: 0x0400079D RID: 1949
		private static readonly HashSet<UnicodeCategory> AdditionalUnicodeCategoriesForIdentifier;

		// Token: 0x0400079E RID: 1950
		private readonly bool useSemicolonDelimeter;

		// Token: 0x0400079F RID: 1951
		private readonly bool parsingFunctionParameters;

		// Token: 0x040007A0 RID: 1952
		private bool ignoreWhitespace;

		// Token: 0x020001E1 RID: 481
		private sealed class UnicodeCategoryEqualityComparer : IEqualityComparer<UnicodeCategory>
		{
			// Token: 0x060011AD RID: 4525 RVA: 0x0003FB51 File Offset: 0x0003DD51
			public bool Equals(UnicodeCategory x, UnicodeCategory y)
			{
				return x == y;
			}

			// Token: 0x060011AE RID: 4526 RVA: 0x0003FB57 File Offset: 0x0003DD57
			public int GetHashCode(UnicodeCategory obj)
			{
				return obj;
			}
		}
	}
}
