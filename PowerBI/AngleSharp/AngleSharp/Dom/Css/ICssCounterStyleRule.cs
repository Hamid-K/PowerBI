using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200031A RID: 794
	[DomName("CSSCounterStyleRule")]
	public interface ICssCounterStyleRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016D5 RID: 5845
		// (set) Token: 0x060016D6 RID: 5846
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016D7 RID: 5847
		// (set) Token: 0x060016D8 RID: 5848
		[DomName("system")]
		string System { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016D9 RID: 5849
		// (set) Token: 0x060016DA RID: 5850
		[DomName("symbols")]
		string Symbols { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016DB RID: 5851
		// (set) Token: 0x060016DC RID: 5852
		[DomName("additiveSymbols")]
		string AdditiveSymbols { get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060016DD RID: 5853
		// (set) Token: 0x060016DE RID: 5854
		[DomName("negative")]
		string Negative { get; set; }

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060016DF RID: 5855
		// (set) Token: 0x060016E0 RID: 5856
		[DomName("prefix")]
		string Prefix { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060016E1 RID: 5857
		// (set) Token: 0x060016E2 RID: 5858
		[DomName("suffix")]
		string Suffix { get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060016E3 RID: 5859
		// (set) Token: 0x060016E4 RID: 5860
		[DomName("range")]
		string Range { get; set; }

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060016E5 RID: 5861
		// (set) Token: 0x060016E6 RID: 5862
		[DomName("pad")]
		string Pad { get; set; }

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060016E7 RID: 5863
		// (set) Token: 0x060016E8 RID: 5864
		[DomName("speakAs")]
		string SpeakAs { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060016E9 RID: 5865
		// (set) Token: 0x060016EA RID: 5866
		[DomName("fallback")]
		string Fallback { get; set; }
	}
}
