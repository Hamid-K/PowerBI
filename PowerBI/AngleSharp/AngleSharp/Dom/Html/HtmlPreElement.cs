using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000383 RID: 899
	internal sealed class HtmlPreElement : HtmlElement, IHtmlPreElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x000541A0 File Offset: 0x000523A0
		public HtmlPreElement(Document owner, string prefix = null)
			: base(owner, TagNames.Pre, prefix, NodeFlags.Special | NodeFlags.LineTolerance)
		{
		}
	}
}
