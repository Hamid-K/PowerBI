using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000368 RID: 872
	internal static class SortOpRules
	{
		// Token: 0x06002A51 RID: 10833 RVA: 0x0008A5A8 File Offset: 0x000887A8
		private static bool ProcessSortOpOverAtMostOneRow(RuleProcessingContext context, Node n, out Node newNode)
		{
			ExtendedNodeInfo extendedNodeInfo = context.Command.GetExtendedNodeInfo(n.Child0);
			if (extendedNodeInfo.MaxRows == RowCount.Zero || extendedNodeInfo.MaxRows == RowCount.One)
			{
				newNode = n.Child0;
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x04000E94 RID: 3732
		internal static readonly SimpleRule Rule_SortOpOverAtMostOneRow = new SimpleRule(OpType.Sort, new Rule.ProcessNodeDelegate(SortOpRules.ProcessSortOpOverAtMostOneRow));

		// Token: 0x04000E95 RID: 3733
		internal static readonly Rule[] Rules = new Rule[] { SortOpRules.Rule_SortOpOverAtMostOneRow };
	}
}
