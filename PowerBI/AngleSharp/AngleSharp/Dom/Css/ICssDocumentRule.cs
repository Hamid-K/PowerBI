using System;
using System.Collections.Generic;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200031B RID: 795
	[DomName("CSSDocumentRule")]
	public interface ICssDocumentRule : ICssConditionRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060016EB RID: 5867
		IEnumerable<IDocumentFunction> Conditions { get; }
	}
}
