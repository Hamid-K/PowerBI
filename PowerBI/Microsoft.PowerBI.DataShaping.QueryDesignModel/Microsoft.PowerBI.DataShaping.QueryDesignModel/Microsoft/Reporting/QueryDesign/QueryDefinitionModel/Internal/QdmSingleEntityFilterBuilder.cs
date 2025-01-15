using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000102 RID: 258
	internal sealed class QdmSingleEntityFilterBuilder
	{
		// Token: 0x06000EDA RID: 3802 RVA: 0x00027EC4 File Offset: 0x000260C4
		public QdmSingleEntityFilterBuilder(QdmDecomposedMultiColumnFiltersBuilder multiColumnFilterBuilder, DaxCapabilities daxCapabilities, IDataComparer comparer, ScanKind preferredScanKind, CancellationToken cancellationToken, bool clearDefaultValues, bool useConceptualSchema, IConceptualSchema schema)
		{
			this._daxCapabilities = daxCapabilities;
			this._comparer = comparer;
			this._preferredScanKind = preferredScanKind;
			this._cancellationToken = cancellationToken;
			this._clearDefaultValues = clearDefaultValues;
			this._multiColumnFilterBuilder = multiColumnFilterBuilder;
			this._useConceptualSchema = useConceptualSchema;
			this._schema = schema;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00027F14 File Offset: 0x00026114
		public List<QueryExpression> BuildFilterExpressionsForEntities(IDictionary<EntitySet, List<QueryExpression>> filterPredicatesByEntitySet, IDictionary<EntitySet, ISet<IEdmFieldInstance>> filterGroupingFields, IDictionary<IConceptualEntity, List<QueryExpression>> filterPredicatesByEntity = null, IDictionary<IConceptualEntity, ISet<IConceptualColumn>> filterGroupingColumns = null)
		{
			List<QueryExpression> list = new List<QueryExpression>(this._useConceptualSchema ? filterPredicatesByEntity.Count : filterPredicatesByEntitySet.Count);
			if (this._useConceptualSchema)
			{
				using (IEnumerator<KeyValuePair<IConceptualEntity, List<QueryExpression>>> enumerator = filterPredicatesByEntity.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<IConceptualEntity, List<QueryExpression>> keyValuePair = enumerator.Current;
						List<QueryExpression> value = keyValuePair.Value;
						IConceptualEntity key = keyValuePair.Key;
						ISet<IConceptualColumn> set = filterGroupingColumns[key];
						List<QueryExpression> list2 = this.BuildFilterExpressionsForEntity(null, key, null, set, value);
						list.AddRange(list2);
					}
					return list;
				}
			}
			foreach (KeyValuePair<EntitySet, List<QueryExpression>> keyValuePair2 in filterPredicatesByEntitySet)
			{
				List<QueryExpression> value2 = keyValuePair2.Value;
				EntitySet key2 = keyValuePair2.Key;
				ISet<IEdmFieldInstance> set2 = filterGroupingFields[keyValuePair2.Key];
				List<QueryExpression> list3 = this.BuildFilterExpressionsForEntity(key2, null, set2, null, value2);
				list.AddRange(list3);
			}
			return list;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002801C File Offset: 0x0002621C
		public IReadOnlyList<QueryExpression> BuildFilterExpressionsForEntity(EntitySet entitySet, IGrouping<ReadOnlyOrderedHashSet<IEdmFieldInstance>, QdmFilterPredicate> predicatesByGroupingFields, IConceptualEntity entity = null, IGrouping<ReadOnlyOrderedHashSet<IConceptualColumn>, QdmFilterPredicate> predicatesByGroupingColumns = null)
		{
			ReadOnlyOrderedHashSet<IEdmFieldInstance> readOnlyOrderedHashSet = ((predicatesByGroupingFields != null) ? predicatesByGroupingFields.Key : null);
			ReadOnlyOrderedHashSet<IConceptualColumn> readOnlyOrderedHashSet2 = ((predicatesByGroupingColumns != null) ? predicatesByGroupingColumns.Key : null);
			IReadOnlyList<QueryExpression> readOnlyList;
			if (!this._useConceptualSchema)
			{
				readOnlyList = predicatesByGroupingFields.Select((QdmFilterPredicate p) => p.PredicateExpression).EvaluateReadOnly<QueryExpression>();
			}
			else
			{
				readOnlyList = predicatesByGroupingColumns.Select((QdmFilterPredicate p) => p.PredicateExpression).EvaluateReadOnly<QueryExpression>();
			}
			IReadOnlyList<QueryExpression> readOnlyList2 = readOnlyList;
			return this.BuildFilterExpressionsForEntity(entitySet, entity, readOnlyOrderedHashSet, readOnlyOrderedHashSet2, readOnlyList2);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000280B0 File Offset: 0x000262B0
		private List<QueryExpression> BuildFilterExpressionsForEntity(EntitySet entitySet, IConceptualEntity entity, ISet<IEdmFieldInstance> groupingFields, ISet<IConceptualColumn> groupingColumns, IReadOnlyList<QueryExpression> predicates)
		{
			List<QueryExpression> list = new List<QueryExpression>();
			List<QueryExpression> list2 = new List<QueryExpression>();
			foreach (QueryExpression queryExpression in predicates)
			{
				if (queryExpression is QueryTreatAsExpression)
				{
					list.Add(queryExpression);
				}
				else if (this._multiColumnFilterBuilder.ConjunctionShouldBeDecomposedToColumnFilters(queryExpression))
				{
					QueryExpression queryExpression2 = this._multiColumnFilterBuilder.DecomposeConjunctionToColumnFilters(queryExpression, JoinPredicateBehavior.None, groupingFields, groupingColumns, new BuildFilterExpression(this.BuildSingleFilter));
					list.Add(queryExpression2);
				}
				else
				{
					list2.Add(queryExpression);
				}
			}
			if (!list2.IsNullOrEmpty<QueryExpression>())
			{
				QueryExpression queryExpression3 = this.BuildSingleFilter(entitySet, groupingFields, list2.AndAll(true), entity, groupingColumns);
				list.Add(queryExpression3);
			}
			return list;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00028174 File Offset: 0x00026374
		internal QueryExpression BuildSingleFilter(EntitySet entitySet, ISet<IEdmFieldInstance> groupingFields, QueryExpression predicate, IConceptualEntity entity, ISet<IConceptualColumn> groupingColumns)
		{
			QueryExpression queryExpression;
			if (QdmTreatAsFilters.TryHandlePredicateUsingTreatAs(predicate, groupingFields, groupingColumns, this._daxCapabilities, this._comparer, this._cancellationToken, this._useConceptualSchema, out queryExpression))
			{
				return queryExpression;
			}
			return this.TableScanFilter(entitySet, entity, groupingFields, groupingColumns, predicate);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000281B8 File Offset: 0x000263B8
		private QueryFilterExpression TableScanFilter(EntitySet entitySet, IConceptualEntity entity, IEnumerable<IEdmFieldInstance> filteredFields, IEnumerable<IConceptualColumn> filteredColumns, QueryExpression predicate)
		{
			QueryExpression queryExpression;
			if (this._useConceptualSchema)
			{
				bool flag;
				ScanKind scanKind = this.DetermineTableScanKind(filteredColumns, out flag);
				if (scanKind == ScanKind.IndependentFilterContextIncludeBlankRow && flag)
				{
					queryExpression = entity.All(filteredColumns.EvaluateReadOnly<IConceptualColumn>());
				}
				else
				{
					queryExpression = filteredFields.QdmFilterGroupBy(this._schema, this._useConceptualSchema, filteredColumns, scanKind);
				}
			}
			else
			{
				bool flag;
				ScanKind scanKind2 = this.DetermineTableScanKind(filteredFields, out flag);
				if (scanKind2 == ScanKind.IndependentFilterContextIncludeBlankRow && flag)
				{
					queryExpression = entitySet.All(filteredFields.Select((IEdmFieldInstance f) => f.Field).EvaluateReadOnly<EdmField>(), null, null);
				}
				else
				{
					queryExpression = filteredFields.QdmFilterGroupBy(this._schema, this._useConceptualSchema, null, scanKind2);
				}
			}
			QueryExpressionBinding queryExpressionBinding = queryExpression.BindAs(QdmNames.Filtered(this._useConceptualSchema ? entity.EdmName : entitySet.Name));
			QueryExpression queryExpression2 = QdmExpressionBuilder.RewriteEntityPlaceholders(predicate, queryExpressionBinding.Variable, new ConceptualRowType[] { this._useConceptualSchema ? entity.GetExtensionAwareResultType().RowType : entitySet.ElementType.ConceptualType });
			return queryExpressionBinding.Filter(queryExpression2);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000282CC File Offset: 0x000264CC
		private ScanKind DetermineTableScanKind(IEnumerable<IEdmFieldInstance> filteredFields, out bool useIndependentScanToClearDefaultValues)
		{
			useIndependentScanToClearDefaultValues = false;
			if (this._preferredScanKind == ScanKind.IndependentFilterContextIncludeBlankRow || !this._clearDefaultValues)
			{
				return this._preferredScanKind;
			}
			IEnumerable<IEdmFieldInstance> identityFieldsWithDefaultValues = QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(filteredFields.GetFieldsForDefaultGroupAndFilterExclusions(true));
			useIndependentScanToClearDefaultValues = this._clearDefaultValues && !identityFieldsWithDefaultValues.IsNullOrEmpty<IEdmFieldInstance>();
			if (!useIndependentScanToClearDefaultValues)
			{
				return this._preferredScanKind;
			}
			return ScanKind.IndependentFilterContextIncludeBlankRow;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00028324 File Offset: 0x00026524
		private ScanKind DetermineTableScanKind(IEnumerable<IConceptualColumn> filteredColumns, out bool useIndependentScanToClearDefaultValues)
		{
			useIndependentScanToClearDefaultValues = false;
			if (this._preferredScanKind == ScanKind.IndependentFilterContextIncludeBlankRow || !this._clearDefaultValues)
			{
				return this._preferredScanKind;
			}
			IEnumerable<IConceptualColumn> identityColumnsWithDefaultValues = QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(filteredColumns.GetColumnsForDefaultGroupAndFilterExclusions(this._schema.GetFieldRelationshipAnnotations(), this._schema.GetColumnGroupingAnnotations(), true));
			useIndependentScanToClearDefaultValues = this._clearDefaultValues && !identityColumnsWithDefaultValues.IsNullOrEmpty<IConceptualColumn>();
			if (!useIndependentScanToClearDefaultValues)
			{
				return this._preferredScanKind;
			}
			return ScanKind.IndependentFilterContextIncludeBlankRow;
		}

		// Token: 0x040009E7 RID: 2535
		private readonly QdmDecomposedMultiColumnFiltersBuilder _multiColumnFilterBuilder;

		// Token: 0x040009E8 RID: 2536
		private readonly DaxCapabilities _daxCapabilities;

		// Token: 0x040009E9 RID: 2537
		private readonly IDataComparer _comparer;

		// Token: 0x040009EA RID: 2538
		private readonly ScanKind _preferredScanKind;

		// Token: 0x040009EB RID: 2539
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040009EC RID: 2540
		private readonly IConceptualSchema _schema;

		// Token: 0x040009ED RID: 2541
		private readonly bool _clearDefaultValues;

		// Token: 0x040009EE RID: 2542
		private readonly bool _useConceptualSchema;
	}
}
