using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D0 RID: 976
	[DomName("HTMLMenuElement")]
	public interface IHtmlMenuElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001F24 RID: 7972
		// (set) Token: 0x06001F25 RID: 7973
		[DomName("label")]
		string Label { get; set; }

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001F26 RID: 7974
		// (set) Token: 0x06001F27 RID: 7975
		[DomName("type")]
		string Type { get; set; }
	}
}
