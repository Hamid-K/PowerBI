using System;
using System.Collections.Generic;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000071 RID: 113
	internal sealed class HtmlTokenizer : BaseTokenizer
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002E7 RID: 743 RVA: 0x00014188 File Offset: 0x00012388
		// (remove) Token: 0x060002E8 RID: 744 RVA: 0x000141C0 File Offset: 0x000123C0
		public event EventHandler<HtmlErrorEvent> Error;

		// Token: 0x060002E9 RID: 745 RVA: 0x000141F5 File Offset: 0x000123F5
		public HtmlTokenizer(TextSource source, IEntityProvider resolver)
			: base(source)
		{
			this.State = HtmlParseMode.PCData;
			this.IsAcceptingCharacterData = false;
			this.IsStrictMode = false;
			this._lastStartTag = string.Empty;
			this._resolver = resolver;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00014225 File Offset: 0x00012425
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0001422D File Offset: 0x0001242D
		public bool IsAcceptingCharacterData { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00014236 File Offset: 0x00012436
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0001423E File Offset: 0x0001243E
		public HtmlParseMode State { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00014247 File Offset: 0x00012447
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0001424F File Offset: 0x0001244F
		public bool IsStrictMode { get; set; }

		// Token: 0x060002F0 RID: 752 RVA: 0x00014258 File Offset: 0x00012458
		public HtmlToken Get()
		{
			char next = base.GetNext();
			this._position = base.GetCurrentPosition();
			if (next != '\uffff')
			{
				switch (this.State)
				{
				case HtmlParseMode.PCData:
					return this.Data(next);
				case HtmlParseMode.RCData:
					return this.RCData(next);
				case HtmlParseMode.Plaintext:
					return this.Plaintext(next);
				case HtmlParseMode.Rawtext:
					return this.Rawtext(next);
				case HtmlParseMode.Script:
					return this.ScriptData(next);
				}
			}
			return this.NewEof(true);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000142D4 File Offset: 0x000124D4
		internal void RaiseErrorOccurred(HtmlParseError code, TextPosition position)
		{
			EventHandler<HtmlErrorEvent> error = this.Error;
			if (this.IsStrictMode)
			{
				string text = "Error while parsing the provided HTML document.";
				throw new HtmlParseException(code.GetCode(), text, position);
			}
			if (error != null)
			{
				HtmlErrorEvent htmlErrorEvent = new HtmlErrorEvent(code, position);
				error(this, htmlErrorEvent);
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00014317 File Offset: 0x00012517
		private HtmlToken Data(char c)
		{
			if (c != '<')
			{
				return this.DataText(c);
			}
			return this.TagOpen(base.GetNext());
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00014334 File Offset: 0x00012534
		private HtmlToken DataText(char c)
		{
			for (;;)
			{
				if (c <= '&')
				{
					if (c != '\0')
					{
						if (c != '&')
						{
							goto IL_0041;
						}
						this.AppendCharacterReference(base.GetNext(), '\0');
					}
					else
					{
						this.RaiseErrorOccurred(HtmlParseError.Null);
					}
				}
				else
				{
					if (c == '<' || c == '\uffff')
					{
						break;
					}
					goto IL_0041;
				}
				IL_004E:
				c = base.GetNext();
				continue;
				IL_0041:
				base.StringBuffer.Append(c);
				goto IL_004E;
			}
			base.Back();
			return this.NewCharacter();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00014398 File Offset: 0x00012598
		private HtmlToken Plaintext(char c)
		{
			for (;;)
			{
				if (c != '\0')
				{
					if (c == '\uffff')
					{
						break;
					}
					base.StringBuffer.Append(c);
				}
				else
				{
					this.AppendReplacement();
				}
				c = base.GetNext();
			}
			base.Back();
			return this.NewCharacter();
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000143D2 File Offset: 0x000125D2
		private HtmlToken RCData(char c)
		{
			if (c != '<')
			{
				return this.RCDataText(c);
			}
			return this.RCDataLt(base.GetNext());
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000143F0 File Offset: 0x000125F0
		private HtmlToken RCDataText(char c)
		{
			for (;;)
			{
				if (c <= '&')
				{
					if (c != '\0')
					{
						if (c != '&')
						{
							goto IL_0042;
						}
						this.AppendCharacterReference(base.GetNext(), '\0');
					}
					else
					{
						this.AppendReplacement();
					}
				}
				else
				{
					if (c != '<' && c != '\uffff')
					{
						goto IL_0042;
					}
					break;
				}
				IL_004F:
				c = base.GetNext();
				continue;
				IL_0042:
				base.StringBuffer.Append(c);
				goto IL_004F;
			}
			base.Back();
			return this.NewCharacter();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00014458 File Offset: 0x00012658
		private HtmlToken RCDataLt(char c)
		{
			if (c != '/')
			{
				base.StringBuffer.Append('<');
				return this.RCDataText(c);
			}
			c = base.GetNext();
			if (c.IsUppercaseAscii())
			{
				base.StringBuffer.Append(char.ToLowerInvariant(c));
				return this.RCDataNameEndTag(base.GetNext());
			}
			if (c.IsLowercaseAscii())
			{
				base.StringBuffer.Append(c);
				return this.RCDataNameEndTag(base.GetNext());
			}
			base.StringBuffer.Append('<').Append('/');
			return this.RCDataText(c);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000144F0 File Offset: 0x000126F0
		private HtmlToken RCDataNameEndTag(char c)
		{
			HtmlToken htmlToken;
			for (;;)
			{
				htmlToken = this.CreateIfAppropriate(c);
				if (htmlToken != null)
				{
					break;
				}
				if (c.IsUppercaseAscii())
				{
					base.StringBuffer.Append(char.ToLowerInvariant(c));
				}
				else
				{
					if (!c.IsLowercaseAscii())
					{
						goto IL_0040;
					}
					base.StringBuffer.Append(c);
				}
				c = base.GetNext();
			}
			return htmlToken;
			IL_0040:
			base.StringBuffer.Insert(0, '<').Insert(1, '/');
			return this.RCDataText(c);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00014565 File Offset: 0x00012765
		private HtmlToken Rawtext(char c)
		{
			if (c != '<')
			{
				return this.RawtextText(c);
			}
			return this.RawtextLT(base.GetNext());
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00014580 File Offset: 0x00012780
		private HtmlToken RawtextText(char c)
		{
			for (;;)
			{
				if (c != '\0')
				{
					if (c == '<' || c == '\uffff')
					{
						break;
					}
					base.StringBuffer.Append(c);
				}
				else
				{
					this.AppendReplacement();
				}
				c = base.GetNext();
			}
			base.Back();
			return this.NewCharacter();
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000145C0 File Offset: 0x000127C0
		private HtmlToken RawtextLT(char c)
		{
			if (c != '/')
			{
				base.StringBuffer.Append('<');
				return this.RawtextText(c);
			}
			c = base.GetNext();
			if (c.IsUppercaseAscii())
			{
				base.StringBuffer.Append(char.ToLowerInvariant(c));
				return this.RawtextNameEndTag(base.GetNext());
			}
			if (c.IsLowercaseAscii())
			{
				base.StringBuffer.Append(c);
				return this.RawtextNameEndTag(base.GetNext());
			}
			base.StringBuffer.Append('<').Append('/');
			return this.RawtextText(c);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00014658 File Offset: 0x00012858
		private HtmlToken RawtextNameEndTag(char c)
		{
			HtmlToken htmlToken;
			for (;;)
			{
				htmlToken = this.CreateIfAppropriate(c);
				if (htmlToken != null)
				{
					break;
				}
				if (c.IsUppercaseAscii())
				{
					base.StringBuffer.Append(char.ToLowerInvariant(c));
				}
				else
				{
					if (!c.IsLowercaseAscii())
					{
						goto IL_0040;
					}
					base.StringBuffer.Append(c);
				}
				c = base.GetNext();
			}
			return htmlToken;
			IL_0040:
			base.StringBuffer.Insert(0, '<').Insert(1, '/');
			return this.RawtextText(c);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000146D0 File Offset: 0x000128D0
		private HtmlToken CharacterData(char c)
		{
			while (c != '\uffff')
			{
				if (c == ']' && base.ContinuesWithSensitive("]]>"))
				{
					base.Advance(2);
					IL_0042:
					return this.NewCharacter();
				}
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			base.Back();
			goto IL_0042;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00014728 File Offset: 0x00012928
		private void AppendCharacterReference(char c, char allowedCharacter = '\0')
		{
			if (c.IsSpaceCharacter() || c == '<' || c == '\uffff' || c == '&' || c == allowedCharacter)
			{
				base.Back();
				base.StringBuffer.Append('&');
				return;
			}
			string text;
			if (c == '#')
			{
				text = this.GetNumericCharacterReference(base.GetNext());
			}
			else
			{
				text = this.GetLookupCharacterReference(c, allowedCharacter);
			}
			if (text == null)
			{
				base.StringBuffer.Append('&');
				return;
			}
			base.StringBuffer.Append(text);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000147A8 File Offset: 0x000129A8
		private string GetNumericCharacterReference(char c)
		{
			int num = 10;
			int num2 = 1;
			int num3 = 0;
			List<int> list = new List<int>();
			bool flag = c == 'x' || c == 'X';
			if (flag)
			{
				num = 16;
				while ((c = base.GetNext()).IsHex())
				{
					list.Add(c.FromHex());
				}
			}
			else
			{
				while (c.IsDigit())
				{
					list.Add(c.FromHex());
					c = base.GetNext();
				}
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				num3 += list[i] * num2;
				num2 *= num;
			}
			if (list.Count == 0)
			{
				base.Back(2);
				if (flag)
				{
					base.Back();
				}
				this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceWrongNumber);
				return null;
			}
			if (c != ';')
			{
				this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceSemicolonMissing);
				base.Back();
			}
			if (HtmlEntityService.IsInCharacterTable(num3))
			{
				this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidCode);
				return HtmlEntityService.GetSymbolFromTable(num3);
			}
			if (HtmlEntityService.IsInvalidNumber(num3))
			{
				this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidNumber);
				return '\ufffd'.ToString();
			}
			if (HtmlEntityService.IsInInvalidRange(num3))
			{
				this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceInvalidRange);
			}
			return num3.ConvertFromUtf32();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000148C0 File Offset: 0x00012AC0
		private string GetLookupCharacterReference(char c, char allowedCharacter)
		{
			string text = null;
			int num = base.InsertionPoint - 1;
			char[] array = new char[32];
			int num2 = 0;
			char c2 = base.Current;
			while (c2 != ';' && c2.IsName())
			{
				array[num2++] = c2;
				c2 = base.GetNext();
				if (c2 == '\uffff' || num2 >= 31)
				{
					break;
				}
			}
			if (c2 == ';')
			{
				array[num2] = ';';
				string text2 = new string(array, 0, num2 + 1);
				text = this._resolver.GetSymbol(text2);
			}
			while (text == null && num2 > 0)
			{
				string text3 = new string(array, 0, num2--);
				text = this._resolver.GetSymbol(text3);
				if (text == null)
				{
					base.Back();
				}
			}
			c2 = base.Current;
			if (c2 != ';')
			{
				if (allowedCharacter != '\0' && (c2 == '=' || c2.IsAlphanumericAscii()))
				{
					if (c2 == '=')
					{
						this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceAttributeEqualsFound);
					}
					base.InsertionPoint = num;
					return null;
				}
				base.Back();
				this.RaiseErrorOccurred(HtmlParseError.CharacterReferenceNotTerminated);
			}
			return text;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000149B4 File Offset: 0x00012BB4
		private HtmlToken TagOpen(char c)
		{
			if (c == '/')
			{
				return this.TagEnd(base.GetNext());
			}
			if (c.IsLowercaseAscii())
			{
				base.StringBuffer.Append(c);
				return this.TagName(this.NewTagOpen());
			}
			if (c.IsUppercaseAscii())
			{
				base.StringBuffer.Append(char.ToLowerInvariant(c));
				return this.TagName(this.NewTagOpen());
			}
			if (c == '!')
			{
				return this.MarkupDeclaration(base.GetNext());
			}
			if (c != '?')
			{
				this.State = HtmlParseMode.PCData;
				this.RaiseErrorOccurred(HtmlParseError.AmbiguousOpenTag);
				base.StringBuffer.Append('<');
				return this.DataText(c);
			}
			this.RaiseErrorOccurred(HtmlParseError.BogusComment);
			return this.BogusComment(c);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00014A68 File Offset: 0x00012C68
		private HtmlToken TagEnd(char c)
		{
			if (c.IsLowercaseAscii())
			{
				base.StringBuffer.Append(c);
				return this.TagName(this.NewTagClose());
			}
			if (c.IsUppercaseAscii())
			{
				base.StringBuffer.Append(char.ToLowerInvariant(c));
				return this.TagName(this.NewTagClose());
			}
			if (c == '>')
			{
				this.State = HtmlParseMode.PCData;
				this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				return this.Data(base.GetNext());
			}
			if (c == '\uffff')
			{
				base.Back();
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				base.StringBuffer.Append('<').Append('/');
				return this.NewCharacter();
			}
			this.RaiseErrorOccurred(HtmlParseError.BogusComment);
			return this.BogusComment(c);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00014B20 File Offset: 0x00012D20
		private HtmlToken TagName(HtmlTagToken tag)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next == '>')
				{
					break;
				}
				if (next.IsSpaceCharacter())
				{
					goto Block_1;
				}
				if (next == '/')
				{
					goto Block_2;
				}
				if (next.IsUppercaseAscii())
				{
					base.StringBuffer.Append(char.ToLowerInvariant(next));
				}
				else if (next == '\0')
				{
					this.AppendReplacement();
				}
				else
				{
					if (next == '\uffff')
					{
						goto IL_0096;
					}
					base.StringBuffer.Append(next);
				}
			}
			tag.Name = base.FlushBuffer();
			return this.EmitTag(tag);
			Block_1:
			tag.Name = base.FlushBuffer();
			return this.ParseAttributes(tag);
			Block_2:
			tag.Name = base.FlushBuffer();
			return this.TagSelfClosing(tag);
			IL_0096:
			return this.NewEof(false);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00014BCA File Offset: 0x00012DCA
		private HtmlToken TagSelfClosing(HtmlTagToken tag)
		{
			return this.TagSelfClosingInner(tag) ?? this.ParseAttributes(tag);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00014BE0 File Offset: 0x00012DE0
		private HtmlToken TagSelfClosingInner(HtmlTagToken tag)
		{
			char next;
			for (;;)
			{
				next = base.GetNext();
				if (next != '/')
				{
					break;
				}
				this.RaiseErrorOccurred(HtmlParseError.ClosingSlashMisplaced);
			}
			if (next == '>')
			{
				tag.IsSelfClosing = true;
				return this.EmitTag(tag);
			}
			if (next != '\uffff')
			{
				this.RaiseErrorOccurred(HtmlParseError.ClosingSlashMisplaced);
				base.Back();
				return null;
			}
			return this.NewEof(false);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00014C38 File Offset: 0x00012E38
		private HtmlToken MarkupDeclaration(char c)
		{
			if (base.ContinuesWithSensitive("--"))
			{
				base.Advance();
				return this.CommentStart(base.GetNext());
			}
			if (base.ContinuesWithInsensitive(TagNames.Doctype))
			{
				base.Advance(6);
				return this.Doctype(base.GetNext());
			}
			if (this.IsAcceptingCharacterData && base.ContinuesWithSensitive(Keywords.CData))
			{
				base.Advance(6);
				return this.CharacterData(base.GetNext());
			}
			this.RaiseErrorOccurred(HtmlParseError.UndefinedMarkupDeclaration);
			return this.BogusComment(c);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00014CC0 File Offset: 0x00012EC0
		private HtmlToken BogusComment(char c)
		{
			base.StringBuffer.Clear();
			for (;;)
			{
				if (c != '\0')
				{
					if (c == '>')
					{
						goto IL_0042;
					}
					if (c == '\uffff')
					{
						break;
					}
				}
				else
				{
					c = '\ufffd';
				}
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			base.Back();
			IL_0042:
			this.State = HtmlParseMode.PCData;
			return this.NewComment();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00014D1C File Offset: 0x00012F1C
		private HtmlToken CommentStart(char c)
		{
			base.StringBuffer.Clear();
			if (c > '-')
			{
				if (c != '>')
				{
					if (c != '\uffff')
					{
						goto IL_007A;
					}
					this.RaiseErrorOccurred(HtmlParseError.EOF);
					base.Back();
				}
				else
				{
					this.State = HtmlParseMode.PCData;
					this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				}
				return this.NewComment();
			}
			if (c == '\0')
			{
				this.AppendReplacement();
				return this.Comment(base.GetNext());
			}
			if (c == '-')
			{
				return this.CommentDashStart(base.GetNext()) ?? this.Comment(base.GetNext());
			}
			IL_007A:
			base.StringBuffer.Append(c);
			return this.Comment(base.GetNext());
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00014DC4 File Offset: 0x00012FC4
		private HtmlToken CommentDashStart(char c)
		{
			if (c > '-')
			{
				if (c != '>')
				{
					if (c != '\uffff')
					{
						goto IL_0077;
					}
					this.RaiseErrorOccurred(HtmlParseError.EOF);
					base.Back();
				}
				else
				{
					this.State = HtmlParseMode.PCData;
					this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				}
				return this.NewComment();
			}
			if (c == '\0')
			{
				this.RaiseErrorOccurred(HtmlParseError.Null);
				base.StringBuffer.Append('-').Append('\ufffd');
				return this.Comment(base.GetNext());
			}
			if (c == '-')
			{
				return this.CommentEnd(base.GetNext());
			}
			IL_0077:
			base.StringBuffer.Append('-').Append(c);
			return this.Comment(base.GetNext());
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00014E70 File Offset: 0x00013070
		private HtmlToken Comment(char c)
		{
			HtmlToken htmlToken;
			for (;;)
			{
				if (c != '\0')
				{
					if (c != '-')
					{
						if (c == '\uffff')
						{
							goto IL_0024;
						}
						base.StringBuffer.Append(c);
					}
					else
					{
						htmlToken = this.CommentDashEnd(base.GetNext());
						if (htmlToken != null)
						{
							break;
						}
					}
				}
				else
				{
					this.AppendReplacement();
				}
				c = base.GetNext();
			}
			return htmlToken;
			IL_0024:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			base.Back();
			return this.NewComment();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00014ED4 File Offset: 0x000130D4
		private HtmlToken CommentDashEnd(char c)
		{
			if (c != '\0')
			{
				if (c == '-')
				{
					return this.CommentEnd(base.GetNext());
				}
				if (c == '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.EOF);
					base.Back();
					return this.NewComment();
				}
			}
			else
			{
				this.RaiseErrorOccurred(HtmlParseError.Null);
				c = '\ufffd';
			}
			base.StringBuffer.Append('-').Append(c);
			return null;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00014F38 File Offset: 0x00013138
		private HtmlToken CommentEnd(char c)
		{
			while (c > '!')
			{
				if (c != '-')
				{
					if (c == '>')
					{
						this.State = HtmlParseMode.PCData;
						return this.NewComment();
					}
					if (c != '\uffff')
					{
						IL_0096:
						this.RaiseErrorOccurred(HtmlParseError.CommentEndedUnexpected);
						base.StringBuffer.Append('-').Append('-').Append(c);
						return null;
					}
					this.RaiseErrorOccurred(HtmlParseError.EOF);
					base.Back();
					return this.NewComment();
				}
				else
				{
					this.RaiseErrorOccurred(HtmlParseError.CommentEndedWithDash);
					base.StringBuffer.Append('-');
					c = base.GetNext();
				}
			}
			if (c == '\0')
			{
				this.RaiseErrorOccurred(HtmlParseError.Null);
				base.StringBuffer.Append('-').Append('\ufffd');
				return null;
			}
			if (c != '!')
			{
				goto IL_0096;
			}
			this.RaiseErrorOccurred(HtmlParseError.CommentEndedWithEM);
			return this.CommentBangEnd(base.GetNext());
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001500C File Offset: 0x0001320C
		private HtmlToken CommentBangEnd(char c)
		{
			if (c > '-')
			{
				if (c != '>')
				{
					if (c != '\uffff')
					{
						goto IL_008E;
					}
					this.RaiseErrorOccurred(HtmlParseError.EOF);
					base.Back();
				}
				else
				{
					this.State = HtmlParseMode.PCData;
				}
				return this.NewComment();
			}
			if (c == '\0')
			{
				this.RaiseErrorOccurred(HtmlParseError.Null);
				base.StringBuffer.Append('-').Append('-').Append('!')
					.Append('\ufffd');
				return null;
			}
			if (c == '-')
			{
				base.StringBuffer.Append('-').Append('-').Append('!');
				return this.CommentDashEnd(base.GetNext());
			}
			IL_008E:
			base.StringBuffer.Append('-').Append('-').Append('!')
				.Append(c);
			return null;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000150D4 File Offset: 0x000132D4
		private HtmlToken Doctype(char c)
		{
			if (c.IsSpaceCharacter())
			{
				return this.DoctypeNameBefore(base.GetNext());
			}
			if (c == '\uffff')
			{
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				base.Back();
				return this.NewDoctype(true);
			}
			this.RaiseErrorOccurred(HtmlParseError.DoctypeUnexpected);
			return this.DoctypeNameBefore(c);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00015124 File Offset: 0x00013324
		private HtmlToken DoctypeNameBefore(char c)
		{
			while (c.IsSpaceCharacter())
			{
				c = base.GetNext();
			}
			if (c.IsUppercaseAscii())
			{
				HtmlDoctypeToken htmlDoctypeToken = this.NewDoctype(false);
				base.StringBuffer.Append(char.ToLowerInvariant(c));
				return this.DoctypeName(htmlDoctypeToken);
			}
			if (c == '\0')
			{
				HtmlDoctypeToken htmlDoctypeToken2 = this.NewDoctype(false);
				this.AppendReplacement();
				return this.DoctypeName(htmlDoctypeToken2);
			}
			if (c == '>')
			{
				HtmlToken htmlToken = this.NewDoctype(true);
				this.State = HtmlParseMode.PCData;
				this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				return htmlToken;
			}
			if (c == '\uffff')
			{
				HtmlToken htmlToken2 = this.NewDoctype(true);
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				base.Back();
				return htmlToken2;
			}
			HtmlDoctypeToken htmlDoctypeToken3 = this.NewDoctype(false);
			base.StringBuffer.Append(c);
			return this.DoctypeName(htmlDoctypeToken3);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000151DC File Offset: 0x000133DC
		private HtmlToken DoctypeName(HtmlDoctypeToken doctype)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next.IsSpaceCharacter())
				{
					break;
				}
				if (next == '>')
				{
					goto Block_1;
				}
				if (next.IsUppercaseAscii())
				{
					base.StringBuffer.Append(char.ToLowerInvariant(next));
				}
				else if (next == '\0')
				{
					this.AppendReplacement();
				}
				else
				{
					if (next == '\uffff')
					{
						goto Block_4;
					}
					base.StringBuffer.Append(next);
				}
			}
			doctype.Name = base.FlushBuffer();
			return this.DoctypeNameAfter(doctype);
			Block_1:
			this.State = HtmlParseMode.PCData;
			doctype.Name = base.FlushBuffer();
			return doctype;
			Block_4:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			base.Back();
			doctype.IsQuirksForced = true;
			doctype.Name = base.FlushBuffer();
			return doctype;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001528C File Offset: 0x0001348C
		private HtmlToken DoctypeNameAfter(HtmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '>')
			{
				this.State = HtmlParseMode.PCData;
			}
			else if (c == '\uffff')
			{
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				base.Back();
				doctype.IsQuirksForced = true;
			}
			else
			{
				if (base.ContinuesWithInsensitive(Keywords.Public))
				{
					base.Advance(5);
					return this.DoctypePublic(doctype);
				}
				if (base.ContinuesWithInsensitive(Keywords.System))
				{
					base.Advance(5);
					return this.DoctypeSystem(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.DoctypeUnexpectedAfterName);
				doctype.IsQuirksForced = true;
				return this.BogusDoctype(doctype);
			}
			return doctype;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001531C File Offset: 0x0001351C
		private HtmlToken DoctypePublic(HtmlDoctypeToken doctype)
		{
			char next = base.GetNext();
			if (next.IsSpaceCharacter())
			{
				return this.DoctypePublicIdentifierBefore(doctype);
			}
			if (next == '"')
			{
				this.RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
				doctype.PublicIdentifier = string.Empty;
				return this.DoctypePublicIdentifierDoubleQuoted(doctype);
			}
			if (next == '\'')
			{
				this.RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
				doctype.PublicIdentifier = string.Empty;
				return this.DoctypePublicIdentifierSingleQuoted(doctype);
			}
			if (next == '>')
			{
				this.State = HtmlParseMode.PCData;
				this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				doctype.IsQuirksForced = true;
			}
			else
			{
				if (next != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypePublicInvalid);
					doctype.IsQuirksForced = true;
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				base.Back();
			}
			return doctype;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000153D4 File Offset: 0x000135D4
		private HtmlToken DoctypePublicIdentifierBefore(HtmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '"')
			{
				doctype.PublicIdentifier = string.Empty;
				return this.DoctypePublicIdentifierDoubleQuoted(doctype);
			}
			if (c == '\'')
			{
				doctype.PublicIdentifier = string.Empty;
				return this.DoctypePublicIdentifierSingleQuoted(doctype);
			}
			if (c == '>')
			{
				this.State = HtmlParseMode.PCData;
				this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				doctype.IsQuirksForced = true;
			}
			else
			{
				if (c != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypePublicInvalid);
					doctype.IsQuirksForced = true;
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				base.Back();
			}
			return doctype;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001546C File Offset: 0x0001366C
		private HtmlToken DoctypePublicIdentifierDoubleQuoted(HtmlDoctypeToken doctype)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next == '"')
				{
					break;
				}
				if (next == '\0')
				{
					this.AppendReplacement();
				}
				else
				{
					if (next == '>')
					{
						goto Block_2;
					}
					if (next == '\uffff')
					{
						goto Block_3;
					}
					base.StringBuffer.Append(next);
				}
			}
			doctype.PublicIdentifier = base.FlushBuffer();
			return this.DoctypePublicIdentifierAfter(doctype);
			Block_2:
			this.State = HtmlParseMode.PCData;
			this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
			doctype.IsQuirksForced = true;
			doctype.PublicIdentifier = base.FlushBuffer();
			return doctype;
			Block_3:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			base.Back();
			doctype.IsQuirksForced = true;
			doctype.PublicIdentifier = base.FlushBuffer();
			return doctype;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001550C File Offset: 0x0001370C
		private HtmlToken DoctypePublicIdentifierSingleQuoted(HtmlDoctypeToken doctype)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next == '\'')
				{
					break;
				}
				if (next == '\0')
				{
					this.AppendReplacement();
				}
				else
				{
					if (next == '>')
					{
						goto Block_2;
					}
					if (next == '\uffff')
					{
						goto Block_3;
					}
					base.StringBuffer.Append(next);
				}
			}
			doctype.PublicIdentifier = base.FlushBuffer();
			return this.DoctypePublicIdentifierAfter(doctype);
			Block_2:
			this.State = HtmlParseMode.PCData;
			this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
			doctype.IsQuirksForced = true;
			doctype.PublicIdentifier = base.FlushBuffer();
			return doctype;
			Block_3:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			doctype.IsQuirksForced = true;
			doctype.PublicIdentifier = base.FlushBuffer();
			base.Back();
			return doctype;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000155AC File Offset: 0x000137AC
		private HtmlToken DoctypePublicIdentifierAfter(HtmlDoctypeToken doctype)
		{
			char next = base.GetNext();
			if (next.IsSpaceCharacter())
			{
				return this.DoctypeBetween(doctype);
			}
			if (next == '>')
			{
				this.State = HtmlParseMode.PCData;
			}
			else
			{
				if (next == '"')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
					doctype.SystemIdentifier = string.Empty;
					return this.DoctypeSystemIdentifierDoubleQuoted(doctype);
				}
				if (next == '\'')
				{
					this.RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
					doctype.SystemIdentifier = string.Empty;
					return this.DoctypeSystemIdentifierSingleQuoted(doctype);
				}
				if (next != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
					doctype.IsQuirksForced = true;
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				base.Back();
			}
			return doctype;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00015654 File Offset: 0x00013854
		private HtmlToken DoctypeBetween(HtmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '>')
			{
				this.State = HtmlParseMode.PCData;
			}
			else
			{
				if (c == '"')
				{
					doctype.SystemIdentifier = string.Empty;
					return this.DoctypeSystemIdentifierDoubleQuoted(doctype);
				}
				if (c == '\'')
				{
					doctype.SystemIdentifier = string.Empty;
					return this.DoctypeSystemIdentifierSingleQuoted(doctype);
				}
				if (c != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
					doctype.IsQuirksForced = true;
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				base.Back();
			}
			return doctype;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000156DC File Offset: 0x000138DC
		private HtmlToken DoctypeSystem(HtmlDoctypeToken doctype)
		{
			char next = base.GetNext();
			if (next.IsSpaceCharacter())
			{
				this.State = HtmlParseMode.PCData;
				return this.DoctypeSystemIdentifierBefore(doctype);
			}
			if (next == '"')
			{
				this.RaiseErrorOccurred(HtmlParseError.DoubleQuotationMarkUnexpected);
				doctype.SystemIdentifier = string.Empty;
				return this.DoctypeSystemIdentifierDoubleQuoted(doctype);
			}
			if (next == '\'')
			{
				this.RaiseErrorOccurred(HtmlParseError.SingleQuotationMarkUnexpected);
				doctype.SystemIdentifier = string.Empty;
				return this.DoctypeSystemIdentifierSingleQuoted(doctype);
			}
			if (next == '>')
			{
				this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				doctype.SystemIdentifier = base.FlushBuffer();
				doctype.IsQuirksForced = true;
			}
			else
			{
				if (next != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypeSystemInvalid);
					doctype.IsQuirksForced = true;
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				base.Back();
			}
			return doctype;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000157A0 File Offset: 0x000139A0
		private HtmlToken DoctypeSystemIdentifierBefore(HtmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '"')
			{
				doctype.SystemIdentifier = string.Empty;
				return this.DoctypeSystemIdentifierDoubleQuoted(doctype);
			}
			if (c == '\'')
			{
				doctype.SystemIdentifier = string.Empty;
				return this.DoctypeSystemIdentifierSingleQuoted(doctype);
			}
			if (c == '>')
			{
				this.State = HtmlParseMode.PCData;
				this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
				doctype.IsQuirksForced = true;
				doctype.SystemIdentifier = base.FlushBuffer();
			}
			else
			{
				if (c != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
					doctype.IsQuirksForced = true;
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				doctype.SystemIdentifier = base.FlushBuffer();
				base.Back();
			}
			return doctype;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00015850 File Offset: 0x00013A50
		private HtmlToken DoctypeSystemIdentifierDoubleQuoted(HtmlDoctypeToken doctype)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next == '"')
				{
					break;
				}
				if (next == '\0')
				{
					this.AppendReplacement();
				}
				else
				{
					if (next == '>')
					{
						goto Block_2;
					}
					if (next == '\uffff')
					{
						goto Block_3;
					}
					base.StringBuffer.Append(next);
				}
			}
			doctype.SystemIdentifier = base.FlushBuffer();
			return this.DoctypeSystemIdentifierAfter(doctype);
			Block_2:
			this.State = HtmlParseMode.PCData;
			this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
			doctype.IsQuirksForced = true;
			doctype.SystemIdentifier = base.FlushBuffer();
			return doctype;
			Block_3:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			doctype.IsQuirksForced = true;
			doctype.SystemIdentifier = base.FlushBuffer();
			base.Back();
			return doctype;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000158F0 File Offset: 0x00013AF0
		private HtmlToken DoctypeSystemIdentifierSingleQuoted(HtmlDoctypeToken doctype)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next <= '\'')
				{
					if (next == '\0')
					{
						this.AppendReplacement();
						continue;
					}
					if (next == '\'')
					{
						break;
					}
				}
				else
				{
					if (next == '>')
					{
						goto IL_0041;
					}
					if (next == '\uffff')
					{
						goto IL_0065;
					}
				}
				base.StringBuffer.Append(next);
			}
			doctype.SystemIdentifier = base.FlushBuffer();
			return this.DoctypeSystemIdentifierAfter(doctype);
			IL_0041:
			this.State = HtmlParseMode.PCData;
			this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
			doctype.IsQuirksForced = true;
			doctype.SystemIdentifier = base.FlushBuffer();
			return doctype;
			IL_0065:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			doctype.IsQuirksForced = true;
			doctype.SystemIdentifier = base.FlushBuffer();
			base.Back();
			return doctype;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00015998 File Offset: 0x00013B98
		private HtmlToken DoctypeSystemIdentifierAfter(HtmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c != '>')
			{
				if (c != '\uffff')
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypeInvalidCharacter);
					return this.BogusDoctype(doctype);
				}
				this.RaiseErrorOccurred(HtmlParseError.EOF);
				doctype.IsQuirksForced = true;
				base.Back();
			}
			else
			{
				this.State = HtmlParseMode.PCData;
			}
			return doctype;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000159EC File Offset: 0x00013BEC
		private HtmlToken BogusDoctype(HtmlDoctypeToken doctype)
		{
			for (;;)
			{
				char next = base.GetNext();
				if (next == '>')
				{
					break;
				}
				if (next == '\uffff')
				{
					goto IL_001F;
				}
			}
			this.State = HtmlParseMode.PCData;
			return doctype;
			IL_001F:
			base.Back();
			return doctype;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00015A20 File Offset: 0x00013C20
		private HtmlToken ParseAttributes(HtmlTagToken tag)
		{
			HtmlTokenizer.AttributeState attributeState = HtmlTokenizer.AttributeState.BeforeName;
			char c = '"';
			char c2 = '\0';
			for (;;)
			{
				switch (attributeState)
				{
				case HtmlTokenizer.AttributeState.BeforeName:
					c2 = base.SkipSpaces();
					if (c2 == '/')
					{
						attributeState = HtmlTokenizer.AttributeState.SelfClose;
					}
					else
					{
						if (c2 == '>')
						{
							goto Block_3;
						}
						if (c2.IsUppercaseAscii())
						{
							base.StringBuffer.Append(char.ToLowerInvariant(c2));
							attributeState = HtmlTokenizer.AttributeState.Name;
						}
						else if (c2 == '\0')
						{
							this.AppendReplacement();
							attributeState = HtmlTokenizer.AttributeState.Name;
						}
						else if (c2 == '\'' || c2 == '"' || c2 == '=' || c2 == '<')
						{
							this.RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
							base.StringBuffer.Append(c2);
							attributeState = HtmlTokenizer.AttributeState.Name;
						}
						else
						{
							if (c2 == '\uffff')
							{
								goto IL_00C3;
							}
							base.StringBuffer.Append(c2);
							attributeState = HtmlTokenizer.AttributeState.Name;
						}
					}
					break;
				case HtmlTokenizer.AttributeState.Name:
					c2 = base.GetNext();
					if (c2 == '=')
					{
						tag.AddAttribute(base.FlushBuffer());
						attributeState = HtmlTokenizer.AttributeState.BeforeValue;
					}
					else
					{
						if (c2 == '>')
						{
							goto Block_11;
						}
						if (c2.IsSpaceCharacter())
						{
							tag.AddAttribute(base.FlushBuffer());
							attributeState = HtmlTokenizer.AttributeState.AfterName;
						}
						else if (c2 == '/')
						{
							tag.AddAttribute(base.FlushBuffer());
							attributeState = HtmlTokenizer.AttributeState.SelfClose;
						}
						else if (c2.IsUppercaseAscii())
						{
							base.StringBuffer.Append(char.ToLowerInvariant(c2));
						}
						else if (c2 == '"' || c2 == '\'' || c2 == '<')
						{
							this.RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
							base.StringBuffer.Append(c2);
						}
						else if (c2 == '\0')
						{
							this.AppendReplacement();
						}
						else
						{
							if (c2 == '\uffff')
							{
								goto IL_01A6;
							}
							base.StringBuffer.Append(c2);
						}
					}
					break;
				case HtmlTokenizer.AttributeState.AfterName:
					c2 = base.SkipSpaces();
					if (c2 == '>')
					{
						goto Block_19;
					}
					if (c2 == '=')
					{
						attributeState = HtmlTokenizer.AttributeState.BeforeValue;
					}
					else if (c2 == '/')
					{
						attributeState = HtmlTokenizer.AttributeState.SelfClose;
					}
					else if (c2.IsUppercaseAscii())
					{
						base.StringBuffer.Append(char.ToLowerInvariant(c2));
						attributeState = HtmlTokenizer.AttributeState.Name;
					}
					else if (c2 == '"' || c2 == '\'' || c2 == '<')
					{
						this.RaiseErrorOccurred(HtmlParseError.AttributeNameInvalid);
						base.StringBuffer.Append(c2);
						attributeState = HtmlTokenizer.AttributeState.Name;
					}
					else if (c2 == '\0')
					{
						this.AppendReplacement();
						attributeState = HtmlTokenizer.AttributeState.Name;
					}
					else
					{
						if (c2 == '\uffff')
						{
							goto IL_0252;
						}
						base.StringBuffer.Append(c2);
						attributeState = HtmlTokenizer.AttributeState.Name;
					}
					break;
				case HtmlTokenizer.AttributeState.BeforeValue:
					c2 = base.SkipSpaces();
					if (c2 == '"' || c2 == '\'')
					{
						attributeState = HtmlTokenizer.AttributeState.QuotedValue;
						c = c2;
					}
					else if (c2 == '&')
					{
						attributeState = HtmlTokenizer.AttributeState.UnquotedValue;
					}
					else
					{
						if (c2 == '>')
						{
							goto Block_29;
						}
						if (c2 == '<' || c2 == '=' || c2 == '`')
						{
							this.RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
							base.StringBuffer.Append(c2);
							attributeState = HtmlTokenizer.AttributeState.UnquotedValue;
							c2 = base.GetNext();
						}
						else if (c2 == '\0')
						{
							this.AppendReplacement();
							attributeState = HtmlTokenizer.AttributeState.UnquotedValue;
							c2 = base.GetNext();
						}
						else
						{
							if (c2 == '\uffff')
							{
								goto IL_0301;
							}
							base.StringBuffer.Append(c2);
							attributeState = HtmlTokenizer.AttributeState.UnquotedValue;
							c2 = base.GetNext();
						}
					}
					break;
				case HtmlTokenizer.AttributeState.QuotedValue:
					c2 = base.GetNext();
					if (c2 == c)
					{
						tag.SetAttributeValue(base.FlushBuffer());
						attributeState = HtmlTokenizer.AttributeState.AfterValue;
					}
					else if (c2 == '&')
					{
						this.AppendCharacterReference(base.GetNext(), c);
					}
					else if (c2 == '\0')
					{
						this.AppendReplacement();
					}
					else
					{
						if (c2 == '\uffff')
						{
							goto IL_0366;
						}
						base.StringBuffer.Append(c2);
					}
					break;
				case HtmlTokenizer.AttributeState.AfterValue:
					c2 = base.GetNext();
					if (c2 == '>')
					{
						goto Block_47;
					}
					if (c2.IsSpaceCharacter())
					{
						attributeState = HtmlTokenizer.AttributeState.BeforeName;
					}
					else if (c2 == '/')
					{
						attributeState = HtmlTokenizer.AttributeState.SelfClose;
					}
					else
					{
						if (c2 == '\uffff')
						{
							goto Block_50;
						}
						this.RaiseErrorOccurred(HtmlParseError.AttributeNameExpected);
						base.Back();
						attributeState = HtmlTokenizer.AttributeState.BeforeName;
					}
					break;
				case HtmlTokenizer.AttributeState.UnquotedValue:
					if (c2 == '>')
					{
						goto Block_38;
					}
					if (c2.IsSpaceCharacter())
					{
						tag.SetAttributeValue(base.FlushBuffer());
						attributeState = HtmlTokenizer.AttributeState.BeforeName;
					}
					else if (c2 == '&')
					{
						this.AppendCharacterReference(base.GetNext(), '>');
						c2 = base.GetNext();
					}
					else if (c2 == '\0')
					{
						this.AppendReplacement();
						c2 = base.GetNext();
					}
					else if (c2 == '"' || c2 == '\'' || c2 == '<' || c2 == '=' || c2 == '`')
					{
						this.RaiseErrorOccurred(HtmlParseError.AttributeValueInvalid);
						base.StringBuffer.Append(c2);
						c2 = base.GetNext();
					}
					else
					{
						if (c2 == '\uffff')
						{
							goto IL_0431;
						}
						base.StringBuffer.Append(c2);
						c2 = base.GetNext();
					}
					break;
				case HtmlTokenizer.AttributeState.SelfClose:
				{
					HtmlToken htmlToken = this.TagSelfClosingInner(tag);
					if (htmlToken != null)
					{
						return htmlToken;
					}
					attributeState = HtmlTokenizer.AttributeState.BeforeName;
					break;
				}
				}
			}
			Block_3:
			return this.EmitTag(tag);
			IL_00C3:
			return this.NewEof(false);
			Block_11:
			tag.AddAttribute(base.FlushBuffer());
			return this.EmitTag(tag);
			IL_01A6:
			return this.NewEof(false);
			Block_19:
			return this.EmitTag(tag);
			IL_0252:
			return this.NewEof(false);
			Block_29:
			this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong);
			return this.EmitTag(tag);
			IL_0301:
			return this.NewEof(false);
			IL_0366:
			return this.NewEof(false);
			Block_38:
			tag.SetAttributeValue(base.FlushBuffer());
			return this.EmitTag(tag);
			IL_0431:
			return this.NewEof(false);
			Block_47:
			return this.EmitTag(tag);
			Block_50:
			return this.NewEof(false);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00015ED0 File Offset: 0x000140D0
		private HtmlToken ScriptData(char c)
		{
			int length = this._lastStartTag.Length;
			int length2 = TagNames.Script.Length;
			HtmlTokenizer.ScriptState scriptState = HtmlTokenizer.ScriptState.Normal;
			int num = 0;
			HtmlTagToken htmlTagToken;
			for (;;)
			{
				switch (scriptState)
				{
				case HtmlTokenizer.ScriptState.Normal:
					if (c != '\0')
					{
						if (c == '<')
						{
							base.StringBuffer.Append('<');
							scriptState = HtmlTokenizer.ScriptState.OpenTag;
							break;
						}
						if (c == '\uffff')
						{
							goto IL_0093;
						}
						base.StringBuffer.Append(c);
					}
					else
					{
						this.AppendReplacement();
					}
					c = base.GetNext();
					break;
				case HtmlTokenizer.ScriptState.OpenTag:
					c = base.GetNext();
					if (c == '/')
					{
						scriptState = HtmlTokenizer.ScriptState.EndTag;
					}
					else if (c == '!')
					{
						scriptState = HtmlTokenizer.ScriptState.StartEscape;
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.Normal;
					}
					break;
				case HtmlTokenizer.ScriptState.EndTag:
					c = base.GetNext();
					num = base.StringBuffer.Append('/').Length;
					htmlTagToken = this.NewTagClose();
					while (c.IsLetter())
					{
						base.StringBuffer.Append(c);
						c = base.GetNext();
						bool flag = c.IsSpaceCharacter();
						bool flag2 = c == '>';
						bool flag3 = c == '/';
						if (base.StringBuffer.Length - num == length && (flag || flag2 || flag3) && base.StringBuffer.ToString(num, length).Isi(this._lastStartTag))
						{
							if (num > 2)
							{
								goto Block_12;
							}
							base.StringBuffer.Clear();
							if (flag)
							{
								goto Block_13;
							}
							if (flag3)
							{
								goto Block_14;
							}
							if (flag2)
							{
								goto Block_15;
							}
						}
					}
					scriptState = HtmlTokenizer.ScriptState.Normal;
					break;
				case HtmlTokenizer.ScriptState.StartEscape:
					base.StringBuffer.Append('!');
					c = base.GetNext();
					if (c == '-')
					{
						scriptState = HtmlTokenizer.ScriptState.StartEscapeDash;
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.Normal;
					}
					break;
				case HtmlTokenizer.ScriptState.Escaped:
					if (c <= '-')
					{
						if (c == '\0')
						{
							this.AppendReplacement();
							c = base.GetNext();
							break;
						}
						if (c == '-')
						{
							base.StringBuffer.Append('-');
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedDash;
							break;
						}
					}
					else
					{
						if (c == '<')
						{
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedOpenTag;
							break;
						}
						if (c == '\uffff')
						{
							goto IL_02B7;
						}
					}
					scriptState = HtmlTokenizer.ScriptState.Normal;
					break;
				case HtmlTokenizer.ScriptState.StartEscapeDash:
					c = base.GetNext();
					base.StringBuffer.Append('-');
					if (c == '-')
					{
						base.StringBuffer.Append('-');
						scriptState = HtmlTokenizer.ScriptState.EscapedDashDash;
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.Normal;
					}
					break;
				case HtmlTokenizer.ScriptState.EscapedDash:
					if (c <= '-')
					{
						if (c != '\0')
						{
							if (c != '-')
							{
								goto IL_032F;
							}
							base.StringBuffer.Append('-');
							scriptState = HtmlTokenizer.ScriptState.EscapedDashDash;
							break;
						}
						else
						{
							this.AppendReplacement();
						}
					}
					else
					{
						if (c == '<')
						{
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedOpenTag;
							break;
						}
						if (c != '\uffff')
						{
							goto IL_032F;
						}
						goto IL_0322;
					}
					IL_033C:
					c = base.GetNext();
					scriptState = HtmlTokenizer.ScriptState.Escaped;
					break;
					IL_032F:
					base.StringBuffer.Append(c);
					goto IL_033C;
				case HtmlTokenizer.ScriptState.EscapedDashDash:
					c = base.GetNext();
					if (c <= '-')
					{
						if (c == '\0')
						{
							this.AppendReplacement();
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.Escaped;
							break;
						}
						if (c == '-')
						{
							base.StringBuffer.Append('-');
							break;
						}
					}
					else
					{
						if (c == '<')
						{
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedOpenTag;
							break;
						}
						if (c == '>')
						{
							base.StringBuffer.Append('>');
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.Normal;
							break;
						}
						if (c == '\uffff')
						{
							goto IL_03CA;
						}
					}
					base.StringBuffer.Append(c);
					c = base.GetNext();
					scriptState = HtmlTokenizer.ScriptState.Escaped;
					break;
				case HtmlTokenizer.ScriptState.EscapedOpenTag:
					if (c == '/')
					{
						c = base.GetNext();
						scriptState = HtmlTokenizer.ScriptState.EscapedEndTag;
					}
					else if (c.IsLetter())
					{
						num = base.StringBuffer.Append('<').Length;
						base.StringBuffer.Append(c);
						scriptState = HtmlTokenizer.ScriptState.StartDoubleEscape;
					}
					else
					{
						base.StringBuffer.Append('<');
						scriptState = HtmlTokenizer.ScriptState.Escaped;
					}
					break;
				case HtmlTokenizer.ScriptState.EscapedEndTag:
					num = base.StringBuffer.Append('<').Append('/').Length;
					if (c.IsLetter())
					{
						base.StringBuffer.Append(c);
						scriptState = HtmlTokenizer.ScriptState.EscapedNameEndTag;
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.Escaped;
					}
					break;
				case HtmlTokenizer.ScriptState.EscapedNameEndTag:
					c = base.GetNext();
					if (base.StringBuffer.Length - num == length2 && (c == '/' || c == '>' || c.IsSpaceCharacter()) && base.StringBuffer.ToString(num, length2).Isi(TagNames.Script))
					{
						goto Block_39;
					}
					if (!c.IsLetter())
					{
						scriptState = HtmlTokenizer.ScriptState.Escaped;
					}
					else
					{
						base.StringBuffer.Append(c);
					}
					break;
				case HtmlTokenizer.ScriptState.StartDoubleEscape:
					c = base.GetNext();
					if (base.StringBuffer.Length - num == length2 && (c == '/' || c == '>' || c.IsSpaceCharacter()))
					{
						bool flag4 = base.StringBuffer.ToString(num, length2).Isi(TagNames.Script);
						base.StringBuffer.Append(c);
						c = base.GetNext();
						scriptState = (flag4 ? HtmlTokenizer.ScriptState.EscapedDouble : HtmlTokenizer.ScriptState.Escaped);
					}
					else if (c.IsLetter())
					{
						base.StringBuffer.Append(c);
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.Escaped;
					}
					break;
				case HtmlTokenizer.ScriptState.EscapedDouble:
					if (c <= '-')
					{
						if (c != '\0')
						{
							if (c == '-')
							{
								base.StringBuffer.Append('-');
								c = base.GetNext();
								scriptState = HtmlTokenizer.ScriptState.EscapedDoubleDash;
								break;
							}
						}
						else
						{
							this.AppendReplacement();
						}
					}
					else
					{
						if (c == '<')
						{
							base.StringBuffer.Append('<');
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedDoubleOpenTag;
							break;
						}
						if (c == '\uffff')
						{
							goto IL_05F5;
						}
					}
					base.StringBuffer.Append(c);
					c = base.GetNext();
					break;
				case HtmlTokenizer.ScriptState.EscapedDoubleDash:
					if (c <= '-')
					{
						if (c != '\0')
						{
							if (c == '-')
							{
								base.StringBuffer.Append('-');
								scriptState = HtmlTokenizer.ScriptState.EscapedDoubleDashDash;
								break;
							}
						}
						else
						{
							this.RaiseErrorOccurred(HtmlParseError.Null);
							c = '\ufffd';
						}
					}
					else
					{
						if (c == '<')
						{
							base.StringBuffer.Append('<');
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedDoubleOpenTag;
							break;
						}
						if (c == '\uffff')
						{
							goto IL_0685;
						}
					}
					scriptState = HtmlTokenizer.ScriptState.EscapedDouble;
					break;
				case HtmlTokenizer.ScriptState.EscapedDoubleDashDash:
					c = base.GetNext();
					if (c <= '-')
					{
						if (c == '\0')
						{
							this.AppendReplacement();
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedDouble;
							break;
						}
						if (c == '-')
						{
							base.StringBuffer.Append('-');
							break;
						}
					}
					else
					{
						if (c == '<')
						{
							base.StringBuffer.Append('<');
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.EscapedDoubleOpenTag;
							break;
						}
						if (c == '>')
						{
							base.StringBuffer.Append('>');
							c = base.GetNext();
							scriptState = HtmlTokenizer.ScriptState.Normal;
							break;
						}
						if (c == '\uffff')
						{
							goto IL_0733;
						}
					}
					base.StringBuffer.Append(c);
					c = base.GetNext();
					scriptState = HtmlTokenizer.ScriptState.EscapedDouble;
					break;
				case HtmlTokenizer.ScriptState.EscapedDoubleOpenTag:
					if (c == '/')
					{
						num = base.StringBuffer.Append('/').Length;
						scriptState = HtmlTokenizer.ScriptState.EndDoubleEscape;
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.EscapedDouble;
					}
					break;
				case HtmlTokenizer.ScriptState.EndDoubleEscape:
					c = base.GetNext();
					if (base.StringBuffer.Length - num == length2 && (c.IsSpaceCharacter() || c == '/' || c == '>'))
					{
						bool flag5 = base.StringBuffer.ToString(num, length2).Isi(TagNames.Script);
						base.StringBuffer.Append(c);
						c = base.GetNext();
						scriptState = (flag5 ? HtmlTokenizer.ScriptState.Escaped : HtmlTokenizer.ScriptState.EscapedDouble);
					}
					else if (c.IsLetter())
					{
						base.StringBuffer.Append(c);
					}
					else
					{
						scriptState = HtmlTokenizer.ScriptState.EscapedDouble;
					}
					break;
				}
			}
			IL_0093:
			base.Back();
			return this.NewCharacter();
			Block_12:
			base.Back(3 + length);
			base.StringBuffer.Remove(num - 2, length + 2);
			return this.NewCharacter();
			Block_13:
			htmlTagToken.Name = this._lastStartTag;
			return this.ParseAttributes(htmlTagToken);
			Block_14:
			htmlTagToken.Name = this._lastStartTag;
			return this.TagSelfClosing(htmlTagToken);
			Block_15:
			htmlTagToken.Name = this._lastStartTag;
			return this.EmitTag(htmlTagToken);
			IL_02B7:
			base.Back();
			return this.NewCharacter();
			IL_0322:
			base.Back();
			return this.NewCharacter();
			IL_03CA:
			return this.NewCharacter();
			Block_39:
			base.Back(length2 + 3);
			base.StringBuffer.Remove(num - 2, length2 + 2);
			return this.NewCharacter();
			IL_05F5:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			base.Back();
			return this.NewCharacter();
			IL_0685:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			base.Back();
			return this.NewCharacter();
			IL_0733:
			this.RaiseErrorOccurred(HtmlParseError.EOF);
			base.Back();
			return this.NewCharacter();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000166F0 File Offset: 0x000148F0
		private HtmlToken NewCharacter()
		{
			string text = base.FlushBuffer();
			return new HtmlToken(HtmlTokenType.Character, this._position, text);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00016714 File Offset: 0x00014914
		private HtmlToken NewComment()
		{
			string text = base.FlushBuffer();
			return new HtmlToken(HtmlTokenType.Comment, this._position, text);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00016735 File Offset: 0x00014935
		private HtmlToken NewEof(bool acceptable = false)
		{
			if (!acceptable)
			{
				this.RaiseErrorOccurred(HtmlParseError.EOF);
			}
			return new HtmlToken(HtmlTokenType.EndOfFile, this._position, null);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001674E File Offset: 0x0001494E
		private HtmlDoctypeToken NewDoctype(bool quirksForced)
		{
			return new HtmlDoctypeToken(quirksForced, this._position);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001675C File Offset: 0x0001495C
		private HtmlTagToken NewTagOpen()
		{
			return new HtmlTagToken(HtmlTokenType.StartTag, this._position);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001676A File Offset: 0x0001496A
		private HtmlTagToken NewTagClose()
		{
			return new HtmlTagToken(HtmlTokenType.EndTag, this._position);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00016778 File Offset: 0x00014978
		private void RaiseErrorOccurred(HtmlParseError code)
		{
			this.RaiseErrorOccurred(code, base.GetCurrentPosition());
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00016787 File Offset: 0x00014987
		private void AppendReplacement()
		{
			this.RaiseErrorOccurred(HtmlParseError.Null);
			base.StringBuffer.Append('\ufffd');
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000167A4 File Offset: 0x000149A4
		private HtmlToken CreateIfAppropriate(char c)
		{
			bool flag = c.IsSpaceCharacter();
			bool flag2 = c == '>';
			bool flag3 = c == '/';
			if (base.StringBuffer.Length == this._lastStartTag.Length && (flag || flag2 || flag3) && base.StringBuffer.ToString().Is(this._lastStartTag))
			{
				HtmlTagToken htmlTagToken = this.NewTagClose();
				base.StringBuffer.Clear();
				if (flag)
				{
					htmlTagToken.Name = this._lastStartTag;
					return this.ParseAttributes(htmlTagToken);
				}
				if (flag3)
				{
					htmlTagToken.Name = this._lastStartTag;
					return this.TagSelfClosing(htmlTagToken);
				}
				if (flag2)
				{
					htmlTagToken.Name = this._lastStartTag;
					return this.EmitTag(htmlTagToken);
				}
			}
			return null;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00016858 File Offset: 0x00014A58
		private HtmlToken EmitTag(HtmlTagToken tag)
		{
			List<KeyValuePair<string, string>> attributes = tag.Attributes;
			this.State = HtmlParseMode.PCData;
			HtmlTokenType type = tag.Type;
			if (type != HtmlTokenType.StartTag)
			{
				if (type == HtmlTokenType.EndTag)
				{
					if (tag.IsSelfClosing)
					{
						this.RaiseErrorOccurred(HtmlParseError.EndTagCannotBeSelfClosed, tag.Position);
					}
					if (attributes.Count != 0)
					{
						this.RaiseErrorOccurred(HtmlParseError.EndTagCannotHaveAttributes, tag.Position);
					}
				}
			}
			else
			{
				for (int i = attributes.Count - 1; i > 0; i--)
				{
					for (int j = i - 1; j >= 0; j--)
					{
						if (attributes[j].Key == attributes[i].Key)
						{
							attributes.RemoveAt(i);
							this.RaiseErrorOccurred(HtmlParseError.AttributeDuplicateOmitted, tag.Position);
							break;
						}
					}
				}
				this._lastStartTag = tag.Name;
			}
			return tag;
		}

		// Token: 0x040002A5 RID: 677
		private readonly IEntityProvider _resolver;

		// Token: 0x040002A6 RID: 678
		private string _lastStartTag;

		// Token: 0x040002A7 RID: 679
		private TextPosition _position;

		// Token: 0x02000440 RID: 1088
		private enum AttributeState : byte
		{
			// Token: 0x04000F54 RID: 3924
			BeforeName,
			// Token: 0x04000F55 RID: 3925
			Name,
			// Token: 0x04000F56 RID: 3926
			AfterName,
			// Token: 0x04000F57 RID: 3927
			BeforeValue,
			// Token: 0x04000F58 RID: 3928
			QuotedValue,
			// Token: 0x04000F59 RID: 3929
			AfterValue,
			// Token: 0x04000F5A RID: 3930
			UnquotedValue,
			// Token: 0x04000F5B RID: 3931
			SelfClose
		}

		// Token: 0x02000441 RID: 1089
		private enum ScriptState : byte
		{
			// Token: 0x04000F5D RID: 3933
			Normal,
			// Token: 0x04000F5E RID: 3934
			OpenTag,
			// Token: 0x04000F5F RID: 3935
			EndTag,
			// Token: 0x04000F60 RID: 3936
			StartEscape,
			// Token: 0x04000F61 RID: 3937
			Escaped,
			// Token: 0x04000F62 RID: 3938
			StartEscapeDash,
			// Token: 0x04000F63 RID: 3939
			EscapedDash,
			// Token: 0x04000F64 RID: 3940
			EscapedDashDash,
			// Token: 0x04000F65 RID: 3941
			EscapedOpenTag,
			// Token: 0x04000F66 RID: 3942
			EscapedEndTag,
			// Token: 0x04000F67 RID: 3943
			EscapedNameEndTag,
			// Token: 0x04000F68 RID: 3944
			StartDoubleEscape,
			// Token: 0x04000F69 RID: 3945
			EscapedDouble,
			// Token: 0x04000F6A RID: 3946
			EscapedDoubleDash,
			// Token: 0x04000F6B RID: 3947
			EscapedDoubleDashDash,
			// Token: 0x04000F6C RID: 3948
			EscapedDoubleOpenTag,
			// Token: 0x04000F6D RID: 3949
			EndDoubleEscape
		}
	}
}
