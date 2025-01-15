using System;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x020006E9 RID: 1769
	internal abstract class DbExpressionRule
	{
		// Token: 0x060051F7 RID: 20983
		internal abstract bool ShouldProcess(DbExpression expression);

		// Token: 0x060051F8 RID: 20984
		internal abstract bool TryProcess(DbExpression expression, out DbExpression result);

		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x060051F9 RID: 20985
		internal abstract DbExpressionRule.ProcessedAction OnExpressionProcessed { get; }

		// Token: 0x02000C96 RID: 3222
		internal enum ProcessedAction
		{
			// Token: 0x040031B0 RID: 12720
			Continue,
			// Token: 0x040031B1 RID: 12721
			Reset,
			// Token: 0x040031B2 RID: 12722
			Stop
		}
	}
}
