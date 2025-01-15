using System;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Services;

namespace AngleSharp.Parser.Xml
{
	// Token: 0x0200005D RID: 93
	internal sealed class XmlTokenizer : BaseTokenizer
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000DA19 File Offset: 0x0000BC19
		public XmlTokenizer(TextSource source, IEntityProvider resolver)
			: base(source)
		{
			this._resolver = resolver;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000DA29 File Offset: 0x0000BC29
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000DA31 File Offset: 0x0000BC31
		public bool IsSuppressingErrors { get; set; }

		// Token: 0x060001EB RID: 491 RVA: 0x0000DA3C File Offset: 0x0000BC3C
		public XmlToken Get()
		{
			char next = base.GetNext();
			if (next != '\uffff')
			{
				this._position = base.GetCurrentPosition();
				return this.Data(next);
			}
			return this.NewEof();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000DA72 File Offset: 0x0000BC72
		private XmlToken Data(char c)
		{
			if (c == '<')
			{
				return this.TagOpen();
			}
			if (c != '\uffff')
			{
				return this.DataText(c);
			}
			return this.NewEof();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DA98 File Offset: 0x0000BC98
		private XmlToken DataText(char c)
		{
			for (;;)
			{
				if (c <= '<')
				{
					if (c == '&')
					{
						base.StringBuffer.Append(this.CharacterReference());
						c = base.GetNext();
						continue;
					}
					if (c == '<')
					{
						break;
					}
				}
				else
				{
					if (c == ']')
					{
						base.StringBuffer.Append(c);
						c = this.CheckNextCharacter();
						continue;
					}
					if (c == '\uffff')
					{
						break;
					}
				}
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			base.Back();
			return this.NewCharacters();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000DB19 File Offset: 0x0000BD19
		private char CheckNextCharacter()
		{
			char next = base.GetNext();
			if (next == ']')
			{
				if (base.GetNext() == '>')
				{
					throw XmlParseError.XmlInvalidCharData.At(base.GetCurrentPosition());
				}
				base.Back();
			}
			return next;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000DB48 File Offset: 0x0000BD48
		private XmlCDataToken CData()
		{
			for (char c = base.GetNext(); c != '\uffff'; c = base.GetNext())
			{
				if (c == ']' && base.ContinuesWithSensitive("]]>"))
				{
					base.Advance(2);
					return this.NewCharacterData();
				}
				base.StringBuffer.Append(c);
			}
			throw XmlParseError.EOF.At(base.GetCurrentPosition());
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		private string CharacterReference()
		{
			char c = base.GetNext();
			int length = base.StringBuffer.Length;
			bool flag = c == '#';
			if (flag)
			{
				c = base.GetNext();
				if (c == 'x' || c == 'X')
				{
					c = base.GetNext();
					while (c.IsHex())
					{
						base.StringBuffer.Append(c);
						c = base.GetNext();
					}
				}
				else
				{
					while (c.IsDigit())
					{
						base.StringBuffer.Append(c);
						c = base.GetNext();
					}
				}
			}
			else if (c.IsXmlNameStart())
			{
				do
				{
					base.StringBuffer.Append(c);
					c = base.GetNext();
				}
				while (c.IsXmlName());
			}
			if (c == ';' && base.StringBuffer.Length > length)
			{
				int num = base.StringBuffer.Length - length;
				string text = base.StringBuffer.ToString(length, num);
				if (flag)
				{
					int num2 = (flag ? text.FromHex() : text.FromDec());
					if (num2.IsValidAsCharRef())
					{
						base.StringBuffer.Remove(length, num);
						return num2.ConvertFromUtf32();
					}
				}
				else
				{
					string symbol = this._resolver.GetSymbol(text);
					if (!string.IsNullOrEmpty(symbol))
					{
						base.StringBuffer.Remove(length, num);
						return symbol;
					}
				}
				if (!this.IsSuppressingErrors)
				{
					throw XmlParseError.CharacterReferenceInvalidCode.At(this._position);
				}
				base.StringBuffer.Append(c);
			}
			if (!this.IsSuppressingErrors)
			{
				throw XmlParseError.CharacterReferenceNotTerminated.At(base.GetCurrentPosition());
			}
			base.StringBuffer.Insert(length, '&');
			return string.Empty;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DD34 File Offset: 0x0000BF34
		private XmlToken TagOpen()
		{
			char c = base.GetNext();
			if (c == '!')
			{
				return this.MarkupDeclaration();
			}
			if (c == '?')
			{
				c = base.GetNext();
				if (base.ContinuesWithSensitive(TagNames.Xml))
				{
					base.Advance(2);
					return this.DeclarationStart();
				}
				return this.ProcessingStart(c);
			}
			else
			{
				if (c == '/')
				{
					return this.TagEnd();
				}
				if (c.IsXmlNameStart())
				{
					base.StringBuffer.Append(c);
					return this.TagName(this.NewOpenTag());
				}
				throw XmlParseError.XmlInvalidStartTag.At(base.GetCurrentPosition());
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		private XmlToken TagEnd()
		{
			char c = base.GetNext();
			if (c.IsXmlNameStart())
			{
				do
				{
					base.StringBuffer.Append(c);
					c = base.GetNext();
				}
				while (c.IsXmlName());
				while (c.IsSpaceCharacter())
				{
					c = base.GetNext();
				}
				if (c == '>')
				{
					XmlTagToken xmlTagToken = this.NewCloseTag();
					xmlTagToken.Name = base.FlushBuffer();
					return xmlTagToken;
				}
			}
			if (c == '\uffff')
			{
				throw XmlParseError.EOF.At(base.GetCurrentPosition());
			}
			throw XmlParseError.XmlInvalidEndTag.At(base.GetCurrentPosition());
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000DE4C File Offset: 0x0000C04C
		private XmlToken TagName(XmlTagToken tag)
		{
			char c = base.GetNext();
			while (c.IsXmlName())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			tag.Name = base.FlushBuffer();
			if (c == '\uffff')
			{
				throw XmlParseError.EOF.At(base.GetCurrentPosition());
			}
			if (c == '>')
			{
				return tag;
			}
			if (c.IsSpaceCharacter())
			{
				return this.AttributeBeforeName(tag);
			}
			if (c == '/')
			{
				return this.TagSelfClosing(tag);
			}
			throw XmlParseError.XmlInvalidName.At(base.GetCurrentPosition());
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		private XmlToken TagSelfClosing(XmlTagToken tag)
		{
			char next = base.GetNext();
			tag.IsSelfClosing = true;
			if (next == '>')
			{
				return tag;
			}
			if (next == '\uffff')
			{
				throw XmlParseError.EOF.At(base.GetCurrentPosition());
			}
			throw XmlParseError.XmlInvalidName.At(base.GetCurrentPosition());
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000DF1C File Offset: 0x0000C11C
		private XmlToken MarkupDeclaration()
		{
			base.GetNext();
			if (base.ContinuesWithSensitive("--"))
			{
				base.Advance();
				return this.CommentStart();
			}
			if (base.ContinuesWithSensitive(TagNames.Doctype))
			{
				base.Advance(6);
				return this.Doctype();
			}
			if (base.ContinuesWithSensitive(Keywords.CData))
			{
				base.Advance(6);
				return this.CData();
			}
			throw XmlParseError.UndefinedMarkupDeclaration.At(base.GetCurrentPosition());
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000DF90 File Offset: 0x0000C190
		private XmlToken DeclarationStart()
		{
			char c = base.GetNext();
			if (!c.IsSpaceCharacter())
			{
				base.StringBuffer.Append(TagNames.Xml);
				return this.ProcessingTarget(c, this.NewProcessing());
			}
			do
			{
				c = base.GetNext();
			}
			while (c.IsSpaceCharacter());
			if (base.ContinuesWithSensitive(AttributeNames.Version))
			{
				base.Advance(6);
				return this.DeclarationVersionAfterName(this.NewDeclaration());
			}
			throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000E00B File Offset: 0x0000C20B
		private XmlToken DeclarationVersionAfterName(XmlDeclarationToken decl)
		{
			if (base.SkipSpaces() != '=')
			{
				throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
			}
			return this.DeclarationVersionBeforeValue(decl);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000E030 File Offset: 0x0000C230
		private XmlToken DeclarationVersionBeforeValue(XmlDeclarationToken decl)
		{
			char c = base.SkipSpaces();
			if (c != '"' && c != '\'')
			{
				throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
			}
			return this.DeclarationVersionValue(decl, c);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000E068 File Offset: 0x0000C268
		private XmlToken DeclarationVersionValue(XmlDeclarationToken decl, char quote)
		{
			char c;
			for (c = base.GetNext(); c != quote; c = base.GetNext())
			{
				if (c == '\uffff')
				{
					throw XmlParseError.EOF.At(base.GetCurrentPosition());
				}
				base.StringBuffer.Append(c);
			}
			decl.Version = base.FlushBuffer();
			c = base.GetNext();
			if (c.IsSpaceCharacter())
			{
				return this.DeclarationAfterVersion(decl);
			}
			return this.DeclarationEnd(c, decl);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		private XmlToken DeclarationAfterVersion(XmlDeclarationToken decl)
		{
			char c = base.SkipSpaces();
			if (base.ContinuesWithSensitive(AttributeNames.Encoding))
			{
				base.Advance(7);
				return this.DeclarationEncodingAfterName(decl);
			}
			if (base.ContinuesWithSensitive(AttributeNames.Standalone))
			{
				base.Advance(9);
				return this.DeclarationStandaloneAfterName(decl);
			}
			return this.DeclarationEnd(c, decl);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000E12D File Offset: 0x0000C32D
		private XmlToken DeclarationEncodingAfterName(XmlDeclarationToken decl)
		{
			if (base.SkipSpaces() != '=')
			{
				throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
			}
			return this.DeclarationEncodingBeforeValue(decl);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000E154 File Offset: 0x0000C354
		private XmlToken DeclarationEncodingBeforeValue(XmlDeclarationToken decl)
		{
			char c = base.SkipSpaces();
			if (c == '"' || c == '\'')
			{
				char c2 = c;
				c = base.GetNext();
				if (c.IsLetter())
				{
					base.StringBuffer.Append(c);
					return this.DeclarationEncodingValue(decl, c2);
				}
			}
			throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000E1AC File Offset: 0x0000C3AC
		private XmlToken DeclarationEncodingValue(XmlDeclarationToken decl, char quote)
		{
			char c;
			for (c = base.GetNext(); c != quote; c = base.GetNext())
			{
				if (!c.IsAlphanumericAscii() && c != '.' && c != '_' && c != '-')
				{
					throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
				}
				base.StringBuffer.Append(c);
			}
			decl.Encoding = base.FlushBuffer();
			c = base.GetNext();
			if (c.IsSpaceCharacter())
			{
				return this.DeclarationAfterEncoding(decl);
			}
			return this.DeclarationEnd(c, decl);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000E230 File Offset: 0x0000C430
		private XmlToken DeclarationAfterEncoding(XmlDeclarationToken decl)
		{
			char c = base.SkipSpaces();
			if (base.ContinuesWithSensitive(AttributeNames.Standalone))
			{
				base.Advance(9);
				return this.DeclarationStandaloneAfterName(decl);
			}
			return this.DeclarationEnd(c, decl);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000E269 File Offset: 0x0000C469
		private XmlToken DeclarationStandaloneAfterName(XmlDeclarationToken decl)
		{
			if (base.SkipSpaces() != '=')
			{
				throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
			}
			return this.DeclarationStandaloneBeforeValue(decl);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000E290 File Offset: 0x0000C490
		private XmlToken DeclarationStandaloneBeforeValue(XmlDeclarationToken decl)
		{
			char c = base.SkipSpaces();
			if (c != '"' && c != '\'')
			{
				throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
			}
			return this.DeclarationStandaloneValue(decl, c);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		private XmlToken DeclarationStandaloneValue(XmlDeclarationToken decl, char quote)
		{
			for (char c = base.GetNext(); c != quote; c = base.GetNext())
			{
				if (c == '\uffff')
				{
					throw XmlParseError.EOF.At(base.GetCurrentPosition());
				}
				base.StringBuffer.Append(c);
			}
			string text = base.FlushBuffer();
			if (text.Is(Keywords.Yes))
			{
				decl.Standalone = true;
			}
			else
			{
				if (!text.Is(Keywords.No))
				{
					throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
				}
				decl.Standalone = false;
			}
			return this.DeclarationEnd(base.GetNext(), decl);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000E35C File Offset: 0x0000C55C
		private XmlDeclarationToken DeclarationEnd(char c, XmlDeclarationToken decl)
		{
			while (c.IsSpaceCharacter())
			{
				c = base.GetNext();
			}
			if (c != '?' || base.GetNext() != '>')
			{
				throw XmlParseError.XmlDeclarationInvalid.At(base.GetCurrentPosition());
			}
			return decl;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000E391 File Offset: 0x0000C591
		private XmlToken Doctype()
		{
			if (base.GetNext().IsSpaceCharacter())
			{
				return this.DoctypeNameBefore();
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000E3B4 File Offset: 0x0000C5B4
		private XmlToken DoctypeNameBefore()
		{
			char c = base.SkipSpaces();
			if (c.IsXmlNameStart())
			{
				base.StringBuffer.Append(c);
				return this.DoctypeName(this.NewDoctype());
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		private XmlToken DoctypeName(XmlDoctypeToken doctype)
		{
			char c = base.GetNext();
			while (c.IsXmlName())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			doctype.Name = base.FlushBuffer();
			if (c == '>')
			{
				return doctype;
			}
			if (c.IsSpaceCharacter())
			{
				return this.DoctypeNameAfter(doctype);
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000E45C File Offset: 0x0000C65C
		private XmlToken DoctypeNameAfter(XmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '>')
			{
				return doctype;
			}
			if (base.ContinuesWithSensitive(Keywords.Public))
			{
				base.Advance(5);
				return this.DoctypePublic(doctype);
			}
			if (base.ContinuesWithSensitive(Keywords.System))
			{
				base.Advance(5);
				return this.DoctypeSystem(doctype);
			}
			if (c == '[')
			{
				base.Advance();
				return this.DoctypeAfter(base.GetNext(), doctype);
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
		private XmlToken DoctypePublic(XmlDoctypeToken doctype)
		{
			char c = base.GetNext();
			if (c.IsSpaceCharacter())
			{
				c = base.SkipSpaces();
				if (c == '"' || c == '\'')
				{
					doctype.PublicIdentifier = string.Empty;
					return this.DoctypePublicIdentifierValue(doctype, c);
				}
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000E528 File Offset: 0x0000C728
		private XmlToken DoctypePublicIdentifierValue(XmlDoctypeToken doctype, char quote)
		{
			for (char c = base.GetNext(); c != quote; c = base.GetNext())
			{
				if (!c.IsPubidChar())
				{
					throw XmlParseError.XmlInvalidPubId.At(base.GetCurrentPosition());
				}
				base.StringBuffer.Append(c);
			}
			doctype.PublicIdentifier = base.FlushBuffer();
			return this.DoctypePublicIdentifierAfter(doctype);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000E584 File Offset: 0x0000C784
		private XmlToken DoctypePublicIdentifierAfter(XmlDoctypeToken doctype)
		{
			char next = base.GetNext();
			if (next == '>')
			{
				return doctype;
			}
			if (next.IsSpaceCharacter())
			{
				return this.DoctypeBetween(doctype);
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		private XmlToken DoctypeBetween(XmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '>')
			{
				return doctype;
			}
			if (c == '"' || c == '\'')
			{
				doctype.SystemIdentifier = string.Empty;
				return this.DoctypeSystemIdentifierValue(doctype, c);
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000E604 File Offset: 0x0000C804
		private XmlToken DoctypeSystem(XmlDoctypeToken doctype)
		{
			char c = base.GetNext();
			if (c.IsSpaceCharacter())
			{
				c = base.SkipSpaces();
				if (c == '"' || c == '\'')
				{
					doctype.SystemIdentifier = string.Empty;
					return this.DoctypeSystemIdentifierValue(doctype, c);
				}
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000E654 File Offset: 0x0000C854
		private XmlToken DoctypeSystemIdentifierValue(XmlDoctypeToken doctype, char quote)
		{
			for (char c = base.GetNext(); c != quote; c = base.GetNext())
			{
				if (c == '\uffff')
				{
					throw XmlParseError.EOF.At(base.GetCurrentPosition());
				}
				base.StringBuffer.Append(c);
			}
			doctype.SystemIdentifier = base.FlushBuffer();
			return this.DoctypeSystemIdentifierAfter(doctype);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		private XmlToken DoctypeSystemIdentifierAfter(XmlDoctypeToken doctype)
		{
			char c = base.SkipSpaces();
			if (c == '[')
			{
				base.Advance();
				c = base.GetNext();
			}
			return this.DoctypeAfter(c, doctype);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000E6DA File Offset: 0x0000C8DA
		private XmlToken DoctypeAfter(char c, XmlDoctypeToken doctype)
		{
			while (c.IsSpaceCharacter())
			{
				c = base.GetNext();
			}
			if (c == '>')
			{
				return doctype;
			}
			throw XmlParseError.DoctypeInvalid.At(base.GetCurrentPosition());
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000E704 File Offset: 0x0000C904
		private XmlToken AttributeBeforeName(XmlTagToken tag)
		{
			char c = base.SkipSpaces();
			if (c == '/')
			{
				return this.TagSelfClosing(tag);
			}
			if (c == '>')
			{
				return tag;
			}
			if (c == '\uffff')
			{
				throw XmlParseError.EOF.At(base.GetCurrentPosition());
			}
			if (c.IsXmlNameStart())
			{
				base.StringBuffer.Append(c);
				return this.AttributeName(tag);
			}
			throw XmlParseError.XmlInvalidAttribute.At(base.GetCurrentPosition());
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000E770 File Offset: 0x0000C970
		private XmlToken AttributeName(XmlTagToken tag)
		{
			char c = base.GetNext();
			while (c.IsXmlName())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			string text = base.FlushBuffer();
			if (!string.IsNullOrEmpty(tag.GetAttribute(text)))
			{
				throw XmlParseError.XmlUniqueAttribute.At(base.GetCurrentPosition());
			}
			tag.AddAttribute(text);
			if (c.IsSpaceCharacter())
			{
				do
				{
					c = base.GetNext();
				}
				while (c.IsSpaceCharacter());
			}
			if (c != '=')
			{
				throw XmlParseError.XmlInvalidAttribute.At(base.GetCurrentPosition());
			}
			return this.AttributeBeforeValue(tag);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000E804 File Offset: 0x0000CA04
		private XmlToken AttributeBeforeValue(XmlTagToken tag)
		{
			char c = base.SkipSpaces();
			if (c != '"' && c != '\'')
			{
				throw XmlParseError.XmlInvalidAttribute.At(base.GetCurrentPosition());
			}
			return this.AttributeValue(tag, c);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E83C File Offset: 0x0000CA3C
		private XmlToken AttributeValue(XmlTagToken tag, char quote)
		{
			for (char c = base.GetNext(); c != quote; c = base.GetNext())
			{
				if (c == '\uffff')
				{
					throw XmlParseError.EOF.At(base.GetCurrentPosition());
				}
				if (c == '<')
				{
					throw XmlParseError.XmlLtInAttributeValue.At(base.GetCurrentPosition());
				}
				if (c == '&')
				{
					base.StringBuffer.Append(this.CharacterReference());
				}
				else
				{
					base.StringBuffer.Append(c);
				}
			}
			tag.SetAttributeValue(base.FlushBuffer());
			return this.AttributeAfterValue(tag);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		private XmlToken AttributeAfterValue(XmlTagToken tag)
		{
			char next = base.GetNext();
			if (next.IsSpaceCharacter())
			{
				return this.AttributeBeforeName(tag);
			}
			if (next == '/')
			{
				return this.TagSelfClosing(tag);
			}
			if (next == '>')
			{
				return tag;
			}
			throw XmlParseError.XmlInvalidAttribute.At(base.GetCurrentPosition());
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E90C File Offset: 0x0000CB0C
		private XmlToken ProcessingStart(char c)
		{
			if (c.IsXmlNameStart())
			{
				base.StringBuffer.Append(c);
				return this.ProcessingTarget(base.GetNext(), this.NewProcessing());
			}
			throw XmlParseError.XmlInvalidPI.At(base.GetCurrentPosition());
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E948 File Offset: 0x0000CB48
		private XmlToken ProcessingTarget(char c, XmlPIToken pi)
		{
			while (c.IsXmlName())
			{
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			pi.Target = base.FlushBuffer();
			if (pi.Target.Isi(TagNames.Xml))
			{
				throw XmlParseError.XmlInvalidPI.At(base.GetCurrentPosition());
			}
			if (c == '?')
			{
				c = base.GetNext();
				if (c == '>')
				{
					return pi;
				}
			}
			else if (c.IsSpaceCharacter())
			{
				return this.ProcessingContent(pi);
			}
			throw XmlParseError.XmlInvalidPI.At(base.GetCurrentPosition());
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E9D8 File Offset: 0x0000CBD8
		private XmlToken ProcessingContent(XmlPIToken pi)
		{
			char c = base.GetNext();
			while (c != '\uffff')
			{
				if (c == '?')
				{
					c = base.GetNext();
					if (c == '>')
					{
						pi.Content = base.FlushBuffer();
						return pi;
					}
					base.StringBuffer.Append('?');
				}
				else
				{
					base.StringBuffer.Append(c);
					c = base.GetNext();
				}
			}
			throw XmlParseError.EOF.At(base.GetCurrentPosition());
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000EA45 File Offset: 0x0000CC45
		private XmlToken CommentStart()
		{
			return this.Comment(base.GetNext());
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000EA53 File Offset: 0x0000CC53
		private XmlToken Comment(char c)
		{
			while (c.IsXmlChar())
			{
				if (c == '-')
				{
					return this.CommentDash();
				}
				base.StringBuffer.Append(c);
				c = base.GetNext();
			}
			throw XmlParseError.XmlInvalidComment.At(base.GetCurrentPosition());
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000EA90 File Offset: 0x0000CC90
		private XmlToken CommentDash()
		{
			char next = base.GetNext();
			if (next == '-')
			{
				return this.CommentEnd();
			}
			return this.Comment(next);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000EAB7 File Offset: 0x0000CCB7
		private XmlToken CommentEnd()
		{
			if (base.GetNext() == '>')
			{
				return this.NewComment();
			}
			throw XmlParseError.XmlInvalidComment.At(base.GetCurrentPosition());
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000EADA File Offset: 0x0000CCDA
		private XmlEndOfFileToken NewEof()
		{
			return new XmlEndOfFileToken(base.GetCurrentPosition());
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		private XmlCharacterToken NewCharacters()
		{
			string text = base.FlushBuffer();
			return new XmlCharacterToken(this._position, text);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000EB08 File Offset: 0x0000CD08
		private XmlCommentToken NewComment()
		{
			string text = base.FlushBuffer();
			return new XmlCommentToken(this._position, text);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000EB28 File Offset: 0x0000CD28
		private XmlPIToken NewProcessing()
		{
			return new XmlPIToken(this._position);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000EB35 File Offset: 0x0000CD35
		private XmlDoctypeToken NewDoctype()
		{
			return new XmlDoctypeToken(this._position);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000EB42 File Offset: 0x0000CD42
		private XmlDeclarationToken NewDeclaration()
		{
			return new XmlDeclarationToken(this._position);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000EB4F File Offset: 0x0000CD4F
		private XmlTagToken NewOpenTag()
		{
			return new XmlTagToken(XmlTokenType.StartTag, this._position);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000EB5D File Offset: 0x0000CD5D
		private XmlTagToken NewCloseTag()
		{
			return new XmlTagToken(XmlTokenType.EndTag, this._position);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000EB6C File Offset: 0x0000CD6C
		private XmlCDataToken NewCharacterData()
		{
			string text = base.FlushBuffer();
			return new XmlCDataToken(this._position, text);
		}

		// Token: 0x0400020B RID: 523
		private readonly IEntityProvider _resolver;

		// Token: 0x0400020C RID: 524
		private TextPosition _position;
	}
}
