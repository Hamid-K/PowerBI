using System;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002FB RID: 763
	internal abstract class CssGroupingRule : CssRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x0004E27B File Offset: 0x0004C47B
		internal CssGroupingRule(CssRuleType type, CssParser parser)
			: base(type, parser)
		{
			this._rules = new CssRuleList(this);
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0004E291 File Offset: 0x0004C491
		public CssRuleList Rules
		{
			get
			{
				return this._rules;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0004E299 File Offset: 0x0004C499
		ICssRuleList ICssGroupingRule.Rules
		{
			get
			{
				return this.Rules;
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		public ICssRule AddNewRule(CssRuleType ruleType)
		{
			CssRule cssRule = base.Parser.CreateRule(ruleType);
			this.Rules.Add(cssRule);
			return cssRule;
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0004E2CC File Offset: 0x0004C4CC
		public int Insert(string ruleText, int index)
		{
			CssRule cssRule = base.Parser.ParseRule(ruleText);
			this.Rules.Insert(index, cssRule);
			return index;
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0004E2F4 File Offset: 0x0004C4F4
		public void RemoveAt(int index)
		{
			this.Rules.RemoveAt(index);
		}

		// Token: 0x04000C8B RID: 3211
		private readonly CssRuleList _rules;
	}
}
