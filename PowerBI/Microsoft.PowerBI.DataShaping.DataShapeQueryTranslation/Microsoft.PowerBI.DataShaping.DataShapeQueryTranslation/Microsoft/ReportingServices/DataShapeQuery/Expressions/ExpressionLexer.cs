using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.DataShapeQueryTranslation;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200001B RID: 27
	internal sealed class ExpressionLexer
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000479B File Offset: 0x0000299B
		internal ExpressionLexer(ExpressionContext context, string expression)
		{
			this.m_context = context;
			this.m_expression = expression;
			this.SetPosition(0);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000047B8 File Offset: 0x000029B8
		public ExpressionToken NextToken()
		{
			while (char.IsWhiteSpace(this.m_currentChar))
			{
				this.NextChar();
			}
			int currentPosition = this.m_currentPosition;
			char currentChar = this.m_currentChar;
			ExpressionTokenKind expressionTokenKind;
			if (currentChar <= '@')
			{
				switch (currentChar)
				{
				case '!':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Not;
					goto IL_038E;
				case '"':
				case '#':
				case '$':
				case '%':
					break;
				case '&':
					this.NextChar();
					if (this.m_currentChar == '&')
					{
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.And;
						goto IL_038E;
					}
					this.RegisterLexerError(TranslationMessages.ExpressionLexer_InvalidCharacterDetected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
					return this.m_currentToken;
				case '\'':
					if (!this.ParseStringLiteral())
					{
						return this.m_currentToken;
					}
					expressionTokenKind = ExpressionTokenKind.StringLiteral;
					goto IL_038E;
				case '(':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.OpenParen;
					goto IL_038E;
				case ')':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.CloseParen;
					goto IL_038E;
				case '*':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Star;
					goto IL_038E;
				case '+':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Plus;
					goto IL_038E;
				case ',':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Comma;
					goto IL_038E;
				case '-':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Minus;
					goto IL_038E;
				case '.':
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.Dot;
					goto IL_038E;
				case '/':
					this.NextChar();
					if (this.m_currentChar == '/')
					{
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.DoubleSlash;
						goto IL_038E;
					}
					expressionTokenKind = ExpressionTokenKind.Slash;
					goto IL_038E;
				default:
					switch (currentChar)
					{
					case '<':
						this.NextChar();
						if (this.m_currentChar == '=')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.LessThanOrEqual;
							goto IL_038E;
						}
						expressionTokenKind = ExpressionTokenKind.LessThan;
						goto IL_038E;
					case '=':
						this.NextChar();
						if (this.m_currentChar == '=')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.Equal;
							goto IL_038E;
						}
						this.RegisterLexerError(TranslationMessages.ExpressionLexer_InvalidCharacterDetected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
						return this.m_currentToken;
					case '>':
						this.NextChar();
						if (this.m_currentChar == '=')
						{
							this.NextChar();
							expressionTokenKind = ExpressionTokenKind.GreaterThanOrEqual;
							goto IL_038E;
						}
						expressionTokenKind = ExpressionTokenKind.GreaterThan;
						goto IL_038E;
					case '@':
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.At;
						goto IL_038E;
					}
					break;
				}
			}
			else
			{
				if (currentChar == '[')
				{
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.OpenSquareBracket;
					goto IL_038E;
				}
				if (currentChar == ']')
				{
					this.NextChar();
					expressionTokenKind = ExpressionTokenKind.CloseSquareBracket;
					goto IL_038E;
				}
				if (currentChar == '|')
				{
					this.NextChar();
					if (this.m_currentChar == '|')
					{
						this.NextChar();
						expressionTokenKind = ExpressionTokenKind.Or;
						goto IL_038E;
					}
					this.RegisterLexerError(TranslationMessages.ExpressionLexer_InvalidCharacterDetected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
					return this.m_currentToken;
				}
			}
			if (ExpressionTextUtils.IsValidIdentifierStartChar(this.m_currentChar))
			{
				this.ParseIdentifier();
				expressionTokenKind = ExpressionTokenKind.Identifier;
			}
			else if (char.IsDigit(this.m_currentChar))
			{
				expressionTokenKind = this.ParseFromDigit();
			}
			else
			{
				if (this.m_currentChar != '\0')
				{
					this.RegisterLexerError(TranslationMessages.ExpressionLexer_InvalidCharacterDetected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
					return this.m_currentToken;
				}
				expressionTokenKind = ExpressionTokenKind.EndOfExpression;
			}
			IL_038E:
			string text = this.m_expression.Substring(currentPosition, this.m_currentPosition - currentPosition);
			this.SetCurrentToken(expressionTokenKind, text);
			return this.m_currentToken;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004B78 File Offset: 0x00002D78
		public ExpressionToken PeekNextToken()
		{
			int currentPosition = this.m_currentPosition;
			char currentChar = this.m_currentChar;
			ExpressionToken currentToken = this.m_currentToken;
			ExpressionToken expressionToken = this.NextToken();
			this.m_currentPosition = currentPosition;
			this.m_currentChar = currentChar;
			this.m_currentToken = currentToken;
			return expressionToken;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004BB5 File Offset: 0x00002DB5
		private ExpressionToken SetCurrentToken(ExpressionTokenKind kind, string text)
		{
			this.m_currentToken = new ExpressionToken(kind, text);
			return this.m_currentToken;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004BCC File Offset: 0x00002DCC
		public bool TryHandleMinusPrefixedLiteral(out ExpressionToken token)
		{
			ExpressionTokenKind kind = this.m_currentToken.Kind;
			ExpressionTokenKind expressionTokenKind = ExpressionTokenKind.Unknown;
			if (kind == ExpressionTokenKind.DecimalLiteral || kind == ExpressionTokenKind.DoubleLiteral || kind == ExpressionTokenKind.Int32Literal || kind == ExpressionTokenKind.Int64Literal)
			{
				expressionTokenKind = kind;
			}
			else if (kind == ExpressionTokenKind.Identifier && ExpressionLexer.IsInfinityLiteralDouble(this.m_currentToken.Text))
			{
				expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
			}
			if (expressionTokenKind != ExpressionTokenKind.Unknown)
			{
				token = this.SetCurrentToken(expressionTokenKind, "-" + this.m_currentToken.Text);
				return true;
			}
			token = this.m_currentToken;
			return false;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004C50 File Offset: 0x00002E50
		public ExpressionToken ResolveKeywordToken()
		{
			string text = this.m_currentToken.Text;
			if (ExpressionLexer.IsInfinityOrNaNDouble(text))
			{
				return this.SetCurrentToken(ExpressionTokenKind.DoubleLiteral, text);
			}
			if (text == "true" || text == "false")
			{
				return this.SetCurrentToken(ExpressionTokenKind.BooleanLiteral, text);
			}
			if (text == "null")
			{
				return this.SetCurrentToken(ExpressionTokenKind.NullLiteral, text);
			}
			this.HandleTypePrefixedLiteral();
			return this.m_currentToken;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private static bool IsInfinityLiteralDouble(string text)
		{
			if (string.CompareOrdinal(text, 0, "INF", 0, "INF".Length) != 0)
			{
				return false;
			}
			if (text.Length == "INF".Length)
			{
				return true;
			}
			if (text.Length == "INF".Length + 1)
			{
				char c = text["INF".Length];
				return c == 'd' || c == 'D';
			}
			return false;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004D38 File Offset: 0x00002F38
		private static bool IsNaNLiteralDouble(string text)
		{
			if (string.CompareOrdinal(text, 0, "NaN", 0, "NaN".Length) != 0)
			{
				return false;
			}
			if (text.Length == "NaN".Length)
			{
				return true;
			}
			if (text.Length == "NaN".Length + 1)
			{
				char c = text["NaN".Length];
				return c == 'd' || c == 'D';
			}
			return false;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004DAA File Offset: 0x00002FAA
		private static bool IsInfinityOrNaNDouble(string tokenText)
		{
			if (tokenText[0] == "INF"[0])
			{
				return ExpressionLexer.IsInfinityLiteralDouble(tokenText);
			}
			return tokenText[0] == "NaN"[0] && ExpressionLexer.IsNaNLiteralDouble(tokenText);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004DE4 File Offset: 0x00002FE4
		private void HandleTypePrefixedLiteral()
		{
			if (this.m_currentChar != '\'')
			{
				return;
			}
			if (!string.Equals(this.m_currentToken.Text, "datetime", StringComparison.OrdinalIgnoreCase))
			{
				return;
			}
			ExpressionTokenKind expressionTokenKind = ExpressionTokenKind.DateTimeLiteral;
			int currentPosition = this.m_currentPosition;
			this.NextChar();
			while (this.m_currentChar != '\0' && this.m_currentChar != '\'')
			{
				this.NextChar();
			}
			if (this.m_currentChar == '\0')
			{
				this.RegisterLexerError(TranslationMessages.ExpressionLexer_UnterminatedLiteral(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
				return;
			}
			this.NextChar();
			this.SetCurrentToken(expressionTokenKind, this.m_currentToken.Text + this.m_expression.Substring(currentPosition, this.m_currentPosition - currentPosition));
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004EC0 File Offset: 0x000030C0
		private bool IsValidPosition
		{
			get
			{
				return this.m_currentPosition < this.m_expression.Length;
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004ED8 File Offset: 0x000030D8
		private bool NextChar()
		{
			if (this.IsValidPosition)
			{
				this.m_currentPosition++;
			}
			if (this.IsValidPosition)
			{
				this.m_currentChar = this.m_expression[this.m_currentPosition];
				return true;
			}
			this.m_currentChar = '\0';
			return false;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004F24 File Offset: 0x00003124
		private bool ParseStringLiteral()
		{
			for (;;)
			{
				this.NextChar();
				while (this.IsValidPosition && this.m_currentChar != '\'')
				{
					this.NextChar();
				}
				if (!this.IsValidPosition)
				{
					break;
				}
				this.NextChar();
				if (this.m_currentChar != '\'')
				{
					return true;
				}
			}
			this.RegisterLexerError(TranslationMessages.ExpressionLexer_UnterminatedStringLiteral(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
			return false;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004FAE File Offset: 0x000031AE
		private void ParseIdentifier()
		{
			do
			{
				this.NextChar();
			}
			while (ExpressionTextUtils.IsValidIdentifierChar(this.m_currentChar));
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004FC4 File Offset: 0x000031C4
		private ExpressionTokenKind ParseFromDigit()
		{
			this.NextChar();
			while (char.IsDigit(this.m_currentChar))
			{
				this.NextChar();
			}
			int currentPosition = this.m_currentPosition;
			int num = -1;
			int num2 = -1;
			if (this.m_currentChar == '.')
			{
				num = this.m_currentPosition;
				this.NextChar();
				this.ValidateDigit();
				do
				{
					this.NextChar();
				}
				while (char.IsDigit(this.m_currentChar));
			}
			if (this.m_currentChar == 'E' || this.m_currentChar == 'e')
			{
				num2 = this.m_currentPosition;
				this.NextChar();
				if (this.m_currentChar == '+' || this.m_currentChar == '-')
				{
					this.NextChar();
				}
				this.ValidateDigit();
				do
				{
					this.NextChar();
				}
				while (char.IsDigit(this.m_currentChar));
			}
			ExpressionTokenKind expressionTokenKind;
			if (this.m_currentChar == 'M' || this.m_currentChar == 'm')
			{
				if (num2 >= 0)
				{
					expressionTokenKind = this.RegisterLexerError(TranslationMessages.ExpressionLexer_DecimalWithExponent(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
				}
				else
				{
					expressionTokenKind = ExpressionTokenKind.DecimalLiteral;
					this.NextChar();
				}
			}
			else if (this.m_currentChar == 'd' || this.m_currentChar == 'D')
			{
				expressionTokenKind = ExpressionTokenKind.DoubleLiteral;
				this.NextChar();
			}
			else if (this.m_currentChar == 'L' || this.m_currentChar == 'l')
			{
				if (num2 >= 0 || num >= 0)
				{
					expressionTokenKind = this.RegisterLexerError(TranslationMessages.ExpressionLexer_Int64WithExponentOrDot(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
				}
				else
				{
					expressionTokenKind = ExpressionTokenKind.Int64Literal;
					this.NextChar();
				}
			}
			else
			{
				if (this.m_currentToken.Kind == ExpressionTokenKind.EndOfExpression)
				{
					return ExpressionTokenKind.EndOfExpression;
				}
				this.m_currentPosition = currentPosition - 1;
				expressionTokenKind = ExpressionTokenKind.Int32Literal;
				this.NextChar();
			}
			return expressionTokenKind;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000051A4 File Offset: 0x000033A4
		private void ValidateDigit()
		{
			if (!char.IsDigit(this.m_currentChar))
			{
				this.RegisterLexerError(TranslationMessages.ExpressionLexer_DigitExpected(EngineMessageSeverity.Error, this.m_context.ObjectType, this.m_context.ObjectId, this.m_context.PropertyName, this.m_currentPosition, this.m_expression.MarkAsExpressionContent()));
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000051FD File Offset: 0x000033FD
		private void SetPosition(int newPosition)
		{
			this.m_currentPosition = newPosition;
			this.m_currentChar = (this.IsValidPosition ? this.m_expression[newPosition] : '\0');
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005223 File Offset: 0x00003423
		private ExpressionTokenKind RegisterLexerError(TranslationMessage message)
		{
			this.m_context.ErrorContext.Register(message);
			this.SetPosition(this.m_expression.Length);
			this.SetCurrentToken(ExpressionTokenKind.EndOfExpression, null);
			return ExpressionTokenKind.EndOfExpression;
		}

		// Token: 0x04000050 RID: 80
		private readonly string m_expression;

		// Token: 0x04000051 RID: 81
		private readonly ExpressionContext m_context;

		// Token: 0x04000052 RID: 82
		private ExpressionToken m_currentToken;

		// Token: 0x04000053 RID: 83
		private int m_currentPosition;

		// Token: 0x04000054 RID: 84
		private char m_currentChar;
	}
}
