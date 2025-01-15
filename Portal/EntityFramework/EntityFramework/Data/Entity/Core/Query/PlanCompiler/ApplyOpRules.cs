using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200032F RID: 815
	internal static class ApplyOpRules
	{
		// Token: 0x060026EC RID: 9964 RVA: 0x000703B8 File Offset: 0x0006E5B8
		private static bool ProcessApplyOverFilter(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			if (((TransformationRulesContext)context).PlanCompiler.TransformationsDeferred)
			{
				return false;
			}
			Node child = applyNode.Child1;
			Command command = context.Command;
			NodeInfo nodeInfo = command.GetNodeInfo(child.Child0);
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(applyNode.Child0);
			if (nodeInfo.ExternalReferences.Overlaps(extendedNodeInfo.Definitions))
			{
				return false;
			}
			JoinBaseOp joinBaseOp;
			if (applyNode.Op.OpType == OpType.CrossApply)
			{
				joinBaseOp = command.CreateInnerJoinOp();
			}
			else
			{
				joinBaseOp = command.CreateLeftOuterJoinOp();
			}
			newNode = command.CreateNode(joinBaseOp, applyNode.Child0, child.Child0, child.Child1);
			return true;
		}

		// Token: 0x060026ED RID: 9965 RVA: 0x00070454 File Offset: 0x0006E654
		private static bool ProcessOuterApplyOverDummyProjectOverFilter(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			ProjectOp projectOp = (ProjectOp)child.Op;
			Node child2 = child.Child0;
			Node child3 = child2.Child0;
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child3);
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(applyNode.Child0);
			if (projectOp.Outputs.Overlaps(extendedNodeInfo2.Definitions) || extendedNodeInfo.ExternalReferences.Overlaps(extendedNodeInfo2.Definitions))
			{
				return false;
			}
			bool flag = false;
			Node node = null;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Var first;
			bool flag2;
			if (TransformationRulesContext.TryGetInt32Var(extendedNodeInfo.NonNullableDefinitions, out first))
			{
				flag2 = true;
			}
			else
			{
				first = extendedNodeInfo.NonNullableDefinitions.First;
				flag2 = false;
			}
			if (first != null)
			{
				flag = true;
				Node child4 = child.Child1.Child0;
				if (child4.Child0.Op.OpType == OpType.NullSentinel && flag2 && transformationRulesContext.CanChangeNullSentinelValue)
				{
					child4.Child0 = context.Command.CreateNode(context.Command.CreateVarRefOp(first));
				}
				else
				{
					child4.Child0 = transformationRulesContext.BuildNullIfExpression(first, child4.Child0);
				}
				command.RecomputeNodeInfo(child4);
				command.RecomputeNodeInfo(child.Child1);
				node = child3;
			}
			else
			{
				node = child;
				foreach (Var var in command.GetNodeInfo(child2.Child1).ExternalReferences)
				{
					if (extendedNodeInfo.Definitions.IsSet(var))
					{
						projectOp.Outputs.Set(var);
					}
				}
				child.Child0 = child3;
			}
			context.Command.RecomputeNodeInfo(child);
			Node node2 = command.CreateNode(command.CreateLeftOuterJoinOp(), applyNode.Child0, node, child2.Child1);
			if (flag)
			{
				ExtendedNodeInfo extendedNodeInfo3 = command.GetExtendedNodeInfo(node2);
				child.Child0 = node2;
				projectOp.Outputs.Or(extendedNodeInfo3.Definitions);
				newNode = child;
			}
			else
			{
				newNode = node2;
			}
			return true;
		}

		// Token: 0x060026EE RID: 9966 RVA: 0x00070660 File Offset: 0x0006E860
		private static bool ProcessCrossApplyOverProject(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			ProjectOp projectOp = (ProjectOp)child.Op;
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(applyNode);
			VarVec varVec = command.CreateVarVec(projectOp.Outputs);
			varVec.Or(extendedNodeInfo.Definitions);
			projectOp.Outputs.InitFrom(varVec);
			applyNode.Child1 = child.Child0;
			context.Command.RecomputeNodeInfo(applyNode);
			child.Child0 = applyNode;
			newNode = child;
			return true;
		}

		// Token: 0x060026EF RID: 9967 RVA: 0x000706D8 File Offset: 0x0006E8D8
		private static bool ProcessOuterApplyOverProject(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			Node child2 = child.Child1;
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			Var first = context.Command.GetExtendedNodeInfo(child.Child0).NonNullableDefinitions.First;
			if (first == null && child2.Children.Count == 1 && (child2.Child0.Child0.Op.OpType == OpType.InternalConstant || child2.Child0.Child0.Op.OpType == OpType.NullSentinel))
			{
				return false;
			}
			Command command = context.Command;
			Node node = null;
			InternalConstantOp internalConstantOp = null;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child.Child0);
			bool flag = false;
			foreach (Node node2 in child2.Children)
			{
				PlanCompiler.Assert(node2.Op.OpType == OpType.VarDef, "Expected VarDefOp. Found " + node2.Op.OpType.ToString() + " instead");
				VarRefOp varRefOp = node2.Child0.Op as VarRefOp;
				if (varRefOp == null || !extendedNodeInfo.Definitions.IsSet(varRefOp.Var))
				{
					if (first == null)
					{
						internalConstantOp = command.CreateInternalConstantOp(command.IntegerType, 1);
						Node node3 = command.CreateNode(internalConstantOp);
						Node node4 = command.CreateVarDefListNode(node3, out first);
						ProjectOp projectOp = command.CreateProjectOp(first);
						projectOp.Outputs.Or(extendedNodeInfo.Definitions);
						node = command.CreateNode(projectOp, child.Child0, node4);
					}
					Node node5;
					if (internalConstantOp != null && (internalConstantOp.IsEquivalent(node2.Child0.Op) || node2.Child0.Op.OpType == OpType.NullSentinel))
					{
						node5 = command.CreateNode(command.CreateVarRefOp(first));
					}
					else
					{
						node5 = transformationRulesContext.BuildNullIfExpression(first, node2.Child0);
					}
					node2.Child0 = node5;
					command.RecomputeNodeInfo(node2);
					flag = true;
				}
			}
			if (flag)
			{
				command.RecomputeNodeInfo(child2);
			}
			applyNode.Child1 = ((node != null) ? node : child.Child0);
			command.RecomputeNodeInfo(applyNode);
			child.Child0 = applyNode;
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(applyNode.Child0);
			((ProjectOp)child.Op).Outputs.Or(extendedNodeInfo2.Definitions);
			newNode = child;
			return true;
		}

		// Token: 0x060026F0 RID: 9968 RVA: 0x00070964 File Offset: 0x0006EB64
		private static bool ProcessApplyOverAnything(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child0;
			Node child2 = applyNode.Child1;
			ApplyBaseOp applyBaseOp = (ApplyBaseOp)applyNode.Op;
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(child2);
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(child);
			bool flag = false;
			if (applyBaseOp.OpType == OpType.OuterApply && extendedNodeInfo.MinRows >= RowCount.One)
			{
				applyBaseOp = command.CreateCrossApplyOp();
				flag = true;
			}
			if (!extendedNodeInfo.ExternalReferences.Overlaps(extendedNodeInfo2.Definitions))
			{
				if (applyBaseOp.OpType == OpType.CrossApply)
				{
					newNode = command.CreateNode(command.CreateCrossJoinOp(), child, child2);
				}
				else
				{
					LeftOuterJoinOp leftOuterJoinOp = command.CreateLeftOuterJoinOp();
					ConstantPredicateOp constantPredicateOp = command.CreateTrueOp();
					Node node = command.CreateNode(constantPredicateOp);
					newNode = command.CreateNode(leftOuterJoinOp, child, child2, node);
				}
				return true;
			}
			if (flag)
			{
				newNode = command.CreateNode(applyBaseOp, child, child2);
				return true;
			}
			return false;
		}

		// Token: 0x060026F1 RID: 9969 RVA: 0x00070A38 File Offset: 0x0006EC38
		private static bool ProcessApplyIntoScalarSubquery(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			Command command = context.Command;
			ExtendedNodeInfo extendedNodeInfo = command.GetExtendedNodeInfo(applyNode.Child1);
			OpType opType = applyNode.Op.OpType;
			if (!ApplyOpRules.CanRewriteApply(applyNode.Child1, extendedNodeInfo, opType))
			{
				newNode = applyNode;
				return false;
			}
			ExtendedNodeInfo extendedNodeInfo2 = command.GetExtendedNodeInfo(applyNode.Child0);
			Var first = extendedNodeInfo.Definitions.First;
			VarVec varVec = command.CreateVarVec(extendedNodeInfo2.Definitions);
			TransformationRulesContext transformationRulesContext = (TransformationRulesContext)context;
			transformationRulesContext.RemapSubtree(applyNode.Child1);
			ApplyOpRules.VarDefinitionRemapper.RemapSubtree(applyNode.Child1, command, first);
			Node node = command.CreateNode(command.CreateElementOp(first.Type), applyNode.Child1);
			Var var;
			Node node2 = command.CreateVarDefListNode(node, out var);
			varVec.Set(var);
			newNode = command.CreateNode(command.CreateProjectOp(varVec), applyNode.Child0, node2);
			transformationRulesContext.AddVarMapping(first, var);
			return true;
		}

		// Token: 0x060026F2 RID: 9970 RVA: 0x00070B13 File Offset: 0x0006ED13
		private static bool CanRewriteApply(Node rightChild, ExtendedNodeInfo applyRightChildNodeInfo, OpType applyKind)
		{
			return applyRightChildNodeInfo.Definitions.Count == 1 && applyRightChildNodeInfo.MaxRows == RowCount.One && (applyKind != OpType.CrossApply || applyRightChildNodeInfo.MinRows == RowCount.One) && ApplyOpRules.OutputCountVisitor.CountOutputs(rightChild) == 1;
		}

		// Token: 0x060026F3 RID: 9971 RVA: 0x00070B4C File Offset: 0x0006ED4C
		private static bool ProcessCrossApplyOverLeftOuterJoinOverSingleRowTable(RuleProcessingContext context, Node applyNode, out Node newNode)
		{
			newNode = applyNode;
			Node child = applyNode.Child1;
			if (((ConstantPredicateOp)child.Child2.Op).IsFalse)
			{
				return false;
			}
			applyNode.Op = context.Command.CreateOuterApplyOp();
			applyNode.Child1 = child.Child1;
			return true;
		}

		// Token: 0x04000D88 RID: 3464
		internal static readonly PatternMatchRule Rule_CrossApplyOverFilter = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverFilter));

		// Token: 0x04000D89 RID: 3465
		internal static readonly PatternMatchRule Rule_OuterApplyOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(FilterOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverFilter));

		// Token: 0x04000D8A RID: 3466
		internal static readonly PatternMatchRule Rule_OuterApplyOverProjectInternalConstantOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(FilterOp.Pattern, new Node[]
				{
					new Node(LeafOp.Pattern, new Node[0]),
					new Node(LeafOp.Pattern, new Node[0])
				}),
				new Node(VarDefListOp.Pattern, new Node[]
				{
					new Node(VarDefOp.Pattern, new Node[]
					{
						new Node(InternalConstantOp.Pattern, new Node[0])
					})
				})
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverDummyProjectOverFilter));

		// Token: 0x04000D8B RID: 3467
		internal static readonly PatternMatchRule Rule_OuterApplyOverProjectNullSentinelOverFilter = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(FilterOp.Pattern, new Node[]
				{
					new Node(LeafOp.Pattern, new Node[0]),
					new Node(LeafOp.Pattern, new Node[0])
				}),
				new Node(VarDefListOp.Pattern, new Node[]
				{
					new Node(VarDefOp.Pattern, new Node[]
					{
						new Node(NullSentinelOp.Pattern, new Node[0])
					})
				})
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverDummyProjectOverFilter));

		// Token: 0x04000D8C RID: 3468
		internal static readonly PatternMatchRule Rule_CrossApplyOverProject = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessCrossApplyOverProject));

		// Token: 0x04000D8D RID: 3469
		internal static readonly PatternMatchRule Rule_OuterApplyOverProject = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(ProjectOp.Pattern, new Node[]
			{
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessOuterApplyOverProject));

		// Token: 0x04000D8E RID: 3470
		internal static readonly PatternMatchRule Rule_CrossApplyOverAnything = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverAnything));

		// Token: 0x04000D8F RID: 3471
		internal static readonly PatternMatchRule Rule_OuterApplyOverAnything = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyOverAnything));

		// Token: 0x04000D90 RID: 3472
		internal static readonly PatternMatchRule Rule_CrossApplyIntoScalarSubquery = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyIntoScalarSubquery));

		// Token: 0x04000D91 RID: 3473
		internal static readonly PatternMatchRule Rule_OuterApplyIntoScalarSubquery = new PatternMatchRule(new Node(OuterApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeafOp.Pattern, new Node[0])
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessApplyIntoScalarSubquery));

		// Token: 0x04000D92 RID: 3474
		internal static readonly PatternMatchRule Rule_CrossApplyOverLeftOuterJoinOverSingleRowTable = new PatternMatchRule(new Node(CrossApplyOp.Pattern, new Node[]
		{
			new Node(LeafOp.Pattern, new Node[0]),
			new Node(LeftOuterJoinOp.Pattern, new Node[]
			{
				new Node(SingleRowTableOp.Pattern, new Node[0]),
				new Node(LeafOp.Pattern, new Node[0]),
				new Node(ConstantPredicateOp.Pattern, new Node[0])
			})
		}), new Rule.ProcessNodeDelegate(ApplyOpRules.ProcessCrossApplyOverLeftOuterJoinOverSingleRowTable));

		// Token: 0x04000D93 RID: 3475
		internal static readonly Rule[] Rules = new Rule[]
		{
			ApplyOpRules.Rule_CrossApplyOverAnything,
			ApplyOpRules.Rule_CrossApplyOverFilter,
			ApplyOpRules.Rule_CrossApplyOverProject,
			ApplyOpRules.Rule_OuterApplyOverAnything,
			ApplyOpRules.Rule_OuterApplyOverProjectInternalConstantOverFilter,
			ApplyOpRules.Rule_OuterApplyOverProjectNullSentinelOverFilter,
			ApplyOpRules.Rule_OuterApplyOverProject,
			ApplyOpRules.Rule_OuterApplyOverFilter,
			ApplyOpRules.Rule_CrossApplyOverLeftOuterJoinOverSingleRowTable,
			ApplyOpRules.Rule_CrossApplyIntoScalarSubquery,
			ApplyOpRules.Rule_OuterApplyIntoScalarSubquery
		};

		// Token: 0x020009D0 RID: 2512
		internal class OutputCountVisitor : BasicOpVisitorOfT<int>
		{
			// Token: 0x06005F6E RID: 24430 RVA: 0x001489FE File Offset: 0x00146BFE
			internal static int CountOutputs(Node node)
			{
				return new ApplyOpRules.OutputCountVisitor().VisitNode(node);
			}

			// Token: 0x06005F6F RID: 24431 RVA: 0x00148A0C File Offset: 0x00146C0C
			internal new int VisitChildren(Node n)
			{
				int num = 0;
				foreach (Node node in n.Children)
				{
					num += base.VisitNode(node);
				}
				return num;
			}

			// Token: 0x06005F70 RID: 24432 RVA: 0x00148A68 File Offset: 0x00146C68
			protected override int VisitDefault(Node n)
			{
				return this.VisitChildren(n);
			}

			// Token: 0x06005F71 RID: 24433 RVA: 0x00148A71 File Offset: 0x00146C71
			protected override int VisitSetOp(SetOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06005F72 RID: 24434 RVA: 0x00148A7E File Offset: 0x00146C7E
			public override int Visit(DistinctOp op, Node n)
			{
				return op.Keys.Count;
			}

			// Token: 0x06005F73 RID: 24435 RVA: 0x00148A8B File Offset: 0x00146C8B
			public override int Visit(FilterOp op, Node n)
			{
				return base.VisitNode(n.Child0);
			}

			// Token: 0x06005F74 RID: 24436 RVA: 0x00148A99 File Offset: 0x00146C99
			public override int Visit(GroupByOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06005F75 RID: 24437 RVA: 0x00148AA6 File Offset: 0x00146CA6
			public override int Visit(ProjectOp op, Node n)
			{
				return op.Outputs.Count;
			}

			// Token: 0x06005F76 RID: 24438 RVA: 0x00148AB3 File Offset: 0x00146CB3
			public override int Visit(ScanTableOp op, Node n)
			{
				return op.Table.Columns.Count;
			}

			// Token: 0x06005F77 RID: 24439 RVA: 0x00148AC5 File Offset: 0x00146CC5
			public override int Visit(SingleRowTableOp op, Node n)
			{
				return 0;
			}

			// Token: 0x06005F78 RID: 24440 RVA: 0x00148AC8 File Offset: 0x00146CC8
			protected override int VisitSortOp(SortBaseOp op, Node n)
			{
				return base.VisitNode(n.Child0);
			}
		}

		// Token: 0x020009D1 RID: 2513
		internal class VarDefinitionRemapper : VarRemapper
		{
			// Token: 0x06005F7A RID: 24442 RVA: 0x00148ADE File Offset: 0x00146CDE
			private VarDefinitionRemapper(Var oldVar, Command command)
				: base(command)
			{
				this.m_oldVar = oldVar;
			}

			// Token: 0x06005F7B RID: 24443 RVA: 0x00148AEE File Offset: 0x00146CEE
			internal static void RemapSubtree(Node root, Command command, Var oldVar)
			{
				new ApplyOpRules.VarDefinitionRemapper(oldVar, command).RemapSubtree(root);
			}

			// Token: 0x06005F7C RID: 24444 RVA: 0x00148B00 File Offset: 0x00146D00
			internal override void RemapSubtree(Node subTree)
			{
				foreach (Node node in subTree.Children)
				{
					this.RemapSubtree(node);
				}
				this.VisitNode(subTree);
				this.m_command.RecomputeNodeInfo(subTree);
			}

			// Token: 0x06005F7D RID: 24445 RVA: 0x00148B68 File Offset: 0x00146D68
			public override void Visit(VarDefOp op, Node n)
			{
				if (op.Var == this.m_oldVar)
				{
					Var var = this.m_command.CreateComputedVar(n.Child0.Op.Type);
					n.Op = this.m_command.CreateVarDefOp(var);
					base.AddMapping(this.m_oldVar, var);
				}
			}

			// Token: 0x06005F7E RID: 24446 RVA: 0x00148BC0 File Offset: 0x00146DC0
			public override void Visit(ScanTableOp op, Node n)
			{
				if (op.Table.Columns.Contains(this.m_oldVar))
				{
					ScanTableOp scanTableOp = this.m_command.CreateScanTableOp(op.Table.TableMetadata);
					for (int i = 0; i < op.Table.Columns.Count; i++)
					{
						base.AddMapping(op.Table.Columns[i], scanTableOp.Table.Columns[i]);
					}
					n.Op = scanTableOp;
				}
			}

			// Token: 0x06005F7F RID: 24447 RVA: 0x00148C48 File Offset: 0x00146E48
			protected override void VisitSetOp(SetOp op, Node n)
			{
				base.VisitSetOp(op, n);
				if (op.Outputs.IsSet(this.m_oldVar))
				{
					Var var = this.m_command.CreateSetOpVar(this.m_oldVar.Type);
					op.Outputs.Clear(this.m_oldVar);
					op.Outputs.Set(var);
					this.RemapVarMapKey(op.VarMap[0], var);
					this.RemapVarMapKey(op.VarMap[1], var);
					base.AddMapping(this.m_oldVar, var);
				}
			}

			// Token: 0x06005F80 RID: 24448 RVA: 0x00148CD0 File Offset: 0x00146ED0
			private void RemapVarMapKey(VarMap varMap, Var newVar)
			{
				Var var = varMap[this.m_oldVar];
				varMap.Remove(this.m_oldVar);
				varMap.Add(newVar, var);
			}

			// Token: 0x04002858 RID: 10328
			private readonly Var m_oldVar;
		}
	}
}
