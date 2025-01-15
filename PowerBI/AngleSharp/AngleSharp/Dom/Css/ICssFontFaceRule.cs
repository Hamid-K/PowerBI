using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200031C RID: 796
	[DomName("CSSFontFaceRule")]
	public interface ICssFontFaceRule : ICssRule, ICssNode, IStyleFormattable, ICssProperties, IEnumerable<ICssProperty>, IEnumerable
	{
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060016EC RID: 5868
		// (set) Token: 0x060016ED RID: 5869
		[DomName("family")]
		string Family { get; set; }

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x060016EE RID: 5870
		// (set) Token: 0x060016EF RID: 5871
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060016F0 RID: 5872
		// (set) Token: 0x060016F1 RID: 5873
		[DomName("style")]
		string Style { get; set; }

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x060016F2 RID: 5874
		// (set) Token: 0x060016F3 RID: 5875
		[DomName("weight")]
		string Weight { get; set; }

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x060016F4 RID: 5876
		// (set) Token: 0x060016F5 RID: 5877
		[DomName("stretch")]
		string Stretch { get; set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x060016F6 RID: 5878
		// (set) Token: 0x060016F7 RID: 5879
		[DomName("unicodeRange")]
		string Range { get; set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060016F8 RID: 5880
		// (set) Token: 0x060016F9 RID: 5881
		[DomName("variant")]
		string Variant { get; set; }

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060016FA RID: 5882
		// (set) Token: 0x060016FB RID: 5883
		[DomName("featureSettings")]
		string Features { get; set; }
	}
}
