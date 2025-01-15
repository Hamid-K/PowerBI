using System;
using System.IO;
using System.Linq;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x020002FE RID: 766
	internal sealed class CssKeyframesRule : CssRule, ICssKeyframesRule, ICssRule, ICssNode, IStyleFormattable
	{
		// Token: 0x0600162E RID: 5678 RVA: 0x0004E4FD File Offset: 0x0004C6FD
		internal CssKeyframesRule(CssParser parser)
			: base(CssRuleType.Keyframes, parser)
		{
			this._rules = new CssRuleList(this);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x0004E513 File Offset: 0x0004C713
		// (set) Token: 0x06001630 RID: 5680 RVA: 0x0004E51B File Offset: 0x0004C71B
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x0004E524 File Offset: 0x0004C724
		public CssRuleList Rules
		{
			get
			{
				return this._rules;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x0004E524 File Offset: 0x0004C724
		ICssRuleList ICssKeyframesRule.Rules
		{
			get
			{
				return this._rules;
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004E52C File Offset: 0x0004C72C
		public void Add(string ruleText)
		{
			CssKeyframeRule cssKeyframeRule = base.Parser.ParseKeyframeRule(ruleText);
			this._rules.Add(cssKeyframeRule);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0004E554 File Offset: 0x0004C754
		public void Remove(string key)
		{
			CssKeyframeRule cssKeyframeRule = this.Find(key);
			this._rules.Remove(cssKeyframeRule);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004E578 File Offset: 0x0004C778
		public CssKeyframeRule Find(string key)
		{
			return this._rules.OfType<CssKeyframeRule>().FirstOrDefault((CssKeyframeRule m) => key.Isi(m.KeyText));
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0004E5B0 File Offset: 0x0004C7B0
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = formatter.Block(this.Rules);
			writer.Write(formatter.Rule("@keyframes", this._name, text));
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0004E5E2 File Offset: 0x0004C7E2
		ICssKeyframeRule ICssKeyframesRule.Find(string key)
		{
			return this.Find(key);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0004E5EC File Offset: 0x0004C7EC
		protected override void ReplaceWith(ICssRule rule)
		{
			CssKeyframesRule cssKeyframesRule = rule as CssKeyframesRule;
			this._name = cssKeyframesRule._name;
			base.ReplaceWith(rule);
		}

		// Token: 0x04000C8E RID: 3214
		private readonly CssRuleList _rules;

		// Token: 0x04000C8F RID: 3215
		private string _name;
	}
}
