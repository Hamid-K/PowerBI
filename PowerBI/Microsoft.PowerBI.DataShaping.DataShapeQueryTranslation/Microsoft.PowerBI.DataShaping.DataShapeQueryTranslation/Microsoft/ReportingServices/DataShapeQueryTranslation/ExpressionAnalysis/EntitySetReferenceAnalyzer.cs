using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000AC RID: 172
	internal sealed class EntitySetReferenceAnalyzer : ExpressionNodeTreeTransform
	{
		// Token: 0x060007A3 RID: 1955 RVA: 0x0001D8C1 File Offset: 0x0001BAC1
		private EntitySetReferenceAnalyzer()
			: base(false)
		{
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001D8CC File Offset: 0x0001BACC
		public static IReadOnlyList<IConceptualEntity> Analyze(ExpressionNode node)
		{
			EntitySetReferenceAnalyzer entitySetReferenceAnalyzer = new EntitySetReferenceAnalyzer();
			entitySetReferenceAnalyzer.Visit(node);
			if (entitySetReferenceAnalyzer.m_referencedEntities != null)
			{
				return entitySetReferenceAnalyzer.m_referencedEntities.AsReadOnly();
			}
			return null;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		public override ExpressionNode Visit(ResolvedEntitySetExpressionNode node)
		{
			this.RecordEntity(node.Entity);
			return base.Visit(node);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001D911 File Offset: 0x0001BB11
		public override ExpressionNode Visit(ResolvedPropertyExpressionNode node)
		{
			this.RecordEntity(node.Property.Entity);
			return base.Visit(node);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001D92B File Offset: 0x0001BB2B
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			if (node.Descriptor.Name == "Scope")
			{
				return node;
			}
			return base.Visit(node);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001D94D File Offset: 0x0001BB4D
		private void RecordEntity(IConceptualEntity entity)
		{
			if (this.m_referencedEntities == null)
			{
				this.m_referencedEntities = new List<IConceptualEntity>();
			}
			if (!this.m_referencedEntities.Contains(entity, ConceptualEntityExtensionAwareEqualityComparer.Instance))
			{
				this.m_referencedEntities.Add(entity);
			}
		}

		// Token: 0x040003CF RID: 975
		private List<IConceptualEntity> m_referencedEntities;
	}
}
