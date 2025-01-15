using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006EA RID: 1770
	internal abstract class DbExpressionRuleProcessingVisitor : DefaultExpressionVisitor
	{
		// Token: 0x060051FB RID: 20987
		protected abstract IEnumerable<DbExpressionRule> GetRules();

		// Token: 0x060051FC RID: 20988 RVA: 0x00125784 File Offset: 0x00123984
		private static Tuple<DbExpression, DbExpressionRule.ProcessedAction> ProcessRules(DbExpression expression, List<DbExpressionRule> rules)
		{
			for (int i = 0; i < rules.Count; i++)
			{
				DbExpressionRule dbExpressionRule = rules[i];
				DbExpression dbExpression;
				if (dbExpressionRule.ShouldProcess(expression) && dbExpressionRule.TryProcess(expression, out dbExpression))
				{
					if (dbExpressionRule.OnExpressionProcessed != DbExpressionRule.ProcessedAction.Continue)
					{
						return Tuple.Create<DbExpression, DbExpressionRule.ProcessedAction>(dbExpression, dbExpressionRule.OnExpressionProcessed);
					}
					expression = dbExpression;
				}
			}
			return Tuple.Create<DbExpression, DbExpressionRule.ProcessedAction>(expression, DbExpressionRule.ProcessedAction.Continue);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x001257E0 File Offset: 0x001239E0
		private DbExpression ApplyRules(DbExpression expression)
		{
			List<DbExpressionRule> list = this.GetRules().ToList<DbExpressionRule>();
			Tuple<DbExpression, DbExpressionRule.ProcessedAction> tuple = DbExpressionRuleProcessingVisitor.ProcessRules(expression, list);
			while (tuple.Item2 == DbExpressionRule.ProcessedAction.Reset)
			{
				list = this.GetRules().ToList<DbExpressionRule>();
				tuple = DbExpressionRuleProcessingVisitor.ProcessRules(tuple.Item1, list);
			}
			if (tuple.Item2 == DbExpressionRule.ProcessedAction.Stop)
			{
				this._stopped = true;
			}
			return tuple.Item1;
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x0012583C File Offset: 0x00123A3C
		protected override DbExpression VisitExpression(DbExpression expression)
		{
			DbExpression dbExpression = this.ApplyRules(expression);
			if (this._stopped)
			{
				return dbExpression;
			}
			dbExpression = base.VisitExpression(dbExpression);
			if (this._stopped)
			{
				return dbExpression;
			}
			return this.ApplyRules(dbExpression);
		}

		// Token: 0x04001DD8 RID: 7640
		private bool _stopped;
	}
}
