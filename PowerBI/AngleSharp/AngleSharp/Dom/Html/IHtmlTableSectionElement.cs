using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003ED RID: 1005
	[DomName("HTMLTableSectionElement")]
	public interface IHtmlTableSectionElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001FF3 RID: 8179
		[DomName("rows")]
		IHtmlCollection<IHtmlTableRowElement> Rows { get; }

		// Token: 0x06001FF4 RID: 8180
		[DomName("insertRow")]
		IHtmlTableRowElement InsertRowAt(int index = -1);

		// Token: 0x06001FF5 RID: 8181
		[DomName("deleteRow")]
		void RemoveRowAt(int index);
	}
}
