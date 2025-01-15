using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200002A RID: 42
	internal sealed class RelatedResolvedPropertyExpressionNode : ExpressionNode
	{
		// Token: 0x06000226 RID: 550 RVA: 0x00006FFD File Offset: 0x000051FD
		internal RelatedResolvedPropertyExpressionNode(IConceptualProperty property)
		{
			this.m_property = property;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000700C File Offset: 0x0000520C
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.RelatedResolvedProperty;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00007010 File Offset: 0x00005210
		public string Container
		{
			get
			{
				return this.m_property.Entity.EntityContainerName;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00007022 File Offset: 0x00005222
		public IConceptualProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000702C File Offset: 0x0000522C
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			RelatedResolvedPropertyExpressionNode relatedResolvedPropertyExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<RelatedResolvedPropertyExpressionNode>(this, other, out flag, out relatedResolvedPropertyExpressionNode))
			{
				return flag;
			}
			return this.Container == relatedResolvedPropertyExpressionNode.Container && object.Equals(this.Property, relatedResolvedPropertyExpressionNode.Property);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000706E File Offset: 0x0000526E
		protected override int GetHashCodeImpl()
		{
			return Microsoft.DataShaping.Common.Hashing.CombineHash(this.Container.GetHashCode(), Microsoft.DataShaping.Common.Hashing.GetHashCode<IConceptualProperty>(this.Property, null));
		}

		// Token: 0x0400009B RID: 155
		private readonly IConceptualProperty m_property;
	}
}
