using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200031E RID: 798
	[DomName("CSSGroupingRule")]
	[DomNoInterfaceObject]
	public interface ICssGroupingRule : ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060016FE RID: 5886
		[DomName("cssRules")]
		ICssRuleList Rules { get; }

		// Token: 0x060016FF RID: 5887
		[DomName("insertRule")]
		int Insert(string rule, int index);

		// Token: 0x06001700 RID: 5888
		[DomName("deleteRule")]
		void RemoveAt(int index);
	}
}
