using System;
using System.IO;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002F6 RID: 758
	internal sealed class CssCharsetRule : CssRule, ICssCharsetRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x060015E8 RID: 5608 RVA: 0x0004DD9E File Offset: 0x0004BF9E
		internal CssCharsetRule(CssParser parser)
			: base(CssRuleType.Charset, parser)
		{
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x0004DDA8 File Offset: 0x0004BFA8
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x0004DDB0 File Offset: 0x0004BFB0
		public string CharacterSet { get; set; }

		// Token: 0x060015EB RID: 5611 RVA: 0x0004DDBC File Offset: 0x0004BFBC
		protected override void ReplaceWith(ICssRule rule)
		{
			CssCharsetRule cssCharsetRule = rule as CssCharsetRule;
			this.CharacterSet = cssCharsetRule.CharacterSet;
			base.ReplaceWith(rule);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0004DDE3 File Offset: 0x0004BFE3
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Rule("@charset", this.CharacterSet.CssString()));
		}
	}
}
