using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200023B RID: 571
	internal sealed class Lexer
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x0002C51A File Offset: 0x0002A71A
		internal Lexer(TextReader file)
		{
			this.tokens = new TokenList();
			this.textRun = new CharReader(file);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0002C53C File Offset: 0x0002A73C
		internal TokenList Lex()
		{
			for (ExpressionToken expressionToken = this.ReadToken(); expressionToken != null; expressionToken = this.ReadToken())
			{
				this.tokens.Add(expressionToken);
			}
			this.tokens.Add(new ExpressionToken(TokenTypes.EOF, null, this.textRun.Line, this.textRun.Column, this.textRun.Column));
			this.FinalPass();
			return this.tokens;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0002C5AC File Offset: 0x0002A7AC
		private void FinalPass()
		{
			if (this.bFinalPassNeeded)
			{
				TokenList tokenList = new TokenList();
				ExpressionToken expressionToken = this.tokens.Extract();
				do
				{
					if (expressionToken._TokenType == TokenTypes.LESSTHAN && this.tokens.Peek()._TokenType == TokenTypes.GREATERTHAN)
					{
						this.tokens.Extract();
						tokenList.Add(new ExpressionToken(TokenTypes.NOTEQUAL));
					}
					else if (expressionToken._TokenType == TokenTypes.LESSTHAN && this.tokens.Peek()._TokenType == TokenTypes.LESSTHAN)
					{
						this.tokens.Extract();
						tokenList.Add(new ExpressionToken(TokenTypes.SHIFTLEFT));
					}
					else if (expressionToken._TokenType == TokenTypes.GREATERTHAN && this.tokens.Peek()._TokenType == TokenTypes.GREATERTHAN)
					{
						this.tokens.Extract();
						tokenList.Add(new ExpressionToken(TokenTypes.SHIFTRIGHT));
					}
					else
					{
						tokenList.Add(expressionToken);
					}
					expressionToken = this.tokens.Extract();
				}
				while (expressionToken._TokenType != TokenTypes.EOF);
				tokenList.Add(expressionToken);
				this.tokens = tokenList;
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		private ExpressionToken ReadToken()
		{
			while (!this.textRun.FileEnd())
			{
				char c = this.textRun.Get();
				if (!char.IsWhiteSpace(c))
				{
					if (c <= '^')
					{
						if (c <= '>')
						{
							switch (c)
							{
							case '!':
								return new ExpressionToken(TokenTypes.BANG, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case '"':
								break;
							case '#':
								return this.ProcessDateTime(c);
							case '$':
							case '%':
							case '\'':
							case '.':
								goto IL_0585;
							case '&':
								return new ExpressionToken(TokenTypes.CONCATENATE, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case '(':
								return new ExpressionToken(TokenTypes.LPAREN, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case ')':
								return new ExpressionToken(TokenTypes.RPAREN, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case '*':
								return new ExpressionToken(TokenTypes.STAR, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case '+':
								return new ExpressionToken(TokenTypes.PLUS, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case ',':
								return new ExpressionToken(TokenTypes.COMMA, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case '-':
								return new ExpressionToken(TokenTypes.MINUS, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							case '/':
								if (this.textRun.Peek == '*')
								{
									this.textRun.Get();
									this.ProcessComment();
									continue;
								}
								return new ExpressionToken(TokenTypes.FORWARDSLASH, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							default:
								switch (c)
								{
								case '<':
									this.bFinalPassNeeded = true;
									if (this.textRun.Peek == '=')
									{
										this.textRun.Get();
										return new ExpressionToken(TokenTypes.LESSTHANOREQUAL, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
									}
									if (this.textRun.Peek == '>')
									{
										this.textRun.Get();
										return new ExpressionToken(TokenTypes.NOTEQUAL, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
									}
									if (this.textRun.Peek == '<')
									{
										this.textRun.Get();
										return new ExpressionToken(TokenTypes.SHIFTLEFT, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
									}
									return new ExpressionToken(TokenTypes.LESSTHAN, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
								case '=':
									return new ExpressionToken(TokenTypes.EQUAL, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
								case '>':
									this.bFinalPassNeeded = true;
									if (this.textRun.Peek == '=')
									{
										this.textRun.Get();
										return new ExpressionToken(TokenTypes.GREATERTHANOREQUAL, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
									}
									if (this.textRun.Peek == '>')
									{
										this.textRun.Get();
										return new ExpressionToken(TokenTypes.SHIFTRIGHT, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
									}
									return new ExpressionToken(TokenTypes.GREATERTHAN, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
								default:
									goto IL_0585;
								}
								break;
							}
						}
						else
						{
							if (c == '\\')
							{
								return new ExpressionToken(TokenTypes.BACKWARDSLASH, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
							}
							if (c != '^')
							{
								goto IL_0585;
							}
							return new ExpressionToken(TokenTypes.EXP, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
						}
					}
					else if (c <= '}')
					{
						if (c == '{')
						{
							return new ExpressionToken(TokenTypes.LCURLY, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
						}
						if (c != '}')
						{
							goto IL_0585;
						}
						return new ExpressionToken(TokenTypes.RCURLY, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					else if (c != '“' && c != '”')
					{
						goto IL_0585;
					}
					return this.ProcessQuote();
					IL_0585:
					if (char.IsDigit(c))
					{
						return this.ProcessNumber(c);
					}
					if (c == '.')
					{
						if (char.IsDigit(this.textRun.Peek))
						{
							return this.ProcessNumber(c);
						}
						return new ExpressionToken(TokenTypes.PERIOD, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					else
					{
						if (char.IsLetter(c) || c == '_')
						{
							return this.ProcessWord(c);
						}
						return new ExpressionToken(TokenTypes.OTHER, c.ToString(), this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
				}
			}
			return null;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0002CCF8 File Offset: 0x0002AEF8
		private void ProcessComment()
		{
			while (!this.textRun.FileEnd())
			{
				if (this.textRun.Get() == '*' && this.textRun.Peek == '/')
				{
					this.textRun.Get();
					return;
				}
			}
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnterminatedComment", "Lexer", 0, 0);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0002CD50 File Offset: 0x0002AF50
		private ExpressionToken ProcessQuote()
		{
			int line = this.textRun.Line;
			int column = this.textRun.Column;
			string text = string.Empty;
			while (!this.textRun.FileEnd())
			{
				char c = this.textRun.Get();
				if (this.IsDoubleQuote(c))
				{
					if (this.IsDoubleQuote(this.textRun.Peek))
					{
						c = this.textRun.Get();
					}
					else
					{
						if (text.Length == 1 && (this.textRun.Peek == 'C' || this.textRun.Peek == 'c'))
						{
							this.textRun.Get();
							return new ExpressionToken(TokenTypes.CHAR, text, line, column, this.textRun.Column);
						}
						return new ExpressionToken(TokenTypes.QUOTE, text, line, column, this.textRun.Column);
					}
				}
				text += c.ToString();
			}
			throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.UnterminatedString", "Lexer", column - 2, column - 1);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0002CE47 File Offset: 0x0002B047
		private bool IsDoubleQuote(char ch)
		{
			return ch == '"' || ch == '“' || ch == '”';
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0002CE60 File Offset: 0x0002B060
		private ExpressionToken ProcessWord(char ch)
		{
			int line = this.textRun.Line;
			int column = this.textRun.Column;
			string text = ch.ToString();
			while (!this.textRun.FileEnd() && (char.IsLetterOrDigit(this.textRun.Peek) || this.textRun.Peek == '_'))
			{
				text += this.textRun.Get().ToString();
			}
			string text2 = text.ToUpperInvariant();
			if (text2 != null)
			{
				switch (text2.Length)
				{
				case 2:
				{
					char c = text2[0];
					if (c != 'I')
					{
						if (c == 'O')
						{
							if (text2 == "OR")
							{
								return new ExpressionToken(TokenTypes.OR, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
							}
						}
					}
					else if (text2 == "IS")
					{
						return new ExpressionToken(TokenTypes.IS, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					break;
				}
				case 3:
				{
					char c = text2[0];
					if (c <= 'M')
					{
						if (c != 'A')
						{
							if (c == 'M')
							{
								if (text2 == "MOD")
								{
									return new ExpressionToken(TokenTypes.MODULUS, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
								}
							}
						}
						else if (text2 == "AND")
						{
							return new ExpressionToken(TokenTypes.AND, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
						}
					}
					else if (c != 'N')
					{
						if (c == 'X')
						{
							if (text2 == "XOR")
							{
								return new ExpressionToken(TokenTypes.XOR, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
							}
						}
					}
					else
					{
						if (text2 == "NOT")
						{
							return new ExpressionToken(TokenTypes.NOT, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
						}
						if (text2 == "NEW")
						{
							return new ExpressionToken(TokenTypes.NEW, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
						}
					}
					break;
				}
				case 4:
				{
					char c = text2[0];
					if (c != 'L')
					{
						if (c == 'T')
						{
							if (text2 == "TRUE")
							{
								return new ExpressionToken(TokenTypes.TRUE, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
							}
						}
					}
					else if (text2 == "LIKE")
					{
						return new ExpressionToken(TokenTypes.LIKE, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					break;
				}
				case 5:
				{
					char c = text2[0];
					if (c != 'F')
					{
						if (c == 'I')
						{
							if (text2 == "ISNOT")
							{
								return new ExpressionToken(TokenTypes.IS, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
							}
						}
					}
					else if (text2 == "FALSE")
					{
						return new ExpressionToken(TokenTypes.FALSE, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					break;
				}
				case 6:
				{
					char c = text2[0];
					if (c != 'O')
					{
						if (c == 'T')
						{
							if (text2 == "TYPEOF")
							{
								return new ExpressionToken(TokenTypes.TYPEOF, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
							}
						}
					}
					else if (text2 == "ORELSE")
					{
						return new ExpressionToken(TokenTypes.ORELSE, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					break;
				}
				case 7:
					if (text2 == "ANDALSO")
					{
						return new ExpressionToken(TokenTypes.ANDALSO, text, this.textRun.Line, this.textRun.Column, this.textRun.Column);
					}
					break;
				}
			}
			return new ExpressionToken(TokenTypes.IDENTIFIER, text, line, column, this.textRun.Column);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0002D354 File Offset: 0x0002B554
		private ExpressionToken ProcessDateTime(char ch)
		{
			int line = this.textRun.Line;
			int column = this.textRun.Column;
			this.ConsumeWhiteSpace();
			int num = this.ReadIntegerOnly(column, 1, 2);
			int num2 = 1;
			int num3 = 1;
			int num4 = 1;
			int num5 = 0;
			int num6 = 0;
			int num7 = 0;
			ch = this.textRun.Peek;
			if (ch == '-' || ch == '/')
			{
				char c = ch;
				this.textRun.Get();
				num2 = num;
				num3 = this.ReadIntegerOnly(column, 1, 2);
				if (this.textRun.Get() != c)
				{
					throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, this.textRun.Column);
				}
				num4 = this.ReadIntegerOnly(column, 4, 4);
				this.ConsumeWhiteSpace();
				if (this.textRun.Peek != '#')
				{
					num = this.ReadIntegerOnly(column, 1, 2);
					if (!this.ReadTime(num, column, out num5, out num6, out num7))
					{
						throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, this.textRun.Column);
					}
				}
			}
			else if (!this.ReadTime(num, column, out num5, out num6, out num7))
			{
				throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, this.textRun.Column);
			}
			this.ConsumeWhiteSpace();
			if (this.textRun.Get() != '#')
			{
				throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", column, this.textRun.Column);
			}
			DateTime dateTime = new DateTime(num4, num2, num3, num5, num6, num7, CultureInfo.InvariantCulture.Calendar);
			return new ExpressionToken(TokenTypes.DATETIME, dateTime.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0002D4DC File Offset: 0x0002B6DC
		private bool ReadTime(int firstNumber, int startCol, out int hour, out int minute, out int second)
		{
			hour = firstNumber;
			minute = 0;
			second = 0;
			char c = this.textRun.Peek;
			bool flag = false;
			if (c == ':')
			{
				this.textRun.Get();
				minute = this.ReadIntegerOnly(startCol, 1, 2);
				c = this.textRun.Peek;
				if (c == ':')
				{
					this.textRun.Get();
					second = this.ReadIntegerOnly(startCol, 1, 2);
				}
			}
			else
			{
				flag = true;
			}
			this.ConsumeWhiteSpace();
			c = this.textRun.Peek;
			if (c == 'A' || c == 'a')
			{
				this.textRun.Get();
				c = this.textRun.Get();
				if (c != 'M' && c != 'm')
				{
					return false;
				}
				if (hour > 12)
				{
					return false;
				}
				if (hour == 12)
				{
					hour = 0;
				}
			}
			else if (c == 'P' || c == 'p')
			{
				this.textRun.Get();
				c = this.textRun.Get();
				if (c != 'M' && c != 'm')
				{
					return false;
				}
				if (hour > 12)
				{
					return false;
				}
				hour += 12;
			}
			else if (flag)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0002D5E8 File Offset: 0x0002B7E8
		private bool ConsumeWhiteSpace()
		{
			bool flag = false;
			while (!this.textRun.FileEnd() && char.IsWhiteSpace(this.textRun.Peek))
			{
				flag = true;
				this.textRun.Get();
			}
			return flag;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0002D628 File Offset: 0x0002B828
		private int ReadIntegerOnly(int startCol, int minChars, int maxChars)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (!this.textRun.FileEnd())
			{
				char peek = this.textRun.Peek;
				if (!char.IsDigit(peek))
				{
					break;
				}
				this.textRun.Get();
				stringBuilder.Append(peek);
				num++;
			}
			int num2;
			if (num < minChars || num > maxChars || !int.TryParse(stringBuilder.ToString(), out num2))
			{
				throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidDateTimeLiteral", "Lexer", startCol, this.textRun.Column);
			}
			return num2;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0002D6AC File Offset: 0x0002B8AC
		private ExpressionToken ProcessNumber(char ch)
		{
			int line = this.textRun.Line;
			int column = this.textRun.Column;
			bool flag = false;
			TokenTypes tokenTypes = TokenTypes.EOF;
			string text = ch.ToString();
			bool flag2 = ch == '.';
			while (!this.textRun.FileEnd())
			{
				char c = this.textRun.Peek;
				char c2 = char.ToUpperInvariant(c);
				if (char.IsWhiteSpace(c))
				{
					break;
				}
				if (char.IsDigit(c))
				{
					text += this.textRun.Get().ToString();
				}
				else
				{
					if (!flag2 && !flag)
					{
						if (c2 == 'S')
						{
							tokenTypes = TokenTypes.SHORT;
							this.textRun.Get();
							break;
						}
						if (c2 == 'I' || c2 == '%')
						{
							tokenTypes = TokenTypes.INTEGER;
							this.textRun.Get();
							break;
						}
						if (c2 == 'L' || c2 == '&')
						{
							tokenTypes = TokenTypes.LONG;
							this.textRun.Get();
							break;
						}
						if (c2 == 'U')
						{
							char c3 = char.ToUpperInvariant(this.textRun.Peek2);
							if (c3 == 'S')
							{
								tokenTypes = TokenTypes.USHORT;
								this.textRun.Get();
								this.textRun.Get();
								break;
							}
							if (c3 == 'I')
							{
								tokenTypes = TokenTypes.UINTEGER;
								this.textRun.Get();
								this.textRun.Get();
								break;
							}
							if (c3 == 'L')
							{
								tokenTypes = TokenTypes.ULONG;
								this.textRun.Get();
								this.textRun.Get();
								break;
							}
						}
					}
					if (c2 == 'D' || c2 == '@')
					{
						tokenTypes = TokenTypes.DECIMAL;
						this.textRun.Get();
						break;
					}
					if (c2 == 'R' || c2 == '#')
					{
						tokenTypes = TokenTypes.DOUBLE;
						this.textRun.Get();
						break;
					}
					if (c2 == 'F')
					{
						tokenTypes = TokenTypes.SINGLE;
						this.textRun.Get();
						break;
					}
					if (c2 == '!')
					{
						char peek = this.textRun.Peek2;
						if (!char.IsLetter(peek) && peek != '_')
						{
							tokenTypes = TokenTypes.SINGLE;
							this.textRun.Get();
							break;
						}
						break;
					}
					else
					{
						if (c2 == 'E' && !flag)
						{
							text += this.textRun.Get().ToString();
							c = this.textRun.Peek;
							if (char.IsDigit(c) || c == '-' || c == '+')
							{
								flag = true;
								if (char.IsDigit(c))
								{
									text += this.textRun.Get().ToString();
									continue;
								}
								text += this.textRun.Get().ToString();
								if (char.IsDigit(this.textRun.Peek))
								{
									continue;
								}
							}
							throw new ExpressionParserException("RDLEngine.Error.RDLObjects.Expression.InvalidConstant", "Lexer", column - 2, column - 1);
						}
						if (flag2 || flag || c != '.' || !char.IsDigit(this.textRun.Peek2))
						{
							break;
						}
						flag2 = true;
						text += this.textRun.Get().ToString();
					}
				}
			}
			if (tokenTypes == TokenTypes.EOF)
			{
				if (flag || flag2)
				{
					tokenTypes = TokenTypes.DOUBLE;
				}
				else
				{
					tokenTypes = TokenTypes.INTEGER;
					int num;
					if (!int.TryParse(text, out num))
					{
						tokenTypes = TokenTypes.LONG;
					}
				}
			}
			return new ExpressionToken(tokenTypes, text, line, column, this.textRun.Column);
		}

		// Token: 0x04000657 RID: 1623
		private TokenList tokens;

		// Token: 0x04000658 RID: 1624
		private readonly CharReader textRun;

		// Token: 0x04000659 RID: 1625
		private bool bFinalPassNeeded;
	}
}
