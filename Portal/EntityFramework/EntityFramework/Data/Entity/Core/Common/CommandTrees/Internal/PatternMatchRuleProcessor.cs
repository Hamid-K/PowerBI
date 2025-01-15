using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006F1 RID: 1777
	internal class PatternMatchRuleProcessor : DbExpressionRuleProcessingVisitor
	{
		// Token: 0x06005298 RID: 21144 RVA: 0x001289B0 File Offset: 0x00126BB0
		private PatternMatchRuleProcessor(ReadOnlyCollection<PatternMatchRule> rules)
		{
			this.ruleSet = rules;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x001289BF File Offset: 0x00126BBF
		private DbExpression Process(DbExpression expression)
		{
			expression = this.VisitExpression(expression);
			return expression;
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x001289CB File Offset: 0x00126BCB
		protected override IEnumerable<DbExpressionRule> GetRules()
		{
			return this.ruleSet;
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x001289D3 File Offset: 0x00126BD3
		internal static Func<DbExpression, DbExpression> Create(params PatternMatchRule[] rules)
		{
			return new Func<DbExpression, DbExpression>(new PatternMatchRuleProcessor(new ReadOnlyCollection<PatternMatchRule>(rules)).Process);
		}

		// Token: 0x04001DE0 RID: 7648
		private readonly ReadOnlyCollection<PatternMatchRule> ruleSet;
	}
}
