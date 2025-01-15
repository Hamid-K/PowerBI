using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003DE RID: 990
	[DomName("HTMLProgressElement")]
	public interface IHtmlProgressElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILabelabelElement
	{
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001F8B RID: 8075
		// (set) Token: 0x06001F8C RID: 8076
		[DomName("value")]
		double Value { get; set; }

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001F8D RID: 8077
		// (set) Token: 0x06001F8E RID: 8078
		[DomName("max")]
		double Maximum { get; set; }

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001F8F RID: 8079
		[DomName("position")]
		double Position { get; }
	}
}
