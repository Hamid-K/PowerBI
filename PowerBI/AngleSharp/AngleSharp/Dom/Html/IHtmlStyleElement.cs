using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E5 RID: 997
	[DomName("HTMLStyleElement")]
	public interface IHtmlStyleElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILinkStyle
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06001FC9 RID: 8137
		// (set) Token: 0x06001FCA RID: 8138
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06001FCB RID: 8139
		// (set) Token: 0x06001FCC RID: 8140
		[DomName("media")]
		string Media { get; set; }

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06001FCD RID: 8141
		// (set) Token: 0x06001FCE RID: 8142
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06001FCF RID: 8143
		// (set) Token: 0x06001FD0 RID: 8144
		[DomName("scoped")]
		bool IsScoped { get; set; }
	}
}
