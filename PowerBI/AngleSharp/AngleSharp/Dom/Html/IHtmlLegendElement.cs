using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003CB RID: 971
	[DomName("HTMLLegendElement")]
	public interface IHtmlLegendElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001EEE RID: 7918
		[DomName("form")]
		IHtmlFormElement Form { get; }
	}
}
