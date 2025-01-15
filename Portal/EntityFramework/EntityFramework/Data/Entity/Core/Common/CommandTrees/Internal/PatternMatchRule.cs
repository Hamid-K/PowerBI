using System;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006F0 RID: 1776
	internal class PatternMatchRule : DbExpressionRule
	{
		// Token: 0x06005292 RID: 21138 RVA: 0x00128954 File Offset: 0x00126B54
		private PatternMatchRule(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor, DbExpressionRule.ProcessedAction onProcessed)
		{
			this.isMatch = matchFunc;
			this.process = processor;
			this.processed = onProcessed;
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00128971 File Offset: 0x00126B71
		internal override bool ShouldProcess(DbExpression expression)
		{
			return this.isMatch(expression);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0012897F File Offset: 0x00126B7F
		internal override bool TryProcess(DbExpression expression, out DbExpression result)
		{
			result = this.process(expression);
			return result != null;
		}

		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x00128994 File Offset: 0x00126B94
		internal override DbExpressionRule.ProcessedAction OnExpressionProcessed
		{
			get
			{
				return this.processed;
			}
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0012899C File Offset: 0x00126B9C
		internal static PatternMatchRule Create(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor)
		{
			return PatternMatchRule.Create(matchFunc, processor, DbExpressionRule.ProcessedAction.Reset);
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x001289A6 File Offset: 0x00126BA6
		internal static PatternMatchRule Create(Func<DbExpression, bool> matchFunc, Func<DbExpression, DbExpression> processor, DbExpressionRule.ProcessedAction onProcessed)
		{
			return new PatternMatchRule(matchFunc, processor, onProcessed);
		}

		// Token: 0x04001DDD RID: 7645
		private readonly Func<DbExpression, bool> isMatch;

		// Token: 0x04001DDE RID: 7646
		private readonly Func<DbExpression, DbExpression> process;

		// Token: 0x04001DDF RID: 7647
		private readonly DbExpressionRule.ProcessedAction processed;
	}
}
