using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000330 RID: 816
	[DomName("CSSSupportsRule")]
	public interface ICssSupportsRule : ICssConditionRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060018FF RID: 6399
		IConditionFunction Condition { get; }
	}
}
