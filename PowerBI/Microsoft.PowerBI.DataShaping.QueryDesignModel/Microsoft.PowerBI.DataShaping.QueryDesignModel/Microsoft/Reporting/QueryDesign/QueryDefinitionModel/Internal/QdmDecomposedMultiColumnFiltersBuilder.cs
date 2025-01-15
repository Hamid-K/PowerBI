using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000FA RID: 250
	internal sealed class QdmDecomposedMultiColumnFiltersBuilder
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x00026191 File Offset: 0x00024391
		internal QdmDecomposedMultiColumnFiltersBuilder(IConceptualModel model, IConceptualSchema schema, IDataComparer dataComparer, bool clearDefaultValues, bool useConceptualSchema)
		{
			this._model = model;
			this._schema = schema;
			this._dataComparer = dataComparer;
			this._clearDefaultValues = clearDefaultValues;
			this._useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000261C0 File Offset: 0x000243C0
		internal bool ConjunctionShouldBeDecomposedToColumnFilters(QueryExpression expression)
		{
			QueryInExpression queryInExpression = expression as QueryInExpression;
			return queryInExpression != null && queryInExpression.HasOneTuple && !queryInExpression.CanBePreservedAsTuples;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000261EC File Offset: 0x000243EC
		internal QueryExpression DecomposeConjunctionToColumnFilters(QueryExpression expression, JoinPredicateBehavior predicateBehavior, ISet<IEdmFieldInstance> predicatesGroupingFields, ISet<IConceptualColumn> predicatesGroupingColumns, BuildFilterExpression buildFilter)
		{
			QueryInExpression queryInExpression = expression as QueryInExpression;
			if (queryInExpression == null)
			{
				return expression;
			}
			queryInExpression = QueryAlgorithms.DedupeValues(queryInExpression, this._dataComparer);
			Dictionary<IEdmFieldInstance, List<int>> dictionary = (this._useConceptualSchema ? null : QdmDecomposedMultiColumnFiltersBuilder.CreateFieldInfoMap(queryInExpression));
			Dictionary<IConceptualColumn, List<int>> dictionary2 = (this._useConceptualSchema ? QdmDecomposedMultiColumnFiltersBuilder.CreateColumnInfoMap(queryInExpression) : null);
			List<QueryExpression> list = this.DecomposeInExpressionByField(queryInExpression, buildFilter, dictionary, dictionary2);
			Dictionary<QueryExpression, QueryExpression> dictionary3;
			return this.BuildGroupAndJoinForPredicate(predicatesGroupingFields, predicatesGroupingColumns, predicateBehavior, list, out dictionary3).Expression;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x00026254 File Offset: 0x00024454
		internal QueryTable BuildGroupAndJoinForPredicate(ISet<IEdmFieldInstance> predicatesGroupingFields, ISet<IConceptualColumn> predicatesGroupingColumns, JoinPredicateBehavior predicateBehavior, IReadOnlyList<QueryExpression> prefilters, out Dictionary<QueryExpression, QueryExpression> predicateGroupingFieldToColumnReferenceMapping)
		{
			GroupAndJoinTableBuilder groupAndJoinTableBuilder = new GroupAndJoinTableBuilder(this._model, this._schema, true, this._useConceptualSchema, null, predicateBehavior, null);
			groupAndJoinTableBuilder.SetAllowEmptyGroups();
			IQueryTableGroupBuilder queryTableGroupBuilder = groupAndJoinTableBuilder.AddGroup();
			predicateGroupingFieldToColumnReferenceMapping = new Dictionary<QueryExpression, QueryExpression>((predicatesGroupingColumns != null) ? predicatesGroupingColumns.Count : predicatesGroupingFields.Count, QueryExpression.Comparer);
			if (this._useConceptualSchema)
			{
				using (IEnumerator<IConceptualColumn> enumerator = predicatesGroupingColumns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IConceptualColumn conceptualColumn = enumerator.Current;
						QueryFieldExpression queryFieldExpression = conceptualColumn.QdmReference();
						QueryTableColumn queryTableColumn = queryTableGroupBuilder.AddGroupKey(queryFieldExpression, conceptualColumn.EdmName);
						predicateGroupingFieldToColumnReferenceMapping.Add(queryFieldExpression, queryTableColumn.QdmReference());
					}
					goto IL_00F6;
				}
			}
			foreach (IEdmFieldInstance edmFieldInstance in predicatesGroupingFields)
			{
				QueryFieldExpression queryFieldExpression2 = edmFieldInstance.QdmReference();
				QueryTableColumn queryTableColumn2 = queryTableGroupBuilder.AddGroupKey(queryFieldExpression2, edmFieldInstance.Field.Name);
				predicateGroupingFieldToColumnReferenceMapping.Add(queryFieldExpression2, queryTableColumn2.QdmReference());
			}
			IL_00F6:
			foreach (QueryExpression queryExpression in this.BuildContextTables(predicatesGroupingFields, predicatesGroupingColumns, prefilters))
			{
				groupAndJoinTableBuilder.AddContextTable(queryExpression, false);
			}
			return groupAndJoinTableBuilder.ToQueryTable();
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000263C4 File Offset: 0x000245C4
		private IEnumerable<QueryExpression> BuildContextTables(ISet<IEdmFieldInstance> filteredFields, ISet<IConceptualColumn> filteredColumns, IEnumerable<QueryExpression> prefilters)
		{
			if (!this._clearDefaultValues)
			{
				return prefilters;
			}
			List<QueryExpression> list = null;
			if (this._useConceptualSchema)
			{
				Dictionary<IConceptualEntity, HashSet<IConceptualColumn>> dictionary = new Dictionary<IConceptualEntity, HashSet<IConceptualColumn>>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
				foreach (IConceptualColumn conceptualColumn in filteredColumns)
				{
					IConceptualEntity entity = conceptualColumn.Entity;
					dictionary.Add(entity, conceptualColumn);
				}
				using (Dictionary<IConceptualEntity, HashSet<IConceptualColumn>>.Enumerator enumerator2 = dictionary.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<IConceptualEntity, HashSet<IConceptualColumn>> keyValuePair = enumerator2.Current;
						IEnumerable<IConceptualColumn> identityColumnsWithDefaultValues = QdmExpressionBuilder.GetIdentityColumnsWithDefaultValues(keyValuePair.Value.GetColumnsForDefaultGroupAndFilterExclusions(this._schema.GetFieldRelationshipAnnotations(), this._schema.GetColumnGroupingAnnotations(), true));
						if (!identityColumnsWithDefaultValues.IsNullOrEmpty<IConceptualColumn>())
						{
							List<IConceptualColumn> list2 = identityColumnsWithDefaultValues.ToList<IConceptualColumn>();
							list2.Sort((IConceptualColumn value1, IConceptualColumn value2) => value1.EdmName.CompareTo(value2.EdmName));
							QueryAllExpression queryAllExpression = keyValuePair.Key.All(list2);
							if (list == null)
							{
								list = new List<QueryExpression>(prefilters);
							}
							list.Add(queryAllExpression);
						}
					}
					goto IL_021C;
				}
			}
			Dictionary<EntitySet, HashSet<IEdmFieldInstance>> dictionary2 = new Dictionary<EntitySet, HashSet<IEdmFieldInstance>>();
			foreach (IEdmFieldInstance edmFieldInstance in filteredFields)
			{
				EntitySet entity2 = edmFieldInstance.Entity;
				dictionary2.Add(entity2, edmFieldInstance);
			}
			foreach (KeyValuePair<EntitySet, HashSet<IEdmFieldInstance>> keyValuePair2 in dictionary2)
			{
				IEnumerable<IEdmFieldInstance> identityFieldsWithDefaultValues = QdmExpressionBuilder.GetIdentityFieldsWithDefaultValues(keyValuePair2.Value.GetFieldsForDefaultGroupAndFilterExclusions(true));
				if (!identityFieldsWithDefaultValues.IsNullOrEmpty<IEdmFieldInstance>())
				{
					List<EdmField> list3 = identityFieldsWithDefaultValues.Select((IEdmFieldInstance f) => f.Field).ToList<EdmField>();
					list3.Sort((EdmField value1, EdmField value2) => value1.Name.CompareTo(value2.Name));
					QueryAllExpression queryAllExpression2 = keyValuePair2.Key.All(list3, null, null);
					if (list == null)
					{
						list = new List<QueryExpression>(prefilters);
					}
					list.Add(queryAllExpression2);
				}
			}
			IL_021C:
			IEnumerable<QueryExpression> enumerable = list;
			return enumerable ?? prefilters;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0002662C File Offset: 0x0002482C
		private List<QueryExpression> DecomposeInExpressionByField(QueryInExpression inExpression, BuildFilterExpression buildFilter, Dictionary<IEdmFieldInstance, List<int>> fieldInfoMap, Dictionary<IConceptualColumn, List<int>> columnInfoMap)
		{
			List<QueryExpression> list = new List<QueryExpression>(inExpression.Expressions.Count);
			if (this._useConceptualSchema)
			{
				HashSet<IConceptualColumn> hashSet = new HashSet<IConceptualColumn>();
				IOrderedEnumerable<KeyValuePair<IConceptualColumn, List<int>>> orderedEnumerable = columnInfoMap.OrderBy((KeyValuePair<IConceptualColumn, List<int>> kvp) => kvp.Key.EdmName);
				for (int i = 0; i < orderedEnumerable.Count<KeyValuePair<IConceptualColumn, List<int>>>(); i++)
				{
					KeyValuePair<IConceptualColumn, List<int>> keyValuePair = orderedEnumerable.ElementAt(i);
					IConceptualColumn key = keyValuePair.Key;
					if (hashSet.Add(key))
					{
						HashSet<IConceptualColumn> hashSet2 = new HashSet<IConceptualColumn> { keyValuePair.Key };
						List<QueryExpression> list2 = new List<QueryExpression>();
						List<QueryExpression> list3 = new List<QueryExpression>();
						this.TryAddColumn(key, list2, list3, inExpression, columnInfoMap);
						foreach (IConceptualColumn conceptualColumn in key.Grouping.IdentityColumns)
						{
							if (conceptualColumn != key && this.TryAddColumn(conceptualColumn, list2, list3, inExpression, columnInfoMap))
							{
								hashSet2.Add(conceptualColumn);
								hashSet.Add(conceptualColumn);
							}
						}
						QueryInExpression queryInExpression = list2.In(new List<QueryExpression>[] { list3 }, inExpression.IsStrict);
						QueryExpression queryExpression = buildFilter(null, null, queryInExpression, key.Entity, hashSet2);
						list.Add(queryExpression);
					}
				}
			}
			else
			{
				HashSet<IEdmFieldInstance> hashSet3 = new HashSet<IEdmFieldInstance>();
				foreach (KeyValuePair<IEdmFieldInstance, List<int>> keyValuePair2 in fieldInfoMap.OrderBy((KeyValuePair<IEdmFieldInstance, List<int>> kvp) => kvp.Key.Field.Name))
				{
					IEdmFieldInstance key2 = keyValuePair2.Key;
					if (hashSet3.Add(key2))
					{
						HashSet<IEdmFieldInstance> hashSet4 = new HashSet<IEdmFieldInstance> { keyValuePair2.Key };
						List<QueryExpression> list4 = new List<QueryExpression>();
						List<QueryExpression> list5 = new List<QueryExpression>();
						this.TryAddField(key2, list4, list5, inExpression, fieldInfoMap);
						foreach (EdmField edmField in key2.Field.Grouping.IdentityFields)
						{
							EdmFieldInstance edmFieldInstance = key2.Entity.FieldInstance(edmField);
							if (edmFieldInstance.Field != key2.Field && this.TryAddField(edmFieldInstance, list4, list5, inExpression, fieldInfoMap))
							{
								hashSet4.Add(edmFieldInstance);
								hashSet3.Add(edmFieldInstance);
							}
						}
						QueryInExpression queryInExpression2 = list4.In(new List<QueryExpression>[] { list5 }, inExpression.IsStrict);
						QueryExpression queryExpression2 = buildFilter(key2.Entity, hashSet4, queryInExpression2, null, null);
						list.Add(queryExpression2);
					}
				}
			}
			return list;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00026940 File Offset: 0x00024B40
		private static Dictionary<IEdmFieldInstance, List<int>> CreateFieldInfoMap(QueryInExpression inExpression)
		{
			Dictionary<IEdmFieldInstance, List<int>> dictionary = new Dictionary<IEdmFieldInstance, List<int>>(inExpression.Expressions.Count);
			for (int i = 0; i < inExpression.Expressions.Count; i++)
			{
				IEdmFieldInstance referencedModelField = inExpression.Expressions[i].GetReferencedModelField();
				if (referencedModelField.IsValid)
				{
					List<int> list;
					if (!dictionary.TryGetValue(referencedModelField, out list))
					{
						list = new List<int>();
						dictionary.Add(referencedModelField, list);
					}
					list.Add(i);
				}
			}
			return dictionary;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000269B0 File Offset: 0x00024BB0
		private static Dictionary<IConceptualColumn, List<int>> CreateColumnInfoMap(QueryInExpression inExpression)
		{
			Dictionary<IConceptualColumn, List<int>> dictionary = new Dictionary<IConceptualColumn, List<int>>(inExpression.Expressions.Count);
			for (int i = 0; i < inExpression.Expressions.Count; i++)
			{
				IConceptualColumn referencedModelColumn = inExpression.Expressions[i].GetReferencedModelColumn();
				if (referencedModelColumn != null)
				{
					List<int> list;
					if (!dictionary.TryGetValue(referencedModelColumn, out list))
					{
						list = new List<int>();
						dictionary.Add(referencedModelColumn, list);
					}
					list.Add(i);
				}
			}
			return dictionary;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00026A1C File Offset: 0x00024C1C
		private bool TryAddField(IEdmFieldInstance field, List<QueryExpression> expressions, List<QueryExpression> valueTuple, QueryInExpression rootInExpression, Dictionary<IEdmFieldInstance, List<int>> fieldToPositionMap)
		{
			List<int> list;
			if (!fieldToPositionMap.TryGetValue(field, out list))
			{
				return false;
			}
			foreach (int num in list)
			{
				expressions.Add(rootInExpression.Expressions[num]);
				valueTuple.Add(rootInExpression.Values.Single("Expected only 1 tuple of values for decomposing In filter into GroupAndJoin", Array.Empty<string>())[num]);
			}
			return true;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x00026AA8 File Offset: 0x00024CA8
		private bool TryAddColumn(IConceptualColumn column, List<QueryExpression> expressions, List<QueryExpression> valueTuple, QueryInExpression rootInExpression, Dictionary<IConceptualColumn, List<int>> columnToPositionMap)
		{
			List<int> list;
			if (!columnToPositionMap.TryGetValue(column, out list))
			{
				return false;
			}
			foreach (int num in list)
			{
				expressions.Add(rootInExpression.Expressions[num]);
				valueTuple.Add(rootInExpression.Values.Single("Expected only 1 tuple of values for decomposing In filter into GroupAndJoin", Array.Empty<string>())[num]);
			}
			return true;
		}

		// Token: 0x040009CC RID: 2508
		private readonly IConceptualModel _model;

		// Token: 0x040009CD RID: 2509
		private readonly IConceptualSchema _schema;

		// Token: 0x040009CE RID: 2510
		private readonly IDataComparer _dataComparer;

		// Token: 0x040009CF RID: 2511
		private readonly bool _clearDefaultValues;

		// Token: 0x040009D0 RID: 2512
		private readonly bool _useConceptualSchema;
	}
}
