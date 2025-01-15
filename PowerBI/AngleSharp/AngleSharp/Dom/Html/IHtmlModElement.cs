using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D4 RID: 980
	[DomName("HTMLModElement")]
	public interface IHtmlModElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001F49 RID: 8009
		// (set) Token: 0x06001F4A RID: 8010
		[DomName("cite")]
		string Citation { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001F4B RID: 8011
		// (set) Token: 0x06001F4C RID: 8012
		[DomName("datetime")]
		string DateTime { get; set; }
	}
}
