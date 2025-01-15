using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001FE RID: 510
	internal sealed class HtmlLexer
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x0004D4E8 File Offset: 0x0004B6E8
		internal HtmlLexer(string html)
		{
			this.m_htmlReader = new HtmlLexer.HtmlStringReader(html);
			this.m_elementStack = new Stack<HtmlElement>();
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004D514 File Offset: 0x0004B714
		internal HtmlElement CurrentElement
		{
			get
			{
				return this.m_currentElement;
			}
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004D51C File Offset: 0x0004B71C
		private static HtmlElement.HtmlElementType GetElementType(string elementName)
		{
			string text = elementName.ToUpperInvariant();
			if (text != null)
			{
				switch (text.Length)
				{
				case 1:
				{
					char c = text[0];
					if (c <= 'I')
					{
						if (c == 'A')
						{
							return HtmlElement.HtmlElementType.A;
						}
						if (c == 'B')
						{
							return HtmlElement.HtmlElementType.B;
						}
						if (c == 'I')
						{
							return HtmlElement.HtmlElementType.I;
						}
					}
					else
					{
						if (c == 'P')
						{
							return HtmlElement.HtmlElementType.P;
						}
						if (c == 'S')
						{
							return HtmlElement.HtmlElementType.S;
						}
						if (c == 'U')
						{
							return HtmlElement.HtmlElementType.U;
						}
					}
					break;
				}
				case 2:
				{
					char c = text[1];
					if (c <= 'D')
					{
						switch (c)
						{
						case '1':
							if (text == "H1")
							{
								return HtmlElement.HtmlElementType.H1;
							}
							break;
						case '2':
							if (text == "H2")
							{
								return HtmlElement.HtmlElementType.H2;
							}
							break;
						case '3':
							if (text == "H3")
							{
								return HtmlElement.HtmlElementType.H3;
							}
							break;
						case '4':
							if (text == "H4")
							{
								return HtmlElement.HtmlElementType.H4;
							}
							break;
						case '5':
							if (text == "H5")
							{
								return HtmlElement.HtmlElementType.H5;
							}
							break;
						case '6':
							if (text == "H6")
							{
								return HtmlElement.HtmlElementType.H6;
							}
							break;
						default:
							if (c == 'D')
							{
								if (text == "DD")
								{
									return HtmlElement.HtmlElementType.DD;
								}
							}
							break;
						}
					}
					else
					{
						switch (c)
						{
						case 'I':
							if (text == "LI")
							{
								return HtmlElement.HtmlElementType.LI;
							}
							break;
						case 'J':
						case 'K':
							break;
						case 'L':
							if (text == "OL")
							{
								return HtmlElement.HtmlElementType.OL;
							}
							if (text == "UL")
							{
								return HtmlElement.HtmlElementType.UL;
							}
							break;
						case 'M':
							if (text == "EM")
							{
								return HtmlElement.HtmlElementType.EM;
							}
							break;
						default:
							if (c != 'R')
							{
								if (c == 'T')
								{
									if (text == "DT")
									{
										return HtmlElement.HtmlElementType.DT;
									}
								}
							}
							else if (text == "BR")
							{
								return HtmlElement.HtmlElementType.BR;
							}
							break;
						}
					}
					break;
				}
				case 3:
					if (text == "DIV")
					{
						return HtmlElement.HtmlElementType.DIV;
					}
					break;
				case 4:
				{
					char c = text[0];
					if (c != 'F')
					{
						if (c == 'S')
						{
							if (text == "SPAN")
							{
								return HtmlElement.HtmlElementType.SPAN;
							}
						}
					}
					else if (text == "FONT")
					{
						return HtmlElement.HtmlElementType.FONT;
					}
					break;
				}
				case 5:
				{
					char c = text[0];
					if (c != 'S')
					{
						if (c == 'T')
						{
							if (text == "TITLE")
							{
								return HtmlElement.HtmlElementType.TITLE;
							}
						}
					}
					else if (text == "STYLE")
					{
						return HtmlElement.HtmlElementType.STYLE;
					}
					break;
				}
				case 6:
				{
					char c = text[4];
					if (c != 'K')
					{
						if (c != 'N')
						{
							if (c == 'P')
							{
								if (text == "SCRIPT")
								{
									return HtmlElement.HtmlElementType.SCRIPT;
								}
							}
						}
						else if (text == "STRONG")
						{
							return HtmlElement.HtmlElementType.STRONG;
						}
					}
					else if (text == "STRIKE")
					{
						return HtmlElement.HtmlElementType.STRIKE;
					}
					break;
				}
				case 10:
					if (text == "BLOCKQUOTE")
					{
						return HtmlElement.HtmlElementType.BLOCKQUOTE;
					}
					break;
				}
			}
			return HtmlElement.HtmlElementType.Unsupported;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004D880 File Offset: 0x0004BA80
		internal bool Read()
		{
			char c;
			if (!this.m_htmlReader.Peek(out c))
			{
				return false;
			}
			if (c == '<')
			{
				if (this.m_htmlReader.Peek(1, out c))
				{
					if (c != '!')
					{
						if (c == '/')
						{
							this.m_htmlReader.Advance();
							this.ReadEndElement();
						}
						else if (char.IsLetter(c))
						{
							this.m_htmlReader.Advance();
							this.ReadStartElement();
						}
						else
						{
							this.ReadTextElement();
							if (string.IsNullOrEmpty(this.m_currentElement.Value))
							{
								return this.Read();
							}
						}
					}
					else
					{
						this.m_htmlReader.Advance();
						this.ReadBangElement();
					}
				}
				else
				{
					this.ReadTextElement();
				}
				return true;
			}
			if (this.m_elementStack.Count > 0)
			{
				HtmlElement.HtmlElementType elementType = this.m_elementStack.Peek().ElementType;
				if (elementType == HtmlElement.HtmlElementType.STYLE || elementType == HtmlElement.HtmlElementType.SCRIPT)
				{
					this.ReadScriptOrStyleContents(elementType);
					return true;
				}
			}
			this.ReadTextElement();
			return !string.IsNullOrEmpty(this.m_currentElement.Value) || this.Read();
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004D984 File Offset: 0x0004BB84
		private void ReadStartElement()
		{
			int position = this.m_htmlReader.Position;
			HtmlElement.HtmlElementType htmlElementType = this.ReadElementType(false);
			bool flag;
			this.m_currentElement = new HtmlElement(HtmlElement.HtmlNodeType.Element, htmlElementType, this.GetAttributesAsString(out flag), flag, position);
			this.m_elementStack.Push(this.m_currentElement);
			this.AdvanceToEndOfElement();
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004D9D4 File Offset: 0x0004BBD4
		private void ReadScriptOrStyleContents(HtmlElement.HtmlElementType aElementType)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			int position = this.m_htmlReader.Position;
			char c;
			while (this.m_htmlReader.Peek(out c) && flag)
			{
				if (c == '<')
				{
					this.m_htmlReader.Mark();
					this.m_htmlReader.Advance();
					char c2;
					if (this.m_htmlReader.Peek(out c2))
					{
						if (c2 == '!' && this.m_htmlReader.Peek(1, out c2) && c2 == '-' && this.m_htmlReader.Peek(2, out c2) && c2 == '-')
						{
							flag = false;
							this.m_htmlReader.Reset();
						}
						else if (c2 == '/')
						{
							HtmlElement.HtmlElementType htmlElementType = this.ReadElementType(true);
							this.m_htmlReader.Reset();
							if (htmlElementType == aElementType)
							{
								flag = false;
							}
							else
							{
								this.m_htmlReader.Advance();
								stringBuilder.Append(c);
							}
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
				else
				{
					this.m_htmlReader.Advance();
					stringBuilder.Append(c);
				}
			}
			this.m_currentElement = new HtmlElement((aElementType == HtmlElement.HtmlElementType.SCRIPT) ? HtmlElement.HtmlNodeType.ScriptText : HtmlElement.HtmlNodeType.StyleText, HtmlElement.HtmlElementType.None, stringBuilder.ToString(), position);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004DAEC File Offset: 0x0004BCEC
		private void ReadBangElement()
		{
			bool flag = false;
			this.m_sb.Length = 0;
			this.m_htmlReader.Advance();
			char c;
			char c2;
			if (this.m_htmlReader.Peek(out c) && c == '-' && this.m_htmlReader.Peek(1, out c2) && c2 == '-')
			{
				int position = this.m_htmlReader.Position;
				this.m_htmlReader.Advance(2);
				this.m_htmlReader.Mark();
				while (this.m_htmlReader.Read(out c))
				{
					if (c == '-' && this.m_htmlReader.Peek(out c2) && c2 == '-' && this.m_htmlReader.Peek(1, out c2) && c2 == '>')
					{
						this.m_htmlReader.Advance(2);
						flag = true;
						break;
					}
					this.m_sb.Append(c);
				}
				if (!flag)
				{
					this.m_htmlReader.Reset();
					bool flag2;
					this.m_sb = this.ReadTextContent(true, out flag2);
				}
				this.m_currentElement = new HtmlElement(HtmlElement.HtmlNodeType.Comment, HtmlElement.HtmlElementType.None, this.m_sb.ToString(), position);
				return;
			}
			this.ReadStartElement();
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004DC04 File Offset: 0x0004BE04
		private void AdvanceToEndOfElement()
		{
			char c;
			while (this.m_htmlReader.Peek(out c))
			{
				if (c == '<')
				{
					return;
				}
				if (c == '>')
				{
					this.m_htmlReader.Advance();
					return;
				}
				this.m_htmlReader.Advance();
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004DC44 File Offset: 0x0004BE44
		private HtmlElement.HtmlElementType ReadElementType(bool isEndElement)
		{
			this.m_sb.Length = 0;
			bool flag = true;
			char c;
			while (flag && this.m_htmlReader.Peek(out c))
			{
				if (c != ' ')
				{
					if (c != '/')
					{
						if (c != '>')
						{
							this.m_htmlReader.Advance();
							this.m_sb.Append(c);
							continue;
						}
					}
					else if (isEndElement)
					{
						this.m_htmlReader.Advance();
						continue;
					}
				}
				else if (isEndElement && this.m_sb.Length == 0)
				{
					this.m_htmlReader.Advance();
					continue;
				}
				if (this.m_sb.Length == 0)
				{
					return HtmlElement.HtmlElementType.Unsupported;
				}
				return HtmlLexer.GetElementType(this.m_sb.ToString());
			}
			return HtmlElement.HtmlElementType.Unsupported;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004DCF0 File Offset: 0x0004BEF0
		private string GetAttributesAsString(out bool isEmpty)
		{
			this.m_sb.Length = 0;
			isEmpty = false;
			HtmlLexer.AttributeEscapeState attributeEscapeState = HtmlLexer.AttributeEscapeState.None;
			char c;
			while (this.m_htmlReader.Peek(out c))
			{
				switch (attributeEscapeState)
				{
				case HtmlLexer.AttributeEscapeState.None:
					if (c != '/')
					{
						switch (c)
						{
						case '<':
						case '>':
							return this.m_sb.ToString();
						case '=':
							this.ConsumeAndAppend(c);
							attributeEscapeState = HtmlLexer.AttributeEscapeState.RawEquals;
							break;
						default:
							this.ConsumeAndAppend(c);
							break;
						}
					}
					else
					{
						char c2;
						if (this.m_htmlReader.Peek(1, out c2) && c2 == '>')
						{
							isEmpty = true;
							return this.m_sb.ToString();
						}
						this.ConsumeAndAppend(c);
					}
					break;
				case HtmlLexer.AttributeEscapeState.SingleQuote:
					this.ConsumeAndAppend(c);
					if (c == '\'')
					{
						attributeEscapeState = HtmlLexer.AttributeEscapeState.None;
					}
					break;
				case HtmlLexer.AttributeEscapeState.DoubleQuote:
					this.ConsumeAndAppend(c);
					if (c == '"')
					{
						attributeEscapeState = HtmlLexer.AttributeEscapeState.None;
					}
					break;
				case HtmlLexer.AttributeEscapeState.NoQuote:
					if (c != ' ')
					{
						if (c == '>')
						{
							return this.m_sb.ToString();
						}
						this.ConsumeAndAppend(c);
					}
					else
					{
						this.ConsumeAndAppend(c);
						attributeEscapeState = HtmlLexer.AttributeEscapeState.None;
					}
					break;
				case HtmlLexer.AttributeEscapeState.RawEquals:
					if (c <= '"')
					{
						if (c == ' ')
						{
							this.ConsumeAndAppend(c);
							break;
						}
						if (c == '"')
						{
							this.ConsumeAndAppend(c);
							attributeEscapeState = HtmlLexer.AttributeEscapeState.DoubleQuote;
							break;
						}
					}
					else
					{
						if (c == '\'')
						{
							attributeEscapeState = HtmlLexer.AttributeEscapeState.SingleQuote;
							this.ConsumeAndAppend(c);
							break;
						}
						if (c == '>')
						{
							return this.m_sb.ToString();
						}
					}
					attributeEscapeState = HtmlLexer.AttributeEscapeState.NoQuote;
					this.ConsumeAndAppend(c);
					break;
				}
			}
			return this.m_sb.ToString();
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004DE5C File Offset: 0x0004C05C
		private void ConsumeAndAppend(char c)
		{
			this.m_htmlReader.Advance();
			this.m_sb.Append(c);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004DE78 File Offset: 0x0004C078
		private StringBuilder ReadTextContent(bool inComment, out bool hasEntity)
		{
			hasEntity = false;
			this.m_sb.Length = 0;
			bool flag = true;
			char c;
			while (flag && this.m_htmlReader.Peek(out c))
			{
				if (c <= '&')
				{
					if (c != ' ')
					{
						if (c != '&')
						{
							goto IL_0085;
						}
						hasEntity = true;
						goto IL_0085;
					}
					else if (!this.m_readWhiteSpace)
					{
						this.m_sb.Append(c);
						this.m_readWhiteSpace = true;
					}
				}
				else if (c != '<')
				{
					if (c != '>' || !inComment)
					{
						goto IL_0085;
					}
					flag = false;
				}
				else
				{
					char c2;
					this.m_htmlReader.Peek(1, out c2);
					if (c2 == '!' || c2 == '/')
					{
						flag = false;
					}
					else if (char.IsLetter(c2))
					{
						flag = false;
					}
					if (flag)
					{
						goto IL_0085;
					}
				}
				IL_00A1:
				if (flag)
				{
					this.m_htmlReader.Advance();
					continue;
				}
				continue;
				IL_0085:
				this.m_sb.Append(c);
				if (this.m_readWhiteSpace)
				{
					this.m_readWhiteSpace = false;
					goto IL_00A1;
				}
				goto IL_00A1;
			}
			return this.m_sb;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004DF50 File Offset: 0x0004C150
		private void ReadTextElement()
		{
			int position = this.m_htmlReader.Position;
			bool flag;
			this.m_sb = this.ReadTextContent(false, out flag);
			if (flag)
			{
				HtmlEntityResolver.ResolveEntities(this.m_sb);
			}
			this.m_currentElement = new HtmlElement(HtmlElement.HtmlNodeType.Text, HtmlElement.HtmlElementType.None, this.m_sb.ToString(), position);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		private void ReadEndElement()
		{
			int position = this.m_htmlReader.Position;
			HtmlElement.HtmlElementType htmlElementType = this.ReadElementType(true);
			this.m_currentElement = new HtmlElement(HtmlElement.HtmlNodeType.EndElement, htmlElementType, position);
			this.AdvanceToEndOfElement();
			if (this.m_elementStack.Count > 0)
			{
				HtmlElement.HtmlElementType elementType = this.m_elementStack.Pop().ElementType;
			}
		}

		// Token: 0x0400092C RID: 2348
		private StringBuilder m_sb = new StringBuilder(16);

		// Token: 0x0400092D RID: 2349
		private HtmlElement m_currentElement;

		// Token: 0x0400092E RID: 2350
		private bool m_readWhiteSpace;

		// Token: 0x0400092F RID: 2351
		private HtmlLexer.HtmlStringReader m_htmlReader;

		// Token: 0x04000930 RID: 2352
		private Stack<HtmlElement> m_elementStack;

		// Token: 0x02000935 RID: 2357
		internal sealed class Constants
		{
			// Token: 0x02000D31 RID: 3377
			internal class AttributeNames
			{
				// Token: 0x04005074 RID: 20596
				internal const string Align = "align";

				// Token: 0x04005075 RID: 20597
				internal const string Padding = "padding";

				// Token: 0x04005076 RID: 20598
				internal const string PaddingTop = "padding-top";

				// Token: 0x04005077 RID: 20599
				internal const string PaddingBottom = "padding-bottom";

				// Token: 0x04005078 RID: 20600
				internal const string PaddingLeft = "padding-left";

				// Token: 0x04005079 RID: 20601
				internal const string PaddingRight = "padding-right";

				// Token: 0x0400507A RID: 20602
				internal const string Href = "href";

				// Token: 0x0400507B RID: 20603
				internal const string Size = "size";

				// Token: 0x0400507C RID: 20604
				internal const string Face = "face";

				// Token: 0x0400507D RID: 20605
				internal const string Color = "color";

				// Token: 0x0400507E RID: 20606
				internal const string Style = "style";

				// Token: 0x0400507F RID: 20607
				internal const string FontFamily = "font-family";

				// Token: 0x04005080 RID: 20608
				internal const string FontSize = "font-size";

				// Token: 0x04005081 RID: 20609
				internal const string FontWeight = "font-weight";

				// Token: 0x04005082 RID: 20610
				internal const string TextAlign = "text-align";

				// Token: 0x04005083 RID: 20611
				internal const string TextIndent = "text-indent";
			}

			// Token: 0x02000D32 RID: 3378
			internal class ElementNames
			{
				// Token: 0x04005084 RID: 20612
				internal const string SCRIPT = "SCRIPT";

				// Token: 0x04005085 RID: 20613
				internal const string STYLE = "STYLE";

				// Token: 0x04005086 RID: 20614
				internal const string P = "P";

				// Token: 0x04005087 RID: 20615
				internal const string DIV = "DIV";

				// Token: 0x04005088 RID: 20616
				internal const string BR = "BR";

				// Token: 0x04005089 RID: 20617
				internal const string UL = "UL";

				// Token: 0x0400508A RID: 20618
				internal const string OL = "OL";

				// Token: 0x0400508B RID: 20619
				internal const string LI = "LI";

				// Token: 0x0400508C RID: 20620
				internal const string SPAN = "SPAN";

				// Token: 0x0400508D RID: 20621
				internal const string FONT = "FONT";

				// Token: 0x0400508E RID: 20622
				internal const string A = "A";

				// Token: 0x0400508F RID: 20623
				internal const string STRONG = "STRONG";

				// Token: 0x04005090 RID: 20624
				internal const string STRIKE = "STRIKE";

				// Token: 0x04005091 RID: 20625
				internal const string B = "B";

				// Token: 0x04005092 RID: 20626
				internal const string I = "I";

				// Token: 0x04005093 RID: 20627
				internal const string U = "U";

				// Token: 0x04005094 RID: 20628
				internal const string S = "S";

				// Token: 0x04005095 RID: 20629
				internal const string EM = "EM";

				// Token: 0x04005096 RID: 20630
				internal const string H1 = "H1";

				// Token: 0x04005097 RID: 20631
				internal const string H2 = "H2";

				// Token: 0x04005098 RID: 20632
				internal const string H3 = "H3";

				// Token: 0x04005099 RID: 20633
				internal const string H4 = "H4";

				// Token: 0x0400509A RID: 20634
				internal const string H5 = "H5";

				// Token: 0x0400509B RID: 20635
				internal const string H6 = "H6";

				// Token: 0x0400509C RID: 20636
				internal const string DD = "DD";

				// Token: 0x0400509D RID: 20637
				internal const string DT = "DT";

				// Token: 0x0400509E RID: 20638
				internal const string BLOCKQUOTE = "BLOCKQUOTE";

				// Token: 0x0400509F RID: 20639
				internal const string TITLE = "TITLE";
			}
		}

		// Token: 0x02000936 RID: 2358
		private enum AttributeEscapeState
		{
			// Token: 0x04003FB5 RID: 16309
			None,
			// Token: 0x04003FB6 RID: 16310
			SingleQuote,
			// Token: 0x04003FB7 RID: 16311
			DoubleQuote,
			// Token: 0x04003FB8 RID: 16312
			NoQuote,
			// Token: 0x04003FB9 RID: 16313
			RawEquals
		}

		// Token: 0x02000937 RID: 2359
		private sealed class HtmlStringReader
		{
			// Token: 0x06007F80 RID: 32640 RVA: 0x0020DB93 File Offset: 0x0020BD93
			internal HtmlStringReader(string html)
			{
				this.m_html = html;
			}

			// Token: 0x17002966 RID: 10598
			// (get) Token: 0x06007F81 RID: 32641 RVA: 0x0020DBA2 File Offset: 0x0020BDA2
			internal int Position
			{
				get
				{
					return this.m_currentIndex;
				}
			}

			// Token: 0x06007F82 RID: 32642 RVA: 0x0020DBAA File Offset: 0x0020BDAA
			internal bool Read(out char c)
			{
				if (this.Peek(out c))
				{
					this.m_currentIndex++;
					return true;
				}
				return false;
			}

			// Token: 0x06007F83 RID: 32643 RVA: 0x0020DBC8 File Offset: 0x0020BDC8
			internal bool Peek(out char c)
			{
				if (this.m_currentIndex < this.m_html.Length)
				{
					c = this.m_html[this.m_currentIndex];
					switch (c)
					{
					case '\t':
					case '\n':
					case '\v':
					case '\f':
						c = ' ';
						break;
					case '\r':
						this.m_currentIndex++;
						return this.Peek(out c);
					}
					return true;
				}
				c = '\0';
				return false;
			}

			// Token: 0x06007F84 RID: 32644 RVA: 0x0020DC40 File Offset: 0x0020BE40
			internal bool Peek(int lookAhead, out char c)
			{
				int currentIndex = this.m_currentIndex;
				this.m_currentIndex += lookAhead;
				bool flag = this.Peek(out c);
				this.m_currentIndex = currentIndex;
				return flag;
			}

			// Token: 0x06007F85 RID: 32645 RVA: 0x0020DC70 File Offset: 0x0020BE70
			internal void Advance()
			{
				this.m_currentIndex++;
			}

			// Token: 0x06007F86 RID: 32646 RVA: 0x0020DC80 File Offset: 0x0020BE80
			internal void Advance(int amount)
			{
				this.m_currentIndex += amount;
			}

			// Token: 0x06007F87 RID: 32647 RVA: 0x0020DC90 File Offset: 0x0020BE90
			internal void Mark()
			{
				this.m_markedIndex = this.m_currentIndex;
			}

			// Token: 0x06007F88 RID: 32648 RVA: 0x0020DC9E File Offset: 0x0020BE9E
			internal void Reset()
			{
				this.m_currentIndex = this.m_markedIndex;
			}

			// Token: 0x04003FBA RID: 16314
			private int m_markedIndex;

			// Token: 0x04003FBB RID: 16315
			private int m_currentIndex;

			// Token: 0x04003FBC RID: 16316
			private string m_html;
		}
	}
}
