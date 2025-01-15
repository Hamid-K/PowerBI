using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200032F RID: 815
	[DomName("CSSStyleSheet")]
	public interface ICssStyleSheet : IStyleSheet, IStyleFormattable, ICssNode, ICssRuleCreator
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060018FA RID: 6394
		[DomName("ownerRule")]
		ICssRule OwnerRule { get; }

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060018FB RID: 6395
		[DomName("cssRules")]
		ICssRuleList Rules { get; }

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060018FC RID: 6396
		[DomName("parentStyleSheet")]
		ICssStyleSheet Parent { get; }

		// Token: 0x060018FD RID: 6397
		[DomName("insertRule")]
		int Insert(string rule, int index);

		// Token: 0x060018FE RID: 6398
		[DomName("deleteRule")]
		void RemoveAt(int index);
	}
}
