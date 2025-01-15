using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000321 RID: 801
	[DomName("CSSKeyframesRule")]
	public interface ICssKeyframesRule : ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001709 RID: 5897
		// (set) Token: 0x0600170A RID: 5898
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600170B RID: 5899
		[DomName("cssRules")]
		ICssRuleList Rules { get; }

		// Token: 0x0600170C RID: 5900
		[DomName("appendRule")]
		void Add(string rule);

		// Token: 0x0600170D RID: 5901
		[DomName("deleteRule")]
		void Remove(string key);

		// Token: 0x0600170E RID: 5902
		[DomName("findRule")]
		ICssKeyframeRule Find(string key);
	}
}
