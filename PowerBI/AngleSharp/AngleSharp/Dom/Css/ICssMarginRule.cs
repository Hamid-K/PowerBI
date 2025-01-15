using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000322 RID: 802
	[DomName("CSSMarginRule")]
	public interface ICssMarginRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600170F RID: 5903
		[DomName("name")]
		string Name { get; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001710 RID: 5904
		[DomName("style")]
		[DomPutForwards("cssText")]
		ICssStyleDeclaration Style { get; }
	}
}
