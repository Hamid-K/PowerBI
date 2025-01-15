using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.DataShapeQueryGeneration;

namespace Microsoft.DataShaping.SemanticQueryTranslation.SparklineData
{
	// Token: 0x02000023 RID: 35
	internal sealed class SparklineDataQueryDefinitionRewriter : ResolvedQueryDefinitionRewriter
	{
		// Token: 0x06000111 RID: 273 RVA: 0x00006309 File Offset: 0x00004509
		internal SparklineDataQueryDefinitionRewriter(SparklineDataExpressionRewriter expressionRewriter)
			: base(Util.EmptyReadOnlyCollection<ResolvedQueryExpressionRewriter>())
		{
			this._sparklineDataExpressionRewriter = expressionRewriter;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006320 File Offset: 0x00004520
		internal static ResolvedQueryDefinition RewriteQuery(ResolvedQueryDefinition query, IQuerySchemaExtender querySchemaExtender, ISourceNameGenerator nameGenerator, SemanticQueryTranslatorContext context, out SparklineDataStatistics sparklineStatistics)
		{
			sparklineStatistics = SparklineDataStatistics.Empty;
			if (!query.Select.Any((ResolvedQuerySelect select) => select.Expression is ResolvedQuerySparklineDataExpression))
			{
				return query;
			}
			if (!context.FeatureSwitchProvider.IsEnabled(FeatureSwitchKind.SparklineData))
			{
				context.ErrorContext.Register(SemanticQueryTranslationMessages.UnsupportedSparklineDataExpression(EngineMessageSeverity.Error));
				return query;
			}
			SparklineDataExpressionRewriter sparklineDataExpressionRewriter = new SparklineDataExpressionRewriter(context, query.From, querySchemaExtender, nameGenerator);
			ResolvedQueryDefinition resolvedQueryDefinition = new SparklineDataQueryDefinitionRewriter(sparklineDataExpressionRewriter).Rewrite(query);
			sparklineStatistics = sparklineDataExpressionRewriter.SparklineDataStatistics;
			return resolvedQueryDefinition;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000063A8 File Offset: 0x000045A8
		protected override ResolvedQueryExpression RewriteSelectExpression(ResolvedQueryExpression selectExpression)
		{
			return selectExpression.Accept<ResolvedQueryExpression>(this._sparklineDataExpressionRewriter);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000063B6 File Offset: 0x000045B6
		protected override IReadOnlyList<ResolvedQuerySource> GetNewSources()
		{
			return this._sparklineDataExpressionRewriter.GetAndClearNewSources();
		}

		// Token: 0x0400007D RID: 125
		private SparklineDataExpressionRewriter _sparklineDataExpressionRewriter;
	}
}
