using System;
using System.Globalization;
using AngleSharp.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200007E RID: 126
	internal sealed class CssTokenizer : BaseTokenizer
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060003D5 RID: 981 RVA: 0x00019F54 File Offset: 0x00018154
		// (remove) Token: 0x060003D6 RID: 982 RVA: 0x00019F8C File Offset: 0x0001818C
		public event EventHandler<CssErrorEvent> Error;

		// Token: 0x060003D7 RID: 983 RVA: 0x00019FC1 File Offset: 0x000181C1
		public CssTokenizer(TextSource source)
			: base(source)
		{
			this._valueMode = false;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00019FD1 File Offset: 0x000181D1
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00019FD9 File Offset: 0x000181D9
		public bool IsInValue
		{
			get
			{
				return this._valueMode;
			}
			set
			{
				this._valueMode = value;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00019FE4 File Offset: 0x000181E4
		public CssToken Get()
		{
			char next = base.GetNext();
			this._position = base.GetCurrentPosition();
			return this.Data(next);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001A00C File Offset: 0x0001820C
		internal void RaiseErrorOccurred(CssParseError error, TextPosition position)
		{
			EventHandler<CssErrorEvent> error2 = this.Error;
			if (error2 != null)
			{
				CssErrorEvent cssErrorEvent = new CssErrorEvent(error, position);
				error2(this, cssErrorEvent);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001A034 File Offset: 0x00018234
		private CssToken Data(char current)
		{
			this._position = base.GetCurrentPosition();
			if (current <= '^')
			{
				switch (current)
				{
				case '\t':
				case '\n':
				case '\f':
				case '\r':
				case ' ':
					return this.NewWhitespace(current);
				case '\v':
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
				case '%':
				case '&':
				case '=':
				case '>':
				case '?':
					goto IL_04BA;
				case '!':
					current = base.GetNext();
					if (current == '=')
					{
						return this.NewMatch(CombinatorSymbols.Unlike);
					}
					return this.NewDelimiter(base.GetPrevious());
				case '"':
					return this.StringDQ();
				case '#':
					if (!this._valueMode)
					{
						return this.HashStart();
					}
					return this.ColorLiteral();
				case '$':
					current = base.GetNext();
					if (current == '=')
					{
						return this.NewMatch(CombinatorSymbols.Ends);
					}
					return this.NewDelimiter(base.GetPrevious());
				case '\'':
					return this.StringSQ();
				case '(':
					return this.NewOpenRound();
				case ')':
					return this.NewCloseRound();
				case '*':
					current = base.GetNext();
					if (current == '=')
					{
						return this.NewMatch(CombinatorSymbols.InText);
					}
					return this.NewDelimiter(base.GetPrevious());
				case '+':
				{
					char next = base.GetNext();
					if (next != '\uffff')
					{
						char next2 = base.GetNext();
						base.Back(2);
						if (next.IsDigit() || (next == '.' && next2.IsDigit()))
						{
							return this.NumberStart(current);
						}
					}
					else
					{
						base.Back();
					}
					return this.NewDelimiter(current);
				}
				case ',':
					return this.NewComma();
				case '-':
				{
					char next3 = base.GetNext();
					if (next3 != '\uffff')
					{
						char next4 = base.GetNext();
						base.Back(2);
						if (next3.IsDigit() || (next3 == '.' && next4.IsDigit()))
						{
							return this.NumberStart(current);
						}
						if (next3.IsNameStart())
						{
							return this.IdentStart(current);
						}
						if (next3 == '\\' && !next4.IsLineBreak() && next4 != '\uffff')
						{
							return this.IdentStart(current);
						}
						if (next3 == '-' && next4 == '>')
						{
							base.Advance(2);
							return this.NewCloseComment();
						}
					}
					else
					{
						base.Back();
					}
					return this.NewDelimiter(current);
				}
				case '.':
					if (base.GetNext().IsDigit())
					{
						return this.NumberStart(base.GetPrevious());
					}
					return this.NewDelimiter(base.GetPrevious());
				case '/':
					current = base.GetNext();
					if (current == '*')
					{
						return this.Comment();
					}
					return this.NewDelimiter(base.GetPrevious());
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
					return this.NumberStart(current);
				case ':':
					return this.NewColon();
				case ';':
					return this.NewSemicolon();
				case '<':
					current = base.GetNext();
					if (current == '!')
					{
						current = base.GetNext();
						if (current == '-')
						{
							current = base.GetNext();
							if (current == '-')
							{
								return this.NewOpenComment();
							}
							current = base.GetPrevious();
						}
						current = base.GetPrevious();
					}
					return this.NewDelimiter(base.GetPrevious());
				case '@':
					return this.AtKeywordStart();
				default:
					if (current != 'U')
					{
						switch (current)
						{
						case '[':
							return this.NewOpenSquare();
						case '\\':
							current = base.GetNext();
							if (current.IsLineBreak())
							{
								this.RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
								return this.NewDelimiter(base.GetPrevious());
							}
							if (current == '\uffff')
							{
								this.RaiseErrorOccurred(CssParseError.EOF);
								return this.NewDelimiter(base.GetPrevious());
							}
							return this.IdentStart(base.GetPrevious());
						case ']':
							return this.NewCloseSquare();
						case '^':
							current = base.GetNext();
							if (current == '=')
							{
								return this.NewMatch(CombinatorSymbols.Begins);
							}
							return this.NewDelimiter(base.GetPrevious());
						default:
							goto IL_04BA;
						}
					}
					break;
				}
			}
			else if (current != 'u')
			{
				switch (current)
				{
				case '{':
					return this.NewOpenCurly();
				case '|':
					current = base.GetNext();
					if (current == '=')
					{
						return this.NewMatch(CombinatorSymbols.InToken);
					}
					if (current == '|')
					{
						return this.NewColumn();
					}
					return this.NewDelimiter(base.GetPrevious());
				case '}':
					return this.NewCloseCurly();
				case '~':
					current = base.GetNext();
					if (current == '=')
					{
						return this.NewMatch(CombinatorSymbols.InList);
					}
					return this.NewDelimiter(base.GetPrevious());
				default:
					if (current != '\uffff')
					{
						goto IL_04BA;
					}
					return this.NewEof();
				}
			}
			current = base.GetNext();
			if (current == '+')
			{
				current = base.GetNext();
				if (current.IsHex() || current == '?')
				{
					return this.UnicodeRange(current);
				}
				current = base.GetPrevious();
			}
			return this.IdentStart(base.GetPrevious());
			IL_04BA:
			if (current.IsNameStart())
			{
				return this.IdentStart(current);
			}
			return this.NewDelimiter(current);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001A514 File Offset: 0x00018714
		private CssToken StringDQ()
		{
			for (;;)
			{
				char c = base.GetNext();
				if (c <= '\f')
				{
					if (c == '\n' || c == '\f')
					{
						goto IL_0040;
					}
				}
				else
				{
					if (c == '"')
					{
						break;
					}
					if (c != '\\')
					{
						if (c == '\uffff')
						{
							break;
						}
					}
					else
					{
						c = base.GetNext();
						if (c.IsLineBreak())
						{
							base.StringBuffer.AppendLine();
							continue;
						}
						if (c != '\uffff')
						{
							base.StringBuffer.Append(this.ConsumeEscape(c));
							continue;
						}
						goto IL_009B;
					}
				}
				base.StringBuffer.Append(c);
			}
			return this.NewString(base.FlushBuffer(), '"', false);
			IL_0040:
			this.RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
			base.Back();
			return this.NewString(base.FlushBuffer(), '"', true);
			IL_009B:
			this.RaiseErrorOccurred(CssParseError.EOF);
			base.Back();
			return this.NewString(base.FlushBuffer(), '"', true);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001A5EC File Offset: 0x000187EC
		private CssToken StringSQ()
		{
			for (;;)
			{
				char c = base.GetNext();
				if (c <= '\f')
				{
					if (c == '\n' || c == '\f')
					{
						goto IL_0040;
					}
				}
				else
				{
					if (c == '\'')
					{
						break;
					}
					if (c != '\\')
					{
						if (c == '\uffff')
						{
							break;
						}
					}
					else
					{
						c = base.GetNext();
						if (c.IsLineBreak())
						{
							base.StringBuffer.AppendLine();
							continue;
						}
						if (c != '\uffff')
						{
							base.StringBuffer.Append(this.ConsumeEscape(c));
							continue;
						}
						goto IL_009B;
					}
				}
				base.StringBuffer.Append(c);
			}
			return this.NewString(base.FlushBuffer(), '\'', false);
			IL_0040:
			this.RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
			base.Back();
			return this.NewString(base.FlushBuffer(), '\'', true);
			IL_009B:
			this.RaiseErrorOccurred(CssParseError.EOF);
			base.Back();
			return this.NewString(base.FlushBuffer(), '\'', true);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001A6C4 File Offset: 0x000188C4
		private CssToken ColorLiteral()
		{
			char c = base.GetNext();
			while (c.IsHex())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			base.Back();
			return this.NewColor(base.FlushBuffer());
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001A708 File Offset: 0x00018908
		private CssToken HashStart()
		{
			char c = base.GetNext();
			if (c.IsNameStart())
			{
				base.StringBuffer.Append(c);
				return this.HashRest();
			}
			if (this.IsValidEscape(c))
			{
				c = base.GetNext();
				base.StringBuffer.Append(this.ConsumeEscape(c));
				return this.HashRest();
			}
			if (c == '\\')
			{
				this.RaiseErrorOccurred(CssParseError.InvalidCharacter);
				base.Back();
				return this.NewDelimiter('#');
			}
			base.Back();
			return this.NewDelimiter('#');
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001A78C File Offset: 0x0001898C
		private CssToken HashRest()
		{
			char c;
			for (;;)
			{
				c = base.GetNext();
				if (c.IsName())
				{
					base.StringBuffer.Append(c);
				}
				else
				{
					if (!this.IsValidEscape(c))
					{
						break;
					}
					c = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(c));
				}
			}
			if (c == '\\')
			{
				this.RaiseErrorOccurred(CssParseError.InvalidCharacter);
				base.Back();
				return this.NewHash(base.FlushBuffer());
			}
			base.Back();
			return this.NewHash(base.FlushBuffer());
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001A810 File Offset: 0x00018A10
		private CssToken Comment()
		{
			char c = base.GetNext();
			while (c != '\uffff')
			{
				if (c == '*')
				{
					c = base.GetNext();
					if (c == '/')
					{
						return this.NewComment(base.FlushBuffer(), false);
					}
					base.StringBuffer.Append('*');
				}
				else
				{
					base.StringBuffer.Append(c);
					c = base.GetNext();
				}
			}
			this.RaiseErrorOccurred(CssParseError.EOF);
			return this.NewComment(base.FlushBuffer(), true);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001A888 File Offset: 0x00018A88
		private CssToken AtKeywordStart()
		{
			char c = base.GetNext();
			if (c == '-')
			{
				c = base.GetNext();
				if (c.IsNameStart() || this.IsValidEscape(c))
				{
					base.StringBuffer.Append('-');
					return this.AtKeywordRest(c);
				}
				base.Back(2);
				return this.NewDelimiter('@');
			}
			else
			{
				if (c.IsNameStart())
				{
					base.StringBuffer.Append(c);
					return this.AtKeywordRest(base.GetNext());
				}
				if (this.IsValidEscape(c))
				{
					c = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(c));
					return this.AtKeywordRest(base.GetNext());
				}
				base.Back();
				return this.NewDelimiter('@');
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001A940 File Offset: 0x00018B40
		private CssToken AtKeywordRest(char current)
		{
			for (;;)
			{
				if (current.IsName())
				{
					base.StringBuffer.Append(current);
				}
				else
				{
					if (!this.IsValidEscape(current))
					{
						break;
					}
					current = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(current));
				}
				current = base.GetNext();
			}
			base.Back();
			return this.NewAtKeyword(base.FlushBuffer());
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		private CssToken IdentStart(char current)
		{
			if (current == '-')
			{
				current = base.GetNext();
				if (current.IsNameStart() || this.IsValidEscape(current))
				{
					base.StringBuffer.Append('-');
					return this.IdentRest(current);
				}
				base.Back();
				return this.NewDelimiter('-');
			}
			else
			{
				if (current.IsNameStart())
				{
					base.StringBuffer.Append(current);
					return this.IdentRest(base.GetNext());
				}
				if (current == '\\' && this.IsValidEscape(current))
				{
					current = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(current));
					return this.IdentRest(base.GetNext());
				}
				return this.Data(current);
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001AA58 File Offset: 0x00018C58
		private CssToken IdentRest(char current)
		{
			for (;;)
			{
				if (current.IsName())
				{
					base.StringBuffer.Append(current);
				}
				else
				{
					if (!this.IsValidEscape(current))
					{
						break;
					}
					current = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(current));
				}
				current = base.GetNext();
			}
			if (current != '(')
			{
				base.Back();
				return this.NewIdent(base.FlushBuffer());
			}
			string text = base.FlushBuffer();
			if (text.GetTypeFromName() != CssTokenType.Function)
			{
				return this.UrlStart(text);
			}
			return this.NewFunction(text);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001AAE3 File Offset: 0x00018CE3
		private CssToken TransformFunctionWhitespace(char current)
		{
			for (;;)
			{
				current = base.GetNext();
				if (current == '(')
				{
					break;
				}
				if (!current.IsSpaceCharacter())
				{
					goto Block_1;
				}
			}
			base.Back();
			return this.NewFunction(base.FlushBuffer());
			Block_1:
			base.Back(2);
			return this.NewIdent(base.FlushBuffer());
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001AB20 File Offset: 0x00018D20
		private CssToken NumberStart(char current)
		{
			while (!current.IsOneOf('+', '-'))
			{
				if (current == '.')
				{
					base.StringBuffer.Append(current);
					base.StringBuffer.Append(base.GetNext());
					return this.NumberFraction();
				}
				if (current.IsDigit())
				{
					base.StringBuffer.Append(current);
					return this.NumberRest();
				}
				current = base.GetNext();
			}
			base.StringBuffer.Append(current);
			current = base.GetNext();
			if (current == '.')
			{
				base.StringBuffer.Append(current);
				base.StringBuffer.Append(base.GetNext());
				return this.NumberFraction();
			}
			base.StringBuffer.Append(current);
			return this.NumberRest();
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001ABE0 File Offset: 0x00018DE0
		private CssToken NumberRest()
		{
			char c = base.GetNext();
			while (c.IsDigit())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			if (c.IsNameStart())
			{
				string text = base.FlushBuffer();
				base.StringBuffer.Append(c);
				return this.Dimension(text);
			}
			if (this.IsValidEscape(c))
			{
				c = base.GetNext();
				string text2 = base.FlushBuffer();
				base.StringBuffer.Append(this.ConsumeEscape(c));
				return this.Dimension(text2);
			}
			if (c <= '-')
			{
				if (c == '%')
				{
					return this.NewPercentage(base.FlushBuffer());
				}
				if (c == '-')
				{
					return this.NumberDash();
				}
			}
			else if (c != '.')
			{
				if (c == 'E' || c == 'e')
				{
					return this.NumberExponential(c);
				}
			}
			else
			{
				c = base.GetNext();
				if (c.IsDigit())
				{
					base.StringBuffer.Append('.').Append(c);
					return this.NumberFraction();
				}
				base.Back();
				return this.NewNumber(base.FlushBuffer());
			}
			base.Back();
			return this.NewNumber(base.FlushBuffer());
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		private CssToken NumberFraction()
		{
			char c = base.GetNext();
			while (c.IsDigit())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			if (c.IsNameStart())
			{
				string text = base.FlushBuffer();
				base.StringBuffer.Append(c);
				return this.Dimension(text);
			}
			if (this.IsValidEscape(c))
			{
				c = base.GetNext();
				string text2 = base.FlushBuffer();
				base.StringBuffer.Append(this.ConsumeEscape(c));
				return this.Dimension(text2);
			}
			if (c <= '-')
			{
				if (c == '%')
				{
					return this.NewPercentage(base.FlushBuffer());
				}
				if (c == '-')
				{
					return this.NumberDash();
				}
			}
			else if (c == 'E' || c == 'e')
			{
				return this.NumberExponential(c);
			}
			base.Back();
			return this.NewNumber(base.FlushBuffer());
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001ADCC File Offset: 0x00018FCC
		private CssToken Dimension(string number)
		{
			for (;;)
			{
				char c = base.GetNext();
				if (c.IsLetter())
				{
					base.StringBuffer.Append(c);
				}
				else
				{
					if (!this.IsValidEscape(c))
					{
						break;
					}
					c = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(c));
				}
			}
			base.Back();
			return this.NewDimension(number, base.FlushBuffer());
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001AE30 File Offset: 0x00019030
		private CssToken SciNotation()
		{
			for (;;)
			{
				char next = base.GetNext();
				if (!next.IsDigit())
				{
					break;
				}
				base.StringBuffer.Append(next);
			}
			base.Back();
			return this.NewNumber(base.FlushBuffer());
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001AE70 File Offset: 0x00019070
		private CssToken UrlStart(string functionName)
		{
			char c = base.SkipSpaces();
			if (c <= '\'')
			{
				if (c == '"')
				{
					return this.UrlDQ(functionName);
				}
				if (c == '\'')
				{
					return this.UrlSQ(functionName);
				}
			}
			else
			{
				if (c == ')')
				{
					return this.NewUrl(functionName, string.Empty, false);
				}
				if (c == '\uffff')
				{
					this.RaiseErrorOccurred(CssParseError.EOF);
					return this.NewUrl(functionName, string.Empty, true);
				}
			}
			return this.UrlUQ(c, functionName);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001AEE0 File Offset: 0x000190E0
		private CssToken UrlDQ(string functionName)
		{
			for (;;)
			{
				char c = base.GetNext();
				if (c.IsLineBreak())
				{
					break;
				}
				if ('\uffff' == c)
				{
					goto Block_1;
				}
				if (c == '"')
				{
					goto Block_2;
				}
				if (c != '\\')
				{
					base.StringBuffer.Append(c);
				}
				else
				{
					c = base.GetNext();
					if (c == '\uffff')
					{
						goto Block_4;
					}
					if (c.IsLineBreak())
					{
						base.StringBuffer.AppendLine();
					}
					else
					{
						base.StringBuffer.Append(this.ConsumeEscape(c));
					}
				}
			}
			this.RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
			return this.UrlBad(functionName);
			Block_1:
			return this.NewUrl(functionName, base.FlushBuffer(), false);
			Block_2:
			return this.UrlEnd(functionName);
			Block_4:
			base.Back(2);
			this.RaiseErrorOccurred(CssParseError.EOF);
			return this.NewUrl(functionName, base.FlushBuffer(), true);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001AFA0 File Offset: 0x000191A0
		private CssToken UrlSQ(string functionName)
		{
			for (;;)
			{
				char c = base.GetNext();
				if (c.IsLineBreak())
				{
					break;
				}
				if (c == '\uffff')
				{
					goto Block_1;
				}
				if (c == '\'')
				{
					goto Block_2;
				}
				if (c != '\\')
				{
					base.StringBuffer.Append(c);
				}
				else
				{
					c = base.GetNext();
					if (c == '\uffff')
					{
						goto Block_4;
					}
					if (c.IsLineBreak())
					{
						base.StringBuffer.AppendLine();
					}
					else
					{
						base.StringBuffer.Append(this.ConsumeEscape(c));
					}
				}
			}
			this.RaiseErrorOccurred(CssParseError.LineBreakUnexpected);
			return this.UrlBad(functionName);
			Block_1:
			return this.NewUrl(functionName, base.FlushBuffer(), false);
			Block_2:
			return this.UrlEnd(functionName);
			Block_4:
			base.Back(2);
			this.RaiseErrorOccurred(CssParseError.EOF);
			return this.NewUrl(functionName, base.FlushBuffer(), true);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001B060 File Offset: 0x00019260
		private CssToken UrlUQ(char current, string functionName)
		{
			while (!current.IsSpaceCharacter())
			{
				if (current.IsOneOf(')', '\uffff'))
				{
					return this.NewUrl(functionName, base.FlushBuffer(), false);
				}
				if (current.IsOneOf('"', '\'', '(') || current.IsNonPrintable())
				{
					this.RaiseErrorOccurred(CssParseError.InvalidCharacter);
					return this.UrlBad(functionName);
				}
				if (current != '\\')
				{
					base.StringBuffer.Append(current);
				}
				else
				{
					if (!this.IsValidEscape(current))
					{
						this.RaiseErrorOccurred(CssParseError.InvalidCharacter);
						return this.UrlBad(functionName);
					}
					current = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(current));
				}
				current = base.GetNext();
			}
			return this.UrlEnd(functionName);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001B118 File Offset: 0x00019318
		private CssToken UrlEnd(string functionName)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next == ')')
				{
					break;
				}
				if (!next.IsSpaceCharacter())
				{
					goto Block_1;
				}
			}
			return this.NewUrl(functionName, base.FlushBuffer(), false);
			Block_1:
			this.RaiseErrorOccurred(CssParseError.InvalidCharacter);
			base.Back();
			return this.UrlBad(functionName);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001B160 File Offset: 0x00019360
		private CssToken UrlBad(string functionName)
		{
			char c = base.Current;
			int num = 0;
			int num2 = 1;
			while (c != '\uffff')
			{
				if (c == ';')
				{
					base.Back();
					return this.NewUrl(functionName, base.FlushBuffer(), true);
				}
				if (c == '}' && --num == -1)
				{
					base.Back();
					return this.NewUrl(functionName, base.FlushBuffer(), true);
				}
				if (c == ')' && --num2 == 0)
				{
					return this.NewUrl(functionName, base.FlushBuffer(), true);
				}
				if (this.IsValidEscape(c))
				{
					c = base.GetNext();
					base.StringBuffer.Append(this.ConsumeEscape(c));
				}
				else
				{
					if (c == '(')
					{
						num2++;
					}
					else if (num == 123)
					{
						num++;
					}
					base.StringBuffer.Append(c);
				}
				c = base.GetNext();
			}
			this.RaiseErrorOccurred(CssParseError.EOF);
			return this.NewUrl(functionName, base.FlushBuffer(), true);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001B244 File Offset: 0x00019444
		private CssToken UnicodeRange(char current)
		{
			int num = 0;
			while (num < 6 && current.IsHex())
			{
				base.StringBuffer.Append(current);
				current = base.GetNext();
				num++;
			}
			if (base.StringBuffer.Length != 6)
			{
				for (int i = 0; i < 6 - base.StringBuffer.Length; i++)
				{
					if (current != '?')
					{
						current = base.GetPrevious();
						break;
					}
					base.StringBuffer.Append(current);
					current = base.GetNext();
				}
				return this.NewRange(base.FlushBuffer());
			}
			if (current != '-')
			{
				base.Back();
				return this.NewRange(base.FlushBuffer());
			}
			current = base.GetNext();
			if (current.IsHex())
			{
				string text = base.FlushBuffer();
				for (int j = 0; j < 6; j++)
				{
					if (!current.IsHex())
					{
						current = base.GetPrevious();
						break;
					}
					base.StringBuffer.Append(current);
					current = base.GetNext();
				}
				string text2 = base.FlushBuffer();
				return this.NewRange(text, text2);
			}
			base.Back(2);
			return this.NewRange(base.FlushBuffer());
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001B35A File Offset: 0x0001955A
		private CssToken NewMatch(string match)
		{
			return new CssToken(CssTokenType.Match, match, this._position);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001B36A File Offset: 0x0001956A
		private CssToken NewColumn()
		{
			return new CssToken(CssTokenType.Column, CombinatorSymbols.Column, this._position);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001B37E File Offset: 0x0001957E
		private CssToken NewCloseCurly()
		{
			return new CssToken(CssTokenType.CurlyBracketClose, "}", this._position);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001B392 File Offset: 0x00019592
		private CssToken NewOpenCurly()
		{
			return new CssToken(CssTokenType.CurlyBracketOpen, "{", this._position);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001B3A6 File Offset: 0x000195A6
		private CssToken NewCloseSquare()
		{
			return new CssToken(CssTokenType.SquareBracketClose, "]", this._position);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001B3BA File Offset: 0x000195BA
		private CssToken NewOpenSquare()
		{
			return new CssToken(CssTokenType.SquareBracketOpen, "[", this._position);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001B3CE File Offset: 0x000195CE
		private CssToken NewOpenComment()
		{
			return new CssToken(CssTokenType.Cdo, "<!--", this._position);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001B3E2 File Offset: 0x000195E2
		private CssToken NewSemicolon()
		{
			return new CssToken(CssTokenType.Semicolon, ";", this._position);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001B3F6 File Offset: 0x000195F6
		private CssToken NewColon()
		{
			return new CssToken(CssTokenType.Colon, ":", this._position);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001B40A File Offset: 0x0001960A
		private CssToken NewCloseComment()
		{
			return new CssToken(CssTokenType.Cdc, "-->", this._position);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001B41E File Offset: 0x0001961E
		private CssToken NewComma()
		{
			return new CssToken(CssTokenType.Comma, ",", this._position);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001B432 File Offset: 0x00019632
		private CssToken NewCloseRound()
		{
			return new CssToken(CssTokenType.RoundBracketClose, ")", this._position);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001B446 File Offset: 0x00019646
		private CssToken NewOpenRound()
		{
			return new CssToken(CssTokenType.RoundBracketOpen, "(", this._position);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001B45A File Offset: 0x0001965A
		private CssToken NewString(string value, char quote, bool bad = false)
		{
			return new CssStringToken(value, bad, quote, this._position);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001B46A File Offset: 0x0001966A
		private CssToken NewHash(string value)
		{
			return new CssKeywordToken(CssTokenType.Hash, value, this._position);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001B479 File Offset: 0x00019679
		private CssToken NewComment(string value, bool bad = false)
		{
			return new CssCommentToken(value, bad, this._position);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001B488 File Offset: 0x00019688
		private CssToken NewAtKeyword(string value)
		{
			return new CssKeywordToken(CssTokenType.AtKeyword, value, this._position);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001B497 File Offset: 0x00019697
		private CssToken NewIdent(string value)
		{
			return new CssKeywordToken(CssTokenType.Ident, value, this._position);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001B4A8 File Offset: 0x000196A8
		private CssToken NewFunction(string value)
		{
			CssFunctionToken cssFunctionToken = new CssFunctionToken(value, this._position);
			CssToken cssToken = this.Get();
			while (cssToken.Type != CssTokenType.EndOfFile)
			{
				cssFunctionToken.AddArgumentToken(cssToken);
				if (cssToken.Type == CssTokenType.RoundBracketClose)
				{
					break;
				}
				cssToken = this.Get();
			}
			return cssFunctionToken;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001B4EE File Offset: 0x000196EE
		private CssToken NewPercentage(string value)
		{
			return new CssUnitToken(CssTokenType.Percentage, value, "%", this._position);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001B503 File Offset: 0x00019703
		private CssToken NewDimension(string value, string unit)
		{
			return new CssUnitToken(CssTokenType.Dimension, value, unit, this._position);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001B514 File Offset: 0x00019714
		private CssToken NewUrl(string functionName, string data, bool bad = false)
		{
			return new CssUrlToken(functionName, data, bad, this._position);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001B524 File Offset: 0x00019724
		private CssToken NewRange(string range)
		{
			return new CssRangeToken(range, this._position);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001B532 File Offset: 0x00019732
		private CssToken NewRange(string start, string end)
		{
			return new CssRangeToken(start, end, this._position);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001B541 File Offset: 0x00019741
		private CssToken NewWhitespace(char c)
		{
			return new CssToken(CssTokenType.Whitespace, c.ToString(), this._position);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001B557 File Offset: 0x00019757
		private CssToken NewNumber(string number)
		{
			return new CssNumberToken(number, this._position);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001B565 File Offset: 0x00019765
		private CssToken NewDelimiter(char c)
		{
			return new CssToken(CssTokenType.Delim, c.ToString(), this._position);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001B57B File Offset: 0x0001977B
		private CssToken NewColor(string text)
		{
			return new CssColorToken(text, this._position);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001B589 File Offset: 0x00019789
		private CssToken NewEof()
		{
			return new CssToken(CssTokenType.EndOfFile, string.Empty, this._position);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001B5A0 File Offset: 0x000197A0
		private CssToken NumberExponential(char letter)
		{
			char c = base.GetNext();
			if (c.IsDigit())
			{
				base.StringBuffer.Append(letter).Append(c);
				return this.SciNotation();
			}
			if (c == '+' || c == '-')
			{
				char c2 = c;
				c = base.GetNext();
				if (c.IsDigit())
				{
					base.StringBuffer.Append(letter).Append(c2).Append(c);
					return this.SciNotation();
				}
				base.Back();
			}
			string text = base.FlushBuffer();
			base.StringBuffer.Append(letter);
			base.Back();
			return this.Dimension(text);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001B638 File Offset: 0x00019838
		private CssToken NumberDash()
		{
			char c = base.GetNext();
			if (c.IsNameStart())
			{
				string text = base.FlushBuffer();
				base.StringBuffer.Append('-').Append(c);
				return this.Dimension(text);
			}
			if (this.IsValidEscape(c))
			{
				c = base.GetNext();
				string text2 = base.FlushBuffer();
				base.StringBuffer.Append('-').Append(this.ConsumeEscape(c));
				return this.Dimension(text2);
			}
			base.Back(2);
			return this.NewNumber(base.FlushBuffer());
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001B6C4 File Offset: 0x000198C4
		private string ConsumeEscape(char current)
		{
			if (current.IsHex())
			{
				bool flag = true;
				char[] array = new char[6];
				int num = 0;
				while (flag && num < array.Length)
				{
					array[num++] = current;
					current = base.GetNext();
					flag = current.IsHex();
				}
				if (!current.IsSpaceCharacter())
				{
					base.Back();
				}
				int num2 = int.Parse(new string(array, 0, num), NumberStyles.HexNumber);
				if (!num2.IsInvalid())
				{
					return num2.ConvertFromUtf32();
				}
				current = '\ufffd';
			}
			return current.ToString();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001B744 File Offset: 0x00019944
		private bool IsValidEscape(char current)
		{
			if (current == '\\')
			{
				current = base.GetNext();
				base.Back();
				return current != char.MaxValue && !current.IsLineBreak();
			}
			return false;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001B76E File Offset: 0x0001996E
		private void RaiseErrorOccurred(CssParseError code)
		{
			this.RaiseErrorOccurred(code, base.GetCurrentPosition());
		}

		// Token: 0x0400030A RID: 778
		private bool _valueMode;

		// Token: 0x0400030B RID: 779
		private TextPosition _position;
	}
}
