using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E3 RID: 995
	[DomName("HTMLSourceElement")]
	public interface IHtmlSourceElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06001FBF RID: 8127
		// (set) Token: 0x06001FC0 RID: 8128
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06001FC1 RID: 8129
		// (set) Token: 0x06001FC2 RID: 8130
		[DomName("srcset")]
		string SourceSet { get; set; }

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06001FC3 RID: 8131
		// (set) Token: 0x06001FC4 RID: 8132
		[DomName("sizes")]
		string Sizes { get; set; }

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06001FC5 RID: 8133
		// (set) Token: 0x06001FC6 RID: 8134
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06001FC7 RID: 8135
		// (set) Token: 0x06001FC8 RID: 8136
		[DomName("media")]
		string Media { get; set; }
	}
}
