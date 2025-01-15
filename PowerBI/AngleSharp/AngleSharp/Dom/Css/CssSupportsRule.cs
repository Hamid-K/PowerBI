using System;
using System.IO;
using System.Linq;
using AngleSharp.Css;
using AngleSharp.Extensions;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000305 RID: 773
	internal sealed class CssSupportsRule : CssConditionRule, ICssSupportsRule, ICssConditionRule, ICssGroupingRule, ICssRule, ICssNode, IStyleFormattable, ICssRuleCreator
	{
		// Token: 0x06001671 RID: 5745 RVA: 0x0004EB61 File Offset: 0x0004CD61
		internal CssSupportsRule(CssParser parser)
			: base(CssRuleType.Supports, parser)
		{
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x0004EB6C File Offset: 0x0004CD6C
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x0004EB7C File Offset: 0x0004CD7C
		public string ConditionText
		{
			get
			{
				return this.Condition.ToCss();
			}
			set
			{
				IConditionFunction conditionFunction = base.Parser.ParseCondition(value);
				if (conditionFunction == null)
				{
					throw new DomException(DomError.Syntax);
				}
				this.Condition = conditionFunction;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0004EBA8 File Offset: 0x0004CDA8
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x0004EBC3 File Offset: 0x0004CDC3
		public IConditionFunction Condition
		{
			get
			{
				return base.Children.OfType<IConditionFunction>().FirstOrDefault<IConditionFunction>() ?? new EmptyCondition();
			}
			set
			{
				if (value != null)
				{
					base.RemoveChild(this.Condition);
					base.AppendChild(value);
				}
			}
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0004EBDB File Offset: 0x0004CDDB
		internal override bool IsValid(RenderDevice device)
		{
			return this.Condition.Check();
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x0004EBE8 File Offset: 0x0004CDE8
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			string text = formatter.Block(base.Rules);
			writer.Write(formatter.Rule("@supports", this.ConditionText, text));
		}
	}
}
