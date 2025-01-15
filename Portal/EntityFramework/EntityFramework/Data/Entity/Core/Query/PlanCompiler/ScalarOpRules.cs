using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000364 RID: 868
	internal static class ScalarOpRules
	{
		// Token: 0x06002A32 RID: 10802 RVA: 0x000895DC File Offset: 0x000877DC
		private static bool ProcessSimplifyCase(RuleProcessingContext context, Node caseOpNode, out Node newNode)
		{
			CaseOp caseOp = (CaseOp)caseOpNode.Op;
			newNode = caseOpNode;
			return ScalarOpRules.ProcessSimplifyCase_Collapse(caseOpNode, out newNode) || ScalarOpRules.ProcessSimplifyCase_EliminateWhenClauses(context, caseOp, caseOpNode, out newNode);
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00089614 File Offset: 0x00087814
		private static bool ProcessSimplifyCase_Collapse(Node caseOpNode, out Node newNode)
		{
			newNode = caseOpNode;
			Node child = caseOpNode.Child1;
			Node node = caseOpNode.Children[caseOpNode.Children.Count - 1];
			if (!child.IsEquivalent(node))
			{
				return false;
			}
			for (int i = 3; i < caseOpNode.Children.Count - 1; i += 2)
			{
				if (!caseOpNode.Children[i].IsEquivalent(child))
				{
					return false;
				}
			}
			newNode = child;
			return true;
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x00089684 File Offset: 0x00087884
		private static bool ProcessSimplifyCase_EliminateWhenClauses(RuleProcessingContext context, CaseOp caseOp, Node caseOpNode, out Node newNode)
		{
			List<Node> list = null;
			newNode = caseOpNode;
			int i = 0;
			while (i < caseOpNode.Children.Count)
			{
				if (i == caseOpNode.Children.Count - 1)
				{
					if (OpType.SoftCast == caseOpNode.Children[i].Op.OpType)
					{
						return false;
					}
					if (list != null)
					{
						list.Add(caseOpNode.Children[i]);
						break;
					}
					break;
				}
				else
				{
					if (OpType.SoftCast == caseOpNode.Children[i + 1].Op.OpType)
					{
						return false;
					}
					if (caseOpNode.Children[i].Op.OpType != OpType.ConstantPredicate)
					{
						if (list != null)
						{
							list.Add(caseOpNode.Children[i]);
							list.Add(caseOpNode.Children[i + 1]);
						}
						i += 2;
					}
					else
					{
						ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)caseOpNode.Children[i].Op;
						if (list == null)
						{
							list = new List<Node>();
							for (int j = 0; j < i; j++)
							{
								list.Add(caseOpNode.Children[j]);
							}
						}
						if (constantPredicateOp.IsTrue)
						{
							list.Add(caseOpNode.Children[i + 1]);
							break;
						}
						PlanCompiler.Assert(constantPredicateOp.IsFalse, "constant predicate must be either true or false");
						i += 2;
					}
				}
			}
			if (list == null)
			{
				return false;
			}
			PlanCompiler.Assert(list.Count > 0, "new args list must not be empty");
			if (list.Count == 1)
			{
				newNode = list[0];
			}
			else
			{
				newNode = context.Command.CreateNode(caseOp, list);
			}
			return true;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0008980C File Offset: 0x00087A0C
		private static bool ProcessFlattenCase(RuleProcessingContext context, Node caseOpNode, out Node newNode)
		{
			newNode = caseOpNode;
			Node node = caseOpNode.Children[caseOpNode.Children.Count - 1];
			if (node.Op.OpType != OpType.Case)
			{
				return false;
			}
			caseOpNode.Children.RemoveAt(caseOpNode.Children.Count - 1);
			caseOpNode.Children.AddRange(node.Children);
			return true;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00089870 File Offset: 0x00087A70
		private static bool ProcessIsNullOverCase(RuleProcessingContext context, Node isNullOpNode, out Node newNode)
		{
			Node child = isNullOpNode.Child0;
			if (child.Children.Count != 3)
			{
				newNode = isNullOpNode;
				return false;
			}
			Node child2 = child.Child0;
			Node child3 = child.Child1;
			Node child4 = child.Child2;
			OpType opType = child3.Op.OpType;
			if (opType > OpType.NullSentinel)
			{
				if (opType == OpType.Null)
				{
					OpType opType2 = child4.Op.OpType;
					if (opType2 <= OpType.NullSentinel)
					{
						newNode = child2;
						return true;
					}
				}
			}
			else if (child4.Op.OpType == OpType.Null)
			{
				newNode = context.Command.CreateNode(context.Command.CreateConditionalOp(OpType.Not), child2);
				return true;
			}
			newNode = isNullOpNode;
			return false;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x0008990C File Offset: 0x00087B0C
		private static bool ProcessComparisonsOverConstant(RuleProcessingContext context, Node node, out Node newNode)
		{
			newNode = node;
			PlanCompiler.Assert(node.Op.OpType == OpType.EQ || node.Op.OpType == OpType.NE, "unexpected comparison op type?");
			bool? flag = new bool?(node.Child0.Op.IsEquivalent(node.Child1.Op));
			if (flag == null)
			{
				return false;
			}
			bool flag2 = ((node.Op.OpType == OpType.EQ) ? flag.Value : (!flag.Value));
			ConstantPredicateOp constantPredicateOp = context.Command.CreateConstantPredicateOp(flag2);
			newNode = context.Command.CreateNode(constantPredicateOp);
			return true;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000899B4 File Offset: 0x00087BB4
		private static bool? MatchesPattern(string str, string pattern)
		{
			int num = pattern.IndexOf('%');
			if (num == -1 || num != pattern.Length - 1 || pattern.Length > str.Length + 1)
			{
				return null;
			}
			bool flag = true;
			int num2 = 0;
			while (num2 < str.Length && num2 < pattern.Length - 1)
			{
				if (pattern[num2] != str[num2])
				{
					flag = false;
					break;
				}
				num2++;
			}
			return new bool?(flag);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00089A30 File Offset: 0x00087C30
		private static bool ProcessLikeOverConstant(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			InternalConstantOp internalConstantOp = (InternalConstantOp)n.Child1.Op;
			bool? flag = ScalarOpRules.MatchesPattern((string)((InternalConstantOp)n.Child0.Op).Value, (string)internalConstantOp.Value);
			if (flag == null)
			{
				return false;
			}
			ConstantPredicateOp constantPredicateOp = context.Command.CreateConstantPredicateOp(flag.Value);
			newNode = context.Command.CreateNode(constantPredicateOp);
			return true;
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x00089AAC File Offset: 0x00087CAC
		private static bool ProcessLogOpOverConstant(RuleProcessingContext context, Node node, Node constantPredicateNode, Node otherNode, out Node newNode)
		{
			PlanCompiler.Assert(constantPredicateNode != null, "null constantPredicateOp?");
			ConstantPredicateOp constantPredicateOp = (ConstantPredicateOp)constantPredicateNode.Op;
			switch (node.Op.OpType)
			{
			case OpType.And:
				newNode = (constantPredicateOp.IsTrue ? otherNode : constantPredicateNode);
				return true;
			case OpType.Or:
				newNode = (constantPredicateOp.IsTrue ? constantPredicateNode : otherNode);
				return true;
			case OpType.Not:
				PlanCompiler.Assert(otherNode == null, "Not Op with more than 1 child. Gasp!");
				newNode = context.Command.CreateNode(context.Command.CreateConstantPredicateOp(!constantPredicateOp.Value));
				return true;
			}
			PlanCompiler.Assert(false, "Unexpected OpType - " + node.Op.OpType.ToString());
			newNode = null;
			return true;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x00089B7C File Offset: 0x00087D7C
		private static bool ProcessAndOverConstantPredicate1(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child1, node.Child0, out newNode);
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x00089B92 File Offset: 0x00087D92
		private static bool ProcessAndOverConstantPredicate2(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, node.Child1, out newNode);
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00089BA8 File Offset: 0x00087DA8
		private static bool ProcessOrOverConstantPredicate1(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child1, node.Child0, out newNode);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00089BBE File Offset: 0x00087DBE
		private static bool ProcessOrOverConstantPredicate2(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, node.Child1, out newNode);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x00089BD4 File Offset: 0x00087DD4
		private static bool ProcessNotOverConstantPredicate(RuleProcessingContext context, Node node, out Node newNode)
		{
			return ScalarOpRules.ProcessLogOpOverConstant(context, node, node.Child0, null, out newNode);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00089BE5 File Offset: 0x00087DE5
		private static bool ProcessIsNullOverConstant(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateFalseOp());
			return true;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x00089C00 File Offset: 0x00087E00
		private static bool ProcessIsNullOverNull(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateTrueOp());
			return true;
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x00089C1B File Offset: 0x00087E1B
		private static bool ProcessNullCast(RuleProcessingContext context, Node castNullOp, out Node newNode)
		{
			newNode = context.Command.CreateNode(context.Command.CreateNullOp(castNullOp.Op.Type));
			return true;
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x00089C44 File Offset: 0x00087E44
		private static bool ProcessIsNullOverVarRef(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			Command command = context.Command;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Var var = ((VarRefOp)isNullNode.Child0.Op).Var;
			if (transformationRulesContext.IsNonNullable(var))
			{
				newNode = command.CreateNode(context.Command.CreateFalseOp());
				return true;
			}
			newNode = isNullNode;
			return false;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x00089C98 File Offset: 0x00087E98
		private static bool ProcessIsNullOverAnything(RuleProcessingContext context, Node isNullNode, out Node newNode)
		{
			Command command = context.Command;
			OpType opType = isNullNode.Child0.Op.OpType;
			if (opType != OpType.Cast)
			{
				if (opType != OpType.Function)
				{
					newNode = isNullNode;
				}
				else
				{
					EdmFunction function = ((FunctionOp)isNullNode.Child0.Op).Function;
					newNode = (ScalarOpRules.PreservesNulls(function) ? command.CreateNode(command.CreateConditionalOp(OpType.IsNull), isNullNode.Child0.Child0) : isNullNode);
				}
			}
			else
			{
				newNode = command.CreateNode(command.CreateConditionalOp(OpType.IsNull), isNullNode.Child0.Child0);
			}
			switch (isNullNode.Child0.Op.OpType)
			{
			case OpType.Constant:
			case OpType.InternalConstant:
			case OpType.NullSentinel:
				return ScalarOpRules.ProcessIsNullOverConstant(context, newNode, out newNode);
			case OpType.Null:
				return ScalarOpRules.ProcessIsNullOverNull(context, newNode, out newNode);
			case OpType.VarRef:
				return ScalarOpRules.ProcessIsNullOverVarRef(context, newNode, out newNode);
			}
			return isNullNode != newNode;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00089D7E File Offset: 0x00087F7E
		private static bool PreservesNulls(EdmFunction function)
		{
			return function.FullName == "Edm.Length";
		}

		// Token: 0x04000E7B RID: 3707
		internal static readonly SimpleRule Rule_SimplifyCase = new SimpleRule(OpType.Case, new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessSimplifyCase));

		// Token: 0x04000E7C RID: 3708
		internal static readonly SimpleRule Rule_FlattenCase = new SimpleRule(OpType.Case, new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessFlattenCase));

		// Token: 0x04000E7D RID: 3709
		internal static readonly PatternMatchRule Rule_IsNullOverCase = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(CaseOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverCase));

		// Token: 0x04000E7E RID: 3710
		internal static readonly PatternMatchRule Rule_EqualsOverConstant = new PatternMatchRule(new Node(ComparisonOp.PatternEq, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(InternalConstantOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessComparisonsOverConstant));

		// Token: 0x04000E7F RID: 3711
		internal static readonly PatternMatchRule Rule_LikeOverConstants = new PatternMatchRule(new Node(LikeOp.Pattern, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(InternalConstantOp.Pattern, new Node[0]),
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessLikeOverConstant));

		// Token: 0x04000E80 RID: 3712
		internal static readonly PatternMatchRule Rule_AndOverConstantPred1 = new PatternMatchRule(new Node(ConditionalOp.PatternAnd, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessAndOverConstantPredicate1));

		// Token: 0x04000E81 RID: 3713
		internal static readonly PatternMatchRule Rule_AndOverConstantPred2 = new PatternMatchRule(new Node(ConditionalOp.PatternAnd, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessAndOverConstantPredicate2));

		// Token: 0x04000E82 RID: 3714
		internal static readonly PatternMatchRule Rule_OrOverConstantPred1 = new PatternMatchRule(new Node(ConditionalOp.PatternOr, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessOrOverConstantPredicate1));

		// Token: 0x04000E83 RID: 3715
		internal static readonly PatternMatchRule Rule_OrOverConstantPred2 = new PatternMatchRule(new Node(ConditionalOp.PatternOr, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessOrOverConstantPredicate2));

		// Token: 0x04000E84 RID: 3716
		internal static readonly PatternMatchRule Rule_NotOverConstantPred = new PatternMatchRule(new Node(ConditionalOp.PatternNot, new Node[]
		{
			new Node(ConstantPredicateOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessNotOverConstantPredicate));

		// Token: 0x04000E85 RID: 3717
		internal static readonly PatternMatchRule Rule_IsNullOverConstant = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(InternalConstantOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverConstant));

		// Token: 0x04000E86 RID: 3718
		internal static readonly PatternMatchRule Rule_IsNullOverNullSentinel = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(NullSentinelOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverConstant));

		// Token: 0x04000E87 RID: 3719
		internal static readonly PatternMatchRule Rule_IsNullOverNull = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverNull));

		// Token: 0x04000E88 RID: 3720
		internal static readonly PatternMatchRule Rule_NullCast = new PatternMatchRule(new Node(CastOp.Pattern, new Node[]
		{
			new Node(NullOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessNullCast));

		// Token: 0x04000E89 RID: 3721
		internal static readonly PatternMatchRule Rule_IsNullOverVarRef = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(VarRefOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverVarRef));

		// Token: 0x04000E8A RID: 3722
		internal static readonly PatternMatchRule Rule_IsNullOverAnything = new PatternMatchRule(new Node(ConditionalOp.PatternIsNull, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ScalarOpRules.ProcessIsNullOverAnything));

		// Token: 0x04000E8B RID: 3723
		internal static readonly Rule[] Rules = new Rule[]
		{
			ScalarOpRules.Rule_IsNullOverCase,
			ScalarOpRules.Rule_SimplifyCase,
			ScalarOpRules.Rule_FlattenCase,
			ScalarOpRules.Rule_LikeOverConstants,
			ScalarOpRules.Rule_EqualsOverConstant,
			ScalarOpRules.Rule_AndOverConstantPred1,
			ScalarOpRules.Rule_AndOverConstantPred2,
			ScalarOpRules.Rule_OrOverConstantPred1,
			ScalarOpRules.Rule_OrOverConstantPred2,
			ScalarOpRules.Rule_NotOverConstantPred,
			ScalarOpRules.Rule_IsNullOverConstant,
			ScalarOpRules.Rule_IsNullOverNullSentinel,
			ScalarOpRules.Rule_IsNullOverNull,
			ScalarOpRules.Rule_NullCast,
			ScalarOpRules.Rule_IsNullOverVarRef
		};
	}
}
