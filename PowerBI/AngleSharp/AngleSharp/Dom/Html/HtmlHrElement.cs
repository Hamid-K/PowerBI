using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000362 RID: 866
	internal sealed class HtmlHrElement : HtmlElement, IHtmlHrElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001AC1 RID: 6849 RVA: 0x000526FE File Offset: 0x000508FE
		public HtmlHrElement(Document owner, string prefix = null)
			: base(owner, TagNames.Hr, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}
	}
}
