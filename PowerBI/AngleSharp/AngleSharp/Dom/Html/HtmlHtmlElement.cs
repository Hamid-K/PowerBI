using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000363 RID: 867
	internal sealed class HtmlHtmlElement : HtmlElement, IHtmlHtmlElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001AC2 RID: 6850 RVA: 0x0005270E File Offset: 0x0005090E
		public HtmlHtmlElement(Document owner, string prefix = null)
			: base(owner, TagNames.Html, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableSectionScoped | NodeFlags.HtmlTableScoped)
		{
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00052722 File Offset: 0x00050922
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x0005272F File Offset: 0x0005092F
		public string Manifest
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Manifest);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Manifest, value, false);
			}
		}
	}
}
