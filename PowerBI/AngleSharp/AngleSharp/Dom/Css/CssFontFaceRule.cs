using System;
using System.Collections;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002FA RID: 762
	internal sealed class CssFontFaceRule : CssDeclarationRule, ICssFontFaceRule, ICssRule, ICssNode, IStyleFormattable, ICssProperties, IEnumerable<ICssProperty>, IEnumerable
	{
		// Token: 0x06001604 RID: 5636 RVA: 0x0004E1A2 File Offset: 0x0004C3A2
		internal CssFontFaceRule(CssParser parser)
			: base(CssRuleType.FontFace, RuleNames.FontFace, parser)
		{
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0004E1B1 File Offset: 0x0004C3B1
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x0004E1BE File Offset: 0x0004C3BE
		string ICssFontFaceRule.Family
		{
			get
			{
				return base.GetValue(PropertyNames.FontFamily);
			}
			set
			{
				base.SetValue(PropertyNames.FontFamily, value);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x0004E1CC File Offset: 0x0004C3CC
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x0004E1D9 File Offset: 0x0004C3D9
		string ICssFontFaceRule.Source
		{
			get
			{
				return base.GetValue(PropertyNames.Src);
			}
			set
			{
				base.SetValue(PropertyNames.Src, value);
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0004E1E7 File Offset: 0x0004C3E7
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0004E1F4 File Offset: 0x0004C3F4
		string ICssFontFaceRule.Style
		{
			get
			{
				return base.GetValue(PropertyNames.FontStyle);
			}
			set
			{
				base.SetValue(PropertyNames.FontStyle, value);
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x0004E202 File Offset: 0x0004C402
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x0004E20F File Offset: 0x0004C40F
		string ICssFontFaceRule.Weight
		{
			get
			{
				return base.GetValue(PropertyNames.FontWeight);
			}
			set
			{
				base.SetValue(PropertyNames.FontWeight, value);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0004E21D File Offset: 0x0004C41D
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x0004E22A File Offset: 0x0004C42A
		string ICssFontFaceRule.Stretch
		{
			get
			{
				return base.GetValue(PropertyNames.FontStretch);
			}
			set
			{
				base.SetValue(PropertyNames.FontStretch, value);
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0004E238 File Offset: 0x0004C438
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x0004E245 File Offset: 0x0004C445
		string ICssFontFaceRule.Range
		{
			get
			{
				return base.GetValue(PropertyNames.UnicodeRange);
			}
			set
			{
				base.SetValue(PropertyNames.UnicodeRange, value);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0004E253 File Offset: 0x0004C453
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x0004E260 File Offset: 0x0004C460
		string ICssFontFaceRule.Variant
		{
			get
			{
				return base.GetValue(PropertyNames.FontVariant);
			}
			set
			{
				base.SetValue(PropertyNames.FontVariant, value);
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0004280F File Offset: 0x00040A0F
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x00003C25 File Offset: 0x00001E25
		string ICssFontFaceRule.Features
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004E26E File Offset: 0x0004C46E
		protected override CssProperty CreateNewProperty(string name)
		{
			return Factory.Properties.CreateFont(name);
		}
	}
}
