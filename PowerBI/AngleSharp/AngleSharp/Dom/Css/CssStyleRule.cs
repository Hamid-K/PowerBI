using System;
using System.IO;
using System.Linq;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000304 RID: 772
	internal sealed class CssStyleRule : CssRule, ICssStyleRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x06001669 RID: 5737 RVA: 0x0004EAEE File Offset: 0x0004CCEE
		internal CssStyleRule(CssParser parser)
			: base(CssRuleType.Style, parser)
		{
			base.AppendChild(SimpleSelector.All);
			base.AppendChild(new CssStyleDeclaration(this));
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0004E82A File Offset: 0x0004CA2A
		// (set) Token: 0x0600166B RID: 5739 RVA: 0x0004EB0F File Offset: 0x0004CD0F
		public ISelector Selector
		{
			get
			{
				return base.Children.OfType<ISelector>().FirstOrDefault<ISelector>();
			}
			set
			{
				base.ReplaceSingle(this.Selector, value);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x0004EB1E File Offset: 0x0004CD1E
		// (set) Token: 0x0600166D RID: 5741 RVA: 0x0004EB2B File Offset: 0x0004CD2B
		public string SelectorText
		{
			get
			{
				return this.Selector.Text;
			}
			set
			{
				this.Selector = base.Parser.ParseSelector(value);
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0004EB3F File Offset: 0x0004CD3F
		ICssStyleDeclaration ICssStyleRule.Style
		{
			get
			{
				return this.Style;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0004E4D1 File Offset: 0x0004C6D1
		public CssStyleDeclaration Style
		{
			get
			{
				return base.Children.OfType<CssStyleDeclaration>().FirstOrDefault<CssStyleDeclaration>();
			}
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0004EB47 File Offset: 0x0004CD47
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Style(this.SelectorText, this.Style));
		}
	}
}
