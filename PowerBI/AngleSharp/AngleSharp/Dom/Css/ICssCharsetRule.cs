using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000317 RID: 791
	[DomName("CSSCharsetRule")]
	public interface ICssCharsetRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060016D0 RID: 5840
		// (set) Token: 0x060016D1 RID: 5841
		[DomName("encoding")]
		string CharacterSet { get; set; }
	}
}
