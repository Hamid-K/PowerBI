using System;
using System.Collections.Generic;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Services.Default
{
	// Token: 0x0200004A RID: 74
	internal sealed class HtmlElementFactory : IElementFactory<HtmlElement>
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00009B48 File Offset: 0x00007D48
		public HtmlElement Create(Document document, string localName, string prefix = null)
		{
			HtmlElementFactory.Creator creator = null;
			if (this.creators.TryGetValue(localName, out creator))
			{
				return creator(document, prefix);
			}
			return new HtmlUnknownElement(document, localName.HtmlLower(), prefix);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00009B80 File Offset: 0x00007D80
		public HtmlElementFactory()
		{
			Dictionary<string, HtmlElementFactory.Creator> dictionary = new Dictionary<string, HtmlElementFactory.Creator>(StringComparer.OrdinalIgnoreCase);
			dictionary.Add(TagNames.Div, (Document document, string prefix) => new HtmlDivElement(document, prefix));
			dictionary.Add(TagNames.A, (Document document, string prefix) => new HtmlAnchorElement(document, prefix));
			dictionary.Add(TagNames.Img, (Document document, string prefix) => new HtmlImageElement(document, prefix));
			dictionary.Add(TagNames.P, (Document document, string prefix) => new HtmlParagraphElement(document, prefix));
			dictionary.Add(TagNames.Br, (Document document, string prefix) => new HtmlBreakRowElement(document, prefix));
			dictionary.Add(TagNames.Input, (Document document, string prefix) => new HtmlInputElement(document, prefix));
			dictionary.Add(TagNames.Button, (Document document, string prefix) => new HtmlButtonElement(document, prefix));
			dictionary.Add(TagNames.Textarea, (Document document, string prefix) => new HtmlTextAreaElement(document, prefix));
			dictionary.Add(TagNames.Li, (Document document, string prefix) => new HtmlListItemElement(document, TagNames.Li, prefix));
			dictionary.Add(TagNames.H1, (Document document, string prefix) => new HtmlHeadingElement(document, TagNames.H1, prefix));
			dictionary.Add(TagNames.H2, (Document document, string prefix) => new HtmlHeadingElement(document, TagNames.H2, prefix));
			dictionary.Add(TagNames.H3, (Document document, string prefix) => new HtmlHeadingElement(document, TagNames.H3, prefix));
			dictionary.Add(TagNames.H4, (Document document, string prefix) => new HtmlHeadingElement(document, TagNames.H4, prefix));
			dictionary.Add(TagNames.H5, (Document document, string prefix) => new HtmlHeadingElement(document, TagNames.H5, prefix));
			dictionary.Add(TagNames.H6, (Document document, string prefix) => new HtmlHeadingElement(document, TagNames.H6, prefix));
			dictionary.Add(TagNames.Ul, (Document document, string prefix) => new HtmlUnorderedListElement(document, prefix));
			dictionary.Add(TagNames.Ol, (Document document, string prefix) => new HtmlOrderedListElement(document, prefix));
			dictionary.Add(TagNames.Dl, (Document document, string prefix) => new HtmlDefinitionListElement(document, prefix));
			dictionary.Add(TagNames.Link, (Document document, string prefix) => new HtmlLinkElement(document, prefix));
			dictionary.Add(TagNames.Meta, (Document document, string prefix) => new HtmlMetaElement(document, prefix));
			dictionary.Add(TagNames.Label, (Document document, string prefix) => new HtmlLabelElement(document, prefix));
			dictionary.Add(TagNames.Fieldset, (Document document, string prefix) => new HtmlFieldSetElement(document, prefix));
			dictionary.Add(TagNames.Legend, (Document document, string prefix) => new HtmlLegendElement(document, prefix));
			dictionary.Add(TagNames.Form, (Document document, string prefix) => new HtmlFormElement(document, prefix));
			dictionary.Add(TagNames.Select, (Document document, string prefix) => new HtmlSelectElement(document, prefix));
			dictionary.Add(TagNames.Pre, (Document document, string prefix) => new HtmlPreElement(document, prefix));
			dictionary.Add(TagNames.Hr, (Document document, string prefix) => new HtmlHrElement(document, prefix));
			dictionary.Add(TagNames.Dir, (Document document, string prefix) => new HtmlDirectoryElement(document, prefix));
			dictionary.Add(TagNames.Font, (Document document, string prefix) => new HtmlFontElement(document, prefix));
			dictionary.Add(TagNames.Param, (Document document, string prefix) => new HtmlParamElement(document, prefix));
			dictionary.Add(TagNames.BlockQuote, (Document document, string prefix) => new HtmlQuoteElement(document, TagNames.BlockQuote, prefix));
			dictionary.Add(TagNames.Quote, (Document document, string prefix) => new HtmlQuoteElement(document, TagNames.Quote, prefix));
			dictionary.Add(TagNames.Q, (Document document, string prefix) => new HtmlQuoteElement(document, TagNames.Q, prefix));
			dictionary.Add(TagNames.Canvas, (Document document, string prefix) => new HtmlCanvasElement(document, prefix));
			dictionary.Add(TagNames.Caption, (Document document, string prefix) => new HtmlTableCaptionElement(document, prefix));
			dictionary.Add(TagNames.Td, (Document document, string prefix) => new HtmlTableDataCellElement(document, prefix));
			dictionary.Add(TagNames.Tr, (Document document, string prefix) => new HtmlTableRowElement(document, prefix));
			dictionary.Add(TagNames.Table, (Document document, string prefix) => new HtmlTableElement(document, prefix));
			dictionary.Add(TagNames.Tbody, (Document document, string prefix) => new HtmlTableSectionElement(document, TagNames.Tbody, prefix));
			dictionary.Add(TagNames.Th, (Document document, string prefix) => new HtmlTableHeaderCellElement(document, prefix));
			dictionary.Add(TagNames.Tfoot, (Document document, string prefix) => new HtmlTableSectionElement(document, TagNames.Tfoot, prefix));
			dictionary.Add(TagNames.Thead, (Document document, string prefix) => new HtmlTableSectionElement(document, TagNames.Thead, prefix));
			dictionary.Add(TagNames.Colgroup, (Document document, string prefix) => new HtmlTableColgroupElement(document, prefix));
			dictionary.Add(TagNames.Col, (Document document, string prefix) => new HtmlTableColElement(document, prefix));
			dictionary.Add(TagNames.Del, (Document document, string prefix) => new HtmlModElement(document, TagNames.Del, prefix));
			dictionary.Add(TagNames.Ins, (Document document, string prefix) => new HtmlModElement(document, TagNames.Ins, prefix));
			dictionary.Add(TagNames.Applet, (Document document, string prefix) => new HtmlAppletElement(document, prefix));
			dictionary.Add(TagNames.Object, (Document document, string prefix) => new HtmlObjectElement(document, prefix));
			dictionary.Add(TagNames.Optgroup, (Document document, string prefix) => new HtmlOptionsGroupElement(document, prefix));
			dictionary.Add(TagNames.Option, (Document document, string prefix) => new HtmlOptionElement(document, prefix));
			dictionary.Add(TagNames.Style, (Document document, string prefix) => new HtmlStyleElement(document, prefix));
			dictionary.Add(TagNames.Script, (Document document, string prefix) => new HtmlScriptElement(document, prefix, false, false));
			dictionary.Add(TagNames.Iframe, (Document document, string prefix) => new HtmlIFrameElement(document, prefix));
			dictionary.Add(TagNames.Dd, (Document document, string prefix) => new HtmlListItemElement(document, TagNames.Dd, prefix));
			dictionary.Add(TagNames.Dt, (Document document, string prefix) => new HtmlListItemElement(document, TagNames.Dt, prefix));
			dictionary.Add(TagNames.Frameset, (Document document, string prefix) => new HtmlFrameSetElement(document, prefix));
			dictionary.Add(TagNames.Frame, (Document document, string prefix) => new HtmlFrameElement(document, prefix));
			dictionary.Add(TagNames.Audio, (Document document, string prefix) => new HtmlAudioElement(document, prefix));
			dictionary.Add(TagNames.Video, (Document document, string prefix) => new HtmlVideoElement(document, prefix));
			dictionary.Add(TagNames.Span, (Document document, string prefix) => new HtmlSpanElement(document, prefix));
			dictionary.Add(TagNames.Dialog, (Document document, string prefix) => new HtmlDialogElement(document, prefix));
			dictionary.Add(TagNames.Details, (Document document, string prefix) => new HtmlDetailsElement(document, prefix));
			dictionary.Add(TagNames.Source, (Document document, string prefix) => new HtmlSourceElement(document, prefix));
			dictionary.Add(TagNames.Track, (Document document, string prefix) => new HtmlTrackElement(document, prefix));
			dictionary.Add(TagNames.Wbr, (Document document, string prefix) => new HtmlWbrElement(document, prefix));
			dictionary.Add(TagNames.B, (Document document, string prefix) => new HtmlBoldElement(document, prefix));
			dictionary.Add(TagNames.Big, (Document document, string prefix) => new HtmlBigElement(document, prefix));
			dictionary.Add(TagNames.Strike, (Document document, string prefix) => new HtmlStrikeElement(document, prefix));
			dictionary.Add(TagNames.Code, (Document document, string prefix) => new HtmlCodeElement(document, prefix));
			dictionary.Add(TagNames.Em, (Document document, string prefix) => new HtmlEmphasizeElement(document, prefix));
			dictionary.Add(TagNames.I, (Document document, string prefix) => new HtmlItalicElement(document, prefix));
			dictionary.Add(TagNames.S, (Document document, string prefix) => new HtmlStruckElement(document, prefix));
			dictionary.Add(TagNames.Small, (Document document, string prefix) => new HtmlSmallElement(document, prefix));
			dictionary.Add(TagNames.Strong, (Document document, string prefix) => new HtmlStrongElement(document, prefix));
			dictionary.Add(TagNames.U, (Document document, string prefix) => new HtmlUnderlineElement(document, prefix));
			dictionary.Add(TagNames.Tt, (Document document, string prefix) => new HtmlTeletypeTextElement(document, prefix));
			dictionary.Add(TagNames.Address, (Document document, string prefix) => new HtmlAddressElement(document, prefix));
			dictionary.Add(TagNames.Main, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Main, prefix));
			dictionary.Add(TagNames.Summary, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Summary, prefix));
			dictionary.Add(TagNames.Center, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Center, prefix));
			dictionary.Add(TagNames.Listing, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Listing, prefix));
			dictionary.Add(TagNames.Nav, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Nav, prefix));
			dictionary.Add(TagNames.Article, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Article, prefix));
			dictionary.Add(TagNames.Aside, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Aside, prefix));
			dictionary.Add(TagNames.Figcaption, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Figcaption, prefix));
			dictionary.Add(TagNames.Figure, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Figure, prefix));
			dictionary.Add(TagNames.Section, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Section, prefix));
			dictionary.Add(TagNames.Footer, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Footer, prefix));
			dictionary.Add(TagNames.Header, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Header, prefix));
			dictionary.Add(TagNames.Hgroup, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Hgroup, prefix));
			dictionary.Add(TagNames.Cite, (Document document, string prefix) => new HtmlElement(document, TagNames.Cite, prefix, NodeFlags.None));
			dictionary.Add(TagNames.Ruby, (Document document, string prefix) => new HtmlRubyElement(document, prefix));
			dictionary.Add(TagNames.Rt, (Document document, string prefix) => new HtmlRtElement(document, prefix));
			dictionary.Add(TagNames.Rp, (Document document, string prefix) => new HtmlRpElement(document, prefix));
			dictionary.Add(TagNames.Rtc, (Document document, string prefix) => new HtmlRtcElement(document, prefix));
			dictionary.Add(TagNames.Rb, (Document document, string prefix) => new HtmlRbElement(document, prefix));
			dictionary.Add(TagNames.Map, (Document document, string prefix) => new HtmlMapElement(document, prefix));
			dictionary.Add(TagNames.Datalist, (Document document, string prefix) => new HtmlDataListElement(document, prefix));
			dictionary.Add(TagNames.Xmp, (Document document, string prefix) => new HtmlXmpElement(document, prefix));
			dictionary.Add(TagNames.Picture, (Document document, string prefix) => new HtmlPictureElement(document, prefix));
			dictionary.Add(TagNames.Template, (Document document, string prefix) => new HtmlTemplateElement(document, prefix));
			dictionary.Add(TagNames.Time, (Document document, string prefix) => new HtmlTimeElement(document, prefix));
			dictionary.Add(TagNames.Progress, (Document document, string prefix) => new HtmlProgressElement(document, prefix));
			dictionary.Add(TagNames.Meter, (Document document, string prefix) => new HtmlMeterElement(document, prefix));
			dictionary.Add(TagNames.Output, (Document document, string prefix) => new HtmlOutputElement(document, prefix));
			dictionary.Add(TagNames.Keygen, (Document document, string prefix) => new HtmlKeygenElement(document, prefix));
			dictionary.Add(TagNames.Title, (Document document, string prefix) => new HtmlTitleElement(document, prefix));
			dictionary.Add(TagNames.Head, (Document document, string prefix) => new HtmlHeadElement(document, prefix));
			dictionary.Add(TagNames.Body, (Document document, string prefix) => new HtmlBodyElement(document, prefix));
			dictionary.Add(TagNames.Html, (Document document, string prefix) => new HtmlHtmlElement(document, prefix));
			dictionary.Add(TagNames.Area, (Document document, string prefix) => new HtmlAreaElement(document, prefix));
			dictionary.Add(TagNames.Embed, (Document document, string prefix) => new HtmlEmbedElement(document, prefix));
			dictionary.Add(TagNames.MenuItem, (Document document, string prefix) => new HtmlMenuItemElement(document, prefix));
			dictionary.Add(TagNames.Slot, (Document document, string prefix) => new HtmlSlotElement(document, prefix));
			dictionary.Add(TagNames.NoScript, (Document document, string prefix) => new HtmlNoScriptElement(document, prefix));
			dictionary.Add(TagNames.NoEmbed, (Document document, string prefix) => new HtmlNoEmbedElement(document, prefix));
			dictionary.Add(TagNames.NoFrames, (Document document, string prefix) => new HtmlNoFramesElement(document, prefix));
			dictionary.Add(TagNames.NoBr, (Document document, string prefix) => new HtmlNoNewlineElement(document, prefix));
			dictionary.Add(TagNames.Menu, (Document document, string prefix) => new HtmlMenuElement(document, prefix));
			dictionary.Add(TagNames.Base, (Document document, string prefix) => new HtmlBaseElement(document, prefix));
			dictionary.Add(TagNames.BaseFont, (Document document, string prefix) => new HtmlBaseFontElement(document, prefix));
			dictionary.Add(TagNames.Bgsound, (Document document, string prefix) => new HtmlBgsoundElement(document, prefix));
			dictionary.Add(TagNames.Marquee, (Document document, string prefix) => new HtmlMarqueeElement(document, prefix));
			dictionary.Add(TagNames.Data, (Document document, string prefix) => new HtmlDataElement(document, prefix));
			dictionary.Add(TagNames.Plaintext, (Document document, string prefix) => new HtmlSemanticElement(document, TagNames.Plaintext, prefix));
			dictionary.Add(TagNames.IsIndex, (Document document, string prefix) => new HtmlIsIndexElement(document, prefix));
			dictionary.Add(TagNames.Mark, (Document document, string prefix) => new HtmlElement(document, TagNames.Mark, null, NodeFlags.None));
			dictionary.Add(TagNames.Sub, (Document document, string prefix) => new HtmlElement(document, TagNames.Sub, null, NodeFlags.None));
			dictionary.Add(TagNames.Sup, (Document document, string prefix) => new HtmlElement(document, TagNames.Sup, null, NodeFlags.None));
			dictionary.Add(TagNames.Dfn, (Document document, string prefix) => new HtmlElement(document, TagNames.Dfn, null, NodeFlags.None));
			dictionary.Add(TagNames.Kbd, (Document document, string prefix) => new HtmlElement(document, TagNames.Kbd, null, NodeFlags.None));
			dictionary.Add(TagNames.Var, (Document document, string prefix) => new HtmlElement(document, TagNames.Var, null, NodeFlags.None));
			dictionary.Add(TagNames.Samp, (Document document, string prefix) => new HtmlElement(document, TagNames.Samp, null, NodeFlags.None));
			dictionary.Add(TagNames.Abbr, (Document document, string prefix) => new HtmlElement(document, TagNames.Abbr, null, NodeFlags.None));
			dictionary.Add(TagNames.Bdi, (Document document, string prefix) => new HtmlElement(document, TagNames.Bdi, null, NodeFlags.None));
			dictionary.Add(TagNames.Bdo, (Document document, string prefix) => new HtmlElement(document, TagNames.Bdo, null, NodeFlags.None));
			this.creators = dictionary;
			base..ctor();
		}

		// Token: 0x040001C8 RID: 456
		private readonly Dictionary<string, HtmlElementFactory.Creator> creators;

		// Token: 0x0200042A RID: 1066
		// (Invoke) Token: 0x0600220B RID: 8715
		private delegate HtmlElement Creator(Document owner, string prefix);
	}
}
