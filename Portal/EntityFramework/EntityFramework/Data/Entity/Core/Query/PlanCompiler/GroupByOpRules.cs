using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000347 RID: 839
	internal static class GroupByOpRules
	{
		// Token: 0x060027E2 RID: 10210 RVA: 0x00075E74 File Offset: 0x00074074
		private static bool ProcessGroupByWithSimpleVarRedefinitions(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			GroupByOp groupByOp = (GroupByOp)n.Op;
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
					groupByOp.Outputs.Clear(varDefOp.Var);
					groupByOp.Outputs.Set(varRefOp2.Var);
					groupByOp.Keys.Clear(varDefOp.Var);
					groupByOp.Keys.Set(varRefOp2.Var);
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

		// Token: 0x060027E3 RID: 10211 RVA: 0x0007604C File Offset: 0x0007424C
		private static bool ProcessGroupByOpOnAllInputColumnsWithAggregateOperation(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			PhysicalProjectOp physicalProjectOp = context.Command.Root.Op as PhysicalProjectOp;
			if (physicalProjectOp == null || physicalProjectOp.Outputs.Count > 1)
			{
				return false;
			}
			if (n.Child0.Op.OpType != OpType.ScanTable)
			{
				return false;
			}
			if (n.Child2 == null || n.Child2.Child0 == null || n.Child2.Child0.Child0 == null || n.Child2.Child0.Child0.Op.OpType != OpType.Aggregate)
			{
				return false;
			}
			GroupByOp groupByOp = (GroupByOp)n.Op;
			Table table = ((ScanTableOp)n.Child0.Op).Table;
			VarList columns = table.Columns;
			foreach (Var var in columns)
			{
				if (!groupByOp.Keys.IsSet(var))
				{
					return false;
				}
			}
			foreach (Var var2 in columns)
			{
				groupByOp.Outputs.Clear(var2);
				groupByOp.Keys.Clear(var2);
			}
			Command command = context.Command;
			ScanTableOp scanTableOp = command.CreateScanTableOp(table.TableMetadata);
			Node node = command.CreateNode(scanTableOp);
			Node node2 = command.CreateNode(command.CreateOuterApplyOp(), node, n);
			Var var3;
			Node node3 = command.CreateVarDefListNode(command.CreateNode(command.CreateVarRefOp(groupByOp.Outputs.First)), out var3);
			newNode = command.CreateNode(command.CreateProjectOp(var3), node2, node3);
			Node node4 = null;
			IEnumerator<Var> enumerator2 = scanTableOp.Table.Keys.GetEnumerator();
			IEnumerator<Var> enumerator3 = table.Keys.GetEnumerator();
			for (int i = 0; i < table.Keys.Count; i++)
			{
				enumerator2.MoveNext();
				enumerator3.MoveNext();
				Node node5 = command.CreateNode(command.CreateComparisonOp(OpType.EQ, false), command.CreateNode(command.CreateVarRefOp(enumerator2.Current)), command.CreateNode(command.CreateVarRefOp(enumerator3.Current)));
				if (node4 != null)
				{
					node4 = command.CreateNode(command.CreateConditionalOp(OpType.And), node4, node5);
				}
				else
				{
					node4 = node5;
				}
			}
			Node node6 = command.CreateNode(command.CreateFilterOp(), n.Child0, node4);
			n.Child0 = node6;
			return true;
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000762F0 File Offset: 0x000744F0
		private static bool ProcessGroupByOverProject(RuleProcessingContext context, Node n, out Node newNode)
		{
			newNode = n;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			Command command = context.Command;
			Node child = n.Child0;
			Node child2 = child.Child1;
			Node child3 = n.Child1;
			Node child4 = n.Child2;
			if (child3.Children.Count > 0)
			{
				return false;
			}
			VarVec varVec = command.GetExtendedNodeInfo(child).LocalDefinitions;
			if (groupByOp.Outputs.Overlaps(varVec))
			{
				return false;
			}
			bool flag = false;
			for (int i = 0; i < child2.Children.Count; i++)
			{
				Node node = child2.Children[i];
				if (node.Child0.Op.OpType == OpType.Constant || node.Child0.Op.OpType == OpType.InternalConstant || node.Child0.Op.OpType == OpType.NullSentinel)
				{
					if (!flag)
					{
						varVec = command.CreateVarVec(varVec);
						flag = true;
					}
					varVec.Clear(((VarDefOp)node.Op).Var);
				}
			}
			if (GroupByOpRules.VarRefUsageFinder.AnyVarUsedMoreThanOnce(varVec, child4, command))
			{
				return false;
			}
			Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>(child2.Children.Count);
			for (int j = 0; j < child2.Children.Count; j++)
			{
				Node node2 = child2.Children[j];
				Var var = ((VarDefOp)node2.Op).Var;
				dictionary.Add(var, node2.Child0);
			}
			newNode.Child2 = GroupByOpRules.VarRefReplacer.Replace(dictionary, child4, command);
			newNode.Child0 = child.Child0;
			return true;
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0007647C File Offset: 0x0007467C
		private static bool ProcessGroupByOpWithNoAggregates(RuleProcessingContext context, Node n, out Node newNode)
		{
			Command command = context.Command;
			GroupByOp groupByOp = (GroupByOp)n.Op;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(n.Child0);
			ProjectOp projectOp = command.CreateProjectOp(groupByOp.Keys);
			newNode = command.CreateNode(projectOp, n.Child0, n.Child1);
			if (extendedNodeInfo.Keys.NoKeys || !groupByOp.Keys.Subsumes(extendedNodeInfo.Keys.KeyVars))
			{
				newNode = command.CreateNode(command.CreateDistinctOp(command.CreateVarVec(groupByOp.Keys)), newNode);
			}
			return true;
		}

		// Token: 0x04000DF1 RID: 3569
		internal static readonly SimpleRule Rule_GroupByOpWithSimpleVarRedefinitions = new SimpleRule(OpType.GroupBy, new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByWithSimpleVarRedefinitions));

		// Token: 0x04000DF2 RID: 3570
		internal static readonly SimpleRule Rule_GroupByOpOnAllInputColumnsWithAggregateOperation = new SimpleRule(OpType.GroupBy, new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOpOnAllInputColumnsWithAggregateOperation));

		// Token: 0x04000DF3 RID: 3571
		internal static readonly PatternMatchRule Rule_GroupByOverProject = new PatternMatchRule(new Node(GroupByOp.Pattern, new Node[]
		{
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			}),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOverProject));

		// Token: 0x04000DF4 RID: 3572
		internal static readonly PatternMatchRule Rule_GroupByOpWithNoAggregates = new PatternMatchRule(new Node(GroupByOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(VarDefListOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(GroupByOpRules.ProcessGroupByOpWithNoAggregates));

		// Token: 0x04000DF5 RID: 3573
		internal static readonly Rule[] Rules = new Rule[]
		{
			GroupByOpRules.Rule_GroupByOpWithSimpleVarRedefinitions,
			GroupByOpRules.Rule_GroupByOverProject,
			GroupByOpRules.Rule_GroupByOpWithNoAggregates,
			GroupByOpRules.Rule_GroupByOpOnAllInputColumnsWithAggregateOperation
		};

		// Token: 0x020009DC RID: 2524
		internal class VarRefReplacer : BasicOpVisitorOfNode
		{
			// Token: 0x06005FA0 RID: 24480 RVA: 0x001490E4 File Offset: 0x001472E4
			private VarRefReplacer(Dictionary<Var, Node> varReplacementTable, Command command)
			{
				this.m_varReplacementTable = varReplacementTable;
				this.m_command = command;
			}

			// Token: 0x06005FA1 RID: 24481 RVA: 0x001490FA File Offset: 0x001472FA
			internal static Node Replace(Dictionary<Var, Node> varReplacementTable, Node root, Command command)
			{
				return new GroupByOpRules.VarRefReplacer(varReplacementTable, command).VisitNode(root);
			}

			// Token: 0x06005FA2 RID: 24482 RVA: 0x0014910C File Offset: 0x0014730C
			public override Node Visit(VarRefOp op, Node n)
			{
				Node node;
				if (this.m_varReplacementTable.TryGetValue(op.Var, out node))
				{
					return node;
				}
				return n;
			}

			// Token: 0x06005FA3 RID: 24483 RVA: 0x00149134 File Offset: 0x00147334
			protected override Node VisitDefault(Node n)
			{
				Node node = base.VisitDefault(n);
				this.m_command.RecomputeNodeInfo(node);
				return node;
			}

			// Token: 0x04002863 RID: 10339
			private readonly Dictionary<Var, Node> m_varReplacementTable;

			// Token: 0x04002864 RID: 10340
			private readonly Command m_command;
		}

		// Token: 0x020009DD RID: 2525
		internal class VarRefUsageFinder : BasicOpVisitor
		{
			// Token: 0x06005FA4 RID: 24484 RVA: 0x00149156 File Offset: 0x00147356
			private VarRefUsageFinder(VarVec varVec, Command command)
			{
				this.m_varVec = varVec;
				this.m_usedVars = command.CreateVarVec();
			}

			// Token: 0x06005FA5 RID: 24485 RVA: 0x00149171 File Offset: 0x00147371
			internal static bool AnyVarUsedMoreThanOnce(VarVec varVec, Node root, Command command)
			{
				GroupByOpRules.VarRefUsageFinder varRefUsageFinder = new GroupByOpRules.VarRefUsageFinder(varVec, command);
				varRefUsageFinder.VisitNode(root);
				return varRefUsageFinder.m_anyUsedMoreThenOnce;
			}

			// Token: 0x06005FA6 RID: 24486 RVA: 0x00149188 File Offset: 0x00147388
			public override void Visit(VarRefOp op, Node n)
			{
				Var var = op.Var;
				if (this.m_varVec.IsSet(var))
				{
					if (this.m_usedVars.IsSet(var))
					{
						this.m_anyUsedMoreThenOnce = true;
						return;
					}
					this.m_usedVars.Set(var);
				}
			}

			// Token: 0x06005FA7 RID: 24487 RVA: 0x001491CC File Offset: 0x001473CC
			protected override void VisitChildren(Node n)
			{
				if (this.m_anyUsedMoreThenOnce)
				{
					return;
				}
				base.VisitChildren(n);
			}

			// Token: 0x04002865 RID: 10341
			private bool m_anyUsedMoreThenOnce;

			// Token: 0x04002866 RID: 10342
			private readonly VarVec m_varVec;

			// Token: 0x04002867 RID: 10343
			private readonly VarVec m_usedVars;
		}
	}
}
