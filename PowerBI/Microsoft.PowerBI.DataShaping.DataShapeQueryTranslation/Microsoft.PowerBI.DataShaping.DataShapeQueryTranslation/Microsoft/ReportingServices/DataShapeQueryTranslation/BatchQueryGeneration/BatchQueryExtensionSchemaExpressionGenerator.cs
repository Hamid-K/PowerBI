using System;
using System.Threading;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x0200013A RID: 314
	internal sealed class BatchQueryExtensionSchemaExpressionGenerator : IBatchQueryExtensionSchemaExpressionGenerator
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002EB98 File Offset: 0x0002CD98
		internal BatchQueryExtensionSchemaExpressionGenerator(ExpressionTable expressionTable, TranslationErrorContext errorContext, DaxCapabilities daxCapabilities, bool suppressModelGrouping, BatchQueryBuilder batchQueryBuilder, FederatedEntityDataModel model, IConceptualSchema schema, CancellationToken cancellationToken, IFeatureSwitchProvider featureSwitchProvider)
		{
			this.m_expressionTable = expressionTable;
			this.m_errorContext = errorContext;
			this.m_daxCapabilities = daxCapabilities;
			this.m_suppressModelGrouping = suppressModelGrouping;
			this.m_queryBuilder = batchQueryBuilder;
			this.m_model = model;
			this.m_schema = schema;
			this.m_cancellationToken = cancellationToken;
			this.m_featureSwitchProvider = featureSwitchProvider;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002EBF0 File Offset: 0x0002CDF0
		public QueryExpressionContext Generate(ExtensionProperty property, EntitySet targetEntitySet, IConceptualEntity targetEntity)
		{
			ExpressionNode node = this.m_expressionTable.GetNode(property.Expression);
			DaxTextExpressionNode daxTextExpressionNode = node as DaxTextExpressionNode;
			if (daxTextExpressionNode != null)
			{
				return this.GenerateDaxText(property, daxTextExpressionNode);
			}
			return this.GenerateGeneralExpression(node, property, targetEntitySet, targetEntity);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002EC2C File Offset: 0x0002CE2C
		private QueryExpressionContext GenerateGeneralExpression(ExpressionNode node, ExtensionProperty property, EntitySet targetEntitySet, IConceptualEntity targetEntity)
		{
			ExpressionContext expressionContext = new ExpressionContext(this.m_errorContext, (property is ExtensionColumn) ? ObjectType.ExtensionColumn : ObjectType.ExtensionMeasure, property.Name, "Expression");
			QueryExpressionContext queryExpressionContext = BatchQueryExtensionSchemaExpressionTranslator.Translate(node, expressionContext, this.m_suppressModelGrouping, this.m_daxCapabilities, this.m_queryBuilder, this.m_model, this.m_schema, this.m_cancellationToken, this.m_featureSwitchProvider);
			QueryExpression queryExpression = queryExpressionContext.QueryExpression;
			if (property is ExtensionColumn)
			{
				queryExpression = queryExpressionContext.QueryExpression.RewriteEntityPlaceholdersToScalarEntityReferences(targetEntitySet, targetEntity);
			}
			return new QueryExpressionContext(queryExpression, queryExpressionContext.QueryExpressionFeatures, queryExpressionContext.CalculateInMeasureContext);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
		private QueryExpressionContext GenerateDaxText(ExtensionProperty property, DaxTextExpressionNode daxTextNode)
		{
			string text = daxTextNode.Text;
			return new QueryExpressionContext(QueryExpressionBuilder.DaxText(property.GetConceptualResultType(), text), QueryExpressionFeatures.None, false);
		}

		// Token: 0x040005DE RID: 1502
		private readonly ExpressionTable m_expressionTable;

		// Token: 0x040005DF RID: 1503
		private readonly TranslationErrorContext m_errorContext;

		// Token: 0x040005E0 RID: 1504
		private readonly DaxCapabilities m_daxCapabilities;

		// Token: 0x040005E1 RID: 1505
		private readonly bool m_suppressModelGrouping;

		// Token: 0x040005E2 RID: 1506
		private readonly BatchQueryBuilder m_queryBuilder;

		// Token: 0x040005E3 RID: 1507
		private readonly FederatedEntityDataModel m_model;

		// Token: 0x040005E4 RID: 1508
		private readonly IConceptualSchema m_schema;

		// Token: 0x040005E5 RID: 1509
		private readonly CancellationToken m_cancellationToken;

		// Token: 0x040005E6 RID: 1510
		private readonly IFeatureSwitchProvider m_featureSwitchProvider;
	}
}
