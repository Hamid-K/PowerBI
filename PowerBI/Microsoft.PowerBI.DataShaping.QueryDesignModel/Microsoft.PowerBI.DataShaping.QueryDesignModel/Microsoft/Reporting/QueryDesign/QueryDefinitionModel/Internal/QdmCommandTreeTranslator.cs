using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.BatchQueries;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000F6 RID: 246
	internal sealed class QdmCommandTreeTranslator
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x00023DE0 File Offset: 0x00021FE0
		private QdmCommandTreeTranslator(QueryDefinition queryDefinition, QueryConstraintValidator constraintValidator, IFeatureSwitchProvider featureSwitchProvider, ReadOnlyCollection<IJoinPredicate> fullJoinPredicates, bool crossJoinQueryPlan, bool composable, bool allowSummarizeColumns, CancellationToken cancellationToken)
		{
			this._queryDefinition = queryDefinition;
			this._daxCapabilities = (featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema) ? DaxCapabilitiesBuilder.BuildCapabilities(queryDefinition.ConceptualSchema) : queryDefinition.EntityDataModel.DaxCapabilities);
			this._constraintValidator = constraintValidator;
			this._featureSwitchProvider = featureSwitchProvider;
			this._fullJoinPredicates = fullJoinPredicates;
			this._hasUnanchoredJoinPredicates = this._fullJoinPredicates.EmptyIfNull<IJoinPredicate>().Any((IJoinPredicate p) => !p.IsAnchored);
			this._crossJoinQueryPlan = crossJoinQueryPlan;
			this._composable = composable;
			this._allowSummarizeColumns = allowSummarizeColumns;
			this._existsFilters = queryDefinition.ExistsFilters.ToReadOnlyCollection<QueryExpression>();
			this._cancellationToken = cancellationToken;
			this._useConceptualSchema = featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema);
			this._comparer = EntityDataModelExtensions.GetComparer(this._queryDefinition.EntityDataModel, this._queryDefinition.ConceptualSchema, this._featureSwitchProvider);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00023ED4 File Offset: 0x000220D4
		internal static QueryCommandTree Translate(QueryDefinition queryDefinition, QdmTranslationSettings settings, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken, QueryTrimmer getGroupsToTrimFromQuery = null)
		{
			ArgumentValidation.CheckNotNull<QueryDefinition>(queryDefinition, "queryDefinition");
			QdmCommandTreeTranslator.ValidateResultFieldNames(queryDefinition);
			queryDefinition = QueryAlgorithms.RemoveRedundantSlicerInSubqueries(queryDefinition, featureSwitchProvider, cancellationToken);
			bool flag = QdmCommandTreeTranslator.DetermineCrossJoinQueryPlanRequired(queryDefinition, settings);
			QueryConstraintValidator queryConstraintValidator = queryDefinition.CreateConstraintValidator(flag);
			ReadOnlyCollection<IJoinPredicate> readOnlyCollection = queryConstraintValidator.GetJoinPredicates().ToReadOnlyCollection<IJoinPredicate>();
			return new QdmCommandTreeTranslator(QueryAlgorithms.TrimUnneccessaryNonProjectedGroups(queryDefinition, flag, featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), getGroupsToTrimFromQuery), queryConstraintValidator, featureSwitchProvider, readOnlyCollection, flag, settings.Composable, settings.AllowSummarizeColumns, cancellationToken).TranslateCore();
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00023F4C File Offset: 0x0002214C
		private bool TryApplyingSummarizeColumns(out KeyValuePair<string, QueryExpression> querySummarizeColumns)
		{
			querySummarizeColumns = default(KeyValuePair<string, QueryExpression>);
			if (this._allowSummarizeColumns && !this._queryDefinition.Limits.Any<Limit>() && !this._queryDefinition.ExistsFilters.Any<QueryExpression>() && this._queryDefinition.ExplicitJoinPredicates == null && !(this._useConceptualSchema ? this._queryDefinition.ColumnsRequiringClearDefaultFilterContext.Any<IConceptualColumn>() : this._queryDefinition.FieldsRequiringClearDefaultFilterContext.Any<IEdmFieldInstance>()) && this._queryDefinition.PostRegroupLimit == null && this._queryDefinition.GroupFilter == null && this._queryDefinition.Rollup == null)
			{
				if (!this._queryDefinition.Groups.Any((Group x) => !x.IsProjected || x.FollowingJoinBehavior == FollowingJoinBehavior.OuterJoin || x.Details.Any<GroupDetail>()))
				{
					GroupAndJoinTableBuilder groupAndJoinTableBuilder = new GroupAndJoinTableBuilder(this._queryDefinition.EntityDataModel, this._queryDefinition.ConceptualSchema, this._queryDefinition.IncludeDirectManyToManyAssociations, this._featureSwitchProvider.IsEnabled(FeatureSwitchKind.QDMConceptualSchema), null, JoinPredicateBehavior.AutoPredicates, null);
					if (this._queryDefinition.Slicer != null)
					{
						foreach (QueryExpression queryExpression in this._queryDefinition.Slicer.Conditions.QdmFilters(this._queryDefinition.EntityDataModel, this._queryDefinition.ConceptualSchema, this._daxCapabilities, this._featureSwitchProvider, this._comparer, this._cancellationToken, ScanKind.InheritFilterContextIncludeBlankRow, false))
						{
							groupAndJoinTableBuilder.AddContextTable(queryExpression.AsTableDefinition(), false);
						}
					}
					foreach (Group group in this._queryDefinition.Groups)
					{
						IQueryTableGroupBuilder queryTableGroupBuilder = groupAndJoinTableBuilder.AddGroup();
						foreach (GroupKey groupKey in group.Keys)
						{
							queryTableGroupBuilder.AddGroupKey(groupKey.Expression, groupKey.Name);
						}
					}
					foreach (Measure measure in this._queryDefinition.Measures)
					{
						groupAndJoinTableBuilder.AddOrReuseColumn(measure.Expression, measure.Name, false);
					}
					querySummarizeColumns = groupAndJoinTableBuilder.ToQueryTable().Expression.As("GroupAndJoin");
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000241E0 File Offset: 0x000223E0
		private QueryCommandTree TranslateCore()
		{
			KeyValuePair<string, QueryExpression> keyValuePair;
			if (!this.TryApplyingSummarizeColumns(out keyValuePair))
			{
				QdmCommandTreeTranslator.BlankRowFilterContext blankRowFilterContext = QdmCommandTreeTranslator.DetermineBlankRowFilterRequired(this._queryDefinition, this._fullJoinPredicates);
				QdmCommandTreeTranslator.PostRegroupLimitContext andValidatePostRegroupLimit = QdmCommandTreeTranslator.GetAndValidatePostRegroupLimit(this._queryDefinition, blankRowFilterContext);
				KeyValuePair<string, QueryExpression> keyValuePair2 = this.BuildQueryGroups(andValidatePostRegroupLimit);
				KeyValuePair<string, QueryExpression> keyValuePair3 = keyValuePair2;
				if (andValidatePostRegroupLimit == null)
				{
					keyValuePair3 = QdmCommandTreeTranslator.ApplyQueryProjection(keyValuePair2, this._queryDefinition.Groups, this._queryDefinition.Measures, this._queryDefinition.Rollup, blankRowFilterContext);
				}
				KeyValuePair<string, QueryExpression> keyValuePair4 = QdmCommandTreeTranslator.ApplyQueryBlankRowFilter(QdmCommandTreeTranslator.ApplyTopLevelValueFilter(keyValuePair3, this._queryDefinition), this._queryDefinition, blankRowFilterContext, this._composable);
				KeyValuePair<string, QueryExpression> keyValuePair5 = this.ApplyQuerySlicer(keyValuePair4, this._queryDefinition.Slicer, this._queryDefinition.EntityDataModel, this._queryDefinition.ConceptualSchema, this._daxCapabilities, this._featureSwitchProvider, this._comparer, this._cancellationToken);
				keyValuePair5 = (this._useConceptualSchema ? this.ApplyClearDefaultFiltersAndApplyFilters(keyValuePair5, this._queryDefinition.ColumnsRequiringClearDefaultFilterContext, this._queryDefinition.ApplyFilters) : this.ApplyClearDefaultFiltersAndApplyFilters(keyValuePair5, this._queryDefinition.FieldsRequiringClearDefaultFilterContext, this._queryDefinition.ApplyFilters));
				keyValuePair = keyValuePair5;
			}
			bool flag = this._queryDefinition.TopLevelLimit != null;
			if (flag)
			{
				keyValuePair = QdmCommandTreeTranslator.ApplyStartAtAsFilter(keyValuePair, this._queryDefinition.Sorting, this._queryDefinition.StartAt);
			}
			keyValuePair = QdmCommandTreeTranslator.ApplyLimit(keyValuePair, this._queryDefinition.TopLevelLimit, this._queryDefinition.Sorting);
			QueryExpression queryExpression;
			if (this._composable)
			{
				queryExpression = keyValuePair.Value;
			}
			else
			{
				queryExpression = QdmCommandTreeTranslator.ApplyQuerySorting(keyValuePair, this._queryDefinition.Sorting).Value;
				if (!flag)
				{
					queryExpression = QdmCommandTreeTranslator.ApplyQueryStartAt(queryExpression, this._queryDefinition.StartAt);
				}
			}
			queryExpression = QueryAlgorithms.MergeGroupExpressions(queryExpression);
			if (!this._composable && (this._queryDefinition.Declarations.Count > 0 || this._queryDefinition.QueryParameters.Count > 0))
			{
				queryExpression = QueryExpressionBuilder.BatchRoot(this._queryDefinition.QueryParameters, this._queryDefinition.Declarations, queryExpression);
			}
			return new QueryCommandTree(this._queryDefinition.EntityDataModel, this._queryDefinition.ConceptualSchema, queryExpression, DaxCapabilitiesBuilder.BuildCapabilities(this._queryDefinition.EntityDataModel, this._queryDefinition.ConceptualSchema, this._featureSwitchProvider));
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00024423 File Offset: 0x00022623
		internal static bool ShouldUseCrossJoinQueryPlan(EntityDataModel model, IConceptualSchema schema, bool useConceptualSchema, QueryExpression measureExpression)
		{
			return QdmCommandTreeTranslator.CanCrossJoinIntroduceNewGroupTuples(model, schema, useConceptualSchema) && !Measure.IsMeasureExpressionAnchored(measureExpression);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002443A File Offset: 0x0002263A
		private static bool ShouldUseCrossJoinQueryPlan(EntityDataModel model, IConceptualSchema schema, bool useConceptualSchema, IEnumerable<IJoinPredicate> joinPredicates)
		{
			if (QdmCommandTreeTranslator.CanCrossJoinIntroduceNewGroupTuples(model, schema, useConceptualSchema))
			{
				if (joinPredicates.Any((IJoinPredicate p) => !p.IsAnchored))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00024470 File Offset: 0x00022670
		private static bool CanCrossJoinIntroduceNewGroupTuples(EntityDataModel model, IConceptualSchema schema, bool useConceptualSchema)
		{
			if (useConceptualSchema)
			{
				return !schema.GetDaxCapabilitiesAnnotation().AlwaysCrossFilteringWithinTable;
			}
			return model.ModelCapabilities.CrossFilteringWithinTable != CrossFilteringWithinTableType.Always;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00024498 File Offset: 0x00022698
		private static bool DetermineCrossJoinQueryPlanRequired(QueryDefinition queryDefinition, QdmTranslationSettings settings)
		{
			bool flag;
			if (settings.ForceCrossJoinQueryPlan)
			{
				flag = true;
			}
			else
			{
				IEnumerable<IJoinPredicate> enumerable = queryDefinition.ExplicitJoinPredicates ?? queryDefinition.DefaultMeasurePredicates;
				if (QdmCommandTreeTranslator.ShouldUseCrossJoinQueryPlan(queryDefinition.EntityDataModel, queryDefinition.ConceptualSchema, queryDefinition.UseConceptualSchema, enumerable))
				{
					flag = true;
				}
				else
				{
					IEnumerable<Group> enumerable2;
					QdmCommandTreeTranslator.GetFilteredGroup(queryDefinition, queryDefinition.Groups, out enumerable2);
					flag = enumerable2.Any<Group>();
				}
			}
			if (!queryDefinition.UseConceptualSchema)
			{
				GroupByValidationType groupByValidation = queryDefinition.EntityDataModel.ModelCapabilities.GroupByValidation;
			}
			else
			{
				bool enforcesGroupByValidation = queryDefinition.ConceptualSchema.GetDaxCapabilitiesAnnotation().EnforcesGroupByValidation;
			}
			return flag;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002452C File Offset: 0x0002272C
		private static QdmCommandTreeTranslator.PostRegroupLimitContext GetAndValidatePostRegroupLimit(QueryDefinition queryDefinition, QdmCommandTreeTranslator.BlankRowFilterContext blankRowFilterContext)
		{
			if (queryDefinition.PostRegroupLimit == null)
			{
				return null;
			}
			Group group = null;
			foreach (Group group2 in queryDefinition.Groups)
			{
				bool flag = queryDefinition.PostRegroupLimit.RefersTo(group2);
				if (group2.IsProjected)
				{
					if (group != null && !flag)
					{
						throw new QueryDefinitionTranslationException("The query contains a grouping structure that is not compatible with the post-regroup limit. The limit must cover all groups after the first limited group. Non-projected groups must be contiguous with the first limited group.");
					}
					if (group == null && flag)
					{
						group = group2;
					}
				}
				else if (group == null)
				{
					group = group2;
				}
				if (!flag && queryDefinition.GetAggregateIndicatorNameForGroup(group2) != null)
				{
					throw new QueryDefinitionTranslationException("The query contains a Rollup that refers to a group not covered by the post-regroup Limit. When the query has a post-regroup limit, all Rollups may only refer to groups inside that Limit.");
				}
			}
			return new QdmCommandTreeTranslator.PostRegroupLimitContext(queryDefinition.PostRegroupLimit, group, blankRowFilterContext);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000245DC File Offset: 0x000227DC
		internal static IEnumerable<string> GetResultFieldNames(QueryDefinition queryDefinition)
		{
			foreach (Group group in queryDefinition.Groups)
			{
				foreach (GroupField groupField in group.GetProjectedFields(false))
				{
					yield return groupField.Name;
				}
				IEnumerator<GroupField> enumerator2 = null;
			}
			IEnumerator<Group> enumerator = null;
			foreach (Measure measure in queryDefinition.Measures)
			{
				yield return measure.Name;
			}
			IEnumerator<Measure> enumerator3 = null;
			if (queryDefinition.Rollup != null)
			{
				foreach (RollupGroup rollupGroup in queryDefinition.Rollup.RollupGroups)
				{
					yield return rollupGroup.AggregateIndicatorName;
				}
				IEnumerator<RollupGroup> enumerator4 = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000245EC File Offset: 0x000227EC
		private static void ValidateResultFieldNames(QueryDefinition queryDefinition)
		{
			string[] array = QdmCommandTreeTranslator.GetResultFieldNames(queryDefinition).ToArray<string>();
			if (!array.Any<string>())
			{
				throw new QueryDefinitionTranslationException(SR.QueryDefinitionIsEmpty);
			}
			if (array.Any((string a) => a.IsNullOrWhiteSpace()))
			{
				throw new QueryDefinitionTranslationException(SR.ProjectedExpressionWithEmptyAlias);
			}
			if (array.Distinct(EdmItem.IdentityComparer).Count<string>() != array.Count<string>())
			{
				throw new QueryDefinitionTranslationException(SR.ProjectedExpressionWithDuplicateAlias);
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00024670 File Offset: 0x00022870
		private KeyValuePair<string, QueryExpression> BuildQueryGroups(QdmCommandTreeTranslator.PostRegroupLimitContext postRegroupLimit)
		{
			QdmCommandTreeTranslator.GroupTranslationContext[] array = this.CalculateGroupTranslationContexts(this._queryDefinition.Groups.ToArray<Group>(), false, false, false, false);
			return this.BuildQueryGroupsCore(array, 0, -1, postRegroupLimit, null, default(KeyValuePair<string, QueryExpression>));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000246AC File Offset: 0x000228AC
		private QdmCommandTreeTranslator.GroupTranslationContext[] CalculateGroupTranslationContexts(Group[] groups, bool applyCalculateToFirstGroup = false, bool ignoreLimits = false, bool ignoreFilters = false, bool ignoreOuterJoin = false)
		{
			int? num;
			QdmCommandTreeTranslator.GroupPredicateApplication[] groupPredicateApplicationKinds = this.GetGroupPredicateApplicationKinds(groups, ignoreLimits, ignoreOuterJoin, out num);
			QdmCommandTreeTranslator.GroupTranslationContext[] array = new QdmCommandTreeTranslator.GroupTranslationContext[groups.Length];
			for (int i = 0; i < groups.Length; i++)
			{
				Group group = groups[i];
				Group originalGroup = this.GetOriginalGroup(group);
				QdmCommandTreeTranslator.GroupPredicateApplication groupPredicateApplication = groupPredicateApplicationKinds[i];
				IEnumerable<QueryExpression> enumerable = null;
				if (groupPredicateApplication == QdmCommandTreeTranslator.GroupPredicateApplication.FullExplicit)
				{
					enumerable = this.GetJoinPredicatesForGroupContext(groups, i);
				}
				else if (groupPredicateApplication == QdmCommandTreeTranslator.GroupPredicateApplication.PrecedingOnly)
				{
					enumerable = this.GetJoinPredicatesForOuterJoinGroup(groups, i);
				}
				FollowingJoinBehavior followingJoinBehavior = (ignoreOuterJoin ? FollowingJoinBehavior.InnerJoin : group.FollowingJoinBehavior);
				QueryExpression queryExpression = null;
				QueryExpression queryExpression2 = null;
				IEnumerable<GroupDetail> enumerable2 = group.Details.Where((GroupDetail d) => d.CalculateInMeasureContext);
				bool flag = enumerable2.Any<GroupDetail>();
				bool flag2 = false;
				if (flag && enumerable2.Any((GroupDetail d) => this._constraintValidator.IsMeasureExpressionUnconstrained(d.Expression)))
				{
					flag2 = true;
				}
				if (!ignoreFilters)
				{
					IEnumerable<Group> enumerable3;
					Group filteredGroup = QdmCommandTreeTranslator.GetFilteredGroup(this._queryDefinition, groups, out enumerable3);
					if (group.EqualsByName(filteredGroup))
					{
						queryExpression = this.GetGroupFilterPredicate(groups.Skip(i + 1), enumerable3, false);
					}
					else
					{
						bool flag3 = groupPredicateApplication != QdmCommandTreeTranslator.GroupPredicateApplication.FullImplicit;
						if (followingJoinBehavior == FollowingJoinBehavior.OuterJoin && enumerable3.Contains(originalGroup))
						{
							flag3 = false;
						}
						if (flag3 || flag)
						{
							IEnumerable<Group> enumerable4;
							bool flag4;
							if (followingJoinBehavior == FollowingJoinBehavior.OuterJoin)
							{
								enumerable4 = groups.Skip(i);
								flag4 = this._crossJoinQueryPlan;
							}
							else
							{
								enumerable4 = groups.Skip(i + 1);
								flag4 = false;
							}
							QueryExpression queryExpression3;
							QueryExpression queryExpression4;
							this.GetInnerGroupFilterContextExpressions(enumerable4, flag4, flag2, out queryExpression3, out queryExpression4);
							if (flag3)
							{
								queryExpression = queryExpression3;
							}
							if (flag)
							{
								queryExpression2 = queryExpression4;
							}
						}
					}
				}
				if (flag2 && queryExpression2 == null)
				{
					queryExpression2 = this.GetInnerGroupJoinPredicateExpression(groups.Skip(i + 1), false);
				}
				Limit limit = null;
				if (!ignoreLimits)
				{
					limit = this._queryDefinition.Limits.GetLimitForGroup(originalGroup);
				}
				QdmCommandTreeTranslator.GroupTranslationContext[] array2 = array;
				int num2 = i;
				Group group2 = group;
				FollowingJoinBehavior followingJoinBehavior2 = followingJoinBehavior;
				object obj;
				if (queryExpression2 == null)
				{
					obj = null;
				}
				else
				{
					(obj = new QueryExpression[1])[0] = queryExpression2;
				}
				array2[num2] = new QdmCommandTreeTranslator.GroupTranslationContext(group2, followingJoinBehavior2, obj, enumerable, queryExpression, limit, applyCalculateToFirstGroup && i == 0);
			}
			if (num != null)
			{
				array = array.Take(num.Value).ToArray<QdmCommandTreeTranslator.GroupTranslationContext>();
			}
			return array;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000248A8 File Offset: 0x00022AA8
		private QdmCommandTreeTranslator.GroupPredicateApplication[] GetGroupPredicateApplicationKinds(Group[] groups, bool ignoreLimits, bool ignoreOuterJoin, out int? numGroupsToRetain)
		{
			QdmCommandTreeTranslator.GroupPredicateApplication[] defaultGroupPredicateApplications = this.GetDefaultGroupPredicateApplications(groups, ignoreLimits, ignoreOuterJoin);
			if (groups.Length != 0 && this._queryDefinition.Measures.Count == 0)
			{
				QdmCommandTreeTranslator.PromoteInnermostProjectedFullImplicitGroup(groups, defaultGroupPredicateApplications, out numGroupsToRetain);
			}
			else
			{
				numGroupsToRetain = null;
			}
			return defaultGroupPredicateApplications;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000248EC File Offset: 0x00022AEC
		private QdmCommandTreeTranslator.GroupPredicateApplication[] GetDefaultGroupPredicateApplications(Group[] groups, bool ignoreLimits, bool ignoreOuterJoin)
		{
			QdmCommandTreeTranslator.GroupPredicateApplication[] array = new QdmCommandTreeTranslator.GroupPredicateApplication[groups.Length];
			QdmCommandTreeTranslator.GroupPredicateApplication? groupPredicateApplication = null;
			for (int i = groups.Length - 1; i >= 0; i--)
			{
				this._cancellationToken.ThrowIfCancellationRequested();
				Group group = groups[i];
				Group originalGroup = this.GetOriginalGroup(group);
				int num = (int)(ignoreOuterJoin ? FollowingJoinBehavior.InnerJoin : group.FollowingJoinBehavior);
				Limit limit = this._queryDefinition.Limits.Where((Limit l) => l.RefersTo(originalGroup)).FirstOrDefault<Limit>();
				bool flag = limit != null && (!limit.SpansToMultipleGroups || limit.IsSpanningLimitEndingWith(originalGroup));
				QdmCommandTreeTranslator.GroupPredicateApplication groupPredicateApplication2;
				if (num == 1)
				{
					groupPredicateApplication2 = QdmCommandTreeTranslator.GroupPredicateApplication.PrecedingOnly;
					IEnumerable<QueryExpression> joinPredicatesForOuterJoinGroup = this.GetJoinPredicatesForOuterJoinGroup(groups, i);
					QueryExpression[] joinPredicatesForGroupContext = this.GetJoinPredicatesForGroupContext(groups, i);
					if (JoinPredicates.AreEquivalent(joinPredicatesForOuterJoinGroup, joinPredicatesForGroupContext))
					{
						groupPredicateApplication2 = QdmCommandTreeTranslator.GroupPredicateApplication.FullExplicit;
					}
				}
				else if (!ignoreLimits && flag)
				{
					groupPredicateApplication2 = QdmCommandTreeTranslator.GroupPredicateApplication.FullExplicit;
				}
				else
				{
					if (groupPredicateApplication != null)
					{
						QdmCommandTreeTranslator.GroupPredicateApplication? groupPredicateApplication3 = groupPredicateApplication;
						QdmCommandTreeTranslator.GroupPredicateApplication groupPredicateApplication4 = QdmCommandTreeTranslator.GroupPredicateApplication.PrecedingOnly;
						if (!((groupPredicateApplication3.GetValueOrDefault() == groupPredicateApplication4) & (groupPredicateApplication3 != null)))
						{
							groupPredicateApplication2 = QdmCommandTreeTranslator.GroupPredicateApplication.FullImplicit;
							goto IL_00F6;
						}
					}
					groupPredicateApplication2 = QdmCommandTreeTranslator.GroupPredicateApplication.FullExplicit;
				}
				IL_00F6:
				array[i] = groupPredicateApplication2;
				groupPredicateApplication = new QdmCommandTreeTranslator.GroupPredicateApplication?(groupPredicateApplication2);
			}
			return array;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00024A0C File Offset: 0x00022C0C
		private static void PromoteInnermostProjectedFullImplicitGroup(Group[] groups, QdmCommandTreeTranslator.GroupPredicateApplication[] applyPredicates, out int? numGroupsToRetain)
		{
			numGroupsToRetain = null;
			int num = groups.Length - 1;
			if (groups[num].IsProjected)
			{
				return;
			}
			int? num2 = null;
			int? num3 = null;
			int? num4 = null;
			for (int i = num; i >= 0; i--)
			{
				if (groups[i].IsProjected)
				{
					QdmCommandTreeTranslator.GroupPredicateApplication groupPredicateApplication = applyPredicates[i];
					if (num2 == null)
					{
						num2 = new int?(i);
					}
					if (num3 == null && groupPredicateApplication == QdmCommandTreeTranslator.GroupPredicateApplication.FullExplicit)
					{
						num3 = new int?(i);
					}
					if (num4 == null && groupPredicateApplication == QdmCommandTreeTranslator.GroupPredicateApplication.FullImplicit)
					{
						num4 = new int?(i);
					}
					if (num3 != null && num4 != null)
					{
						break;
					}
				}
			}
			if (num3 != null)
			{
				if (num4 != null)
				{
					int? num5 = num4;
					int? num6 = num3;
					if ((num5.GetValueOrDefault() > num6.GetValueOrDefault()) & ((num5 != null) & (num6 != null)))
					{
						applyPredicates[num4.Value] = QdmCommandTreeTranslator.GroupPredicateApplication.FullExplicit;
					}
				}
				numGroupsToRetain = num2 + 1;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00024B2C File Offset: 0x00022D2C
		private Group GetOriginalGroup(Group group)
		{
			return QdmCommandTreeTranslator.GetOriginalGroup(group, this._queryDefinition);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00024B3C File Offset: 0x00022D3C
		private static Group GetOriginalGroup(Group group, QueryDefinition queryDefinition)
		{
			return queryDefinition.Groups.Single((Group g) => g.EqualsByName(group));
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00024B70 File Offset: 0x00022D70
		private QueryExpression[] GetJoinPredicatesForGroupContext(Group[] groups, int index)
		{
			if (index < groups.Length - 1 && this._hasUnanchoredJoinPredicates)
			{
				QueryExpression innerGroupJoinPredicateExpression = this.GetInnerGroupJoinPredicateExpression(groups.Skip(index + 1), true);
				return new QueryExpression[] { innerGroupJoinPredicateExpression.HasAnyRows(this._crossJoinQueryPlan) };
			}
			return this._fullJoinPredicates.ToPredicateExpressions().ToArray<QueryExpression>();
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00024BC4 File Offset: 0x00022DC4
		private IEnumerable<QueryExpression> GetJoinPredicatesForOuterJoinGroup(Group[] groups, int index)
		{
			Group group = groups[index];
			Group originalGroup = this.GetOriginalGroup(group);
			if (this._queryDefinition.ExplicitJoinPredicates != null && !this._queryDefinition.ExplicitJoinPredicates.Any<IJoinPredicate>())
			{
				return Enumerable.Empty<QueryExpression>();
			}
			bool flag;
			IEnumerable<QueryExpression> enumerable = this._queryDefinition.GetHierarchicalJoinPredicatesUpToGroup(originalGroup, this._crossJoinQueryPlan, out flag).ToPredicateExpressions();
			if (!enumerable.Any<QueryExpression>())
			{
				IEnumerable<bool> enumerable2 = groups.Where((Group g) => g != group).Select(delegate(Group g)
				{
					if (!this._useConceptualSchema)
					{
						return QueryAlgorithms.AreHierarchicallyRelated(group, g, this._queryDefinition.EntityDataModel, this._queryDefinition.IncludeDirectManyToManyAssociations);
					}
					return QueryAlgorithms.AreHierarchicallyRelated(group, g, this._queryDefinition.ConceptualSchema, this._queryDefinition.IncludeDirectManyToManyAssociations);
				});
				if (!flag)
				{
					if (enumerable2.Any((bool isHierarchical) => !isHierarchical))
					{
						if (this._useConceptualSchema)
						{
							enumerable = (from e in QueryConstraintValidator.FindAllGroupEntityReferences(new Group[] { @group })
								select new EntityJoinPredicate(null, e)).ToPredicateExpressions().ToArray<QueryExpression>();
						}
						else
						{
							enumerable = (from e in QueryConstraintValidator.FindAllGroupEntitySetReferences(new Group[] { @group })
								select new EntityJoinPredicate(e, null)).ToPredicateExpressions().ToArray<QueryExpression>();
						}
					}
				}
			}
			if (enumerable.Any<QueryExpression>())
			{
				QueryExpression[] joinPredicatesForFollowingHierarchicalOuterJoinGroups = this.GetJoinPredicatesForFollowingHierarchicalOuterJoinGroups(groups, index);
				QueryExpression[] joinPredicatesForGroupContext = this.GetJoinPredicatesForGroupContext(groups, index);
				enumerable = enumerable.Union(joinPredicatesForFollowingHierarchicalOuterJoinGroups).Union(joinPredicatesForGroupContext).ToArray<QueryExpression>();
			}
			return enumerable;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00024D58 File Offset: 0x00022F58
		private QueryExpression[] GetJoinPredicatesForFollowingHierarchicalOuterJoinGroups(Group[] groups, int index)
		{
			Group originalGroup = this.GetOriginalGroup(groups[index]);
			IEnumerable<QueryExpression> enumerable = Enumerable.Empty<QueryExpression>();
			foreach (Group group in groups.Skip(index + 1))
			{
				if (group.FollowingJoinBehavior == FollowingJoinBehavior.OuterJoin)
				{
					this._cancellationToken.ThrowIfCancellationRequested();
					if (!(this._useConceptualSchema ? (!QueryAlgorithms.AreHierarchicallyRelated(originalGroup, group, this._queryDefinition.ConceptualSchema, this._queryDefinition.IncludeDirectManyToManyAssociations)) : (!QueryAlgorithms.AreHierarchicallyRelated(originalGroup, group, this._queryDefinition.EntityDataModel, this._queryDefinition.IncludeDirectManyToManyAssociations))))
					{
						Group originalGroup2 = this.GetOriginalGroup(group);
						IEnumerable<QueryExpression> enumerable2 = this._queryDefinition.GetHierarchicalJoinPredicatesUpToGroup(originalGroup2, this._crossJoinQueryPlan).ToPredicateExpressions();
						enumerable = enumerable.Union(enumerable2);
					}
				}
			}
			return enumerable.ToArray<QueryExpression>();
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00024E50 File Offset: 0x00023050
		private QueryExpression GetInnerGroupJoinPredicateExpression(IEnumerable<Group> groups, bool ignoreOuterJoin = false)
		{
			QdmCommandTreeTranslator.GroupTranslationContext[] array = this.CalculateGroupTranslationContexts(QdmCommandTreeTranslator.GetGroupsOmittingDetails(groups), false, true, true, ignoreOuterJoin);
			return this.BuildQueryGroupsCore(array, 0, -1, null, null, default(KeyValuePair<string, QueryExpression>)).Value;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00024E8C File Offset: 0x0002308C
		private static Group GetFilteredGroup(QueryDefinition queryDefinition, IEnumerable<Group> groups, out IEnumerable<Group> excludedGroups)
		{
			Group group = ((queryDefinition.GroupFilter != null) ? groups.LastOrDefault(new Func<Group, bool>(queryDefinition.GroupFilter.RefersTo)) : null);
			if (group != null)
			{
				excludedGroups = (from g in queryDefinition.Groups.TakeUntil(QdmCommandTreeTranslator.GetOriginalGroup(@group, queryDefinition))
					where !queryDefinition.GroupFilter.RefersTo(g)
					select g).ToList<Group>();
			}
			else
			{
				excludedGroups = Enumerable.Empty<Group>();
			}
			return group;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00024F14 File Offset: 0x00023114
		private QueryExpression GetGroupFilterPredicate(IEnumerable<Group> innerGroups, IEnumerable<Group> excludedGroups, bool includeAllGroupsForContext = false)
		{
			QueryExpression queryExpression = this._queryDefinition.GroupFilter.Filter.ToPredicate();
			List<QueryExpression> list = new List<QueryExpression>();
			if (this._useConceptualSchema)
			{
				list.AddRange(from c in excludedGroups.SelectMany((Group g) => g.GetKeyAndDetailModelColumnReferences())
					select c.AllSelected());
			}
			else
			{
				list.AddRange(from f in excludedGroups.SelectMany((Group g) => g.GetKeyAndDetailModelFieldReferences())
					select f.AllSelected(null));
			}
			if (includeAllGroupsForContext || this._constraintValidator.IsMeasureExpressionUnconstrained(queryExpression))
			{
				QueryExpression innerGroupJoinPredicateExpression = this.GetInnerGroupJoinPredicateExpression(innerGroups, false);
				if (innerGroupJoinPredicateExpression != null)
				{
					list.Add(innerGroupJoinPredicateExpression);
				}
			}
			if (list.Count > 0)
			{
				return queryExpression.Calculate(list);
			}
			return queryExpression;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0002501C File Offset: 0x0002321C
		private void GetInnerGroupFilterContextExpressions(IEnumerable<Group> groups, bool applyCalculateToFirstGroup, bool includeAllGroupsForContext, out QueryExpression groupFilterPredicate, out QueryExpression detailMeasureSubgroupContextExpression)
		{
			IEnumerable<Group> enumerable;
			Group filteredGroup = QdmCommandTreeTranslator.GetFilteredGroup(this._queryDefinition, groups, out enumerable);
			if (filteredGroup == null)
			{
				groupFilterPredicate = null;
				detailMeasureSubgroupContextExpression = null;
				return;
			}
			List<Group> list = groups.Intersect(enumerable, Group.NameComparer).ToList<Group>();
			IEnumerable<Group> enumerable2 = groups.Except(list).TakeUntil(filteredGroup);
			int num = enumerable2.Count<Group>();
			List<Group> list2 = list.Concat(groups.TakeAfter(filteredGroup)).ToList<Group>();
			IEnumerable<Group> enumerable3;
			if (includeAllGroupsForContext || this._hasUnanchoredJoinPredicates)
			{
				enumerable3 = enumerable2.Concat(list2);
			}
			else
			{
				enumerable3 = enumerable2;
			}
			QueryExpression groupFilterPredicate2 = this.GetGroupFilterPredicate(list2, enumerable.Except(groups, Group.NameComparer), includeAllGroupsForContext);
			QdmCommandTreeTranslator.GroupTranslationContext[] array = this.CalculateGroupTranslationContexts(QdmCommandTreeTranslator.GetGroupsOmittingDetails(enumerable3), applyCalculateToFirstGroup, true, true, false);
			KeyValuePair<string, QueryExpression> keyValuePair = QdmCommandTreeTranslator.ApplyGroupFilter(this.BuildQueryGroupsCore(array.Take(num).ToArray<QdmCommandTreeTranslator.GroupTranslationContext>(), 0, -1, null, null, default(KeyValuePair<string, QueryExpression>)), groupFilterPredicate2);
			groupFilterPredicate = keyValuePair.Value.HasAnyRows(this._crossJoinQueryPlan);
			detailMeasureSubgroupContextExpression = this.BuildQueryGroupsCore(array.Skip(num).ToArray<QdmCommandTreeTranslator.GroupTranslationContext>(), 0, -1, null, null, keyValuePair).Value;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002512A File Offset: 0x0002332A
		private static Group[] GetGroupsOmittingDetails(IEnumerable<Group> groups)
		{
			return groups.Select((Group g) => g.OmitDetails()).ToArray<Group>();
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00025158 File Offset: 0x00023358
		private KeyValuePair<string, QueryExpression> BuildQueryGroupsCore(QdmCommandTreeTranslator.GroupTranslationContext[] groups, int startingIndex = 0, int endIndex = -1, QdmCommandTreeTranslator.PostRegroupLimitContext postRegroupLimit = null, Limit limitBeingApplied = null, KeyValuePair<string, QueryExpression> allGroupsExpr = default(KeyValuePair<string, QueryExpression>))
		{
			if (endIndex == -1)
			{
				endIndex = groups.Length - 1;
			}
			IList<Group> list = groups.Select((QdmCommandTreeTranslator.GroupTranslationContext g) => g.Group).Evaluate<Group>();
			int i = startingIndex;
			while (i <= endIndex)
			{
				QdmCommandTreeTranslator.GroupTranslationContext groupTranslationContext = groups[i];
				Group group = groupTranslationContext.Group;
				if (postRegroupLimit != null && postRegroupLimit.ApplyAtGroup == group)
				{
					allGroupsExpr = this.ApplyPostRegroupLimit(allGroupsExpr, groups, postRegroupLimit, i);
					break;
				}
				Limit limit = groupTranslationContext.Limit;
				if (limitBeingApplied != limit && limit != null && limit.IsSpanningLimitStartingFrom(group))
				{
					int lastGroupIndexFrom = limit.GetLastGroupIndexFrom(list);
					allGroupsExpr = this.ApplyLimitSpanningMultipleGroups(allGroupsExpr, groups, groupTranslationContext.Limit, i, lastGroupIndexFrom);
					i = lastGroupIndexFrom + 1;
				}
				else
				{
					allGroupsExpr = this.AppendGroupExpression(allGroupsExpr, group, groupTranslationContext.DetailMeasureContextFilters, groupTranslationContext.JoinPredicates, groupTranslationContext.GroupFilterPredicate, groupTranslationContext.Limit, groupTranslationContext.ApplyCalculate);
					if (groupTranslationContext.FollowingJoinBehavior == FollowingJoinBehavior.OuterJoin && i < endIndex)
					{
						KeyValuePair<string, QueryExpression> keyValuePair = this.BuildQueryGroupsCore(groups, i + 1, endIndex, postRegroupLimit, limitBeingApplied, default(KeyValuePair<string, QueryExpression>));
						KeyValuePair<string, QueryExpression> keyValuePair2 = allGroupsExpr;
						KeyValuePair<string, QueryExpression> keyValuePair3 = keyValuePair;
						Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> func;
						if ((func = QdmCommandTreeTranslator.<>O.<0>__GenerateAll) == null)
						{
							func = (QdmCommandTreeTranslator.<>O.<0>__GenerateAll = new Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression>(QueryExpressionBuilder.GenerateAll));
						}
						allGroupsExpr = this.JoinGroupExpressions(keyValuePair2, keyValuePair3, func);
						break;
					}
					i++;
				}
			}
			return allGroupsExpr;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00025298 File Offset: 0x00023498
		private KeyValuePair<string, QueryExpression> ApplyLimitSpanningMultipleGroups(KeyValuePair<string, QueryExpression> outerGroupsExpr, QdmCommandTreeTranslator.GroupTranslationContext[] groups, Limit limit, int limitStartIndex, int limitEndIndex)
		{
			KeyValuePair<string, QueryExpression> keyValuePair = this.BuildQueryGroupsCore(groups, limitStartIndex, limitEndIndex, null, limit, default(KeyValuePair<string, QueryExpression>));
			keyValuePair = QdmCommandTreeTranslator.ApplyLimit(keyValuePair, limit);
			return this.ApplyJoinGroupExpression(keyValuePair, outerGroupsExpr);
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000252CC File Offset: 0x000234CC
		private KeyValuePair<string, QueryExpression> ApplyPostRegroupLimit(KeyValuePair<string, QueryExpression> outerGroupsExpr, QdmCommandTreeTranslator.GroupTranslationContext[] groups, QdmCommandTreeTranslator.PostRegroupLimitContext limit, int startingGroupIndex)
		{
			KeyValuePair<string, QueryExpression> keyValuePair = this.BuildQueryGroupsCore(groups, startingGroupIndex, -1, null, null, default(KeyValuePair<string, QueryExpression>));
			keyValuePair = QdmCommandTreeTranslator.ApplyQueryProjection(keyValuePair, from c in groups.Skip(startingGroupIndex)
				select c.Group, this._queryDefinition.Measures, this._queryDefinition.Rollup, limit.BlankRowFilterContext);
			keyValuePair = QdmCommandTreeTranslator.ApplyLimit(keyValuePair, limit.Limit);
			return this.ApplyJoinGroupExpression(keyValuePair, outerGroupsExpr);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00025353 File Offset: 0x00023553
		private KeyValuePair<string, QueryExpression> ApplyJoinGroupExpression(KeyValuePair<string, QueryExpression> innerQuery, KeyValuePair<string, QueryExpression> outerGroupsExpr)
		{
			if (outerGroupsExpr.Key != null)
			{
				KeyValuePair<string, QueryExpression> keyValuePair = outerGroupsExpr;
				Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> func;
				if ((func = QdmCommandTreeTranslator.<>O.<1>__Generate) == null)
				{
					func = (QdmCommandTreeTranslator.<>O.<1>__Generate = new Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression>(QueryExpressionBuilder.Generate));
				}
				return this.JoinGroupExpressions(keyValuePair, innerQuery, func);
			}
			return innerQuery;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00025384 File Offset: 0x00023584
		private KeyValuePair<string, QueryExpression> AppendGroupExpression(KeyValuePair<string, QueryExpression> allGroupsExpr, Group group, IEnumerable<QueryExpression> detailMeasureContextFilters, IEnumerable<QueryExpression> joinPredicates, QueryExpression groupFilterPredicate, Limit limit, bool applyCalculate)
		{
			bool flag = group.ShouldTranslateKeys();
			KeyValuePair<string, QueryExpression> keyValuePair;
			if (flag)
			{
				keyValuePair = QdmCommandTreeTranslator.GetModelReferenceGroupKeyAndDetailExpressions(group).QdmGroupBy(this._useConceptualSchema, this._queryDefinition.ConceptualSchema, ScanKind.InheritFilterContextIncludeBlankRow).As(QdmNames.Grouped(group.Name));
			}
			else
			{
				keyValuePair = group.GetSingleGeneratedGroupKeyDataTableExpression().As(QdmNames.Grouped(group.Name));
			}
			if (applyCalculate)
			{
				keyValuePair = keyValuePair.Value.Calculate(Array.Empty<QueryExpression>()).As(keyValuePair.Key);
			}
			keyValuePair = QdmCommandTreeTranslator.ApplyJoinPredicate(keyValuePair, joinPredicates);
			keyValuePair = QdmCommandTreeTranslator.ApplyGroupFilter(keyValuePair, groupFilterPredicate);
			if (flag)
			{
				keyValuePair = QdmCommandTreeTranslator.ApplyGroupNonModelFieldProjection(keyValuePair, group, detailMeasureContextFilters, this._existsFilters);
			}
			if (limit != null && !limit.SpansToMultipleGroups)
			{
				keyValuePair = QdmCommandTreeTranslator.ApplyLimit(keyValuePair, limit);
			}
			keyValuePair = QdmCommandTreeTranslator.ApplyGroupFieldProjection(keyValuePair, group);
			if (allGroupsExpr.Value == null || !flag)
			{
				return keyValuePair;
			}
			KeyValuePair<string, QueryExpression> keyValuePair2 = allGroupsExpr;
			KeyValuePair<string, QueryExpression> keyValuePair3 = keyValuePair;
			Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> func;
			if ((func = QdmCommandTreeTranslator.<>O.<1>__Generate) == null)
			{
				func = (QdmCommandTreeTranslator.<>O.<1>__Generate = new Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression>(QueryExpressionBuilder.Generate));
			}
			return this.JoinGroupExpressions(keyValuePair2, keyValuePair3, func);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00025477 File Offset: 0x00023677
		private static IEnumerable<KeyValuePair<string, QueryExpression>> GetModelReferenceGroupKeyAndDetailExpressions(Group group)
		{
			foreach (GroupKey groupKey in group.Keys)
			{
				yield return groupKey.Expression.As(groupKey.Name);
			}
			IEnumerator<GroupKey> enumerator = null;
			foreach (GroupDetail groupDetail in group.Details)
			{
				if (groupDetail.Expression.IsModelFieldReference())
				{
					yield return groupDetail.Expression.As(groupDetail.Name);
				}
			}
			IEnumerator<GroupDetail> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00025488 File Offset: 0x00023688
		private static KeyValuePair<string, QueryExpression> ApplyGroupNonModelFieldProjection(KeyValuePair<string, QueryExpression> groupExpr, Group group, IEnumerable<QueryExpression> detailMeasureContextFilters, ReadOnlyCollection<QueryExpression> existsFilters)
		{
			KeyValuePair<string, QueryExpression>[] array = (from d in @group.Details
				where !d.Expression.IsModelFieldReference()
				select QdmCommandTreeTranslator.DetailToNamedExpression(d, detailMeasureContextFilters, existsFilters)).ToArray<KeyValuePair<string, QueryExpression>>();
			if (array.Length == 0)
			{
				return groupExpr;
			}
			QueryGroupByExpression queryGroupByExpression = groupExpr.Value as QueryGroupByExpression;
			if (queryGroupByExpression != null)
			{
				return queryGroupByExpression.AppendAggregates(array).As(groupExpr.Key);
			}
			QueryExpressionBinding queryExpressionBinding = groupExpr.Value.BindAs(groupExpr.Key);
			return QdmCommandTreeTranslator.AppendProjectedExpressions(queryExpressionBinding, array).As(QdmNames.Projected(queryExpressionBinding.Variable.VariableName));
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00025544 File Offset: 0x00023744
		private static KeyValuePair<string, QueryExpression> DetailToNamedExpression(GroupDetail detail, IEnumerable<QueryExpression> detailMeasureContextFilters, ReadOnlyCollection<QueryExpression> existsFilters)
		{
			QueryExpression queryExpression;
			if (detail.CalculateInMeasureContext && detailMeasureContextFilters != null && detailMeasureContextFilters.Any<QueryExpression>())
			{
				queryExpression = detail.Expression.Calculate(detailMeasureContextFilters);
			}
			else
			{
				queryExpression = detail.Expression;
			}
			if (detail.CalculateInMeasureContext && existsFilters != null && existsFilters.Count > 0)
			{
				queryExpression = queryExpression.Calculate(existsFilters);
			}
			return queryExpression.As(detail.Name);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000255A4 File Offset: 0x000237A4
		private KeyValuePair<string, QueryExpression> JoinGroupExpressions(KeyValuePair<string, QueryExpression> firstGroupsExpr, KeyValuePair<string, QueryExpression> secondGroupsExpr, Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> generateFunction)
		{
			QueryExpressionBinding queryExpressionBinding = firstGroupsExpr.Value.BindAs(firstGroupsExpr.Key);
			QueryExpression queryExpression = secondGroupsExpr.Value;
			if (!this._crossJoinQueryPlan)
			{
				queryExpression = queryExpression.Calculate(Array.Empty<QueryExpression>());
			}
			QueryExpressionBinding queryExpressionBinding2 = queryExpression.BindAs(secondGroupsExpr.Key);
			return generateFunction(new QueryExpressionBinding[] { queryExpressionBinding, queryExpressionBinding2 }).As(QdmNames.Projected(QdmNames.Apply(firstGroupsExpr.Key)));
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002561C File Offset: 0x0002381C
		private static KeyValuePair<string, QueryExpression> ApplyJoinPredicate(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<QueryExpression> joinPredicates)
		{
			if (joinPredicates.EmptyIfNull<QueryExpression>().Any<QueryExpression>())
			{
				QueryExpression queryExpression = joinPredicates.Distinct<QueryExpression>().OrAll();
				QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
				return queryExpressionBinding.Filter(queryExpression).As(QdmNames.Filtered(queryExpressionBinding.Variable.VariableName));
			}
			return innerQuery;
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00025674 File Offset: 0x00023874
		private static KeyValuePair<string, QueryExpression> ApplyGroupFilter(KeyValuePair<string, QueryExpression> innerQuery, QueryExpression groupFilterPredicate)
		{
			if (groupFilterPredicate == null)
			{
				return innerQuery;
			}
			QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
			return queryExpressionBinding.Filter(groupFilterPredicate).As(QdmNames.Filtered(queryExpressionBinding.Variable.VariableName));
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000256B6 File Offset: 0x000238B6
		private static KeyValuePair<string, QueryExpression> ApplyLimit(KeyValuePair<string, QueryExpression> innerQuery, Limit limit)
		{
			if (limit == null)
			{
				return innerQuery;
			}
			return QdmCommandTreeTranslator.ApplyLimit(innerQuery, limit.Operator, limit.Sorting);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000256D0 File Offset: 0x000238D0
		private static KeyValuePair<string, QueryExpression> ApplyLimit(KeyValuePair<string, QueryExpression> innerQuery, LimitOperator limitOp, IEnumerable<SortItem> sorting)
		{
			if (limitOp == null)
			{
				return innerQuery;
			}
			QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
			IEnumerable<QuerySortClause> enumerable = QdmCommandTreeTranslator.CreateSortClauses(queryExpressionBinding, sorting);
			TopLimitOperator topLimitOperator = limitOp as TopLimitOperator;
			QueryExpression queryExpression;
			if (topLimitOperator == null)
			{
				SampleLimitOperator sampleLimitOperator = limitOp as SampleLimitOperator;
				if (sampleLimitOperator == null)
				{
					throw new NotSupportedException(DevErrors.QdmCommandTreeTranslation.UnsupportedLimitOperator(limitOp.GetType().FullName));
				}
				queryExpression = queryExpressionBinding.Sample(sampleLimitOperator.Count, enumerable);
			}
			else if (topLimitOperator.Skip == null)
			{
				queryExpression = queryExpressionBinding.TopN(topLimitOperator.Count, enumerable);
			}
			else
			{
				queryExpression = queryExpressionBinding.TopNSkip(topLimitOperator.Count, topLimitOperator.Skip.Value, enumerable);
			}
			return queryExpression.As(QdmNames.Limit(queryExpressionBinding.Variable.VariableName));
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00025794 File Offset: 0x00023994
		private static KeyValuePair<string, QueryExpression> ApplyGroupFieldProjection(KeyValuePair<string, QueryExpression> innerQuery, Group group)
		{
			if (group.Details.Any((GroupDetail d) => !d.IsProjected))
			{
				QueryGroupExpressionBinding groupByInput = innerQuery.Value.GroupBindAs(innerQuery.Key);
				return groupByInput.GroupBy(group.GetProjectedFields(true).Select(delegate(GroupField f)
				{
					QueryExpression variable = groupByInput.Variable;
					GroupField groupField = f;
					QueryExpression queryExpression = variable.Field(groupField.Name);
					groupField = f;
					return queryExpression.As(groupField.Name);
				})).As(QdmNames.Grouped(groupByInput.Variable.VariableName));
			}
			return innerQuery;
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002582C File Offset: 0x00023A2C
		private static KeyValuePair<string, QueryExpression> ApplyQueryProjection(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<Group> groups, IEnumerable<Measure> measures, Rollup rollup, QdmCommandTreeTranslator.BlankRowFilterContext blankRowFilterContext)
		{
			IEnumerable<RollupGroup> enumerable;
			if (rollup != null)
			{
				IEnumerable<RollupGroup> rollupGroups = rollup.RollupGroups;
				enumerable = rollupGroups;
			}
			else
			{
				enumerable = Enumerable.Empty<RollupGroup>();
			}
			IEnumerable<RollupGroup> enumerable2 = enumerable;
			bool flag;
			if (!enumerable2.Any<RollupGroup>())
			{
				flag = groups.Any((Group g) => !g.IsProjected);
			}
			else
			{
				flag = true;
			}
			KeyValuePair<string, QueryExpression>[] array = measures.Select((Measure m) => m.Expression.As(m.Name)).ToArray<KeyValuePair<string, QueryExpression>>();
			if (flag)
			{
				ArgumentValidation.CheckCondition(innerQuery.Value != null, "innerQuery");
				QueryGroupExpressionBinding groupByInput = innerQuery.Value.GroupBindAs(innerQuery.Key);
				IEnumerable<KeyValuePair<string, QueryExpression>> enumerable3 = enumerable2.Select((RollupGroup rg) => groupByInput.Variable.Field(rg.GroupRefs.First<string>()).IsAggregate().As(rg.AggregateIndicatorName));
				IEnumerable<KeyValuePair<string, QueryExpression>> enumerable4 = Enumerable.Empty<KeyValuePair<string, QueryExpression>>();
				if (blankRowFilterContext.JoinPredicateFieldName != null)
				{
					QueryExpression queryExpression = blankRowFilterContext.JoinPredicates.ToPredicateExpressions().OrAll();
					enumerable4 = new KeyValuePair<string, QueryExpression>[] { queryExpression.As(blankRowFilterContext.JoinPredicateFieldName) };
				}
				return groupByInput.GroupBy(QdmCommandTreeTranslator.CreateGroupItems(groups.ToArray<Group>(), groupByInput, enumerable2), enumerable3.Concat(array).Concat(enumerable4)).As(QdmNames.Grouped(groupByInput.Variable.VariableName));
			}
			return QdmCommandTreeTranslator.ApplyMeasureProjection(innerQuery, groups, array);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00025984 File Offset: 0x00023B84
		private static KeyValuePair<string, QueryExpression> ApplyMeasureProjection(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<Group> groups, KeyValuePair<string, QueryExpression>[] measuresToProject)
		{
			if (!measuresToProject.Any<KeyValuePair<string, QueryExpression>>())
			{
				return innerQuery;
			}
			if (innerQuery.Value != null)
			{
				QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
				QdmCommandTreeTranslator.OptimizeProjectedMeasureExpressions(measuresToProject, groups, queryExpressionBinding);
				return QdmCommandTreeTranslator.AppendProjectedExpressions(queryExpressionBinding, measuresToProject).As(QdmNames.Projected(queryExpressionBinding.Variable.VariableName));
			}
			return QueryExpressionBuilder.NewTable(measuresToProject).As(QdmNames.Projected(null));
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000259F0 File Offset: 0x00023BF0
		private static void OptimizeProjectedMeasureExpressions(KeyValuePair<string, QueryExpression>[] measuresToProject, IEnumerable<Group> groups, QueryExpressionBinding input)
		{
			List<GroupDetail> list = groups.Last<Group>().Details.Where((GroupDetail d) => d.CalculateInMeasureContext).ToList<GroupDetail>();
			if (list.Count == 0)
			{
				return;
			}
			int i;
			Func<GroupDetail, bool> <>9__1;
			int num;
			for (i = 0; i < measuresToProject.Length; i = num)
			{
				IEnumerable<GroupDetail> enumerable = list.EmptyIfNull<GroupDetail>();
				Func<GroupDetail, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (GroupDetail d) => d.Expression.Equals(measuresToProject[i].Value));
				}
				GroupDetail groupDetail = enumerable.Where(func).FirstOrDefault<GroupDetail>();
				if (groupDetail != null)
				{
					measuresToProject[i] = input.Variable.Field(groupDetail.Name).As(measuresToProject[i].Key);
				}
				num = i + 1;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00025AE9 File Offset: 0x00023CE9
		private static IEnumerable<IGroupItem> CreateGroupItems(IEnumerable<Group> groups, QueryGroupExpressionBinding groupByInput, IEnumerable<RollupGroup> rollupGroups)
		{
			Func<GroupField, KeyValuePair<string, QueryExpression>> <>9__1;
			Func<Group, IEnumerable<KeyValuePair<string, QueryExpression>>> createCompositeKeysForGroup = delegate(Group group)
			{
				IEnumerable<GroupField> projectedFields = group.GetProjectedFields(false);
				Func<GroupField, KeyValuePair<string, QueryExpression>> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(GroupField f)
					{
						QueryExpression variable = groupByInput.Variable;
						GroupField groupField = f;
						QueryExpression queryExpression = variable.Field(groupField.Name);
						groupField = f;
						return queryExpression.As(groupField.Name);
					});
				}
				return projectedFields.Select(func);
			};
			bool hadRollup = false;
			using (StatefulEnumerator<Group> groupEnumerator = StatefulEnumerator<Group>.CreateAtFirstItem(groups))
			{
				using (StatefulEnumerator<RollupGroup> rollupEnumerator = StatefulEnumerator<RollupGroup>.CreateAtFirstItem(rollupGroups))
				{
					while (groupEnumerator.HasItem)
					{
						if (QdmCommandTreeTranslator.GroupMatchesFirstRollupGroup(groupEnumerator, rollupEnumerator))
						{
							if (hadRollup)
							{
								throw new QueryDefinitionTranslationException("Rollup was specified on multiple non-contiguous groups. Rollup may only be specified on a single set of contiguous groups.");
							}
							hadRollup = true;
							List<CompositeKeyGroupItem> list = new List<CompositeKeyGroupItem>();
							do
							{
								List<KeyValuePair<string, QueryExpression>> list2 = new List<KeyValuePair<string, QueryExpression>>();
								foreach (string text in rollupEnumerator.Current.GroupRefs)
								{
									Group group3 = groupEnumerator.ConsumeCurrent();
									if (!EdmItem.IdentityComparer.Equals(group3.Name, text))
									{
										throw new QueryDefinitionTranslationException(DevErrors.QdmCommandTreeTranslation.RollupNotConsistentWithGrouping(group3.Name.MarkAsModelInfo(), text.MarkAsModelInfo()));
									}
									if (!group3.IsProjected)
									{
										throw new QueryDefinitionTranslationException("Rollup was specified on a group that is not projected. Rollup is not allowed on non-projected groups.");
									}
									list2.AddRange(createCompositeKeysForGroup(group3));
								}
								list.Add(new CompositeKeyGroupItem(list2));
							}
							while (rollupEnumerator.MoveNext() && QdmCommandTreeTranslator.GroupMatchesFirstRollupGroup(groupEnumerator, rollupEnumerator));
							yield return new RollupGroupItem(list);
						}
						else
						{
							Group group2 = groupEnumerator.ConsumeCurrent();
							if (group2.IsProjected)
							{
								yield return new CompositeKeyGroupItem(createCompositeKeysForGroup(group2));
							}
						}
					}
				}
				StatefulEnumerator<RollupGroup> rollupEnumerator = null;
			}
			StatefulEnumerator<Group> groupEnumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00025B07 File Offset: 0x00023D07
		private static bool GroupMatchesFirstRollupGroup(StatefulEnumerator<Group> groupEnumerator, StatefulEnumerator<RollupGroup> rollupEnumerator)
		{
			return rollupEnumerator.HasItem && EdmItem.IdentityComparer.Equals(rollupEnumerator.Current.GroupRefs.First<string>(), groupEnumerator.Current.Name);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00025B38 File Offset: 0x00023D38
		internal static QueryProjectExpression AppendProjectedExpressions(QueryExpressionBinding projectInput, IEnumerable<KeyValuePair<string, QueryExpression>> namedExpressions)
		{
			ConceptualRowType conceptualRowType = (ConceptualRowType)projectInput.Variable.ConceptualResultType;
			return projectInput.Project(QueryExpressionBuilder.NewRow(conceptualRowType.Columns.Select((ConceptualTypeColumn f) => projectInput.Variable.Field(f.Name).As(f.Name)).Concat(namedExpressions)), ProjectSubsetStrategy.Default);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00025B98 File Offset: 0x00023D98
		private static KeyValuePair<string, QueryExpression> ApplyTopLevelValueFilter(KeyValuePair<string, QueryExpression> innerQuery, QueryDefinition queryDefinition)
		{
			if (queryDefinition.TopLevelValueFilter == null)
			{
				return innerQuery;
			}
			QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
			QueryExpression queryExpression = queryDefinition.TopLevelValueFilter.ToPredicate();
			return queryExpressionBinding.Filter(queryExpression).As(QdmNames.Filtered(innerQuery.Key));
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00025BE8 File Offset: 0x00023DE8
		private static KeyValuePair<string, QueryExpression> ApplyQueryBlankRowFilter(KeyValuePair<string, QueryExpression> innerQuery, QueryDefinition queryDefinition, QdmCommandTreeTranslator.BlankRowFilterContext blankRowFilterContext, bool composable)
		{
			if (blankRowFilterContext.IsBlankRowFilterRequired)
			{
				IEnumerable<string> names = blankRowFilterContext.ProjectedGroups.SelectMany((Group g) => g.Keys).GetNames();
				IEnumerable<string> enumerable = Enumerable.Empty<string>();
				IEnumerable<string> enumerable2 = Enumerable.Empty<string>();
				IEnumerable<QueryExpression> enumerable3 = Enumerable.Empty<QueryExpression>();
				if (blankRowFilterContext.JoinPredicateFieldName != null)
				{
					enumerable2 = new string[] { blankRowFilterContext.JoinPredicateFieldName };
				}
				else if (queryDefinition.AllowBlankRow == BlankRowBehavior.FilterByProjection)
				{
					if (composable)
					{
						enumerable3 = queryDefinition.DefaultMeasurePredicates.ToPredicateExpressions();
					}
					else
					{
						enumerable = queryDefinition.Measures.Select((Measure m) => m.Name);
					}
				}
				else
				{
					enumerable3 = queryDefinition.ExplicitJoinPredicates.EmptyIfNull<IJoinPredicate>().ToPredicateExpressions();
				}
				return QdmCommandTreeTranslator.ApplyBlankRowFilter(innerQuery, names.Concat(enumerable), enumerable2, enumerable3);
			}
			return innerQuery;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00025CC4 File Offset: 0x00023EC4
		private static QdmCommandTreeTranslator.BlankRowFilterContext DetermineBlankRowFilterRequired(QueryDefinition queryDefinition, IEnumerable<IJoinPredicate> joinPredicates)
		{
			IEnumerable<Group> enumerable = queryDefinition.Groups.Where((Group g) => g.IsProjected);
			bool flag = false;
			if (queryDefinition.AllowBlankRow != BlankRowBehavior.Allow && enumerable.Any<Group>())
			{
				if (queryDefinition.Groups.TakeUntil(enumerable.First<Group>()).Any((Group g) => g.FollowingJoinBehavior > FollowingJoinBehavior.InnerJoin))
				{
					flag = true;
				}
				else if (queryDefinition.AllowBlankRow == BlankRowBehavior.FilterByProjection)
				{
					IEnumerable<IJoinPredicate> enumerable2 = queryDefinition.Measures.OfType<IJoinPredicate>();
					if (!joinPredicates.Any<IJoinPredicate>() || joinPredicates.Intersect(enumerable2).Count<IJoinPredicate>() != joinPredicates.Count<IJoinPredicate>())
					{
						flag = true;
					}
				}
				else if (!queryDefinition.ExplicitJoinPredicates.EmptyIfNull<IJoinPredicate>().Any<IJoinPredicate>())
				{
					flag = true;
				}
			}
			string text = null;
			if (flag && queryDefinition.AllowBlankRow == BlankRowBehavior.FilterByExplicitJoinPredicates && queryDefinition.ExplicitJoinPredicates.EmptyIfNull<IJoinPredicate>().Any<IJoinPredicate>() && queryDefinition.Rollup != null)
			{
				text = queryDefinition.CreateAdditionalProjectionName("JoinPredicate");
			}
			return new QdmCommandTreeTranslator.BlankRowFilterContext(flag, text, joinPredicates, enumerable);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00025DDC File Offset: 0x00023FDC
		private static KeyValuePair<string, QueryExpression> ApplyBlankRowFilter(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<string> valueFieldNames, IEnumerable<string> predicateFieldNames, IEnumerable<QueryExpression> additionalPredicateExpressions)
		{
			QueryExpressionBinding filterInput = innerQuery.Value.BindAs(innerQuery.Key);
			QueryExpression[] array = valueFieldNames.Select((string f) => filterInput.Variable.Field(f).IsNull().Not()).Concat(predicateFieldNames.Select((string f) => filterInput.Variable.Field(f))).Concat(additionalPredicateExpressions)
				.ToArray<QueryExpression>();
			if (array.Length != 0)
			{
				QueryExpression queryExpression = array.OrAll();
				return filterInput.Filter(queryExpression).As(QdmNames.NonEmpty(filterInput.Variable.VariableName));
			}
			return innerQuery;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x00025E70 File Offset: 0x00024070
		private KeyValuePair<string, QueryExpression> ApplyQuerySlicer(KeyValuePair<string, QueryExpression> innerQuery, Filter slicer, IConceptualModel model, IConceptualSchema schema, DaxCapabilities daxCapabilities, IFeatureSwitchProvider featureSwitchProvider, IDataComparer comparer, CancellationToken cancellationToken)
		{
			if (!slicer.IsNullOrEmpty())
			{
				QueryExpression[] array = slicer.Conditions.QdmFilters(model, schema, daxCapabilities, featureSwitchProvider, comparer, cancellationToken, ScanKind.InheritFilterContextIncludeBlankRow, false);
				return innerQuery.Value.Calculate(array).As(QdmNames.Filtered(innerQuery.Key));
			}
			return innerQuery;
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00025EC0 File Offset: 0x000240C0
		private static KeyValuePair<string, QueryExpression> ApplyQuerySorting(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<SortItem> sortFields)
		{
			if (sortFields.Any<SortItem>())
			{
				QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
				return queryExpressionBinding.Sort(QdmCommandTreeTranslator.CreateSortClauses(queryExpressionBinding, sortFields)).As(QdmNames.Sorted(queryExpressionBinding.Variable.VariableName));
			}
			return innerQuery;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00025F10 File Offset: 0x00024110
		private KeyValuePair<string, QueryExpression> ApplyClearDefaultFiltersAndApplyFilters(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<IEdmFieldInstance> fields, IEnumerable<QueryDefinition> applyFilters)
		{
			IEnumerable<QueryExpression> enumerable = null;
			if (fields.Any<IEdmFieldInstance>())
			{
				enumerable = fields.Select((IEdmFieldInstance f) => f.All(null));
			}
			if (!applyFilters.IsNullOrEmpty<QueryDefinition>())
			{
				IEnumerable<QueryExpression> enumerable2 = applyFilters.Select((QueryDefinition applyFilter) => applyFilter.ToQueryCommandTree(new QdmTranslationSettings(true, false, false), this._featureSwitchProvider, this._cancellationToken, null).Query);
				enumerable = enumerable.EmptyIfNull<QueryExpression>().Concat(enumerable2);
			}
			if (enumerable != null)
			{
				return innerQuery.Value.Calculate(enumerable).As(innerQuery.Key);
			}
			return innerQuery;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00025F94 File Offset: 0x00024194
		private KeyValuePair<string, QueryExpression> ApplyClearDefaultFiltersAndApplyFilters(KeyValuePair<string, QueryExpression> innerQuery, IEnumerable<IConceptualColumn> columns, IEnumerable<QueryDefinition> applyFilters)
		{
			IEnumerable<QueryExpression> enumerable = null;
			if (columns.Any<IConceptualColumn>())
			{
				enumerable = columns.Select((IConceptualColumn c) => c.All());
			}
			if (!applyFilters.IsNullOrEmpty<QueryDefinition>())
			{
				IEnumerable<QueryExpression> enumerable2 = applyFilters.Select((QueryDefinition applyFilter) => applyFilter.ToQueryCommandTree(new QdmTranslationSettings(true, false, false), this._featureSwitchProvider, this._cancellationToken, null).Query);
				enumerable = enumerable.EmptyIfNull<QueryExpression>().Concat(enumerable2);
			}
			if (enumerable != null)
			{
				return innerQuery.Value.Calculate(enumerable).As(innerQuery.Key);
			}
			return innerQuery;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00026018 File Offset: 0x00024218
		private static IEnumerable<QuerySortClause> CreateSortClauses(QueryExpressionBinding binding, IEnumerable<SortItem> sortFields)
		{
			return from sortField in sortFields
				let fieldExpr = binding.Variable.Field(sortField.Name)
				select fieldExpr.ToSortClause(sortField.SortDirection);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00026068 File Offset: 0x00024268
		private static QueryExpression ApplyQueryStartAt(QueryExpression innerQuery, IList<QueryExpression> values)
		{
			if (values.Any<QueryExpression>())
			{
				return (innerQuery as QuerySortExpression).StartAt(values);
			}
			return innerQuery;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00026080 File Offset: 0x00024280
		private static KeyValuePair<string, QueryExpression> ApplyStartAtAsFilter(KeyValuePair<string, QueryExpression> innerQuery, IList<SortItem> sorting, Collection<QueryExpression> values)
		{
			if (!values.Any<QueryExpression>())
			{
				return innerQuery;
			}
			QueryExpressionBinding queryExpressionBinding = innerQuery.Value.BindAs(innerQuery.Key);
			List<QueryIsOnOrAfterArgument> list = new List<QueryIsOnOrAfterArgument>(values.Count);
			for (int i = 0; i < values.Count; i++)
			{
				QueryExpression queryExpression = values[i];
				SortItem sortItem = sorting[i];
				QueryIsOnOrAfterArgument queryIsOnOrAfterArgument = new QueryIsOnOrAfterArgument(queryExpressionBinding.Variable.Field(sortItem.Name), queryExpression, sortItem.SortDirection);
				list.Add(queryIsOnOrAfterArgument);
			}
			QueryIsOnOrAfterExpression queryIsOnOrAfterExpression = QueryExpressionBuilder.IsOnOrAfter(list);
			return queryExpressionBinding.Filter(queryIsOnOrAfterExpression).As(QdmNames.Filtered(queryExpressionBinding.Variable.VariableName));
		}

		// Token: 0x040009BF RID: 2495
		private readonly QueryDefinition _queryDefinition;

		// Token: 0x040009C0 RID: 2496
		private readonly DaxCapabilities _daxCapabilities;

		// Token: 0x040009C1 RID: 2497
		private readonly IFeatureSwitchProvider _featureSwitchProvider;

		// Token: 0x040009C2 RID: 2498
		private readonly QueryConstraintValidator _constraintValidator;

		// Token: 0x040009C3 RID: 2499
		private readonly ReadOnlyCollection<IJoinPredicate> _fullJoinPredicates;

		// Token: 0x040009C4 RID: 2500
		private readonly bool _hasUnanchoredJoinPredicates;

		// Token: 0x040009C5 RID: 2501
		private readonly bool _crossJoinQueryPlan;

		// Token: 0x040009C6 RID: 2502
		private readonly bool _composable;

		// Token: 0x040009C7 RID: 2503
		private readonly bool _allowSummarizeColumns;

		// Token: 0x040009C8 RID: 2504
		private readonly bool _useConceptualSchema;

		// Token: 0x040009C9 RID: 2505
		private readonly ReadOnlyCollection<QueryExpression> _existsFilters;

		// Token: 0x040009CA RID: 2506
		private readonly CancellationToken _cancellationToken;

		// Token: 0x040009CB RID: 2507
		private readonly IDataComparer _comparer;

		// Token: 0x02000307 RID: 775
		private enum GroupPredicateApplication
		{
			// Token: 0x04001100 RID: 4352
			PrecedingOnly,
			// Token: 0x04001101 RID: 4353
			FullImplicit,
			// Token: 0x04001102 RID: 4354
			FullExplicit
		}

		// Token: 0x02000308 RID: 776
		private sealed class GroupTranslationContext
		{
			// Token: 0x06001D63 RID: 7523 RVA: 0x00050C5C File Offset: 0x0004EE5C
			internal GroupTranslationContext(Group group, FollowingJoinBehavior followingJoinBehavior, IEnumerable<QueryExpression> detailMeasureContextFilters, IEnumerable<QueryExpression> joinPredicates, QueryExpression groupFilterPredicate, Limit limit, bool applyCalculate)
			{
				this.Group = ArgumentValidation.CheckNotNull<Group>(group, "group");
				this.FollowingJoinBehavior = followingJoinBehavior;
				this.DetailMeasureContextFilters = detailMeasureContextFilters;
				this.JoinPredicates = joinPredicates;
				this.GroupFilterPredicate = groupFilterPredicate;
				this.Limit = limit;
				this.ApplyCalculate = applyCalculate;
			}

			// Token: 0x170007EA RID: 2026
			// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00050CAE File Offset: 0x0004EEAE
			// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00050CB6 File Offset: 0x0004EEB6
			internal Group Group { get; private set; }

			// Token: 0x170007EB RID: 2027
			// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00050CBF File Offset: 0x0004EEBF
			// (set) Token: 0x06001D67 RID: 7527 RVA: 0x00050CC7 File Offset: 0x0004EEC7
			internal FollowingJoinBehavior FollowingJoinBehavior { get; private set; }

			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x06001D68 RID: 7528 RVA: 0x00050CD0 File Offset: 0x0004EED0
			// (set) Token: 0x06001D69 RID: 7529 RVA: 0x00050CD8 File Offset: 0x0004EED8
			internal IEnumerable<QueryExpression> DetailMeasureContextFilters { get; private set; }

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00050CE1 File Offset: 0x0004EEE1
			// (set) Token: 0x06001D6B RID: 7531 RVA: 0x00050CE9 File Offset: 0x0004EEE9
			internal IEnumerable<QueryExpression> JoinPredicates { get; private set; }

			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06001D6C RID: 7532 RVA: 0x00050CF2 File Offset: 0x0004EEF2
			// (set) Token: 0x06001D6D RID: 7533 RVA: 0x00050CFA File Offset: 0x0004EEFA
			internal QueryExpression GroupFilterPredicate { get; private set; }

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06001D6E RID: 7534 RVA: 0x00050D03 File Offset: 0x0004EF03
			// (set) Token: 0x06001D6F RID: 7535 RVA: 0x00050D0B File Offset: 0x0004EF0B
			internal Limit Limit { get; private set; }

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06001D70 RID: 7536 RVA: 0x00050D14 File Offset: 0x0004EF14
			// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00050D1C File Offset: 0x0004EF1C
			internal bool ApplyCalculate { get; private set; }
		}

		// Token: 0x02000309 RID: 777
		private sealed class BlankRowFilterContext
		{
			// Token: 0x06001D72 RID: 7538 RVA: 0x00050D25 File Offset: 0x0004EF25
			internal BlankRowFilterContext(bool isBlankRowFilterRequired, string joinPredicateFieldName, IEnumerable<IJoinPredicate> joinPredicates, IEnumerable<Group> projectedGroups)
			{
				this.IsBlankRowFilterRequired = isBlankRowFilterRequired;
				this.JoinPredicateFieldName = joinPredicateFieldName;
				this.JoinPredicates = joinPredicates;
				this.ProjectedGroups = projectedGroups;
			}

			// Token: 0x0400110A RID: 4362
			internal readonly bool IsBlankRowFilterRequired;

			// Token: 0x0400110B RID: 4363
			internal readonly string JoinPredicateFieldName;

			// Token: 0x0400110C RID: 4364
			internal readonly IEnumerable<IJoinPredicate> JoinPredicates;

			// Token: 0x0400110D RID: 4365
			internal readonly IEnumerable<Group> ProjectedGroups;
		}

		// Token: 0x0200030A RID: 778
		private sealed class PostRegroupLimitContext
		{
			// Token: 0x06001D73 RID: 7539 RVA: 0x00050D4A File Offset: 0x0004EF4A
			internal PostRegroupLimitContext(Limit limit, Group applyAtGroup, QdmCommandTreeTranslator.BlankRowFilterContext blankRowFilterContext)
			{
				this.Limit = ArgumentValidation.CheckNotNull<Limit>(limit, "limit");
				this.ApplyAtGroup = ArgumentValidation.CheckNotNull<Group>(applyAtGroup, "applyAtGroup");
				this.BlankRowFilterContext = blankRowFilterContext;
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00050D7B File Offset: 0x0004EF7B
			// (set) Token: 0x06001D75 RID: 7541 RVA: 0x00050D83 File Offset: 0x0004EF83
			internal Limit Limit { get; private set; }

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00050D8C File Offset: 0x0004EF8C
			// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00050D94 File Offset: 0x0004EF94
			internal Group ApplyAtGroup { get; private set; }

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00050D9D File Offset: 0x0004EF9D
			// (set) Token: 0x06001D79 RID: 7545 RVA: 0x00050DA5 File Offset: 0x0004EFA5
			internal QdmCommandTreeTranslator.BlankRowFilterContext BlankRowFilterContext { get; private set; }
		}

		// Token: 0x0200030B RID: 779
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001111 RID: 4369
			public static Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> <0>__GenerateAll;

			// Token: 0x04001112 RID: 4370
			public static Func<IReadOnlyList<QueryExpressionBinding>, QueryExpression> <1>__Generate;
		}
	}
}
