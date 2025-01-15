using System;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x02000361 RID: 865
	internal sealed class HtmlHeadingElement : HtmlElement, IHtmlHeadingElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x000526E9 File Offset: 0x000508E9
		public HtmlHeadingElement(Document owner, string name = null, string prefix = null)
			: base(owner, name ?? TagNames.H1, prefix, NodeFlags.Special)
		{
		}
	}
}
