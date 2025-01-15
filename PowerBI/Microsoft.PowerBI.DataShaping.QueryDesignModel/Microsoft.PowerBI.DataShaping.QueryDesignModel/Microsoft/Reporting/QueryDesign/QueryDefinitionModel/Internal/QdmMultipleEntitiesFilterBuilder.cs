using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000100 RID: 256
	internal sealed class QdmMultipleEntitiesFilterBuilder
	{
		// Token: 0x06000EBA RID: 3770 RVA: 0x00027A9E File Offset: 0x00025C9E
		internal QdmMultipleEntitiesFilterBuilder(QdmDecomposedMultiColumnFiltersBuilder multiColumnFilterBuilder, QdmSingleEntityFilterBuilder singleEntityFilterBuilder, IConceptualSchema schema, IDataComparer dataComparer, CancellationToken cancellationToken, bool useConceptualSchema)
		{
			this._multiColumnFilterBuilder = multiColumnFilterBuilder;
			this._singleEntityFilterBuilder = singleEntityFilterBuilder;
			this._schema = schema;
			this._dataComparer = dataComparer;
			this._cancellationToken = cancellationToken;
			this._useConceptualSchema = useConceptualSchema;
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00027AD4 File Offset: 0x00025CD4
		internal IReadOnlyList<QueryExpression> BuildMultiEntityFilters(ISet<IEdmFieldInstance> predicatesGroupingFields, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Predicate", "CanBeComposedWithPrefiltersOnly" })] IReadOnlyList<global::System.ValueTuple<QdmFilterPredicate, bool>> multiEntityPredicates, IDictionary<EntitySet, List<QueryExpression>> prefiltersByEntitySet, IDictionary<EntitySet, ISet<IEdmFieldInstance>> filteredFieldsByEntitySet, DaxCapabilities daxCapabilities, ISet<IConceptualColumn> predicatesGroupingColumns, IDictionary<IConceptualEntity, List<QueryExpression>> prefiltersByEntity = null, IDictionary<IConceptualEntity, ISet<IConceptualColumn>> filteredColumnsByEntity = null)
		{
			List<QueryExpression> list = this._singleEntityFilterBuilder.BuildFilterExpressionsForEntities(prefiltersByEntitySet, filteredFieldsByEntitySet, prefiltersByEntity, filteredColumnsByEntity);
			List<QueryExpression> list2 = new List<QueryExpression>(1);
			List<QueryExpression> list3 = null;
			foreach (global::System.ValueTuple<QdmFilterPredicate, bool> valueTuple in multiEntityPredicates)
			{
				QueryExpression queryExpression;
				if (QdmTreatAsFilters.TryHandlePredicateUsingTreatAs(valueTuple.Item1.PredicateExpression, predicatesGroupingFields, predicatesGroupingColumns, daxCapabilities, this._dataComparer, this._cancellationToken, this._useConceptualSchema, out queryExpression))
				{
					list2.Add(queryExpression);
				}
				else if (valueTuple.Item2 && this._multiColumnFilterBuilder.ConjunctionShouldBeDecomposedToColumnFilters(valueTuple.Item1.PredicateExpression))
				{
					list2.Add(this.Build(predicatesGroupingFields, predicatesGroupingColumns, valueTuple.Item1.PredicateExpression, list, true));
				}
				else
				{
					Util.AddToLazyList<QueryExpression>(ref list3, valueTuple.Item1.PredicateExpression);
				}
			}
			if (!list3.IsNullOrEmpty<QueryExpression>())
			{
				list2.Add(this.Build(predicatesGroupingFields, predicatesGroupingColumns, list3.AndAll(true), list, false));
			}
			return list2;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00027BE4 File Offset: 0x00025DE4
		private QueryExpression Build(ISet<IEdmFieldInstance> predicatesGroupingFields, ISet<IConceptualColumn> predicatesGroupingColumns, QueryExpression predicate, IReadOnlyList<QueryExpression> prefilters, bool skipPostFilters)
		{
			JoinPredicateBehavior joinPredicateBehavior = this.GetJoinPredicateBehavior(predicatesGroupingFields, predicatesGroupingColumns, predicate, prefilters);
			Dictionary<QueryExpression, QueryExpression> dictionary;
			QueryTable queryTable = this._multiColumnFilterBuilder.BuildGroupAndJoinForPredicate(predicatesGroupingFields, predicatesGroupingColumns, joinPredicateBehavior, prefilters, out dictionary);
			if (skipPostFilters)
			{
				return queryTable.Expression;
			}
			QueryExpressionBinding queryExpressionBinding = queryTable.Expression.BindAs(QdmNames.TupleFiltered());
			if (this._useConceptualSchema)
			{
				using (IEnumerator<IConceptualColumn> enumerator = predicatesGroupingColumns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IConceptualColumn conceptualColumn = enumerator.Current;
						QueryFieldExpression queryFieldExpression = conceptualColumn.QdmReference();
						dictionary[queryFieldExpression] = dictionary[queryFieldExpression].RewriteColumnReferences(queryTable.Columns, queryExpressionBinding.Variable);
					}
					goto IL_00E3;
				}
			}
			foreach (IEdmFieldInstance edmFieldInstance in predicatesGroupingFields)
			{
				QueryFieldExpression queryFieldExpression2 = edmFieldInstance.QdmReference();
				dictionary[queryFieldExpression2] = dictionary[queryFieldExpression2].RewriteColumnReferences(queryTable.Columns, queryExpressionBinding.Variable);
			}
			IL_00E3:
			QueryExpression queryExpression = predicate.ReplaceMultiple(dictionary, false);
			return queryExpressionBinding.Filter(queryExpression);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x00027D04 File Offset: 0x00025F04
		private JoinPredicateBehavior GetJoinPredicateBehavior(ISet<IEdmFieldInstance> predicatesGroupingFields, ISet<IConceptualColumn> predicatesGroupingColumns, QueryExpression predicate, IReadOnlyList<QueryExpression> prefilters)
		{
			QueryInExpression queryInExpression;
			bool flag;
			if (prefilters.IsNullOrEmpty<QueryExpression>() || !QueryFilterExpressionAnalyzer.TryExtractExpression<QueryInExpression>(predicate, out queryInExpression, out flag) || flag || (this._useConceptualSchema ? (!predicatesGroupingColumns.SetEquals(QdmExpressionBuilder.GetReferencedIdentityColumns(predicate))) : (!predicatesGroupingFields.SetEquals(QdmExpressionBuilder.GetReferencedIdentityFields(predicate)))))
			{
				return JoinPredicateBehavior.AutoPredicates | JoinPredicateBehavior.AutoPredicatesForFilters;
			}
			return JoinPredicateBehavior.None;
		}

		// Token: 0x040009DA RID: 2522
		private readonly QdmDecomposedMultiColumnFiltersBuilder _multiColumnFilterBuilder;

		// Token: 0x040009DB RID: 2523
		private readonly QdmSingleEntityFilterBuilder _singleEntityFilterBuilder;

		// Token: 0x040009DC RID: 2524
		private readonly IConceptualSchema _schema;

		// Token: 0x040009DD RID: 2525
		private readonly IDataComparer _dataComparer;

		// Token: 0x040009DE RID: 2526
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040009DF RID: 2527
		private readonly bool _useConceptualSchema;
	}
}
