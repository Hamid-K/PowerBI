using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000339 RID: 825
	internal static class ConstrainedSortOpRules
	{
		// Token: 0x06002740 RID: 10048 RVA: 0x00072319 File Offset: 0x00070519
		private static bool ProcessConstrainedSortOpOverEmptySet(RuleProcessingContext context, Node n, out Node newNode)
		{
			if (context.Command.GetExtendedNodeInfo(n.Child0).MaxRows == RowCount.Zero)
			{
				newNode = n.Child0;
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x04000DAD RID: 3501
		internal static readonly SimpleRule Rule_ConstrainedSortOpOverEmptySet = new SimpleRule(OpType.ConstrainedSort, new Rule.ProcessNodeDelegate(ConstrainedSortOpRules.ProcessConstrainedSortOpOverEmptySet));

		// Token: 0x04000DAE RID: 3502
		internal static readonly Rule[] Rules = new Rule[] { ConstrainedSortOpRules.Rule_ConstrainedSortOpOverEmptySet };
	}
}
