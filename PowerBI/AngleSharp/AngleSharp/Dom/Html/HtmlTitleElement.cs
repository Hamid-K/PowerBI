using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A4 RID: 932
	internal sealed class HtmlTitleElement : HtmlElement, IHtmlTitleElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001D58 RID: 7512 RVA: 0x00055947 File Offset: 0x00053B47
		public HtmlTitleElement(Document owner, string prefix = null)
			: base(owner, TagNames.Title, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x0004FCC7 File Offset: 0x0004DEC7
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x0004FCCF File Offset: 0x0004DECF
		public string Text
		{
			get
			{
				return this.TextContent;
			}
			set
			{
				this.TextContent = value;
			}
		}
	}
}
