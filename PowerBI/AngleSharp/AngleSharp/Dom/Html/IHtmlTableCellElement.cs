using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003E7 RID: 999
	[DomName("HTMLTableCellElement")]
	public interface IHtmlTableCellElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06001FD1 RID: 8145
		// (set) Token: 0x06001FD2 RID: 8146
		[DomName("colSpan")]
		int ColumnSpan { get; set; }

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06001FD3 RID: 8147
		// (set) Token: 0x06001FD4 RID: 8148
		[DomName("rowSpan")]
		int RowSpan { get; set; }

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06001FD5 RID: 8149
		[DomName("headers")]
		ISettableTokenList Headers { get; }

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06001FD6 RID: 8150
		[DomName("cellIndex")]
		int Index { get; }
	}
}
