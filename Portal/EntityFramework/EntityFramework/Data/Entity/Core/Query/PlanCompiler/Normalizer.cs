using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000352 RID: 850
	internal class Normalizer : SubqueryTrackingVisitor
	{
		// Token: 0x06002931 RID: 10545 RVA: 0x00083A9A File Offset: 0x00081C9A
		private Normalizer(PlanCompiler planCompilerState)
			: base(planCompilerState)
		{
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00083AA3 File Offset: 0x00081CA3
		internal static void Process(PlanCompiler planCompilerState)
		{
			new Normalizer(planCompilerState).Process();
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00083AB0 File Offset: 0x00081CB0
		private void Process()
		{
			base.m_command.Root = base.VisitNode(base.m_command.Root);
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x00083ACE File Offset: 0x00081CCE
		public override Node Visit(ExistsOp op, Node n)
		{
			this.VisitChildren(n);
			n.Child0 = this.BuildDummyProjectForExists(n.Child0);
			return n;
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x00083AEC File Offset: 0x00081CEC
		private Node BuildDummyProjectForExists(Node child)
		{
			Var var;
			return base.m_command.BuildProject(child, base.m_command.CreateNode(base.m_command.CreateInternalConstantOp(base.m_command.IntegerType, 1)), out var);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x00083B30 File Offset: 0x00081D30
		private Node BuildUnnest(Node collectionNode)
		{
			PlanCompiler.Assert(collectionNode.Op.IsScalarOp, "non-scalar usage of Un-nest?");
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(collectionNode.Op.Type), "non-collection usage for Un-nest?");
			Var var;
			Node node = base.m_command.CreateVarDefNode(collectionNode, out var);
			UnnestOp unnestOp = base.m_command.CreateUnnestOp(var);
			return base.m_command.CreateNode(unnestOp, node);
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x00083B98 File Offset: 0x00081D98
		private Node VisitCollectionFunction(FunctionOp op, Node n)
		{
			PlanCompiler.Assert(TypeSemantics.IsCollectionType(op.Type), "non-TVF function?");
			Node node = this.BuildUnnest(n);
			UnnestOp unnestOp = node.Op as UnnestOp;
			PhysicalProjectOp physicalProjectOp = base.m_command.CreatePhysicalProjectOp(unnestOp.Table.Columns[0]);
			Node node2 = base.m_command.CreateNode(physicalProjectOp, node);
			CollectOp collectOp = base.m_command.CreateCollectOp(n.Op.Type);
			return base.m_command.CreateNode(collectOp, node2);
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x00083C20 File Offset: 0x00081E20
		private Node VisitCollectionAggregateFunction(FunctionOp op, Node n)
		{
			TypeUsage typeUsage = null;
			Node node = n.Child0;
			if (OpType.SoftCast == node.Op.OpType)
			{
				typeUsage = TypeHelpers.GetEdmType<CollectionType>(node.Op.Type).TypeUsage;
				node = node.Child0;
				while (OpType.SoftCast == node.Op.OpType)
				{
					node = node.Child0;
				}
			}
			Node node2 = this.BuildUnnest(node);
			Var var = (node2.Op as UnnestOp).Table.Columns[0];
			AggregateOp aggregateOp = base.m_command.CreateAggregateOp(op.Function, false);
			VarRefOp varRefOp = base.m_command.CreateVarRefOp(var);
			Node node3 = base.m_command.CreateNode(varRefOp);
			if (typeUsage != null)
			{
				node3 = base.m_command.CreateNode(base.m_command.CreateSoftCastOp(typeUsage), node3);
			}
			Node node4 = base.m_command.CreateNode(aggregateOp, node3);
			VarVec varVec = base.m_command.CreateVarVec();
			Node node5 = base.m_command.CreateNode(base.m_command.CreateVarDefListOp());
			VarVec varVec2 = base.m_command.CreateVarVec();
			Var var2;
			Node node6 = base.m_command.CreateVarDefListNode(node4, out var2);
			varVec2.Set(var2);
			GroupByOp groupByOp = base.m_command.CreateGroupByOp(varVec, varVec2);
			Node node7 = base.m_command.CreateNode(groupByOp, node2, node5, node6);
			return base.AddSubqueryToParentRelOp(var2, node7);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x00083D7C File Offset: 0x00081F7C
		public override Node Visit(FunctionOp op, Node n)
		{
			this.VisitScalarOpDefault(op, n);
			Node node;
			if (TypeSemantics.IsCollectionType(op.Type))
			{
				node = this.VisitCollectionFunction(op, n);
			}
			else if (PlanCompilerUtil.IsCollectionAggregateFunction(op, n))
			{
				node = this.VisitCollectionAggregateFunction(op, n);
			}
			else
			{
				node = n;
			}
			PlanCompiler.Assert(node != null, "failure to construct a functionOp?");
			return node;
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x00083DD1 File Offset: 0x00081FD1
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (base.ProcessJoinOp(n))
			{
				n.Child2.Child0 = this.BuildDummyProjectForExists(n.Child2.Child0);
			}
			return n;
		}
	}
}
