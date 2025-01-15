using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200035D RID: 861
	internal static class ProjectOpRules
	{
		// Token: 0x060029DF RID: 10719 RVA: 0x0008801C File Offset: 0x0008621C
		private static bool ProcessProjectOverProject(RuleProcessingContext context, Node projectNode, out Node newNode)
		{
			newNode = projectNode;
			Node child = projectNode.Child1;
			Node child2 = projectNode.Child0;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Dictionary<Var, int> dictionary = new Dictionary<Var, int>();
			foreach (Node node in child.Children)
			{
				if (!transformationRulesContext.IsScalarOpTree(node.Child0, dictionary))
				{
					return false;
				}
			}
			Dictionary<Var, Node> varMap = transformationRulesContext.GetVarMap(child2.Child1, dictionary);
			if (varMap == null)
			{
				return false;
			}
			Node node2 = transformationRulesContext.Command.CreateNode(transformationRulesContext.Command.CreateVarDefListOp());
			foreach (Node node3 in child.Children)
			{
				node3.Child0 = transformationRulesContext.ReMap(node3.Child0, varMap);
				transformationRulesContext.Command.RecomputeNodeInfo(node3);
				node2.Children.Add(node3);
			}
			ExtendedNodeInfo extendedNodeInfo = transformationRulesContext.Command.GetExtendedNodeInfo(projectNode);
			foreach (Node node4 in child2.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node4.Op;
				if (extendedNodeInfo.Definitions.IsSet(varDefOp.Var))
				{
					node2.Children.Add(node4);
				}
			}
			projectNode.Child0 = child2.Child0;
			projectNode.Child1 = node2;
			return true;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000881D4 File Offset: 0x000863D4
		private static bool ProcessProjectWithNoLocalDefinitions(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			if (!context.Command.GetNodeInfo(n).ExternalReferences.IsEmpty)
			{
				return false;
			}
			newNode = n.Child0;
			return true;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000881FC File Offset: 0x000863FC
		private static bool ProcessProjectWithSimpleVarRedefinitions(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			ProjectOp projectOp = (ProjectOp)n.Op;
			if (n.Child1.Children.Count == 0)
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n);
			bool flag = false;
			foreach (Node node in n.Child1.Children)
			{
				Node child = node.Child0;
				if (child.Op.OpType == OpType.VarRef)
				{
					VarRefOp varRefOp = (VarRefOp)child.Op;
					if (!extendedNodeInfo.ExternalReferences.IsSet(varRefOp.Var))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			List<Node> list = new List<Node>();
			foreach (Node node2 in n.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node2.Op;
				VarRefOp varRefOp2 = node2.Child0.Op as VarRefOp;
				if (varRefOp2 != null && !extendedNodeInfo.ExternalReferences.IsSet(varRefOp2.Var))
				{
					projectOp.Outputs.Clear(varDefOp.Var);
					projectOp.Outputs.Set(varRefOp2.Var);
					transformationRulesContext.AddVarMapping(varDefOp.Var, varRefOp2.Var);
				}
				else
				{
					list.Add(node2);
				}
			}
			Node node3 = command.CreateNode(command.CreateVarDefListOp(), list);
			n.Child1 = node3;
			return true;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000883B0 File Offset: 0x000865B0
		private static bool ProcessProjectOpWithNullSentinel(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			ProjectOp projectOp = (ProjectOp)n.Op;
			if (n.Child1.Children.Where((Node c) => c.Child0.Op.OpType == OpType.NullSentinel).Count<Node>() == 0)
			{
				return false;
			}
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Command command = transformationRulesContext.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			bool flag = false;
			bool canChangeNullSentinelValue = transformationRulesContext.CanChangeNullSentinelValue;
			Var var;
			if (!canChangeNullSentinelValue || !TransformationRulesContext.TryGetInt32Var(extendedNodeInfo.NonNullableDefinitions, out var))
			{
				flag = true;
				if (canChangeNullSentinelValue)
				{
					if (TransformationRulesContext.TryGetInt32Var(from child in n.Child1.Children
						where child.Child0.Op.OpType == OpType.Constant || child.Child0.Op.OpType == OpType.InternalConstant
						select ((VarDefOp)child.Op).Var, out var))
					{
						goto IL_0146;
					}
				}
				var = (from child in n.Child1.Children
					where child.Child0.Op.OpType == OpType.NullSentinel
					select ((VarDefOp)child.Op).Var).FirstOrDefault<Var>();
				if (var == null)
				{
					return false;
				}
			}
			IL_0146:
			bool flag2 = false;
			for (int i = n.Child1.Children.Count - 1; i >= 0; i--)
			{
				Node node = n.Child1.Children[i];
				if (node.Child0.Op.OpType == OpType.NullSentinel)
				{
					if (!flag)
					{
						VarRefOp varRefOp = command.CreateVarRefOp(var);
						node.Child0 = command.CreateNode(varRefOp);
						command.RecomputeNodeInfo(node);
						flag2 = true;
					}
					else if (!var.Equals(((VarDefOp)node.Op).Var))
					{
						projectOp.Outputs.Clear(((VarDefOp)node.Op).Var);
						n.Child1.Children.RemoveAt(i);
						transformationRulesContext.AddVarMapping(((VarDefOp)node.Op).Var, var);
						flag2 = true;
					}
				}
			}
			if (flag2)
			{
				command.RecomputeNodeInfo(n.Child1);
			}
			return flag2;
		}

		// Token: 0x04000E66 RID: 3686
		internal static readonly PatternMatchRule Rule_ProjectOverProject = new PatternMatchRule(new Node(ProjectOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectOverProject));

		// Token: 0x04000E67 RID: 3687
		internal static readonly PatternMatchRule Rule_ProjectWithNoLocalDefs = new PatternMatchRule(new Node(ProjectOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(VarDefListOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectWithNoLocalDefinitions));

		// Token: 0x04000E68 RID: 3688
		internal static readonly SimpleRule Rule_ProjectOpWithSimpleVarRedefinitions = new SimpleRule(OpType.Project, new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectWithSimpleVarRedefinitions));

		// Token: 0x04000E69 RID: 3689
		internal static readonly SimpleRule Rule_ProjectOpWithNullSentinel = new SimpleRule(OpType.Project, new Rule.ProcessNodeDelegate(ProjectOpRules.ProcessProjectOpWithNullSentinel));

		// Token: 0x04000E6A RID: 3690
		internal static readonly Rule[] Rules = new Rule[]
		{
			ProjectOpRules.Rule_ProjectOpWithNullSentinel,
			ProjectOpRules.Rule_ProjectOpWithSimpleVarRedefinitions,
			ProjectOpRules.Rule_ProjectOverProject,
			ProjectOpRules.Rule_ProjectWithNoLocalDefs
		};
	}
}
