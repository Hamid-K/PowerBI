using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000260 RID: 608
	internal sealed class BatchQueryConstraintValidatorAlgorithms
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x00048F94 File Offset: 0x00047194
		internal static IReadOnlyList<GroupAndJoinMeasure> DeterminePredicatesFromContextTables(IEnumerable<EntitySet> groupEntitySets, IEnumerable<IConceptualEntity> groupEntities, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] IEnumerable<global::System.ValueTuple<QueryExpression, bool>> contextTables, IReadOnlyList<GroupAndJoinMeasure> measures, IConceptualModel model, IConceptualSchema schema, bool includeDirectManyToManyAssociations, ExpressionReferenceNameToTableMapping referenceNameMapping, QueryNamingContext namingContext, bool useConceptualSchema)
		{
			List<IEnumerable<EntitySet>> list = new List<IEnumerable<EntitySet>>();
			List<IEnumerable<IConceptualEntity>> list2 = new List<IEnumerable<IConceptualEntity>>();
			bool flag = true;
			if (useConceptualSchema)
			{
				foreach (global::System.ValueTuple<QueryExpression, bool> valueTuple in contextTables)
				{
					IEnumerable<IConceptualEntity> enumerable = BatchQueryConstraintValidatorAlgorithms.FindContextTableEntitiesForPredicates(valueTuple, referenceNameMapping);
					if (!enumerable.Any<IConceptualEntity>())
					{
						flag = false;
						break;
					}
					list2.Add(enumerable);
				}
				List<IConceptualEntity> list3 = (flag ? BatchQueryConstraintValidatorAlgorithms.DetermineAffectedGroupEntities(groupEntities, list2, schema, includeDirectManyToManyAssociations).ToList<IConceptualEntity>() : groupEntities.ToList<IConceptualEntity>());
				if (list3.Any<IConceptualEntity>())
				{
					IEnumerable<GroupAndJoinMeasure> enumerable2 = BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumnsInlineFiltering(null, list3, namingContext, schema.DiscourageCountRowsOverTables(), useConceptualSchema);
					return measures.Concat(enumerable2).ToList<GroupAndJoinMeasure>();
				}
			}
			else
			{
				foreach (global::System.ValueTuple<QueryExpression, bool> valueTuple2 in contextTables)
				{
					IEnumerable<EntitySet> enumerable3 = BatchQueryConstraintValidatorAlgorithms.FindContextTableEntitySetsForPredicates(valueTuple2, referenceNameMapping);
					if (!enumerable3.Any<EntitySet>())
					{
						flag = false;
						break;
					}
					list.Add(enumerable3);
				}
				IReadOnlyList<EntitySet> readOnlyList = (flag ? BatchQueryConstraintValidatorAlgorithms.DetermineAffectedGroupEntitySets(groupEntitySets, list, model, includeDirectManyToManyAssociations).ToList<EntitySet>() : groupEntitySets.ToList<EntitySet>());
				if (readOnlyList.Any<EntitySet>())
				{
					IEnumerable<GroupAndJoinMeasure> enumerable4 = BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumnsInlineFiltering(readOnlyList, null, namingContext, model.DiscourageCountRowsOverTables(), useConceptualSchema);
					return measures.Concat(enumerable4).ToList<GroupAndJoinMeasure>();
				}
			}
			return measures;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000490F4 File Offset: 0x000472F4
		internal static IEnumerable<EntitySet> DetermineAffectedGroupEntitySets(IEnumerable<EntitySet> groupEntitySets, IEnumerable<IEnumerable<EntitySet>> contextTableSets, IConceptualModel model, bool includeDirectManyToManyAssociations)
		{
			List<EntitySet> list = new List<EntitySet>();
			foreach (IEnumerable<EntitySet> enumerable in contextTableSets)
			{
				list.AddRange(BatchQueryConstraintValidatorAlgorithms.DetectRelatedEntitySets(groupEntitySets, enumerable, model, includeDirectManyToManyAssociations));
			}
			return list.Distinct<EntitySet>().Evaluate<EntitySet>();
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00049158 File Offset: 0x00047358
		internal static IEnumerable<IConceptualEntity> DetermineAffectedGroupEntities(IEnumerable<IConceptualEntity> groupEntities, IEnumerable<IEnumerable<IConceptualEntity>> contextTableSets, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			List<IConceptualEntity> list = new List<IConceptualEntity>();
			foreach (IEnumerable<IConceptualEntity> enumerable in contextTableSets)
			{
				list.AddRange(BatchQueryConstraintValidatorAlgorithms.DetectRelatedEntities(groupEntities, enumerable, schema, includeDirectManyToManyAssociations));
			}
			return list.Distinct(ConceptualEntityExtensionAwareEqualityComparer.Instance).Evaluate<IConceptualEntity>();
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000491C0 File Offset: 0x000473C0
		internal static IEnumerable<EntitySet> DetectRelatedEntitySets(IEnumerable<EntitySet> groupEntities, IEnumerable<EntitySet> contextTableEntities, IConceptualModel model, bool includeDirectManyToManyAssociations)
		{
			IEnumerable<EntitySet> enumerable = contextTableEntities.Except(groupEntities);
			IList<ReadOnlyCollection<EntitySet>> trimmedContextTableBaseEntities = enumerable.Select((EntitySet entity) => QueryAlgorithms.DetectCrossFilteredEntities(new EntitySet[] { entity }, model, includeDirectManyToManyAssociations).CompleteSet).Evaluate<ReadOnlyCollection<EntitySet>>();
			return groupEntities.Where((EntitySet groupEntity) => trimmedContextTableBaseEntities.Any((ReadOnlyCollection<EntitySet> baseEntities) => baseEntities.Contains(groupEntity))).Evaluate<EntitySet>();
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00049220 File Offset: 0x00047420
		internal static IEnumerable<IConceptualEntity> DetectRelatedEntities(IEnumerable<IConceptualEntity> groupEntities, IEnumerable<IConceptualEntity> contextTableEntities, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			IEnumerable<IConceptualEntity> enumerable = contextTableEntities.Except(groupEntities, ConceptualEntityExtensionAwareEqualityComparer.Instance);
			IList<IReadOnlyList<IConceptualEntity>> trimmedContextTableBaseEntities = enumerable.Select((IConceptualEntity entity) => QueryAlgorithms.DetectCrossFilteredEntities(new IConceptualEntity[] { entity }, schema, includeDirectManyToManyAssociations).CompleteSet).Evaluate<IReadOnlyList<IConceptualEntity>>();
			return groupEntities.Where((IConceptualEntity groupEntity) => trimmedContextTableBaseEntities.Any((IReadOnlyList<IConceptualEntity> baseEntities) => baseEntities.Contains(groupEntity, ConceptualEntityExtensionAwareEqualityComparer.Instance))).Evaluate<IConceptualEntity>();
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x00049282 File Offset: 0x00047482
		private static IEnumerable<EntitySet> FindContextTableEntitySetsForPredicates([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] global::System.ValueTuple<QueryExpression, bool> contextTable, ExpressionReferenceNameToTableMapping referenceNameMapping)
		{
			if (!contextTable.Item2)
			{
				return Enumerable.Empty<EntitySet>();
			}
			return BatchQueryConstraintValidatorAlgorithms.GetOriginalExpression(contextTable.Item1, referenceNameMapping).FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All);
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000492A4 File Offset: 0x000474A4
		private static IEnumerable<IConceptualEntity> FindContextTableEntitiesForPredicates([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Expression", "ShouldCrossFilterGroupColumns" })] global::System.ValueTuple<QueryExpression, bool> contextTable, ExpressionReferenceNameToTableMapping referenceNameMapping)
		{
			if (!contextTable.Item2)
			{
				return Enumerable.Empty<IConceptualEntity>();
			}
			return BatchQueryConstraintValidatorAlgorithms.GetOriginalExpression(contextTable.Item1, referenceNameMapping).FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All);
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000492C8 File Offset: 0x000474C8
		internal static QueryExpression GetOriginalExpression(QueryExpression expression, ExpressionReferenceNameToTableMapping mapping)
		{
			QueryVariableReferenceExpression queryVariableReferenceExpression = expression as QueryVariableReferenceExpression;
			if (queryVariableReferenceExpression != null && mapping != null)
			{
				QueryTable table = mapping.GetTable(queryVariableReferenceExpression.VariableName);
				if (table != null)
				{
					expression = table.Expression;
				}
			}
			return expression;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000492FB File Offset: 0x000474FB
		internal static QueryExpression CreateNonEmptyPredicate(GroupAndJoinMeasure measure)
		{
			return measure.Column.QdmReference().IsNull().Not();
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00049312 File Offset: 0x00047512
		internal static QueryExpression CreateIsTruePredicate(GroupAndJoinMeasure measure)
		{
			return measure.Column.QdmReference();
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00049320 File Offset: 0x00047520
		internal static IEnumerable<GroupAndJoinMeasure> CreateBaseEntityPredicateColumns(IReadOnlyList<EntitySet> baseEntitySets, IReadOnlyList<IConceptualEntity> baseEntities, QueryNamingContext namingContext, bool useConceptualSchema)
		{
			List<GroupAndJoinMeasure> list = new List<GroupAndJoinMeasure>();
			if (useConceptualSchema)
			{
				using (IEnumerator<IConceptualEntity> enumerator = baseEntities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IConceptualEntity conceptualEntity = enumerator.Current;
						QueryExpression queryExpression = conceptualEntity.Scan(true).HasAnyRows(false);
						GroupAndJoinMeasure groupAndJoinMeasure = new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("HasData" + conceptualEntity.EdmName), queryExpression), true);
						list.Add(groupAndJoinMeasure);
					}
					return list;
				}
			}
			foreach (EntitySet entitySet in baseEntitySets)
			{
				QueryExpression queryExpression2 = entitySet.Scan(true).HasAnyRows(false);
				GroupAndJoinMeasure groupAndJoinMeasure2 = new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("HasData" + entitySet.Name), queryExpression2), true);
				list.Add(groupAndJoinMeasure2);
			}
			return list;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0004941C File Offset: 0x0004761C
		internal static GroupAndJoinMeasure CreateDefaultMeasureColumn(EdmMeasureInstance defaultMeasure, QueryNamingContext namingContext)
		{
			QueryFunctionExpression queryFunctionExpression = defaultMeasure.InvokeMeasure(null).IsNull().Not();
			return new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("DefaultMeasureNotBlank" + defaultMeasure.Measure.Name), queryFunctionExpression), true);
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00049464 File Offset: 0x00047664
		internal static GroupAndJoinMeasure CreateDefaultMeasureColumn(IConceptualMeasure conceptualMeasure, QueryNamingContext namingContext)
		{
			QueryFunctionExpression queryFunctionExpression = conceptualMeasure.InvokeMeasure().IsNull().Not();
			return new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("DefaultMeasureNotBlank" + ((conceptualMeasure != null) ? conceptualMeasure.EdmName : null)), queryFunctionExpression), true);
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000494AA File Offset: 0x000476AA
		internal static IEnumerable<GroupAndJoinMeasure> CreateBaseEntityPredicateColumnsInlineFiltering(IReadOnlyList<EntitySet> baseEntitySets, IReadOnlyList<IConceptualEntity> baseEntities, QueryNamingContext namingContext, bool useIsEmptyStrategy, bool useConceptualSchema)
		{
			if (useIsEmptyStrategy)
			{
				return BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumnsIsEmptyInlineFiltering(baseEntitySets, baseEntities, namingContext, useConceptualSchema);
			}
			return BatchQueryConstraintValidatorAlgorithms.CreateBaseEntityPredicateColumnsCountRowsInlineFiltering(baseEntitySets, baseEntities, namingContext, useConceptualSchema);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000494C4 File Offset: 0x000476C4
		private static IEnumerable<GroupAndJoinMeasure> CreateBaseEntityPredicateColumnsCountRowsInlineFiltering(IReadOnlyList<EntitySet> baseEntitySets, IReadOnlyList<IConceptualEntity> baseEntities, QueryNamingContext namingContext, bool useConceptualSchema)
		{
			List<GroupAndJoinMeasure> list = new List<GroupAndJoinMeasure>();
			if (useConceptualSchema)
			{
				for (int i = 0; i < baseEntities.Count; i++)
				{
					IConceptualEntity conceptualEntity = baseEntities[i];
					QueryCalculateExpression queryCalculateExpression = conceptualEntity.Scan(true).CountRows().Calculate(Array.Empty<QueryExpression>());
					GroupAndJoinMeasure groupAndJoinMeasure = new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("CountRows" + conceptualEntity.EdmName), queryCalculateExpression), false);
					list.Add(groupAndJoinMeasure);
				}
			}
			else
			{
				for (int j = 0; j < baseEntitySets.Count; j++)
				{
					EntitySet entitySet = baseEntitySets[j];
					QueryCalculateExpression queryCalculateExpression2 = entitySet.Scan(true).CountRows().Calculate(Array.Empty<QueryExpression>());
					GroupAndJoinMeasure groupAndJoinMeasure2 = new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("CountRows" + entitySet.Name), queryCalculateExpression2), false);
					list.Add(groupAndJoinMeasure2);
				}
			}
			return list;
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000495A0 File Offset: 0x000477A0
		internal static IEnumerable<GroupAndJoinMeasure> CreateBaseEntityPredicateColumnsIsEmptyInlineFiltering(IReadOnlyList<EntitySet> baseEntitySets, IReadOnlyList<IConceptualEntity> baseEntities, QueryNamingContext namingContext, bool useConceptualSchema)
		{
			QueryExpression queryExpression;
			if (useConceptualSchema)
			{
				if (baseEntities.IsNullOrEmpty<IConceptualEntity>())
				{
					return null;
				}
				if (baseEntities.Count == 1)
				{
					queryExpression = baseEntities[0].Scan(true).IsEmpty();
				}
				else
				{
					QueryExpression[] array = new QueryExpression[baseEntities.Count];
					for (int i = 0; i < baseEntities.Count; i++)
					{
						IConceptualEntity conceptualEntity = baseEntities[i];
						array[i] = conceptualEntity.Scan(true).IsEmpty();
					}
					queryExpression = array.AndAll(false);
				}
			}
			else
			{
				if (baseEntitySets.IsNullOrEmpty<EntitySet>())
				{
					return null;
				}
				if (baseEntitySets.Count == 1)
				{
					queryExpression = baseEntitySets[0].Scan(true).IsEmpty();
				}
				else
				{
					QueryExpression[] array2 = new QueryExpression[baseEntitySets.Count];
					for (int j = 0; j < baseEntitySets.Count; j++)
					{
						EntitySet entitySet = baseEntitySets[j];
						array2[j] = entitySet.Scan(true).IsEmpty();
					}
					queryExpression = array2.AndAll(false);
				}
			}
			string text = namingContext.CreateAndRegisterUniqueName("BlankIfIsEmpty");
			QueryCalculateExpression queryCalculateExpression = queryExpression.If(Literals.NullInt64, Literals.Zero).Calculate(Array.Empty<QueryExpression>());
			return new List<GroupAndJoinMeasure>
			{
				new GroupAndJoinMeasure(new QueryTableColumn(text, queryCalculateExpression), false)
			};
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000496D0 File Offset: 0x000478D0
		internal static GroupAndJoinMeasure CreateDefaultMeasureColumnInlineFiltering(EdmMeasureInstance defaultMeasure, QueryNamingContext namingContext)
		{
			QueryMeasureExpression queryMeasureExpression = defaultMeasure.InvokeMeasure(null);
			return new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("DefaultMeasure" + defaultMeasure.Measure.Name), queryMeasureExpression), false);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00049710 File Offset: 0x00047910
		internal static GroupAndJoinMeasure CreateDefaultMeasureColumnInlineFiltering(IConceptualMeasure conceptualMeasure, QueryNamingContext namingContext)
		{
			QueryMeasureExpression queryMeasureExpression = conceptualMeasure.InvokeMeasure();
			return new GroupAndJoinMeasure(new QueryTableColumn(namingContext.CreateAndRegisterUniqueName("DefaultMeasure" + ((conceptualMeasure != null) ? conceptualMeasure.EdmName : null)), queryMeasureExpression), false);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0004974C File Offset: 0x0004794C
		internal static IEnumerable<GroupAndJoinMeasure> CreatePredicatesFromExistsFilters(IReadOnlyList<QueryExistsFilter> existsFilters, QueryNamingContext namingContext, bool useConceptualSchema)
		{
			QueryExpression queryExpression;
			if (useConceptualSchema)
			{
				IList<IConceptualEntity> list = existsFilters.Select((QueryExistsFilter f) => f.ExistsEntity).Distinct<IConceptualEntity>().Evaluate<IConceptualEntity>();
				queryExpression = list[0].Scan(true).CountRows();
				for (int i = 1; i < list.Count; i++)
				{
					queryExpression = queryExpression.Multiply(list[i].Scan(true).CountRows());
				}
			}
			else
			{
				IList<EntitySet> list2 = existsFilters.Select((QueryExistsFilter f) => f.ExistsEntitySet).Distinct<EntitySet>().Evaluate<EntitySet>();
				queryExpression = list2[0].Scan(true).CountRows();
				for (int j = 1; j < list2.Count; j++)
				{
					queryExpression = queryExpression.Multiply(list2[j].Scan(true).CountRows());
				}
			}
			string text = namingContext.CreateAndRegisterUniqueName("ExistsPredicate");
			return Util.AsEnumerable<GroupAndJoinMeasure>(new GroupAndJoinMeasure(queryExpression.ToQueryTableColumn(text), false));
		}
	}
}
