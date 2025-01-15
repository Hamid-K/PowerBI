using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000105 RID: 261
	internal static class QueryAlgorithms
	{
		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002879D File Offset: 0x0002699D
		internal static QueryExpression MergeGroupExpressions(QueryExpression expression)
		{
			return QueryAlgorithms.MergeGroupExpressionsTransform.Invoke(expression);
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x000287A5 File Offset: 0x000269A5
		internal static BaseEntitySets DetectCrossFilteredEntitySetsByCardinalityOnly(IEnumerable<EntitySet> referencedEntities, IConceptualModel model, bool includeDirectManyToManyAssociations)
		{
			return QueryAlgorithms.DetectBaseEntitySetsAlgorithm.Invoke(referencedEntities, model.GetAssociationsFromOneGraph(includeDirectManyToManyAssociations));
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000287B4 File Offset: 0x000269B4
		internal static BaseConceptualEntities DetectCrossFilteredEntitiesByCardinalityOnly(IEnumerable<IConceptualEntity> referencedEntities, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			IDirectedGraph<IConceptualEntity> associationsFromOneGraph = schema.GetAssociationsFromOneGraph(includeDirectManyToManyAssociations);
			return QueryAlgorithms.DetectBaseEntitiesAlgorithm.Invoke(referencedEntities, associationsFromOneGraph);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000287D0 File Offset: 0x000269D0
		internal static BaseEntitySets DetectCrossFilteredEntitySets(IEnumerable<EntitySet> referencedEntities, EntityDataModel model, bool includeDirectManyToManyAssociations)
		{
			return QueryAlgorithms.DetectCrossFilteredEntities(referencedEntities, model, includeDirectManyToManyAssociations);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000287DC File Offset: 0x000269DC
		internal static BaseConceptualEntities DetectCrossFilteredEntities(IEnumerable<IConceptualEntity> referencedEntities, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			IDirectedGraph<IConceptualEntity> associationsFromOneWithBidirCrossFilteringGraph = schema.GetAssociationsFromOneWithBidirCrossFilteringGraph(includeDirectManyToManyAssociations);
			return QueryAlgorithms.DetectBaseEntitiesAlgorithm.Invoke(referencedEntities, associationsFromOneWithBidirCrossFilteringGraph);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000287F8 File Offset: 0x000269F8
		internal static BaseEntitySets DetectCrossFilteredEntities(IEnumerable<EntitySet> referencedEntities, IConceptualModel model, bool includeDirectManyToManyAssociations)
		{
			return QueryAlgorithms.DetectBaseEntitySetsAlgorithm.Invoke(referencedEntities, model.GetAssociationsFromOneWithBidirCrossFilteringGraph(includeDirectManyToManyAssociations));
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00028807 File Offset: 0x00026A07
		internal static ReadOnlyCollection<EntitySet> DetectRelatedManyToOneEntities(EntitySet sourceEntity, EntityDataModel model)
		{
			return QueryAlgorithms.DetectBaseEntitySetsAlgorithm.Invoke(new EntitySet[] { sourceEntity }, model.GetAssociationsFromManyGraph()).CompleteSet;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00028824 File Offset: 0x00026A24
		internal static IReadOnlyList<IConceptualEntity> DetectRelatedManyToOneEntities(IConceptualEntity sourceEntity, IConceptualSchema schema)
		{
			NavigationPropertyGraphAnnotation navigationPropertyGraphAnnotation = schema.GetNavigationPropertyGraphAnnotation();
			return QueryAlgorithms.DetectBaseEntitiesAlgorithm.Invoke(new IConceptualEntity[] { sourceEntity }, navigationPropertyGraphAnnotation.AssociationsFromManyGraph).CompleteSet;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00028852 File Offset: 0x00026A52
		internal static IReadOnlyList<IConceptualEntity> GetRelatedToOneEntities(IConceptualEntity source, IConceptualSchema schema)
		{
			if (source == null)
			{
				return Microsoft.DataShaping.Util.EmptyReadOnlyList<IConceptualEntity>();
			}
			return QueryAlgorithms.DetectRelatedManyToOneEntities(source, schema).Union(QueryAlgorithms.GetRelatedEntitiesOneToOne(source, schema)).ToReadOnlyList<IConceptualEntity>();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x00028878 File Offset: 0x00026A78
		private static IList<IConceptualEntity> GetRelatedEntitiesOneToOne(IConceptualEntity source, IConceptualSchema schema)
		{
			source = source.GetBaseEntityOrSelf();
			NavigationPropertyGraphAnnotation navigationPropertyGraphAnnotation = schema.GetNavigationPropertyGraphAnnotation();
			IDirectedGraph<IConceptualEntity> associationsFromOneGraph = navigationPropertyGraphAnnotation.AssociationsFromOneGraph;
			IDirectedGraph<IConceptualEntity> fromManyAssociations = navigationPropertyGraphAnnotation.AssociationsFromManyGraph;
			Func<IConceptualEntity, bool> func = (IConceptualEntity e) => !fromManyAssociations.GetEdgesFromVertex(e).Contains(source);
			return associationsFromOneGraph.GetEdgesFromVertex(source).Where(func).ToList<IConceptualEntity>();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000288DF File Offset: 0x00026ADF
		internal static bool AreHierarchicallyRelated(Group firstGroup, Group secondGroup, EntityDataModel model, bool includeDirectManyToManyAssociations)
		{
			return QueryAlgorithms.AreHierarchicallyRelated(QueryConstraintValidator.FindAllGroupEntitySetReferences(new Group[] { firstGroup, secondGroup }), model, includeDirectManyToManyAssociations);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000288FB File Offset: 0x00026AFB
		internal static bool AreHierarchicallyRelated(Group firstGroup, Group secondGroup, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			return QueryAlgorithms.AreHierarchicallyRelated(QueryConstraintValidator.FindAllGroupEntityReferences(new Group[] { firstGroup, secondGroup }), schema, includeDirectManyToManyAssociations);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00028918 File Offset: 0x00026B18
		internal static bool AreHierarchicallyRelated(HashSet<EntitySet> groupEntities, EntityDataModel model, bool includeDirectManyToManyAssociations)
		{
			BaseEntitySets baseEntitySets = QueryAlgorithms.DetectCrossFilteredEntitySetsByCardinalityOnly(groupEntities, model, includeDirectManyToManyAssociations);
			if (baseEntitySets.PrunedSet.Count == 1)
			{
				EntitySet entitySet = baseEntitySets.PrunedSet.Single<EntitySet>();
				if (groupEntities.Contains(entitySet))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00028954 File Offset: 0x00026B54
		internal static bool AreHierarchicallyRelated(HashSet<IConceptualEntity> groupEntities, IConceptualSchema schema, bool includeDirectManyToManyAssociations)
		{
			BaseConceptualEntities baseConceptualEntities = QueryAlgorithms.DetectCrossFilteredEntitiesByCardinalityOnly(groupEntities, schema, includeDirectManyToManyAssociations);
			if (baseConceptualEntities.PrunedSet.Count == 1)
			{
				IConceptualEntity conceptualEntity = baseConceptualEntities.PrunedSet.Single<IConceptualEntity>();
				if (groupEntities.Contains(conceptualEntity))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00028990 File Offset: 0x00026B90
		internal static bool HasHierarchicalFilterPath(HashSet<EntitySet> entitiesToFind, EntityDataModel model)
		{
			IReadOnlyList<EntitySet> readOnlyList;
			return QueryAlgorithms.HasHierarchicalFilterPath(entitiesToFind, model, out readOnlyList);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000289A8 File Offset: 0x00026BA8
		internal static bool HasHierarchicalFilterPath(HashSet<IConceptualEntity> entitiesToFind, IConceptualSchema schema)
		{
			IReadOnlyList<IConceptualEntity> readOnlyList;
			return QueryAlgorithms.HasHierarchicalFilterPath(entitiesToFind, schema, out readOnlyList);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000289BE File Offset: 0x00026BBE
		internal static bool HasHierarchicalFilterPath(HashSet<EntitySet> entitiesToFind, EntityDataModel model, out IReadOnlyList<EntitySet> path)
		{
			return QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<EntitySet>.TryFindHierarchicalPath(entitiesToFind, model, out path);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000289C8 File Offset: 0x00026BC8
		internal static bool HasHierarchicalFilterPath(HashSet<IConceptualEntity> entitiesToFind, IConceptualSchema schema, out IReadOnlyList<IConceptualEntity> path)
		{
			return QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<IConceptualEntity>.TryFindHierarchicalPath(entitiesToFind, schema, out path);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x000289D4 File Offset: 0x00026BD4
		internal static bool AreDirectlyRelated(HashSet<EntitySet> groupEntities, EntityDataModel model)
		{
			foreach (EntitySet entitySet in QueryAlgorithms.DetectCrossFilteredEntitySetsByCardinalityOnly(groupEntities, model, true).PrunedSet)
			{
				if (groupEntities.Contains(entitySet))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x00028A34 File Offset: 0x00026C34
		internal static bool AreDirectlyRelated(HashSet<IConceptualEntity> groupEntities, IConceptualSchema schema)
		{
			foreach (IConceptualEntity conceptualEntity in QueryAlgorithms.DetectCrossFilteredEntitiesByCardinalityOnly(groupEntities, schema, true).PrunedSet)
			{
				if (groupEntities.Contains(conceptualEntity))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x00028A94 File Offset: 0x00026C94
		internal static bool IsVariant(QueryExpression expr)
		{
			QueryMeasureExpression queryMeasureExpression = expr as QueryMeasureExpression;
			if (queryMeasureExpression == null)
			{
				return false;
			}
			if (queryMeasureExpression.TargetEntity != null)
			{
				return queryMeasureExpression.TargetMeasure.IsVariant;
			}
			ActualDataType? actualType = queryMeasureExpression.Measure.ActualType;
			ActualDataType actualDataType = ActualDataType.Any;
			return (actualType.GetValueOrDefault() == actualDataType) & (actualType != null);
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00028AE1 File Offset: 0x00026CE1
		internal static bool CanUseIsOnOrAfter(IConceptualSchema schema)
		{
			return schema.GetDaxCapabilitiesAnnotation().DaxFunctions.SupportsIsOnOrAfter;
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00028AF3 File Offset: 0x00026CF3
		internal static QueryExpression DaxScalarToIdentityComparisons(QueryExpression expression)
		{
			return QueryAlgorithms.DaxScalarToIdentityComparisonsTransform.Invoke(expression);
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00028AFB File Offset: 0x00026CFB
		internal static QueryInExpression RewriteWithEqualitySemantics(QueryInExpression expression, CancellationToken cancellationToken)
		{
			return QueryAlgorithms.DaxInExpressionComparisonsTransform.Invoke(expression, cancellationToken);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00028B04 File Offset: 0x00026D04
		internal static bool CanBePreservedAsTuples(QueryInExpression expression)
		{
			return QueryAlgorithms.DaxInExpressionComparisonsTransform.ShouldUseInWithEqualitySemantics(expression);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00028B0C File Offset: 0x00026D0C
		internal static QueryInExpression DedupeValues(QueryInExpression expression, IDataComparer comparer)
		{
			return QueryAlgorithms.DaxInExpressionDedupingTransform.Invoke(expression, comparer);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00028B15 File Offset: 0x00026D15
		internal static bool ShouldUseDaxIdentityComparison(QueryExpression left, QueryExpression right)
		{
			return (left.ConceptualResultType.IsText() || right.ConceptualResultType.IsText()) && !QueryAlgorithms.IsExemptFromStringBlankComparison(left, right);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00028B40 File Offset: 0x00026D40
		private static bool RequiresEqualitySemantics(QueryExpression expr)
		{
			ConceptualPrimitiveType? primitiveTypeKind = expr.ConceptualResultType.GetPrimitiveTypeKind();
			ConceptualPrimitiveType? conceptualPrimitiveType = primitiveTypeKind;
			ConceptualPrimitiveType conceptualPrimitiveType2 = ConceptualPrimitiveType.Double;
			if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
			{
				conceptualPrimitiveType = primitiveTypeKind;
				conceptualPrimitiveType2 = ConceptualPrimitiveType.Boolean;
				if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
				{
					conceptualPrimitiveType = primitiveTypeKind;
					conceptualPrimitiveType2 = ConceptualPrimitiveType.Decimal;
					if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
					{
						conceptualPrimitiveType = primitiveTypeKind;
						conceptualPrimitiveType2 = ConceptualPrimitiveType.DateTime;
						if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
						{
							conceptualPrimitiveType = primitiveTypeKind;
							conceptualPrimitiveType2 = ConceptualPrimitiveType.Integer;
							return (conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00028BD1 File Offset: 0x00026DD1
		private static bool IsExemptFromStringBlankComparison(QueryExpression left, QueryExpression right)
		{
			return QueryAlgorithms.IsNonBlankStringLiteral(left) || QueryAlgorithms.IsNonBlankStringLiteral(right);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00028BE3 File Offset: 0x00026DE3
		internal static bool IsExemptFromBlankComparison(QueryExpression left, QueryExpression right)
		{
			return QueryAlgorithms.IsNonBlankLiteral(left) || QueryAlgorithms.IsNonBlankLiteral(right);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00028BF8 File Offset: 0x00026DF8
		private static bool IsNonBlankStringLiteral(QueryExpression candidate)
		{
			QueryLiteralExpression queryLiteralExpression = candidate as QueryLiteralExpression;
			return queryLiteralExpression != null && !string.IsNullOrEmpty(queryLiteralExpression.Value.Value as string);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00028C2C File Offset: 0x00026E2C
		private static bool IsNonBlankLiteral(QueryExpression candidate)
		{
			QueryLiteralExpression queryLiteralExpression = candidate as QueryLiteralExpression;
			return queryLiteralExpression != null && !queryLiteralExpression.IsDaxLiteralBlankEquivalent();
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00028C4E File Offset: 0x00026E4E
		internal static QueryExpression CountRowsToIsEmpty(QueryExpression expression)
		{
			return QueryAlgorithms.CountRowsToIsEmptyTransform.Invoke(expression);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00028C56 File Offset: 0x00026E56
		internal static QueryExpression IsOnOrAfterToComparisons(QueryIsOnOrAfterExpression expression)
		{
			return QueryAlgorithms.IsOnOrAfterToComparisonsTransform.Invoke(expression);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00028C5E File Offset: 0x00026E5E
		internal static QueryExpression IsAfterToComparisons(QueryIsAfterExpression expression)
		{
			return QueryAlgorithms.IsAfterToComparisonsTransform.Invoke(expression);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00028C68 File Offset: 0x00026E68
		internal static QueryDefinition RemoveRedundantSlicerInSubqueries(QueryDefinition originalQuery, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			if (originalQuery.Slicer != null && originalQuery.ExplicitJoinPredicates != null)
			{
				ICollection<global::System.ValueTuple<int, SubqueryJoinPredicate>> collection = null;
				int num = -1;
				foreach (IJoinPredicate joinPredicate in originalQuery.ExplicitJoinPredicates)
				{
					num++;
					SubqueryJoinPredicate subqueryJoinPredicate = joinPredicate as SubqueryJoinPredicate;
					if (subqueryJoinPredicate != null)
					{
						QueryDefinition queryDefinition = QueryAlgorithms.RemoveRedundantSlicerInSubqueries(subqueryJoinPredicate.Query, featureSwitchProvider, cancellationToken);
						if (FilterExtensions.AreEquivalent(originalQuery.Slicer, queryDefinition.Slicer))
						{
							queryDefinition = queryDefinition.UpdateSlicer(null);
						}
						if (queryDefinition != subqueryJoinPredicate.Query)
						{
							if (collection == null)
							{
								collection = new List<global::System.ValueTuple<int, SubqueryJoinPredicate>>();
							}
							collection.Add(new global::System.ValueTuple<int, SubqueryJoinPredicate>(num, new SubqueryJoinPredicate(queryDefinition, featureSwitchProvider, cancellationToken)));
						}
					}
				}
				if (collection != null)
				{
					IJoinPredicate[] array = originalQuery.ExplicitJoinPredicates.ToArray<IJoinPredicate>();
					foreach (global::System.ValueTuple<int, SubqueryJoinPredicate> valueTuple in collection)
					{
						array[valueTuple.Item1] = valueTuple.Item2;
					}
					return originalQuery.UpdateExplicitJoinPredicates(array);
				}
			}
			return originalQuery;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x00028D8C File Offset: 0x00026F8C
		internal static QueryDefinition TrimUnneccessaryNonProjectedGroups(QueryDefinition originalQuery, bool crossJoinQueryPlan, bool useConceptualSchema, QueryTrimmer getGroupsToTrimFromQuery = null)
		{
			if (QueryAlgorithms.CanTrimGroups(originalQuery))
			{
				List<Group> list = new List<Group>(originalQuery.Groups.Count);
				for (int i = originalQuery.Groups.Count - 1; i >= 0; i--)
				{
					Group group = originalQuery.Groups[i];
					if ((originalQuery.PostRegroupLimit != null && originalQuery.PostRegroupLimit.RefersTo(group)) || originalQuery.Limits.Any((Limit l) => l.RefersTo(group)))
					{
						break;
					}
					if (!group.IsProjected && (originalQuery.GroupFilter == null || !originalQuery.GroupFilter.RefersTo(group)) && (originalQuery.Rollup == null || !originalQuery.Rollup.RollupGroups.Any((RollupGroup r) => r.RefersTo(group))))
					{
						list.Add(group);
					}
				}
				if (getGroupsToTrimFromQuery != null)
				{
					list = list.Union(getGroupsToTrimFromQuery(originalQuery).EmptyIfNull<Group>()).ToList<Group>();
				}
				if (list.Any<Group>())
				{
					Group[] array = originalQuery.Groups.Except(list).ToArray<Group>();
					BlankRowBehavior blankRowBehavior = originalQuery.AllowBlankRow;
					IEnumerable<IJoinPredicate> enumerable = originalQuery.ExplicitJoinPredicates;
					QueryConstraintValidator queryConstraintValidator = new QueryConstraintValidator(originalQuery.EntityDataModel, originalQuery.ConceptualSchema, array, originalQuery.Measures, originalQuery.ExplicitJoinPredicates, crossJoinQueryPlan, originalQuery.IncludeDirectManyToManyAssociations, useConceptualSchema);
					if (originalQuery.AllowBlankRow == BlankRowBehavior.FilterByProjection)
					{
						IEnumerable<IJoinPredicate> joinPredicates = originalQuery.GetJoinPredicates(crossJoinQueryPlan);
						if (!joinPredicates.SequenceEqual(queryConstraintValidator.GetJoinPredicates(), JoinPredicates.Comparer))
						{
							blankRowBehavior = BlankRowBehavior.FilterByExplicitJoinPredicates;
							enumerable = joinPredicates;
						}
					}
					return new QueryDefinition(originalQuery.EntityDataModel, originalQuery.ConceptualSchema, useConceptualSchema, array, enumerable, blankRowBehavior, originalQuery.GroupFilter, originalQuery.Limits, originalQuery.PostRegroupLimit, originalQuery.Measures, originalQuery.Rollup, null, originalQuery.Slicer, originalQuery.ApplyFilters, originalQuery.FieldsRequiringClearDefaultFilterContext, originalQuery.ColumnsRequiringClearDefaultFilterContext, originalQuery.Sorting, originalQuery.StartAt, originalQuery.Declarations, null, null, originalQuery.TopLevelLimit, false, originalQuery.QueryParameters);
				}
			}
			return originalQuery;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00028F88 File Offset: 0x00027188
		internal static bool CanTrimGroups(QueryDefinition query)
		{
			QueryConstraintValidator queryConstraintValidator = query.CreateConstraintValidator(false);
			if (queryConstraintValidator.HasUnconstrainedMeasuresOrUnanchoredExplicitJoinPredicates())
			{
				return false;
			}
			if (query.GroupFilter != null && queryConstraintValidator.IsMeasureExpressionUnconstrained(query.GroupFilter.Filter.ToPredicate()))
			{
				return false;
			}
			return !(from g in query.Groups
				from d in g.Details
				where d.CalculateInMeasureContext
				select d.Expression).Any(new Func<QueryExpression, bool>(queryConstraintValidator.IsMeasureExpressionUnconstrained));
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00029070 File Offset: 0x00027270
		internal static bool CanMergePredicateWithPreviousFilter(QueryExpression predicate)
		{
			return QueryAlgorithms.CanMergeFilterPredicateVisitor.Invoke(predicate);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x00029078 File Offset: 0x00027278
		internal static bool UsesCurrentGroup(QueryExpression expression)
		{
			return QueryAlgorithms.UsesCurrentGroupVisitor.Invoke(expression);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00029080 File Offset: 0x00027280
		internal static IEnumerable<IEdmFieldInstance> GetFieldsForImplicitDefaultFilterContextExclusions(this IEnumerable<IEdmFieldInstance> fields)
		{
			return fields.GetRelatedFieldsWithDefaultMember((IEdmFieldInstance field) => field.GetLowerRelationshipPath().Except(fields));
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000290B4 File Offset: 0x000272B4
		internal static IEnumerable<IConceptualColumn> GetColumnsForImplicitDefaultFilterContextExclusions(this IEnumerable<IConceptualColumn> columns, FieldRelationshipAnnotations relationshipAnnotations, ColumnGroupingAnnotations groupingAnnotations)
		{
			return columns.GetRelatedColumnsWithDefaultValue(groupingAnnotations, (IConceptualColumn column) => column.GetLowerRelationshipPath(relationshipAnnotations).Except(columns));
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000290F0 File Offset: 0x000272F0
		internal static IEnumerable<IEdmFieldInstance> GetFieldsForDefaultGroupAndFilterExclusions(this IEnumerable<IEdmFieldInstance> fields, bool clearAllFieldsInTable = false)
		{
			if (clearAllFieldsInTable)
			{
				IEnumerable<IEdmFieldInstance> enumerable = fields.Select((IEdmFieldInstance f) => f.Entity).Distinct<EntitySet>().SelectMany((EntitySet e) => from p in e.GetProperties()
					select p.ToIEdmFieldInstance() into fi
					where fi.IsValid
					select fi);
				fields = fields.Union(enumerable).Evaluate<IEdmFieldInstance>();
			}
			return fields.GetRelatedFieldsWithDefaultMember((IEdmFieldInstance field) => field.GetAllFieldsOnPath());
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00029184 File Offset: 0x00027384
		internal static IEnumerable<IConceptualColumn> GetColumnsForDefaultGroupAndFilterExclusions(this IEnumerable<IConceptualColumn> columns, FieldRelationshipAnnotations relationshipAnnotations, ColumnGroupingAnnotations groupingAnnotations, bool clearAllColumnsInTable = false)
		{
			if (clearAllColumnsInTable)
			{
				IEnumerable<IConceptualColumn> enumerable = columns.Select((IConceptualColumn f) => f.Entity).Distinct<IConceptualEntity>().SelectMany((IConceptualEntity e) => e.Properties.OfType<IConceptualColumn>());
				columns = columns.Union(enumerable).Evaluate<IConceptualColumn>();
			}
			return columns.GetRelatedColumnsWithDefaultValue(groupingAnnotations, (IConceptualColumn field) => field.GetAllFieldsOnPath(relationshipAnnotations));
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00029211 File Offset: 0x00027411
		internal static IEnumerable<IEdmFieldInstance> GetFieldsForExplicitDefaultFilterRetain(this IEnumerable<IEdmFieldInstance> fields)
		{
			return fields.GetRelatedFieldsWithDefaultMember((IEdmFieldInstance field) => field.GetAllFieldsHigherOnPath());
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00029238 File Offset: 0x00027438
		internal static IEnumerable<IConceptualColumn> GetColumnsForExplicitDefaultFilterRetain(this IEnumerable<IConceptualColumn> columns, FieldRelationshipAnnotations fieldRelationshipAnnotations, ColumnGroupingAnnotations groupingAnnotations)
		{
			return columns.GetRelatedColumnsWithDefaultValue(groupingAnnotations, (IConceptualColumn field) => field.GetAllFieldsHigherOnPath(fieldRelationshipAnnotations));
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00029268 File Offset: 0x00027468
		private static IEnumerable<IEdmFieldInstance> GetRelatedFieldsWithDefaultMember(this IEnumerable<IEdmFieldInstance> fields, Func<IEdmFieldInstance, IEnumerable<IEdmFieldInstance>> getRelatedFields)
		{
			if (fields == null)
			{
				return null;
			}
			HashSet<IEdmFieldInstance> hashSet = new HashSet<IEdmFieldInstance>();
			IEnumerable<IEdmFieldInstance> enumerable = fields.SelectMany((IEdmFieldInstance f) => f.GetFieldsThatGroupOnCurrentFieldInstances().Cast<IEdmFieldInstance>());
			foreach (IEdmFieldInstance edmFieldInstance in fields.Union(enumerable))
			{
				IEnumerable<IEdmFieldInstance> enumerable2 = getRelatedFields(edmFieldInstance);
				if (enumerable2 != null)
				{
					IEnumerable<IEdmFieldInstance> enumerable3 = enumerable2.Where((IEdmFieldInstance f) => f.ToPropertyInstance().Property.DefaultMember != null);
					if (enumerable3 != null)
					{
						hashSet.UnionWith(enumerable3);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00029324 File Offset: 0x00027524
		private static IEnumerable<IConceptualColumn> GetRelatedColumnsWithDefaultValue(this IEnumerable<IConceptualColumn> columns, ColumnGroupingAnnotations groupingAnnotations, Func<IConceptualColumn, IEnumerable<IConceptualColumn>> getRelatedColumns)
		{
			if (columns == null)
			{
				return null;
			}
			HashSet<IConceptualColumn> hashSet = new HashSet<IConceptualColumn>();
			List<IConceptualColumn> list = new List<IConceptualColumn>();
			foreach (IConceptualColumn conceptualColumn in columns)
			{
				ColumnGroupingAnnotation columnGroupingAnnotation;
				if (groupingAnnotations.TryGetAnnotation(conceptualColumn, out columnGroupingAnnotation))
				{
					list.AddRange(columnGroupingAnnotation.ColumnsWithThisAsIdentity);
				}
			}
			foreach (IConceptualColumn conceptualColumn2 in columns.Union(list))
			{
				IEnumerable<IConceptualColumn> enumerable = getRelatedColumns(conceptualColumn2);
				if (enumerable != null)
				{
					IEnumerable<IConceptualColumn> enumerable2 = enumerable.Where((IConceptualColumn f) => f.DefaultValue != null);
					if (enumerable2 != null)
					{
						hashSet.UnionWith(enumerable2);
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00029408 File Offset: 0x00027608
		internal static QueryExpression AndAll(this IReadOnlyList<QueryExpression> expressions, bool useBinaryEquivalent = true)
		{
			if (expressions.Count == 1)
			{
				return expressions[0];
			}
			return expressions.And(useBinaryEquivalent);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00029422 File Offset: 0x00027622
		internal static QueryExpression OrAll(this IEnumerable<QueryExpression> expressions)
		{
			return expressions.Aggregate((QueryExpression expr, QueryExpression next) => expr.Or(next));
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0002944C File Offset: 0x0002764C
		internal static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> collections, IEqualityComparer<T> comparer = null)
		{
			return collections.Aggregate((IEnumerable<T> set, IEnumerable<T> next) => set.Intersect(next, comparer));
		}

		// Token: 0x0200032D RID: 813
		private sealed class MergeGroupExpressionsTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001E27 RID: 7719 RVA: 0x00052F8A File Offset: 0x0005118A
			internal static QueryExpression Invoke(QueryExpression expression)
			{
				return expression.Accept<QueryExpression>(new QueryAlgorithms.MergeGroupExpressionsTransform());
			}

			// Token: 0x06001E28 RID: 7720 RVA: 0x00052F97 File Offset: 0x00051197
			private MergeGroupExpressionsTransform()
			{
			}

			// Token: 0x06001E29 RID: 7721 RVA: 0x00052FA0 File Offset: 0x000511A0
			protected internal override QueryExpression Visit(QueryGenerateExpression expression)
			{
				QueryExpression queryExpression = base.Visit(expression);
				QueryGenerateExpression queryGenerateExpression = queryExpression as QueryGenerateExpression;
				QueryExpression queryExpression2;
				if (this.TryCollapseGenerate(queryGenerateExpression, out queryExpression2))
				{
					return queryExpression2;
				}
				return queryExpression;
			}

			// Token: 0x06001E2A RID: 7722 RVA: 0x00052FCC File Offset: 0x000511CC
			private bool TryCollapseGenerate(QueryGenerateExpression expression, out QueryExpression collapsedExpr)
			{
				QueryCalculateExpression queryCalculateExpression = expression.Inputs[1].Expression as QueryCalculateExpression;
				if (expression.GenerateKind == QueryGenerateKind.Generate && queryCalculateExpression != null && queryCalculateExpression.Filters.Count == 0 && this.TryMergeGenerateArguments(expression.Inputs[0].Expression, queryCalculateExpression.Argument, out collapsedExpr))
				{
					return true;
				}
				collapsedExpr = null;
				return false;
			}

			// Token: 0x06001E2B RID: 7723 RVA: 0x00053030 File Offset: 0x00051230
			private bool TryMergeGenerateArguments(QueryExpression input, QueryExpression apply, out QueryExpression mergedExpr)
			{
				QueryFilterExpression queryFilterExpression = apply as QueryFilterExpression;
				if (queryFilterExpression != null && this.TryMergeGenerateArguments(input, queryFilterExpression.Input.Expression, out mergedExpr))
				{
					mergedExpr = queryFilterExpression.Replace(queryFilterExpression.Input.Expression, mergedExpr, true);
					return true;
				}
				QueryGroupByExpression queryGroupByExpression = input as QueryGroupByExpression;
				QueryGroupByExpression queryGroupByExpression2 = apply as QueryGroupByExpression;
				if (queryGroupByExpression != null && queryGroupByExpression2 != null && queryGroupByExpression.Input.Equals(queryGroupByExpression2.Input))
				{
					if (queryGroupByExpression.GroupItems.All((IGroupItem g) => g is CompositeKeyGroupItem))
					{
						if (queryGroupByExpression2.GroupItems.All((IGroupItem g) => g is CompositeKeyGroupItem) && queryGroupByExpression.Aggregates.Count == 0 && queryGroupByExpression2.Aggregates.Count == 0)
						{
							mergedExpr = queryGroupByExpression.Input.GroupBy(queryGroupByExpression.GroupItems.Concat(queryGroupByExpression2.GroupItems));
							return true;
						}
					}
				}
				mergedExpr = null;
				return false;
			}
		}

		// Token: 0x0200032E RID: 814
		private sealed class DetermineHierarchicalFilterPathAlgorithm<T>
		{
			// Token: 0x06001E2C RID: 7724 RVA: 0x0005313A File Offset: 0x0005133A
			private DetermineHierarchicalFilterPathAlgorithm(HashSet<T> entitiesToFind, IDirectedGraph<T> assocs, IEqualityComparer<T> comparer)
			{
				this.entitiesToFind = new HashSet<T>(entitiesToFind, comparer);
				this.assocs = assocs;
				this.corePath = QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath.CreatePath(entitiesToFind, comparer);
				this.comparer = comparer;
			}

			// Token: 0x06001E2D RID: 7725 RVA: 0x0005316C File Offset: 0x0005136C
			internal static bool TryFindHierarchicalPath(HashSet<EntitySet> entitiesToFind, EntityDataModel model, out IReadOnlyList<EntitySet> path)
			{
				if (entitiesToFind.Count < 2)
				{
					path = entitiesToFind.ToList<EntitySet>();
					return true;
				}
				QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<EntitySet> determineHierarchicalFilterPathAlgorithm = new QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<EntitySet>(entitiesToFind, model.GetStrongAssociationsFromOneGraph(), EqualityComparer<EntitySet>.Default);
				bool flag = determineHierarchicalFilterPathAlgorithm.Run();
				path = determineHierarchicalFilterPathAlgorithm.corePath.Path;
				return (!flag || entitiesToFind.Count == path.Count) && flag;
			}

			// Token: 0x06001E2E RID: 7726 RVA: 0x000531C8 File Offset: 0x000513C8
			internal static bool TryFindHierarchicalPath(HashSet<IConceptualEntity> entitiesToFind, IConceptualSchema schema, out IReadOnlyList<IConceptualEntity> path)
			{
				if (entitiesToFind.Count < 2)
				{
					path = entitiesToFind.ToList<IConceptualEntity>();
					return true;
				}
				IDirectedGraph<IConceptualEntity> strongAssociationsFromOneGraph = schema.GetNavigationPropertyGraphAnnotation().StrongAssociationsFromOneGraph;
				QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<IConceptualEntity> determineHierarchicalFilterPathAlgorithm = new QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<IConceptualEntity>(entitiesToFind.Select((IConceptualEntity e) => e.GetBaseEntityOrSelf()).ToHashSet(null), strongAssociationsFromOneGraph, ConceptualEntityExtensionAwareEqualityComparer.Instance);
				bool flag = determineHierarchicalFilterPathAlgorithm.Run();
				path = determineHierarchicalFilterPathAlgorithm.corePath.Path;
				return (!flag || entitiesToFind.Count == path.Count) && flag;
			}

			// Token: 0x06001E2F RID: 7727 RVA: 0x00053254 File Offset: 0x00051454
			private bool Run()
			{
				Stack<T> stack = new Stack<T>(this.entitiesToFind);
				while (stack.Any<T>())
				{
					QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.<>c__DisplayClass7_0 CS$<>8__locals1 = new QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.<>c__DisplayClass7_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.entityToCheck = stack.Pop();
					QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath reversibleEntityPath = QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath.CreatePath(this.entitiesToFind, this.comparer);
					Stack<T> stack2 = new Stack<T>();
					reversibleEntityPath.Add(CS$<>8__locals1.entityToCheck, true);
					this.entitiesToFind.Remove(CS$<>8__locals1.entityToCheck);
					bool flag = false;
					Stack<bool> stack3 = new Stack<bool>();
					stack3.Push(true);
					HashSet<T> hashSet = new HashSet<T>();
					while (!this.comparer.Equals(CS$<>8__locals1.entityToCheck, default(T)) || (!flag && reversibleEntityPath.IsReversible))
					{
						if (EqualityComparer<T>.Default.Equals(CS$<>8__locals1.entityToCheck, default(T)) && !flag && reversibleEntityPath.IsReversible)
						{
							flag = true;
							reversibleEntityPath.Reverse();
							CS$<>8__locals1.entityToCheck = reversibleEntityPath.Path.Last<T>();
							hashSet.Remove(CS$<>8__locals1.entityToCheck);
							Microsoft.DataShaping.Contract.RetailAssert(stack2.Count == 0, "If entityToCheck is default, entitiesToCheck must be empty.");
							stack3.Clear();
							stack3.Push(false);
						}
						else
						{
							Microsoft.DataShaping.Contract.RetailAssert(!this.comparer.Equals(CS$<>8__locals1.entityToCheck, default(T)), "entityToCheck");
							Microsoft.DataShaping.Contract.RetailAssert(hashSet.Add(CS$<>8__locals1.entityToCheck), "We shouldn't revisit entities.");
							IReadOnlyList<T> readOnlyList;
							bool flag2 = !this.assocs.TryGetEdgesFromVertex(CS$<>8__locals1.entityToCheck, out readOnlyList) || readOnlyList.IsNullOrEmpty<T>();
							IEnumerable<T> enumerable;
							if (!flag2)
							{
								enumerable = readOnlyList.Except(stack2).Except(reversibleEntityPath.VisitedEntities).Except(hashSet);
							}
							else
							{
								IEnumerable<T> enumerable2 = readOnlyList;
								enumerable = enumerable2;
							}
							IEnumerable<T> enumerable3 = enumerable;
							if (flag2 || !enumerable3.Any<T>())
							{
								if (stack2.Count > 0)
								{
									CS$<>8__locals1.entityToCheck = stack2.Pop();
									stack3.Pop();
								}
								else
								{
									CS$<>8__locals1.entityToCheck = default(T);
									if (flag || !reversibleEntityPath.IsReversible)
									{
										if (this.corePath.Path.Count > 0)
										{
											return false;
										}
										this.corePath = reversibleEntityPath;
										break;
									}
								}
							}
							else
							{
								HashSet<T> hashSet2 = this.entitiesToFind.Concat(this.corePath.VisitedEntities).Intersect(enumerable3).ToSet<T>();
								if (hashSet2.Any<T>())
								{
									if (hashSet2.Count > 2)
									{
										return false;
									}
									if (hashSet2.Count == 2)
									{
										if (reversibleEntityPath.Path.Count == 1)
										{
											QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
											ref T ptr = ref CS$<>8__locals2.entityToCheck;
											if (default(T) == null)
											{
												T t = CS$<>8__locals2.entityToCheck;
												ptr = ref t;
											}
											if (ptr.Equals(reversibleEntityPath.Path.Single("Only enter this codepath if there is 1 entity in the subPath", Array.Empty<string>())))
											{
												goto IL_0287;
											}
										}
										return false;
									}
									IL_0287:
									T t2 = hashSet2.First<T>();
									if (hashSet2.Count == 2 && this.assocs.HasEdge(t2, CS$<>8__locals1.entityToCheck))
									{
										reversibleEntityPath.Prepend(t2);
										this.entitiesToFind.Remove(t2);
										if (stack.Contains(t2))
										{
											stack = new Stack<T>(stack.Except(new T[] { t2 }));
										}
										t2 = hashSet2.Skip(1).Single("Only enter this codepath if there are 2 foundEntities", Array.Empty<string>());
									}
									reversibleEntityPath.Add(t2, stack3.Peek() && this.assocs.HasEdge(t2, CS$<>8__locals1.entityToCheck));
									this.entitiesToFind.Remove(t2);
									if (stack.Contains(t2))
									{
										stack = new Stack<T>(stack.Except(new T[] { t2 }));
									}
									if (this.entitiesToFind.IsNullOrEmpty<T>())
									{
										if (this.corePath.Path.IsNullOrEmpty<T>())
										{
											if (reversibleEntityPath.IsReversible)
											{
												T t = reversibleEntityPath.Path.First<T>();
												string text = t.ToString();
												t = reversibleEntityPath.Path.Last<T>();
												if (text.CompareTo(t.ToString()) > 0)
												{
													reversibleEntityPath.Reverse();
												}
											}
											this.corePath = reversibleEntityPath;
											return true;
										}
										if (this.corePath.TryPrepend(reversibleEntityPath))
										{
											return true;
										}
										if (this.corePath.VisitedEntities.Intersect(reversibleEntityPath.VisitedEntities).Any<T>())
										{
											return false;
										}
									}
									else if (!this.corePath.Path.IsNullOrEmpty<T>())
									{
										if (this.corePath.TryPrepend(reversibleEntityPath))
										{
											break;
										}
										if (this.corePath.VisitedEntities.Contains(t2))
										{
											return false;
										}
									}
									stack2 = new Stack<T>();
									stack2.Push(t2);
									stack3.Clear();
									stack3.Push(reversibleEntityPath.IsReversible);
								}
								else
								{
									stack3.Pop();
									Stack<bool> stack4 = stack3;
									IEnumerable<T> enumerable4 = enumerable3;
									Func<T, bool> func;
									if ((func = CS$<>8__locals1.<>9__0) == null)
									{
										func = (CS$<>8__locals1.<>9__0 = (T e) => CS$<>8__locals1.<>4__this.assocs.HasEdge(e, CS$<>8__locals1.entityToCheck));
									}
									stack4.Push(enumerable4.Select(func));
									stack2.Push(enumerable3);
								}
								CS$<>8__locals1.entityToCheck = stack2.Pop();
							}
						}
					}
				}
				return false;
			}

			// Token: 0x0400119F RID: 4511
			private readonly HashSet<T> entitiesToFind;

			// Token: 0x040011A0 RID: 4512
			private readonly IDirectedGraph<T> assocs;

			// Token: 0x040011A1 RID: 4513
			private readonly IEqualityComparer<T> comparer;

			// Token: 0x040011A2 RID: 4514
			private QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath corePath;

			// Token: 0x02000437 RID: 1079
			private sealed class ReversibleEntityPath
			{
				// Token: 0x06002205 RID: 8709 RVA: 0x0005B228 File Offset: 0x00059428
				private ReversibleEntityPath()
				{
				}

				// Token: 0x17000840 RID: 2112
				// (get) Token: 0x06002206 RID: 8710 RVA: 0x0005B230 File Offset: 0x00059430
				public IReadOnlyList<T> Path
				{
					get
					{
						return this.path;
					}
				}

				// Token: 0x17000841 RID: 2113
				// (get) Token: 0x06002207 RID: 8711 RVA: 0x0005B238 File Offset: 0x00059438
				public bool IsReversible
				{
					get
					{
						return this.isReversible && this.path.Count > 1;
					}
				}

				// Token: 0x06002208 RID: 8712 RVA: 0x0005B252 File Offset: 0x00059452
				public void Add(T entity, bool isReversible)
				{
					this.path.Add(entity);
					this.VisitedEntities.Add(entity);
					this.isReversible = this.isReversible && isReversible;
				}

				// Token: 0x06002209 RID: 8713 RVA: 0x0005B27B File Offset: 0x0005947B
				public void Prepend(T entity)
				{
					this.path.Insert(0, entity);
					this.VisitedEntities.Add(entity);
				}

				// Token: 0x0600220A RID: 8714 RVA: 0x0005B298 File Offset: 0x00059498
				public bool TryPrepend(QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath subPath)
				{
					if (this.IsReversible)
					{
						if (subPath.IsReversible)
						{
							return false;
						}
						T t = this.path.Last<T>();
						if (t.Equals(subPath.path.Last<T>()))
						{
							this.Reverse();
						}
						else
						{
							t = this.path[0];
							if (!t.Equals(subPath.path.Last<T>()))
							{
								return false;
							}
						}
					}
					else
					{
						T t;
						if (subPath.IsReversible)
						{
							t = this.path[0];
							if (t.Equals(subPath.path[0]))
							{
								subPath.Reverse();
								goto IL_00E1;
							}
						}
						t = this.path[0];
						if (!t.Equals(subPath.path.Last<T>()))
						{
							return false;
						}
					}
					IL_00E1:
					bool flag = this.isReversible;
					this.isReversible = false;
					this.path = subPath.path.Concat(this.path.Skip(1)).ToList<T>();
					this.VisitedEntities.UnionWith(subPath.path);
					return true;
				}

				// Token: 0x0600220B RID: 8715 RVA: 0x0005B3C8 File Offset: 0x000595C8
				internal void Reverse()
				{
					this.path.Reverse();
				}

				// Token: 0x0600220C RID: 8716 RVA: 0x0005B3D5 File Offset: 0x000595D5
				public static QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath CreatePath(HashSet<T> entitiesToFind, IEqualityComparer<T> comparer)
				{
					return new QueryAlgorithms.DetermineHierarchicalFilterPathAlgorithm<T>.ReversibleEntityPath
					{
						path = new List<T>(entitiesToFind.Count),
						VisitedEntities = new HashSet<T>(comparer),
						isReversible = true
					};
				}

				// Token: 0x040014BD RID: 5309
				private List<T> path;

				// Token: 0x040014BE RID: 5310
				public HashSet<T> VisitedEntities;

				// Token: 0x040014BF RID: 5311
				private bool isReversible;
			}
		}

		// Token: 0x0200032F RID: 815
		private static class DetectBaseEntitySetsAlgorithm
		{
			// Token: 0x06001E30 RID: 7728 RVA: 0x00053748 File Offset: 0x00051948
			internal static BaseEntitySets Invoke(IEnumerable<EntitySet> referencedEntities, IDirectedGraph<EntitySet> assocsFromOneGraph)
			{
				KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[][] array = (from e in referencedEntities.Distinct<EntitySet>()
					select QueryAlgorithms.DetectBaseEntitySetsAlgorithm.DetectBaseEntitiesForOne(e, assocsFromOneGraph)).ToArray<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[]>();
				if (!array.Any<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[]>())
				{
					return BaseEntitySets.Empty;
				}
				List<EntitySet> baseEntitiesIntersection = array.Select((KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[] entities) => entities.Select((KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>> e) => e.Key)).IntersectAll(null).ToList<EntitySet>();
				List<EntitySet> list = new List<EntitySet>(baseEntitiesIntersection);
				Func<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>, bool> <>9__3;
				for (int i = 0; i < array.Length; i++)
				{
					KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[][] array2 = array;
					int num = i;
					IEnumerable<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>> enumerable = array[i];
					Func<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>> e) => baseEntitiesIntersection.Contains(e.Key));
					}
					array2[num] = enumerable.Where(func).ToArray<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>>();
				}
				for (int j = 0; j < baseEntitiesIntersection.Count; j++)
				{
					EntitySet entity = baseEntitiesIntersection[j];
					if (array.Select((KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[] baseEntities) => QueryAlgorithms.DetectBaseEntitySetsAlgorithm.DetermineEntitiesExtendedBy(entity, baseEntities.SelectMany((KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>> e) => e.Value))).IntersectAll(null).Any<EntitySet>())
					{
						baseEntitiesIntersection.RemoveAt(j);
						j--;
					}
				}
				return new BaseEntitySets(list.ToReadOnlyCollection<EntitySet>(), baseEntitiesIntersection.ToReadOnlyCollection<EntitySet>());
			}

			// Token: 0x06001E31 RID: 7729 RVA: 0x0005388C File Offset: 0x00051A8C
			private static KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>[] DetectBaseEntitiesForOne(EntitySet referenceEntity, IDirectedGraph<EntitySet> assocsFromOneGraph)
			{
				Dictionary<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>> dictionary = new Dictionary<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>();
				Dictionary<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>> dictionary2 = new Dictionary<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>();
				Queue<EntitySet> queue = new Queue<EntitySet>();
				dictionary2.Add(referenceEntity, new List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>
				{
					new QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath(new EntitySet[] { referenceEntity })
				});
				queue.Enqueue(referenceEntity);
				while (queue.Count > 0)
				{
					EntitySet entitySet = queue.Dequeue();
					List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath> list = dictionary2[entitySet];
					dictionary2.Remove(entitySet);
					dictionary.Add(entitySet, list);
					IReadOnlyList<EntitySet> readOnlyList;
					if (assocsFromOneGraph.TryGetEdgesFromVertex(entitySet, out readOnlyList))
					{
						using (IEnumerator<EntitySet> enumerator = readOnlyList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								EntitySet relatedEntity = enumerator.Current;
								if (!dictionary.ContainsKey(relatedEntity))
								{
									List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath> list2 = list.Select((QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath p) => p.Extend(new EntitySet[] { relatedEntity })).ToList<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>();
									List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath> list3;
									if (dictionary2.TryGetValue(relatedEntity, out list3))
									{
										list3.AddRange(list2);
									}
									else
									{
										dictionary2.Add(relatedEntity, list2);
										queue.Enqueue(relatedEntity);
									}
								}
							}
						}
					}
				}
				return dictionary.ToArray<KeyValuePair<EntitySet, List<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>>>();
			}

			// Token: 0x06001E32 RID: 7730 RVA: 0x000539CC File Offset: 0x00051BCC
			private static IEnumerable<EntitySet> DetermineEntitiesExtendedBy(EntitySet targetEntity, IEnumerable<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath> paths)
			{
				IEnumerable<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath> enumerable = paths.Where((QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath p) => p.Target.Equals(targetEntity));
				List<EntitySet> list = new List<EntitySet>();
				using (IEnumerator<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath targetPath = enumerator.Current;
						IEnumerable<EntitySet> enumerable2 = from p in paths
							where targetPath.Extends(p)
							select p.Target;
						list.AddRange(enumerable2);
					}
				}
				return list;
			}

			// Token: 0x0200043A RID: 1082
			private sealed class EntityPath : IEnumerable<EntitySet>, IEnumerable
			{
				// Token: 0x06002212 RID: 8722 RVA: 0x0005B43D File Offset: 0x0005963D
				internal EntityPath(params EntitySet[] path)
				{
					this._path = path.ToReadOnlyCollection<EntitySet>();
				}

				// Token: 0x06002213 RID: 8723 RVA: 0x0005B451 File Offset: 0x00059651
				internal EntityPath(IEnumerable<EntitySet> path)
				{
					this._path = path.ToReadOnlyCollection<EntitySet>();
				}

				// Token: 0x17000842 RID: 2114
				// (get) Token: 0x06002214 RID: 8724 RVA: 0x0005B465 File Offset: 0x00059665
				public EntitySet Target
				{
					get
					{
						return this._path.Last<EntitySet>();
					}
				}

				// Token: 0x17000843 RID: 2115
				// (get) Token: 0x06002215 RID: 8725 RVA: 0x0005B472 File Offset: 0x00059672
				private int Length
				{
					get
					{
						return this._path.Count;
					}
				}

				// Token: 0x06002216 RID: 8726 RVA: 0x0005B47F File Offset: 0x0005967F
				public QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath Extend(params EntitySet[] extendPath)
				{
					ArgumentValidation.CheckNotNull<EntitySet[]>(extendPath, "extendPath");
					return new QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath(this._path.Concat(extendPath));
				}

				// Token: 0x06002217 RID: 8727 RVA: 0x0005B49E File Offset: 0x0005969E
				public bool Extends(QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath otherPath)
				{
					ArgumentValidation.CheckNotNull<QueryAlgorithms.DetectBaseEntitySetsAlgorithm.EntityPath>(otherPath, "otherPath");
					return this.Length > otherPath.Length && this._path.Take(otherPath.Length).SequenceEqual(otherPath);
				}

				// Token: 0x06002218 RID: 8728 RVA: 0x0005B4D3 File Offset: 0x000596D3
				public IEnumerator<EntitySet> GetEnumerator()
				{
					return this._path.GetEnumerator();
				}

				// Token: 0x06002219 RID: 8729 RVA: 0x0005B4E0 File Offset: 0x000596E0
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this._path.GetEnumerator();
				}

				// Token: 0x040014C5 RID: 5317
				private readonly ReadOnlyCollection<EntitySet> _path;
			}
		}

		// Token: 0x02000330 RID: 816
		private static class DetectBaseEntitiesAlgorithm
		{
			// Token: 0x06001E33 RID: 7731 RVA: 0x00053A80 File Offset: 0x00051C80
			internal static BaseConceptualEntities Invoke(IEnumerable<IConceptualEntity> referencedEntities, IDirectedGraph<IConceptualEntity> assocsFromOneGraph)
			{
				KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[][] array = (from e in referencedEntities.Distinct(ConceptualEntityExtensionAwareEqualityComparer.Instance)
					select QueryAlgorithms.DetectBaseEntitiesAlgorithm.DetectBaseEntitiesForOne(e.GetBaseEntityOrSelf(), assocsFromOneGraph)).ToArray<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[]>();
				if (!array.Any<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[]>())
				{
					return BaseConceptualEntities.Empty;
				}
				List<IConceptualEntity> baseEntitiesIntersection = array.Select((KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[] entities) => entities.Select((KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>> e) => e.Key)).IntersectAll(ConceptualEntityExtensionAwareEqualityComparer.Instance).ToList<IConceptualEntity>();
				List<IConceptualEntity> list = new List<IConceptualEntity>(baseEntitiesIntersection);
				Func<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>, bool> <>9__3;
				for (int i = 0; i < array.Length; i++)
				{
					KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[][] array2 = array;
					int num = i;
					IEnumerable<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>> enumerable = array[i];
					Func<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>> e) => baseEntitiesIntersection.Contains(e.Key));
					}
					array2[num] = enumerable.Where(func).ToArray<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>>();
				}
				for (int j = 0; j < baseEntitiesIntersection.Count; j++)
				{
					IConceptualEntity entity = baseEntitiesIntersection[j];
					if (array.Select((KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[] baseEntities) => QueryAlgorithms.DetectBaseEntitiesAlgorithm.DetermineEntitiesExtendedBy(entity, baseEntities.SelectMany((KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>> e) => e.Value))).IntersectAll(ConceptualEntityExtensionAwareEqualityComparer.Instance).Any<IConceptualEntity>())
					{
						baseEntitiesIntersection.RemoveAt(j);
						j--;
					}
				}
				return new BaseConceptualEntities(list, baseEntitiesIntersection);
			}

			// Token: 0x06001E34 RID: 7732 RVA: 0x00053BC8 File Offset: 0x00051DC8
			private static KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>[] DetectBaseEntitiesForOne(IConceptualEntity referenceEntity, IDirectedGraph<IConceptualEntity> assocsFromOneGraph)
			{
				Dictionary<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>> dictionary = new Dictionary<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
				Dictionary<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>> dictionary2 = new Dictionary<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>(ConceptualEntityExtensionAwareEqualityComparer.Instance);
				Queue<IConceptualEntity> queue = new Queue<IConceptualEntity>();
				dictionary2.Add(referenceEntity, new List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>
				{
					new QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath(new IConceptualEntity[] { referenceEntity })
				});
				queue.Enqueue(referenceEntity);
				while (queue.Count > 0)
				{
					IConceptualEntity conceptualEntity = queue.Dequeue();
					List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath> list = dictionary2[conceptualEntity];
					dictionary2.Remove(conceptualEntity);
					dictionary.Add(conceptualEntity, list);
					IReadOnlyList<IConceptualEntity> readOnlyList;
					if (assocsFromOneGraph.TryGetEdgesFromVertex(conceptualEntity, out readOnlyList))
					{
						using (IEnumerator<IConceptualEntity> enumerator = readOnlyList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								IConceptualEntity relatedEntity = enumerator.Current;
								if (!dictionary.ContainsKey(relatedEntity))
								{
									List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath> list2 = list.Select((QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath p) => p.Extend(new IConceptualEntity[] { relatedEntity })).ToList<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>();
									List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath> list3;
									if (dictionary2.TryGetValue(relatedEntity, out list3))
									{
										list3.AddRange(list2);
									}
									else
									{
										dictionary2.Add(relatedEntity, list2);
										queue.Enqueue(relatedEntity);
									}
								}
							}
						}
					}
				}
				return dictionary.ToArray<KeyValuePair<IConceptualEntity, List<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>>>();
			}

			// Token: 0x06001E35 RID: 7733 RVA: 0x00053D10 File Offset: 0x00051F10
			private static IEnumerable<IConceptualEntity> DetermineEntitiesExtendedBy(IConceptualEntity targetEntity, IEnumerable<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath> paths)
			{
				IEnumerable<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath> enumerable = paths.Where((QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath p) => ConceptualEntityExtensionAwareEqualityComparer.Instance.Equals(p.Target, targetEntity));
				List<IConceptualEntity> list = new List<IConceptualEntity>();
				using (IEnumerator<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath targetPath = enumerator.Current;
						IEnumerable<IConceptualEntity> enumerable2 = from p in paths
							where targetPath.Extends(p)
							select p.Target;
						list.AddRange(enumerable2);
					}
				}
				return list;
			}

			// Token: 0x02000441 RID: 1089
			private sealed class EntityPath : IEnumerable<IConceptualEntity>, IEnumerable
			{
				// Token: 0x0600222B RID: 8747 RVA: 0x0005B5F6 File Offset: 0x000597F6
				internal EntityPath(params IConceptualEntity[] path)
				{
					this._path = path;
				}

				// Token: 0x0600222C RID: 8748 RVA: 0x0005B605 File Offset: 0x00059805
				internal EntityPath(IEnumerable<IConceptualEntity> path)
				{
					this._path = path.ToReadOnlyList<IConceptualEntity>();
				}

				// Token: 0x17000844 RID: 2116
				// (get) Token: 0x0600222D RID: 8749 RVA: 0x0005B619 File Offset: 0x00059819
				public IConceptualEntity Target
				{
					get
					{
						return this._path.Last<IConceptualEntity>();
					}
				}

				// Token: 0x17000845 RID: 2117
				// (get) Token: 0x0600222E RID: 8750 RVA: 0x0005B626 File Offset: 0x00059826
				private int Length
				{
					get
					{
						return this._path.Count;
					}
				}

				// Token: 0x0600222F RID: 8751 RVA: 0x0005B633 File Offset: 0x00059833
				public QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath Extend(params IConceptualEntity[] extendPath)
				{
					ArgumentValidation.CheckNotNull<IConceptualEntity[]>(extendPath, "extendPath");
					return new QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath(this._path.Concat(extendPath));
				}

				// Token: 0x06002230 RID: 8752 RVA: 0x0005B652 File Offset: 0x00059852
				public bool Extends(QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath otherPath)
				{
					ArgumentValidation.CheckNotNull<QueryAlgorithms.DetectBaseEntitiesAlgorithm.EntityPath>(otherPath, "otherPath");
					return this.Length > otherPath.Length && this._path.Take(otherPath.Length).SequenceEqual(otherPath, ConceptualEntityExtensionAwareEqualityComparer.Instance);
				}

				// Token: 0x06002231 RID: 8753 RVA: 0x0005B68C File Offset: 0x0005988C
				public IEnumerator<IConceptualEntity> GetEnumerator()
				{
					return this._path.GetEnumerator();
				}

				// Token: 0x06002232 RID: 8754 RVA: 0x0005B699 File Offset: 0x00059899
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this._path.GetEnumerator();
				}

				// Token: 0x040014D2 RID: 5330
				private readonly IReadOnlyList<IConceptualEntity> _path;
			}
		}

		// Token: 0x02000331 RID: 817
		private sealed class DaxScalarToIdentityComparisonsTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001E36 RID: 7734 RVA: 0x00053DC4 File Offset: 0x00051FC4
			internal static QueryExpression Invoke(QueryExpression expression)
			{
				return expression.Accept<QueryExpression>(new QueryAlgorithms.DaxScalarToIdentityComparisonsTransform());
			}

			// Token: 0x06001E37 RID: 7735 RVA: 0x00053DD1 File Offset: 0x00051FD1
			private DaxScalarToIdentityComparisonsTransform()
			{
			}

			// Token: 0x06001E38 RID: 7736 RVA: 0x00053DDC File Offset: 0x00051FDC
			protected internal override QueryExpression Visit(QueryComparisonExpression expression)
			{
				if (QueryAlgorithms.ShouldUseDaxIdentityComparison(expression.Left, expression.Right))
				{
					QueryComparisonKind comparisonKind = expression.ComparisonKind;
					if (comparisonKind == QueryComparisonKind.Equals)
					{
						return expression.And(expression.Left.IsNull().Equal(expression.Right.IsNull()));
					}
					if (comparisonKind == QueryComparisonKind.NotEquals)
					{
						return expression.Or(expression.Left.IsNull().NotEqual(expression.Right.IsNull()));
					}
				}
				return base.Visit(expression);
			}
		}

		// Token: 0x02000332 RID: 818
		internal sealed class DaxInExpressionComparisonsTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001E39 RID: 7737 RVA: 0x00053E57 File Offset: 0x00052057
			internal static QueryInExpression Invoke(QueryExpression expression, CancellationToken cancellationToken)
			{
				return (QueryInExpression)expression.Accept<QueryExpression>(new QueryAlgorithms.DaxInExpressionComparisonsTransform(cancellationToken));
			}

			// Token: 0x06001E3A RID: 7738 RVA: 0x00053E6A File Offset: 0x0005206A
			private DaxInExpressionComparisonsTransform(CancellationToken cancellationToken)
			{
				this._cancellationToken = cancellationToken;
			}

			// Token: 0x06001E3B RID: 7739 RVA: 0x00053E7C File Offset: 0x0005207C
			internal static bool ShouldUseInWithEqualitySemantics(QueryInExpression expression)
			{
				if (expression.IsStrict)
				{
					return true;
				}
				IReadOnlyList<QueryExpression> expressions = expression.Expressions;
				IReadOnlyList<IReadOnlyList<QueryExpression>> values = expression.Values;
				if (values.Count > 1)
				{
					return true;
				}
				if (expressions.Count <= DataShapeEngineConfig.MaxNumberOfInFilterTupleValuesWithDefaults)
				{
					return true;
				}
				int num = 0;
				for (int i = 0; i < expressions.Count; i++)
				{
					if (num > DataShapeEngineConfig.MaxNumberOfInFilterTupleValuesWithDefaults)
					{
						return false;
					}
					QueryExpression queryExpression = expressions[i];
					if (QueryAlgorithms.RequiresEqualitySemantics(queryExpression))
					{
						QueryExpression queryExpression2 = values[0][i];
						QueryLiteralExpression typedZero = Literals.GetTypedZero(queryExpression.ConceptualResultType);
						if (QueryAlgorithms.DaxInExpressionComparisonsTransform.NeedsEqualityPair(queryExpression2, typedZero))
						{
							num++;
						}
					}
				}
				return num <= DataShapeEngineConfig.MaxNumberOfInFilterTupleValuesWithDefaults;
			}

			// Token: 0x06001E3C RID: 7740 RVA: 0x00053F20 File Offset: 0x00052120
			protected internal override QueryExpression Visit(QueryInExpression expression)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				IReadOnlyList<QueryExpression> expressions = expression.Expressions;
				IReadOnlyList<IReadOnlyList<QueryExpression>> values = expression.Values;
				List<IReadOnlyList<QueryExpression>> list = null;
				int num = 0;
				for (int i = 0; i < expressions.Count; i++)
				{
					QueryExpression queryExpression = expressions[i];
					if (QueryAlgorithms.RequiresEqualitySemantics(queryExpression))
					{
						QueryLiteralExpression typedZero = Literals.GetTypedZero(queryExpression.ConceptualResultType);
						IReadOnlyList<IReadOnlyList<QueryExpression>> readOnlyList = list;
						IReadOnlyList<IReadOnlyList<QueryExpression>> readOnlyList2 = readOnlyList ?? values;
						HashSet<IReadOnlyList<QueryExpression>> hashSet = this.BuildEqualityPairsForIndex(readOnlyList2, i, queryExpression.ConceptualResultType, typedZero);
						if (hashSet.Count > 0)
						{
							if (list == null)
							{
								list = new List<IReadOnlyList<QueryExpression>>(values);
							}
							if (list.Count + hashSet.Count > 30000)
							{
								throw new CommandTreeTranslationException("The count of expressions in the Values argument to the In operator exceeds the maximum number allowed.", CommandTreeTranslationErrorCode.InvalidFilterExceedsMaxNumberOfValuesForInFilter);
							}
							list.AddRange(hashSet);
							num += hashSet.Count;
							if (num >= 5000)
							{
								this._cancellationToken.ThrowIfCancellationRequested();
								num = 0;
							}
						}
					}
				}
				if (list == null)
				{
					return expression;
				}
				return expression.Expressions.In(list, expression.IsStrict);
			}

			// Token: 0x06001E3D RID: 7741 RVA: 0x00054028 File Offset: 0x00052228
			private HashSet<IReadOnlyList<QueryExpression>> BuildEqualityPairsForIndex(IReadOnlyList<IReadOnlyList<QueryExpression>> values, int index, ConceptualResultType conceptualResultType, QueryLiteralExpression defaultLiteralForType)
			{
				HashSet<IReadOnlyList<QueryExpression>> hashSet = new HashSet<IReadOnlyList<QueryExpression>>(new QueryAlgorithms.DaxInExpressionComparisonsTransform.ExpressionListComparer());
				HashSet<IReadOnlyList<QueryExpression>> hashSet2 = new HashSet<IReadOnlyList<QueryExpression>>(new QueryAlgorithms.DaxInExpressionComparisonsTransform.ExpressionListComparer());
				foreach (IReadOnlyList<QueryExpression> readOnlyList in values)
				{
					QueryExpression queryExpression = readOnlyList[index];
					if (queryExpression.IsDaxLiteralBlank())
					{
						hashSet2.Add(readOnlyList);
						hashSet.Remove(readOnlyList);
					}
					else if (QueryAlgorithms.DaxInExpressionComparisonsTransform.NeedsEqualityPair(queryExpression, defaultLiteralForType))
					{
						List<QueryExpression> list = this.BuildEqualityPair(readOnlyList, index, conceptualResultType);
						if (!hashSet2.Contains(list))
						{
							hashSet.Add(list);
						}
					}
				}
				return hashSet;
			}

			// Token: 0x06001E3E RID: 7742 RVA: 0x000540D0 File Offset: 0x000522D0
			private static bool NeedsEqualityPair(QueryExpression value, QueryLiteralExpression defaultLiteralForType)
			{
				return defaultLiteralForType.Equals(value) || QueryAlgorithms.DaxInExpressionComparisonsTransform.ExpressionListComparer.AreCrossTypeEqual(defaultLiteralForType, value);
			}

			// Token: 0x06001E3F RID: 7743 RVA: 0x000540E4 File Offset: 0x000522E4
			private List<QueryExpression> BuildEqualityPair(IReadOnlyList<QueryExpression> values, int indexOfPair, ConceptualResultType conceptualResultType)
			{
				List<QueryExpression> list = new List<QueryExpression>(values.Count);
				for (int i = 0; i < values.Count; i++)
				{
					if (i == indexOfPair)
					{
						list.Add(conceptualResultType.Null());
					}
					else
					{
						list.Add(values[i]);
					}
				}
				return list;
			}

			// Token: 0x040011A3 RID: 4515
			private readonly CancellationToken _cancellationToken;

			// Token: 0x02000448 RID: 1096
			internal class ExpressionListComparer : IEqualityComparer<IReadOnlyList<QueryExpression>>
			{
				// Token: 0x06002244 RID: 8772 RVA: 0x0005B7BC File Offset: 0x000599BC
				public bool Equals(IReadOnlyList<QueryExpression> x, IReadOnlyList<QueryExpression> y)
				{
					bool? flag = CompareUtil.AreEqual<IReadOnlyList<QueryExpression>, IReadOnlyList<QueryExpression>>(x, y);
					if (flag != null)
					{
						return flag.Value;
					}
					if (x.Count != y.Count)
					{
						return false;
					}
					for (int i = 0; i < x.Count; i++)
					{
						if (!x[i].Equals(y[i]) && !QueryAlgorithms.DaxInExpressionComparisonsTransform.ExpressionListComparer.AreCrossTypeEqual(x[i], y[i]))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x06002245 RID: 8773 RVA: 0x0005B830 File Offset: 0x00059A30
				public int GetHashCode(IReadOnlyList<QueryExpression> obj)
				{
					if (obj == null)
					{
						return -48879;
					}
					return Microsoft.DataShaping.Common.Hashing.CombineHashReadonly<QueryExpression>(obj, null);
				}

				// Token: 0x06002246 RID: 8774 RVA: 0x0005B844 File Offset: 0x00059A44
				internal static bool AreCrossTypeEqual(QueryExpression x, QueryExpression y)
				{
					if (x.IsDaxLiteralBlank() && y.IsDaxLiteralBlank())
					{
						return true;
					}
					QueryLiteralExpression queryLiteralExpression = x as QueryLiteralExpression;
					QueryLiteralExpression queryLiteralExpression2 = y as QueryLiteralExpression;
					return queryLiteralExpression != null && queryLiteralExpression2 != null && QueryAlgorithms.DaxInExpressionComparisonsTransform.ExpressionListComparer.IsAcceptedNumericTypeDefault(queryLiteralExpression) && QueryAlgorithms.DaxInExpressionComparisonsTransform.ExpressionListComparer.IsAcceptedNumericTypeDefault(queryLiteralExpression2);
				}

				// Token: 0x06002247 RID: 8775 RVA: 0x0005B889 File Offset: 0x00059A89
				private static bool IsAcceptedNumericTypeDefault(QueryLiteralExpression expr)
				{
					return expr.ConceptualResultType.IsNumeric() && expr.IsDaxLiteralBlankEquivalent();
				}
			}
		}

		// Token: 0x02000333 RID: 819
		internal sealed class DaxInExpressionDedupingTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001E40 RID: 7744 RVA: 0x0005412E File Offset: 0x0005232E
			internal static QueryInExpression Invoke(QueryExpression expression, IDataComparer comparer)
			{
				return (QueryInExpression)expression.Accept<QueryExpression>(new QueryAlgorithms.DaxInExpressionDedupingTransform(comparer));
			}

			// Token: 0x06001E41 RID: 7745 RVA: 0x00054141 File Offset: 0x00052341
			private DaxInExpressionDedupingTransform(IDataComparer comparer)
			{
				this._comparer = comparer;
			}

			// Token: 0x06001E42 RID: 7746 RVA: 0x00054150 File Offset: 0x00052350
			protected internal override QueryExpression Visit(QueryInExpression expression)
			{
				if (expression.Expressions.Count == 1)
				{
					return expression;
				}
				IReadOnlyList<QueryExpression> expressions = expression.Expressions;
				IReadOnlyList<IReadOnlyList<QueryExpression>> values = expression.Values;
				List<QueryExpression> list = null;
				int[] array = null;
				Dictionary<QueryExpression, int> dictionary = new Dictionary<QueryExpression, int>();
				for (int i = 0; i < expressions.Count; i++)
				{
					QueryExpression queryExpression = expressions[i];
					int num;
					if (dictionary.TryGetValue(queryExpression, out num))
					{
						if (list == null)
						{
							list = this.CreateCopyUntilIndex<QueryExpression>(expressions, i, out array);
						}
						array[i] = num;
					}
					else
					{
						dictionary.Add(queryExpression, i);
						if (array != null)
						{
							array[i] = i;
						}
						if (list != null)
						{
							list.Add(queryExpression);
						}
					}
				}
				if (list == null)
				{
					return expression;
				}
				List<IReadOnlyList<QueryExpression>> list2 = new List<IReadOnlyList<QueryExpression>>(values.Count);
				foreach (IReadOnlyList<QueryExpression> readOnlyList in values)
				{
					List<QueryExpression> list3 = new List<QueryExpression>(list.Count);
					for (int j = 0; j < readOnlyList.Count; j++)
					{
						int num2 = array[j];
						if (num2 == j)
						{
							list3.Add(readOnlyList[j]);
						}
						else if (!this.AreEqual(readOnlyList[num2], readOnlyList[j]))
						{
							throw new CommandTreeTranslationException("The duplicate expressions in the In filter do not have identical values. They cannot be deduped.", CommandTreeTranslationErrorCode.InvalidInFilterWithDuplicateColumns);
						}
					}
					list2.Add(list3);
				}
				return list.In(list2, expression.IsStrict);
			}

			// Token: 0x06001E43 RID: 7747 RVA: 0x000542B8 File Offset: 0x000524B8
			private List<T> CreateCopyUntilIndex<T>(IReadOnlyList<T> original, int index, out int[] indicesArray)
			{
				List<T> list = new List<T>(original.Count);
				indicesArray = new int[original.Count];
				for (int i = 0; i < index; i++)
				{
					list.Add(original[i]);
					indicesArray[i] = i;
				}
				return list;
			}

			// Token: 0x06001E44 RID: 7748 RVA: 0x00054300 File Offset: 0x00052500
			private bool AreEqual(QueryExpression expr1, QueryExpression expr2)
			{
				if (expr1 is QueryNullExpression && expr2 is QueryNullExpression)
				{
					return true;
				}
				object obj;
				object obj2;
				if (this.TryExtractLiteralValue(expr1, out obj) && this.TryExtractLiteralValue(expr2, out obj2))
				{
					return this._comparer.Equals(obj, obj2);
				}
				return expr1.Equals(expr2);
			}

			// Token: 0x06001E45 RID: 7749 RVA: 0x0005434C File Offset: 0x0005254C
			private bool TryExtractLiteralValue(QueryExpression expr, out object value)
			{
				value = null;
				QueryLiteralExpression queryLiteralExpression = expr as QueryLiteralExpression;
				if (queryLiteralExpression == null)
				{
					return false;
				}
				value = queryLiteralExpression.Value.Value;
				return true;
			}

			// Token: 0x040011A4 RID: 4516
			private readonly IDataComparer _comparer;
		}

		// Token: 0x02000334 RID: 820
		private static class IsOnOrAfterToComparisonsTransform
		{
			// Token: 0x06001E46 RID: 7750 RVA: 0x0005437C File Offset: 0x0005257C
			internal static QueryExpression Invoke(QueryIsOnOrAfterExpression isOnOrAfter)
			{
				QueryExpression queryExpression = null;
				ReadOnlyCollection<QueryIsOnOrAfterArgument> arguments = isOnOrAfter.Arguments;
				for (int i = arguments.Count - 1; i >= 0; i--)
				{
					QueryIsOnOrAfterArgument queryIsOnOrAfterArgument = arguments[i];
					QueryExpression queryExpression2 = queryIsOnOrAfterArgument.Left;
					QueryExpression right = queryIsOnOrAfterArgument.Right;
					ConceptualPrimitiveType? primitiveTypeKind = queryExpression2.ConceptualResultType.GetPrimitiveTypeKind();
					ConceptualPrimitiveType? primitiveTypeKind2 = right.ConceptualResultType.GetPrimitiveTypeKind();
					if (!((primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2.GetValueOrDefault()) & (primitiveTypeKind != null == (primitiveTypeKind2 != null))))
					{
						queryExpression2 = queryExpression2.Cast(right.ConceptualResultType);
					}
					if (queryExpression == null)
					{
						queryExpression = QueryAlgorithms.IsOnOrAfterToComparisonsTransform.GetOrEqualComparison(queryIsOnOrAfterArgument.Direction)(queryExpression2, right);
					}
					else
					{
						queryExpression = QueryAlgorithms.IsOnOrAfterToComparisonsTransform.GetComparison(queryIsOnOrAfterArgument.Direction)(queryExpression2, right).Or(QueryStrictOperators.EqualStrict(queryExpression2, right).And(queryExpression));
					}
				}
				return queryExpression;
			}

			// Token: 0x06001E47 RID: 7751 RVA: 0x00054456 File Offset: 0x00052656
			private static Func<QueryExpression, QueryExpression, QueryExpression> GetOrEqualComparison(SortDirection direction)
			{
				if (direction == SortDirection.Ascending)
				{
					Func<QueryExpression, QueryExpression, QueryExpression> func;
					if ((func = QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<0>__GreaterThanOrEqualStrict) == null)
					{
						func = (QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<0>__GreaterThanOrEqualStrict = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryStrictOperators.GreaterThanOrEqualStrict));
					}
					return func;
				}
				Func<QueryExpression, QueryExpression, QueryExpression> func2;
				if ((func2 = QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<1>__LessThanOrEqualStrict) == null)
				{
					func2 = (QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<1>__LessThanOrEqualStrict = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryStrictOperators.LessThanOrEqualStrict));
				}
				return func2;
			}

			// Token: 0x06001E48 RID: 7752 RVA: 0x00054492 File Offset: 0x00052692
			private static Func<QueryExpression, QueryExpression, QueryExpression> GetComparison(SortDirection direction)
			{
				if (direction == SortDirection.Ascending)
				{
					Func<QueryExpression, QueryExpression, QueryExpression> func;
					if ((func = QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<2>__GreaterThanStrict) == null)
					{
						func = (QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<2>__GreaterThanStrict = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryStrictOperators.GreaterThanStrict));
					}
					return func;
				}
				Func<QueryExpression, QueryExpression, QueryExpression> func2;
				if ((func2 = QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<3>__LessThanStrict) == null)
				{
					func2 = (QueryAlgorithms.IsOnOrAfterToComparisonsTransform.<>O.<3>__LessThanStrict = new Func<QueryExpression, QueryExpression, QueryExpression>(QueryStrictOperators.LessThanStrict));
				}
				return func2;
			}

			// Token: 0x02000449 RID: 1097
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040014DF RID: 5343
				public static Func<QueryExpression, QueryExpression, QueryExpression> <0>__GreaterThanOrEqualStrict;

				// Token: 0x040014E0 RID: 5344
				public static Func<QueryExpression, QueryExpression, QueryExpression> <1>__LessThanOrEqualStrict;

				// Token: 0x040014E1 RID: 5345
				public static Func<QueryExpression, QueryExpression, QueryExpression> <2>__GreaterThanStrict;

				// Token: 0x040014E2 RID: 5346
				public static Func<QueryExpression, QueryExpression, QueryExpression> <3>__LessThanStrict;
			}
		}

		// Token: 0x02000335 RID: 821
		private static class IsAfterToComparisonsTransform
		{
			// Token: 0x06001E49 RID: 7753 RVA: 0x000544D0 File Offset: 0x000526D0
			internal static QueryExpression Invoke(QueryIsAfterExpression isAfter)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.IsOnOrAfter(isAfter.Arguments);
				ReadOnlyCollection<QueryIsOnOrAfterArgument> arguments = isAfter.Arguments;
				List<QueryExpression> list = new List<QueryExpression>(arguments.Count);
				List<QueryExpression> list2 = new List<QueryExpression>(arguments.Count);
				foreach (QueryIsOnOrAfterArgument queryIsOnOrAfterArgument in arguments)
				{
					list.Add(queryIsOnOrAfterArgument.Left);
					list2.Add(queryIsOnOrAfterArgument.Right);
				}
				QueryInExpression queryInExpression = list.In(list2.AsList<List<QueryExpression>>(), true);
				queryExpression = queryExpression.And(queryInExpression.Not());
				return queryExpression;
			}
		}

		// Token: 0x02000336 RID: 822
		private sealed class CountRowsToIsEmptyTransform : DefaultExpressionVisitor
		{
			// Token: 0x06001E4A RID: 7754 RVA: 0x00054578 File Offset: 0x00052778
			internal static QueryExpression Invoke(QueryExpression expression)
			{
				return expression.Accept<QueryExpression>(new QueryAlgorithms.CountRowsToIsEmptyTransform());
			}

			// Token: 0x06001E4B RID: 7755 RVA: 0x00054585 File Offset: 0x00052785
			private CountRowsToIsEmptyTransform()
			{
			}

			// Token: 0x06001E4C RID: 7756 RVA: 0x00054590 File Offset: 0x00052790
			protected internal override QueryExpression Visit(QueryFunctionExpression expression)
			{
				ArgumentValidation.CheckNotNull<QueryFunctionExpression>(expression, "expression");
				if (expression.Function.FullName == "Core.Not" && expression.Arguments.Count == 1)
				{
					QueryIsNullExpression queryIsNullExpression = expression.Arguments[0] as QueryIsNullExpression;
					if (queryIsNullExpression != null)
					{
						QueryCalculateExpression queryCalculateExpression = queryIsNullExpression.Argument as QueryCalculateExpression;
						if (queryCalculateExpression != null && queryCalculateExpression.Filters.Count == 0)
						{
							QueryCountRowsExpression queryCountRowsExpression = queryCalculateExpression.Argument as QueryCountRowsExpression;
							if (queryCountRowsExpression != null)
							{
								QueryScanExpression queryScanExpression = queryCountRowsExpression.Argument as QueryScanExpression;
								if (queryScanExpression != null)
								{
									return queryScanExpression.HasAnyRows(false);
								}
							}
						}
					}
				}
				return base.Visit(expression);
			}

			// Token: 0x06001E4D RID: 7757 RVA: 0x0005462C File Offset: 0x0005282C
			protected internal override QueryExpression Visit(QueryComparisonExpression expression)
			{
				ArgumentValidation.CheckNotNull<QueryComparisonExpression>(expression, "expression");
				QueryCalculateExpression queryCalculateExpression = expression.Left as QueryCalculateExpression;
				QueryLiteralExpression queryLiteralExpression = expression.Right as QueryLiteralExpression;
				QueryComparisonKind comparisonKind = expression.ComparisonKind;
				if (queryCalculateExpression != null && queryLiteralExpression != null && queryCalculateExpression.Filters.Count == 0 && (queryLiteralExpression.Value.Equals(0) || queryLiteralExpression.Value.Equals(0L)) && (comparisonKind == QueryComparisonKind.GreaterThan || comparisonKind == QueryComparisonKind.NotEquals || comparisonKind == QueryComparisonKind.NotEqualsIdentity))
				{
					QueryCountRowsExpression queryCountRowsExpression = queryCalculateExpression.Argument as QueryCountRowsExpression;
					if (queryCountRowsExpression != null)
					{
						QueryScanExpression queryScanExpression = queryCountRowsExpression.Argument as QueryScanExpression;
						if (queryScanExpression != null)
						{
							return queryScanExpression.HasAnyRows(false);
						}
					}
				}
				return base.Visit(expression);
			}
		}

		// Token: 0x02000337 RID: 823
		private sealed class CanMergeFilterPredicateVisitor : DefaultExpressionVisitor
		{
			// Token: 0x06001E4E RID: 7758 RVA: 0x000546E3 File Offset: 0x000528E3
			private CanMergeFilterPredicateVisitor()
			{
			}

			// Token: 0x06001E4F RID: 7759 RVA: 0x000546EC File Offset: 0x000528EC
			internal static bool Invoke(QueryExpression expression)
			{
				QueryAlgorithms.CanMergeFilterPredicateVisitor canMergeFilterPredicateVisitor = new QueryAlgorithms.CanMergeFilterPredicateVisitor();
				expression.Accept<QueryExpression>(canMergeFilterPredicateVisitor);
				return !canMergeFilterPredicateVisitor._isDisqualified;
			}

			// Token: 0x06001E50 RID: 7760 RVA: 0x00054710 File Offset: 0x00052910
			protected internal override QueryExpression Visit(QueryExtensionExpression expression)
			{
				if (expression is QdmTableColumnReferenceExpression)
				{
					return expression;
				}
				return base.Visit(expression);
			}

			// Token: 0x06001E51 RID: 7761 RVA: 0x00054723 File Offset: 0x00052923
			protected internal override QueryExpression Visit(QueryCalculateExpression expression)
			{
				this._isDisqualified = true;
				return expression;
			}

			// Token: 0x06001E52 RID: 7762 RVA: 0x0005472D File Offset: 0x0005292D
			protected internal override QueryExpression Visit(QueryMeasureExpression expression)
			{
				this._isDisqualified = true;
				return expression;
			}

			// Token: 0x040011A5 RID: 4517
			private bool _isDisqualified;
		}

		// Token: 0x02000338 RID: 824
		private sealed class UsesCurrentGroupVisitor : DefaultExpressionVisitor
		{
			// Token: 0x06001E53 RID: 7763 RVA: 0x00054737 File Offset: 0x00052937
			private UsesCurrentGroupVisitor()
			{
			}

			// Token: 0x06001E54 RID: 7764 RVA: 0x00054740 File Offset: 0x00052940
			internal static bool Invoke(QueryExpression expression)
			{
				QueryAlgorithms.UsesCurrentGroupVisitor usesCurrentGroupVisitor = new QueryAlgorithms.UsesCurrentGroupVisitor();
				expression.Accept<QueryExpression>(usesCurrentGroupVisitor);
				return usesCurrentGroupVisitor._usesCurrentGroup;
			}

			// Token: 0x06001E55 RID: 7765 RVA: 0x00054761 File Offset: 0x00052961
			protected internal override QueryExpression Visit(QueryCurrentGroupExpression expression)
			{
				this._usesCurrentGroup = true;
				return expression;
			}

			// Token: 0x040011A6 RID: 4518
			private bool _usesCurrentGroup;
		}
	}
}
