using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200031F RID: 799
	[DomName("CSSImportRule")]
	public interface ICssImportRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001701 RID: 5889
		[DomName("href")]
		string Href { get; }

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001702 RID: 5890
		[DomName("media")]
		[DomPutForwards("mediaText")]
		IMediaList Media { get; }

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001703 RID: 5891
		[DomName("styleSheet")]
		ICssStyleSheet Sheet { get; }
	}
}
