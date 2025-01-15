using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000323 RID: 803
	[DomName("CSSMediaRule")]
	public interface ICssMediaRule : ICssConditionRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001711 RID: 5905
		[DomName("media")]
		[DomPutForwards("mediaText")]
		IMediaList Media { get; }
	}
}
