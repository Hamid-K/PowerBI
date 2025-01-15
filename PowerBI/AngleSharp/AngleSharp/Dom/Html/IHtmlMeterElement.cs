using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003D3 RID: 979
	[DomName("HTMLMeterElement")]
	public interface IHtmlMeterElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILabelabelElement
	{
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001F3D RID: 7997
		// (set) Token: 0x06001F3E RID: 7998
		[DomName("value")]
		double Value { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001F3F RID: 7999
		// (set) Token: 0x06001F40 RID: 8000
		[DomName("min")]
		double Minimum { get; set; }

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001F41 RID: 8001
		// (set) Token: 0x06001F42 RID: 8002
		[DomName("max")]
		double Maximum { get; set; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001F43 RID: 8003
		// (set) Token: 0x06001F44 RID: 8004
		[DomName("low")]
		double Low { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001F45 RID: 8005
		// (set) Token: 0x06001F46 RID: 8006
		[DomName("high")]
		double High { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001F47 RID: 8007
		// (set) Token: 0x06001F48 RID: 8008
		[DomName("optimum")]
		double Optimum { get; set; }
	}
}
