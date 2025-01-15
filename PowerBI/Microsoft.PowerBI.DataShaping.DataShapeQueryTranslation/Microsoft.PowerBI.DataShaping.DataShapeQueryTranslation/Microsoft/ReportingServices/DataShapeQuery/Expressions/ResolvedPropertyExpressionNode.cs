using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000032 RID: 50
	internal sealed class ResolvedPropertyExpressionNode : ExpressionNode
	{
		// Token: 0x06000255 RID: 597 RVA: 0x0000736F File Offset: 0x0000556F
		internal ResolvedPropertyExpressionNode(IConceptualProperty property)
		{
			this.m_property = property;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000737E File Offset: 0x0000557E
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedProperty;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007382 File Offset: 0x00005582
		public string Container
		{
			get
			{
				return this.m_property.Entity.EntityContainerName;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00007394 File Offset: 0x00005594
		public IConceptualProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000739C File Offset: 0x0000559C
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedPropertyExpressionNode resolvedPropertyExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedPropertyExpressionNode>(this, other, out flag, out resolvedPropertyExpressionNode))
			{
				return flag;
			}
			return object.Equals(this.Property, resolvedPropertyExpressionNode.Property);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000073C9 File Offset: 0x000055C9
		protected override int GetHashCodeImpl()
		{
			return Microsoft.DataShaping.Common.Hashing.GetHashCode<IConceptualProperty>(this.Property, null);
		}

		// Token: 0x040000A4 RID: 164
		private readonly IConceptualProperty m_property;
	}
}
