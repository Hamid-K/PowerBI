using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000136 RID: 310
	internal sealed class BatchQueryExpressionGenerator : QueryExpressionGeneratorBase
	{
		// Token: 0x06000B8E RID: 2958 RVA: 0x0002E4B0 File Offset: 0x0002C6B0
		internal BatchQueryExpressionGenerator(BatchQueryGenerationContext context, WritableExpressionTable outputExpressionTable, BatchQueryTableGenerator tableGenerator, BatchQueryExpressionReferenceContext referenceContext, GeneratedDeclarationCollection declarations, GeneratedQueryParameterMap queryParameterMap)
			: base(context, outputExpressionTable, queryParameterMap)
		{
			this.m_batchContext = context;
			this.m_tableGenerator = tableGenerator;
			this.m_referenceContext = referenceContext;
			this.m_declarations = declarations;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002E4DC File Offset: 0x0002C6DC
		public override List<KeyValuePair<ExpressionId, QueryExpressionContext>> TranslateCalculation(Calculation calculation)
		{
			List<KeyValuePair<ExpressionId, QueryExpressionContext>> list = new List<KeyValuePair<ExpressionId, QueryExpressionContext>>();
			ReadOnlyCollection<ExpressionId> readOnlyCollection;
			if (this.m_batchContext.CalculationExpressionMapping.TryGetExpressions(calculation, out readOnlyCollection))
			{
				ExpressionContext expressionContext = base.CreateExpressionContext(calculation);
				using (IEnumerator<ExpressionId> enumerator = readOnlyCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ExpressionId exprId = enumerator.Current;
						QueryExpressionContext queryExpressionContext = base.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(exprId, expressionContext, null));
						list.Add(Microsoft.DataShaping.Util.ToKeyValuePair<ExpressionId, QueryExpressionContext>(exprId, queryExpressionContext));
					}
				}
				if (this.m_outputExpressionTable.GetNodeOrDefault(calculation.Value.ExpressionId.Value) == null)
				{
					ExpressionNode node = this.m_context.ExpressionTable.GetNode(calculation.Value.ExpressionId.Value);
					this.m_outputExpressionTable.SetNode(calculation.Value.ExpressionId.Value, node);
				}
			}
			return list;
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002E608 File Offset: 0x0002C808
		public override QueryExpressionContext TranslateExpression(ExpressionId expressionId, ExpressionContext expressionContext)
		{
			QueryTableColumn queryTableColumn;
			if (this.m_referenceContext.TryGetAvailableColumn(expressionId, out queryTableColumn))
			{
				return new QueryExpressionContext(queryTableColumn.QdmReference(), QueryExpressionFeatures.None, false);
			}
			return base.TranslateExpression(expressionId, expressionContext);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002E63C File Offset: 0x0002C83C
		protected override QueryExpressionContext TranslateNode(ExpressionContext expressionContext, ExpressionNode expressionNode)
		{
			return BatchQueryExpressionTranslator.Translate(expressionContext, this, expressionNode, this.m_tableGenerator, this.m_referenceContext, this.m_declarations, this.m_queryParameterMap, this.m_batchContext.Model, this.m_batchContext.Schema.GetDefaultSchema(), this.m_batchContext.DaxCapabilities, this.m_batchContext.FeatureSwitchProvider, this.m_comparer, this.m_batchContext.SuppressModelGrouping, this.m_batchContext.CancellationToken);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002E6B8 File Offset: 0x0002C8B8
		public override QueryExpressionContext TranslateFilterExpression(ExpressionId expressionId, ExpressionContext expressionContext)
		{
			QueryTableColumn queryTableColumn;
			if (this.m_referenceContext.TryGetAvailableColumn(expressionId, out queryTableColumn))
			{
				return new QueryExpressionContext(queryTableColumn.QdmReference(), QueryExpressionFeatures.None, false);
			}
			return base.TranslateExpressionInternal<QueryExpressionContext>(expressionContext, () => this.TranslateSingleExpressionImpl(expressionId, expressionContext, null));
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002E71C File Offset: 0x0002C91C
		public override QueryExpressionContext TranslateCalculationReference(Calculation calculation)
		{
			QueryTableColumn queryTableColumn;
			if (this.m_referenceContext.TryGetAvailableColumn(calculation.Value.ExpressionId.Value, out queryTableColumn))
			{
				return new QueryExpressionContext(queryTableColumn.QdmReference(), QueryExpressionFeatures.None, false);
			}
			return base.TranslateCalculationReference(calculation);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0002E760 File Offset: 0x0002C960
		public override QueryExpressionContext TranslateGroupKeyReference(GroupKey groupKey)
		{
			QueryTableColumn queryTableColumn;
			if (this.m_referenceContext.TryGetAvailableColumn(groupKey.Value.ExpressionId.Value, out queryTableColumn))
			{
				return new QueryExpressionContext(queryTableColumn.QdmReference(), QueryExpressionFeatures.None, false);
			}
			return base.TranslateGroupKeyReference(groupKey);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		public override QueryExpressionContext TranslateSortKeyReference(SortKey sortKey)
		{
			QueryTableColumn queryTableColumn;
			if (this.m_referenceContext.TryGetAvailableColumn(sortKey.Value.ExpressionId.Value, out queryTableColumn))
			{
				return new QueryExpressionContext(queryTableColumn.QdmReference(), QueryExpressionFeatures.None, false);
			}
			return base.TranslateSortKeyReference(sortKey);
		}

		// Token: 0x040005D6 RID: 1494
		private readonly BatchQueryGenerationContext m_batchContext;

		// Token: 0x040005D7 RID: 1495
		private readonly BatchQueryTableGenerator m_tableGenerator;

		// Token: 0x040005D8 RID: 1496
		private readonly BatchQueryExpressionReferenceContext m_referenceContext;

		// Token: 0x040005D9 RID: 1497
		private readonly GeneratedDeclarationCollection m_declarations;
	}
}
