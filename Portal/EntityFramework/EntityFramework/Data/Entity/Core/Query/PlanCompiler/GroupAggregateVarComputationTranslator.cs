using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000343 RID: 835
	internal class GroupAggregateVarComputationTranslator : BasicOpVisitorOfNode
	{
		// Token: 0x060027C7 RID: 10183 RVA: 0x000758A4 File Offset: 0x00073AA4
		private GroupAggregateVarComputationTranslator(Command command, GroupAggregateVarInfoManager groupAggregateVarInfoManager)
		{
			this._command = command;
			this._groupAggregateVarInfoManager = groupAggregateVarInfoManager;
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000758BC File Offset: 0x00073ABC
		public static bool TryTranslateOverGroupAggregateVar(Node subtree, bool isVarDefinition, Command command, GroupAggregateVarInfoManager groupAggregateVarInfoManager, out GroupAggregateVarInfo groupAggregateVarInfo, out Node templateNode, out bool isUnnested)
		{
			GroupAggregateVarComputationTranslator groupAggregateVarComputationTranslator = new GroupAggregateVarComputationTranslator(command, groupAggregateVarInfoManager);
			Node node = subtree;
			SoftCastOp softCastOp = null;
			if (node.Op.OpType == OpType.SoftCast)
			{
				softCastOp = (SoftCastOp)node.Op;
				node = node.Child0;
			}
			bool flag;
			if (node.Op.OpType == OpType.Collect)
			{
				templateNode = groupAggregateVarComputationTranslator.VisitCollect(node);
				flag = true;
			}
			else
			{
				templateNode = groupAggregateVarComputationTranslator.VisitNode(node);
				flag = false;
			}
			groupAggregateVarInfo = groupAggregateVarComputationTranslator._targetGroupAggregateVarInfo;
			isUnnested = groupAggregateVarComputationTranslator._isUnnested;
			if (groupAggregateVarComputationTranslator._targetGroupAggregateVarInfo == null || templateNode == null)
			{
				return false;
			}
			if (softCastOp != null)
			{
				SoftCastOp softCastOp2;
				if (flag || (!isVarDefinition && AggregatePushdownUtil.IsVarRefOverGivenVar(templateNode, groupAggregateVarComputationTranslator._targetGroupAggregateVarInfo.GroupAggregateVar)))
				{
					softCastOp2 = command.CreateSoftCastOp(TypeHelpers.GetEdmType<CollectionType>(softCastOp.Type).TypeUsage);
				}
				else
				{
					softCastOp2 = softCastOp;
				}
				templateNode = command.CreateNode(softCastOp2, templateNode);
			}
			return true;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0007598B File Offset: 0x00073B8B
		public override Node Visit(VarRefOp op, Node n)
		{
			return this.TranslateOverGroupAggregateVar(op.Var, null);
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0007599C File Offset: 0x00073B9C
		public override Node Visit(PropertyOp op, Node n)
		{
			if (n.Child0.Op.OpType != OpType.VarRef)
			{
				return base.Visit(op, n);
			}
			VarRefOp varRefOp = (VarRefOp)n.Child0.Op;
			return this.TranslateOverGroupAggregateVar(varRefOp.Var, op.PropertyInfo);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000759E8 File Offset: 0x00073BE8
		private Node VisitCollect(Node n)
		{
			Node node = n.Child0;
			Dictionary<Var, Node> dictionary = new Dictionary<Var, Node>();
			while (node.Child0.Op.OpType == OpType.Project)
			{
				node = node.Child0;
				if (this.VisitDefault(node.Child1) == null)
				{
					return null;
				}
				foreach (Node node2 in node.Child1.Children)
				{
					if (GroupAggregateVarComputationTranslator.IsConstant(node2.Child0))
					{
						dictionary.Add(((VarDefOp)node2.Op).Var, node2.Child0);
					}
				}
			}
			if (node.Child0.Op.OpType != OpType.Unnest)
			{
				return null;
			}
			UnnestOp unnestOp = (UnnestOp)node.Child0.Op;
			GroupAggregateVarRefInfo groupAggregateVarRefInfo;
			if (!this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(unnestOp.Var, out groupAggregateVarRefInfo))
			{
				return null;
			}
			if (this._targetGroupAggregateVarInfo == null)
			{
				this._targetGroupAggregateVarInfo = groupAggregateVarRefInfo.GroupAggregateVarInfo;
			}
			else if (this._targetGroupAggregateVarInfo != groupAggregateVarRefInfo.GroupAggregateVarInfo)
			{
				return null;
			}
			if (!this._isUnnested)
			{
				return null;
			}
			PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)n.Child0.Op;
			PlanCompiler.Assert(physicalProjectOp.Outputs.Count == 1, "Physical project should only have one output at this stage");
			Var var = physicalProjectOp.Outputs[0];
			Node node3 = this.TranslateOverGroupAggregateVar(var, null);
			if (node3 != null)
			{
				this._isUnnested = true;
				return node3;
			}
			Node node4;
			if (dictionary.TryGetValue(var, out node4))
			{
				this._isUnnested = true;
				return node4;
			}
			return null;
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x00075B78 File Offset: 0x00073D78
		private static bool IsConstant(Node node)
		{
			Node node2 = node;
			while (node2.Op.OpType == OpType.Cast)
			{
				node2 = node2.Child0;
			}
			return PlanCompilerUtil.IsConstantBaseOp(node2.Op.OpType);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x00075BB0 File Offset: 0x00073DB0
		private Node TranslateOverGroupAggregateVar(Var var, EdmMember property)
		{
			GroupAggregateVarRefInfo groupAggregateVarRefInfo;
			EdmMember edmMember;
			if (this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(var, out groupAggregateVarRefInfo))
			{
				edmMember = property;
			}
			else
			{
				if (!this._groupAggregateVarInfoManager.TryGetReferencedGroupAggregateVarInfo(var, property, out groupAggregateVarRefInfo))
				{
					return null;
				}
				edmMember = null;
			}
			if (this._targetGroupAggregateVarInfo == null)
			{
				this._targetGroupAggregateVarInfo = groupAggregateVarRefInfo.GroupAggregateVarInfo;
				this._isUnnested = groupAggregateVarRefInfo.IsUnnested;
			}
			else if (this._targetGroupAggregateVarInfo != groupAggregateVarRefInfo.GroupAggregateVarInfo || this._isUnnested != groupAggregateVarRefInfo.IsUnnested)
			{
				return null;
			}
			Node node = groupAggregateVarRefInfo.Computation;
			if (edmMember != null)
			{
				node = this._command.CreateNode(this._command.CreatePropertyOp(edmMember), node);
			}
			return node;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x00075C4C File Offset: 0x00073E4C
		protected override Node VisitDefault(Node n)
		{
			List<Node> list = new List<Node>(n.Children.Count);
			bool flag = false;
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = base.VisitNode(n.Children[i]);
				if (node == null)
				{
					return null;
				}
				if (!flag && n.Children[i] != node)
				{
					flag = true;
				}
				list.Add(node);
			}
			if (!flag)
			{
				return n;
			}
			return this._command.CreateNode(n.Op, list);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x00075CCC File Offset: 0x00073ECC
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return null;
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x00075CCF File Offset: 0x00073ECF
		public override Node Visit(AggregateOp op, Node n)
		{
			return null;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x00075CD2 File Offset: 0x00073ED2
		public override Node Visit(CollectOp op, Node n)
		{
			return null;
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x00075CD5 File Offset: 0x00073ED5
		public override Node Visit(ElementOp op, Node n)
		{
			return null;
		}

		// Token: 0x04000DE4 RID: 3556
		private GroupAggregateVarInfo _targetGroupAggregateVarInfo;

		// Token: 0x04000DE5 RID: 3557
		private bool _isUnnested;

		// Token: 0x04000DE6 RID: 3558
		private readonly Command _command;

		// Token: 0x04000DE7 RID: 3559
		private readonly GroupAggregateVarInfoManager _groupAggregateVarInfoManager;
	}
}
