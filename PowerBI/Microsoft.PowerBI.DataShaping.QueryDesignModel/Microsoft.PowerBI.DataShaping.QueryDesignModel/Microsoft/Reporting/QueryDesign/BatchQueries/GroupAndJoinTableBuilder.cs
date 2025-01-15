using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000269 RID: 617
	internal sealed class GroupAndJoinTableBuilder
	{
		// Token: 0x06001AA8 RID: 6824 RVA: 0x0004A260 File Offset: 0x00048460
		internal GroupAndJoinTableBuilder(IConceptualModel model, IConceptualSchema schema, bool includeDirectManyToManyAssociations, bool useConceptualSchema, ExpressionReferenceNameToTableMapping referenceNameMapping = null, JoinPredicateBehavior predicateBehavior = JoinPredicateBehavior.AutoPredicates, IEnumerable<string> reservedColumnNames = null)
		{
			this._namingContext = new QueryNamingContext(reservedColumnNames);
			this._measures = new List<GroupAndJoinMeasure>();
			this._groupBuilders = new List<QueryTableGroupBuilderBase>();
			this._model = model;
			this._schema = schema;
			this._includeDirectManyToManyAssociations = includeDirectManyToManyAssociations;
			this._referenceNameMapping = referenceNameMapping;
			this._predicateBehavior = predicateBehavior;
			this._useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0004A2C4 File Offset: 0x000484C4
		public QueryTable ToQueryTable()
		{
			bool flag;
			BatchQueryConstraintTelemetry batchQueryConstraintTelemetry;
			return this.ToQueryTable(out flag, out batchQueryConstraintTelemetry);
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0004A2DB File Offset: 0x000484DB
		public QueryTable ToQueryTable(out bool hasUnconstrainedJoin, out BatchQueryConstraintTelemetry constraintTelemetry)
		{
			return this.DetermineTranslationStrategy().Translate(out hasUnconstrainedJoin, out constraintTelemetry);
		}

		// Token: 0x06001AAB RID: 6827 RVA: 0x0004A2EC File Offset: 0x000484EC
		private GroupAndJoinTranslationStrategyBase DetermineTranslationStrategy()
		{
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection = this.RemoveDuplicateColumns();
			HashSet<EntitySet> hashSet = (this._useConceptualSchema ? null : GroupAndJoinTableBuilder.FindAllGroupEntitySetReferences(readOnlyCollection));
			HashSet<IConceptualEntity> hashSet2 = (this._useConceptualSchema ? GroupAndJoinTableBuilder.FindAllGroupEntityReferences(readOnlyCollection) : null);
			if (this.ShouldUseCalculateTable(hashSet, hashSet2))
			{
				return new CalculateTableStrategy(this._model, this._schema, readOnlyCollection, this._contextTables, this._measures, this._referenceNameMapping, this._allowEmptyGroups, this._useConceptualSchema);
			}
			return this.CreateSummarizeColumnsStrategy(readOnlyCollection, hashSet, hashSet2);
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x0004A368 File Offset: 0x00048568
		private bool ShouldUseCalculateTable(HashSet<EntitySet> groupEntitySets, HashSet<IConceptualEntity> groupEntities)
		{
			return !((groupEntities != null) ? (groupEntities.Count != 1) : (groupEntitySets.Count != 1)) && !this.HasAnyExistsFilters && !this.HasAnyJoinPredicateMeasures && !this.HasAnySubtotalColumns && (this._measures.Count == 0 || (this._measures.Count > 0 && this._allowEmptyGroups && (this._predicateBehavior == JoinPredicateBehavior.AutoPredicates || this._predicateBehavior == (JoinPredicateBehavior.AutoPredicates | JoinPredicateBehavior.AutoPredicatesForFilters))));
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x0004A3EC File Offset: 0x000485EC
		internal SummarizeColumnsStrategy CreateSummarizeColumnsStrategy()
		{
			ReadOnlyCollection<QueryTableColumn> readOnlyCollection = this.RemoveDuplicateColumns();
			HashSet<EntitySet> hashSet = (this._useConceptualSchema ? null : GroupAndJoinTableBuilder.FindAllGroupEntitySetReferences(readOnlyCollection));
			HashSet<IConceptualEntity> hashSet2 = (this._useConceptualSchema ? GroupAndJoinTableBuilder.FindAllGroupEntityReferences(readOnlyCollection) : null);
			return this.CreateSummarizeColumnsStrategy(readOnlyCollection, hashSet, hashSet2);
		}

		// Token: 0x06001AAE RID: 6830 RVA: 0x0004A430 File Offset: 0x00048630
		private SummarizeColumnsStrategy CreateSummarizeColumnsStrategy(ReadOnlyCollection<QueryTableColumn> groupKeys, HashSet<EntitySet> groupEntitySets, HashSet<IConceptualEntity> groupEntities)
		{
			return new SummarizeColumnsStrategy(this._model, this._schema, this._namingContext, groupKeys, this._groupBuilders, (groupEntitySets != null) ? groupEntitySets.ToList<EntitySet>() : null, (groupEntities != null) ? groupEntities.ToList<IConceptualEntity>() : null, this._measures, this._contextTables, this._existsFilters, this._referenceNameMapping, this._predicateBehavior, this._allowEmptyGroups, this._includeDirectManyToManyAssociations, this._useConceptualSchema);
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x0004A4A4 File Offset: 0x000486A4
		internal static HashSet<EntitySet> FindAllGroupEntitySetReferences(IEnumerable<QueryTableColumn> groupKeys)
		{
			return (from k in groupKeys.EmptyIfNull<QueryTableColumn>()
				from entity in k.Expression.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)
				select entity).ToSet<EntitySet>();
		}

		// Token: 0x06001AB0 RID: 6832 RVA: 0x0004A500 File Offset: 0x00048700
		internal static HashSet<IConceptualEntity> FindAllGroupEntityReferences(IEnumerable<QueryTableColumn> groupKeys)
		{
			return (from k in groupKeys.EmptyIfNull<QueryTableColumn>()
				from entity in k.Expression.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All)
				select entity).ToSet(ConceptualEntityExtensionAwareEqualityComparer.Instance);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x0004A560 File Offset: 0x00048760
		private ReadOnlyCollection<QueryTableColumn> RemoveDuplicateColumns()
		{
			HashSet<QueryTableColumn> hashSet = new HashSet<QueryTableColumn>(QueryTableColumn.DefaultExpressionComparer);
			List<QueryTableGroupBuilder> list = this.FlattenedGroupBuilders.ToList<QueryTableGroupBuilder>();
			for (int i = 0; i < list.Count; i++)
			{
				QueryTableGroupBuilder queryTableGroupBuilder = list[i];
				queryTableGroupBuilder.RemoveDuplicateColumns(hashSet);
				if (!queryTableGroupBuilder.HasGroupKeys)
				{
					queryTableGroupBuilder.MoveGroupDetails(list, i - 1);
				}
			}
			this._groupBuilders.RemoveDuplicatedKeysAndEmptyGroups(delegate(QueryTableGroupBuilderBase groupBuilder)
			{
				QueryTableRollupBuilder queryTableRollupBuilder = groupBuilder as QueryTableRollupBuilder;
				if (queryTableRollupBuilder != null)
				{
					queryTableRollupBuilder.RemoveEmptyRollupGroups();
				}
			});
			using (List<GroupAndJoinMeasure>.Enumerator enumerator = this._measures.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					GroupAndJoinMeasure measure = enumerator.Current;
					if (hashSet.Any((QueryTableColumn c) => c.Expression.Equals(measure.Column.Expression)))
					{
						throw new InvalidOperationException("Trying to add an existing group key as a table column is not allowed.");
					}
				}
			}
			return hashSet.ToReadOnlyCollection<QueryTableColumn>();
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x0004A654 File Offset: 0x00048854
		public IQueryTableGroupBuilder AddGroup()
		{
			QueryTableGroupBuilder queryTableGroupBuilder = new QueryTableGroupBuilder(this._namingContext, this._schema, this._useConceptualSchema, null);
			this._groupBuilders.Add(queryTableGroupBuilder);
			return queryTableGroupBuilder;
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0004A688 File Offset: 0x00048888
		public IQueryTableRollupBuilder AddRollup()
		{
			QueryTableRollupBuilder queryTableRollupBuilder = new QueryTableRollupBuilder(this._namingContext, this._schema, this._useConceptualSchema);
			this._groupBuilders.Add(queryTableRollupBuilder);
			return queryTableRollupBuilder;
		}

		// Token: 0x06001AB4 RID: 6836 RVA: 0x0004A6BA File Offset: 0x000488BA
		public QueryTableColumn AddGroupDetail(QueryExpression expression, string suggestedName)
		{
			return ((IQueryTableGroupBuilder)this.FindGroupBuilderForDetail(expression)).AddGroupDetail(expression, suggestedName);
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0004A6CC File Offset: 0x000488CC
		public QueryTableColumn AddOrReuseColumn(QueryExpression expression, string suggestedName, bool suppressJoinPredicate = false)
		{
			for (int i = 0; i < this._measures.Count; i++)
			{
				GroupAndJoinMeasure groupAndJoinMeasure = this._measures[i];
				if (groupAndJoinMeasure.HasMatchingExpression(expression))
				{
					bool flag = groupAndJoinMeasure.SuppressJoinPredicate && suppressJoinPredicate;
					if (flag != groupAndJoinMeasure.SuppressJoinPredicate)
					{
						this._measures[i] = new GroupAndJoinMeasure(groupAndJoinMeasure.Column, flag);
					}
					return this._measures[i].Column;
				}
			}
			GroupAndJoinMeasure groupAndJoinMeasure2 = new GroupAndJoinMeasure(new QueryTableColumn(this._namingContext.CreateOrReuseNameForMeasure(expression, null, suggestedName), expression), suppressJoinPredicate);
			this._measures.Add(groupAndJoinMeasure2);
			return groupAndJoinMeasure2.Column;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x0004A770 File Offset: 0x00048970
		public void AddContextTable(QueryExpression contextTable, bool shouldCrossFilterGroupColumns)
		{
			global::System.ValueTuple<QueryExpression, bool> valueTuple = new global::System.ValueTuple<QueryExpression, bool>(contextTable, shouldCrossFilterGroupColumns);
			Util.AddToLazyList<global::System.ValueTuple<QueryExpression, bool>>(ref this._contextTables, valueTuple);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0004A792 File Offset: 0x00048992
		public void AddContextTable(QueryTable contextTable, bool shouldCrossFilterGroupColumns = false)
		{
			this.AddContextTable(contextTable.Expression, shouldCrossFilterGroupColumns);
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x0004A7A1 File Offset: 0x000489A1
		public void AddContextTables([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryTable", "ShouldCrossFilterGroupColumns" })] IEnumerable<global::System.ValueTuple<QueryTable, bool>> contextTables)
		{
			Util.AddToLazyList<global::System.ValueTuple<QueryExpression, bool>>(ref this._contextTables, contextTables.Select(([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "QueryTable", "ShouldCrossFilterGroupColumns" })] global::System.ValueTuple<QueryTable, bool> t) => new global::System.ValueTuple<QueryExpression, bool>(t.Item1.Expression, t.Item2)));
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x0004A7D3 File Offset: 0x000489D3
		public void AddExistsFilter(QueryExistsFilter existsFilter)
		{
			Util.AddToLazyList<QueryExistsFilter>(ref this._existsFilters, existsFilter);
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x0004A7E1 File Offset: 0x000489E1
		public void AddExistsFilters(IEnumerable<QueryExistsFilter> existsFilters)
		{
			Util.AddToLazyList<QueryExistsFilter>(ref this._existsFilters, existsFilters);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x0004A7EF File Offset: 0x000489EF
		public void SetAllowEmptyGroups()
		{
			this._allowEmptyGroups = true;
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x0004A7F8 File Offset: 0x000489F8
		public QueryTableGroupBuilder FindGroupBuilderForDetail(QueryExpression expression)
		{
			ReadOnlyCollection<QueryTableGroupBuilder> readOnlyCollection = this.FlattenedGroupBuilders.ToReadOnlyCollection<QueryTableGroupBuilder>();
			return readOnlyCollection.FindGroupBuilderForDetail(readOnlyCollection.Count - 1, expression);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x0004A820 File Offset: 0x00048A20
		public bool TryGetMeasure(QueryExpression expression, out QueryTableColumn measureColumn)
		{
			GroupAndJoinMeasure groupAndJoinMeasure = this._measures.SingleOrDefault((GroupAndJoinMeasure m) => m.Column.Expression.Equals(expression));
			measureColumn = ((groupAndJoinMeasure == null) ? null : groupAndJoinMeasure.Column);
			return groupAndJoinMeasure != null;
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001ABE RID: 6846 RVA: 0x0004A864 File Offset: 0x00048A64
		private bool HasAnyJoinPredicateMeasures
		{
			get
			{
				return this._measures.Any((GroupAndJoinMeasure m) => !m.SuppressJoinPredicate);
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0004A890 File Offset: 0x00048A90
		private bool HasAnyExistsFilters
		{
			get
			{
				return this._existsFilters != null && this._existsFilters.Count > 0;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x0004A8AA File Offset: 0x00048AAA
		private IEnumerable<QueryTableGroupBuilder> FlattenedGroupBuilders
		{
			get
			{
				return this._groupBuilders.SelectMany((QueryTableGroupBuilderBase sb) => sb.GroupBuilders);
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0004A8D6 File Offset: 0x00048AD6
		private bool HasAnySubtotalColumns
		{
			get
			{
				return this._groupBuilders.Any((QueryTableGroupBuilderBase gb) => gb.HasAnySubtotalColumns);
			}
		}

		// Token: 0x04000EC5 RID: 3781
		private readonly QueryNamingContext _namingContext;

		// Token: 0x04000EC6 RID: 3782
		private readonly List<GroupAndJoinMeasure> _measures;

		// Token: 0x04000EC7 RID: 3783
		private readonly IConceptualModel _model;

		// Token: 0x04000EC8 RID: 3784
		private readonly IConceptualSchema _schema;

		// Token: 0x04000EC9 RID: 3785
		private readonly bool _includeDirectManyToManyAssociations;

		// Token: 0x04000ECA RID: 3786
		private readonly ExpressionReferenceNameToTableMapping _referenceNameMapping;

		// Token: 0x04000ECB RID: 3787
		private readonly JoinPredicateBehavior _predicateBehavior;

		// Token: 0x04000ECC RID: 3788
		private readonly List<QueryTableGroupBuilderBase> _groupBuilders;

		// Token: 0x04000ECD RID: 3789
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })]
		private List<global::System.ValueTuple<QueryExpression, bool>> _contextTables;

		// Token: 0x04000ECE RID: 3790
		private List<QueryExistsFilter> _existsFilters;

		// Token: 0x04000ECF RID: 3791
		private bool _allowEmptyGroups;

		// Token: 0x04000ED0 RID: 3792
		private bool _useConceptualSchema;
	}
}
