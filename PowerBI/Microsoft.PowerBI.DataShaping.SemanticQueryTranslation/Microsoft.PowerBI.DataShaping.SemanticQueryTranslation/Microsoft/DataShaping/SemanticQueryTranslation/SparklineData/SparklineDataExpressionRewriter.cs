using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.SparklineData
{
	// Token: 0x02000022 RID: 34
	internal sealed class SparklineDataExpressionRewriter : ResolvedQueryExpressionRewriter
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000060CC File Offset: 0x000042CC
		internal SparklineDataExpressionRewriter(SemanticQueryTranslatorContext context, IReadOnlyList<ResolvedQuerySource> queryFromSources, IQuerySchemaExtender querySchemaExtender, ISourceNameGenerator nameGenerator)
		{
			this._context = context;
			this._queryFromSources = queryFromSources;
			this._querySchemaExtender = querySchemaExtender;
			this._nameGenerator = nameGenerator;
			this._sparklineCount = 0;
			this._sparklinesTotalPointsCount = 0;
			this._newSources = new List<ResolvedQuerySource>();
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000610A File Offset: 0x0000430A
		public SparklineDataStatistics SparklineDataStatistics
		{
			get
			{
				return new SparklineDataStatistics(this._sparklineCount, this._sparklinesTotalPointsCount);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000611D File Offset: 0x0000431D
		public IReadOnlyList<ResolvedQuerySource> GetAndClearNewSources()
		{
			IReadOnlyList<ResolvedQuerySource> newSources = this._newSources;
			this._newSources = new List<ResolvedQuerySource>();
			return newSources;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006130 File Offset: 0x00004330
		public override ResolvedQueryExpression Visit(ResolvedQuerySparklineDataExpression expression)
		{
			this._sparklineCount++;
			this._sparklinesTotalPointsCount += expression.PointsPerSparkline;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression;
			string text;
			IConceptualProperty conceptualProperty;
			if (!this.TryCreateQdmExpression(expression, out queryExpression) || !SemanticQueryToDaxTranslator.TryTranslateQueryExpressionToDax(this._context, queryExpression, out text) || !this.TryGetPropertyOrAggregationInputProperty<IConceptualProperty>(expression.Measure, out conceptualProperty))
			{
				return expression;
			}
			IConceptualMeasure conceptualMeasure = this._querySchemaExtender.CreateMeasure(conceptualProperty.Entity.Name, ConceptualPrimitiveType.Text, "Sparkline", text);
			ResolvedQuerySource measureSource = this.GetMeasureSource(conceptualMeasure.Entity);
			return conceptualMeasure.Measure(measureSource.Name);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000061C4 File Offset: 0x000043C4
		private ResolvedQuerySource GetMeasureSource(IConceptualEntity entity)
		{
			ResolvedQuerySource resolvedQuerySource;
			if (this.TryGetExistingSource(entity, out resolvedQuerySource))
			{
				return resolvedQuerySource;
			}
			string text = this._nameGenerator.GenerateUniqueName();
			ResolvedEntitySource resolvedEntitySource = entity.EntitySource(text, entity.EntityContainerName);
			this._newSources.Add(resolvedEntitySource);
			return resolvedEntitySource;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006208 File Offset: 0x00004408
		private bool TryGetExistingSource(IConceptualEntity entity, out ResolvedQuerySource existingSource)
		{
			existingSource = (from f in this._queryFromSources.OfType<ResolvedEntitySource>()
				where EdmNameComparer.Instance.Equals(f.Entity.EdmName, entity.EdmName) && ConceptualNameComparer.Instance.Equals(f.Entity.EntityContainerName, entity.EntityContainerName)
				select f).FirstOrDefault<ResolvedEntitySource>();
			if (existingSource != null)
			{
				return true;
			}
			existingSource = (from f in this._newSources.OfType<ResolvedEntitySource>()
				where EdmNameComparer.Instance.Equals(f.Entity.EdmName, entity.EdmName) && ConceptualNameComparer.Instance.Equals(f.Entity.EntityContainerName, entity.EntityContainerName)
				select f).FirstOrDefault<ResolvedEntitySource>();
			return existingSource != null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006278 File Offset: 0x00004478
		private bool TryCreateQdmExpression(ResolvedQuerySparklineDataExpression expression, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression qdmExpression)
		{
			qdmExpression = SemanticQueryExpressionTranslator.TranslateExpression(this._context.ErrorContext, this._context.Model, this._context.Schema, this._context.FeatureSwitchProvider, expression, true);
			if (qdmExpression == null)
			{
				SemanticQueryTranslationUtils.EnsureContextError(this._context.ErrorContext, SemanticQueryTranslationMessages.UnknownSparklineDataValidationError(EngineMessageSeverity.Error));
				return false;
			}
			return true;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000062D8 File Offset: 0x000044D8
		private bool TryGetPropertyOrAggregationInputProperty<TProperty>(ResolvedQueryExpression expression, out TProperty propertyInstance) where TProperty : class, IConceptualProperty
		{
			ResolvedQueryAggregationExpression resolvedQueryAggregationExpression = expression as ResolvedQueryAggregationExpression;
			if (resolvedQueryAggregationExpression != null)
			{
				return resolvedQueryAggregationExpression.Expression.TryGetAsProperty(out propertyInstance);
			}
			return expression.TryGetAsProperty(out propertyInstance);
		}

		// Token: 0x04000076 RID: 118
		private readonly SemanticQueryTranslatorContext _context;

		// Token: 0x04000077 RID: 119
		private readonly IReadOnlyList<ResolvedQuerySource> _queryFromSources;

		// Token: 0x04000078 RID: 120
		private readonly IQuerySchemaExtender _querySchemaExtender;

		// Token: 0x04000079 RID: 121
		private readonly ISourceNameGenerator _nameGenerator;

		// Token: 0x0400007A RID: 122
		private int _sparklineCount;

		// Token: 0x0400007B RID: 123
		private int _sparklinesTotalPointsCount;

		// Token: 0x0400007C RID: 124
		private List<ResolvedQuerySource> _newSources;
	}
}
