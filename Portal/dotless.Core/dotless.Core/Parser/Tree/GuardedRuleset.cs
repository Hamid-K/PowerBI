using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000038 RID: 56
	public class GuardedRuleset : Ruleset
	{
		// Token: 0x0600021E RID: 542 RVA: 0x0000A891 File Offset: 0x00008A91
		public GuardedRuleset(NodeList<Selector> selectors, NodeList rules, Condition condition)
			: base(selectors, rules)
		{
			this.Condition = condition;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000A8A2 File Offset: 0x00008AA2
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000A8AA File Offset: 0x00008AAA
		public Condition Condition { get; set; }

		// Token: 0x06000221 RID: 545 RVA: 0x0000A8B3 File Offset: 0x00008AB3
		public override Node Evaluate(Env env)
		{
			if (this.Condition.Passes(env))
			{
				return base.EvaluateRulesForFrame(this, env);
			}
			return new NodeList();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		public override void Accept(IVisitor visitor)
		{
			base.Accept(visitor);
			this.Condition = base.VisitAndReplace<Condition>(this.Condition, visitor);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A8ED File Offset: 0x00008AED
		public override void AppendCSS(Env env)
		{
		}
	}
}
