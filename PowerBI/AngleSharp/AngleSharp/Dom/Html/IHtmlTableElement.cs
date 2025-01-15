using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003EA RID: 1002
	[DomName("HTMLTableElement")]
	public interface IHtmlTableElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001FD9 RID: 8153
		// (set) Token: 0x06001FDA RID: 8154
		[DomName("caption")]
		IHtmlTableCaptionElement Caption { get; set; }

		// Token: 0x06001FDB RID: 8155
		[DomName("createCaption")]
		IHtmlTableCaptionElement CreateCaption();

		// Token: 0x06001FDC RID: 8156
		[DomName("deleteCaption")]
		void DeleteCaption();

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001FDD RID: 8157
		// (set) Token: 0x06001FDE RID: 8158
		[DomName("tHead")]
		IHtmlTableSectionElement Head { get; set; }

		// Token: 0x06001FDF RID: 8159
		[DomName("createTHead")]
		IHtmlTableSectionElement CreateHead();

		// Token: 0x06001FE0 RID: 8160
		[DomName("deleteTHead")]
		void DeleteHead();

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001FE1 RID: 8161
		// (set) Token: 0x06001FE2 RID: 8162
		[DomName("tFoot")]
		IHtmlTableSectionElement Foot { get; set; }

		// Token: 0x06001FE3 RID: 8163
		[DomName("createTFoot")]
		IHtmlTableSectionElement CreateFoot();

		// Token: 0x06001FE4 RID: 8164
		[DomName("deleteTFoot")]
		void DeleteFoot();

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001FE5 RID: 8165
		[DomName("tBodies")]
		IHtmlCollection<IHtmlTableSectionElement> Bodies { get; }

		// Token: 0x06001FE6 RID: 8166
		[DomName("createTBody")]
		IHtmlTableSectionElement CreateBody();

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001FE7 RID: 8167
		[DomName("rows")]
		IHtmlCollection<IHtmlTableRowElement> Rows { get; }

		// Token: 0x06001FE8 RID: 8168
		[DomName("insertRow")]
		IHtmlTableRowElement InsertRowAt(int index = -1);

		// Token: 0x06001FE9 RID: 8169
		[DomName("deleteRow")]
		void RemoveRowAt(int index);

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001FEA RID: 8170
		// (set) Token: 0x06001FEB RID: 8171
		[DomName("border")]
		uint Border { get; set; }
	}
}
