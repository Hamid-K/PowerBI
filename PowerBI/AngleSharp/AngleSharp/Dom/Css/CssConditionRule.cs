using System;
using AngleSharp.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F7 RID: 759
	internal abstract class CssConditionRule : CssGroupingRule
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x0004DE01 File Offset: 0x0004C001
		internal CssConditionRule(CssRuleType type, CssParser parser)
			: base(type, parser)
		{
		}

		// Token: 0x060015EE RID: 5614
		internal abstract bool IsValid(RenderDevice device);
	}
}
