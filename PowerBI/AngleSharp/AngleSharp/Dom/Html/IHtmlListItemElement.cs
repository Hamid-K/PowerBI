using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003CD RID: 973
	[DomName("HTMLLIElement")]
	public interface IHtmlListItemElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001F01 RID: 7937
		// (set) Token: 0x06001F02 RID: 7938
		[DomName("value")]
		int? Value { get; set; }
	}
}
