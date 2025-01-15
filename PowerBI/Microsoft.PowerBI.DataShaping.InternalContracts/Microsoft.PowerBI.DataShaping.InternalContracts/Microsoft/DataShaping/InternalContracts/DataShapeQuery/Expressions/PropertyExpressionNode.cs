using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000DA RID: 218
	internal sealed class PropertyExpressionNode : ExpressionNode
	{
		// Token: 0x06000611 RID: 1553 RVA: 0x0000D082 File Offset: 0x0000B282
		internal PropertyExpressionNode(EntitySetExpressionNode entitySet, string name, IConceptualProperty property = null)
		{
			this.EntitySet = entitySet;
			this.Name = name;
			this.Property = property;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0000D09F File Offset: 0x0000B29F
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.Property;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000D0A3 File Offset: 0x0000B2A3
		public EntitySetExpressionNode EntitySet { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000D0AB File Offset: 0x0000B2AB
		public string Name { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000D0B3 File Offset: 0x0000B2B3
		public IConceptualProperty Property { get; }

		// Token: 0x06000616 RID: 1558 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			PropertyExpressionNode propertyExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<PropertyExpressionNode>(this, other, out flag, out propertyExpressionNode))
			{
				return flag;
			}
			return this.EntitySet.Equals(propertyExpressionNode.EntitySet) && this.Name == propertyExpressionNode.Name && object.Equals(this.Property, propertyExpressionNode.Property);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000D111 File Offset: 0x0000B311
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.EntitySet.GetHashCode(), this.Name.GetHashCode(), Hashing.GetHashCode<IConceptualProperty>(this.Property, null));
		}
	}
}
