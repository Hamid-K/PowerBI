using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000340 RID: 832
	internal static class FilterOpRules
	{
		// Token: 0x060027AF RID: 10159 RVA: 0x00074410 File Offset: 0x00072610
		private static Node GetPushdownPredicate(Command command, Node filterNode, VarVec columns, out Node nonPushdownPredicateNode)
		{
			Node node = filterNode.Child1;
			nonPushdownPredicateNode = null;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(filterNode);
			if (columns == null && extendedNodeInfo.ExternalReferences.IsEmpty)
			{
				return node;
			}
			if (columns == null)
			{
				columns = command.GetExtendedNodeInfo(filterNode.Child0).Definitions;
			}
			Predicate predicate;
			node = new Predicate(command, node).GetSingleTablePredicates(columns, out predicate).BuildAndTree();
			nonPushdownPredicateNode = predicate.BuildAndTree();
			return node;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00074478 File Offset: 0x00072678
		private static bool ProcessFilterOverFilter(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			Node node = context.Command.CreateNode(context.Command.CreateConditionalOp(OpType.And), filterNode.Child0.Child1, filterNode.Child1);
			newNode = context.Command.CreateNode(context.Command.CreateFilterOp(), filterNode.Child0.Child0, node);
			return true;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000744D4 File Offset: 0x000726D4
		private static bool ProcessFilterOverProject(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node child = filterNode.Child1;
			if (child.Op.OpType == OpType.ConstantPredicate)
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Dictionary<Var, int> dictionary = new Dictionary<Var, int>();
			if (!transformationRulesContext.IsScalarOpTree(child, dictionary))
			{
				return false;
			}
			Node child2 = filterNode.Child0;
			Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(child2.Child1, dictionary);
			if (varMap == null)
			{
				return false;
			}
			if (transformationRulesContext.IncludeCustomFunctionOp(child, varMap))
			{
				return false;
			}
			Node node = transformationRulesContext.ReMap(child, varMap);
			Node node2 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), child2.Child0, node);
			Node node3 = transformationRulesContext.Command.CreateNode(child2.Op, node2, child2.Child1);
			newNode = node3;
			return true;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x00074588 File Offset: 0x00072788
		private static bool ProcessFilterOverSetOp(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Node node;
			Node pushdownPredicate = FilterOpRules.GetPushdownPredicate(transformationRulesContext.Command, filterNode, null, out node);
			if (pushdownPredicate == null)
			{
				return false;
			}
			if (!transformationRulesContext.IsScalarOpTree(pushdownPredicate))
			{
				return false;
			}
			Node child = filterNode.Child0;
			SetOp setOp = (SetOp)child.Op;
			List<Node> list = new List<Node>();
			int num = 0;
			foreach (VarMap varMap2 in setOp.VarMap)
			{
				if (setOp.OpType == OpType.Except && num == 1)
				{
					list.Add(child.Child1);
					break;
				}
				Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>();
				foreach (KeyValuePair<Var, Var> keyValuePair in varMap2)
				{
					Node node2 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateVarRefOp(keyValuePair.Value));
					dictionary.Add(keyValuePair.Key, node2);
				}
				Node node3 = pushdownPredicate;
				if (num == 0 && filterNode.Op.OpType != OpType.Except)
				{
					node3 = transformationRulesContext.Copy(node3);
				}
				Node node4 = transformationRulesContext.ReMap(node3, dictionary);
				transformationRulesContext.Command.RecomputeNodeInfo(node4);
				Node node5 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), child.Children[num], node4);
				list.Add(node5);
				num++;
			}
			Node node6 = transformationRulesContext.Command.CreateNode(child.Op, list);
			if (node != null)
			{
				newNode = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), node6, node);
			}
			else
			{
				newNode = node6;
			}
			return true;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x00074744 File Offset: 0x00072944
		private static bool ProcessFilterOverDistinct(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node node;
			Node pushdownPredicate = FilterOpRules.GetPushdownPredicate(context.Command, filterNode, null, out node);
			if (pushdownPredicate == null)
			{
				return false;
			}
			Node child = filterNode.Child0;
			Node node2 = context.Command.CreateNode(context.Command.CreateFilterOp(), child.Child0, pushdownPredicate);
			Node node3 = context.Command.CreateNode(child.Op, node2);
			if (node != null)
			{
				newNode = context.Command.CreateNode(context.Command.CreateFilterOp(), node3, node);
			}
			else
			{
				newNode = node3;
			}
			return true;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000747C8 File Offset: 0x000729C8
		private static bool ProcessFilterOverGroupBy(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node child = filterNode.Child0;
			GroupByOp groupByOp = (GroupByOp)child.Op;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Dictionary<Var, int> dictionary = new Dictionary<Var, int>();
			if (!transformationRulesContext.IsScalarOpTree(filterNode.Child1, dictionary))
			{
				return false;
			}
			Node node;
			Node pushdownPredicate = FilterOpRules.GetPushdownPredicate(context.Command, filterNode, groupByOp.Keys, out node);
			if (pushdownPredicate == null)
			{
				return false;
			}
			Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(child.Child1, dictionary);
			if (varMap == null)
			{
				return false;
			}
			Node node2 = transformationRulesContext.ReMap(pushdownPredicate, varMap);
			Node node3 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), child.Child0, node2);
			Node node4 = transformationRulesContext.Command.CreateNode(child.Op, node3, child.Child1, child.Child2);
			if (node == null)
			{
				newNode = node4;
			}
			else
			{
				newNode = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateFilterOp(), node4, node);
			}
			return true;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000748B0 File Offset: 0x00072AB0
		private static bool ProcessFilterOverJoin(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			if (transformationRulesContext.IsFilterPushdownSuppressed(filterNode))
			{
				return false;
			}
			Node child = filterNode.Child0;
			Op op = child.Op;
			Node node = child.Child0;
			Node node2 = child.Child1;
			Command command = transformationRulesContext.Command;
			bool flag = false;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(node2);
			Predicate predicate = new Predicate(command, filterNode.Child1);
			if (op.OpType == OpType.LeftOuterJoin && !predicate.PreservesNulls(extendedNodeInfo.Definitions, true))
			{
				if (transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.NullSemantics) && transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.JoinElimination))
				{
					op = command.CreateInnerJoinOp();
					flag = true;
				}
				else
				{
					transformationRulesContext.PlanCompiler.TransformationsDeferred = true;
				}
			}
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(node);
			Node node3 = null;
			if (node.Op.OpType != OpType.ScanTable)
			{
				node3 = predicate.GetSingleTablePredicates(extendedNodeInfo2.Definitions, out predicate).BuildAndTree();
			}
			Node node4 = null;
			if (node2.Op.OpType != OpType.ScanTable && op.OpType != OpType.LeftOuterJoin)
			{
				node4 = predicate.GetSingleTablePredicates(extendedNodeInfo.Definitions, out predicate).BuildAndTree();
			}
			Node node5 = null;
			if (op.OpType == OpType.CrossJoin || op.OpType == OpType.InnerJoin)
			{
				node5 = predicate.GetJoinPredicates(extendedNodeInfo2.Definitions, extendedNodeInfo.Definitions, out predicate).BuildAndTree();
			}
			if (node3 != null)
			{
				node = command.CreateNode(command.CreateFilterOp(), node, node3);
				flag = true;
			}
			if (node4 != null)
			{
				node2 = command.CreateNode(command.CreateFilterOp(), node2, node4);
				flag = true;
			}
			if (node5 != null)
			{
				flag = true;
				if (op.OpType == OpType.CrossJoin)
				{
					op = command.CreateInnerJoinOp();
				}
				else
				{
					PlanCompiler.Assert(op.OpType == OpType.InnerJoin, "unexpected non-InnerJoin?");
					node5 = PlanCompilerUtil.CombinePredicates(child.Child2, node5, command);
				}
			}
			else
			{
				node5 = ((op.OpType == OpType.CrossJoin) ? null : child.Child2);
			}
			if (!flag)
			{
				return false;
			}
			Node node6;
			if (op.OpType == OpType.CrossJoin)
			{
				node6 = command.CreateNode(op, node, node2);
			}
			else
			{
				node6 = command.CreateNode(op, node, node2, node5);
			}
			Node node7 = predicate.BuildAndTree();
			if (node7 == null)
			{
				newNode = node6;
			}
			else
			{
				newNode = command.CreateNode(command.CreateFilterOp(), node6, node7);
			}
			return true;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00074AE4 File Offset: 0x00072CE4
		private static bool ProcessFilterOverOuterApply(RuleProcessingContext context, Node filterNode, out Node newNode)
		{
			newNode = filterNode;
			Node child = filterNode.Child0;
			Node child2 = child.Child1;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child2);
			if (!new Predicate(command, filterNode.Child1).PreservesNulls(extendedNodeInfo.Definitions, true))
			{
				if (transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.NullSemantics) && transformationRulesContext.PlanCompiler.IsAfterPhase(PlanCompilerPhase.JoinElimination))
				{
					Node node = command.CreateNode(command.CreateCrossApplyOp(), child.Child0, child2);
					Node node2 = command.CreateNode(command.CreateFilterOp(), node, filterNode.Child1);
					newNode = node2;
					return true;
				}
				transformationRulesContext.PlanCompiler.TransformationsDeferred = true;
			}
			return false;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00074B90 File Offset: 0x00072D90
		private static bool ProcessFilterWithConstantPredicate(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)n.Child1.Op;
			if (constantPredicateOp.IsTrue)
			{
				newNode = n.Child0;
				return true;
			}
			PlanCompiler.Assert(constantPredicateOp.IsFalse, "unexpected non-false predicate?");
			if (n.Child0.Op.OpType == OpType.SingleRowTable || (n.Child0.Op.OpType == OpType.Project && n.Child0.Child0.Op.OpType == OpType.SingleRowTable))
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			ExtendedNodeInfo extendedNodeInfo = transformationRulesContext.Command.GetExtendedNodeInfo(n.Child0);
			List<Node> list = new List<Node>();
			VarVec varVec = transformationRulesContext.Command.CreateVarVec();
			foreach (Var var in extendedNodeInfo.Definitions)
			{
				NullOp nullOp = transformationRulesContext.Command.CreateNullOp(var.Type);
				Node node = transformationRulesContext.Command.CreateNode(nullOp);
				Var var2;
				Node node2 = transformationRulesContext.Command.CreateVarDefNode(node, out var2);
				transformationRulesContext.AddVarMapping(var, var2);
				varVec.Set(var2);
				list.Add(node2);
			}
			if (varVec.IsEmpty)
			{
				NullOp nullOp2 = transformationRulesContext.Command.CreateNullOp(transformationRulesContext.Command.BooleanType);
				Node node3 = transformationRulesContext.Command.CreateNode(nullOp2);
				Var var3;
				Node node4 = transformationRulesContext.Command.CreateVarDefNode(node3, out var3);
				varVec.Set(var3);
				list.Add(node4);
			}
			Node node5 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateSingleRowTableOp());
			n.Child0 = node5;
			Node node6 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateVarDefListOp(), list);
			ProjectOp projectOp = transformationRulesContext.Command.CreateProjectOp(varVec);
			Node node7 = transformationRulesContext.Command.CreateNode(projectOp, n, node6);
			node7.Child0 = n;
			newNode = node7;
			return true;
		}

		// Token: 0x04000DCF RID: 3535
		internal static readonly PatternMatchRule Rule_FilterOverFilter = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverFilter));

		// Token: 0x04000DD0 RID: 3536
		internal static readonly PatternMatchRule Rule_FilterOverProject = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverProject));

		// Token: 0x04000DD1 RID: 3537
		internal static readonly PatternMatchRule Rule_FilterOverUnionAll = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(UnionAllOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverSetOp));

		// Token: 0x04000DD2 RID: 3538
		internal static readonly PatternMatchRule Rule_FilterOverIntersect = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(IntersectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverSetOp));

		// Token: 0x04000DD3 RID: 3539
		internal static readonly PatternMatchRule Rule_FilterOverExcept = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(ExceptOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverSetOp));

		// Token: 0x04000DD4 RID: 3540
		internal static readonly PatternMatchRule Rule_FilterOverDistinct = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(DistinctOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverDistinct));

		// Token: 0x04000DD5 RID: 3541
		internal static readonly PatternMatchRule Rule_FilterOverGroupBy = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(GroupByOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverGroupBy));

		// Token: 0x04000DD6 RID: 3542
		internal static readonly PatternMatchRule Rule_FilterOverCrossJoin = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(CrossJoinOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverJoin));

		// Token: 0x04000DD7 RID: 3543
		internal static readonly PatternMatchRule Rule_FilterOverInnerJoin = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(InnerJoinOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverJoin));

		// Token: 0x04000DD8 RID: 3544
		internal static readonly PatternMatchRule Rule_FilterOverLeftOuterJoin = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(LeftOuterJoinOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverJoin));

		// Token: 0x04000DD9 RID: 3545
		internal static readonly PatternMatchRule Rule_FilterOverOuterApply = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(OuterApplyOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterOverOuterApply));

		// Token: 0x04000DDA RID: 3546
		internal static readonly PatternMatchRule Rule_FilterWithConstantPredicate = new PatternMatchRule(new Node(FilterOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(FilterOpRules.ProcessFilterWithConstantPredicate));

		// Token: 0x04000DDB RID: 3547
		internal static readonly Rule[] Rules = new Rule[]
		{
			FilterOpRules.Rule_FilterWithConstantPredicate,
			FilterOpRules.Rule_FilterOverCrossJoin,
			FilterOpRules.Rule_FilterOverDistinct,
			FilterOpRules.Rule_FilterOverExcept,
			FilterOpRules.Rule_FilterOverFilter,
			FilterOpRules.Rule_FilterOverGroupBy,
			FilterOpRules.Rule_FilterOverInnerJoin,
			FilterOpRules.Rule_FilterOverIntersect,
			FilterOpRules.Rule_FilterOverLeftOuterJoin,
			FilterOpRules.Rule_FilterOverProject,
			FilterOpRules.Rule_FilterOverUnionAll,
			FilterOpRules.Rule_FilterOverOuterApply
		};
	}
}
