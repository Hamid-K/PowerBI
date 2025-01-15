using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000087 RID: 135
	internal sealed class QueryExtensionSchemaTranslator : QueryExtensionSchemaTranslatorBase
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00017707 File Offset: 0x00015907
		private QueryExtensionSchemaTranslator(QueryBuilder queryBuilder, FederatedEntityDataModel model, IFederatedConceptualSchema schema, ExpressionTable expressionTable, bool useConceptualSchema)
			: base(model, schema, useConceptualSchema)
		{
			this.m_queryBuilder = queryBuilder;
			this.m_expressionTable = expressionTable;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00017722 File Offset: 0x00015922
		internal static void Translate(ExtensionSchema extensionSchema, QueryBuilder queryBuilder, FederatedEntityDataModel model, IFederatedConceptualSchema schema, ExpressionTable expressionTable, bool useConceptualSchema)
		{
			if (extensionSchema == null)
			{
				return;
			}
			new QueryExtensionSchemaTranslator(queryBuilder, model, schema, expressionTable, useConceptualSchema).Translate(extensionSchema);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001773A File Offset: 0x0001593A
		protected override void DeclareMeasure(QueryExpression measureContentExpr, EntitySet targetEntitySet, EdmMeasure edmMeasure, IConceptualEntity targetEntity, IConceptualMeasure measure)
		{
			this.m_queryBuilder.DeclareMeasure(measureContentExpr, targetEntitySet, edmMeasure, targetEntity, measure);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001774F File Offset: 0x0001594F
		protected override void DeclareField(QueryExpression fieldContentExpr, EntitySet targetEntitySet, EdmField field, IConceptualEntity targetEntity, IConceptualColumn column)
		{
			this.m_queryBuilder.DeclareField(fieldContentExpr, targetEntitySet, field, targetEntity, column);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00017764 File Offset: 0x00015964
		protected override QueryExpression TranslatePropertyWithExpression(ExtensionProperty property, EntitySet targetEntitySet, IConceptualEntity targetEntity)
		{
			DaxTextExpressionNode daxTextExpressionNode = this.m_expressionTable.GetNode(property.Expression) as DaxTextExpressionNode;
			return QueryExpressionBuilder.DaxText(property.GetConceptualResultType(), daxTextExpressionNode.Text);
		}

		// Token: 0x04000324 RID: 804
		private readonly QueryBuilder m_queryBuilder;

		// Token: 0x04000325 RID: 805
		private readonly ExpressionTable m_expressionTable;
	}
}
