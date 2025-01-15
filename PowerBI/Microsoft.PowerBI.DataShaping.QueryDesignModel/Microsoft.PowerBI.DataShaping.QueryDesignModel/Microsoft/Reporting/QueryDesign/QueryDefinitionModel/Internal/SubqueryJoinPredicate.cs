using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000EC RID: 236
	internal sealed class SubqueryJoinPredicate : IJoinPredicate
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x000238AA File Offset: 0x00021AAA
		internal SubqueryJoinPredicate(QueryDefinition query, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			this._query = query;
			this._featureSwitchProvider = featureSwitchProvider;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000238C7 File Offset: 0x00021AC7
		public bool IsAnchored
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x000238CA File Offset: 0x00021ACA
		internal QueryDefinition Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000238D4 File Offset: 0x00021AD4
		public QueryExpression ToPredicateExpression()
		{
			if (this._predicateExpression == null)
			{
				QdmTranslationSettings qdmTranslationSettings = new QdmTranslationSettings(true, false, false);
				QueryCommandTree queryCommandTree = this._query.ToQueryCommandTree(qdmTranslationSettings, this._featureSwitchProvider, this._cancellationToken, null);
				this._predicateExpression = queryCommandTree.Query.HasAnyRows(false);
			}
			return this._predicateExpression;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00023928 File Offset: 0x00021B28
		internal static SubqueryJoinPredicate Create(QueryDefinition subquery, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			IEnumerable<IJoinPredicate> enumerable = subquery.ExplicitJoinPredicates;
			BlankRowBehavior blankRowBehavior = subquery.AllowBlankRow;
			if (enumerable == null && subquery.DefaultMeasurePredicates.Any<IJoinPredicate>())
			{
				enumerable = subquery.DefaultMeasurePredicates;
				if (blankRowBehavior == BlankRowBehavior.FilterByProjection)
				{
					blankRowBehavior = BlankRowBehavior.FilterByExplicitJoinPredicates;
				}
			}
			return new SubqueryJoinPredicate(new QueryDefinition(subquery.EntityDataModel, subquery.ConceptualSchema, featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), subquery.Groups.Select((Group g) => g.ProjectWithoutDetailsForSubqueries()), enumerable, blankRowBehavior, subquery.GroupFilter, null, null, null, null, null, subquery.Slicer, subquery.ApplyFilters, subquery.FieldsRequiringClearDefaultFilterContext, subquery.ColumnsRequiringClearDefaultFilterContext, Enumerable.Empty<SortItem>(), null, null, null, null, null, false, null), featureSwitchProvider, cancellationToken);
		}

		// Token: 0x040009B1 RID: 2481
		private readonly QueryDefinition _query;

		// Token: 0x040009B2 RID: 2482
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x040009B3 RID: 2483
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040009B4 RID: 2484
		private QueryExpression _predicateExpression;
	}
}
