using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003EC RID: 1004
	[DomName("HTMLTableRowElement")]
	public interface IHtmlTableRowElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001FEE RID: 8174
		[DomName("rowIndex")]
		int Index { get; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001FEF RID: 8175
		[DomName("sectionRowIndex")]
		int IndexInSection { get; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001FF0 RID: 8176
		[DomName("cells")]
		IHtmlCollection<IHtmlTableCellElement> Cells { get; }

		// Token: 0x06001FF1 RID: 8177
		[DomName("insertCell")]
		IHtmlTableCellElement InsertCellAt(int index = -1);

		// Token: 0x06001FF2 RID: 8178
		[DomName("deleteCell")]
		void RemoveCellAt(int index);
	}
}
