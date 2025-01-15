using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x0200026D RID: 621
	internal sealed class QueryExistsFilterTranslator
	{
		// Token: 0x06001AD0 RID: 6864 RVA: 0x0004AB9C File Offset: 0x00048D9C
		public static IEnumerable<QueryExpression> Translate(IEnumerable<QueryExistsFilter> existsFilters)
		{
			existsFilters = QueryExistsFilterConsolidator.Consolidate(existsFilters);
			return existsFilters.Select((QueryExistsFilter f) => QueryExistsFilterTranslator.Translate(f));
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0004ABCC File Offset: 0x00048DCC
		private static QueryExpression Translate(QueryExistsFilter existsFilter)
		{
			QueryExpressionBinding queryExpressionBinding = null;
			QueryNamingContext queryNamingContext = new QueryNamingContext(null);
			IReadOnlyList<IConceptualEntity> targetEntities = existsFilter.TargetEntities;
			List<ImplicitJoinSecondaryTable> list = new List<ImplicitJoinSecondaryTable>((targetEntities != null) ? targetEntities.Count : existsFilter.TargetEntitySets.Count);
			List<KeyValuePair<string, QueryFieldExpression>> list2 = new List<KeyValuePair<string, QueryFieldExpression>>();
			if (existsFilter.TargetEntities != null)
			{
				queryExpressionBinding = existsFilter.ExistsEntity.Scan(false).BindAs("Primary");
				for (int i = 0; i < existsFilter.TargetEntities.Count; i++)
				{
					IConceptualEntity conceptualEntity = existsFilter.TargetEntities[i];
					List<KeyValuePair<string, QueryFieldExpression>> list3 = new List<KeyValuePair<string, QueryFieldExpression>>();
					QueryExpressionBinding queryExpressionBinding2 = conceptualEntity.Scan(false).BindAs("Secondary" + i.ToString());
					IEnumerable<IConceptualColumn> targetColumns = QueryExistsFilterTranslator.GetTargetColumns(conceptualEntity);
					QueryVariableReferenceExpression variable = queryExpressionBinding2.Variable;
					foreach (IConceptualColumn conceptualColumn in targetColumns)
					{
						string text = queryNamingContext.CreateAndRegisterUniqueName(conceptualColumn.EdmName);
						QueryFieldExpression queryFieldExpression = variable.Field(conceptualColumn);
						KeyValuePair<string, QueryFieldExpression> keyValuePair = new KeyValuePair<string, QueryFieldExpression>(text, queryFieldExpression);
						list3.Add(keyValuePair);
						list2.Add(keyValuePair);
					}
					list.Add(new ImplicitJoinSecondaryTable(queryExpressionBinding2, list3));
				}
			}
			else
			{
				queryExpressionBinding = existsFilter.ExistsEntitySet.Scan(false).BindAs("Primary");
				for (int j = 0; j < existsFilter.TargetEntitySets.Count; j++)
				{
					List<KeyValuePair<string, QueryFieldExpression>> list4 = new List<KeyValuePair<string, QueryFieldExpression>>();
					EntitySet entitySet = existsFilter.TargetEntitySets[j];
					QueryExpressionBinding queryExpressionBinding3 = entitySet.Scan(false).BindAs("Secondary" + j.ToString());
					IEnumerable<EdmFieldInstance> targetFields = QueryExistsFilterTranslator.GetTargetFields(entitySet);
					QueryVariableReferenceExpression variable2 = queryExpressionBinding3.Variable;
					foreach (EdmFieldInstance edmFieldInstance in targetFields)
					{
						string text2 = queryNamingContext.CreateAndRegisterUniqueName(edmFieldInstance.Field.Name);
						QueryFieldExpression queryFieldExpression2 = variable2.Field(edmFieldInstance.Field, null);
						KeyValuePair<string, QueryFieldExpression> keyValuePair2 = new KeyValuePair<string, QueryFieldExpression>(text2, queryFieldExpression2);
						list4.Add(keyValuePair2);
						list2.Add(keyValuePair2);
					}
					list.Add(new ImplicitJoinSecondaryTable(queryExpressionBinding3, list4));
				}
			}
			QueryImplicitJoinWithProjectionExpression queryImplicitJoinWithProjectionExpression = QueryExpressionBuilder.ImplicitJoinWithProjection(queryExpressionBinding, list);
			QueryGroupExpressionBinding groupBind = queryImplicitJoinWithProjectionExpression.GroupBindAs(QdmNames.Grouped(QdmNames.Join(null)));
			return groupBind.GroupBy(list2.Select((KeyValuePair<string, QueryFieldExpression> kvp) => groupBind.Variable.Field(kvp.Key).As(kvp.Key)));
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0004AE58 File Offset: 0x00049058
		internal static IEnumerable<EdmFieldInstance> GetTargetFields(EntitySet targetEntitySet)
		{
			IEnumerable<EdmFieldInstance> enumerable;
			if (targetEntitySet.ElementType.IsKeyStable())
			{
				enumerable = targetEntitySet.GetKeyFieldInstances();
			}
			else
			{
				enumerable = from f in targetEntitySet.ElementType.Fields
					where f.IsStable()
					select targetEntitySet.FieldInstance(f);
			}
			return enumerable;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0004AEDC File Offset: 0x000490DC
		internal static IEnumerable<IConceptualColumn> GetTargetColumns(IConceptualEntity targetEntity)
		{
			if (targetEntity == null)
			{
				return null;
			}
			if (targetEntity.KeyColumns.Count != 0)
			{
				if (targetEntity.KeyColumns.All((IConceptualColumn c) => c.IsStable))
				{
					return targetEntity.KeyColumns;
				}
			}
			return from f in targetEntity.Properties
				where f.IsStable && f is IConceptualColumn
				select f.AsColumn();
		}
	}
}
