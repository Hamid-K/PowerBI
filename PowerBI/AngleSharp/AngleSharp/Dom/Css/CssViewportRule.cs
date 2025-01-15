using System;
using AngleSharp.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000307 RID: 775
	internal sealed class CssViewportRule : CssDeclarationRule
	{
		// Token: 0x0600167B RID: 5755 RVA: 0x0004EC4D File Offset: 0x0004CE4D
		internal CssViewportRule(CssParser parser)
			: base(CssRuleType.Viewport, RuleNames.ViewPort, parser)
		{
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0004EC5D File Offset: 0x0004CE5D
		protected override CssProperty CreateNewProperty(string name)
		{
			return Factory.Properties.CreateViewport(name);
		}
	}
}
