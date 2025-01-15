using System;
using System.IO;
using System.Linq;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000301 RID: 769
	internal sealed class CssPageRule : CssRule, ICssPageRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x0004E7E8 File Offset: 0x0004C9E8
		internal CssPageRule(CssParser parser)
			: base(CssRuleType.Page, parser)
		{
			base.AppendChild(SimpleSelector.All);
			base.AppendChild(new CssStyleDeclaration(this));
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x0004E809 File Offset: 0x0004CA09
		// (set) Token: 0x0600164B RID: 5707 RVA: 0x0004E816 File Offset: 0x0004CA16
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

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x0004E82A File Offset: 0x0004CA2A
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x0004E83C File Offset: 0x0004CA3C
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

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0004E84B File Offset: 0x0004CA4B
		ICssStyleDeclaration ICssPageRule.Style
		{
			get
			{
				return this.Style;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x0004E4D1 File Offset: 0x0004C6D1
		public CssStyleDeclaration Style
		{
			get
			{
				return base.Children.OfType<CssStyleDeclaration>().FirstOrDefault<CssStyleDeclaration>();
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0004E854 File Offset: 0x0004CA54
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = formatter.Block(this.Style);
			writer.Write(formatter.Rule("@page", this.SelectorText, text));
		}
	}
}
