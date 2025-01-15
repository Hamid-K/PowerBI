using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003CE RID: 974
	[DomName("HTMLMapElement")]
	public interface IHtmlMapElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001F03 RID: 7939
		// (set) Token: 0x06001F04 RID: 7940
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001F05 RID: 7941
		[DomName("areas")]
		IHtmlCollection<IHtmlAreaElement> Areas { get; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001F06 RID: 7942
		[DomName("images")]
		IHtmlCollection<IHtmlImageElement> Images { get; }
	}
}
