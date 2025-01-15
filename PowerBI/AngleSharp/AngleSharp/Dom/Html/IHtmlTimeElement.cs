using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003F0 RID: 1008
	[DomName("HTMLTimeElement")]
	public interface IHtmlTimeElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600201C RID: 8220
		// (set) Token: 0x0600201D RID: 8221
		[DomName("datetime")]
		string DateTime { get; set; }
	}
}
