using System;
using System.Globalization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018B2 RID: 6322
	internal static class TextTokens
	{
		// Token: 0x0600A104 RID: 41220 RVA: 0x00215F24 File Offset: 0x00214124
		public static ITokens New(SegmentedString text)
		{
			LruCache<SegmentedString, ITokens> lruCache = TextTokens.cache;
			lock (lruCache)
			{
				ITokens tokens;
				if (TextTokens.cache.TryGetValue(text, out tokens))
				{
					return tokens;
				}
			}
			TokensBuilder tokensBuilder = new TokensBuilder();
			tokensBuilder.Add(TokenType.Bof, 0);
			TextTokens.AddTo(tokensBuilder, text, 0, text.Length);
			tokensBuilder.Add(TokenType.Eof, 0);
			ITokens tokens2 = tokensBuilder.ToTokens(text);
			TextTokens.Verify(tokens2, text);
			lruCache = TextTokens.cache;
			lock (lruCache)
			{
				ITokens tokens3;
				if (TextTokens.cache.TryGetValue(text, out tokens3))
				{
					tokens2 = tokens3;
				}
				else
				{
					TextTokens.cache.Add(text, tokens2);
				}
			}
			return tokens2;
		}

		// Token: 0x0600A105 RID: 41221 RVA: 0x00215FF0 File Offset: 0x002141F0
		private static void AddTo(TokensBuilder builder, SegmentedString text, int offset, int limit)
		{
			while (offset < limit)
			{
				int num = offset;
				char c = text[offset++];
				TokenType tokenType;
				if (c <= '{')
				{
					if (c <= '[')
					{
						switch (c)
						{
						case '\t':
						case '\n':
						case '\v':
						case '\f':
						case '\r':
						case ' ':
							goto IL_0160;
						case '\u000e':
						case '\u000f':
						case '\u0010':
						case '\u0011':
						case '\u0012':
						case '\u0013':
						case '\u0014':
						case '\u0015':
						case '\u0016':
						case '\u0017':
						case '\u0018':
						case '\u0019':
						case '\u001a':
						case '\u001b':
						case '\u001c':
						case '\u001d':
						case '\u001e':
						case '\u001f':
						case '$':
						case '%':
						case '\'':
						case ':':
							goto IL_04A6;
						case '!':
							tokenType = TokenType.Bang;
							break;
						case '"':
							tokenType = TokenType.Literal;
							offset = TextTokens.ReadStringLiteral(text, offset - 1, limit);
							break;
						case '#':
							tokenType = TokenType.TokenError;
							if (offset < limit)
							{
								char c2 = text[offset];
								if (c2 != '!')
								{
									if (c2 == '"')
									{
										tokenType = TokenType.Identifier;
										offset = TextTokens.ReadStringLiteral(text, offset, limit);
									}
									else if (TextTokens.IsIdentifierStartCharacter(text[offset]))
									{
										int num2 = offset;
										offset = TextTokens.ReadIdentifier(text, offset, limit);
										int num3 = offset - num2;
										if (num3 == Keyword.Shared.SegmentedText.Length && Keyword.Shared.SegmentedText.CompareOrdinal(0, text, num2, num3) == 0)
										{
											tokenType = TokenType.HashShared;
										}
										else if (num3 == Keyword.Sections.SegmentedText.Length && Keyword.Sections.SegmentedText.CompareOrdinal(0, text, num2, num3) == 0)
										{
											tokenType = TokenType.HashSections;
										}
										else
										{
											tokenType = TokenType.Literal;
										}
									}
								}
								else
								{
									tokenType = TokenType.Verbatim;
									offset++;
								}
							}
							break;
						case '&':
							tokenType = TokenType.Ampersand;
							break;
						case '(':
							tokenType = TokenType.LeftParen;
							break;
						case ')':
							tokenType = TokenType.RightParen;
							break;
						case '*':
							tokenType = TokenType.Multiply;
							break;
						case '+':
							tokenType = TokenType.Plus;
							break;
						case ',':
							tokenType = TokenType.Comma;
							break;
						case '-':
							tokenType = TokenType.Minus;
							break;
						case '.':
							tokenType = TokenType.TokenError;
							if (offset < limit)
							{
								switch (text[offset])
								{
								case '.':
									tokenType = TokenType.DotDot;
									offset++;
									if (offset < limit && text[offset] == '.')
									{
										tokenType = TokenType.Ellipsis;
										offset++;
									}
									break;
								case '0':
								case '1':
								case '2':
								case '3':
								case '4':
								case '5':
								case '6':
								case '7':
								case '8':
								case '9':
									tokenType = TokenType.Literal;
									offset = TextTokens.ReadNumericLiteral(text, offset - 1, limit);
									break;
								}
							}
							break;
						case '/':
							tokenType = TokenType.Divide;
							if (offset < limit)
							{
								char c2 = text[offset];
								if (c2 != '*')
								{
									if (c2 == '/')
									{
										tokenType = TokenType.Whitespace;
										offset = TextTokens.ReadSingleLineComment(text, offset - 1, limit);
									}
								}
								else
								{
									tokenType = TokenType.Whitespace;
									bool flag;
									offset = TextTokens.ReadDelimitedComment(text, offset - 1, limit, out flag);
									if (!flag)
									{
										tokenType = TokenType.TokenError;
									}
								}
							}
							break;
						case '0':
						{
							char c3 = ((offset < limit) ? text[offset] : '\0');
							if (c3 == 'x' || c3 == 'X')
							{
								tokenType = TokenType.Literal;
								offset = TextTokens.ReadHexIntegerLiteral(text, offset - 1, limit);
							}
							else
							{
								tokenType = TokenType.Literal;
								offset = TextTokens.ReadNumericLiteral(text, offset - 1, limit);
							}
							break;
						}
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							tokenType = TokenType.Literal;
							offset = TextTokens.ReadNumericLiteral(text, offset - 1, limit);
							break;
						case ';':
							tokenType = TokenType.Semicolon;
							break;
						case '<':
							tokenType = TokenType.LessThan;
							if (offset < limit && text[offset] == '=')
							{
								tokenType = TokenType.LessThanOrEqual;
								offset++;
							}
							if (offset < limit && text[offset] == '>')
							{
								tokenType = TokenType.NotEqual;
								offset++;
							}
							break;
						case '=':
							tokenType = TokenType.Equal;
							if (offset < limit && text[offset] == '>')
							{
								tokenType = TokenType.GoesTo;
								offset++;
							}
							break;
						case '>':
							tokenType = TokenType.GreaterThan;
							if (offset < limit && text[offset] == '=')
							{
								tokenType = TokenType.GreaterThanOrEqual;
								offset++;
							}
							break;
						case '?':
							tokenType = TokenType.QuestionMark;
							if (offset < limit && text[offset] == '?')
							{
								tokenType = TokenType.Coalesce;
								offset++;
							}
							break;
						case '@':
							tokenType = TokenType.At;
							break;
						default:
							if (c != '[')
							{
								goto IL_04A6;
							}
							tokenType = TokenType.LeftBracket;
							break;
						}
					}
					else if (c != ']')
					{
						if (c != '{')
						{
							goto IL_04A6;
						}
						tokenType = TokenType.LeftBrace;
					}
					else
					{
						tokenType = TokenType.RightBracket;
					}
				}
				else if (c <= '\u0085')
				{
					if (c != '}')
					{
						if (c != '\u0085')
						{
							goto IL_04A6;
						}
						goto IL_0160;
					}
					else
					{
						tokenType = TokenType.RightBrace;
					}
				}
				else
				{
					if (c == '\u2028' || c == '\u2029')
					{
						goto IL_0160;
					}
					goto IL_04A6;
				}
				IL_04F7:
				builder.Add(tokenType, offset - num);
				continue;
				IL_0160:
				tokenType = TokenType.Whitespace;
				offset = TextTokens.ReadWhitespace(text, offset, limit);
				goto IL_04F7;
				IL_04A6:
				if (TextTokens.IsIdentifierStartCharacter(c))
				{
					int num4 = TextTokens.ReadIdentifier(text, offset - 1, limit);
					if (num4 > num)
					{
						offset = num4;
						tokenType = TextTokens.GetKeyword(text, num, offset - num);
						goto IL_04F7;
					}
					tokenType = TokenType.TokenError;
					goto IL_04F7;
				}
				else
				{
					if (c >= '\u0080' && char.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator)
					{
						tokenType = TokenType.Whitespace;
						offset = TextTokens.ReadWhitespace(text, offset, limit);
						goto IL_04F7;
					}
					tokenType = TokenType.TokenError;
					goto IL_04F7;
				}
			}
		}

		// Token: 0x0600A106 RID: 41222 RVA: 0x0000336E File Offset: 0x0000156E
		private static void Verify(ITokens tokens, SegmentedString text)
		{
		}

		// Token: 0x0600A107 RID: 41223 RVA: 0x00216508 File Offset: 0x00214708
		private static int ReadNumericLiteral(SegmentedString s, int offset, int limit)
		{
			offset = TextTokens.ReadDecimalDigits(s, offset, limit);
			if (offset < limit && s[offset] == '.' && offset + 1 < limit && TextTokens.IsDecimalDigit(s[offset + 1]))
			{
				offset = TextTokens.ReadDecimalDigits(s, offset + 2, limit);
			}
			if (offset < limit && (s[offset] == 'E' || s[offset] == 'e'))
			{
				int num = offset + 1;
				if (num < limit && (s[num] == '+' || s[num] == '-'))
				{
					num++;
				}
				if (num < limit && TextTokens.IsDecimalDigit(s[num]))
				{
					offset = TextTokens.ReadDecimalDigits(s, num, limit);
				}
			}
			if (offset < limit && (s[offset] == 'd' || s[offset] == 'D'))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A108 RID: 41224 RVA: 0x002165D1 File Offset: 0x002147D1
		private static int ReadHexIntegerLiteral(SegmentedString s, int offset, int limit)
		{
			offset += 2;
			while (offset < limit && HexEncoding.IsDigit(s[offset]))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A109 RID: 41225 RVA: 0x002165F4 File Offset: 0x002147F4
		public static int ReadIdentifier(SegmentedString s, int offset, int limit)
		{
			offset++;
			while (offset < limit && (TextTokens.IsIdentifierPartCharacter(s[offset]) || (s[offset] == '.' && offset + 1 < limit && s[offset + 1] != '.')))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A10A RID: 41226 RVA: 0x00216641 File Offset: 0x00214841
		public static bool IsIdentifierStartCharacter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z') || character == '_' || (character >= '\u0080' && !TextTokens.IsWhitespace(character));
		}

		// Token: 0x0600A10B RID: 41227 RVA: 0x00216671 File Offset: 0x00214871
		private static bool IsIdentifierPartCharacter(char character)
		{
			return (character >= '0' && character <= '9') || TextTokens.IsIdentifierStartCharacter(character);
		}

		// Token: 0x0600A10C RID: 41228 RVA: 0x00216685 File Offset: 0x00214885
		private static int ReadStringLiteral(SegmentedString s, int offset, int limit)
		{
			offset++;
			while (offset < limit)
			{
				if (s[offset++] == '"')
				{
					if (offset >= limit || s[offset] != '"')
					{
						return offset;
					}
					offset++;
				}
			}
			return offset;
		}

		// Token: 0x0600A10D RID: 41229 RVA: 0x002166BB File Offset: 0x002148BB
		public static int ReadNonBreakingWhitespace(SegmentedString s, int offset, int limit)
		{
			while (offset < limit && (s[offset] == ' ' || (TextTokens.IsWhitespace(s[offset]) && !TextTokens.IsNewLineCharacter((int)s[offset]))))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A10E RID: 41230 RVA: 0x002166F3 File Offset: 0x002148F3
		public static int ReadBreakingWhitespace(SegmentedString s, int offset, int limit)
		{
			while (offset < limit && TextTokens.IsNewLineCharacter((int)s[offset]))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A10F RID: 41231 RVA: 0x00216710 File Offset: 0x00214910
		private static int ReadWhitespace(SegmentedString s, int offset, int limit)
		{
			while (offset < limit && (s[offset] == ' ' || TextTokens.IsWhitespace(s[offset])))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A110 RID: 41232 RVA: 0x0021673C File Offset: 0x0021493C
		private static bool IsWhitespace(char ch)
		{
			if (ch > ' ' && ch < '\u0080')
			{
				return false;
			}
			if (ch <= '\u0085')
			{
				switch (ch)
				{
				case '\t':
				case '\n':
				case '\v':
				case '\f':
				case '\r':
					break;
				default:
					if (ch != ' ' && ch != '\u0085')
					{
						goto IL_005F;
					}
					break;
				}
			}
			else
			{
				if (ch == '\u180e')
				{
					return false;
				}
				if (ch != '\u2028' && ch != '\u2029')
				{
					goto IL_005F;
				}
			}
			return true;
			IL_005F:
			return char.GetUnicodeCategory(ch) == UnicodeCategory.SpaceSeparator;
		}

		// Token: 0x0600A111 RID: 41233 RVA: 0x002167B2 File Offset: 0x002149B2
		private static int ReadSingleLineComment(SegmentedString s, int offset, int limit)
		{
			offset += 2;
			while (offset < limit && !TextTokens.IsNewLineCharacter((int)s[offset]))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A112 RID: 41234 RVA: 0x002167D4 File Offset: 0x002149D4
		private static int ReadDelimitedComment(SegmentedString s, int offset, int limit, out bool hasCommentEnd)
		{
			hasCommentEnd = false;
			for (offset += 2; offset < limit; offset++)
			{
				if (s[offset] == '*' && offset + 1 < limit && s[offset + 1] == '/')
				{
					offset += 2;
					hasCommentEnd = true;
					break;
				}
			}
			return offset;
		}

		// Token: 0x0600A113 RID: 41235 RVA: 0x00216814 File Offset: 0x00214A14
		private static int ReadDecimalDigits(SegmentedString s, int offset, int limit)
		{
			while (offset < limit && TextTokens.IsDecimalDigit(s[offset]))
			{
				offset++;
			}
			return offset;
		}

		// Token: 0x0600A114 RID: 41236 RVA: 0x00216831 File Offset: 0x00214A31
		private static bool IsDecimalDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x0600A115 RID: 41237 RVA: 0x00216844 File Offset: 0x00214A44
		public static TokenType GetKeyword(SegmentedString s, int offset, int length)
		{
			Keyword keyword = Keyword.GetKeyword8(s, offset, length);
			if (keyword == null)
			{
				return TokenType.Identifier;
			}
			switch (keyword.Type)
			{
			case KeywordType8.As:
				return TokenType.As;
			case KeywordType8.Each:
				return TokenType.Each;
			case KeywordType8.Else:
				return TokenType.Else;
			case KeywordType8.Error:
				return TokenType.Error;
			case KeywordType8.False:
				return TokenType.Literal;
			case KeywordType8.If:
				return TokenType.If;
			case KeywordType8.In:
				return TokenType.In;
			case KeywordType8.Is:
				return TokenType.Is;
			case KeywordType8.Let:
				return TokenType.Let;
			case KeywordType8.LogicalAnd:
				return TokenType.LogicalAnd;
			case KeywordType8.LogicalOr:
				return TokenType.LogicalOr;
			case KeywordType8.Meta:
				return TokenType.Meta;
			case KeywordType8.Not:
				return TokenType.Not;
			case KeywordType8.Null:
				return TokenType.Literal;
			case KeywordType8.Otherwise:
				return TokenType.Otherwise;
			case KeywordType8.Shared:
				return TokenType.Shared;
			case KeywordType8.Section:
				return TokenType.Section;
			case KeywordType8.Then:
				return TokenType.Then;
			case KeywordType8.True:
				return TokenType.Literal;
			case KeywordType8.Try:
				return TokenType.Try;
			case KeywordType8.Type:
				return TokenType.Type;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x0600A116 RID: 41238 RVA: 0x00216917 File Offset: 0x00214B17
		private static bool IsNewLineCharacter(int ch)
		{
			if (ch <= 13)
			{
				if (ch != 10 && ch != 13)
				{
					return false;
				}
			}
			else if (ch != 133 && ch - 8232 > 1)
			{
				return false;
			}
			return true;
		}

		// Token: 0x0400546D RID: 21613
		private const char NoLongerUnicodeWhiteSpace_180E = '\u180e';

		// Token: 0x0400546E RID: 21614
		private static readonly LruCache<SegmentedString, ITokens> cache = new LruCache<SegmentedString, ITokens>(16, null);
	}
}
