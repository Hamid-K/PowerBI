using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000319 RID: 793
	[DomName("CSSConditionRule")]
	[DomNoInterfaceObject]
	public interface ICssConditionRule : ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016D3 RID: 5843
		// (set) Token: 0x060016D4 RID: 5844
		[DomName("conditionText")]
		string ConditionText { get; set; }
	}
}
