using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200032D RID: 813
	internal static class AggregatePushdownUtil
	{
		// Token: 0x060026E7 RID: 9959 RVA: 0x00070375 File Offset: 0x0006E575
		internal static bool IsVarRefOverGivenVar(Node node, Var var)
		{
			return node.Op.OpType == OpType.VarRef && ((VarRefOp)node.Op).Var == var;
		}
	}
}
