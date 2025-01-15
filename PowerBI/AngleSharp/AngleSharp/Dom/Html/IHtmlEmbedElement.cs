using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003BE RID: 958
	[DomName("HTMLEmbedElement")]
	public interface IHtmlEmbedElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILoadableElement
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001E37 RID: 7735
		// (set) Token: 0x06001E38 RID: 7736
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001E39 RID: 7737
		// (set) Token: 0x06001E3A RID: 7738
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001E3B RID: 7739
		// (set) Token: 0x06001E3C RID: 7740
		[DomName("width")]
		string DisplayWidth { get; set; }

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001E3D RID: 7741
		// (set) Token: 0x06001E3E RID: 7742
		[DomName("height")]
		string DisplayHeight { get; set; }
	}
}
