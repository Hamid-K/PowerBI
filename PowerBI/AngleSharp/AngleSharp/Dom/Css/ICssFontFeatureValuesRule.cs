using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200031D RID: 797
	[DomName("CSSFontFeatureValuesRule")]
	public interface ICssFontFeatureValuesRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060016FC RID: 5884
		// (set) Token: 0x060016FD RID: 5885
		[DomName("fontFamily")]
		string Family { get; set; }
	}
}
