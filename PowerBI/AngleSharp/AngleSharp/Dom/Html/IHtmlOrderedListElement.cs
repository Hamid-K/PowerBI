using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D9 RID: 985
	[DomName("HTMLOListElement")]
	public interface IHtmlOrderedListElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001F77 RID: 8055
		// (set) Token: 0x06001F78 RID: 8056
		[DomName("reversed")]
		bool IsReversed { get; set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001F79 RID: 8057
		// (set) Token: 0x06001F7A RID: 8058
		[DomName("start")]
		int Start { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001F7B RID: 8059
		// (set) Token: 0x06001F7C RID: 8060
		[DomName("type")]
		string Type { get; set; }
	}
}
