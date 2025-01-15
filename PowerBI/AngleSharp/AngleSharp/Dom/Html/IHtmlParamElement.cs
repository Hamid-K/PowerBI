using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003DC RID: 988
	[DomName("HTMLParamElement")]
	public interface IHtmlParamElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001F87 RID: 8071
		// (set) Token: 0x06001F88 RID: 8072
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001F89 RID: 8073
		// (set) Token: 0x06001F8A RID: 8074
		[DomName("value")]
		string Value { get; set; }
	}
}
