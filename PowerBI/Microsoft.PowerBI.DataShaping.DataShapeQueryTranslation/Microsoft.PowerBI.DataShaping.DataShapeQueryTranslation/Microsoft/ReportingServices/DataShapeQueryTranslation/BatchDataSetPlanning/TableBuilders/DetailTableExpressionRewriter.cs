using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001C6 RID: 454
	internal sealed class DetailTableExpressionRewriter : ExpressionNodeTreeTransform
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x000414AF File Offset: 0x0003F6AF
		internal DetailTableExpressionRewriter(IConceptualEntity detailIdentityEntity, IReadOnlyList<IConceptualEntity> relatedEntitiesRelatedToOne, IFederatedConceptualSchema federatedSchema)
			: base(true)
		{
			this.m_detailIdentityEntity = detailIdentityEntity;
			this.m_relatedEntitiesRelatedToOne = relatedEntitiesRelatedToOne;
			this.m_federatedConceptualSchema = federatedSchema;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000414CD File Offset: 0x0003F6CD
		public ExpressionNode Rewrite(ExpressionNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000414D6 File Offset: 0x0003F6D6
		public override ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			if (node.Property.Entity != this.m_detailIdentityEntity)
			{
				return this.RewriteRelatedColumn(node);
			}
			return base.Visit(node);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x000414FC File Offset: 0x0003F6FC
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			if (base.ParentNodes.Count != 1)
			{
				return node;
			}
			ExpressionNode expressionNode = this.RewriteProjection(node);
			if (expressionNode == node)
			{
				return expressionNode;
			}
			return this.Visit(expressionNode);
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00041530 File Offset: 0x0003F730
		private ExpressionNode RewriteProjection(ExpressionNode expressionNode)
		{
			FunctionCallExpressionNode functionCallExpressionNode = expressionNode as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && !functionCallExpressionNode.Arguments.IsNullOrEmpty<ExpressionNode>())
			{
				ResolvedPropertyExpressionNode resolvedPropertyExpressionNode = functionCallExpressionNode.Arguments[0] as ResolvedPropertyExpressionNode;
				if (resolvedPropertyExpressionNode != null && resolvedPropertyExpressionNode.Property.IsFromEntity(this.m_detailIdentityEntity))
				{
					return AggregateExpressionFlattener.Rewrite(functionCallExpressionNode);
				}
			}
			return expressionNode;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00041584 File Offset: 0x0003F784
		private ExpressionNode RewriteRelatedColumn(ResolvedPropertyExpressionNode propertyRef)
		{
			if (this.m_relatedEntitiesRelatedToOne.Contains(propertyRef.Property.Entity))
			{
				return new RelatedResolvedPropertyExpressionNode(propertyRef.Property);
			}
			return propertyRef;
		}

		// Token: 0x0400077C RID: 1916
		private readonly IConceptualEntity m_detailIdentityEntity;

		// Token: 0x0400077D RID: 1917
		private readonly IReadOnlyList<IConceptualEntity> m_relatedEntitiesRelatedToOne;

		// Token: 0x0400077E RID: 1918
		private readonly IFederatedConceptualSchema m_federatedConceptualSchema;
	}
}
