using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200034C RID: 844
	internal sealed class HtmlDataElement : HtmlElement, IHtmlDataElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001985 RID: 6533 RVA: 0x00050402 File Offset: 0x0004E602
		public HtmlDataElement(Document owner, string prefix = null)
			: base(owner, TagNames.Data, prefix, NodeFlags.None)
		{
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x00050412 File Offset: 0x0004E612
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x000500CC File Offset: 0x0004E2CC
		public string Value
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Value);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Value, value, false);
			}
		}
	}
}
