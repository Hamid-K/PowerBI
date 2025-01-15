using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003B6 RID: 950
	[DomName("HTMLCommandElement")]
	public interface IHtmlCommandElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001DFE RID: 7678
		// (set) Token: 0x06001DFF RID: 7679
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001E00 RID: 7680
		// (set) Token: 0x06001E01 RID: 7681
		[DomName("label")]
		string Label { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001E02 RID: 7682
		// (set) Token: 0x06001E03 RID: 7683
		[DomName("icon")]
		string Icon { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001E04 RID: 7684
		// (set) Token: 0x06001E05 RID: 7685
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001E06 RID: 7686
		// (set) Token: 0x06001E07 RID: 7687
		[DomName("checked")]
		bool IsChecked { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001E08 RID: 7688
		// (set) Token: 0x06001E09 RID: 7689
		[DomName("radiogroup")]
		string RadioGroup { get; set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001E0A RID: 7690
		[DomName("command")]
		IHtmlElement Command { get; }
	}
}
