using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000FE RID: 254
	internal static class QdmFilterBuilder
	{
		// Token: 0x06000EB4 RID: 3764 RVA: 0x000278F4 File Offset: 0x00025AF4
		internal static QueryExpression[] QdmFilters(this IEnumerable<FilterCondition> filterConditions, IConceptualModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, CancellationToken cancellationToken, ScanKind preferredScanKind = ScanKind.InheritFilterContextIncludeBlankRow, bool clearDefaultValues = true)
		{
			clearDefaultValues &= daxCapabilities.IsSupported(ModelCapabilitiesKind.DefaultMembers);
			bool flag = featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			QdmDecomposedMultiColumnFiltersBuilder qdmDecomposedMultiColumnFiltersBuilder = new QdmDecomposedMultiColumnFiltersBuilder(model, schema, comparer, clearDefaultValues, flag);
			QdmSingleEntityFilterBuilder qdmSingleEntityFilterBuilder = new QdmSingleEntityFilterBuilder(qdmDecomposedMultiColumnFiltersBuilder, daxCapabilities, comparer, preferredScanKind, cancellationToken, clearDefaultValues, flag, schema);
			QdmMultipleEntitiesFilterBuilder qdmMultipleEntitiesFilterBuilder = new QdmMultipleEntitiesFilterBuilder(qdmDecomposedMultiColumnFiltersBuilder, qdmSingleEntityFilterBuilder, schema, comparer, cancellationToken, flag);
			return new QdmFilterBuilder.QdmFiltersBuilder(daxCapabilities, comparer, qdmSingleEntityFilterBuilder, qdmMultipleEntitiesFilterBuilder, flag).BuildQDMFilters(filterConditions);
		}

		// Token: 0x02000327 RID: 807
		private sealed class QdmFiltersBuilder
		{
			// Token: 0x06001E06 RID: 7686 RVA: 0x00051FF5 File Offset: 0x000501F5
			public QdmFiltersBuilder(DaxCapabilities daxCapabilities, IDataComparer comparer, QdmSingleEntityFilterBuilder singleEntityFilterBuilder, QdmMultipleEntitiesFilterBuilder multiEntityFilterBuilder, bool useConceptualSchema)
			{
				this._daxCapabilities = daxCapabilities;
				this._comparer = comparer;
				this._singleEntityFilterBuilder = singleEntityFilterBuilder;
				this._multiEntityFilterBuilder = multiEntityFilterBuilder;
				this._useConceptualSchema = useConceptualSchema;
			}

			// Token: 0x06001E07 RID: 7687 RVA: 0x00052024 File Offset: 0x00050224
			public QueryExpression[] BuildQDMFilters(IEnumerable<FilterCondition> filterConditions)
			{
				IReadOnlyList<FilterCondition> readOnlyList = filterConditions.Flatten().EvaluateReadOnly<FilterCondition>();
				IDictionary<ReadOnlyOrderedHashSet<IConceptualColumn>, IList<IConceptualEntity>> predicateGroupingColumnSets;
				IDictionary<ReadOnlyOrderedHashSet<IEdmFieldInstance>, IList<EntitySet>> predicateGroupingFieldSets;
				List<QdmFilterPredicate> list;
				List<QdmFilterPredicate> list2;
				this.GroupPredicatesByFilteredFields(readOnlyList, out list, out list2, out predicateGroupingFieldSets, out predicateGroupingColumnSets);
				IList<QdmFilterPredicate> singleEntitySlicersByFilteredFields;
				if (list.IsNullOrEmpty<QdmFilterPredicate>())
				{
					singleEntitySlicersByFilteredFields = new List<QdmFilterPredicate>();
				}
				else if (this._useConceptualSchema)
				{
					SupersetMergingCollection<ReadOnlyOrderedHashSet<IConceptualColumn>, IConceptualColumn> mergedSingleEntityPredicateColumns = SupersetMergingCollection<ReadOnlyOrderedHashSet<IConceptualColumn>, IConceptualColumn>.CopyFrom(list.Select((QdmFilterPredicate p) => p.FilterIdentityTargetColumnList));
					singleEntitySlicersByFilteredFields = new List<QdmFilterPredicate>();
					ILookup<ReadOnlyOrderedHashSet<IConceptualColumn>, QdmFilterPredicate> lookup = list.ToLookup((QdmFilterPredicate p) => mergedSingleEntityPredicateColumns.GetSupersetsOf(p.FilterIdentityTargetColumnList).First<ReadOnlyOrderedHashSet<IConceptualColumn>>());
					for (int i = 0; i < lookup.Count; i++)
					{
						IGrouping<ReadOnlyOrderedHashSet<IConceptualColumn>, QdmFilterPredicate> grouping = ((lookup != null) ? lookup.ElementAt(i) : null);
						IConceptualEntity conceptualEntity = predicateGroupingColumnSets[grouping.Key].Single("singleEntityPredicatesByFilteredFields should contain a collection of predicates with only 1 referenced entity.", Array.Empty<string>());
						foreach (QueryExpression queryExpression in this._singleEntityFilterBuilder.BuildFilterExpressionsForEntity(null, null, conceptualEntity, grouping))
						{
							singleEntitySlicersByFilteredFields.Add(new QdmFilterPredicate(queryExpression, null, grouping.Key));
						}
					}
				}
				else
				{
					SupersetMergingCollection<ReadOnlyOrderedHashSet<IEdmFieldInstance>, IEdmFieldInstance> mergedSingleEntityPredicateFields = SupersetMergingCollection<ReadOnlyOrderedHashSet<IEdmFieldInstance>, IEdmFieldInstance>.CopyFrom(list.Select((QdmFilterPredicate p) => p.FilterIdentityTargetFieldList));
					singleEntitySlicersByFilteredFields = new List<QdmFilterPredicate>();
					foreach (IGrouping<ReadOnlyOrderedHashSet<IEdmFieldInstance>, QdmFilterPredicate> grouping2 in list.ToLookup((QdmFilterPredicate p) => mergedSingleEntityPredicateFields.GetSupersetsOf(p.FilterIdentityTargetFieldList).First<ReadOnlyOrderedHashSet<IEdmFieldInstance>>()))
					{
						EntitySet entitySet = predicateGroupingFieldSets[grouping2.Key].Single("singleEntityPredicatesByFilteredFields should contain a collection of predicates with only 1 referenced entity.", Array.Empty<string>());
						foreach (QueryExpression queryExpression2 in this._singleEntityFilterBuilder.BuildFilterExpressionsForEntity(entitySet, grouping2, null, null))
						{
							singleEntitySlicersByFilteredFields.Add(new QdmFilterPredicate(queryExpression2, grouping2.Key, null));
						}
					}
				}
				IEnumerable<QueryExpression> enumerable = singleEntitySlicersByFilteredFields.Select((QdmFilterPredicate s) => s.PredicateExpression);
				if (list2.IsNullOrEmpty<QdmFilterPredicate>())
				{
					return enumerable.ToArray<QueryExpression>();
				}
				if (this._useConceptualSchema)
				{
					Dictionary<ReadOnlyOrderedHashSet<IConceptualColumn>, List<QdmFilterPredicate>> dictionary = new Dictionary<ReadOnlyOrderedHashSet<IConceptualColumn>, List<QdmFilterPredicate>>();
					foreach (QdmFilterPredicate qdmFilterPredicate in list2)
					{
						ReadOnlyOrderedHashSet<IConceptualColumn> filterIdentityTargetColumnList = qdmFilterPredicate.FilterIdentityTargetColumnList;
						ReadOnlyOrderedHashSet<IEdmFieldInstance> filterIdentityTargetFieldList = qdmFilterPredicate.FilterIdentityTargetFieldList;
						List<QdmFilterPredicate> list3;
						if (dictionary.TryGetValue(filterIdentityTargetColumnList, out list3))
						{
							list3.Add(qdmFilterPredicate);
						}
						else
						{
							dictionary.Add(filterIdentityTargetColumnList, new List<QdmFilterPredicate> { qdmFilterPredicate });
						}
					}
					IList<QueryExpression> list4 = dictionary.SelectMany((KeyValuePair<ReadOnlyOrderedHashSet<IConceptualColumn>, List<QdmFilterPredicate>> kvp) => this.QdmFiltersForMultipleEntities(kvp.Value, null, singleEntitySlicersByFilteredFields.Where((QdmFilterPredicate s) => s.FilterIdentityTargetColumnList.IsSubsetOf(kvp.Key)), null, kvp.Key, predicateGroupingColumnSets[kvp.Key])).Evaluate<QueryExpression>();
					return enumerable.Concat(list4).ToArray<QueryExpression>();
				}
				Dictionary<ReadOnlyOrderedHashSet<IEdmFieldInstance>, List<QdmFilterPredicate>> dictionary2 = new Dictionary<ReadOnlyOrderedHashSet<IEdmFieldInstance>, List<QdmFilterPredicate>>();
				foreach (QdmFilterPredicate qdmFilterPredicate2 in list2)
				{
					ReadOnlyOrderedHashSet<IEdmFieldInstance> filterIdentityTargetFieldList2 = qdmFilterPredicate2.FilterIdentityTargetFieldList;
					List<QdmFilterPredicate> list5;
					if (dictionary2.TryGetValue(filterIdentityTargetFieldList2, out list5))
					{
						list5.Add(qdmFilterPredicate2);
					}
					else
					{
						dictionary2.Add(filterIdentityTargetFieldList2, new List<QdmFilterPredicate> { qdmFilterPredicate2 });
					}
				}
				IList<QueryExpression> list6 = dictionary2.SelectMany((KeyValuePair<ReadOnlyOrderedHashSet<IEdmFieldInstance>, List<QdmFilterPredicate>> kvp) => this.QdmFiltersForMultipleEntities(kvp.Value, kvp.Key, singleEntitySlicersByFilteredFields.Where((QdmFilterPredicate s) => s.FilterIdentityTargetFieldList.IsSubsetOf(kvp.Key)), predicateGroupingFieldSets[kvp.Key], null, null)).Evaluate<QueryExpression>();
				return enumerable.Concat(list6).ToArray<QueryExpression>();
			}

			// Token: 0x06001E08 RID: 7688 RVA: 0x00052438 File Offset: 0x00050638
			private void GroupPredicatesByFilteredFields(IReadOnlyList<FilterCondition> conditions, out List<QdmFilterPredicate> singleEntityPredicates, out List<QdmFilterPredicate> multiEntityPredicates, out IDictionary<ReadOnlyOrderedHashSet<IEdmFieldInstance>, IList<EntitySet>> groupingFieldSetsToEntitySetsMap, out IDictionary<ReadOnlyOrderedHashSet<IConceptualColumn>, IList<IConceptualEntity>> groupingColumnSetsToEntitiesMap)
			{
				singleEntityPredicates = null;
				multiEntityPredicates = null;
				groupingFieldSetsToEntitySetsMap = new Dictionary<ReadOnlyOrderedHashSet<IEdmFieldInstance>, IList<EntitySet>>(conditions.Count, new SetEqualityComparator<IEdmFieldInstance>());
				groupingColumnSetsToEntitiesMap = new Dictionary<ReadOnlyOrderedHashSet<IConceptualColumn>, IList<IConceptualEntity>>(conditions.Count, new SetEqualityComparator<IConceptualColumn>());
				for (int i = 0; i < conditions.Count; i++)
				{
					QdmFilterPredicate qdmFilterPredicate = QdmFilterPredicate.Create(conditions[i], groupingFieldSetsToEntitySetsMap, groupingColumnSetsToEntitiesMap, this._useConceptualSchema);
					if (this._useConceptualSchema)
					{
						IList<IConceptualEntity> list;
						Microsoft.DataShaping.Contract.RetailAssert(groupingColumnSetsToEntitiesMap.TryGetValue(qdmFilterPredicate.FilterIdentityTargetColumnList, out list), "{0} should contain an key-value mapping in {1} for key {2}.", "QdmFilterPredicate", "groupingColumnSetsToEntitiesMap", "FilterIdentityTargetColumnList");
						Microsoft.DataShaping.Contract.RetailAssert(!list.IsNullOrEmpty<IConceptualEntity>(), "{0} should create a non-empty mapping of {1} for key {2}.", "QdmFilterPredicate", "groupingEntities", "FilterIdentityTargetColumnList");
						if (list.Count == 1)
						{
							Microsoft.Reporting.Util.AddToLazyList<QdmFilterPredicate>(ref singleEntityPredicates, qdmFilterPredicate);
						}
						else
						{
							Microsoft.Reporting.Util.AddToLazyList<QdmFilterPredicate>(ref multiEntityPredicates, qdmFilterPredicate);
						}
					}
					else
					{
						IList<EntitySet> list2;
						Microsoft.DataShaping.Contract.RetailAssert(groupingFieldSetsToEntitySetsMap.TryGetValue(qdmFilterPredicate.FilterIdentityTargetFieldList, out list2), "{0} should contain an key-value mapping in {1} for key {2}.", "QdmFilterPredicate", "groupingFieldSetsToEntitySetsMap", "FilterIdentityTargetFieldList");
						Microsoft.DataShaping.Contract.RetailAssert(!list2.IsNullOrEmpty<EntitySet>(), "{0} should create a non-empty mapping of {1} for key {2}.", "QdmFilterPredicate", "groupingEntitySets", "FilterIdentityTargetFieldList");
						if (list2.Count == 1)
						{
							Microsoft.Reporting.Util.AddToLazyList<QdmFilterPredicate>(ref singleEntityPredicates, qdmFilterPredicate);
						}
						else
						{
							Microsoft.Reporting.Util.AddToLazyList<QdmFilterPredicate>(ref multiEntityPredicates, qdmFilterPredicate);
						}
					}
				}
				if (!singleEntityPredicates.IsNullOrEmpty<QdmFilterPredicate>())
				{
					singleEntityPredicates.Sort(delegate(QdmFilterPredicate kvp1, QdmFilterPredicate kvp2)
					{
						if (!this._useConceptualSchema)
						{
							return kvp1.FilterIdentityTargetFieldList.Count.CompareTo(kvp2.FilterIdentityTargetFieldList.Count);
						}
						return kvp1.FilterIdentityTargetColumnList.Count.CompareTo(kvp2.FilterIdentityTargetColumnList.Count);
					});
				}
			}

			// Token: 0x06001E09 RID: 7689 RVA: 0x0005258F File Offset: 0x0005078F
			private static bool SupportsDisjunctionsOnMultipleEntities(DaxCapabilities daxCapabilities)
			{
				return daxCapabilities.IsSupported(DaxFunctionKind.SummarizeColumns) || daxCapabilities.IsSupported(DaxFunctionKind.TreatAs);
			}

			// Token: 0x06001E0A RID: 7690 RVA: 0x000525A4 File Offset: 0x000507A4
			private IReadOnlyList<QueryExpression> QdmFiltersForMultipleEntities(IReadOnlyList<QdmFilterPredicate> predicateExpressions, ISet<IEdmFieldInstance> predicatesGroupingFields, IEnumerable<QdmFilterPredicate> prefilters, IList<EntitySet> entitySets, ISet<IConceptualColumn> predicatesGroupingColumns = null, IList<IConceptualEntity> entities = null)
			{
				if (this._useConceptualSchema)
				{
					Dictionary<IConceptualEntity, List<QueryExpression>> dictionary = new Dictionary<IConceptualEntity, List<QueryExpression>>(entities.Count);
					foreach (QdmFilterPredicate qdmFilterPredicate in prefilters)
					{
						QdmFilterBuilder.QdmFiltersBuilder.MergeInto<IConceptualEntity, QueryExpression>(dictionary, qdmFilterPredicate.FilterIdentityTargetColumnList.First<IConceptualColumn>().Entity, QdmFilterBuilder.QdmFiltersBuilder.UnwrapQueryFilterExpressionPredicate(qdmFilterPredicate.PredicateExpression));
					}
					Dictionary<IConceptualEntity, List<QueryExpression>> dictionary2 = new Dictionary<IConceptualEntity, List<QueryExpression>>(entities.Count);
					List<global::System.ValueTuple<QdmFilterPredicate, bool>> list = new List<global::System.ValueTuple<QdmFilterPredicate, bool>>(1);
					for (int i = 0; i < predicateExpressions.Count; i++)
					{
						QdmFilterPredicate qdmFilterPredicate2 = predicateExpressions[i];
						ReadOnlyOrderedHashSet<IConceptualEntity> readOnlyOrderedHashSet = ReadOnlyOrderedHashSet<IConceptualEntity>.CopyFrom(predicatesGroupingColumns.Select((IConceptualColumn f) => f.Entity));
						Microsoft.DataShaping.Contract.RetailAssert(readOnlyOrderedHashSet.Count > 1, "Single-entity slicers without explicit targets should be handled separately by QdmFilterForSingleEntity.");
						bool flag;
						bool flag2;
						IDictionary<IConceptualEntity, List<QueryExpression>> dictionary3 = QdmFilterBuilder.QdmFiltersBuilder.DecomposeMultiEntityPredicate(readOnlyOrderedHashSet, qdmFilterPredicate2, this._daxCapabilities, out flag, out flag2);
						if (flag2)
						{
							QdmFilterBuilder.QdmFiltersBuilder.MergeInto<IConceptualEntity, QueryExpression>(dictionary2, dictionary3);
						}
						else
						{
							list.Add(new global::System.ValueTuple<QdmFilterPredicate, bool>(qdmFilterPredicate2, flag));
						}
						if (!dictionary3.IsNullOrEmpty<KeyValuePair<IConceptualEntity, List<QueryExpression>>>())
						{
							QdmFilterBuilder.QdmFiltersBuilder.MergeInto<IConceptualEntity, QueryExpression>(dictionary, dictionary3);
						}
					}
					IDictionary<IConceptualEntity, ISet<IConceptualColumn>> dictionary4 = entities.ToDictionary((IConceptualEntity entity) => entity, (IConceptualEntity entity) => ReadOnlyOrderedHashSet<IConceptualColumn>.CopyFrom(predicatesGroupingColumns.Where((IConceptualColumn f) => f.Entity == entity)));
					List<QueryExpression> list2 = this._singleEntityFilterBuilder.BuildFilterExpressionsForEntities(null, null, dictionary2, dictionary4).ToList<QueryExpression>();
					list2.Capacity += list.Count;
					if (list.Count > 0)
					{
						if (!QdmFilterBuilder.QdmFiltersBuilder.SupportsDisjunctionsOnMultipleEntities(this._daxCapabilities))
						{
							throw new InvalidOperationException("The query contains multi-entity filters which are not supported with the current model.");
						}
						IReadOnlyList<QueryExpression> readOnlyList = this._multiEntityFilterBuilder.BuildMultiEntityFilters(predicatesGroupingFields, list, null, null, this._daxCapabilities, predicatesGroupingColumns, dictionary, dictionary4);
						list2.AddRange(readOnlyList);
					}
					return list2;
				}
				Dictionary<EntitySet, List<QueryExpression>> dictionary5 = new Dictionary<EntitySet, List<QueryExpression>>(entitySets.Count);
				foreach (QdmFilterPredicate qdmFilterPredicate3 in prefilters)
				{
					QdmFilterBuilder.QdmFiltersBuilder.MergeInto<EntitySet, QueryExpression>(dictionary5, qdmFilterPredicate3.FilterIdentityTargetFieldList.First<IEdmFieldInstance>().Entity, QdmFilterBuilder.QdmFiltersBuilder.UnwrapQueryFilterExpressionPredicate(qdmFilterPredicate3.PredicateExpression));
				}
				Dictionary<EntitySet, List<QueryExpression>> dictionary6 = new Dictionary<EntitySet, List<QueryExpression>>(entitySets.Count);
				List<global::System.ValueTuple<QdmFilterPredicate, bool>> list3 = new List<global::System.ValueTuple<QdmFilterPredicate, bool>>(1);
				for (int j = 0; j < predicateExpressions.Count; j++)
				{
					QdmFilterPredicate qdmFilterPredicate4 = predicateExpressions[j];
					ReadOnlyOrderedHashSet<EntitySet> readOnlyOrderedHashSet2 = ReadOnlyOrderedHashSet<EntitySet>.CopyFrom(predicatesGroupingFields.Select((IEdmFieldInstance f) => f.Entity));
					Microsoft.DataShaping.Contract.RetailAssert(readOnlyOrderedHashSet2.Count > 1, "Single-entitySet slicers without explicit targets should be handled separately by QdmFilterForSingleEntity.");
					bool flag3;
					bool flag4;
					IDictionary<EntitySet, List<QueryExpression>> dictionary7 = QdmFilterBuilder.QdmFiltersBuilder.DecomposeMultiEntitySetPredicate(readOnlyOrderedHashSet2, qdmFilterPredicate4, this._daxCapabilities, out flag3, out flag4);
					if (flag4)
					{
						QdmFilterBuilder.QdmFiltersBuilder.MergeInto<EntitySet, QueryExpression>(dictionary6, dictionary7);
					}
					else
					{
						list3.Add(new global::System.ValueTuple<QdmFilterPredicate, bool>(qdmFilterPredicate4, flag3));
					}
					if (!dictionary7.IsNullOrEmpty<KeyValuePair<EntitySet, List<QueryExpression>>>())
					{
						QdmFilterBuilder.QdmFiltersBuilder.MergeInto<EntitySet, QueryExpression>(dictionary5, dictionary7);
					}
				}
				IDictionary<EntitySet, ISet<IEdmFieldInstance>> dictionary8 = entitySets.ToDictionary((EntitySet entitySet) => entitySet, (EntitySet entitySet) => ReadOnlyOrderedHashSet<IEdmFieldInstance>.CopyFrom(predicatesGroupingFields.Where((IEdmFieldInstance f) => f.Entity == entitySet)));
				List<QueryExpression> list4 = this._singleEntityFilterBuilder.BuildFilterExpressionsForEntities(dictionary6, dictionary8, null, null).ToList<QueryExpression>();
				list4.Capacity += list3.Count;
				if (list3.Count > 0)
				{
					if (!QdmFilterBuilder.QdmFiltersBuilder.SupportsDisjunctionsOnMultipleEntities(this._daxCapabilities))
					{
						throw new InvalidOperationException("The query contains multi-entity filters which are not supported with the current model.");
					}
					IReadOnlyList<QueryExpression> readOnlyList2 = this._multiEntityFilterBuilder.BuildMultiEntityFilters(predicatesGroupingFields, list3, dictionary5, dictionary8, this._daxCapabilities, predicatesGroupingColumns, null, null);
					list4.AddRange(readOnlyList2);
				}
				return list4;
			}

			// Token: 0x06001E0B RID: 7691 RVA: 0x00052988 File Offset: 0x00050B88
			private static QueryExpression UnwrapQueryFilterExpressionPredicate(QueryExpression value)
			{
				QueryFilterExpression queryFilterExpression = value as QueryFilterExpression;
				if (queryFilterExpression != null)
				{
					return queryFilterExpression.Predicate;
				}
				return value;
			}

			// Token: 0x06001E0C RID: 7692 RVA: 0x000529A8 File Offset: 0x00050BA8
			private static IDictionary<EntitySet, List<QueryExpression>> DecomposeMultiEntitySetPredicate(ISet<EntitySet> entitySets, QdmFilterPredicate singlePredicate, DaxCapabilities daxCapabilities, out bool canBeComposedWithPrefiltersOnly, out bool predicateFullyDecomposed)
			{
				QueryInExpression queryInExpression;
				bool flag;
				if (!QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInExpression>(singlePredicate.PredicateExpression, out queryInExpression, out flag))
				{
					predicateFullyDecomposed = false;
					canBeComposedWithPrefiltersOnly = false;
					return null;
				}
				int num = QdmExpressionBuilder.GetReferencedIdentityFields(queryInExpression).Count<IEdmFieldInstance>();
				if (num > singlePredicate.FilterIdentityTargetFieldList.Count)
				{
					throw new InvalidOperationException("The query filters contain multiple multi-entity filters, and one of those has a subset of the expressions filtered in the other.");
				}
				IDictionary<EntitySet, List<QueryExpression>> dictionary = QdmFilterBuilder.QdmFiltersBuilder.DecomposeMultiEntitySetInExpression(entitySets, queryInExpression, flag, daxCapabilities, out canBeComposedWithPrefiltersOnly, out predicateFullyDecomposed);
				if (num < singlePredicate.FilterIdentityTargetFieldList.Count)
				{
					predicateFullyDecomposed = false;
				}
				return dictionary;
			}

			// Token: 0x06001E0D RID: 7693 RVA: 0x00052A14 File Offset: 0x00050C14
			private static IDictionary<IConceptualEntity, List<QueryExpression>> DecomposeMultiEntityPredicate(ISet<IConceptualEntity> entities, QdmFilterPredicate singlePredicate, DaxCapabilities daxCapabilities, out bool canBeComposedWithPrefiltersOnly, out bool predicateFullyDecomposed)
			{
				QueryInExpression queryInExpression;
				bool flag;
				if (!QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInExpression>(singlePredicate.PredicateExpression, out queryInExpression, out flag))
				{
					predicateFullyDecomposed = false;
					canBeComposedWithPrefiltersOnly = false;
					return null;
				}
				int num = QdmExpressionBuilder.GetReferencedIdentityColumns(queryInExpression).Count<IConceptualColumn>();
				if (num > singlePredicate.FilterIdentityTargetColumnList.Count)
				{
					throw new InvalidOperationException("The query filters contain multiple multi-entity filters, and one of those has a subset of the expressions filtered in the other.");
				}
				IDictionary<IConceptualEntity, List<QueryExpression>> dictionary = QdmFilterBuilder.QdmFiltersBuilder.DecomposeMultiEntityInExpression(entities, queryInExpression, flag, daxCapabilities, out canBeComposedWithPrefiltersOnly, out predicateFullyDecomposed);
				if (num < singlePredicate.FilterIdentityTargetColumnList.Count)
				{
					predicateFullyDecomposed = false;
				}
				return dictionary;
			}

			// Token: 0x06001E0E RID: 7694 RVA: 0x00052A80 File Offset: 0x00050C80
			private static IDictionary<EntitySet, List<QueryExpression>> DecomposeMultiEntitySetInExpression(ISet<EntitySet> entities, QueryInExpression queryInExpression, bool isNegated, DaxCapabilities daxCapabilities, out bool canBeComposedWithPrefiltersOnly, out bool predicateFullyDecomposed)
			{
				IDictionary<EntitySet, List<QueryExpression>> dictionary = null;
				if (!isNegated)
				{
					dictionary = QdmFilterBuilder.QdmFiltersBuilder.ProjectEntitySetPredicates(entities, queryInExpression);
					if (queryInExpression.HasOneTuple)
					{
						bool flag = daxCapabilities.IsSupported(DaxFunctionKind.TreatAs);
						predicateFullyDecomposed = !flag;
						canBeComposedWithPrefiltersOnly = true;
						return dictionary;
					}
				}
				canBeComposedWithPrefiltersOnly = false;
				predicateFullyDecomposed = false;
				return dictionary;
			}

			// Token: 0x06001E0F RID: 7695 RVA: 0x00052AC0 File Offset: 0x00050CC0
			private static IDictionary<IConceptualEntity, List<QueryExpression>> DecomposeMultiEntityInExpression(ISet<IConceptualEntity> entities, QueryInExpression queryInExpression, bool isNegated, DaxCapabilities daxCapabilities, out bool canBeComposedWithPrefiltersOnly, out bool predicateFullyDecomposed)
			{
				IDictionary<IConceptualEntity, List<QueryExpression>> dictionary = null;
				if (!isNegated)
				{
					dictionary = QdmFilterBuilder.QdmFiltersBuilder.ProjectEntityPredicates(entities, queryInExpression);
					if (queryInExpression.HasOneTuple)
					{
						bool flag = daxCapabilities.IsSupported(DaxFunctionKind.TreatAs);
						predicateFullyDecomposed = !flag;
						canBeComposedWithPrefiltersOnly = true;
						return dictionary;
					}
				}
				canBeComposedWithPrefiltersOnly = false;
				predicateFullyDecomposed = false;
				return dictionary;
			}

			// Token: 0x06001E10 RID: 7696 RVA: 0x00052B00 File Offset: 0x00050D00
			private static IDictionary<EntitySet, List<QueryExpression>> ProjectEntitySetPredicates(ISet<EntitySet> entitySets, QueryInExpression queryInExpression)
			{
				Dictionary<EntitySet, List<QueryExpression>> dictionary = new Dictionary<EntitySet, List<QueryExpression>>(entitySets.Count);
				IReadOnlyList<QueryExpression> exprs = queryInExpression.Expressions;
				IList<global::System.ValueTuple<int, EntitySet>> list = exprs.Select((QueryExpression e, int i) => new global::System.ValueTuple<int, EntitySet>(i, QdmExpressionBuilder.GetSingleEntitySet(e))).Evaluate<global::System.ValueTuple<int, EntitySet>>();
				using (IEnumerator<EntitySet> enumerator = entitySets.GetEnumerator())
				{
					Func<int, QueryExpression> <>9__3;
					while (enumerator.MoveNext())
					{
						EntitySet entitySet = enumerator.Current;
						List<int> entityFieldIdx = (from e in list
							where entitySet.Equals(e.Item2)
							select e.Item1).ToList<int>();
						IEnumerable<int> entityFieldIdx2 = entityFieldIdx;
						Func<int, QueryExpression> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = (int exprIndex) => exprs[exprIndex]);
						}
						List<QueryExpression> list2 = entityFieldIdx2.Select(func).ToList<QueryExpression>();
						if (list2.Any<QueryExpression>())
						{
							List<List<QueryExpression>> list3 = queryInExpression.Values.Select((IReadOnlyList<QueryExpression> v) => entityFieldIdx.Select((int exprIndex) => v[exprIndex]).ToList<QueryExpression>()).ToList<List<QueryExpression>>();
							QueryExpression queryExpression = list2.In(list3, queryInExpression.IsStrict);
							dictionary.Add(entitySet, queryExpression);
						}
					}
				}
				return dictionary;
			}

			// Token: 0x06001E11 RID: 7697 RVA: 0x00052C6C File Offset: 0x00050E6C
			private static IDictionary<IConceptualEntity, List<QueryExpression>> ProjectEntityPredicates(ISet<IConceptualEntity> entities, QueryInExpression queryInExpression)
			{
				Dictionary<IConceptualEntity, List<QueryExpression>> dictionary = new Dictionary<IConceptualEntity, List<QueryExpression>>(entities.Count, ConceptualEntityExtensionAwareEqualityComparer.Instance);
				IReadOnlyList<QueryExpression> exprs = queryInExpression.Expressions;
				IList<global::System.ValueTuple<int, IConceptualEntity>> list = exprs.Select((QueryExpression e, int i) => new global::System.ValueTuple<int, IConceptualEntity>(i, QdmExpressionBuilder.GetSingleEntity(e))).Evaluate<global::System.ValueTuple<int, IConceptualEntity>>();
				using (IEnumerator<IConceptualEntity> enumerator = entities.GetEnumerator())
				{
					Func<int, QueryExpression> <>9__3;
					while (enumerator.MoveNext())
					{
						IConceptualEntity entity = enumerator.Current;
						List<int> entityColumnIdx = (from e in list
							where ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(entity, e.Item2)
							select e.Item1).ToList<int>();
						IEnumerable<int> entityColumnIdx2 = entityColumnIdx;
						Func<int, QueryExpression> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = (int exprIndex) => exprs[exprIndex]);
						}
						List<QueryExpression> list2 = entityColumnIdx2.Select(func).ToList<QueryExpression>();
						if (list2.Any<QueryExpression>())
						{
							List<List<QueryExpression>> list3 = queryInExpression.Values.Select((IReadOnlyList<QueryExpression> v) => entityColumnIdx.Select((int exprIndex) => v[exprIndex]).ToList<QueryExpression>()).ToList<List<QueryExpression>>();
							QueryExpression queryExpression = list2.In(list3, queryInExpression.IsStrict);
							dictionary.Add(entity, queryExpression);
						}
					}
				}
				return dictionary;
			}

			// Token: 0x06001E12 RID: 7698 RVA: 0x00052DDC File Offset: 0x00050FDC
			private static void MergeInto<K, V>(IDictionary<K, List<V>> destination, IEnumerable<KeyValuePair<K, List<V>>> source)
			{
				foreach (KeyValuePair<K, List<V>> keyValuePair in source)
				{
					K key = keyValuePair.Key;
					List<V> value = keyValuePair.Value;
					if (value.Count > 0)
					{
						List<V> list;
						if (!destination.TryGetValue(key, out list))
						{
							list = new List<V>(value.Count);
							destination[key] = list;
						}
						list.AddRange(value);
					}
				}
			}

			// Token: 0x06001E13 RID: 7699 RVA: 0x00052E60 File Offset: 0x00051060
			private static void MergeInto<K, V>(IDictionary<K, List<V>> destination, K key, V value)
			{
				List<V> list;
				if (!destination.TryGetValue(key, out list))
				{
					list = new List<V>(1);
					destination[key] = list;
				}
				list.Add(value);
			}

			// Token: 0x0400118D RID: 4493
			private readonly DaxCapabilities _daxCapabilities;

			// Token: 0x0400118E RID: 4494
			private readonly IDataComparer _comparer;

			// Token: 0x0400118F RID: 4495
			private readonly QdmSingleEntityFilterBuilder _singleEntityFilterBuilder;

			// Token: 0x04001190 RID: 4496
			private readonly QdmMultipleEntitiesFilterBuilder _multiEntityFilterBuilder;

			// Token: 0x04001191 RID: 4497
			private readonly bool _useConceptualSchema;
		}
	}
}
