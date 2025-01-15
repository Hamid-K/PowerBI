using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000365 RID: 869
	internal static class SetOpRules
	{
		// Token: 0x06002A47 RID: 10823 RVA: 0x0008A234 File Offset: 0x00088434
		private static bool ProcessSetOpOverEmptySet(RuleProcessingContext context, Node setOpNode, out Node newNode)
		{
			bool flag = context.Command.GetExtendedNodeInfo(setOpNode.Child0).MaxRows == RowCount.Zero;
			bool flag2 = context.Command.GetExtendedNodeInfo(setOpNode.Child1).MaxRows == RowCount.Zero;
			if (!flag && !flag2)
			{
				newNode = setOpNode;
				return false;
			}
			SetOp setOp = (SetOp)setOpNode.Op;
			int num;
			if ((!flag2 && setOp.OpType == OpType.UnionAll) || (!flag && setOp.OpType == OpType.Intersect))
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			newNode = setOpNode.Children[num];
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			foreach (KeyValuePair<Var, Var> keyValuePair in setOp.VarMap[num])
			{
				transformationRulesContext.AddVarMapping(keyValuePair.Key, keyValuePair.Value);
			}
			return true;
		}

		// Token: 0x04000E8C RID: 3724
		internal static readonly SimpleRule Rule_UnionAllOverEmptySet = new SimpleRule(OpType.UnionAll, new Rule.ProcessNodeDelegate(SetOpRules.ProcessSetOpOverEmptySet));

		// Token: 0x04000E8D RID: 3725
		internal static readonly SimpleRule Rule_IntersectOverEmptySet = new SimpleRule(OpType.Intersect, new Rule.ProcessNodeDelegate(SetOpRules.ProcessSetOpOverEmptySet));

		// Token: 0x04000E8E RID: 3726
		internal static readonly SimpleRule Rule_ExceptOverEmptySet = new SimpleRule(OpType.Except, new Rule.ProcessNodeDelegate(SetOpRules.ProcessSetOpOverEmptySet));

		// Token: 0x04000E8F RID: 3727
		internal static readonly Rule[] Rules = new Rule[]
		{
			SetOpRules.Rule_UnionAllOverEmptySet,
			SetOpRules.Rule_IntersectOverEmptySet,
			SetOpRules.Rule_ExceptOverEmptySet
		};
	}
}
