using System;
using System.IO;
using System.Linq;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002FD RID: 765
	internal sealed class CssKeyframeRule : CssRule, ICssKeyframeRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x06001626 RID: 5670 RVA: 0x0004E459 File Offset: 0x0004C659
		internal CssKeyframeRule(CssParser parser)
			: base(CssRuleType.Keyframe, parser)
		{
			base.AppendChild(new CssStyleDeclaration(this));
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0004E46F File Offset: 0x0004C66F
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0004E47C File Offset: 0x0004C67C
		public string KeyText
		{
			get
			{
				return this.Key.Text;
			}
			set
			{
				IKeyframeSelector keyframeSelector = base.Parser.ParseKeyframeSelector(value);
				if (keyframeSelector == null)
				{
					throw new DomException(DomError.Syntax);
				}
				this.Key = keyframeSelector;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x0004E4A8 File Offset: 0x0004C6A8
		// (set) Token: 0x0600162A RID: 5674 RVA: 0x0004E4BA File Offset: 0x0004C6BA
		public IKeyframeSelector Key
		{
			get
			{
				return base.Children.OfType<IKeyframeSelector>().FirstOrDefault<IKeyframeSelector>();
			}
			set
			{
				base.ReplaceSingle(this.Key, value);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0004E4C9 File Offset: 0x0004C6C9
		ICssStyleDeclaration ICssKeyframeRule.Style
		{
			get
			{
				return this.Style;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0004E4D1 File Offset: 0x0004C6D1
		public CssStyleDeclaration Style
		{
			get
			{
				return base.Children.OfType<CssStyleDeclaration>().FirstOrDefault<CssStyleDeclaration>();
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0004E4E3 File Offset: 0x0004C6E3
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Style(this.KeyText, this.Style));
		}
	}
}
