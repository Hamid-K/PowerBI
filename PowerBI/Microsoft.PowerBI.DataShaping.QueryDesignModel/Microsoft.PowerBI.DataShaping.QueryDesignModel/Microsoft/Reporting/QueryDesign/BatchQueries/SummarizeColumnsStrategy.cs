using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000267 RID: 615
	internal sealed class SummarizeColumnsStrategy : GroupAndJoinTranslationStrategyBase
	{
		// Token: 0x06001A9D RID: 6813 RVA: 0x00049BC0 File Offset: 0x00047DC0
		internal SummarizeColumnsStrategy(IConceptualModel model, IConceptualSchema schema, QueryNamingContext namingContext, IReadOnlyList<QueryTableColumn> groupKeys, List<QueryTableGroupBuilderBase> groupBuilders, IReadOnlyList<EntitySet> groupEntitySets, IReadOnlyList<IConceptualEntity> groupEntities, IReadOnlyList<GroupAndJoinMeasure> measures, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] IReadOnlyList<global::System.ValueTuple<QueryExpression, bool>> contextTables, IReadOnlyList<QueryExistsFilter> existsFilters, ExpressionReferenceNameToTableMapping referenceNameMapping, JoinPredicateBehavior predicateBehavior, bool allowEmptyGroups, bool includeDirectManyToManyAssociations, bool useConceptualSchema)
			: base(model, schema)
		{
			this._namingContext = namingContext;
			this._includeDirectManyToManyAssociations = includeDirectManyToManyAssociations;
			this._groupKeys = groupKeys;
			this._groupBuilders = groupBuilders;
			this._groupEntitySets = groupEntitySets;
			this._groupEntities = groupEntities;
			this._contextTables = contextTables ?? new global::System.ValueTuple<QueryExpression, bool>[0];
			this._existsFilters = existsFilters ?? new QueryExistsFilter[0];
			this._referenceNameMapping = referenceNameMapping;
			this._predicateBehavior = predicateBehavior;
			this._allowEmptyGroups = allowEmptyGroups;
			this._useConceptualSchema = useConceptualSchema;
			this._measures = SummarizeColumnsStrategy.ApplyExistsFilters(measures, this._existsFilters);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x00049C5C File Offset: 0x00047E5C
		internal override QueryTable Translate(out bool hasUnconstrainedJoin, out BatchQueryConstraintTelemetry constraintTelemetry)
		{
			constraintTelemetry = null;
			IEnumerable<QueryExpression> enumerable = this._contextTables.Select(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] global::System.ValueTuple<QueryExpression, bool> t) => t.Item1);
			IEnumerable<GroupAndJoinMeasure> enumerable2 = this._measures;
			QueryExpression queryExpression = null;
			hasUnconstrainedJoin = false;
			if (this._predicateBehavior.HasFlag(JoinPredicateBehavior.ExistsPredicates))
			{
				enumerable2 = BatchQueryConstraintValidator.TryAddExistsPredicates(this._namingContext, this._measures, this._existsFilters, this._useConceptualSchema);
			}
			if (this._predicateBehavior.HasFlag(JoinPredicateBehavior.AutoPredicates))
			{
				BatchQueryConstraintValidator batchQueryConstraintValidator = this.CreateConstraintValidator(enumerable2);
				enumerable2 = batchQueryConstraintValidator.DetermineNonEmptyBehavior(out queryExpression, out hasUnconstrainedJoin);
				constraintTelemetry = batchQueryConstraintValidator.Telemetry;
			}
			IEnumerable<IGroupItem> enumerable3 = this._groupBuilders.Select((QueryTableGroupBuilderBase sb) => sb.ToGroupItem()).ToReadOnlyCollection<IGroupItem>();
			IEnumerable<QueryGroupAndJoinAdditionalColumn> enumerable4 = enumerable2.Select((GroupAndJoinMeasure c) => new QueryGroupAndJoinAdditionalColumn(c.Column.Name, c.Column.Expression, c.SuppressJoinPredicate));
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection = this._groupBuilders.GetSubtotalIndicatorColumns().ToReadOnlyCollection<QueryTableColumn>();
			if ((from c in readOnlyCollection
				group c by c.Name).Any((IGrouping<string, QueryTableColumn> g) => g.Count<QueryTableColumn>() > 1))
			{
				throw new InvalidOperationException("Detected duplicate rollup group names in the query.");
			}
			QueryGroupAndJoinExpression queryGroupAndJoinExpression = QdmExpressionBuilder.QdmGroupAndJoin(enumerable3, enumerable4, enumerable);
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection2 = this._groupKeys.Concat(readOnlyCollection).Concat(enumerable2.Select((GroupAndJoinMeasure m) => m.Column)).ToReadOnlyCollection<QueryTableColumn>();
			QueryTableDefinition queryTableDefinition = new QueryTableDefinition(readOnlyCollection2, queryGroupAndJoinExpression, "GroupAndJoin");
			queryTableDefinition = this.ApplyNonEmptyFilters(this._groupKeys, queryExpression, queryTableDefinition);
			if (this._predicateBehavior.HasFlag(JoinPredicateBehavior.AutoPredicates) && !this._predicateBehavior.HasFlag(JoinPredicateBehavior.ExistsPredicates))
			{
				queryTableDefinition = this.ApplyTableProjection(this._groupKeys, readOnlyCollection, readOnlyCollection2, queryTableDefinition);
			}
			return queryTableDefinition;
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x00049E7A File Offset: 0x0004807A
		internal BatchQueryConstraintValidator CreateConstraintValidator()
		{
			return this.CreateConstraintValidator(this._measures);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00049E88 File Offset: 0x00048088
		internal BatchQueryConstraintValidator CreateConstraintValidator(IEnumerable<GroupAndJoinMeasure> measures)
		{
			return new BatchQueryConstraintValidator(base.Model, base.Schema, this._namingContext, this._groupEntitySets, this._groupEntities, measures.ToReadOnlyCollection<GroupAndJoinMeasure>(), this._contextTables, this._existsFilters, this._referenceNameMapping, this._includeDirectManyToManyAssociations, this._useConceptualSchema);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00049EDC File Offset: 0x000480DC
		private QueryTableDefinition ApplyTableProjection(IReadOnlyList<QueryTableColumn> groupKeys, IReadOnlyList<QueryTableColumn> subtotalIndicatorColumns, IReadOnlyList<QueryTableColumn> columns, QueryTableDefinition table)
		{
			int num = groupKeys.Count + subtotalIndicatorColumns.Count + this._measures.Count;
			if (columns.Count > num)
			{
				IEnumerable<QueryTableColumn> enumerable = from c in columns.Take(num)
					select c.QdmReference().ToQueryTableColumn(c.Name);
				ProjectSubsetStrategy projectSubsetStrategy = (this._predicateBehavior.HasFlag(JoinPredicateBehavior.AutoPredicatesForFilters) ? ProjectSubsetStrategy.Summarize : ProjectSubsetStrategy.Default);
				table = table.Project(enumerable, projectSubsetStrategy);
			}
			return table;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00049F64 File Offset: 0x00048164
		private QueryTableDefinition ApplyNonEmptyFilters(IReadOnlyList<QueryTableColumn> groupKeys, QueryExpression extraNonEmptyFilterPredicate, QueryTableDefinition table)
		{
			if (extraNonEmptyFilterPredicate != null)
			{
				table = table.Filter(extraNonEmptyFilterPredicate);
			}
			bool flag = this._measures.Any((GroupAndJoinMeasure m) => !m.SuppressJoinPredicate);
			if (!this._allowEmptyGroups && !flag && groupKeys.Count > 0)
			{
				QueryExpression queryExpression = groupKeys.Select((QueryTableColumn f) => f.QdmReference().IsNull().Not()).OrAll();
				table = table.Filter(queryExpression);
			}
			return table;
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00049FF4 File Offset: 0x000481F4
		private static IReadOnlyList<GroupAndJoinMeasure> ApplyExistsFilters(IReadOnlyList<GroupAndJoinMeasure> rawMeasures, IReadOnlyList<QueryExistsFilter> existsFilters)
		{
			if (existsFilters.Count == 0)
			{
				return rawMeasures;
			}
			List<GroupAndJoinMeasure> list = rawMeasures.EmptyIfNull<GroupAndJoinMeasure>().ToList<GroupAndJoinMeasure>();
			IList<QueryExpression> list2 = QueryExistsFilterTranslator.Translate(existsFilters).Evaluate<QueryExpression>();
			for (int i = 0; i < list.Count; i++)
			{
				GroupAndJoinMeasure groupAndJoinMeasure = list[i];
				QueryTableColumn column = groupAndJoinMeasure.Column;
				QueryCalculateExpression queryCalculateExpression = column.Expression.Calculate(list2);
				list[i] = new GroupAndJoinMeasure(queryCalculateExpression.ToQueryTableColumn(column.Name), groupAndJoinMeasure.SuppressJoinPredicate);
			}
			return list;
		}

		// Token: 0x04000EB2 RID: 3762
		private readonly QueryNamingContext _namingContext;

		// Token: 0x04000EB3 RID: 3763
		private readonly IReadOnlyList<QueryTableColumn> _groupKeys;

		// Token: 0x04000EB4 RID: 3764
		private readonly List<QueryTableGroupBuilderBase> _groupBuilders;

		// Token: 0x04000EB5 RID: 3765
		private readonly IReadOnlyList<EntitySet> _groupEntitySets;

		// Token: 0x04000EB6 RID: 3766
		private readonly IReadOnlyList<IConceptualEntity> _groupEntities;

		// Token: 0x04000EB7 RID: 3767
		private readonly IReadOnlyList<GroupAndJoinMeasure> _measures;

		// Token: 0x04000EB8 RID: 3768
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })]
		private readonly IReadOnlyList<global::System.ValueTuple<QueryExpression, bool>> _contextTables;

		// Token: 0x04000EB9 RID: 3769
		private readonly IReadOnlyList<QueryExistsFilter> _existsFilters;

		// Token: 0x04000EBA RID: 3770
		private readonly ExpressionReferenceNameToTableMapping _referenceNameMapping;

		// Token: 0x04000EBB RID: 3771
		private readonly JoinPredicateBehavior _predicateBehavior;

		// Token: 0x04000EBC RID: 3772
		private readonly bool _allowEmptyGroups;

		// Token: 0x04000EBD RID: 3773
		private readonly bool _includeDirectManyToManyAssociations;

		// Token: 0x04000EBE RID: 3774
		private readonly bool _useConceptualSchema;
	}
}
