using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C8 RID: 200
	internal sealed class EntitySetExpressionNode : ExpressionNode
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000ACD8 File Offset: 0x00008ED8
		internal EntitySetExpressionNode(string container, string name, IConceptualEntity entity = null)
		{
			this.Container = container;
			this.Name = name;
			this.Entity = entity;
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000ACF5 File Offset: 0x00008EF5
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.EntitySet;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		public string Container { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000AD01 File Offset: 0x00008F01
		public string Name { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000AD09 File Offset: 0x00008F09
		public IConceptualEntity Entity { get; }

		// Token: 0x0600052A RID: 1322 RVA: 0x0000AD14 File Offset: 0x00008F14
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			EntitySetExpressionNode entitySetExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<EntitySetExpressionNode>(this, other, out flag, out entitySetExpressionNode))
			{
				return flag;
			}
			return this.Container == entitySetExpressionNode.Container && this.Name == entitySetExpressionNode.Name && object.Equals(this.Entity, entitySetExpressionNode.Entity);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000AD69 File Offset: 0x00008F69
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Container.GetHashCode(), this.Name.GetHashCode(), Hashing.GetHashCode<IConceptualEntity>(this.Entity, null));
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000AD92 File Offset: 0x00008F92
		public string GetFullName()
		{
			return this.Container + "." + this.Name;
		}
	}
}
