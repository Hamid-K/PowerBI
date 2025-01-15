using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D8 RID: 984
	[DomName("HTMLOptGroupElement")]
	public interface IHtmlOptionsGroupElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001F73 RID: 8051
		// (set) Token: 0x06001F74 RID: 8052
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001F75 RID: 8053
		// (set) Token: 0x06001F76 RID: 8054
		[DomName("label")]
		string Label { get; set; }
	}
}
