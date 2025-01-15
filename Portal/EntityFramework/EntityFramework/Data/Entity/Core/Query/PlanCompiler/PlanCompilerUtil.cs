using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000358 RID: 856
	internal static class PlanCompilerUtil
	{
		// Token: 0x06002968 RID: 10600 RVA: 0x000847B0 File Offset: 0x000829B0
		internal static bool IsRowTypeCaseOpWithNullability(CaseOp op, Node n, out bool thenClauseIsNull)
		{
			thenClauseIsNull = false;
			if (!TypeSemantics.IsRowType(op.Type))
			{
				return false;
			}
			if (n.Children.Count != 3)
			{
				return false;
			}
			if (!n.Child1.Op.Type.EdmEquals(op.Type) || !n.Child2.Op.Type.EdmEquals(op.Type))
			{
				return false;
			}
			if (n.Child1.Op.OpType == OpType.Null)
			{
				thenClauseIsNull = true;
				return true;
			}
			return n.Child2.Op.OpType == OpType.Null;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x00084849 File Offset: 0x00082A49
		internal static bool IsCollectionAggregateFunction(FunctionOp op, Node n)
		{
			return n.Children.Count >= 1 && TypeSemantics.IsCollectionType(n.Child0.Op.Type) && TypeSemantics.IsAggregateFunction(op.Function);
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x0008487D File Offset: 0x00082A7D
		internal static bool IsConstantBaseOp(OpType opType)
		{
			return opType == OpType.Constant || opType == OpType.InternalConstant || opType == OpType.Null || opType == OpType.NullSentinel;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00084890 File Offset: 0x00082A90
		internal static Node CombinePredicates(Node predicate1, Node predicate2, Command command)
		{
			IEnumerable<Node> enumerable = PlanCompilerUtil.BreakIntoAndParts(predicate1);
			IEnumerable<Node> enumerable2 = PlanCompilerUtil.BreakIntoAndParts(predicate2);
			Node node = predicate1;
			foreach (Node node2 in enumerable2)
			{
				bool flag = false;
				using (IEnumerator<Node> enumerator2 = enumerable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.IsEquivalent(node2))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					node = command.CreateNode(command.CreateConditionalOp(OpType.And), node, node2);
				}
			}
			return node;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x0008493C File Offset: 0x00082B3C
		private static IEnumerable<Node> BreakIntoAndParts(Node predicate)
		{
			return Helpers.GetLeafNodes<Node>(predicate, (Node node) => node.Op.OpType != OpType.And, (Node node) => new Node[] { node.Child0, node.Child1 });
		}
	}
}
