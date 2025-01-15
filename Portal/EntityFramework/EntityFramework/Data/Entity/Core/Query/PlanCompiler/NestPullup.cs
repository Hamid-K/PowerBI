using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000350 RID: 848
	internal class NestPullup : BasicOpVisitorOfNode
	{
		// Token: 0x060028AE RID: 10414 RVA: 0x0007D92A File Offset: 0x0007BB2A
		private NestPullup(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_varRemapper = new VarRemapper(compilerState.Command);
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0007D960 File Offset: 0x0007BB60
		internal static void Process(PlanCompiler compilerState)
		{
			new NestPullup(compilerState).Process();
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0007D970 File Offset: 0x0007BB70
		private void Process()
		{
			PlanCompiler.Assert(this.Command.Root.Op.OpType == OpType.PhysicalProject, "root node is not physicalProject?");
			this.Command.Root = base.VisitNode(this.Command.Root);
			if (this.m_foundSortUnderUnnest)
			{
				SortRemover.Process(this.Command);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x0007D9CF File Offset: 0x0007BBCF
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x060028B2 RID: 10418 RVA: 0x0007D9DC File Offset: 0x0007BBDC
		private static bool IsNestOpNode(Node n)
		{
			PlanCompiler.Assert(n.Op.OpType != OpType.SingleStreamNest, "illegal singleStreamNest?");
			return n.Op.OpType == OpType.SingleStreamNest || n.Op.OpType == OpType.MultiStreamNest;
		}

		// Token: 0x060028B3 RID: 10419 RVA: 0x0007DA1C File Offset: 0x0007BC1C
		private Node NestingNotSupported(Op op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			foreach (Node node in n.Children)
			{
				if (NestPullup.IsNestOpNode(node))
				{
					throw new NotSupportedException(Strings.ADP_NestingNotSupported(op.OpType.ToString(), node.Op.OpType.ToString()));
				}
			}
			return n;
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x0007DABC File Offset: 0x0007BCBC
		private Var ResolveVarReference(Var refVar)
		{
			Var var = refVar;
			while (this.m_varRefMap.TryGetValue(var, out var))
			{
				refVar = var;
			}
			return refVar;
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x0007DAE4 File Offset: 0x0007BCE4
		private void UpdateReplacementVarMap(IEnumerable<Var> fromVars, IEnumerable<Var> toVars)
		{
			IEnumerator<Var> enumerator = toVars.GetEnumerator();
			foreach (Var var in fromVars)
			{
				if (!enumerator.MoveNext())
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 2, null);
				}
				this.m_varRemapper.AddMapping(var, enumerator.Current);
			}
			if (enumerator.MoveNext())
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 3, null);
			}
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x0007DB68 File Offset: 0x0007BD68
		private static void RemapSortKeys(List<SortKey> sortKeys, Dictionary<Var, Var> varMap)
		{
			if (sortKeys != null)
			{
				foreach (SortKey sortKey in sortKeys)
				{
					Var var;
					if (varMap.TryGetValue(sortKey.Var, out var))
					{
						sortKey.Var = var;
					}
				}
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x0007DBCC File Offset: 0x0007BDCC
		private static IEnumerable<Var> RemapVars(IEnumerable<Var> vars, Dictionary<Var, Var> varMap)
		{
			foreach (Var var in vars)
			{
				Var var2;
				if (varMap.TryGetValue(var, out var2))
				{
					yield return var2;
				}
				else
				{
					yield return var;
				}
			}
			IEnumerator<Var> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x0007DBE3 File Offset: 0x0007BDE3
		private static VarList RemapVarList(VarList varList, Dictionary<Var, Var> varMap)
		{
			return Command.CreateVarList(NestPullup.RemapVars(varList, varMap));
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x0007DBF1 File Offset: 0x0007BDF1
		private VarVec RemapVarVec(VarVec varVec, Dictionary<Var, Var> varMap)
		{
			return this.Command.CreateVarVec(NestPullup.RemapVars(varVec, varMap));
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x0007DC08 File Offset: 0x0007BE08
		public override Node Visit(VarDefOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			if (n.Child0.Op.OpType == OpType.VarRef)
			{
				this.m_varRefMap.Add(op.Var, ((VarRefOp)n.Child0.Op).Var);
			}
			return n;
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x0007DC62 File Offset: 0x0007BE62
		public override Node Visit(VarRefOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			return n;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0007DC78 File Offset: 0x0007BE78
		public override Node Visit(CaseOp op, Node n)
		{
			foreach (Node node in n.Children)
			{
				if (node.Op.OpType == OpType.Collect)
				{
					throw new NotSupportedException(Strings.ADP_NestingNotSupported(op.OpType.ToString(), node.Op.OpType.ToString()));
				}
				if (node.Op.OpType == OpType.VarRef)
				{
					Var var = ((VarRefOp)node.Op).Var;
					if (this.m_definingNodeMap.ContainsKey(var))
					{
						throw new NotSupportedException(Strings.ADP_NestingNotSupported(op.OpType.ToString(), node.Op.OpType.ToString()));
					}
				}
			}
			return this.VisitDefault(n);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x0007DD80 File Offset: 0x0007BF80
		public override Node Visit(ExistsOp op, Node n)
		{
			Var first = ((ProjectOp)n.Child0.Op).Outputs.First;
			this.VisitChildren(n);
			VarVec outputs = ((ProjectOp)n.Child0.Op).Outputs;
			if (outputs.Count > 1)
			{
				PlanCompiler.Assert(outputs.IsSet(first), "The constant var is not present after NestPull up over the input of ExistsOp.");
				outputs.Clear();
				outputs.Set(first);
			}
			return n;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x0007DDED File Offset: 0x0007BFED
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x0007DDF8 File Offset: 0x0007BFF8
		private Node ApplyOpJoinOp(Op op, Node n)
		{
			this.VisitChildren(n);
			int num = 0;
			foreach (Node node in n.Children)
			{
				NestBaseOp nestBaseOp = node.Op as NestBaseOp;
				if (nestBaseOp != null)
				{
					num++;
					if (OpType.SingleStreamNest == node.Op.OpType)
					{
						throw new InvalidOperationException(Strings.ADP_InternalProviderError(1012));
					}
				}
			}
			if (num == 0)
			{
				return n;
			}
			foreach (Node node2 in n.Children)
			{
				if (op.OpType != OpType.MultiStreamNest && node2.Op.IsRelOp)
				{
					KeyVec keyVec = this.Command.PullupKeys(node2);
					if (keyVec == null || keyVec.NoKeys)
					{
						throw new NotSupportedException(Strings.ADP_KeysRequiredForJoinOverNest(op.OpType.ToString()));
					}
				}
			}
			List<Node> list = new List<Node>();
			List<Node> list2 = new List<Node>();
			List<CollectionInfo> list3 = new List<CollectionInfo>();
			foreach (Node node3 in n.Children)
			{
				if (node3.Op.OpType == OpType.MultiStreamNest)
				{
					list3.AddRange(((MultiStreamNestOp)node3.Op).CollectionInfo);
					if (op.OpType == OpType.FullOuterJoin || ((op.OpType == OpType.LeftOuterJoin || op.OpType == OpType.OuterApply) && n.Child1.Op.OpType == OpType.MultiStreamNest))
					{
						Var var = null;
						list2.Add(this.AugmentNodeWithConstant(node3.Child0, () => this.Command.CreateNullSentinelOp(), out var));
						foreach (CollectionInfo collectionInfo in ((MultiStreamNestOp)node3.Op).CollectionInfo)
						{
							this.m_definingNodeMap[collectionInfo.CollectionVar].Child0 = this.ApplyIsNotNullFilter(this.m_definingNodeMap[collectionInfo.CollectionVar].Child0, var);
						}
						for (int i = 1; i < node3.Children.Count; i++)
						{
							Node node4 = this.ApplyIsNotNullFilter(node3.Children[i], var);
							list.Add(node4);
						}
					}
					else
					{
						list2.Add(node3.Child0);
						for (int j = 1; j < node3.Children.Count; j++)
						{
							list.Add(node3.Children[j]);
						}
					}
				}
				else
				{
					list2.Add(node3);
				}
			}
			Node node5 = this.Command.CreateNode(op, list2);
			list.Insert(0, node5);
			ExtendedNodeInfo extendedNodeInfo = node5.GetExtendedNodeInfo(this.Command);
			VarVec varVec = this.Command.CreateVarVec(extendedNodeInfo.Definitions);
			foreach (CollectionInfo collectionInfo2 in list3)
			{
				varVec.Set(collectionInfo2.CollectionVar);
			}
			NestBaseOp nestBaseOp2 = this.Command.CreateMultiStreamNestOp(new List<SortKey>(), varVec, list3);
			return this.Command.CreateNode(nestBaseOp2, list);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x0007E1DC File Offset: 0x0007C3DC
		private Node ApplyIsNotNullFilter(Node node, Var sentinelVar)
		{
			Node node2 = node;
			Node node3 = null;
			while (node2.Op.OpType == OpType.MultiStreamNest)
			{
				node3 = node2;
				node2 = node2.Child0;
			}
			Node node4 = this.CapWithIsNotNullFilter(node2, sentinelVar);
			Node node5;
			if (node3 != null)
			{
				node3.Child0 = node4;
				node5 = node;
			}
			else
			{
				node5 = node4;
			}
			return node5;
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0007E224 File Offset: 0x0007C424
		private Node CapWithIsNotNullFilter(Node input, Var var)
		{
			Node node = this.Command.CreateNode(this.Command.CreateVarRefOp(var));
			Node node2 = this.Command.CreateNode(this.Command.CreateConditionalOp(OpType.Not), this.Command.CreateNode(this.Command.CreateConditionalOp(OpType.IsNull), node));
			return this.Command.CreateNode(this.Command.CreateFilterOp(), input, node2);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0007E293 File Offset: 0x0007C493
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			return this.ApplyOpJoinOp(op, n);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x0007E29D File Offset: 0x0007C49D
		public override Node Visit(DistinctOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0007E2A8 File Offset: 0x0007C4A8
		public override Node Visit(FilterOp op, Node n)
		{
			this.VisitChildren(n);
			NestBaseOp nestBaseOp = n.Child0.Op as NestBaseOp;
			if (nestBaseOp != null)
			{
				Node child = n.Child0;
				Node child2 = child.Child0;
				n.Child0 = child2;
				child.Child0 = n;
				this.Command.RecomputeNodeInfo(n);
				this.Command.RecomputeNodeInfo(child);
				return child;
			}
			return n;
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x0007E307 File Offset: 0x0007C507
		public override Node Visit(GroupByOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x0007E314 File Offset: 0x0007C514
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			PlanCompiler.Assert(n.HasChild3 && n.Child3.Children.Count > 0, "GroupByIntoOp with no group aggregates?");
			Node child = n.Child3;
			VarVec varVec = this.Command.CreateVarVec(op.Outputs);
			VarVec outputs = op.Outputs;
			foreach (Node node in child.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				outputs.Clear(varDefOp.Var);
			}
			Node node2 = this.Command.CreateNode(this.Command.CreateGroupByOp(op.Keys, outputs), n.Child0, n.Child1, n.Child2);
			Node node3 = this.Command.CreateNode(this.Command.CreateProjectOp(varVec), node2, child);
			return base.VisitNode(node3);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x0007E414 File Offset: 0x0007C614
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			return this.ApplyOpJoinOp(op, n);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x0007E420 File Offset: 0x0007C620
		public override Node Visit(ProjectOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			Node node;
			if (n.Child0.Op.OpType == OpType.Sort)
			{
				Node child = n.Child0;
				foreach (SortKey sortKey in ((SortOp)child.Op).Keys)
				{
					if (!this.Command.GetExtendedNodeInfo(child).ExternalReferences.IsSet(sortKey.Var))
					{
						op.Outputs.Set(sortKey.Var);
					}
				}
				n.Child0 = child.Child0;
				this.Command.RecomputeNodeInfo(n);
				child.Child0 = this.HandleProjectNode(n);
				this.Command.RecomputeNodeInfo(child);
				node = child;
			}
			else
			{
				node = this.HandleProjectNode(n);
			}
			return node;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x0007E514 File Offset: 0x0007C714
		private Node HandleProjectNode(Node n)
		{
			Node node = this.ProjectOpCase1(n);
			if (node.Op.OpType == OpType.Project && NestPullup.IsNestOpNode(node.Child0))
			{
				node = this.ProjectOpCase2(node);
			}
			return this.MergeNestedNestOps(node);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0007E558 File Offset: 0x0007C758
		private Node MergeNestedNestOps(Node nestNode)
		{
			if (!NestPullup.IsNestOpNode(nestNode) || !NestPullup.IsNestOpNode(nestNode.Child0))
			{
				return nestNode;
			}
			NestBaseOp nestBaseOp = (NestBaseOp)nestNode.Op;
			Node child = nestNode.Child0;
			NestBaseOp nestBaseOp2 = (NestBaseOp)child.Op;
			VarVec varVec = this.Command.CreateVarVec();
			foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
			{
				varVec.Set(collectionInfo.CollectionVar);
			}
			List<Node> list = new List<Node>();
			List<CollectionInfo> list2 = new List<CollectionInfo>();
			VarVec varVec2 = this.Command.CreateVarVec(nestBaseOp.Outputs);
			list.Add(child.Child0);
			for (int i = 1; i < child.Children.Count; i++)
			{
				CollectionInfo collectionInfo2 = nestBaseOp2.CollectionInfo[i - 1];
				if (varVec.IsSet(collectionInfo2.CollectionVar) || varVec2.IsSet(collectionInfo2.CollectionVar))
				{
					list2.Add(collectionInfo2);
					list.Add(child.Children[i]);
					PlanCompiler.Assert(varVec2.IsSet(collectionInfo2.CollectionVar), "collectionVar not in output Vars?");
				}
			}
			for (int j = 1; j < nestNode.Children.Count; j++)
			{
				CollectionInfo collectionInfo3 = nestBaseOp.CollectionInfo[j - 1];
				list2.Add(collectionInfo3);
				list.Add(nestNode.Children[j]);
				PlanCompiler.Assert(varVec2.IsSet(collectionInfo3.CollectionVar), "collectionVar not in output Vars?");
			}
			List<SortKey> list3 = this.ConsolidateSortKeys(nestBaseOp.PrefixSortKeys, nestBaseOp2.PrefixSortKeys);
			foreach (SortKey sortKey in list3)
			{
				varVec2.Set(sortKey.Var);
			}
			MultiStreamNestOp multiStreamNestOp = this.Command.CreateMultiStreamNestOp(list3, varVec2, list2);
			Node node = this.Command.CreateNode(multiStreamNestOp, list);
			this.Command.RecomputeNodeInfo(node);
			return node;
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0007E790 File Offset: 0x0007C990
		private Node ProjectOpCase1(Node projectNode)
		{
			ProjectOp projectOp = (ProjectOp)projectNode.Op;
			List<CollectionInfo> list = new List<CollectionInfo>();
			List<Node> list2 = new List<Node>();
			List<Node> list3 = new List<Node>();
			VarVec varVec = this.Command.CreateVarVec();
			VarVec varVec2 = this.Command.CreateVarVec();
			List<Node> list4 = new List<Node>();
			List<Node> list5 = new List<Node>();
			foreach (Node node in projectNode.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node.Op;
				Node child = node.Child0;
				if (OpType.Collect == child.Op.OpType)
				{
					PlanCompiler.Assert(child.HasChild0, "collect without input?");
					PlanCompiler.Assert(OpType.PhysicalProject == child.Child0.Op.OpType, "collect without physicalProject?");
					Node child2 = child.Child0;
					this.m_definingNodeMap.Add(varDefOp.Var, child2);
					this.ConvertToNestOpInput(child2, varDefOp.Var, list, list3, varVec, varVec2);
				}
				else if (OpType.VarRef == child.Op.OpType)
				{
					Var var = ((VarRefOp)child.Op).Var;
					Node node2;
					if (this.m_definingNodeMap.TryGetValue(var, out node2))
					{
						node2 = this.CopyCollectionVarDefinition(node2);
						this.m_definingNodeMap.Add(varDefOp.Var, node2);
						this.ConvertToNestOpInput(node2, varDefOp.Var, list, list3, varVec, varVec2);
					}
					else
					{
						list5.Add(node);
						list2.Add(node);
					}
				}
				else
				{
					list4.Add(node);
					list2.Add(node);
				}
			}
			if (list3.Count == 0)
			{
				return projectNode;
			}
			VarVec varVec3 = this.Command.CreateVarVec(projectOp.Outputs);
			VarVec varVec4 = this.Command.CreateVarVec(projectOp.Outputs);
			varVec4.Minus(varVec2);
			varVec4.Or(varVec);
			if (!varVec4.IsEmpty)
			{
				if (NestPullup.IsNestOpNode(projectNode.Child0))
				{
					if (list4.Count == 0 && list5.Count == 0)
					{
						projectNode = projectNode.Child0;
						this.EnsureReferencedVarsAreRemoved(list5, varVec3);
					}
					else
					{
						NestBaseOp nestBaseOp = (NestBaseOp)projectNode.Child0.Op;
						List<Node> list6 = new List<Node>();
						list6.Add(projectNode.Child0.Child0);
						list5.AddRange(list4);
						list6.Add(this.Command.CreateNode(this.Command.CreateVarDefListOp(), list5));
						VarVec varVec5 = this.Command.CreateVarVec(nestBaseOp.Outputs);
						foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
						{
							varVec5.Clear(collectionInfo.CollectionVar);
						}
						foreach (Node node3 in list5)
						{
							varVec5.Set(((VarDefOp)node3.Op).Var);
						}
						Node node4 = this.Command.CreateNode(this.Command.CreateProjectOp(varVec5), list6);
						VarVec varVec6 = this.Command.CreateVarVec(varVec5);
						varVec6.Or(nestBaseOp.Outputs);
						MultiStreamNestOp multiStreamNestOp = this.Command.CreateMultiStreamNestOp(nestBaseOp.PrefixSortKeys, varVec6, nestBaseOp.CollectionInfo);
						List<Node> list7 = new List<Node>();
						list7.Add(node4);
						for (int i = 1; i < projectNode.Child0.Children.Count; i++)
						{
							list7.Add(projectNode.Child0.Children[i]);
						}
						projectNode = this.Command.CreateNode(multiStreamNestOp, list7);
					}
				}
				else
				{
					ProjectOp projectOp2 = this.Command.CreateProjectOp(varVec4);
					projectNode.Child1 = this.Command.CreateNode(projectNode.Child1.Op, list2);
					projectNode.Op = projectOp2;
					this.EnsureReferencedVarsAreRemapped(list5);
				}
			}
			else
			{
				projectNode = projectNode.Child0;
				this.EnsureReferencedVarsAreRemoved(list5, varVec3);
			}
			varVec.And(projectNode.GetExtendedNodeInfo(this.Command).Definitions);
			varVec3.Or(varVec);
			MultiStreamNestOp multiStreamNestOp2 = this.Command.CreateMultiStreamNestOp(new List<SortKey>(), varVec3, list);
			list3.Insert(0, projectNode);
			Node node5 = this.Command.CreateNode(multiStreamNestOp2, list3);
			this.Command.RecomputeNodeInfo(projectNode);
			this.Command.RecomputeNodeInfo(node5);
			return node5;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x0007EC64 File Offset: 0x0007CE64
		private void EnsureReferencedVarsAreRemoved(List<Node> referencedVars, VarVec outputVars)
		{
			foreach (Node node in referencedVars)
			{
				Var var = ((VarDefOp)node.Op).Var;
				Var var2 = this.ResolveVarReference(var);
				this.m_varRemapper.AddMapping(var, var2);
				outputVars.Clear(var);
				outputVars.Set(var2);
			}
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x0007ECE0 File Offset: 0x0007CEE0
		private void EnsureReferencedVarsAreRemapped(List<Node> referencedVars)
		{
			foreach (Node node in referencedVars)
			{
				Var var = ((VarDefOp)node.Op).Var;
				Var var2 = this.ResolveVarReference(var);
				this.m_varRemapper.AddMapping(var2, var);
			}
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x0007ED4C File Offset: 0x0007CF4C
		private void ConvertToNestOpInput(Node physicalProjectNode, Var collectionVar, List<CollectionInfo> collectionInfoList, List<Node> collectionNodes, VarVec externalReferences, VarVec collectionReferences)
		{
			externalReferences.Or(this.Command.GetNodeInfo(physicalProjectNode).ExternalReferences);
			Node child = physicalProjectNode.Child0;
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)physicalProjectNode.Op;
			VarList varList = Command.CreateVarList(physicalProjectOp.Outputs);
			VarVec varVec = this.Command.CreateVarVec(varList);
			List<SortKey> list = null;
			if (OpType.Sort == child.Op.OpType)
			{
				SortOp sortOp = (SortOp)child.Op;
				list = OpCopier.Copy(this.Command, sortOp.Keys);
				using (List<SortKey>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						SortKey sortKey = enumerator.Current;
						if (!varVec.IsSet(sortKey.Var))
						{
							varList.Add(sortKey.Var);
							varVec.Set(sortKey.Var);
						}
					}
					goto IL_00D4;
				}
			}
			list = new List<SortKey>();
			IL_00D4:
			VarVec keyVars = this.Command.GetExtendedNodeInfo(child).Keys.KeyVars;
			VarVec varVec2 = keyVars.Clone();
			varVec2.Minus(varVec);
			VarVec varVec3 = (varVec2.IsEmpty ? keyVars.Clone() : this.Command.CreateVarVec());
			CollectionInfo collectionInfo = Command.CreateCollectionInfo(collectionVar, physicalProjectOp.ColumnMap.Element, varList, varVec3, list, null);
			collectionInfoList.Add(collectionInfo);
			collectionNodes.Add(child);
			collectionReferences.Set(collectionVar);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0007EEB4 File Offset: 0x0007D0B4
		private Node ProjectOpCase2(Node projectNode)
		{
			ProjectOp projectOp = (ProjectOp)projectNode.Op;
			Node node = projectNode.Child0;
			NestBaseOp nestBaseOp = node.Op as NestBaseOp;
			VarVec varVec = this.Command.CreateVarVec();
			foreach (CollectionInfo collectionInfo in nestBaseOp.CollectionInfo)
			{
				varVec.Set(collectionInfo.CollectionVar);
			}
			VarVec varVec2 = this.Command.CreateVarVec(nestBaseOp.Outputs);
			varVec2.Minus(varVec);
			VarVec varVec3 = this.Command.CreateVarVec(projectOp.Outputs);
			varVec3.Minus(varVec);
			VarVec varVec4 = this.Command.CreateVarVec(projectOp.Outputs);
			varVec4.Minus(varVec3);
			VarVec varVec5 = this.Command.CreateVarVec(varVec);
			varVec5.Minus(varVec4);
			List<CollectionInfo> list;
			List<Node> list2;
			if (varVec5.IsEmpty)
			{
				list = nestBaseOp.CollectionInfo;
				list2 = new List<Node>(node.Children);
			}
			else
			{
				list = new List<CollectionInfo>();
				list2 = new List<Node>();
				list2.Add(node.Child0);
				int num = 1;
				foreach (CollectionInfo collectionInfo2 in nestBaseOp.CollectionInfo)
				{
					if (!varVec5.IsSet(collectionInfo2.CollectionVar))
					{
						list.Add(collectionInfo2);
						list2.Add(node.Children[num]);
					}
					num++;
				}
			}
			VarVec varVec6 = this.Command.CreateVarVec();
			for (int i = 1; i < node.Children.Count; i++)
			{
				varVec6.Or(node.Children[i].GetExtendedNodeInfo(this.Command).ExternalReferences);
			}
			varVec6.And(node.Child0.GetExtendedNodeInfo(this.Command).Definitions);
			VarVec varVec7 = this.Command.CreateVarVec(varVec3);
			varVec7.Or(varVec2);
			varVec7.Or(varVec6);
			List<Node> list3 = new List<Node>(projectNode.Child1.Children.Count);
			foreach (Node node2 in projectNode.Child1.Children)
			{
				VarDefOp varDefOp = (VarDefOp)node2.Op;
				if (varVec7.IsSet(varDefOp.Var))
				{
					list3.Add(node2);
				}
			}
			if (list.Count != 0 && varVec7.IsEmpty)
			{
				PlanCompiler.Assert(list3.Count == 0, "outputs is empty with non-zero count of children?");
				NullOp nullOp = this.Command.CreateNullOp(this.Command.StringType);
				Node node3 = this.Command.CreateNode(nullOp);
				Var var;
				Node node4 = this.Command.CreateVarDefNode(node3, out var);
				list3.Add(node4);
				varVec7.Set(var);
			}
			projectNode.Op = this.Command.CreateProjectOp(this.Command.CreateVarVec(varVec7));
			projectNode.Child1 = this.Command.CreateNode(projectNode.Child1.Op, list3);
			if (list.Count == 0)
			{
				projectNode.Child0 = node.Child0;
				node = projectNode;
			}
			else
			{
				VarVec varVec8 = this.Command.CreateVarVec(projectOp.Outputs);
				for (int j = 1; j < list2.Count; j++)
				{
					varVec8.Or(list2[j].GetNodeInfo(this.Command).ExternalReferences);
				}
				foreach (SortKey sortKey in nestBaseOp.PrefixSortKeys)
				{
					varVec8.Set(sortKey.Var);
				}
				node.Op = this.Command.CreateMultiStreamNestOp(nestBaseOp.PrefixSortKeys, varVec8, list);
				node = this.Command.CreateNode(node.Op, list2);
				projectNode.Child0 = node.Child0;
				node.Child0 = projectNode;
				this.Command.RecomputeNodeInfo(projectNode);
			}
			this.Command.RecomputeNodeInfo(node);
			return node;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x0007F314 File Offset: 0x0007D514
		protected override Node VisitSetOp(SetOp op, Node n)
		{
			return this.NestingNotSupported(op, n);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x0007F320 File Offset: 0x0007D520
		public override Node Visit(SingleRowOp op, Node n)
		{
			this.VisitChildren(n);
			if (NestPullup.IsNestOpNode(n.Child0))
			{
				n = n.Child0;
				Node node = this.Command.CreateNode(op, n.Child0);
				n.Child0 = node;
				this.Command.RecomputeNodeInfo(n);
			}
			return n;
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x0007F370 File Offset: 0x0007D570
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			NestBaseOp nestBaseOp = n.Child0.Op as NestBaseOp;
			if (nestBaseOp != null)
			{
				n.Child0.Op = this.GetNestOpWithConsolidatedSortKeys(nestBaseOp, op.Keys);
				return n.Child0;
			}
			return n;
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x0007F3C4 File Offset: 0x0007D5C4
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			this.VisitChildren(n);
			NestBaseOp nestBaseOp = n.Child0.Op as NestBaseOp;
			if (nestBaseOp != null)
			{
				Node child = n.Child0;
				n.Child0 = child.Child0;
				child.Child0 = n;
				child.Op = this.GetNestOpWithConsolidatedSortKeys(nestBaseOp, op.Keys);
				n = child;
			}
			return n;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x0007F420 File Offset: 0x0007D620
		private NestBaseOp GetNestOpWithConsolidatedSortKeys(NestBaseOp inputNestOp, List<SortKey> sortKeys)
		{
			NestBaseOp nestBaseOp;
			if (inputNestOp.PrefixSortKeys.Count == 0)
			{
				foreach (SortKey sortKey in sortKeys)
				{
					inputNestOp.PrefixSortKeys.Add(Command.CreateSortKey(sortKey.Var, sortKey.AscendingSort, sortKey.Collation));
				}
				nestBaseOp = inputNestOp;
			}
			else
			{
				List<SortKey> list = this.ConsolidateSortKeys(sortKeys, inputNestOp.PrefixSortKeys);
				PlanCompiler.Assert(inputNestOp is MultiStreamNestOp, "Unexpected SingleStreamNestOp?");
				nestBaseOp = this.Command.CreateMultiStreamNestOp(list, inputNestOp.Outputs, inputNestOp.CollectionInfo);
			}
			return nestBaseOp;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x0007F4D8 File Offset: 0x0007D6D8
		private List<SortKey> ConsolidateSortKeys(List<SortKey> sortKeyList1, List<SortKey> sortKeyList2)
		{
			VarVec varVec = this.Command.CreateVarVec();
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in sortKeyList1)
			{
				if (!varVec.IsSet(sortKey.Var))
				{
					varVec.Set(sortKey.Var);
					list.Add(Command.CreateSortKey(sortKey.Var, sortKey.AscendingSort, sortKey.Collation));
				}
			}
			foreach (SortKey sortKey2 in sortKeyList2)
			{
				if (!varVec.IsSet(sortKey2.Var))
				{
					varVec.Set(sortKey2.Var);
					list.Add(Command.CreateSortKey(sortKey2.Var, sortKey2.AscendingSort, sortKey2.Collation));
				}
			}
			return list;
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x0007F5DC File Offset: 0x0007D7DC
		public override Node Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
			PlanCompiler.Assert(n.Child0.Op.OpType == OpType.VarDef, "Un-nest without VarDef input?");
			PlanCompiler.Assert(((VarDefOp)n.Child0.Op).Var == op.Var, "Un-nest var not found?");
			PlanCompiler.Assert(n.Child0.HasChild0, "VarDef without input?");
			Node node = n.Child0.Child0;
			if (OpType.Function == node.Op.OpType)
			{
				return n;
			}
			if (OpType.Collect == node.Op.OpType)
			{
				PlanCompiler.Assert(node.HasChild0, "collect without input?");
				node = node.Child0;
				PlanCompiler.Assert(node.Op.OpType == OpType.PhysicalProject, "collect without physicalProject?");
				this.m_definingNodeMap.Add(op.Var, node);
			}
			else
			{
				if (OpType.VarRef != node.Op.OpType)
				{
					throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.InvalidInternalTree, 2, node.Op.OpType);
				}
				Var var = ((VarRefOp)node.Op).Var;
				Node node2;
				PlanCompiler.Assert(this.m_definingNodeMap.TryGetValue(var, out node2), "Could not find a definition for a referenced collection var");
				node = this.CopyCollectionVarDefinition(node2);
				PlanCompiler.Assert(node.Op.OpType == OpType.PhysicalProject, "driving node is not physicalProject?");
			}
			IEnumerable<Var> outputs = ((PhysicalProjectOp)node.Op).Outputs;
			PlanCompiler.Assert(node.HasChild0, "physicalProject without input?");
			node = node.Child0;
			if (node.Op.OpType == OpType.Sort)
			{
				this.m_foundSortUnderUnnest = true;
			}
			this.UpdateReplacementVarMap(op.Table.Columns, outputs);
			return node;
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x0007F784 File Offset: 0x0007D984
		private Node CopyCollectionVarDefinition(Node refVarDefiningNode)
		{
			VarMap varMap;
			Dictionary<Var, Node> dictionary;
			Node node = OpCopierTrackingCollectionVars.Copy(this.Command, refVarDefiningNode, out varMap, out dictionary);
			if (dictionary.Count != 0)
			{
				VarMap reverseMap = varMap.GetReverseMap();
				foreach (KeyValuePair<Var, Node> keyValuePair in dictionary)
				{
					Var var = reverseMap[keyValuePair.Key];
					Node node2;
					if (this.m_definingNodeMap.TryGetValue(var, out node2))
					{
						PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)node2.Op;
						VarList varList = VarRemapper.RemapVarList(this.Command, varMap, physicalProjectOp.Outputs);
						SimpleCollectionColumnMap simpleCollectionColumnMap = (SimpleCollectionColumnMap)ColumnMapCopier.Copy(physicalProjectOp.ColumnMap, varMap);
						PhysicalProjectOp physicalProjectOp2 = this.Command.CreatePhysicalProjectOp(varList, simpleCollectionColumnMap);
						Node node3 = this.Command.CreateNode(physicalProjectOp2, keyValuePair.Value);
						this.m_definingNodeMap.Add(keyValuePair.Key, node3);
					}
				}
			}
			return node;
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x0007F88C File Offset: 0x0007DA8C
		protected override Node VisitNestOp(NestBaseOp op, Node n)
		{
			this.VisitChildren(n);
			using (List<Node>.Enumerator enumerator = n.Children.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (NestPullup.IsNestOpNode(enumerator.Current))
					{
						throw new InvalidOperationException(Strings.ADP_InternalProviderError(1002));
					}
				}
			}
			return n;
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x0007F8FC File Offset: 0x0007DAFC
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			PlanCompiler.Assert(n.Children.Count == 1, "multiple inputs to physicalProject?");
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			if (n != this.Command.Root || !NestPullup.IsNestOpNode(n.Child0))
			{
				return n;
			}
			Node node = n.Child0;
			Dictionary<Var, ColumnMap> dictionary = new Dictionary<Var, ColumnMap>();
			VarList varList = Command.CreateVarList(op.Outputs.Where((Var v) => v.VarType == VarType.Parameter));
			SimpleColumnMap[] array;
			node = this.ConvertToSingleStreamNest(node, dictionary, varList, out array);
			SingleStreamNestOp singleStreamNestOp = (SingleStreamNestOp)node.Op;
			Node node2 = this.BuildSortForNestElimination(singleStreamNestOp, node);
			SimpleCollectionColumnMap simpleCollectionColumnMap = (SimpleCollectionColumnMap)ColumnMapTranslator.Translate(((PhysicalProjectOp)n.Op).ColumnMap, dictionary);
			simpleCollectionColumnMap = new SimpleCollectionColumnMap(simpleCollectionColumnMap.Type, simpleCollectionColumnMap.Name, simpleCollectionColumnMap.Element, array, null);
			n.Op = this.Command.CreatePhysicalProjectOp(varList, simpleCollectionColumnMap);
			n.Child0 = node2;
			return n;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0007FA0C File Offset: 0x0007DC0C
		private Node BuildSortForNestElimination(SingleStreamNestOp ssnOp, Node nestNode)
		{
			List<SortKey> list = this.BuildSortKeyList(ssnOp);
			Node node;
			if (list.Count > 0)
			{
				SortOp sortOp = this.Command.CreateSortOp(list);
				node = this.Command.CreateNode(sortOp, nestNode.Child0);
			}
			else
			{
				node = nestNode.Child0;
			}
			return node;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0007FA54 File Offset: 0x0007DC54
		private List<SortKey> BuildSortKeyList(SingleStreamNestOp ssnOp)
		{
			VarVec varVec = this.Command.CreateVarVec();
			List<SortKey> list = new List<SortKey>();
			foreach (SortKey sortKey in ssnOp.PrefixSortKeys)
			{
				if (!varVec.IsSet(sortKey.Var))
				{
					varVec.Set(sortKey.Var);
					list.Add(sortKey);
				}
			}
			foreach (Var var in ssnOp.Keys)
			{
				if (!varVec.IsSet(var))
				{
					varVec.Set(var);
					SortKey sortKey2 = Command.CreateSortKey(var);
					list.Add(sortKey2);
				}
			}
			PlanCompiler.Assert(!varVec.IsSet(ssnOp.Discriminator), "prefix sort on discriminator?");
			list.Add(Command.CreateSortKey(ssnOp.Discriminator));
			foreach (SortKey sortKey3 in ssnOp.PostfixSortKeys)
			{
				if (!varVec.IsSet(sortKey3.Var))
				{
					varVec.Set(sortKey3.Var);
					list.Add(sortKey3);
				}
			}
			return list;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0007FBBC File Offset: 0x0007DDBC
		private Node ConvertToSingleStreamNest(Node nestNode, Dictionary<Var, ColumnMap> varRefReplacementMap, VarList flattenedOutputVarList, out SimpleColumnMap[] parentKeyColumnMaps)
		{
			MultiStreamNestOp multiStreamNestOp = (MultiStreamNestOp)nestNode.Op;
			for (int i = 1; i < nestNode.Children.Count; i++)
			{
				Node node = nestNode.Children[i];
				if (node.Op.OpType == OpType.MultiStreamNest)
				{
					CollectionInfo collectionInfo = multiStreamNestOp.CollectionInfo[i - 1];
					VarList varList = Command.CreateVarList();
					SimpleColumnMap[] array;
					nestNode.Children[i] = this.ConvertToSingleStreamNest(node, varRefReplacementMap, varList, out array);
					ColumnMap columnMap = ColumnMapTranslator.Translate(collectionInfo.ColumnMap, varRefReplacementMap);
					VarVec varVec = this.Command.CreateVarVec(((SingleStreamNestOp)nestNode.Children[i].Op).Keys);
					multiStreamNestOp.CollectionInfo[i - 1] = Command.CreateCollectionInfo(collectionInfo.CollectionVar, columnMap, varList, varVec, collectionInfo.SortKeys, null);
				}
			}
			Node child = nestNode.Child0;
			KeyVec keyVec = this.Command.PullupKeys(child);
			if (keyVec.NoKeys)
			{
				throw new NotSupportedException(Strings.ADP_KeysRequiredForNesting);
			}
			VarList varList2 = Command.CreateVarList(this.Command.GetExtendedNodeInfo(child).Definitions);
			VarList varList3;
			List<List<SortKey>> list;
			this.NormalizeNestOpInputs(multiStreamNestOp, nestNode, out varList3, out list);
			Var var;
			List<Dictionary<Var, Var>> list2;
			Node node2 = this.BuildUnionAllSubqueryForNestOp(multiStreamNestOp, nestNode, varList2, varList3, out var, out list2);
			Dictionary<Var, Var> dictionary = list2[0];
			flattenedOutputVarList.AddRange(NestPullup.RemapVars(varList2, dictionary));
			VarVec varVec2 = this.Command.CreateVarVec(flattenedOutputVarList);
			VarVec varVec3 = this.Command.CreateVarVec(varVec2);
			foreach (KeyValuePair<Var, Var> keyValuePair in dictionary)
			{
				if (keyValuePair.Key != keyValuePair.Value)
				{
					varRefReplacementMap[keyValuePair.Key] = new VarRefColumnMap(keyValuePair.Value);
				}
			}
			NestPullup.RemapSortKeys(multiStreamNestOp.PrefixSortKeys, dictionary);
			List<SortKey> list3 = new List<SortKey>();
			List<CollectionInfo> list4 = new List<CollectionInfo>();
			VarRefColumnMap varRefColumnMap = new VarRefColumnMap(var);
			varVec3.Set(var);
			if (!varVec2.IsSet(var))
			{
				flattenedOutputVarList.Add(var);
				varVec2.Set(var);
			}
			VarVec varVec4 = this.RemapVarVec(keyVec.KeyVars, dictionary);
			parentKeyColumnMaps = new SimpleColumnMap[varVec4.Count];
			int num = 0;
			foreach (Var var2 in varVec4)
			{
				parentKeyColumnMaps[num] = new VarRefColumnMap(var2);
				num++;
				if (!varVec2.IsSet(var2))
				{
					flattenedOutputVarList.Add(var2);
					varVec2.Set(var2);
				}
			}
			for (int j = 1; j < nestNode.Children.Count; j++)
			{
				CollectionInfo collectionInfo2 = multiStreamNestOp.CollectionInfo[j - 1];
				List<SortKey> list5 = list[j];
				NestPullup.RemapSortKeys(list5, list2[j]);
				list3.AddRange(list5);
				ColumnMap columnMap2 = ColumnMapTranslator.Translate(collectionInfo2.ColumnMap, list2[j]);
				VarList varList4 = NestPullup.RemapVarList(collectionInfo2.FlattenedElementVars, list2[j]);
				VarVec varVec5 = this.RemapVarVec(collectionInfo2.Keys, list2[j]);
				NestPullup.RemapSortKeys(collectionInfo2.SortKeys, list2[j]);
				CollectionInfo collectionInfo3 = Command.CreateCollectionInfo(collectionInfo2.CollectionVar, columnMap2, varList4, varVec5, collectionInfo2.SortKeys, j);
				list4.Add(collectionInfo3);
				foreach (Var var3 in varList4)
				{
					if (!varVec2.IsSet(var3))
					{
						flattenedOutputVarList.Add(var3);
						varVec2.Set(var3);
					}
				}
				varVec3.Set(collectionInfo2.CollectionVar);
				int num2 = 0;
				SimpleColumnMap[] array2 = new SimpleColumnMap[collectionInfo3.Keys.Count];
				foreach (Var var4 in collectionInfo3.Keys)
				{
					array2[num2] = new VarRefColumnMap(var4);
					num2++;
				}
				DiscriminatedCollectionColumnMap discriminatedCollectionColumnMap = new DiscriminatedCollectionColumnMap(TypeUtils.CreateCollectionType(collectionInfo3.ColumnMap.Type), collectionInfo3.ColumnMap.Name, collectionInfo3.ColumnMap, array2, parentKeyColumnMaps, varRefColumnMap, collectionInfo3.DiscriminatorValue);
				varRefReplacementMap[collectionInfo2.CollectionVar] = discriminatedCollectionColumnMap;
			}
			SingleStreamNestOp singleStreamNestOp = this.Command.CreateSingleStreamNestOp(varVec4, multiStreamNestOp.PrefixSortKeys, list3, varVec3, list4, var);
			return this.Command.CreateNode(singleStreamNestOp, node2);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00080088 File Offset: 0x0007E288
		private void NormalizeNestOpInputs(NestBaseOp nestOp, Node nestNode, out VarList discriminatorVarList, out List<List<SortKey>> sortKeys)
		{
			discriminatorVarList = Command.CreateVarList();
			discriminatorVarList.Add(null);
			sortKeys = new List<List<SortKey>>();
			sortKeys.Add(nestOp.PrefixSortKeys);
			for (int i = 1; i < nestNode.Children.Count; i++)
			{
				Node node = nestNode.Children[i];
				SingleStreamNestOp singleStreamNestOp = node.Op as SingleStreamNestOp;
				if (singleStreamNestOp != null)
				{
					List<SortKey> list = this.BuildSortKeyList(singleStreamNestOp);
					sortKeys.Add(list);
					node = node.Child0;
				}
				else
				{
					SortOp sortOp = node.Op as SortOp;
					if (sortOp != null)
					{
						node = node.Child0;
						sortKeys.Add(sortOp.Keys);
					}
					else
					{
						sortKeys.Add(new List<SortKey>());
					}
				}
				VarList flattenedElementVars = nestOp.CollectionInfo[i - 1].FlattenedElementVars;
				foreach (SortKey sortKey in sortKeys[i])
				{
					if (!flattenedElementVars.Contains(sortKey.Var))
					{
						flattenedElementVars.Add(sortKey.Var);
					}
				}
				Var var;
				Node node2 = this.AugmentNodeWithInternalIntegerConstant(node, i, out var);
				nestNode.Children[i] = node2;
				discriminatorVarList.Add(var);
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000801DC File Offset: 0x0007E3DC
		private Node AugmentNodeWithInternalIntegerConstant(Node input, int value, out Var internalConstantVar)
		{
			return this.AugmentNodeWithConstant(input, () => this.Command.CreateInternalConstantOp(this.Command.IntegerType, value), out internalConstantVar);
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00080214 File Offset: 0x0007E414
		private Node AugmentNodeWithConstant(Node input, Func<ConstantBaseOp> createOp, out Var constantVar)
		{
			ConstantBaseOp constantBaseOp = createOp();
			Node node = this.Command.CreateNode(constantBaseOp);
			Node node2 = this.Command.CreateVarDefListNode(node, out constantVar);
			ExtendedNodeInfo extendedNodeInfo = this.Command.GetExtendedNodeInfo(input);
			VarVec varVec = this.Command.CreateVarVec(extendedNodeInfo.Definitions);
			varVec.Set(constantVar);
			ProjectOp projectOp = this.Command.CreateProjectOp(varVec);
			return this.Command.CreateNode(projectOp, input, node2);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0008028C File Offset: 0x0007E48C
		private Node BuildUnionAllSubqueryForNestOp(NestBaseOp nestOp, Node nestNode, VarList drivingNodeVars, VarList discriminatorVarList, out Var discriminatorVar, out List<Dictionary<Var, Var>> varMapList)
		{
			Node child = nestNode.Child0;
			Node node = null;
			VarList varList = null;
			for (int i = 1; i < nestNode.Children.Count; i++)
			{
				VarList varList2;
				Node node2;
				VarList varList3;
				Op op;
				if (i > 1)
				{
					node2 = OpCopier.Copy(this.Command, child, drivingNodeVars, out varList2);
					VarRemapper varRemapper = new VarRemapper(this.Command);
					for (int j = 0; j < drivingNodeVars.Count; j++)
					{
						varRemapper.AddMapping(drivingNodeVars[j], varList2[j]);
					}
					varRemapper.RemapSubtree(nestNode.Children[i]);
					varList3 = varRemapper.RemapVarList(nestOp.CollectionInfo[i - 1].FlattenedElementVars);
					op = this.Command.CreateCrossApplyOp();
				}
				else
				{
					node2 = child;
					varList2 = drivingNodeVars;
					varList3 = nestOp.CollectionInfo[i - 1].FlattenedElementVars;
					op = this.Command.CreateOuterApplyOp();
				}
				Node node3 = this.Command.CreateNode(op, node2, nestNode.Children[i]);
				List<Node> list = new List<Node>();
				VarList varList4 = Command.CreateVarList();
				varList4.Add(discriminatorVarList[i]);
				varList4.AddRange(varList2);
				for (int k = 1; k < nestNode.Children.Count; k++)
				{
					CollectionInfo collectionInfo = nestOp.CollectionInfo[k - 1];
					if (i == k)
					{
						varList4.AddRange(varList3);
					}
					else
					{
						foreach (Var var in collectionInfo.FlattenedElementVars)
						{
							NullOp nullOp = this.Command.CreateNullOp(var.Type);
							Node node4 = this.Command.CreateNode(nullOp);
							Var var2;
							Node node5 = this.Command.CreateVarDefNode(node4, out var2);
							list.Add(node5);
							varList4.Add(var2);
						}
					}
				}
				Node node6 = this.Command.CreateNode(this.Command.CreateVarDefListOp(), list);
				VarVec varVec = this.Command.CreateVarVec(varList4);
				ProjectOp projectOp = this.Command.CreateProjectOp(varVec);
				Node node7 = this.Command.CreateNode(projectOp, node3, node6);
				if (node == null)
				{
					node = node7;
					varList = varList4;
				}
				else
				{
					VarMap varMap = new VarMap();
					VarMap varMap2 = new VarMap();
					for (int l = 0; l < varList.Count; l++)
					{
						Var var3 = this.Command.CreateSetOpVar(varList[l].Type);
						varMap.Add(var3, varList[l]);
						varMap2.Add(var3, varList4[l]);
					}
					UnionAllOp unionAllOp = this.Command.CreateUnionAllOp(varMap, varMap2);
					node = this.Command.CreateNode(unionAllOp, node, node7);
					varList = NestPullup.GetUnionOutputs(unionAllOp, varList);
				}
			}
			varMapList = new List<Dictionary<Var, Var>>();
			IEnumerator<Var> enumerator2 = varList.GetEnumerator();
			if (!enumerator2.MoveNext())
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 4, null);
			}
			discriminatorVar = enumerator2.Current;
			for (int m = 0; m < nestNode.Children.Count; m++)
			{
				Dictionary<Var, Var> dictionary = new Dictionary<Var, Var>();
				foreach (Var var4 in ((m == 0) ? drivingNodeVars : nestOp.CollectionInfo[m - 1].FlattenedElementVars))
				{
					if (!enumerator2.MoveNext())
					{
						throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 5, null);
					}
					dictionary[var4] = enumerator2.Current;
				}
				varMapList.Add(dictionary);
			}
			if (enumerator2.MoveNext())
			{
				throw EntityUtil.InternalError(EntityUtil.InternalErrorCode.ColumnCountMismatch, 6, null);
			}
			return node;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00080664 File Offset: 0x0007E864
		private static VarList GetUnionOutputs(UnionAllOp unionOp, VarList leftVars)
		{
			Dictionary<Var, Var> reverseMap = unionOp.VarMap[0].GetReverseMap();
			VarList varList = Command.CreateVarList();
			foreach (Var var in leftVars)
			{
				Var var2 = reverseMap[var];
				varList.Add(var2);
			}
			return varList;
		}

		// Token: 0x04000E2B RID: 3627
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04000E2C RID: 3628
		private readonly Dictionary<Var, Node> m_definingNodeMap = new Dictionary<Var, Node>();

		// Token: 0x04000E2D RID: 3629
		private readonly VarRemapper m_varRemapper;

		// Token: 0x04000E2E RID: 3630
		private readonly Dictionary<Var, Var> m_varRefMap = new Dictionary<Var, Var>();

		// Token: 0x04000E2F RID: 3631
		private bool m_foundSortUnderUnnest;
	}
}
