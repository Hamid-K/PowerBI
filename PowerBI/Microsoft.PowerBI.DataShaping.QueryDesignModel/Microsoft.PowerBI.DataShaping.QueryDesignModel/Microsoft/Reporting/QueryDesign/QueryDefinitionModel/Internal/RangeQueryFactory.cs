using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000110 RID: 272
	internal static class RangeQueryFactory
	{
		// Token: 0x02000366 RID: 870
		internal abstract class RangeQueryItem : IEquatable<RangeQueryFactory.RangeQueryItem>
		{
			// Token: 0x06001F38 RID: 7992 RVA: 0x000561F4 File Offset: 0x000543F4
			internal QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, string rangeFieldName, Measure measure, IEnumerable<IJoinPredicate> joinPredicates, BlankRowBehavior allowBlankRow, bool reuseSlicerAndApplyFilters, bool measureShouldUseAggregateOverExtensionColumn, bool crossJoinQueryPlan, CancellationToken cancellationToken, out string aggregateFieldName)
			{
				IEnumerable<Group> enumerable;
				if (rangeFieldName == null || queryDefinition.Groups.Any((Group g) => g.Name == rangeFieldName))
				{
					enumerable = queryDefinition.Groups.Select((Group g) => g.OmitDetails());
				}
				else
				{
					enumerable = queryDefinition.Groups;
				}
				rangeFieldName = rangeFieldName ?? measure.Name;
				EntityDataModel entityDataModel = queryDefinition.EntityDataModel;
				IConceptualSchema conceptualSchema = queryDefinition.ConceptualSchema;
				bool flag = featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
				IEnumerable<Group> enumerable2 = enumerable;
				GroupFilter groupFilter = queryDefinition.GroupFilter;
				object obj;
				if (!measureShouldUseAggregateOverExtensionColumn)
				{
					obj = null;
				}
				else
				{
					(obj = new Measure[1])[0] = measure;
				}
				IEnumerable<Measure> enumerable3 = obj;
				QueryDefinition queryDefinition2 = new QueryDefinition(entityDataModel, conceptualSchema, flag, enumerable2, joinPredicates, allowBlankRow, groupFilter, null, null, enumerable3, null, null, reuseSlicerAndApplyFilters ? queryDefinition.Slicer : null, reuseSlicerAndApplyFilters ? queryDefinition.ApplyFilters : Enumerable.Empty<QueryDefinition>(), queryDefinition.FieldsRequiringClearDefaultFilterContext, queryDefinition.ColumnsRequiringClearDefaultFilterContext, null, null, null, null, null, null, false, null);
				QdmTranslationSettings qdmTranslationSettings = new QdmTranslationSettings(true, crossJoinQueryPlan, false);
				QueryExpressionBinding queryExpressionBinding = queryDefinition2.ToQueryCommandTree(qdmTranslationSettings, featureSwitchProvider, cancellationToken, null).Query.BindAs(rangeFieldName);
				QueryExpression queryExpression = ((measure == null || measureShouldUseAggregateOverExtensionColumn) ? queryExpressionBinding.Variable.Field(rangeFieldName) : measure.Expression);
				aggregateFieldName = rangeFieldName;
				return queryExpressionBinding.Project(queryExpression, ProjectSubsetStrategy.Default);
			}

			// Token: 0x06001F39 RID: 7993
			public abstract bool Equals(RangeQueryFactory.RangeQueryItem other);

			// Token: 0x06001F3A RID: 7994 RVA: 0x0005634C File Offset: 0x0005454C
			public override bool Equals(object obj)
			{
				return this.Equals(obj as RangeQueryFactory.RangeQueryItem);
			}

			// Token: 0x06001F3B RID: 7995 RVA: 0x0005635A File Offset: 0x0005455A
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x04001255 RID: 4693
			internal const string MinPrefix = "Min";

			// Token: 0x04001256 RID: 4694
			internal const string MaxPrefix = "Max";
		}

		// Token: 0x02000367 RID: 871
		internal sealed class GroupRangeQueryItem : RangeQueryFactory.RangeQueryItem
		{
			// Token: 0x06001F3D RID: 7997 RVA: 0x0005636A File Offset: 0x0005456A
			internal GroupRangeQueryItem(Group group)
			{
				this.Group = group;
			}

			// Token: 0x1700081D RID: 2077
			// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00056379 File Offset: 0x00054579
			public Group Group { get; }

			// Token: 0x06001F3F RID: 7999 RVA: 0x00056381 File Offset: 0x00054581
			internal QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, bool reuseSlicerAndApplyFilters, CancellationToken cancellationToken, out string aggregateFieldName)
			{
				return this.CreateAggregatableSubQueryExpression(queryDefinition, featureSwitchProvider, this.Group.Keys[0].Name, reuseSlicerAndApplyFilters, cancellationToken, out aggregateFieldName);
			}

			// Token: 0x06001F40 RID: 8000 RVA: 0x000563A8 File Offset: 0x000545A8
			private QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, string rangeFieldName, bool reuseSlicerAndApplyFilters, CancellationToken cancellationToken, out string aggregateFieldName)
			{
				ArgumentValidation.CheckCondition(queryDefinition.Groups.Contains(this.Group), "group");
				IEnumerable<IJoinPredicate> enumerable = null;
				if (queryDefinition.ExplicitJoinPredicates != null)
				{
					enumerable = queryDefinition.ExplicitJoinPredicates.ToArray<IJoinPredicate>();
				}
				else if (queryDefinition.DefaultMeasurePredicates.Any<IJoinPredicate>())
				{
					enumerable = queryDefinition.DefaultMeasurePredicates.ToArray<IJoinPredicate>();
				}
				return base.CreateAggregatableSubQueryExpression(queryDefinition, featureSwitchProvider, rangeFieldName, null, enumerable, BlankRowBehavior.FilterByExplicitJoinPredicates, reuseSlicerAndApplyFilters, false, false, cancellationToken, out aggregateFieldName);
			}

			// Token: 0x06001F41 RID: 8001 RVA: 0x00056415 File Offset: 0x00054615
			public override bool Equals(RangeQueryFactory.RangeQueryItem other)
			{
				return this == other;
			}

			// Token: 0x06001F42 RID: 8002 RVA: 0x0005641C File Offset: 0x0005461C
			public static QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, Group group, CancellationToken cancellationToken)
			{
				string text;
				return new RangeQueryFactory.GroupRangeQueryItem(group).CreateAggregatableSubQueryExpression(queryDefinition, featureSwitchProvider, true, cancellationToken, out text);
			}

			// Token: 0x06001F43 RID: 8003 RVA: 0x0005643C File Offset: 0x0005463C
			public static QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, Group group, string rangeFieldName, CancellationToken cancellationToken)
			{
				string text;
				return new RangeQueryFactory.GroupRangeQueryItem(group).CreateAggregatableSubQueryExpression(queryDefinition, featureSwitchProvider, rangeFieldName, true, cancellationToken, out text);
			}
		}

		// Token: 0x02000368 RID: 872
		internal sealed class MeasureRangeQueryItem : RangeQueryFactory.RangeQueryItem
		{
			// Token: 0x06001F44 RID: 8004 RVA: 0x0005645C File Offset: 0x0005465C
			internal MeasureRangeQueryItem(Measure measure)
			{
				this.Measure = measure;
			}

			// Token: 0x1700081E RID: 2078
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0005646B File Offset: 0x0005466B
			public Measure Measure { get; }

			// Token: 0x06001F46 RID: 8006 RVA: 0x00056474 File Offset: 0x00054674
			internal QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, bool reuseSlicerAndApplyFilters, CancellationToken cancellationToken, out string aggregateFieldName)
			{
				ArgumentValidation.CheckCondition(queryDefinition.Measures.Contains(this.Measure), "measure");
				bool flag = QdmCommandTreeTranslator.ShouldUseCrossJoinQueryPlan(queryDefinition.EntityDataModel, queryDefinition.ConceptualSchema, queryDefinition.UseConceptualSchema, this.Measure.Expression);
				IEnumerable<IJoinPredicate> enumerable;
				bool flag2;
				if (queryDefinition.Groups.Any((Group g) => !g.IsProjected))
				{
					enumerable = queryDefinition.CreateConstraintValidator(flag).GetJoinPredicates();
					flag2 = true;
				}
				else
				{
					enumerable = Enumerable.Empty<IJoinPredicate>();
					flag2 = false;
				}
				return base.CreateAggregatableSubQueryExpression(queryDefinition, featureSwitchProvider, null, this.Measure, enumerable, BlankRowBehavior.Allow, reuseSlicerAndApplyFilters, flag2, flag, cancellationToken, out aggregateFieldName);
			}

			// Token: 0x06001F47 RID: 8007 RVA: 0x0005651C File Offset: 0x0005471C
			public override bool Equals(RangeQueryFactory.RangeQueryItem other)
			{
				if (other == null)
				{
					return false;
				}
				if (this == other)
				{
					return true;
				}
				RangeQueryFactory.MeasureRangeQueryItem measureRangeQueryItem = other as RangeQueryFactory.MeasureRangeQueryItem;
				return measureRangeQueryItem != null && this.Measure.Equals(measureRangeQueryItem.Measure);
			}

			// Token: 0x06001F48 RID: 8008 RVA: 0x00056551 File Offset: 0x00054751
			public override int GetHashCode()
			{
				return this.Measure.GetHashCode();
			}

			// Token: 0x06001F49 RID: 8009 RVA: 0x00056560 File Offset: 0x00054760
			public static QueryExpression CreateAggregatableSubQueryExpression(QueryDefinition queryDefinition, IFeatureSwitchProvider featureSwitchProvider, Measure measure, CancellationToken cancellationToken)
			{
				string text;
				return new RangeQueryFactory.MeasureRangeQueryItem(measure).CreateAggregatableSubQueryExpression(queryDefinition, featureSwitchProvider, true, cancellationToken, out text);
			}
		}
	}
}
