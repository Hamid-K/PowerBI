using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExtensionEdm;
using Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchQueryGeneration
{
	// Token: 0x02000138 RID: 312
	internal sealed class BatchQueryExpressionTranslator : QueryExpressionTranslatorBase
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002E880 File Offset: 0x0002CA80
		private BatchQueryExpressionTranslator(ExpressionContext expressionContext, IQueryExpressionGenerator expressionGenerator, BatchQueryTableGenerator tableGenerator, BatchQueryExpressionReferenceContext referenceContext, GeneratedDeclarationCollection declarations, GeneratedQueryParameterMap queryParameterMap, FederatedEntityDataModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, bool suppressModelGrouping, CancellationToken cancellationToken)
			: base(expressionContext, expressionGenerator, queryParameterMap, model, schema, daxCapabilities, featureSwitchProvider, comparer, suppressModelGrouping, cancellationToken)
		{
			this.m_tableGenerator = tableGenerator;
			this.m_referenceContext = referenceContext;
			this.m_declarations = declarations;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002E8BC File Offset: 0x0002CABC
		public static QueryExpressionContext Translate(ExpressionContext expressionContext, IQueryExpressionGenerator expressionGenerator, ExpressionNode expressionNode, BatchQueryTableGenerator tableGenerator, BatchQueryExpressionReferenceContext referenceContext, GeneratedDeclarationCollection declarations, GeneratedQueryParameterMap queryParameterMap, FederatedEntityDataModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, bool suppressModelGrouping, CancellationToken cancellationToken)
		{
			return new BatchQueryExpressionTranslator(expressionContext, expressionGenerator, tableGenerator, referenceContext, declarations, queryParameterMap, model, schema, daxCapabilities, featureSwitchProvider, comparer, suppressModelGrouping, cancellationToken).Translate(expressionNode);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002E8EC File Offset: 0x0002CAEC
		protected override QueryExpression TranslateAggregatableSubQuery(AggregatableSubQueryExpressionNode node, out QueryExpressionFeatures features)
		{
			GeneratedTable generatedTable = this.m_tableGenerator.Generate(node.Table, this.m_expressionGenerator, null);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			QueryExpressionContext queryExpressionContext = this.m_expressionGenerator.TranslateExpression(node.TargetExpressionId, this.m_expressionContext);
			this.m_referenceContext.PopReferenceTable();
			features = queryExpressionContext.QueryExpressionFeatures;
			return generatedTable.QueryTable.Project(queryExpressionContext.QueryExpression);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002E95A File Offset: 0x0002CB5A
		protected override QueryExpression TranslateBatchSubQuery(BatchSubQueryExpressionNode node)
		{
			return this.m_tableGenerator.Generate(node.Table, this.m_expressionGenerator, null).QueryTable.Expression;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002E980 File Offset: 0x0002CB80
		protected override QueryExpression TranslateAggregatableCurrentGroup(AggregatableCurrentGroupExpressionNode node, out QueryExpressionFeatures features)
		{
			GeneratedTable referenceTable = this.m_referenceContext.ReferenceTable;
			GeneratedTable generatedTable = new GeneratedTable(referenceTable.QueryTable.CurrentGroup(), referenceTable.ColumnMap);
			this.m_referenceContext.PushReferenceTable(generatedTable);
			QueryExpression queryExpression = this.Translate(node.Projection, out features);
			this.m_referenceContext.PopReferenceTable();
			return generatedTable.QueryTable.Project(queryExpression);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002E9E4 File Offset: 0x0002CBE4
		protected override QueryExpression TranslateBatchColumnReference(BatchColumnReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryTableColumn queryTableColumn;
			if (!this.m_referenceContext.TryGetAvailableColumn(node.ColumnName, out queryTableColumn))
			{
				throw BasicQueryExpressionTranslatorBase.TranslationError(QueryGenerationDevErrors.MissingColumn(node.ColumnName.MarkAsModelInfo()));
			}
			features = QueryExpressionFeatures.None;
			return queryTableColumn.QdmReference();
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002EA28 File Offset: 0x0002CC28
		protected override QueryExpression TranslateBatchColumnReference(BatchColumnReferenceByExpressionIdExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryTableColumn queryTableColumn;
			if (!this.m_referenceContext.TryGetAvailableColumn(node.ExpressionId, out queryTableColumn))
			{
				throw BasicQueryExpressionTranslatorBase.TranslationError(QueryGenerationDevErrors.MissingColumn(node.ExpressionId));
			}
			features = QueryExpressionFeatures.None;
			return queryTableColumn.QdmReference();
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002EA64 File Offset: 0x0002CC64
		protected override QueryExpression TranslateBatchScalarDeclarationReference(BatchScalarDeclarationReferenceExpressionNode node, out QueryExpressionFeatures features)
		{
			features = QueryExpressionFeatures.None;
			return this.m_declarations.GetScalarDeclaration(node.DeclarationName).Expression;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002EA80 File Offset: 0x0002CC80
		protected override QueryExpression TranslateApplyContextFilter(ApplyContextFilterExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryExpression queryExpression = this.Translate(node.Expression, out features);
			IEnumerable<GeneratedTable> enumerable = this.m_tableGenerator.GenerateTables(node.ContextTables, this.m_expressionGenerator, null);
			return queryExpression.Calculate(enumerable.Select((GeneratedTable t) => t.QueryTable.Expression));
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		protected override QueryExpression TranslateBatchFilterInlinedDeclarationCalculation(BatchFilterInlinedDeclarationCalculationExpressionNode node, out QueryExpressionFeatures features)
		{
			QueryExpressionContext queryExpressionContext = base.Translate(node.ExpressionNode);
			features = queryExpressionContext.QueryExpressionFeatures;
			IEnumerable<QueryExpression> enumerable;
			if (node.FilterDeclaration.CanExpandToMultiTables)
			{
				enumerable = (from d in this.m_declarations.GetMultiTableDeclarations(node.FilterDeclaration.DeclarationName)
					select d.Table.QueryTable.Expression).Evaluate<QueryExpression>();
			}
			else
			{
				GeneratedTableDeclaration singleTableDeclaration = this.m_declarations.GetSingleTableDeclaration(node.FilterDeclaration.DeclarationName);
				enumerable = new QueryExpression[] { singleTableDeclaration.Table.QueryTable.Expression };
			}
			return queryExpressionContext.QueryExpression.Calculate(enumerable);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002EB8E File Offset: 0x0002CD8E
		protected override QueryExpression TranslateTableSubQuery(TableSubQueryExpressionNode node, out QueryExpressionFeatures features)
		{
			throw BasicQueryExpressionTranslatorBase.SyntaxError(node);
		}

		// Token: 0x040005DB RID: 1499
		private readonly BatchQueryTableGenerator m_tableGenerator;

		// Token: 0x040005DC RID: 1500
		private readonly BatchQueryExpressionReferenceContext m_referenceContext;

		// Token: 0x040005DD RID: 1501
		private readonly GeneratedDeclarationCollection m_declarations;
	}
}
