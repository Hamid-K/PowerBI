using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Html;
using AngleSharp.Dom.Mathml;
using AngleSharp.Dom.Svg;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Services;

namespace AngleSharp.Parser.Html
{
	// Token: 0x02000069 RID: 105
	internal sealed class HtmlDomBuilder
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public HtmlDomBuilder(HtmlDocument document)
		{
			IConfiguration options = document.Options;
			IBrowsingContext context = document.Context;
			IEntityProvider entityProvider = options.GetProvider<IEntityProvider>() ?? HtmlEntityService.Resolver;
			this._tokenizer = new HtmlTokenizer(document.Source, entityProvider);
			this._tokenizer.Error += delegate(object _, HtmlErrorEvent error)
			{
				context.Fire(error);
			};
			this._document = document;
			this._openElements = new List<Element>();
			this._templateModes = new Stack<HtmlTreeMode>();
			this._formattingElements = new List<Element>();
			this._frameset = true;
			this._currentMode = HtmlTreeMode.Initial;
			this._htmlFactory = options.GetFactory<IElementFactory<HtmlElement>>();
			this._mathFactory = options.GetFactory<IElementFactory<MathElement>>();
			this._svgFactory = options.GetFactory<IElementFactory<SvgElement>>();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000EF73 File Offset: 0x0000D173
		public bool IsFragmentCase
		{
			get
			{
				return this._fragmentContext != null;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000EF7E File Offset: 0x0000D17E
		public Element AdjustedCurrentNode
		{
			get
			{
				if (this._fragmentContext == null || this._openElements.Count != 1)
				{
					return this.CurrentNode;
				}
				return this._fragmentContext;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000EFA3 File Offset: 0x0000D1A3
		public Element CurrentNode
		{
			get
			{
				if (this._openElements.Count <= 0)
				{
					return null;
				}
				return this._openElements[this._openElements.Count - 1];
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000EFD0 File Offset: 0x0000D1D0
		public async Task<HtmlDocument> ParseAsync(HtmlParserOptions options, CancellationToken cancelToken)
		{
			TextSource source = this._document.Source;
			HtmlToken token = null;
			this._tokenizer.IsStrictMode = options.IsStrictMode;
			this._options = options;
			do
			{
				if (source.Length - source.Index < 1024)
				{
					await source.PrefetchAsync(8192, cancelToken).ConfigureAwait(false);
				}
				token = this._tokenizer.Get();
				this.Consume(token);
				if (this._waiting != null)
				{
					await this._waiting.ConfigureAwait(false);
					this._waiting = null;
				}
			}
			while (token.Type != HtmlTokenType.EndOfFile);
			return this._document;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000F028 File Offset: 0x0000D228
		public HtmlDocument Parse(HtmlParserOptions options)
		{
			this._tokenizer.IsStrictMode = options.IsStrictMode;
			this._options = options;
			HtmlToken htmlToken;
			do
			{
				htmlToken = this._tokenizer.Get();
				this.Consume(htmlToken);
				Task waiting = this._waiting;
				if (waiting != null)
				{
					waiting.Wait();
				}
				this._waiting = null;
			}
			while (htmlToken.Type != HtmlTokenType.EndOfFile);
			return this._document;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000F08C File Offset: 0x0000D28C
		public HtmlDocument ParseFragment(HtmlParserOptions options, Element context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			this._fragmentContext = context;
			string localName = context.LocalName;
			if (localName.IsOneOf(TagNames.Title, TagNames.Textarea))
			{
				this._tokenizer.State = HtmlParseMode.RCData;
			}
			else if (localName.IsOneOf(TagNames.Style, TagNames.Xmp, TagNames.Iframe, TagNames.NoEmbed, TagNames.NoFrames))
			{
				this._tokenizer.State = HtmlParseMode.Rawtext;
			}
			else if (localName.Is(TagNames.Script))
			{
				this._tokenizer.State = HtmlParseMode.Script;
			}
			else if (localName.Is(TagNames.Plaintext))
			{
				this._tokenizer.State = HtmlParseMode.Plaintext;
			}
			else if (localName.Is(TagNames.NoScript) && options.IsScripting)
			{
				this._tokenizer.State = HtmlParseMode.Rawtext;
			}
			HtmlHtmlElement htmlHtmlElement = new HtmlHtmlElement(this._document, null);
			this._document.AddNode(htmlHtmlElement);
			this._openElements.Add(htmlHtmlElement);
			if (context is HtmlTemplateElement)
			{
				this._templateModes.Push(HtmlTreeMode.InTemplate);
			}
			this.Reset();
			this._tokenizer.IsAcceptingCharacterData = (this.AdjustedCurrentNode.Flags & NodeFlags.HtmlMember) != NodeFlags.HtmlMember;
			while (!(context is HtmlFormElement))
			{
				context = context.ParentElement as Element;
				if (context == null)
				{
					IL_0152:
					return this.Parse(options);
				}
			}
			this._currentFormElement = (HtmlFormElement)context;
			goto IL_0152;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000F1F4 File Offset: 0x0000D3F4
		private void Restart()
		{
			this._currentMode = HtmlTreeMode.Initial;
			this._tokenizer.State = HtmlParseMode.PCData;
			this._document.ReplaceAll(null, true);
			this._frameset = true;
			this._openElements.Clear();
			this._formattingElements.Clear();
			this._templateModes.Clear();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000F24C File Offset: 0x0000D44C
		private void Reset()
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				bool flag = i == 0;
				if (flag && this._fragmentContext != null)
				{
					element = this._fragmentContext;
				}
				HtmlTreeMode? htmlTreeMode = element.SelectMode(flag, this._templateModes);
				if (htmlTreeMode != null)
				{
					this._currentMode = htmlTreeMode.Value;
					return;
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000F2BC File Offset: 0x0000D4BC
		private void Consume(HtmlToken token)
		{
			Element adjustedCurrentNode = this.AdjustedCurrentNode;
			if (adjustedCurrentNode == null || token.Type == HtmlTokenType.EndOfFile || (adjustedCurrentNode.Flags & NodeFlags.HtmlMember) == NodeFlags.HtmlMember || ((adjustedCurrentNode.Flags & NodeFlags.HtmlTip) == NodeFlags.HtmlTip && token.IsHtmlCompatible) || ((adjustedCurrentNode.Flags & NodeFlags.MathTip) == NodeFlags.MathTip && token.IsMathCompatible) || ((adjustedCurrentNode.Flags & NodeFlags.MathMember) == NodeFlags.MathMember && token.IsSvg && adjustedCurrentNode.LocalName.Is(TagNames.AnnotationXml)))
			{
				this.Home(token);
				return;
			}
			this.Foreign(token);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000F364 File Offset: 0x0000D564
		private void Home(HtmlToken token)
		{
			switch (this._currentMode)
			{
			case HtmlTreeMode.Initial:
				this.Initial(token);
				return;
			case HtmlTreeMode.BeforeHtml:
				this.BeforeHtml(token);
				return;
			case HtmlTreeMode.BeforeHead:
				this.BeforeHead(token);
				return;
			case HtmlTreeMode.InHead:
				this.InHead(token);
				return;
			case HtmlTreeMode.InHeadNoScript:
				this.InHeadNoScript(token);
				return;
			case HtmlTreeMode.AfterHead:
				this.AfterHead(token);
				return;
			case HtmlTreeMode.InBody:
				this.InBody(token);
				return;
			case HtmlTreeMode.Text:
				this.Text(token);
				return;
			case HtmlTreeMode.InTable:
				this.InTable(token);
				return;
			case HtmlTreeMode.InCaption:
				this.InCaption(token);
				return;
			case HtmlTreeMode.InColumnGroup:
				this.InColumnGroup(token);
				return;
			case HtmlTreeMode.InTableBody:
				this.InTableBody(token);
				return;
			case HtmlTreeMode.InRow:
				this.InRow(token);
				return;
			case HtmlTreeMode.InCell:
				this.InCell(token);
				return;
			case HtmlTreeMode.InSelect:
				this.InSelect(token);
				return;
			case HtmlTreeMode.InSelectInTable:
				this.InSelectInTable(token);
				return;
			case HtmlTreeMode.InTemplate:
				this.InTemplate(token);
				return;
			case HtmlTreeMode.AfterBody:
				this.AfterBody(token);
				return;
			case HtmlTreeMode.InFrameset:
				this.InFrameset(token);
				return;
			case HtmlTreeMode.AfterFrameset:
				this.AfterFrameset(token);
				return;
			case HtmlTreeMode.AfterAfterBody:
				this.AfterAfterBody(token);
				return;
			case HtmlTreeMode.AfterAfterFrameset:
				this.AfterAfterFrameset(token);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000F488 File Offset: 0x0000D688
		private void Initial(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
			{
				HtmlDoctypeToken htmlDoctypeToken = (HtmlDoctypeToken)token;
				if (!htmlDoctypeToken.IsValid)
				{
					this.RaiseErrorOccurred(HtmlParseError.DoctypeInvalid, token);
				}
				this._document.AddNode(new DocumentType(this._document, htmlDoctypeToken.Name ?? string.Empty)
				{
					SystemIdentifier = htmlDoctypeToken.SystemIdentifier,
					PublicIdentifier = htmlDoctypeToken.PublicIdentifier
				});
				this._document.QuirksMode = htmlDoctypeToken.GetQuirksMode();
				this._currentMode = HtmlTreeMode.BeforeHtml;
				return;
			}
			case HtmlTokenType.Comment:
				this._document.AddComment(token);
				return;
			case HtmlTokenType.Character:
				token.TrimStart();
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			if (!this._options.IsEmbedded)
			{
				this.RaiseErrorOccurred(HtmlParseError.DoctypeMissing, token);
				this._document.QuirksMode = QuirksMode.On;
			}
			this._currentMode = HtmlTreeMode.BeforeHtml;
			this.BeforeHtml(token);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000F578 File Offset: 0x0000D778
		private void BeforeHtml(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
				if (token.Name.Is(TagNames.Html))
				{
					this.AddRoot(token.AsTag());
					this._currentMode = HtmlTreeMode.BeforeHead;
					return;
				}
				break;
			case HtmlTokenType.EndTag:
				if (!TagNames.AllBeforeHead.Contains(token.Name))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this._document.AddComment(token);
				return;
			case HtmlTokenType.Character:
				token.TrimStart();
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			this.BeforeHtml(HtmlTagToken.Open(TagNames.Html));
			this.BeforeHead(token);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000F628 File Offset: 0x0000D828
		private void BeforeHead(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.Head))
				{
					this.AddElement(new HtmlHeadElement(this._document, null), token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InHead;
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
				if (!TagNames.AllBeforeHead.Contains(token.Name))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
				token.TrimStart();
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			this.BeforeHead(HtmlTagToken.Open(TagNames.Head));
			this.InHead(token);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000F700 File Offset: 0x0000D900
		private void InHead(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.Meta))
				{
					HtmlMetaElement htmlMetaElement = new HtmlMetaElement(this._document, null);
					this.AddElement(htmlMetaElement, token.AsTag(), true);
					Encoding encoding = htmlMetaElement.GetEncoding();
					this.CloseCurrentNode();
					if (encoding != null)
					{
						try
						{
							this._document.Source.CurrentEncoding = encoding;
						}
						catch (NotSupportedException)
						{
							this.Restart();
						}
					}
					return;
				}
				if (TagNames.AllHeadBase.Contains(name))
				{
					this.AddElement(token.AsTag(), true);
					this.CloseCurrentNode();
					return;
				}
				if (name.Is(TagNames.Title))
				{
					this.RCDataAlgorithm(token.AsTag());
					return;
				}
				if (name.IsOneOf(TagNames.Style, TagNames.NoFrames) || (this._options.IsScripting && name.Is(TagNames.NoScript)))
				{
					this.RawtextAlgorithm(token.AsTag());
					return;
				}
				if (name.Is(TagNames.NoScript))
				{
					this.AddElement(token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InHeadNoScript;
					return;
				}
				if (name.Is(TagNames.Script))
				{
					HtmlScriptElement htmlScriptElement = new HtmlScriptElement(this._document, null, true, this.IsFragmentCase);
					this.AddElement(htmlScriptElement, token.AsTag(), false);
					this._tokenizer.State = HtmlParseMode.Script;
					this._previousMode = this._currentMode;
					this._currentMode = HtmlTreeMode.Text;
					return;
				}
				if (name.Is(TagNames.Head))
				{
					this.RaiseErrorOccurred(HtmlParseError.HeadTagMisplaced, token);
					return;
				}
				if (name.Is(TagNames.Template))
				{
					this.AddElement(new HtmlTemplateElement(this._document, null), token.AsTag(), false);
					this._formattingElements.AddScopeMarker();
					this._frameset = false;
					this._currentMode = HtmlTreeMode.InTemplate;
					this._templateModes.Push(HtmlTreeMode.InTemplate);
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
			{
				string name2 = token.Name;
				if (name2.Is(TagNames.Head))
				{
					this.CloseCurrentNode();
					this._currentMode = HtmlTreeMode.AfterHead;
					this._waiting = this._document.WaitForReadyAsync();
					return;
				}
				if (name2.Is(TagNames.Template))
				{
					if (this.TagCurrentlyOpen(TagNames.Template))
					{
						this.GenerateImpliedEndTags();
						if (!this.CurrentNode.LocalName.Is(TagNames.Template))
						{
							this.RaiseErrorOccurred(HtmlParseError.TagClosingMismatch, token);
						}
						this.CloseTemplate();
						return;
					}
					this.RaiseErrorOccurred(HtmlParseError.TagInappropriate, token);
					return;
				}
				else if (!name2.IsOneOf(TagNames.Html, TagNames.Body, TagNames.Br))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					return;
				}
				break;
			}
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			}
			this.CloseCurrentNode();
			this._currentMode = HtmlTreeMode.AfterHead;
			this.AfterHead(token);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		private void InHeadNoScript(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (TagNames.AllNoScript.Contains(name))
				{
					this.InHead(token);
					return;
				}
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.IsOneOf(TagNames.Head, TagNames.NoScript))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagInappropriate, token);
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
			{
				string name2 = token.Name;
				if (name2.Is(TagNames.NoScript))
				{
					this.CloseCurrentNode();
					this._currentMode = HtmlTreeMode.InHead;
					return;
				}
				if (!name2.Is(TagNames.Br))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					return;
				}
				break;
			}
			case HtmlTokenType.Comment:
				this.InHead(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
			this.CloseCurrentNode();
			this._currentMode = HtmlTreeMode.InHead;
			this.InHead(token);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000FB04 File Offset: 0x0000DD04
		private void AfterHead(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.Body))
				{
					this.AfterHeadStartTagBody(token.AsTag());
					return;
				}
				if (name.Is(TagNames.Frameset))
				{
					this.AddElement(new HtmlFrameSetElement(this._document, null), token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InFrameset;
					return;
				}
				if (TagNames.AllHeadNoTemplate.Contains(name))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagMustBeInHead, token);
					int count = this._openElements.Count;
					Element element = this._document.Head as Element;
					this._openElements.Add(element);
					this.InHead(token);
					this.CloseNode(element);
					return;
				}
				if (name.Is(TagNames.Head))
				{
					this.RaiseErrorOccurred(HtmlParseError.HeadTagMisplaced, token);
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
				if (!token.Name.IsOneOf(TagNames.Html, TagNames.Body, TagNames.Br))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			}
			this.AfterHeadStartTagBody(HtmlTagToken.Open(TagNames.Body));
			this._frameset = true;
			this.Home(token);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		private void InBodyStartTag(HtmlTagToken tag)
		{
			string name = tag.Name;
			if (name.Is(TagNames.Div))
			{
				if (this.IsInButtonScope())
				{
					this.InBodyEndTagParagraph(tag);
				}
				this.AddElement(tag, false);
				return;
			}
			if (name.Is(TagNames.A))
			{
				int num = this._formattingElements.Count - 1;
				while (num >= 0 && this._formattingElements[num] != null)
				{
					if (this._formattingElements[num].LocalName.Is(TagNames.A))
					{
						Element element = this._formattingElements[num];
						this.RaiseErrorOccurred(HtmlParseError.AnchorNested, tag);
						this.HeisenbergAlgorithm(HtmlTagToken.Close(TagNames.A));
						this.CloseNode(element);
						this._formattingElements.Remove(element);
						break;
					}
					num--;
				}
				this.ReconstructFormatting();
				HtmlAnchorElement htmlAnchorElement = new HtmlAnchorElement(this._document, null);
				this.AddElement(htmlAnchorElement, tag, false);
				this._formattingElements.AddFormatting(htmlAnchorElement);
				return;
			}
			if (name.Is(TagNames.Span))
			{
				this.ReconstructFormatting();
				this.AddElement(tag, false);
				return;
			}
			if (name.Is(TagNames.Li))
			{
				this.InBodyStartTagListItem(tag);
				return;
			}
			if (name.Is(TagNames.Img))
			{
				this.InBodyStartTagBreakrow(tag);
				return;
			}
			if (name.IsOneOf(TagNames.Ul, TagNames.P))
			{
				if (this.IsInButtonScope())
				{
					this.InBodyEndTagParagraph(tag);
				}
				this.AddElement(tag, false);
				return;
			}
			if (TagNames.AllSemanticFormatting.Contains(name))
			{
				this.ReconstructFormatting();
				this._formattingElements.AddFormatting(this.AddElement(tag, false));
				return;
			}
			if (name.Is(TagNames.Script))
			{
				this.InHead(tag);
				return;
			}
			if (TagNames.AllHeadings.Contains(name))
			{
				if (this.IsInButtonScope())
				{
					this.InBodyEndTagParagraph(tag);
				}
				if (TagNames.AllHeadings.Contains(this.CurrentNode.LocalName))
				{
					this.RaiseErrorOccurred(HtmlParseError.HeadingNested, tag);
					this.CloseCurrentNode();
				}
				this.AddElement(new HtmlHeadingElement(this._document, name, null), tag, false);
				return;
			}
			if (name.Is(TagNames.Input))
			{
				this.ReconstructFormatting();
				this.AddElement(new HtmlInputElement(this._document, null), tag, true);
				this.CloseCurrentNode();
				if (!tag.GetAttribute(AttributeNames.Type).Isi(AttributeNames.Hidden))
				{
					this._frameset = false;
					return;
				}
			}
			else if (name.Is(TagNames.Form))
			{
				if (this._currentFormElement == null)
				{
					if (this.IsInButtonScope())
					{
						this.InBodyEndTagParagraph(tag);
					}
					this._currentFormElement = new HtmlFormElement(this._document, null);
					this.AddElement(this._currentFormElement, tag, false);
					return;
				}
				this.RaiseErrorOccurred(HtmlParseError.FormAlreadyOpen, tag);
				return;
			}
			else
			{
				if (TagNames.AllBody.Contains(name))
				{
					if (this.IsInButtonScope())
					{
						this.InBodyEndTagParagraph(tag);
					}
					this.AddElement(tag, false);
					return;
				}
				if (TagNames.AllClassicFormatting.Contains(name))
				{
					this.ReconstructFormatting();
					this._formattingElements.AddFormatting(this.AddElement(tag, false));
					return;
				}
				if (TagNames.AllHead.Contains(name))
				{
					this.InHead(tag);
					return;
				}
				if (name.IsOneOf(TagNames.Pre, TagNames.Listing))
				{
					if (this.IsInButtonScope())
					{
						this.InBodyEndTagParagraph(tag);
					}
					this.AddElement(tag, false);
					this._frameset = false;
					this.PreventNewLine();
					return;
				}
				if (name.Is(TagNames.Button))
				{
					if (this.IsInScope(TagNames.Button))
					{
						this.RaiseErrorOccurred(HtmlParseError.ButtonInScope, tag);
						this.InBodyEndTagBlock(tag);
						this.InBody(tag);
						return;
					}
					this.ReconstructFormatting();
					this.AddElement(new HtmlButtonElement(this._document, null), tag, false);
					this._frameset = false;
					return;
				}
				else
				{
					if (name.Is(TagNames.Table))
					{
						if (this._document.QuirksMode != QuirksMode.On && this.IsInButtonScope())
						{
							this.InBodyEndTagParagraph(tag);
						}
						this.AddElement(new HtmlTableElement(this._document, null), tag, false);
						this._frameset = false;
						this._currentMode = HtmlTreeMode.InTable;
						return;
					}
					if (TagNames.AllBodyBreakrow.Contains(name))
					{
						this.InBodyStartTagBreakrow(tag);
						return;
					}
					if (TagNames.AllBodyClosed.Contains(name))
					{
						this.AddElement(tag, true);
						this.CloseCurrentNode();
						return;
					}
					if (name.Is(TagNames.Hr))
					{
						if (this.IsInButtonScope())
						{
							this.InBodyEndTagParagraph(tag);
						}
						this.AddElement(new HtmlHrElement(this._document, null), tag, true);
						this.CloseCurrentNode();
						this._frameset = false;
						return;
					}
					if (name.Is(TagNames.Textarea))
					{
						this.AddElement(new HtmlTextAreaElement(this._document, null), tag, false);
						this._tokenizer.State = HtmlParseMode.RCData;
						this._previousMode = this._currentMode;
						this._frameset = false;
						this._currentMode = HtmlTreeMode.Text;
						this.PreventNewLine();
						return;
					}
					if (name.Is(TagNames.Select))
					{
						this.ReconstructFormatting();
						this.AddElement(new HtmlSelectElement(this._document, null), tag, false);
						this._frameset = false;
						switch (this._currentMode)
						{
						case HtmlTreeMode.InTable:
						case HtmlTreeMode.InCaption:
						case HtmlTreeMode.InTableBody:
						case HtmlTreeMode.InRow:
						case HtmlTreeMode.InCell:
							this._currentMode = HtmlTreeMode.InSelectInTable;
							return;
						}
						this._currentMode = HtmlTreeMode.InSelect;
						return;
					}
					if (name.IsOneOf(TagNames.Optgroup, TagNames.Option))
					{
						if (this.CurrentNode.LocalName.Is(TagNames.Option))
						{
							this.InBodyEndTagAnythingElse(HtmlTagToken.Close(TagNames.Option));
						}
						this.ReconstructFormatting();
						this.AddElement(tag, false);
						return;
					}
					if (name.IsOneOf(TagNames.Dd, TagNames.Dt))
					{
						this.InBodyStartTagDefinitionItem(tag);
						return;
					}
					if (name.Is(TagNames.Iframe))
					{
						this._frameset = false;
						this.RawtextAlgorithm(tag);
						return;
					}
					if (TagNames.AllBodyObsolete.Contains(name))
					{
						this.ReconstructFormatting();
						this.AddElement(tag, false);
						this._formattingElements.AddScopeMarker();
						this._frameset = false;
						return;
					}
					if (name.Is(TagNames.Image))
					{
						this.RaiseErrorOccurred(HtmlParseError.ImageTagNamedWrong, tag);
						tag.Name = TagNames.Img;
						this.InBodyStartTagBreakrow(tag);
						return;
					}
					if (name.Is(TagNames.NoBr))
					{
						this.ReconstructFormatting();
						if (this.IsInScope(TagNames.NoBr))
						{
							this.RaiseErrorOccurred(HtmlParseError.NobrInScope, tag);
							this.HeisenbergAlgorithm(tag);
							this.ReconstructFormatting();
						}
						this._formattingElements.AddFormatting(this.AddElement(tag, false));
						return;
					}
					if (name.Is(TagNames.Xmp))
					{
						if (this.IsInButtonScope())
						{
							this.InBodyEndTagParagraph(tag);
						}
						this.ReconstructFormatting();
						this._frameset = false;
						this.RawtextAlgorithm(tag);
						return;
					}
					if (name.IsOneOf(TagNames.Rb, TagNames.Rtc))
					{
						if (this.IsInScope(TagNames.Ruby))
						{
							this.GenerateImpliedEndTags();
							if (!this.CurrentNode.LocalName.Is(TagNames.Ruby))
							{
								this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
							}
						}
						this.AddElement(tag, false);
						return;
					}
					if (name.IsOneOf(TagNames.Rp, TagNames.Rt))
					{
						if (this.IsInScope(TagNames.Ruby))
						{
							this.GenerateImpliedEndTagsExceptFor(TagNames.Rtc);
							if (!this.CurrentNode.LocalName.IsOneOf(TagNames.Ruby, TagNames.Rtc))
							{
								this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
							}
						}
						this.AddElement(tag, false);
						return;
					}
					if (name.Is(TagNames.NoEmbed))
					{
						this.RawtextAlgorithm(tag);
						return;
					}
					if (name.Is(TagNames.NoScript))
					{
						if (this._options.IsScripting)
						{
							this.RawtextAlgorithm(tag);
							return;
						}
						this.ReconstructFormatting();
						this.AddElement(tag, false);
						return;
					}
					else if (name.Is(TagNames.Math))
					{
						MathElement mathElement = new MathElement(this._document, name, null, NodeFlags.None);
						this.ReconstructFormatting();
						this.AddElement(mathElement.Setup(tag));
						if (tag.IsSelfClosing)
						{
							this.CloseNode(mathElement);
							return;
						}
					}
					else if (name.Is(TagNames.Svg))
					{
						SvgElement svgElement = new SvgElement(this._document, name, null, NodeFlags.None);
						this.ReconstructFormatting();
						this.AddElement(svgElement.Setup(tag));
						if (tag.IsSelfClosing)
						{
							this.CloseNode(svgElement);
							return;
						}
					}
					else
					{
						if (name.Is(TagNames.Plaintext))
						{
							if (this.IsInButtonScope())
							{
								this.InBodyEndTagParagraph(tag);
							}
							this.AddElement(tag, false);
							this._tokenizer.State = HtmlParseMode.Plaintext;
							return;
						}
						if (name.Is(TagNames.Frameset))
						{
							this.RaiseErrorOccurred(HtmlParseError.FramesetMisplaced, tag);
							if (this._openElements.Count != 1 && this._openElements[1].LocalName.Is(TagNames.Body) && this._frameset)
							{
								this._openElements[1].RemoveFromParent();
								while (this._openElements.Count > 1)
								{
									this.CloseCurrentNode();
								}
								this.AddElement(new HtmlFrameSetElement(this._document, null), tag, false);
								this._currentMode = HtmlTreeMode.InFrameset;
								return;
							}
						}
						else if (name.Is(TagNames.Html))
						{
							this.RaiseErrorOccurred(HtmlParseError.HtmlTagMisplaced, tag);
							if (this._templateModes.Count == 0)
							{
								this._openElements[0].SetUniqueAttributes(tag.Attributes);
								return;
							}
						}
						else if (name.Is(TagNames.Body))
						{
							this.RaiseErrorOccurred(HtmlParseError.BodyTagMisplaced, tag);
							if (this._templateModes.Count == 0 && this._openElements.Count > 1 && this._openElements[1].LocalName.Is(TagNames.Body))
							{
								this._frameset = false;
								this._openElements[1].SetUniqueAttributes(tag.Attributes);
								return;
							}
						}
						else if (name.Is(TagNames.IsIndex))
						{
							this.RaiseErrorOccurred(HtmlParseError.TagInappropriate, tag);
							if (this._currentFormElement == null)
							{
								this.InBody(HtmlTagToken.Open(TagNames.Form));
								if (tag.GetAttribute(AttributeNames.Action).Length > 0)
								{
									this._currentFormElement.SetAttribute(AttributeNames.Action, tag.GetAttribute(AttributeNames.Action));
								}
								this.InBody(HtmlTagToken.Open(TagNames.Hr));
								this.InBody(HtmlTagToken.Open(TagNames.Label));
								if (tag.GetAttribute(AttributeNames.Prompt).Length > 0)
								{
									this.AddCharacters(tag.GetAttribute(AttributeNames.Prompt));
								}
								else
								{
									this.AddCharacters("This is a searchable index. Enter search keywords: ");
								}
								HtmlTagToken htmlTagToken = HtmlTagToken.Open(TagNames.Input);
								htmlTagToken.AddAttribute(AttributeNames.Name, TagNames.IsIndex);
								for (int i = 0; i < tag.Attributes.Count; i++)
								{
									if (!tag.Attributes[i].Key.IsOneOf(AttributeNames.Name, AttributeNames.Action, AttributeNames.Prompt))
									{
										htmlTagToken.AddAttribute(tag.Attributes[i].Key, tag.Attributes[i].Value);
									}
								}
								this.InBody(htmlTagToken);
								this.InBody(HtmlTagToken.Close(TagNames.Label));
								this.InBody(HtmlTagToken.Open(TagNames.Hr));
								this.InBody(HtmlTagToken.Close(TagNames.Form));
								return;
							}
						}
						else
						{
							if (TagNames.AllNested.Contains(name))
							{
								this.RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, tag);
								return;
							}
							this.ReconstructFormatting();
							this.AddElement(tag, false);
						}
					}
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00010788 File Offset: 0x0000E988
		private void InBodyEndTag(HtmlTagToken tag)
		{
			string name = tag.Name;
			if (name.Is(TagNames.Div))
			{
				this.InBodyEndTagBlock(tag);
				return;
			}
			if (name.Is(TagNames.A))
			{
				this.HeisenbergAlgorithm(tag);
				return;
			}
			if (name.Is(TagNames.Li))
			{
				if (this.IsInListItemScope())
				{
					this.GenerateImpliedEndTagsExceptFor(name);
					if (!this.CurrentNode.LocalName.Is(TagNames.Li))
					{
						this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
					}
					this.ClearStackBackTo(TagNames.Li);
					this.CloseCurrentNode();
					return;
				}
				this.RaiseErrorOccurred(HtmlParseError.ListItemNotInScope, tag);
				return;
			}
			else
			{
				if (name.Is(TagNames.P))
				{
					this.InBodyEndTagParagraph(tag);
					return;
				}
				if (TagNames.AllBlocks.Contains(name))
				{
					this.InBodyEndTagBlock(tag);
					return;
				}
				if (TagNames.AllFormatting.Contains(name))
				{
					this.HeisenbergAlgorithm(tag);
					return;
				}
				if (name.Is(TagNames.Form))
				{
					HtmlFormElement currentFormElement = this._currentFormElement;
					this._currentFormElement = null;
					if (currentFormElement != null && this.IsInScope(currentFormElement.LocalName))
					{
						this.GenerateImpliedEndTags();
						if (this.CurrentNode != currentFormElement)
						{
							this.RaiseErrorOccurred(HtmlParseError.FormClosedWrong, tag);
						}
						this.CloseNode(currentFormElement);
						return;
					}
					this.RaiseErrorOccurred(HtmlParseError.FormNotInScope, tag);
					return;
				}
				else
				{
					if (name.Is(TagNames.Br))
					{
						this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, tag);
						this.InBodyStartTagBreakrow(HtmlTagToken.Open(TagNames.Br));
						return;
					}
					if (TagNames.AllHeadings.Contains(name))
					{
						if (this.IsInScope(TagNames.AllHeadings))
						{
							this.GenerateImpliedEndTags();
							if (!this.CurrentNode.LocalName.Is(name))
							{
								this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
							}
							this.ClearStackBackTo(TagNames.AllHeadings);
							this.CloseCurrentNode();
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.HeadingNotInScope, tag);
						return;
					}
					else if (name.IsOneOf(TagNames.Dd, TagNames.Dt))
					{
						if (this.IsInScope(name))
						{
							this.GenerateImpliedEndTagsExceptFor(name);
							if (!this.CurrentNode.LocalName.Is(name))
							{
								this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
							}
							this.ClearStackBackTo(name);
							this.CloseCurrentNode();
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.ListItemNotInScope, tag);
						return;
					}
					else if (name.IsOneOf(TagNames.Applet, TagNames.Marquee, TagNames.Object))
					{
						if (this.IsInScope(name))
						{
							this.GenerateImpliedEndTags();
							if (!this.CurrentNode.LocalName.Is(name))
							{
								this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
							}
							this.ClearStackBackTo(name);
							this.CloseCurrentNode();
							this._formattingElements.ClearFormatting();
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.ObjectNotInScope, tag);
						return;
					}
					else
					{
						if (name.Is(TagNames.Body))
						{
							this.InBodyEndTagBody(tag);
							return;
						}
						if (name.Is(TagNames.Html))
						{
							if (this.InBodyEndTagBody(tag))
							{
								this.AfterBody(tag);
								return;
							}
						}
						else
						{
							if (name.Is(TagNames.Template))
							{
								this.InHead(tag);
								return;
							}
							this.InBodyEndTagAnythingElse(tag);
						}
						return;
					}
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010A44 File Offset: 0x0000EC44
		private void InBody(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
				this.InBodyStartTag(token.AsTag());
				return;
			case HtmlTokenType.EndTag:
				this.InBodyEndTag(token.AsTag());
				return;
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
				this.ReconstructFormatting();
				this.AddCharacters(token.Data);
				this._frameset = !token.HasContent && this._frameset;
				return;
			case HtmlTokenType.EndOfFile:
				this.CheckBodyOnClosing(token);
				if (this._templateModes.Count != 0)
				{
					this.InTemplate(token);
					return;
				}
				this.End();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		private void Text(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.EndTag:
				if (!token.Name.Is(TagNames.Script))
				{
					this.CloseCurrentNode();
					this._currentMode = this._previousMode;
					return;
				}
				this.HandleScript(this.CurrentNode as HtmlScriptElement);
				return;
			case HtmlTokenType.Comment:
				return;
			case HtmlTokenType.Character:
				this.AddCharacters(token.Data);
				return;
			case HtmlTokenType.EndOfFile:
				this.RaiseErrorOccurred(HtmlParseError.EOF, token);
				this.CloseCurrentNode();
				this._currentMode = this._previousMode;
				this.Consume(token);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00010B88 File Offset: 0x0000ED88
		private void InTable(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Caption))
				{
					this.ClearStackBackTo(TagNames.Table);
					this._formattingElements.AddScopeMarker();
					this.AddElement(new HtmlTableCaptionElement(this._document, null), token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InCaption;
					return;
				}
				if (name.Is(TagNames.Colgroup))
				{
					this.ClearStackBackTo(TagNames.Table);
					this.AddElement(new HtmlTableColgroupElement(this._document, null), token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InColumnGroup;
					return;
				}
				if (name.Is(TagNames.Col))
				{
					this.InTable(HtmlTagToken.Open(TagNames.Colgroup));
					this.InColumnGroup(token);
					return;
				}
				if (TagNames.AllTableSections.Contains(name))
				{
					this.ClearStackBackTo(TagNames.Table);
					this.AddElement(new HtmlTableSectionElement(this._document, name, null), token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InTableBody;
					return;
				}
				if (TagNames.AllTableCellsRows.Contains(name))
				{
					this.InTable(HtmlTagToken.Open(TagNames.Tbody));
					this.InTableBody(token);
					return;
				}
				if (name.Is(TagNames.Table))
				{
					this.RaiseErrorOccurred(HtmlParseError.TableNesting, token);
					if (this.InTableEndTagTable(token))
					{
						this.Home(token);
						return;
					}
				}
				else if (name.Is(TagNames.Input))
				{
					HtmlTagToken htmlTagToken = token.AsTag();
					if (htmlTagToken.GetAttribute(AttributeNames.Type).Isi(AttributeNames.Hidden))
					{
						this.RaiseErrorOccurred(HtmlParseError.InputUnexpected, token);
						this.AddElement(new HtmlInputElement(this._document, null), htmlTagToken, true);
						this.CloseCurrentNode();
						return;
					}
					this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
					this.InBodyWithFoster(token);
					return;
				}
				else if (name.Is(TagNames.Form))
				{
					this.RaiseErrorOccurred(HtmlParseError.FormInappropriate, token);
					if (this._currentFormElement == null)
					{
						this._currentFormElement = new HtmlFormElement(this._document, null);
						this.AddElement(this._currentFormElement, token.AsTag(), false);
						this.CloseCurrentNode();
						return;
					}
				}
				else
				{
					if (TagNames.AllTableHead.Contains(name))
					{
						this.InHead(token);
						return;
					}
					this.RaiseErrorOccurred(HtmlParseError.IllegalElementInTableDetected, token);
					this.InBodyWithFoster(token);
				}
				return;
			}
			case HtmlTokenType.EndTag:
			{
				string name2 = token.Name;
				if (name2.Is(TagNames.Table))
				{
					this.InTableEndTagTable(token);
					return;
				}
				if (name2.Is(TagNames.Template))
				{
					this.InHead(token);
					return;
				}
				if (TagNames.AllTableSpecial.Contains(name2) || TagNames.AllTableInner.Contains(name2))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					return;
				}
				this.RaiseErrorOccurred(HtmlParseError.IllegalElementInTableDetected, token);
				this.InBodyWithFoster(token);
				return;
			}
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
				if (TagNames.AllTableMajor.Contains(this.CurrentNode.LocalName))
				{
					this.InTableText(token);
					return;
				}
				break;
			case HtmlTokenType.EndOfFile:
				this.InBody(token);
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
			this.InBodyWithFoster(token);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00010E88 File Offset: 0x0000F088
		private void InTableText(HtmlToken token)
		{
			if (token.HasContent)
			{
				this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
				this.InBodyWithFoster(token);
				return;
			}
			this.AddCharacters(token.Data);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		private void InCaption(HtmlToken token)
		{
			HtmlTokenType type = token.Type;
			if (type != HtmlTokenType.StartTag)
			{
				if (type == HtmlTokenType.EndTag)
				{
					string name = token.Name;
					if (name.Is(TagNames.Caption))
					{
						this.InCaptionEndTagCaption(token);
						return;
					}
					if (TagNames.AllCaptionStart.Contains(name))
					{
						this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
						return;
					}
					if (name.Is(TagNames.Table))
					{
						this.RaiseErrorOccurred(HtmlParseError.TableNesting, token);
						if (this.InCaptionEndTagCaption(token))
						{
							this.InTable(token);
						}
						return;
					}
				}
			}
			else
			{
				string name2 = token.Name;
				if (TagNames.AllCaptionEnd.Contains(name2))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, token);
					if (this.InCaptionEndTagCaption(token))
					{
						this.InTable(token);
					}
					return;
				}
			}
			this.InBody(token);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010F68 File Offset: 0x0000F168
		private void InColumnGroup(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.Col))
				{
					this.AddElement(new HtmlTableColElement(this._document, null), token.AsTag(), true);
					this.CloseCurrentNode();
					return;
				}
				if (name.Is(TagNames.Template))
				{
					this.InHead(token);
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
			{
				string name2 = token.Name;
				if (name2.Is(TagNames.Colgroup))
				{
					this.InColumnGroupEndTagColgroup(token);
					return;
				}
				if (name2.Is(TagNames.Col))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong, token);
					return;
				}
				if (name2.Is(TagNames.Template))
				{
					this.InHead(token);
					return;
				}
				break;
			}
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			case HtmlTokenType.EndOfFile:
				this.InBody(token);
				return;
			}
			if (this.InColumnGroupEndTagColgroup(token))
			{
				this.InTable(token);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00011090 File Offset: 0x0000F290
		private void InTableBody(HtmlToken token)
		{
			HtmlTokenType type = token.Type;
			if (type != HtmlTokenType.StartTag)
			{
				if (type == HtmlTokenType.EndTag)
				{
					string name = token.Name;
					if (TagNames.AllTableSections.Contains(name))
					{
						if (this.IsInTableScope(name))
						{
							this.ClearStackBackTo(TagNames.AllTableSections);
							this.CloseCurrentNode();
							this._currentMode = HtmlTreeMode.InTable;
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, token);
						return;
					}
					else
					{
						if (name.Is(TagNames.Tr) || TagNames.AllTableSpecial.Contains(name))
						{
							this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
							return;
						}
						if (name.Is(TagNames.Table))
						{
							this.InTableBodyCloseTable(token.AsTag());
							return;
						}
					}
				}
			}
			else
			{
				string name2 = token.Name;
				if (name2.Is(TagNames.Tr))
				{
					this.ClearStackBackTo(TagNames.AllTableSections);
					this.AddElement(new HtmlTableRowElement(this._document, null), token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InRow;
					return;
				}
				if (TagNames.AllTableCells.Contains(name2))
				{
					this.InTableBody(HtmlTagToken.Open(TagNames.Tr));
					this.InRow(token);
					return;
				}
				if (TagNames.AllTableGeneral.Contains(name2))
				{
					this.InTableBodyCloseTable(token.AsTag());
					return;
				}
			}
			this.InTable(token);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000111BC File Offset: 0x0000F3BC
		private void InRow(HtmlToken token)
		{
			HtmlTokenType type = token.Type;
			if (type != HtmlTokenType.StartTag)
			{
				if (type == HtmlTokenType.EndTag)
				{
					string name = token.Name;
					if (name.Is(TagNames.Tr))
					{
						this.InRowEndTagTablerow(token);
						return;
					}
					if (name.Is(TagNames.Table))
					{
						if (this.InRowEndTagTablerow(token))
						{
							this.InTableBody(token);
							return;
						}
					}
					else if (TagNames.AllTableSections.Contains(name))
					{
						if (this.IsInTableScope(name))
						{
							this.InRowEndTagTablerow(token);
							this.InTableBody(token);
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, token);
						return;
					}
					else
					{
						if (!TagNames.AllTableSpecial.Contains(name))
						{
							goto IL_0105;
						}
						this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
					}
					return;
				}
			}
			else
			{
				string name2 = token.Name;
				if (TagNames.AllTableCells.Contains(name2))
				{
					this.ClearStackBackTo(TagNames.Tr);
					this.AddElement(token.AsTag(), false);
					this._currentMode = HtmlTreeMode.InCell;
					this._formattingElements.AddScopeMarker();
					return;
				}
				if (name2.Is(TagNames.Tr) || TagNames.AllTableGeneral.Contains(name2))
				{
					if (this.InRowEndTagTablerow(token))
					{
						this.InTableBody(token);
					}
					return;
				}
			}
			IL_0105:
			this.InTable(token);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000112D8 File Offset: 0x0000F4D8
		private void InCell(HtmlToken token)
		{
			HtmlTokenType type = token.Type;
			if (type != HtmlTokenType.StartTag)
			{
				if (type == HtmlTokenType.EndTag)
				{
					string name = token.Name;
					if (TagNames.AllTableCells.Contains(name))
					{
						this.InCellEndTagCell(token);
						return;
					}
					if (TagNames.AllTableCore.Contains(name))
					{
						if (this.IsInTableScope(name))
						{
							this.InCellEndTagCell(token);
							this.Home(token);
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.TableNotInScope, token);
						return;
					}
					else
					{
						if (!TagNames.AllTableSpecial.Contains(name))
						{
							this.InBody(token);
							return;
						}
						this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
						return;
					}
				}
			}
			else
			{
				string name2 = token.Name;
				if (TagNames.AllTableCellsRows.Contains(name2) || TagNames.AllTableGeneral.Contains(name2))
				{
					if (this.IsInTableScope(TagNames.AllTableCells))
					{
						this.InCellEndTagCell(token);
						this.Home(token);
						return;
					}
					this.RaiseErrorOccurred(HtmlParseError.TableCellNotInScope, token);
					return;
				}
			}
			this.InBody(token);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000113B8 File Offset: 0x0000F5B8
		private void InSelect(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.Option))
				{
					if (this.CurrentNode.LocalName.Is(TagNames.Option))
					{
						this.InSelectEndTagOption(token);
					}
					this.AddElement(new HtmlOptionElement(this._document, null), token.AsTag(), false);
					return;
				}
				if (name.Is(TagNames.Optgroup))
				{
					if (this.CurrentNode.LocalName.Is(TagNames.Option))
					{
						this.InSelectEndTagOption(token);
					}
					if (this.CurrentNode.LocalName.Is(TagNames.Optgroup))
					{
						this.InSelectEndTagOptgroup(token);
					}
					this.AddElement(new HtmlOptionsGroupElement(this._document, null), token.AsTag(), false);
					return;
				}
				if (name.Is(TagNames.Select))
				{
					this.RaiseErrorOccurred(HtmlParseError.SelectNesting, token);
					this.InSelectEndTagSelect();
					return;
				}
				if (TagNames.AllInput.Contains(name))
				{
					this.RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, token);
					if (this.IsInSelectScope(TagNames.Select))
					{
						this.InSelectEndTagSelect();
						this.Home(token);
						return;
					}
				}
				else
				{
					if (name.IsOneOf(TagNames.Template, TagNames.Script))
					{
						this.InHead(token);
						return;
					}
					this.RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, token);
				}
				return;
			}
			case HtmlTokenType.EndTag:
			{
				string name2 = token.Name;
				if (name2.Is(TagNames.Template))
				{
					this.InHead(token);
					return;
				}
				if (name2.Is(TagNames.Optgroup))
				{
					this.InSelectEndTagOptgroup(token);
					return;
				}
				if (name2.Is(TagNames.Option))
				{
					this.InSelectEndTagOption(token);
					return;
				}
				if (name2.Is(TagNames.Select) && this.IsInSelectScope(TagNames.Select))
				{
					this.InSelectEndTagSelect();
					return;
				}
				if (name2.Is(TagNames.Select))
				{
					this.RaiseErrorOccurred(HtmlParseError.SelectNotInScope, token);
					return;
				}
				this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
				return;
			}
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
				this.AddCharacters(token.Data);
				return;
			case HtmlTokenType.EndOfFile:
				this.InBody(token);
				return;
			default:
				this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
				return;
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000115F0 File Offset: 0x0000F7F0
		private void InSelectInTable(HtmlToken token)
		{
			HtmlTokenType type = token.Type;
			if (type != HtmlTokenType.StartTag)
			{
				if (type == HtmlTokenType.EndTag)
				{
					string name = token.Name;
					if (TagNames.AllTableSelects.Contains(name))
					{
						this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
						if (this.IsInTableScope(name))
						{
							this.InSelectEndTagSelect();
							this.Home(token);
						}
						return;
					}
				}
			}
			else
			{
				string name2 = token.Name;
				if (TagNames.AllTableSelects.Contains(name2))
				{
					this.RaiseErrorOccurred(HtmlParseError.IllegalElementInSelectDetected, token);
					this.InSelectEndTagSelect();
					this.Home(token);
					return;
				}
			}
			this.InSelect(token);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00011678 File Offset: 0x0000F878
		private void InTemplate(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Script) || TagNames.AllHead.Contains(name))
				{
					this.InHead(token);
					return;
				}
				if (TagNames.AllTableRoot.Contains(name))
				{
					this.TemplateStep(token, HtmlTreeMode.InTable);
					return;
				}
				if (name.Is(TagNames.Col))
				{
					this.TemplateStep(token, HtmlTreeMode.InColumnGroup);
					return;
				}
				if (name.Is(TagNames.Tr))
				{
					this.TemplateStep(token, HtmlTreeMode.InTableBody);
					return;
				}
				if (TagNames.AllTableCells.Contains(name))
				{
					this.TemplateStep(token, HtmlTreeMode.InRow);
					return;
				}
				this.TemplateStep(token, HtmlTreeMode.InBody);
				return;
			}
			case HtmlTokenType.EndTag:
				if (token.Name.Is(TagNames.Template))
				{
					this.InHead(token);
					return;
				}
				this.RaiseErrorOccurred(HtmlParseError.TagCannotEndHere, token);
				return;
			case HtmlTokenType.EndOfFile:
				if (this.TagCurrentlyOpen(TagNames.Template))
				{
					this.RaiseErrorOccurred(HtmlParseError.EOF, token);
					this.CloseTemplate();
					this.Home(token);
					return;
				}
				this.End();
				return;
			}
			this.InBody(token);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00011790 File Offset: 0x0000F990
		private void AfterBody(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
				if (token.Name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				break;
			case HtmlTokenType.EndTag:
				if (token.Name.Is(TagNames.Html))
				{
					if (this.IsFragmentCase)
					{
						this.RaiseErrorOccurred(HtmlParseError.TagInvalidInFragmentMode, token);
						return;
					}
					this._currentMode = HtmlTreeMode.AfterAfterBody;
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this._openElements[0].AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.ReconstructFormatting();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			case HtmlTokenType.EndOfFile:
				this.End();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
			this._currentMode = HtmlTreeMode.InBody;
			this.InBody(token);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001186C File Offset: 0x0000FA6C
		private void InFrameset(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.Frameset))
				{
					this.AddElement(new HtmlFrameSetElement(this._document, null), token.AsTag(), false);
					return;
				}
				if (name.Is(TagNames.Frame))
				{
					this.AddElement(new HtmlFrameElement(this._document, null), token.AsTag(), true);
					this.CloseCurrentNode();
					return;
				}
				if (name.Is(TagNames.NoFrames))
				{
					this.InHead(token);
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
				if (token.Name.Is(TagNames.Frameset))
				{
					if (this.CurrentNode != this._openElements[0])
					{
						this.CloseCurrentNode();
						if (!this.IsFragmentCase && !this.CurrentNode.LocalName.Is(TagNames.Frameset))
						{
							this._currentMode = HtmlTreeMode.AfterFrameset;
							return;
						}
					}
					else
					{
						this.RaiseErrorOccurred(HtmlParseError.CurrentNodeIsRoot, token);
					}
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			case HtmlTokenType.EndOfFile:
				if (this.CurrentNode != this._document.DocumentElement)
				{
					this.RaiseErrorOccurred(HtmlParseError.CurrentNodeIsNotRoot, token);
				}
				this.End();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000119F0 File Offset: 0x0000FBF0
		private void AfterFrameset(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.NoFrames))
				{
					this.InHead(token);
					return;
				}
				break;
			}
			case HtmlTokenType.EndTag:
				if (token.Name.Is(TagNames.Html))
				{
					this._currentMode = HtmlTreeMode.AfterAfterFrameset;
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			case HtmlTokenType.EndOfFile:
				this.End();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00011AB4 File Offset: 0x0000FCB4
		private void AfterAfterBody(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.InBody(token);
				return;
			case HtmlTokenType.StartTag:
				if (token.Name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				break;
			case HtmlTokenType.Comment:
				this._document.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.ReconstructFormatting();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			case HtmlTokenType.EndOfFile:
				this.End();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
			this._currentMode = HtmlTreeMode.InBody;
			this.InBody(token);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00011B58 File Offset: 0x0000FD58
		private void AfterAfterFrameset(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.InBody(token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				if (name.Is(TagNames.Html))
				{
					this.InBody(token);
					return;
				}
				if (name.Is(TagNames.NoFrames))
				{
					this.InHead(token);
					return;
				}
				break;
			}
			case HtmlTokenType.Comment:
				this._document.AddComment(token);
				return;
			case HtmlTokenType.Character:
			{
				string text = token.TrimStart();
				this.ReconstructFormatting();
				this.AddCharacters(text);
				if (token.IsEmpty)
				{
					return;
				}
				break;
			}
			case HtmlTokenType.EndOfFile:
				this.End();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TokenNotPossible, token);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00011C02 File Offset: 0x0000FE02
		private void TemplateStep(HtmlToken token, HtmlTreeMode mode)
		{
			this._templateModes.Pop();
			this._templateModes.Push(mode);
			this._currentMode = mode;
			this.Home(token);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00011C2C File Offset: 0x0000FE2C
		private void CloseTemplate()
		{
			while (this._openElements.Count > 0)
			{
				HtmlTemplateElement htmlTemplateElement = this.CurrentNode as HtmlTemplateElement;
				this.CloseCurrentNode();
				if (htmlTemplateElement != null)
				{
					htmlTemplateElement.PopulateFragment();
					break;
				}
			}
			this._formattingElements.ClearFormatting();
			this._templateModes.Pop();
			this.Reset();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00011C83 File Offset: 0x0000FE83
		private void InTableBodyCloseTable(HtmlTagToken tag)
		{
			if (this.IsInTableScope(TagNames.AllTableSections))
			{
				this.ClearStackBackTo(TagNames.AllTableSections);
				this.CloseCurrentNode();
				this._currentMode = HtmlTreeMode.InTable;
				this.InTable(tag);
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TableSectionNotInScope, tag);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00011CBB File Offset: 0x0000FEBB
		private void InSelectEndTagOption(HtmlToken token)
		{
			if (this.CurrentNode.LocalName.Is(TagNames.Option))
			{
				this.CloseCurrentNode();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		private void InSelectEndTagOptgroup(HtmlToken token)
		{
			if (this._openElements.Count > 1 && this._openElements[this._openElements.Count - 1].LocalName.Is(TagNames.Option) && this._openElements[this._openElements.Count - 2].LocalName.Is(TagNames.Optgroup))
			{
				this.CloseCurrentNode();
			}
			if (this.CurrentNode.LocalName.Is(TagNames.Optgroup))
			{
				this.CloseCurrentNode();
				return;
			}
			this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00011D7E File Offset: 0x0000FF7E
		private bool InColumnGroupEndTagColgroup(HtmlToken token)
		{
			if (this.CurrentNode.LocalName.Is(TagNames.Colgroup))
			{
				this.CloseCurrentNode();
				this._currentMode = HtmlTreeMode.InTable;
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
			return false;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		private void AfterHeadStartTagBody(HtmlTagToken token)
		{
			this.AddElement(new HtmlBodyElement(this._document, null), token, false);
			this._frameset = false;
			this._currentMode = HtmlTreeMode.InBody;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		private void RawtextAlgorithm(HtmlTagToken tag)
		{
			this.AddElement(tag, false);
			this._previousMode = this._currentMode;
			this._currentMode = HtmlTreeMode.Text;
			this._tokenizer.State = HtmlParseMode.Rawtext;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00011DFE File Offset: 0x0000FFFE
		private void RCDataAlgorithm(HtmlTagToken tag)
		{
			this.AddElement(tag, false);
			this._previousMode = this._currentMode;
			this._currentMode = HtmlTreeMode.Text;
			this._tokenizer.State = HtmlParseMode.RCData;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00011E28 File Offset: 0x00010028
		private void InBodyStartTagListItem(HtmlTagToken tag)
		{
			int num = this._openElements.Count - 1;
			Element element = this._openElements[num];
			this._frameset = false;
			while (!element.LocalName.Is(TagNames.Li))
			{
				if ((element.Flags & NodeFlags.Special) == NodeFlags.Special && !TagNames.AllBasicBlocks.Contains(element.LocalName))
				{
					IL_0077:
					if (this.IsInButtonScope())
					{
						this.InBodyEndTagParagraph(tag);
					}
					this.AddElement(tag, false);
					return;
				}
				element = this._openElements[--num];
			}
			this.InBody(HtmlTagToken.Close(element.LocalName));
			goto IL_0077;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00011EC8 File Offset: 0x000100C8
		private void InBodyStartTagDefinitionItem(HtmlTagToken tag)
		{
			this._frameset = false;
			int num = this._openElements.Count - 1;
			Element element = this._openElements[num];
			while (!element.LocalName.IsOneOf(TagNames.Dd, TagNames.Dt))
			{
				if ((element.Flags & NodeFlags.Special) == NodeFlags.Special && !TagNames.AllBasicBlocks.Contains(element.LocalName))
				{
					IL_007C:
					if (this.IsInButtonScope())
					{
						this.InBodyEndTagParagraph(tag);
					}
					this.AddElement(tag, false);
					return;
				}
				element = this._openElements[--num];
			}
			this.InBody(HtmlTagToken.Close(element.LocalName));
			goto IL_007C;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00011F6C File Offset: 0x0001016C
		private bool InBodyEndTagBlock(HtmlTagToken tag)
		{
			if (this.IsInScope(tag.Name))
			{
				this.GenerateImpliedEndTags();
				if (!this.CurrentNode.LocalName.Is(tag.Name))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, tag);
				}
				this.ClearStackBackTo(tag.Name);
				this.CloseCurrentNode();
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.BlockNotInScope, tag);
			return false;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00011FCC File Offset: 0x000101CC
		private void HeisenbergAlgorithm(HtmlTagToken tag)
		{
			int i = 0;
			while (i < 8)
			{
				Element element = null;
				Element element2 = null;
				i++;
				int num = 0;
				int num2 = 0;
				int num3 = this._formattingElements.Count - 1;
				while (num3 >= 0 && this._formattingElements[num3] != null)
				{
					if (this._formattingElements[num3].LocalName.Is(tag.Name))
					{
						num = num3;
						element = this._formattingElements[num3];
						break;
					}
					num3--;
				}
				if (element == null)
				{
					this.InBodyEndTagAnythingElse(tag);
					return;
				}
				int num4 = this._openElements.IndexOf(element);
				if (num4 == -1)
				{
					this.RaiseErrorOccurred(HtmlParseError.FormattingElementNotFound, tag);
					this._formattingElements.Remove(element);
					return;
				}
				if (!this.IsInScope(element.LocalName))
				{
					this.RaiseErrorOccurred(HtmlParseError.ElementNotInScope, tag);
					return;
				}
				if (num4 != this._openElements.Count - 1)
				{
					this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong, tag);
				}
				int num5 = num;
				for (int j = num4 + 1; j < this._openElements.Count; j++)
				{
					if ((this._openElements[j].Flags & NodeFlags.Special) == NodeFlags.Special)
					{
						num = j;
						element2 = this._openElements[j];
						break;
					}
				}
				if (element2 == null)
				{
					do
					{
						element2 = this.CurrentNode;
						this.CloseCurrentNode();
					}
					while (element2 != element);
					this._formattingElements.Remove(element);
					return;
				}
				Element element3 = this._openElements[num4 - 1];
				Element element4 = element2;
				for (;;)
				{
					num2++;
					Element element5 = this._openElements[--num];
					if (element5 == element)
					{
						break;
					}
					if (num2 > 3 && this._formattingElements.Contains(element5))
					{
						this._formattingElements.Remove(element5);
					}
					if (!this._formattingElements.Contains(element5))
					{
						this.CloseNode(element5);
					}
					else
					{
						Element element6 = this.CopyElement(element5);
						element3.AddNode(element6);
						this._openElements[num] = element6;
						for (int num6 = 0; num6 != this._formattingElements.Count; num6++)
						{
							if (this._formattingElements[num6] == element5)
							{
								this._formattingElements[num6] = element6;
								break;
							}
						}
						element5 = element6;
						if (element4 == element2)
						{
							num5++;
						}
						Node parent = element4.Parent;
						if (parent != null)
						{
							parent.RemoveChild(element4);
						}
						element5.AddNode(element4);
						element4 = element5;
					}
				}
				Node parent2 = element4.Parent;
				if (parent2 != null)
				{
					parent2.RemoveChild(element4);
				}
				if (!TagNames.AllTableMajor.Contains(element3.LocalName))
				{
					element3.AddNode(element4);
				}
				else
				{
					this.AddElementWithFoster(element4);
				}
				Element element7 = this.CopyElement(element);
				while (element2.ChildNodes.Length > 0)
				{
					Node node = element2.ChildNodes[0];
					element2.RemoveNode(0, node);
					element7.AddNode(node);
				}
				element2.AddNode(element7);
				this._formattingElements.Remove(element);
				this._formattingElements.Insert(num5, element7);
				this.CloseNode(element);
				this._openElements.Insert(this._openElements.IndexOf(element2) + 1, element7);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000122FD File Offset: 0x000104FD
		private Element CopyElement(Element element)
		{
			return (Element)element.Clone(false);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0001230B File Offset: 0x0001050B
		private void InBodyWithFoster(HtmlToken token)
		{
			this._foster = true;
			this.InBody(token);
			this._foster = false;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00012324 File Offset: 0x00010524
		private void InBodyEndTagAnythingElse(HtmlTagToken tag)
		{
			int num = this._openElements.Count - 1;
			for (Element element = this.CurrentNode; element != null; element = this._openElements[--num])
			{
				if (element.LocalName.Is(tag.Name))
				{
					this.GenerateImpliedEndTagsExceptFor(tag.Name);
					if (!element.LocalName.Is(tag.Name))
					{
						this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong, tag);
					}
					this.CloseNodesFrom(num);
					return;
				}
				if ((element.Flags & NodeFlags.Special) == NodeFlags.Special)
				{
					this.RaiseErrorOccurred(HtmlParseError.TagClosedWrong, tag);
					return;
				}
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000123B4 File Offset: 0x000105B4
		private bool InBodyEndTagBody(HtmlToken token)
		{
			if (this.IsInScope(TagNames.Body))
			{
				this.CheckBodyOnClosing(token);
				this._currentMode = HtmlTreeMode.AfterBody;
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.BodyNotInScope, token);
			return false;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000123DE File Offset: 0x000105DE
		private void InBodyStartTagBreakrow(HtmlTagToken tag)
		{
			this.ReconstructFormatting();
			this.AddElement(tag, true);
			this.CloseCurrentNode();
			this._frameset = false;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000123FC File Offset: 0x000105FC
		private bool InBodyEndTagParagraph(HtmlToken token)
		{
			if (this.IsInButtonScope())
			{
				this.GenerateImpliedEndTagsExceptFor(TagNames.P);
				if (!this.CurrentNode.LocalName.Is(TagNames.P))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
				}
				this.ClearStackBackTo(TagNames.P);
				this.CloseCurrentNode();
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.ParagraphNotInScope, token);
			this.InBody(HtmlTagToken.Open(TagNames.P));
			this.InBodyEndTagParagraph(token);
			return false;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00012471 File Offset: 0x00010671
		private bool InTableEndTagTable(HtmlToken token)
		{
			if (this.IsInTableScope(TagNames.Table))
			{
				this.ClearStackBackTo(TagNames.Table);
				this.CloseCurrentNode();
				this.Reset();
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.TableNotInScope, token);
			return false;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000124A3 File Offset: 0x000106A3
		private bool InRowEndTagTablerow(HtmlToken token)
		{
			if (this.IsInTableScope(TagNames.Tr))
			{
				this.ClearStackBackTo(TagNames.Tr);
				this.CloseCurrentNode();
				this._currentMode = HtmlTreeMode.InTableBody;
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.TableRowNotInScope, token);
			return false;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000124D7 File Offset: 0x000106D7
		private void InSelectEndTagSelect()
		{
			this.ClearStackBackTo(TagNames.Select);
			this.CloseCurrentNode();
			this.Reset();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000124F0 File Offset: 0x000106F0
		private bool InCaptionEndTagCaption(HtmlToken token)
		{
			if (this.IsInTableScope(TagNames.Caption))
			{
				this.GenerateImpliedEndTags();
				if (!this.CurrentNode.LocalName.Is(TagNames.Caption))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
				}
				this.ClearStackBackTo(TagNames.Caption);
				this.CloseCurrentNode();
				this._formattingElements.ClearFormatting();
				this._currentMode = HtmlTreeMode.InTable;
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.CaptionNotInScope, token);
			return false;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00012560 File Offset: 0x00010760
		private bool InCellEndTagCell(HtmlToken token)
		{
			if (this.IsInTableScope(TagNames.AllTableCells))
			{
				this.GenerateImpliedEndTags();
				if (!TagNames.AllTableCells.Contains(this.CurrentNode.LocalName))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagDoesNotMatchCurrentNode, token);
				}
				this.ClearStackBackTo(TagNames.AllTableCells);
				this.CloseCurrentNode();
				this._formattingElements.ClearFormatting();
				this._currentMode = HtmlTreeMode.InRow;
				return true;
			}
			this.RaiseErrorOccurred(HtmlParseError.TableCellNotInScope, token);
			return false;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000125D0 File Offset: 0x000107D0
		private void Foreign(HtmlToken token)
		{
			switch (token.Type)
			{
			case HtmlTokenType.Doctype:
				this.RaiseErrorOccurred(HtmlParseError.DoctypeTagInappropriate, token);
				return;
			case HtmlTokenType.StartTag:
			{
				string name = token.Name;
				HtmlTagToken htmlTagToken = token.AsTag();
				if (name.Is(TagNames.Font))
				{
					for (int num = 0; num != htmlTagToken.Attributes.Count; num++)
					{
						if (htmlTagToken.Attributes[num].Key.IsOneOf(AttributeNames.Color, AttributeNames.Face, AttributeNames.Size))
						{
							this.ForeignNormalTag(htmlTagToken);
							return;
						}
					}
					this.ForeignSpecialTag(htmlTagToken);
					return;
				}
				if (TagNames.AllForeignExceptions.Contains(name))
				{
					this.ForeignNormalTag(htmlTagToken);
					return;
				}
				this.ForeignSpecialTag(htmlTagToken);
				return;
			}
			case HtmlTokenType.EndTag:
			{
				string name2 = token.Name;
				Element element = this.CurrentNode;
				HtmlScriptElement htmlScriptElement = element as HtmlScriptElement;
				if (htmlScriptElement != null)
				{
					this.HandleScript(htmlScriptElement);
					return;
				}
				if (!element.LocalName.Is(name2))
				{
					this.RaiseErrorOccurred(HtmlParseError.TagClosingMismatch, token);
				}
				for (int i = this._openElements.Count - 1; i > 0; i--)
				{
					if (element.LocalName.Isi(name2))
					{
						this.CloseNodesFrom(i);
						return;
					}
					element = this._openElements[i - 1];
					if ((element.Flags & NodeFlags.HtmlMember) == NodeFlags.HtmlMember)
					{
						this.Home(token);
						return;
					}
				}
				return;
			}
			case HtmlTokenType.Comment:
				this.CurrentNode.AddComment(token);
				return;
			case HtmlTokenType.Character:
				this.AddCharacters(token.Data.Replace('\0', '\ufffd'));
				this._frameset = !token.HasContent && this._frameset;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00012770 File Offset: 0x00010970
		private void ForeignSpecialTag(HtmlTagToken tag)
		{
			Element element = this.CreateForeignElementFrom(tag);
			if (element != null)
			{
				bool isSelfClosing = tag.IsSelfClosing;
				this.CurrentNode.AddNode(element);
				if (isSelfClosing)
				{
					element.SetupElement();
				}
				if (!isSelfClosing)
				{
					this._openElements.Add(element);
					this._tokenizer.IsAcceptingCharacterData = true;
					return;
				}
				if (tag.Name.Is(TagNames.Script))
				{
					this.Foreign(HtmlTagToken.Close(TagNames.Script));
				}
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000127E0 File Offset: 0x000109E0
		private Element CreateForeignElementFrom(HtmlTagToken tag)
		{
			if ((this.AdjustedCurrentNode.Flags & NodeFlags.MathMember) == NodeFlags.MathMember)
			{
				string name = tag.Name;
				MathElement mathElement = this._mathFactory.Create(this._document, name, null);
				this.AuxiliarySetupSteps(mathElement, tag);
				return mathElement.Setup(tag);
			}
			if ((this.AdjustedCurrentNode.Flags & NodeFlags.SvgMember) == NodeFlags.SvgMember)
			{
				string text = tag.Name.SanatizeSvgTagName();
				SvgElement svgElement = this._svgFactory.Create(this._document, text, null);
				this.AuxiliarySetupSteps(svgElement, tag);
				return svgElement.Setup(tag);
			}
			return null;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001287C File Offset: 0x00010A7C
		private void ForeignNormalTag(HtmlTagToken tag)
		{
			this.RaiseErrorOccurred(HtmlParseError.TagCannotStartHere, tag);
			if (!this.IsFragmentCase)
			{
				Element element = this.CurrentNode;
				for (;;)
				{
					if (element.LocalName.Is(TagNames.AnnotationXml))
					{
						string attribute = element.GetAttribute(null, AttributeNames.Encoding);
						if (attribute.Isi(MimeTypeNames.Html) || attribute.Isi(MimeTypeNames.ApplicationXHtml))
						{
							break;
						}
					}
					this.CloseCurrentNode();
					element = this.CurrentNode;
					if ((element.Flags & (NodeFlags.HtmlMember | NodeFlags.HtmlTip | NodeFlags.MathTip)) != NodeFlags.None)
					{
						goto Block_4;
					}
				}
				this.AddElement(tag, false);
				return;
				Block_4:
				this.Consume(tag);
				return;
			}
			this.ForeignSpecialTag(tag);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00012910 File Offset: 0x00010B10
		private bool IsInScope(string tagName)
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (element.LocalName.Is(tagName))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.Scoped) == NodeFlags.Scoped)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00012964 File Offset: 0x00010B64
		private bool IsInScope(HashSet<string> tags)
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (tags.Contains(element.LocalName))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.Scoped) == NodeFlags.Scoped)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000129B8 File Offset: 0x00010BB8
		private bool IsInListItemScope()
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (element.LocalName.Is(TagNames.Li))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.HtmlListScoped) == NodeFlags.HtmlListScoped)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00012A14 File Offset: 0x00010C14
		private bool IsInButtonScope()
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (element.LocalName.Is(TagNames.P))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.Scoped) == NodeFlags.Scoped || element.LocalName.Is(TagNames.Button))
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00012A7C File Offset: 0x00010C7C
		private bool IsInTableScope(HashSet<string> tags)
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (tags.Contains(element.LocalName))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.HtmlTableScoped) == NodeFlags.HtmlTableScoped)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00012AD4 File Offset: 0x00010CD4
		private bool IsInTableScope(string tagName)
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (element.LocalName.Is(tagName))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.HtmlTableScoped) == NodeFlags.HtmlTableScoped)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00012B2C File Offset: 0x00010D2C
		private bool IsInSelectScope(string tagName)
		{
			for (int i = this._openElements.Count - 1; i >= 0; i--)
			{
				Element element = this._openElements[i];
				if (element.LocalName.Is(tagName))
				{
					return true;
				}
				if ((element.Flags & NodeFlags.HtmlSelectScoped) != NodeFlags.HtmlSelectScoped)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00012B84 File Offset: 0x00010D84
		private void HandleScript(HtmlScriptElement script)
		{
			if (script != null)
			{
				if (this.IsFragmentCase)
				{
					this.CloseCurrentNode();
					this._currentMode = this._previousMode;
					return;
				}
				this._document.PerformMicrotaskCheckpoint();
				this._document.ProvideStableState();
				this.CloseCurrentNode();
				this._currentMode = this._previousMode;
				if (script.Prepare(this._document))
				{
					this._waiting = this.RunScript(script);
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00012BF4 File Offset: 0x00010DF4
		private async Task RunScript(HtmlScriptElement script)
		{
			await this._document.WaitForReadyAsync().ConfigureAwait(false);
			await script.RunAsync(CancellationToken.None).ConfigureAwait(false);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00012C44 File Offset: 0x00010E44
		private void CheckBodyOnClosing(HtmlToken token)
		{
			for (int i = 0; i < this._openElements.Count; i++)
			{
				if ((this._openElements[i].Flags & NodeFlags.ImplicitelyClosed) != NodeFlags.ImplicitelyClosed)
				{
					this.RaiseErrorOccurred(HtmlParseError.BodyClosedWrong, token);
					return;
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00012C8C File Offset: 0x00010E8C
		private bool TagCurrentlyOpen(string tagName)
		{
			for (int i = 0; i < this._openElements.Count; i++)
			{
				if (this._openElements[i].LocalName.Is(tagName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00012CCC File Offset: 0x00010ECC
		private void PreventNewLine()
		{
			HtmlToken htmlToken = this._tokenizer.Get();
			if (htmlToken.Type == HtmlTokenType.Character)
			{
				htmlToken.RemoveNewLine();
			}
			this.Home(htmlToken);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00012CFB File Offset: 0x00010EFB
		private void End()
		{
			while (this._openElements.Count != 0)
			{
				this.CloseCurrentNode();
			}
			if (this._document.IsLoading)
			{
				this._waiting = this._document.FinishLoadingAsync();
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00012D30 File Offset: 0x00010F30
		private void AddRoot(HtmlTagToken tag)
		{
			HtmlHtmlElement htmlHtmlElement = new HtmlHtmlElement(this._document, null);
			this._document.AddNode(htmlHtmlElement);
			this.SetupElement(htmlHtmlElement, tag, false);
			this._openElements.Add(htmlHtmlElement);
			this._tokenizer.IsAcceptingCharacterData = false;
			this._document.ApplyManifest();
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00012D82 File Offset: 0x00010F82
		private void CloseNode(Element element)
		{
			element.SetupElement();
			this._openElements.Remove(element);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00012D98 File Offset: 0x00010F98
		private void CloseNodesFrom(int index)
		{
			for (int i = this._openElements.Count - 1; i > index; i--)
			{
				this._openElements[i].SetupElement();
				this._openElements.RemoveAt(i);
			}
			this.CloseCurrentNode();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00012DE0 File Offset: 0x00010FE0
		private void CloseCurrentNode()
		{
			if (this._openElements.Count > 0)
			{
				int num = this._openElements.Count - 1;
				this._openElements[num].SetupElement();
				this._openElements.RemoveAt(num);
				Element adjustedCurrentNode = this.AdjustedCurrentNode;
				this._tokenizer.IsAcceptingCharacterData = adjustedCurrentNode != null && (adjustedCurrentNode.Flags & NodeFlags.HtmlMember) != NodeFlags.HtmlMember;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00012E54 File Offset: 0x00011054
		private void SetupElement(Element element, HtmlTagToken tag, bool acknowledgeSelfClosing)
		{
			if (tag.IsSelfClosing && !acknowledgeSelfClosing)
			{
				this.RaiseErrorOccurred(HtmlParseError.TagCannotBeSelfClosed, tag);
			}
			this.AuxiliarySetupSteps(element, tag);
			element.SetAttributes(tag.Attributes);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00012E80 File Offset: 0x00011080
		private Element AddElement(HtmlTagToken tag, bool acknowledgeSelfClosing = false)
		{
			HtmlElement htmlElement = this._htmlFactory.Create(this._document, tag.Name, null);
			this.SetupElement(htmlElement, tag, acknowledgeSelfClosing);
			this.AddElement(htmlElement);
			return htmlElement;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00012EB7 File Offset: 0x000110B7
		private void AddElement(Element element, HtmlTagToken tag, bool acknowledgeSelfClosing = false)
		{
			this.SetupElement(element, tag, acknowledgeSelfClosing);
			this.AddElement(element);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00012ECC File Offset: 0x000110CC
		private void AddElement(Element element)
		{
			Element currentNode = this.CurrentNode;
			if (this._foster && TagNames.AllTableMajor.Contains(currentNode.LocalName))
			{
				this.AddElementWithFoster(element);
			}
			else
			{
				currentNode.AddNode(element);
			}
			this._openElements.Add(element);
			this._tokenizer.IsAcceptingCharacterData = (element.Flags & NodeFlags.HtmlMember) != NodeFlags.HtmlMember;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00012F38 File Offset: 0x00011138
		private void AddElementWithFoster(Element element)
		{
			bool flag = false;
			int num = this._openElements.Count;
			while (--num != 0)
			{
				if (this._openElements[num].LocalName.Is(TagNames.Template))
				{
					this._openElements[num].AddNode(element);
					return;
				}
				if (this._openElements[num].LocalName.Is(TagNames.Table))
				{
					flag = true;
					break;
				}
			}
			Node node = this._openElements[num].Parent ?? this._openElements[num + 1];
			if (flag && this._openElements[num].Parent != null)
			{
				for (int i = 0; i < node.ChildNodes.Length; i++)
				{
					if (node.ChildNodes[i] == this._openElements[num])
					{
						node.InsertNode(i, element);
						return;
					}
				}
				return;
			}
			node.AddNode(element);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00013028 File Offset: 0x00011228
		private void AddCharacters(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				Element currentNode = this.CurrentNode;
				if (this._foster && TagNames.AllTableMajor.Contains(currentNode.LocalName))
				{
					this.AddCharactersWithFoster(text);
					return;
				}
				currentNode.AppendText(text);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00013070 File Offset: 0x00011270
		private void AddCharactersWithFoster(string text)
		{
			bool flag = false;
			int num = this._openElements.Count;
			while (--num != 0)
			{
				if (this._openElements[num].LocalName.Is(TagNames.Template))
				{
					this._openElements[num].AppendText(text);
					return;
				}
				if (this._openElements[num].LocalName.Is(TagNames.Table))
				{
					flag = true;
					break;
				}
			}
			Node node = this._openElements[num].Parent ?? this._openElements[num + 1];
			if (flag && this._openElements[num].Parent != null)
			{
				for (int i = 0; i < node.ChildNodes.Length; i++)
				{
					if (node.ChildNodes[i] == this._openElements[num])
					{
						node.InsertText(i, text);
						return;
					}
				}
				return;
			}
			node.AppendText(text);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00013160 File Offset: 0x00011360
		private void AuxiliarySetupSteps(Element element, HtmlTagToken tag)
		{
			if (this._options.OnCreated != null)
			{
				this._options.OnCreated(element, tag.Position);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00013188 File Offset: 0x00011388
		private void ClearStackBackTo(string tagName)
		{
			Element element = this.CurrentNode;
			while (!element.LocalName.IsOneOf(tagName, TagNames.Html, TagNames.Template))
			{
				this.CloseCurrentNode();
				element = this.CurrentNode;
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000131C4 File Offset: 0x000113C4
		private void ClearStackBackTo(HashSet<string> tags)
		{
			Element element = this.CurrentNode;
			while (!tags.Contains(element.LocalName) && !element.LocalName.IsOneOf(TagNames.Html, TagNames.Template))
			{
				this.CloseCurrentNode();
				element = this.CurrentNode;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0001320C File Offset: 0x0001140C
		private void GenerateImpliedEndTagsExceptFor(string tagName)
		{
			Element element = this.CurrentNode;
			while ((element.Flags & NodeFlags.ImpliedEnd) == NodeFlags.ImpliedEnd && !element.LocalName.Is(tagName))
			{
				this.CloseCurrentNode();
				element = this.CurrentNode;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001324A File Offset: 0x0001144A
		private void GenerateImpliedEndTags()
		{
			while ((this.CurrentNode.Flags & NodeFlags.ImpliedEnd) == NodeFlags.ImpliedEnd)
			{
				this.CloseCurrentNode();
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00013268 File Offset: 0x00011468
		private void ReconstructFormatting()
		{
			if (this._formattingElements.Count == 0)
			{
				return;
			}
			int i = this._formattingElements.Count - 1;
			Element element = this._formattingElements[i];
			if (element == null || this._openElements.Contains(element))
			{
				return;
			}
			while (i > 0)
			{
				element = this._formattingElements[--i];
				if (element == null || this._openElements.Contains(element))
				{
					i++;
					IL_0094:
					while (i < this._formattingElements.Count)
					{
						Element element2 = this.CopyElement(this._formattingElements[i]);
						this.AddElement(element2);
						this._formattingElements[i] = element2;
						i++;
					}
					return;
				}
			}
			goto IL_0094;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00013317 File Offset: 0x00011517
		private void RaiseErrorOccurred(HtmlParseError code, HtmlToken token)
		{
			this._tokenizer.RaiseErrorOccurred(code, token.Position);
		}

		// Token: 0x04000230 RID: 560
		private readonly HtmlTokenizer _tokenizer;

		// Token: 0x04000231 RID: 561
		private readonly HtmlDocument _document;

		// Token: 0x04000232 RID: 562
		private readonly List<Element> _openElements;

		// Token: 0x04000233 RID: 563
		private readonly List<Element> _formattingElements;

		// Token: 0x04000234 RID: 564
		private readonly Stack<HtmlTreeMode> _templateModes;

		// Token: 0x04000235 RID: 565
		private readonly IElementFactory<HtmlElement> _htmlFactory;

		// Token: 0x04000236 RID: 566
		private readonly IElementFactory<MathElement> _mathFactory;

		// Token: 0x04000237 RID: 567
		private readonly IElementFactory<SvgElement> _svgFactory;

		// Token: 0x04000238 RID: 568
		private HtmlFormElement _currentFormElement;

		// Token: 0x04000239 RID: 569
		private HtmlTreeMode _currentMode;

		// Token: 0x0400023A RID: 570
		private HtmlTreeMode _previousMode;

		// Token: 0x0400023B RID: 571
		private HtmlParserOptions _options;

		// Token: 0x0400023C RID: 572
		private Element _fragmentContext;

		// Token: 0x0400023D RID: 573
		private bool _foster;

		// Token: 0x0400023E RID: 574
		private bool _frameset;

		// Token: 0x0400023F RID: 575
		private Task _waiting;
	}
}
