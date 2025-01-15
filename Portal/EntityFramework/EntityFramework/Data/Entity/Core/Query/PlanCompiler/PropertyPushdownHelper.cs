using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200035E RID: 862
	internal class PropertyPushdownHelper : BasicOpVisitor
	{
		// Token: 0x060029E4 RID: 10724 RVA: 0x0008871E File Offset: 0x0008691E
		private PropertyPushdownHelper()
		{
			this.m_varPropertyRefMap = new Dictionary<Var, PropertyRefList>();
			this.m_nodePropertyRefMap = new Dictionary<Node, PropertyRefList>();
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x0008873C File Offset: 0x0008693C
		internal static void Process(Command itree, out Dictionary<Var, PropertyRefList> varPropertyRefs, out Dictionary<Node, PropertyRefList> nodePropertyRefs)
		{
			PropertyPushdownHelper propertyPushdownHelper = new PropertyPushdownHelper();
			propertyPushdownHelper.Process(itree.Root);
			varPropertyRefs = propertyPushdownHelper.m_varPropertyRefMap;
			nodePropertyRefs = propertyPushdownHelper.m_nodePropertyRefMap;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x0008876B File Offset: 0x0008696B
		private void Process(Node rootNode)
		{
			rootNode.Op.Accept(this, rootNode);
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0008877C File Offset: 0x0008697C
		private PropertyRefList GetPropertyRefList(Node node)
		{
			PropertyRefList propertyRefList;
			if (!this.m_nodePropertyRefMap.TryGetValue(node, out propertyRefList))
			{
				propertyRefList = new PropertyRefList();
				this.m_nodePropertyRefMap[node] = propertyRefList;
			}
			return propertyRefList;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000887AD File Offset: 0x000869AD
		private void AddPropertyRefs(Node node, PropertyRefList propertyRefs)
		{
			this.GetPropertyRefList(node).Append(propertyRefs);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000887BC File Offset: 0x000869BC
		private PropertyRefList GetPropertyRefList(Var v)
		{
			PropertyRefList propertyRefList;
			if (!this.m_varPropertyRefMap.TryGetValue(v, out propertyRefList))
			{
				propertyRefList = new PropertyRefList();
				this.m_varPropertyRefMap[v] = propertyRefList;
			}
			return propertyRefList;
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000887ED File Offset: 0x000869ED
		private void AddPropertyRefs(Var v, PropertyRefList propertyRefs)
		{
			this.GetPropertyRefList(v).Append(propertyRefs);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000887FC File Offset: 0x000869FC
		private static PropertyRefList GetIdentityProperties(EntityType type)
		{
			PropertyRefList keyProperties = PropertyPushdownHelper.GetKeyProperties(type);
			keyProperties.Add(EntitySetIdPropertyRef.Instance);
			return keyProperties;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00088810 File Offset: 0x00086A10
		private static PropertyRefList GetKeyProperties(EntityType entityType)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			foreach (EdmMember edmMember in entityType.KeyMembers)
			{
				EdmProperty edmProperty = edmMember as EdmProperty;
				PlanCompiler.Assert(edmProperty != null, "EntityType had non-EdmProperty key member?");
				SimplePropertyRef simplePropertyRef = new SimplePropertyRef(edmProperty);
				propertyRefList.Add(simplePropertyRef);
			}
			return propertyRefList;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00088884 File Offset: 0x00086A84
		protected override void VisitDefault(Node n)
		{
			foreach (Node node in n.Children)
			{
				ScalarOp scalarOp = node.Op as ScalarOp;
				if (scalarOp != null && TypeUtils.IsStructuredType(scalarOp.Type))
				{
					this.AddPropertyRefs(node, PropertyRefList.All);
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00088900 File Offset: 0x00086B00
		public override void Visit(SoftCastOp op, Node n)
		{
			PropertyRefList propertyRefList = null;
			if (TypeSemantics.IsReferenceType(op.Type))
			{
				propertyRefList = PropertyRefList.All;
			}
			else if (TypeSemantics.IsNominalType(op.Type))
			{
				propertyRefList = this.m_nodePropertyRefMap[n].Clone();
			}
			else if (TypeSemantics.IsRowType(op.Type))
			{
				propertyRefList = PropertyRefList.All;
			}
			if (propertyRefList != null)
			{
				this.AddPropertyRefs(n.Child0, propertyRefList);
			}
			this.VisitChildren(n);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00088970 File Offset: 0x00086B70
		public override void Visit(CaseOp op, Node n)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(n);
			for (int i = 1; i < n.Children.Count - 1; i += 2)
			{
				PropertyRefList propertyRefList2 = propertyRefList.Clone();
				this.AddPropertyRefs(n.Children[i], propertyRefList2);
			}
			this.AddPropertyRefs(n.Children[n.Children.Count - 1], propertyRefList.Clone());
			this.VisitChildren(n);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000889E2 File Offset: 0x00086BE2
		public override void Visit(CollectOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000889EC File Offset: 0x00086BEC
		public override void Visit(ComparisonOp op, Node n)
		{
			TypeUsage type = (n.Child0.Op as ScalarOp).Type;
			if (!TypeUtils.IsStructuredType(type))
			{
				this.VisitChildren(n);
				return;
			}
			if (TypeSemantics.IsRowType(type) || TypeSemantics.IsReferenceType(type))
			{
				this.VisitDefault(n);
				return;
			}
			PlanCompiler.Assert(TypeSemantics.IsEntityType(type), "unexpected childOpType?");
			PropertyRefList identityProperties = PropertyPushdownHelper.GetIdentityProperties(TypeHelpers.GetEdmType<EntityType>(type));
			foreach (Node node in n.Children)
			{
				this.AddPropertyRefs(node, identityProperties);
			}
			this.VisitChildren(n);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00088AA4 File Offset: 0x00086CA4
		public override void Visit(ElementOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00088AAC File Offset: 0x00086CAC
		public override void Visit(GetEntityRefOp op, Node n)
		{
			ScalarOp scalarOp = n.Child0.Op as ScalarOp;
			PlanCompiler.Assert(scalarOp != null, "input to GetEntityRefOp is not a ScalarOp?");
			PropertyRefList identityProperties = PropertyPushdownHelper.GetIdentityProperties(TypeHelpers.GetEdmType<EntityType>(scalarOp.Type));
			this.AddPropertyRefs(n.Child0, identityProperties);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00088B00 File Offset: 0x00086D00
		public override void Visit(IsOfOp op, Node n)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			propertyRefList.Add(TypeIdPropertyRef.Instance);
			this.AddPropertyRefs(n.Child0, propertyRefList);
			this.VisitChildren(n);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00088B34 File Offset: 0x00086D34
		private void VisitPropertyOp(Op op, Node n, PropertyRef propertyRef)
		{
			PropertyRefList propertyRefList = new PropertyRefList();
			if (!TypeUtils.IsStructuredType(op.Type))
			{
				propertyRefList.Add(propertyRef);
			}
			else
			{
				PropertyRefList propertyRefList2 = this.GetPropertyRefList(n);
				if (propertyRefList2.AllProperties)
				{
					propertyRefList = propertyRefList2;
				}
				else
				{
					foreach (PropertyRef propertyRef2 in propertyRefList2.Properties)
					{
						propertyRefList.Add(propertyRef2.CreateNestedPropertyRef(propertyRef));
					}
				}
			}
			this.AddPropertyRefs(n.Child0, propertyRefList);
			this.VisitChildren(n);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x00088BCC File Offset: 0x00086DCC
		public override void Visit(RelPropertyOp op, Node n)
		{
			this.VisitPropertyOp(op, n, new RelPropertyRef(op.PropertyInfo));
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00088BE1 File Offset: 0x00086DE1
		public override void Visit(PropertyOp op, Node n)
		{
			this.VisitPropertyOp(op, n, new SimplePropertyRef(op.PropertyInfo));
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x00088BF8 File Offset: 0x00086DF8
		public override void Visit(TreatOp op, Node n)
		{
			PropertyRefList propertyRefList = this.GetPropertyRefList(n).Clone();
			propertyRefList.Add(TypeIdPropertyRef.Instance);
			this.AddPropertyRefs(n.Child0, propertyRefList);
			this.VisitChildren(n);
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x00088C34 File Offset: 0x00086E34
		public override void Visit(VarRefOp op, Node n)
		{
			if (TypeUtils.IsStructuredType(op.Var.Type))
			{
				PropertyRefList propertyRefList = this.GetPropertyRefList(n);
				this.AddPropertyRefs(op.Var, propertyRefList);
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x00088C68 File Offset: 0x00086E68
		public override void Visit(VarDefOp op, Node n)
		{
			if (TypeUtils.IsStructuredType(op.Var.Type))
			{
				PropertyRefList propertyRefList = this.GetPropertyRefList(op.Var);
				this.AddPropertyRefs(n.Child0, propertyRefList);
			}
			this.VisitChildren(n);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x00088CA8 File Offset: 0x00086EA8
		public override void Visit(VarDefListOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x00088CB1 File Offset: 0x00086EB1
		protected override void VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x00088CCC File Offset: 0x00086ECC
		public override void Visit(DistinctOp op, Node n)
		{
			foreach (Var var in op.Keys)
			{
				if (TypeUtils.IsStructuredType(var.Type))
				{
					this.AddPropertyRefs(var, PropertyRefList.All);
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x00088D34 File Offset: 0x00086F34
		public override void Visit(FilterOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x00088D50 File Offset: 0x00086F50
		protected override void VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			foreach (Var var in op.Keys)
			{
				if (TypeUtils.IsStructuredType(var.Type))
				{
					this.AddPropertyRefs(var, PropertyRefList.All);
				}
			}
			this.VisitChildrenReverse(n);
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x00088DB8 File Offset: 0x00086FB8
		protected override void VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (n.Op.OpType == OpType.CrossJoin)
			{
				this.VisitChildren(n);
				return;
			}
			this.VisitNode(n.Child2);
			this.VisitNode(n.Child0);
			this.VisitNode(n.Child1);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x00088DF5 File Offset: 0x00086FF5
		public override void Visit(ProjectOp op, Node n)
		{
			this.VisitNode(n.Child1);
			this.VisitNode(n.Child0);
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00088E0F File Offset: 0x0008700F
		public override void Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(!n.HasChild0, "scanTableOp with an input?");
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00088E24 File Offset: 0x00087024
		public override void Visit(ScanViewOp op, Node n)
		{
			PlanCompiler.Assert(op.Table.Columns.Count == 1, "ScanViewOp with multiple columns?");
			Var var = op.Table.Columns[0];
			PropertyRefList propertyRefList = this.GetPropertyRefList(var);
			Var singletonVar = NominalTypeEliminator.GetSingletonVar(n.Child0);
			PlanCompiler.Assert(singletonVar != null, "cannot determine single Var from ScanViewOp's input");
			this.AddPropertyRefs(singletonVar, propertyRefList.Clone());
			this.VisitChildren(n);
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00088E98 File Offset: 0x00087098
		protected override void VisitSetOp(SetOp op, Node n)
		{
			VarMap[] varMap = op.VarMap;
			for (int i = 0; i < varMap.Length; i++)
			{
				foreach (KeyValuePair<Var, Var> keyValuePair in varMap[i])
				{
					if (TypeUtils.IsStructuredType(keyValuePair.Key.Type))
					{
						PropertyRefList propertyRefList = this.GetPropertyRefList(keyValuePair.Key);
						if (op.OpType == OpType.Intersect || op.OpType == OpType.Except)
						{
							propertyRefList = PropertyRefList.All;
							this.AddPropertyRefs(keyValuePair.Key, propertyRefList);
						}
						else
						{
							propertyRefList = propertyRefList.Clone();
						}
						this.AddPropertyRefs(keyValuePair.Value, propertyRefList);
					}
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x00088F6C File Offset: 0x0008716C
		protected override void VisitSortOp(SortBaseOp op, Node n)
		{
			foreach (SortKey sortKey in op.Keys)
			{
				if (TypeUtils.IsStructuredType(sortKey.Var.Type))
				{
					this.AddPropertyRefs(sortKey.Var, PropertyRefList.All);
				}
			}
			if (n.HasChild1)
			{
				this.VisitNode(n.Child1);
			}
			this.VisitNode(n.Child0);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00088FFC File Offset: 0x000871FC
		public override void Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00089008 File Offset: 0x00087208
		public override void Visit(PhysicalProjectOp op, Node n)
		{
			foreach (Var var in op.Outputs)
			{
				if (TypeUtils.IsStructuredType(var.Type))
				{
					this.AddPropertyRefs(var, PropertyRefList.All);
				}
			}
			this.VisitChildren(n);
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x00089074 File Offset: 0x00087274
		public override void Visit(MultiStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0008907B File Offset: 0x0008727B
		public override void Visit(SingleStreamNestOp op, Node n)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000E6B RID: 3691
		private readonly Dictionary<Node, PropertyRefList> m_nodePropertyRefMap;

		// Token: 0x04000E6C RID: 3692
		private readonly Dictionary<Var, PropertyRefList> m_varPropertyRefMap;
	}
}
