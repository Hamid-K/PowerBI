using System;
using System.Collections.Generic;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E2 RID: 994
	[DomName("HTMLSlotElement")]
	public interface IHtmlSlotElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06001FBC RID: 8124
		// (set) Token: 0x06001FBD RID: 8125
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x06001FBE RID: 8126
		[DomName("getDistributedNodes")]
		IEnumerable<INode> GetDistributedNodes();
	}
}
