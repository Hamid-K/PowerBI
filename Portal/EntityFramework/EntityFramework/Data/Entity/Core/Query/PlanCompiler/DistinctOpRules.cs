using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200033D RID: 829
	internal static class DistinctOpRules
	{
		// Token: 0x060027A5 RID: 10149 RVA: 0x000742C0 File Offset: 0x000724C0
		private static bool ProcessDistinctOpOfKeys(RuleProcessingContext context, Node n, out Node newNode)
		{
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			DistinctOp distinctOp = (DistinctOp)n.Op;
			if (!extendedNodeInfo.Keys.NoKeys && distinctOp.Keys.Subsumes(extendedNodeInfo.Keys.KeyVars))
			{
				ProjectOp projectOp = command.CreateProjectOp(distinctOp.Keys);
				VarDefListOp varDefListOp = command.CreateVarDefListOp();
				Node node = command.CreateNode(varDefListOp);
				newNode = command.CreateNode(projectOp, n.Child0, node);
				return true;
			}
			newNode = n;
			return false;
		}

		// Token: 0x04000DCA RID: 3530
		internal static readonly SimpleRule Rule_DistinctOpOfKeys = new SimpleRule(OpType.Distinct, new Rule.ProcessNodeDelegate(DistinctOpRules.ProcessDistinctOpOfKeys));

		// Token: 0x04000DCB RID: 3531
		internal static readonly Rule[] Rules = new Rule[] { DistinctOpRules.Rule_DistinctOpOfKeys };
	}
}
