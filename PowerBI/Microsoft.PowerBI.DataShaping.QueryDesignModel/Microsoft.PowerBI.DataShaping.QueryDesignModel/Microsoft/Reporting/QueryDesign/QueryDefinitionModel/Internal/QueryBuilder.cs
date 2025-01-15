using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x02000108 RID: 264
	internal sealed class QueryBuilder : IQueryBuilder
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0002955C File Offset: 0x0002775C
		internal QueryBuilder(EntityDataModel entityDataModel, IConceptualSchema schema, bool useConceptualSchema, DefaultContextManager defaultContextManager = null, bool suppressDefaultContextFilters = false, bool includeDirectManyToManyAssociations = false, IQueryBuilderExtension builderExtension = null)
		{
			this._entityDataModel = entityDataModel;
			this._schema = schema;
			this._useConceptualSchema = useConceptualSchema;
			this._daxCapabilities = DaxCapabilitiesBuilder.BuildCapabilities(entityDataModel, schema, useConceptualSchema);
			this._namingContext = new QueryNamingContext(null);
			this._defaultContextManager = defaultContextManager ?? new DefaultContextManager();
			this._suppressDefaultContextFilters = suppressDefaultContextFilters;
			this._includeDirectManyToManyAssociations = includeDirectManyToManyAssociations;
			this._builderExtension = builderExtension;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0002962A File Offset: 0x0002782A
		private bool IsEmpty
		{
			get
			{
				return this._measures.Count == 0 && this._groups.Count == 0;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00029649 File Offset: 0x00027849
		public QueryNamingContext NamingContext
		{
			get
			{
				return this._namingContext;
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00029654 File Offset: 0x00027854
		public QueryDefinition GetQueryDefinition(bool omitDetails = false)
		{
			if (this.IsEmpty)
			{
				return null;
			}
			Rollup rollup = null;
			if (this._rollups.Count > 0)
			{
				rollup = new Rollup(this._rollups);
			}
			IList<Limit> list = this._limits.Select((QueryBuilder.LimitInfo l) => this.CreateQueryLimit(l, false)).Evaluate<Limit>();
			SortItem[] array = this.GetQuerySorting().ToArray<SortItem>();
			IEnumerable<QueryExpression> enumerable = this.CreateStartAt(array);
			IEnumerable<IConceptualColumn> enumerable2 = ((!this._useConceptualSchema || this._suppressDefaultContextFilters) ? null : this._defaultContextManager.GetColumnsRequiringClearDefaultFilterContext());
			IEnumerable<IEdmFieldInstance> enumerable3 = ((this._useConceptualSchema || this._suppressDefaultContextFilters) ? null : this._defaultContextManager.GetFieldsRequiringClearDefaultFilterContext());
			if (this._topLevelLimit != null && (array == null || array.Length == 0))
			{
				throw new InvalidOperationException("A top-level Limit may only be used when the query has sorting.");
			}
			EntityDataModel entityDataModel = this._entityDataModel;
			IConceptualSchema schema = this._schema;
			bool useConceptualSchema = this._useConceptualSchema;
			IEnumerable<Group> groupsAndOmitProjection = this.GetGroupsAndOmitProjection(list, omitDetails);
			IEnumerable<IJoinPredicate> explicitJoinPredicates = this._explicitJoinPredicates;
			BlankRowBehavior allowBlankRow = this._allowBlankRow;
			IEnumerable<IEdmFieldInstance> enumerable4 = enumerable3;
			IEnumerable<IConceptualColumn> enumerable5 = enumerable2;
			GroupFilter groupFilter = this._groupFilter;
			IEnumerable<Limit> enumerable6 = list;
			Limit limit = this.CreateQueryLimit(this._postRegroupLimit, true);
			IEnumerable<Measure> measures = this._measures;
			Rollup rollup2 = rollup;
			IEnumerable<FilterCondition> slicerConditions = this._slicerConditions;
			Filter filter = null;
			IEnumerable<QueryDefinition> applyFilters = this._applyFilters;
			IEnumerable<IEdmFieldInstance> enumerable7 = enumerable4;
			IEnumerable<IConceptualColumn> enumerable8 = enumerable5;
			IEnumerable<SortItem> enumerable9 = array;
			IEnumerable<QueryExpression> enumerable10 = enumerable;
			IEnumerable<QueryExpression> existsFilters = this._existsFilters;
			FilterCondition topLevelValueFilter = this._topLevelValueFilter;
			LimitOperator topLevelLimit = this._topLevelLimit;
			return new QueryDefinition(entityDataModel, schema, useConceptualSchema, groupsAndOmitProjection, explicitJoinPredicates, allowBlankRow, groupFilter, enumerable6, limit, measures, rollup2, slicerConditions, filter, applyFilters, enumerable7, enumerable8, enumerable9, enumerable10, this._declarations, existsFilters, topLevelValueFilter, topLevelLimit, this._includeDirectManyToManyAssociations, this._queryParameters);
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000297A3 File Offset: 0x000279A3
		private IEnumerable<QueryExpression> CreateStartAt(SortItem[] sorts)
		{
			if (this._startAt.Count == 0)
			{
				yield break;
			}
			if (this._startAt.Count > sorts.Length)
			{
				throw new InvalidOperationException(DevErrors.QueryDefinitionBuilder.TooManyStartAtValues(this._startAt.Count, sorts.Length));
			}
			int num;
			for (int i = 0; i < this._startAt.Count; i = num + 1)
			{
				ScalarValue scalarValue = this._startAt[i];
				SortItem sort = sorts[i];
				QueryExpression queryExpression = (from groupInfo in this._groups
					let expr = groupInfo.Group.GetExpressionByName(sort.Name)
					where expr != null
					select expr).Single<QueryExpression>();
				yield return QueryExpressionBuilder.ToExpressionFromDeclaredType(scalarValue, queryExpression.ConceptualResultType);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000297BA File Offset: 0x000279BA
		private IEnumerable<SortItem> GetQuerySorting()
		{
			HashSet<string> existingSorts = QueryBuilder.CreateNameHashSet();
			foreach (QueryBuilder.SortItemInfo sortItemInfo in this._sorting)
			{
				if (sortItemInfo.IsProjected && !existingSorts.Contains(sortItemInfo.SortItem.Name))
				{
					existingSorts.Add(sortItemInfo.SortItem.Name);
					yield return sortItemInfo.SortItem;
				}
			}
			List<QueryBuilder.SortItemInfo>.Enumerator enumerator = default(List<QueryBuilder.SortItemInfo>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000297CA File Offset: 0x000279CA
		private static HashSet<string> CreateNameHashSet()
		{
			return new HashSet<string>(EdmItem.IdentityComparer);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000297D8 File Offset: 0x000279D8
		private Limit CreateQueryLimit(QueryBuilder.LimitInfo info, bool postRegroupLimit)
		{
			if (info == null)
			{
				return null;
			}
			IEnumerable<SortItem> sortingForGroups = this.GetSortingForGroups(info.Groups, true, postRegroupLimit, false);
			return new Limit(info.Operator, info.Groups.Select((GroupReference g) => g.Group), info.PrimarySorts.Concat(sortingForGroups));
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0002983B File Offset: 0x00027A3B
		private IEnumerable<SortItem> GetSortingForGroups(IEnumerable<GroupReference> groups, bool includeNonProjectionSorts = false, bool includeRollupSorts = false, bool useGroupIdentity = false)
		{
			HashSet<string> includedSorts = QueryBuilder.CreateNameHashSet();
			foreach (QueryBuilder.SortItemInfo sortItemInfo in this._sorting)
			{
				if (sortItemInfo.RefersToAny(groups, useGroupIdentity) && !includedSorts.Contains(sortItemInfo.SortItem.Name) && (includeRollupSorts || !sortItemInfo.IsRollupSort) && (includeNonProjectionSorts || sortItemInfo.IsProjected))
				{
					includedSorts.Add(sortItemInfo.SortItem.Name);
					yield return sortItemInfo.SortItem;
				}
			}
			List<QueryBuilder.SortItemInfo>.Enumerator enumerator = default(List<QueryBuilder.SortItemInfo>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00029868 File Offset: 0x00027A68
		private IEnumerable<Group> GetGroupsAndOmitProjection(IList<Limit> limits, bool omitDetails)
		{
			foreach (QueryBuilder.GroupInfo groupInfo in this._groups)
			{
				Group group = groupInfo.Group;
				if (!groupInfo.IsProjected)
				{
					Limit limitForGroup = limits.GetLimitForGroup(group);
					group = group.OmitProjection(limitForGroup, null);
				}
				else if (omitDetails)
				{
					group = group.OmitDetails();
				}
				yield return group;
			}
			List<QueryBuilder.GroupInfo>.Enumerator enumerator = default(List<QueryBuilder.GroupInfo>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x00029888 File Offset: 0x00027A88
		public QueryParameterReferenceExpression DeclareQueryParameter(ConceptualResultType resultType, string parameterName)
		{
			QueryParameterDeclarationExpression queryParameterDeclarationExpression = resultType.DeclareParameterAs(parameterName);
			Util.AddToLazyList<QueryParameterDeclarationExpression>(ref this._queryParameters, queryParameterDeclarationExpression);
			return queryParameterDeclarationExpression.Parameter;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000298B0 File Offset: 0x00027AB0
		public QueryMeasureExpression DeclareMeasure(QueryExpression expression, EntitySet targetEntitySet, EdmMeasure edmMeasure, IConceptualEntity targetEntity, IConceptualMeasure measure)
		{
			QueryMeasureDeclarationExpression queryMeasureDeclarationExpression = expression.DeclareMeasureAs(targetEntitySet, edmMeasure, targetEntity, measure);
			return this.DeclareMeasure(queryMeasureDeclarationExpression);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000298D1 File Offset: 0x00027AD1
		internal QueryMeasureExpression DeclareMeasure(QueryMeasureDeclarationExpression declarationExpr)
		{
			Util.AddToLazyList<QueryBaseDeclarationExpression>(ref this._declarations, declarationExpr);
			return declarationExpr.MeasureRef;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000298E8 File Offset: 0x00027AE8
		public QueryFieldExpression DeclareField(QueryExpression expression, EntitySet targetEntitySet, EdmField field, IConceptualEntity targetEntity, IConceptualColumn column)
		{
			QueryFieldDeclarationExpression queryFieldDeclarationExpression = expression.RewriteEntityPlaceholdersToScalarEntityReferences(targetEntitySet, targetEntity).DeclareFieldAs(targetEntitySet, field, targetEntity, column);
			return this.DeclareField(queryFieldDeclarationExpression);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00029911 File Offset: 0x00027B11
		internal QueryFieldExpression DeclareField(QueryFieldDeclarationExpression declarationExpr)
		{
			Util.AddToLazyList<QueryBaseDeclarationExpression>(ref this._declarations, declarationExpr);
			return declarationExpr.FieldRef;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00029928 File Offset: 0x00027B28
		public void DeclareDataSourceVariables(string dataSourceVariables)
		{
			QueryDataSourceVariablesDeclarationExpression queryDataSourceVariablesDeclarationExpression = QueryExpressionBuilder.Literal(dataSourceVariables).DeclareDataSourceVariables();
			Util.AddToLazyList<QueryBaseDeclarationExpression>(ref this._declarations, queryDataSourceVariablesDeclarationExpression);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00029952 File Offset: 0x00027B52
		public void DeclareMParameter(QueryMParameterDeclarationExpression queryMParameterDeclarationExpression)
		{
			Util.AddToLazyList<QueryBaseDeclarationExpression>(ref this._declarations, queryMParameterDeclarationExpression);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00029960 File Offset: 0x00027B60
		public GroupReference AddOrReuseGroup(QueryExpression groupExpr, CompoundFilterCondition groupFilter = null, IEnumerable<QueryBuilder.SortDetail> sortDetails = null, bool omitProjection = false, FollowingJoinBehavior followingJoinBehavior = FollowingJoinBehavior.InnerJoin)
		{
			return this.AddOrReuseGroup(new QueryExpression[] { groupExpr }, groupFilter, sortDetails, omitProjection, followingJoinBehavior);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00029978 File Offset: 0x00027B78
		public GroupReference AddOrReuseGroup(IEnumerable<QueryExpression> groupExprs, out IEnumerable<GroupReference> existingGroupRefsWithOverlappingKeys, out GroupReference completeGroupChain, CompoundFilterCondition groupFilter = null, IEnumerable<QueryBuilder.SortDetail> sortDetails = null, bool omitProjection = false, FollowingJoinBehavior followingJoinBehavior = FollowingJoinBehavior.InnerJoin)
		{
			ArgumentValidation.CheckNotNullOrEmpty<QueryExpression>(groupExprs, "groupExprs");
			IEnumerable<QueryBuilder.GroupInfo> enumerable = this.FindProjectedGroupsWithOverlappingKeys(groupExprs);
			GroupReference groupReference = this.AddOrReuseGroup(groupExprs, groupFilter, sortDetails, omitProjection, followingJoinBehavior);
			if (groupReference.Usage == GroupReference.GroupUsage.New && enumerable.Any<QueryBuilder.GroupInfo>())
			{
				existingGroupRefsWithOverlappingKeys = enumerable.Select((QueryBuilder.GroupInfo g) => g.NewReference(GroupReference.GroupUsage.Reused));
			}
			else
			{
				existingGroupRefsWithOverlappingKeys = Enumerable.Empty<GroupReference>();
			}
			completeGroupChain = this.CreateNewGroupReference(groupExprs, followingJoinBehavior, omitProjection);
			return groupReference;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000299F8 File Offset: 0x00027BF8
		private GroupReference CreateNewGroupReference(IEnumerable<QueryExpression> groupExprs, FollowingJoinBehavior followingJoinBehavior, bool omitProjection)
		{
			IList<GroupKey> list = groupExprs.Select((QueryExpression e) => new GroupKey(this._namingContext.CreateOrReuseNameForGroupKey(e, null, null), e)).Evaluate<GroupKey>();
			return new QueryBuilder.GroupInfo(new Group(list[0].Name, list, null, true, followingJoinBehavior), !omitProjection).NewReference(GroupReference.GroupUsage.New);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00029A44 File Offset: 0x00027C44
		public GroupReference AddOrReuseGroup(IEnumerable<QueryExpression> groupExprs, CompoundFilterCondition groupFilter = null, IEnumerable<QueryBuilder.SortDetail> sortDetails = null, bool omitProjection = false, FollowingJoinBehavior followingJoinBehavior = FollowingJoinBehavior.InnerJoin)
		{
			ArgumentValidation.CheckNotNullOrEmpty<QueryExpression>(groupExprs, "groupExprs");
			IEnumerable<QueryExpression> enumerable;
			QueryBuilder.GroupInfo groupInfo = this.ReconcileExistingGroups(groupExprs, !omitProjection, omitProjection, out enumerable);
			GroupReference groupReference;
			if (groupInfo == null)
			{
				if (!omitProjection)
				{
					foreach (QueryExpression queryExpression in enumerable)
					{
						this.RemoveDuplicateDetails(queryExpression);
					}
				}
				IList<GroupKey> list = this.GenerateNewGroupKeys(enumerable);
				groupInfo = new QueryBuilder.GroupInfo(new Group(list[0].Name, list, null, true, followingJoinBehavior), !omitProjection);
				this._groups.Add(groupInfo);
				groupReference = groupInfo.NewReference(GroupReference.GroupUsage.New);
				this.AddImplicitGroupingDefaultFilterContextExclusions(groupInfo.Group, this._useConceptualSchema);
				this.AddGroupSorts(groupReference, sortDetails, omitProjection);
			}
			else
			{
				groupReference = this.ReuseGroup(groupInfo, sortDetails, !omitProjection);
			}
			if (groupFilter != null)
			{
				this.AddGroupFilter(groupFilter, null);
			}
			return groupReference;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00029B30 File Offset: 0x00027D30
		private QueryBuilder.GroupInfo ReconcileExistingGroups(IEnumerable<QueryExpression> groupExprs, bool newGroupIsProjected, bool checkGroupDetails, out IEnumerable<QueryExpression> remainingGroupExprs)
		{
			remainingGroupExprs = groupExprs.ToList<QueryExpression>();
			foreach (QueryBuilder.GroupInfo groupInfo in this._groups)
			{
				IList<QueryExpression> list = QueryBuilder.ExcludeExistingGroupExpressions(groupInfo, remainingGroupExprs, checkGroupDetails);
				if (!list.Any<QueryExpression>())
				{
					return groupInfo;
				}
				if (groupInfo.Group.Keys.GetExpressions().IsSubsetOf(remainingGroupExprs) && !groupInfo.IsProjected && newGroupIsProjected)
				{
					this.ReuseGroup(groupInfo, null, true);
				}
				remainingGroupExprs = list;
			}
			return null;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00029BD8 File Offset: 0x00027DD8
		private static IList<QueryExpression> ExcludeExistingGroupExpressions(QueryBuilder.GroupInfo existingGroup, IEnumerable<QueryExpression> groupExprs, bool checkGroupDetails)
		{
			IEnumerable<QueryExpression> enumerable = existingGroup.Group.Keys.GetExpressions();
			if (checkGroupDetails)
			{
				IEnumerable<GroupDetail> enumerable2 = existingGroup.Group.Details.Where((GroupDetail d) => d.IsProjected);
				enumerable = enumerable.Union(enumerable2.GetExpressions());
			}
			return groupExprs.Except(enumerable).Evaluate<QueryExpression>();
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00029C42 File Offset: 0x00027E42
		private IList<GroupKey> GenerateNewGroupKeys(IEnumerable<QueryExpression> newGroupExprs)
		{
			return newGroupExprs.Select((QueryExpression e) => new GroupKey(this._namingContext.CreateOrReuseNameForGroupKey(e, null, null), e)).Evaluate<GroupKey>();
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00029C5C File Offset: 0x00027E5C
		private GroupReference ReuseGroup(QueryBuilder.GroupInfo group, IEnumerable<QueryBuilder.SortDetail> sortDetails, bool isProjected)
		{
			ArgumentValidation.CheckNotNull<QueryBuilder.GroupInfo>(group, "group");
			GroupReference groupReference;
			if (isProjected && !group.IsProjected)
			{
				groupReference = group.NewReference(GroupReference.GroupUsage.Promoted);
				group.PromoteToProjected();
				this.AddGroupSorts(groupReference, sortDetails, !isProjected);
			}
			else
			{
				groupReference = group.NewReference(GroupReference.GroupUsage.Reused);
				if (sortDetails != null)
				{
					foreach (QueryBuilder.SortDetail sortDetail in sortDetails)
					{
						this.AddOrReuseGroupDetail(groupReference.Group, sortDetail.SortExpression, false, false);
					}
				}
			}
			return groupReference;
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00029CF4 File Offset: 0x00027EF4
		public void AddGroupFilter(FilterCondition filter, IEnumerable<GroupReference> excludeGroupRefs = null)
		{
			if (filter.IsNullOrEmpty())
			{
				return;
			}
			if (this._groupFilter != null)
			{
				throw new InvalidOperationException("Trying to add a group filter when a group filter has already been added. A query may only contain one group filter.");
			}
			IEnumerable<Group> enumerable = from g in excludeGroupRefs.EmptyIfNull<GroupReference>()
				where !g.WasReused
				select g.Group;
			IEnumerable<Group> enumerable2 = this._groups.Select((QueryBuilder.GroupInfo g) => g.Group).Except(enumerable);
			this._groupFilter = new GroupFilter(filter.ToCompoundFilterConditionIfNonNull(), enumerable2);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00029DB0 File Offset: 0x00027FB0
		private void AddGroupSorts(GroupReference groupRef, IEnumerable<QueryBuilder.SortDetail> sortDetails, bool omitProjection)
		{
			Func<SortItem, QueryBuilder.SortItemInfo> func = (SortItem sortItem) => new QueryBuilder.SortItemInfo(sortItem, groupRef, !omitProjection, false);
			this.AddUserSpecifiedSort(this._sorting, func, groupRef, sortDetails);
			IQueryBuilderExtension builderExtension = this._builderExtension;
			if (builderExtension == null)
			{
				return;
			}
			builderExtension.AddAutomaticGroupSorts(this._sorting, this._groups, func, new Func<Group, QueryExpression, bool, bool, QueryBuilder.GroupKeyOrDetail>(this.AddOrReuseGroupDetail), groupRef, omitProjection);
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00029E27 File Offset: 0x00028027
		public void AddLimit(GroupReference group, LimitOperator limitOperator, IEnumerable<QueryBuilder.SortDetail> primarySorts = null)
		{
			ArgumentValidation.CheckNotNull<GroupReference>(group, "group");
			this.AddLimit(new GroupReference[] { group }, limitOperator, primarySorts);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00029E48 File Offset: 0x00028048
		public void AddLimit(IEnumerable<GroupReference> groupRefs, LimitOperator limitOperator, IEnumerable<QueryBuilder.SortDetail> primarySorts = null)
		{
			QueryBuilder.LimitInfo limitInfo = this.CreateLimitInfo(groupRefs, limitOperator, primarySorts, false);
			if (limitInfo != null)
			{
				this._limits.Add(limitInfo);
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00029E70 File Offset: 0x00028070
		private QueryBuilder.LimitInfo CreateLimitInfo(IEnumerable<GroupReference> groupRefs, LimitOperator limitOperator, IEnumerable<QueryBuilder.SortDetail> primarySorts, bool postRegroupLimit)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<GroupReference>>(groupRefs, "groupRefs");
			IList<GroupReference> groups = groupRefs.Where((GroupReference r) => !r.WasReused || (postRegroupLimit && r.Usage == GroupReference.GroupUsage.Promoted)).Evaluate<GroupReference>();
			if (limitOperator == null || groups.Count == 0)
			{
				return null;
			}
			if (!postRegroupLimit && groups.Any((GroupReference g) => this._limits.Any((QueryBuilder.LimitInfo l) => l.RefersTo(g))))
			{
				throw new InvalidOperationException("Trying to add multiple limits to the same group is not allowed.");
			}
			IList<SortItem> list = null;
			if (primarySorts != null)
			{
				if (primarySorts.Any((QueryBuilder.SortDetail s) => !groups.Contains(s.TargetGroup)))
				{
					throw new InvalidOperationException("Trying to add a Limit sort expression that is not on the groups in the limit");
				}
				list = new List<SortItem>();
				foreach (QueryBuilder.SortDetail sortDetail in primarySorts)
				{
					Group group = sortDetail.TargetGroup.Group;
					QueryBuilder.GroupKeyOrDetail groupKeyOrDetail = this.AddOrReuseGroupDetail(group, sortDetail.SortExpression, sortDetail.CalculateInMeasureContext, true);
					list.Add(new SortItem(groupKeyOrDetail.Name, sortDetail.SortDirection, sortDetail.SortExpression, false));
				}
			}
			return new QueryBuilder.LimitInfo(limitOperator, groups, list);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00029FAC File Offset: 0x000281AC
		public void AddPostRegroupLimit(IEnumerable<GroupReference> groupRefs, LimitOperator limitOperator, IEnumerable<QueryBuilder.SortDetail> primarySorts = null)
		{
			if (this._postRegroupLimit != null)
			{
				throw new InvalidOperationException("Trying to add multiple post-regroup Limits is not allowed. A query may only contain one post-regroup Limit.");
			}
			this._postRegroupLimit = this.CreateLimitInfo(groupRefs, limitOperator, primarySorts, true);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00029FD1 File Offset: 0x000281D1
		public void AddTopLevelLimit(LimitOperator limitOperator)
		{
			if (this._topLevelLimit != null)
			{
				throw new InvalidOperationException("Trying to add multiple top-level Limits is not allowed. A query may only contain one top-level Limit.");
			}
			this._topLevelLimit = limitOperator;
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00029FED File Offset: 0x000281ED
		public void AddRollup(GroupReference groupRef, string aggregateIndicatorName, SortDirection aggregateSortDir)
		{
			ArgumentValidation.CheckNotNull<GroupReference>(groupRef, "groupRef");
			this.AddRollup(new GroupReference[] { groupRef }, aggregateIndicatorName, aggregateSortDir);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0002A010 File Offset: 0x00028210
		public void AddRollup(IEnumerable<GroupReference> groupRefs, string aggregateIndicatorName, SortDirection aggregateSortDir)
		{
			ArgumentValidation.CheckNotNullOrEmpty<GroupReference>(groupRefs, "groupRefs");
			if (string.IsNullOrEmpty(aggregateIndicatorName))
			{
				return;
			}
			IList<Group> list = (from r in groupRefs
				where !r.WasReused
				select r.Group).Evaluate<Group>();
			if (list.Count == 0)
			{
				return;
			}
			if (this.GetAndVerifyExistingRollup(list, aggregateIndicatorName) == null)
			{
				RollupGroup rollupGroup = new RollupGroup(aggregateIndicatorName, list);
				this._rollups.Add(rollupGroup);
				QueryBuilder.SortItemInfo sortItemInfo = new QueryBuilder.SortItemInfo(new SortItem(aggregateIndicatorName, aggregateSortDir, null, true), groupRefs, true, true);
				this.InsertGroupSpanningSort(groupRefs.First<GroupReference>(), new QueryBuilder.SortItemInfo[] { sortItemInfo }, false);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0002A0D4 File Offset: 0x000282D4
		private RollupGroup GetAndVerifyExistingRollup(IList<Group> groups, string aggregateIndicatorName)
		{
			RollupGroup rollupGroup = this._rollups.FirstOrDefault((RollupGroup r) => r.AggregateIndicatorNameEquals(aggregateIndicatorName));
			if (rollupGroup != null && !rollupGroup.GroupRefs.SequenceEqualByName(groups))
			{
				throw new InvalidOperationException("Trying to add a rollup with the same indicator name but different groups.");
			}
			using (IEnumerator<Group> enumerator = groups.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Group group = enumerator.Current;
					RollupGroup rollupGroup2 = this._rollups.FirstOrDefault((RollupGroup r) => r.RefersTo(group));
					if (rollupGroup2 != null && !rollupGroup2.AggregateIndicatorNameEquals(aggregateIndicatorName))
					{
						throw new InvalidOperationException("Trying to add multiple rollups with different aggregate indicator names to the same group.");
					}
				}
			}
			return rollupGroup;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0002A19C File Offset: 0x0002839C
		public void AddSorting(IEnumerable<GroupReference> groupRefs, IEnumerable<QueryBuilder.SortDetail> sortDetails)
		{
			ArgumentValidation.CheckNotNullOrEmpty<GroupReference>(groupRefs, "groupRefs");
			IList<GroupReference> referencesToSort = groupRefs.Where((GroupReference r) => !r.WasReused).Evaluate<GroupReference>();
			if (referencesToSort.Count == 0)
			{
				return;
			}
			List<QueryBuilder.SortItemInfo> list = new List<QueryBuilder.SortItemInfo>();
			GroupReference groupReference = referencesToSort.Last<GroupReference>();
			foreach (QueryBuilder.SortDetail sortDetail in sortDetails)
			{
				GroupReference groupReference2 = sortDetail.TargetGroup;
				if (groupReference2 == null)
				{
					groupReference2 = groupReference;
				}
				if (groupReference2 != null && !groupReference2.WasReused)
				{
					QueryBuilder.GroupInfo targetGroupInfo = this.GetGroupInfo(groupReference2.Group);
					this.AddUserSpecifiedSort(list, (SortItem sortItem) => new QueryBuilder.SortItemInfo(sortItem, referencesToSort, targetGroupInfo.IsProjected, false), groupReference2, sortDetail, referencesToSort);
				}
			}
			this.InsertGroupSpanningSort(referencesToSort[0], list, true);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0002A2B8 File Offset: 0x000284B8
		private void InsertGroupSpanningSort(GroupReference firstTargetGroup, IEnumerable<QueryBuilder.SortItemInfo> sorting, bool insertAfterRollupSort = false)
		{
			int i = 0;
			while (i < this._sorting.Count)
			{
				QueryBuilder.SortItemInfo sortItemInfo = this._sorting[i];
				if (sortItemInfo.RefersTo(firstTargetGroup, false))
				{
					if (sortItemInfo.IsRollupSort && insertAfterRollupSort)
					{
						i++;
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			this._sorting.InsertRange(i, sorting);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0002A30F File Offset: 0x0002850F
		public IEnumerable<SortItem> GetSorting(IEnumerable<GroupReference> groupRefs)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<GroupReference>>(groupRefs, "groupRefs");
			return this.GetSortingForGroups(groupRefs.Evaluate<GroupReference>(), false, true, true);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0002A32C File Offset: 0x0002852C
		private QueryBuilder.GroupInfo GetGroupInfo(Group group)
		{
			QueryBuilder.GroupInfo groupInfo = this._groups.FirstOrDefault((QueryBuilder.GroupInfo g) => g.Group.EqualsByName(group));
			Contracts.Check(groupInfo != null, DevErrors.QueryDefinitionBuilder.UnknownGroup(group.Name.MarkAsModelInfo()));
			return groupInfo;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002A37C File Offset: 0x0002857C
		private void AddUserSpecifiedSort(List<QueryBuilder.SortItemInfo> sortItems, Func<SortItem, QueryBuilder.SortItemInfo> createItemInfo, GroupReference groupRef, IEnumerable<QueryBuilder.SortDetail> sortDetails)
		{
			if (sortDetails != null)
			{
				foreach (QueryBuilder.SortDetail sortDetail in sortDetails)
				{
					this.AddUserSpecifiedSort(sortItems, createItemInfo, groupRef, sortDetail, null);
				}
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002A3D0 File Offset: 0x000285D0
		private void AddUserSpecifiedSort(List<QueryBuilder.SortItemInfo> sortItems, Func<SortItem, QueryBuilder.SortItemInfo> createItemInfo, GroupReference groupRef, QueryBuilder.SortDetail item, IEnumerable<GroupReference> additionalGroups = null)
		{
			IQueryBuilderExtension builderExtension = this._builderExtension;
			if (builderExtension != null)
			{
				builderExtension.AddOrderBySort(sortItems, this._groups, createItemInfo, new Func<Group, QueryExpression, bool, bool, QueryBuilder.GroupKeyOrDetail>(this.AddOrReuseGroupDetail), groupRef, item.SortExpression, item.SortDirection, item.CalculateInMeasureContext, additionalGroups);
			}
			this.AddGroupDetailSort(sortItems, createItemInfo, groupRef, item.SortExpression, item.SortDirection, item.CalculateInMeasureContext, item.AutoGenerated, additionalGroups);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002A444 File Offset: 0x00028644
		private void AddGroupDetailSort(List<QueryBuilder.SortItemInfo> sortItems, Func<SortItem, QueryBuilder.SortItemInfo> createItemInfo, GroupReference groupRef, QueryExpression fieldExpr, SortDirection sortDirection, bool calculateInMeasureContext = false, bool autoGenerated = false, IEnumerable<GroupReference> additionalGroups = null)
		{
			Group group = groupRef.Group;
			if (!fieldExpr.IsModelFieldReference())
			{
				string text = fieldExpr.GetDefaultName();
				if (text == null)
				{
					text = "DetailSort";
				}
				this._namingContext.CreateOrReuseNameForDetail(group.Keys.GetExpressions(), fieldExpr, text + "_" + group.Name, null);
			}
			GroupKey groupKey = group.Keys.FindByExpression(fieldExpr);
			if (groupKey != null)
			{
				SortItem sortItem = new SortItem(groupKey, sortDirection, fieldExpr, autoGenerated);
				sortItems.Add(createItemInfo(sortItem));
				return;
			}
			QueryBuilder.GroupKeyOrDetail groupKeyOrDetail = this.AddOrReuseGroupDetail(group, fieldExpr, calculateInMeasureContext, false);
			if (groupKeyOrDetail.GroupDetail != null || (additionalGroups != null && additionalGroups.Any((GroupReference g) => g.Group.ContainsKey(fieldExpr))))
			{
				SortItem sortItem2 = new SortItem(groupKeyOrDetail.Name, sortDirection, fieldExpr, autoGenerated);
				sortItems.Add(createItemInfo(sortItem2));
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002A548 File Offset: 0x00028748
		private void RemoveDuplicateDetails(QueryExpression detailExpr)
		{
			foreach (var <>f__AnonymousType in (from g in this._groups
				from d in g.Group.Details
				where d.Expression.Equals(detailExpr) && d.IsProjected
				select new
				{
					Group = g.Group,
					Detail = d,
					IsSorted = this._sorting.Any((QueryBuilder.SortItemInfo s) => s.SortItem.RefersTo(d))
				}).Evaluate())
			{
				int num = <>f__AnonymousType.Group.Details.IndexOf(<>f__AnonymousType.Detail);
				<>f__AnonymousType.Group.Details.RemoveAt(num);
				this._namingContext.Remove(<>f__AnonymousType.Detail.Name);
				if (<>f__AnonymousType.IsSorted)
				{
					GroupDetail groupDetail = <>f__AnonymousType.Detail.OmitProjection();
					<>f__AnonymousType.Group.Details.Insert(num, groupDetail);
				}
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002A674 File Offset: 0x00028874
		public QueryBuilder.GroupKeyOrDetail AddOrReuseGroupDetail(IEnumerable<QueryExpression> groupKeyExprs, QueryExpression detailExpr)
		{
			QueryBuilder.GroupInfo groupInfo = this.FindGroup(groupKeyExprs);
			return this.AddOrReuseGroupDetail(groupInfo.Group, detailExpr, false, false);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002A698 File Offset: 0x00028898
		public QueryBuilder.GroupKeyOrDetail AddOrReuseGroupDetail(Group group, QueryExpression detailExpr, bool calculateInMeasureContext = false, bool omitProjection = false)
		{
			QueryBuilder.GroupKeyOrDetail groupKeyOrDetail = this.FindGroupDetail(group, detailExpr);
			if (groupKeyOrDetail == null)
			{
				string text = this._namingContext.CreateOrReuseNameForDetail(group.Keys.GetExpressions(), detailExpr, null, null);
				if (!this.CanAddGroupDetail(group, detailExpr))
				{
					throw new InvalidOperationException(DevErrors.QueryDefinitionBuilder.TryingToAddIncompatibleDetail(group.Name.MarkAsModelInfo(), text.MarkAsModelInfo()));
				}
				groupKeyOrDetail = new QueryBuilder.GroupKeyOrDetail(text, new GroupDetail(detailExpr, text, !omitProjection, calculateInMeasureContext));
				group.Details.Add(groupKeyOrDetail.GroupDetail);
			}
			else
			{
				QueryBuilder.VerifyExistingGroupKeyOrDetail(groupKeyOrDetail, calculateInMeasureContext);
				if (groupKeyOrDetail.GroupDetail == null)
				{
					Group group2;
					if (this._useConceptualSchema)
					{
						IConceptualEntity conceptualEntity = detailExpr.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).Single<IConceptualEntity>();
						group2 = this.FindEntityGroup(conceptualEntity);
					}
					else
					{
						EntitySet entitySet = detailExpr.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).Single<EntitySet>();
						group2 = this.FindEntityGroup(entitySet, null);
					}
					if (group2 != null)
					{
						this._namingContext.AddGroupDetailNameForReusedGroup(group2.Keys.GetExpressions(), detailExpr, groupKeyOrDetail.Name);
					}
				}
			}
			return groupKeyOrDetail;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002A784 File Offset: 0x00028984
		public bool CanAddGroupDetail(Group group, QueryExpression candidateExpr)
		{
			ArgumentValidation.CheckNotNull<Group>(group, "group");
			candidateExpr = ArgumentValidation.CheckNotNull<QueryExpression>(candidateExpr, "candidateExpr");
			if (!candidateExpr.IsModelFieldReference())
			{
				return true;
			}
			if (this._useConceptualSchema)
			{
				IEquatable<IConceptualEntity> equatable = group.FindEntityReferences().Single<IConceptualEntity>();
				IConceptualEntity conceptualEntity = candidateExpr.FindEntityReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).Single<IConceptualEntity>();
				return equatable.Equals(conceptualEntity);
			}
			EntitySetBase entitySetBase = group.FindEntitySetReferences().Single<EntitySet>();
			EntitySet entitySet = candidateExpr.FindEntitySetReferences(QdmExpressionBuilder.EntityRefSearchBehavior.All).Single<EntitySet>();
			return entitySetBase.Equals(entitySet);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002A7F9 File Offset: 0x000289F9
		private static void VerifyExistingGroupKeyOrDetail(QueryBuilder.GroupKeyOrDetail detail, bool calculateInMeasureContext)
		{
			if (detail.GroupDetail != null && detail.GroupDetail.CalculateInMeasureContext != calculateInMeasureContext)
			{
				throw new InvalidOperationException("Trying to add a query detail that is inconsistent with an existing query detail.");
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002A81C File Offset: 0x00028A1C
		public Measure AddOrReuseMeasure(QueryExpression measureExpr, bool suppressJoinPredicate = false, string fallbackCandidateName = null)
		{
			return this.AddOrReuseMeasureCore(measureExpr, suppressJoinPredicate, fallbackCandidateName);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002A828 File Offset: 0x00028A28
		private Measure AddOrReuseMeasureCore(QueryExpression measureExpr, bool suppressJoinPredicate, string fallbackCandidateName)
		{
			Measure measure = this.FindMeasure(measureExpr);
			if (measure == null)
			{
				string text = this._namingContext.CreateOrReuseNameForMeasure(measureExpr, null, fallbackCandidateName);
				measure = Measure.Create(measureExpr, text, suppressJoinPredicate);
				this._measures.Add(measure);
			}
			else
			{
				QueryBuilder.VerifyExistingMeasure(measure, suppressJoinPredicate);
			}
			return measure;
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002A86E File Offset: 0x00028A6E
		private static void VerifyExistingMeasure(Measure measure, bool suppressJoinPredicate)
		{
			if (!suppressJoinPredicate && !(measure is IJoinPredicate))
			{
				throw new InvalidOperationException("Trying to add a query measure that is inconsistent with an existing query measure.");
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002A886 File Offset: 0x00028A86
		public void SetTopLevelValueFilter(FilterCondition filterCondition)
		{
			this._topLevelValueFilter = filterCondition;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002A890 File Offset: 0x00028A90
		public void AddSlicer(FilterCondition filterCondition)
		{
			if (filterCondition.IsNullOrEmpty())
			{
				return;
			}
			if (this._useConceptualSchema)
			{
				this._defaultContextManager.AddColumnsRequiringClearedContext(filterCondition);
			}
			else
			{
				this._defaultContextManager.AddFieldsRequiringClearedContext(filterCondition);
			}
			CompoundFilterCondition compoundFilterCondition = filterCondition as CompoundFilterCondition;
			if (compoundFilterCondition != null && compoundFilterCondition.Operator == CompoundFilterOperator.All)
			{
				this._slicerConditions.AddRange(compoundFilterCondition.Conditions);
				return;
			}
			this._slicerConditions.Add(filterCondition);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002A8F8 File Offset: 0x00028AF8
		public void AddApplyFilter(QueryDefinition applyFilterQueryDefinition)
		{
			this._applyFilters.Add(applyFilterQueryDefinition);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002A906 File Offset: 0x00028B06
		public void AddFieldRequiringClearedContext(IEdmFieldInstance field)
		{
			this._defaultContextManager.AddFieldRequiringClearedContext(field);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0002A914 File Offset: 0x00028B14
		public void AddColumnRequiringClearedContext(IConceptualColumn column)
		{
			this._defaultContextManager.AddColumnRequiringClearedContext(column);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0002A922 File Offset: 0x00028B22
		public void AddFieldRequiringImplicitGroupingClearedContext(IEdmFieldInstance field)
		{
			this._defaultContextManager.AddFieldRequiringImplicitGroupingClearedContext(new IEdmFieldInstance[] { field });
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0002A939 File Offset: 0x00028B39
		public void AddColumnRequiringImplicitGroupingClearedContext(IConceptualColumn column)
		{
			this._defaultContextManager.AddColumnRequiringImplicitGroupingClearedContext(new IConceptualColumn[] { column });
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0002A950 File Offset: 0x00028B50
		public void AddFieldRequiringDefaultContext(IEdmFieldInstance field)
		{
			this._defaultContextManager.AddFieldRequiringDefaultContext(field);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002A95E File Offset: 0x00028B5E
		public void AddColumnRequiringDefaultContext(IConceptualColumn column)
		{
			this._defaultContextManager.AddColumnRequiringDefaultContext(column);
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002A96C File Offset: 0x00028B6C
		internal void AddImplicitGroupingDefaultFilterContextExclusions(Group group, bool useConceptualSchema)
		{
			this._defaultContextManager.AddImplicitGroupingDefaultFilterExclusions(group, useConceptualSchema);
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0002A97B File Offset: 0x00028B7B
		public void AddExistsFilters(IEnumerable<QueryExpression> existsFilters)
		{
			this._existsFilters.AddRange(existsFilters);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0002A989 File Offset: 0x00028B89
		private QueryBuilder.GroupInfo FindGroup(IEnumerable<QueryExpression> groupExprs)
		{
			return this.FindGroup(groupExprs, false);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0002A994 File Offset: 0x00028B94
		private QueryBuilder.GroupInfo FindGroup(IEnumerable<QueryExpression> groupExprs, bool checkGroupDetails)
		{
			if (groupExprs.IsNullOrEmpty<QueryExpression>())
			{
				return null;
			}
			IEnumerable<QueryExpression> enumerable = groupExprs.ToList<QueryExpression>();
			foreach (QueryBuilder.GroupInfo groupInfo in this._groups)
			{
				enumerable = QueryBuilder.ExcludeExistingGroupExpressions(groupInfo, enumerable, checkGroupDetails);
				if (!enumerable.Any<QueryExpression>())
				{
					return groupInfo;
				}
			}
			return null;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0002AA0C File Offset: 0x00028C0C
		private IEnumerable<QueryBuilder.GroupInfo> FindProjectedGroupsWithOverlappingKeys(IEnumerable<QueryExpression> groupExprs)
		{
			List<QueryBuilder.GroupInfo> list = new List<QueryBuilder.GroupInfo>();
			foreach (QueryBuilder.GroupInfo groupInfo in this._groups)
			{
				IEnumerable<QueryExpression> expressions = groupInfo.Group.Keys.GetExpressions();
				if (groupExprs.Intersect(expressions).Any<QueryExpression>())
				{
					list.Add(groupInfo);
				}
			}
			return list;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0002AA88 File Offset: 0x00028C88
		public Group FindEntityGroup(EntitySet entitySet, IConceptualEntity entity = null)
		{
			QueryBuilder.GroupInfo groupInfo = this.FindGroup(entitySet.QdmKeyReferences(entity).Cast<QueryExpression>());
			if (groupInfo != null)
			{
				return groupInfo.Group;
			}
			return null;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0002AAB4 File Offset: 0x00028CB4
		public Group FindEntityGroup(IConceptualEntity entity)
		{
			QueryBuilder.GroupInfo groupInfo = this.FindGroup(entity.QdmKeyReferences().Cast<QueryExpression>());
			if (groupInfo != null)
			{
				return groupInfo.Group;
			}
			return null;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		private QueryBuilder.GroupKeyOrDetail FindGroupDetail(Group group, QueryExpression detailExpr)
		{
			bool flag = detailExpr.IsModelFieldReference();
			foreach (QueryBuilder.GroupInfo groupInfo in this._groups)
			{
				bool isTargetGroup = group == groupInfo.Group;
				GroupKey groupKey = groupInfo.Group.Keys.FindByExpression(detailExpr);
				if (groupKey != null)
				{
					return new QueryBuilder.GroupKeyOrDetail(groupKey.Name, null);
				}
				if (isTargetGroup || flag)
				{
					GroupDetail groupDetail = groupInfo.Group.Details.FirstOrDefault((GroupDetail d) => (isTargetGroup || d.IsProjected) && d.Expression.Equals(detailExpr));
					if (groupDetail != null)
					{
						return new QueryBuilder.GroupKeyOrDetail(groupDetail.Name, groupDetail);
					}
				}
			}
			return null;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0002ABE4 File Offset: 0x00028DE4
		private Measure FindMeasure(QueryExpression measureExpr)
		{
			return this._measures.FirstOrDefault((Measure m) => m.Expression.Equals(measureExpr));
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002AC15 File Offset: 0x00028E15
		public void SetJoinPredicates(IEnumerable<IJoinPredicate> joinPredicates)
		{
			if (!joinPredicates.SequenceEqual(this.GetJoinPredicates(), JoinPredicates.Comparer))
			{
				this._explicitJoinPredicates = joinPredicates;
				if (this._allowBlankRow == BlankRowBehavior.FilterByProjection)
				{
					this._allowBlankRow = BlankRowBehavior.FilterByExplicitJoinPredicates;
				}
			}
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002AC40 File Offset: 0x00028E40
		public void MergeJoinPredicates(IEnumerable<QueryDefinition> subqueries, IFeatureSwitchProvider featureSwitchProvider, CancellationToken cancellationToken)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<QueryDefinition>>(subqueries, "subqueries");
			IEnumerable<SubqueryJoinPredicate> enumerable = subqueries.Select((QueryDefinition s) => SubqueryJoinPredicate.Create(s, featureSwitchProvider, cancellationToken));
			IEnumerable<IJoinPredicate> enumerable2 = this.GetJoinPredicates().Concat(enumerable);
			this.SetJoinPredicates(enumerable2);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0002AC94 File Offset: 0x00028E94
		public void SetAllowBlankRow()
		{
			this._allowBlankRow = BlankRowBehavior.Allow;
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0002ACA0 File Offset: 0x00028EA0
		private IEnumerable<IJoinPredicate> GetJoinPredicates()
		{
			return new QueryConstraintValidator(this._entityDataModel, this._schema, this._groups.Select((QueryBuilder.GroupInfo g) => g.Group), this._measures, this._explicitJoinPredicates, false, this._includeDirectManyToManyAssociations, this._useConceptualSchema).GetJoinPredicates();
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0002AD06 File Offset: 0x00028F06
		public void AddStartAt(ScalarValue value)
		{
			this._startAt.Add(value);
		}

		// Token: 0x040009F5 RID: 2549
		private readonly EntityDataModel _entityDataModel;

		// Token: 0x040009F6 RID: 2550
		private readonly IConceptualSchema _schema;

		// Token: 0x040009F7 RID: 2551
		private readonly DaxCapabilities _daxCapabilities;

		// Token: 0x040009F8 RID: 2552
		private readonly QueryNamingContext _namingContext;

		// Token: 0x040009F9 RID: 2553
		private readonly List<QueryBuilder.GroupInfo> _groups = new List<QueryBuilder.GroupInfo>();

		// Token: 0x040009FA RID: 2554
		private readonly List<RollupGroup> _rollups = new List<RollupGroup>();

		// Token: 0x040009FB RID: 2555
		private readonly List<QueryBuilder.LimitInfo> _limits = new List<QueryBuilder.LimitInfo>();

		// Token: 0x040009FC RID: 2556
		private readonly List<Measure> _measures = new List<Measure>();

		// Token: 0x040009FD RID: 2557
		private readonly List<FilterCondition> _slicerConditions = new List<FilterCondition>();

		// Token: 0x040009FE RID: 2558
		private readonly List<QueryDefinition> _applyFilters = new List<QueryDefinition>();

		// Token: 0x040009FF RID: 2559
		private readonly List<QueryBuilder.SortItemInfo> _sorting = new List<QueryBuilder.SortItemInfo>();

		// Token: 0x04000A00 RID: 2560
		private readonly List<ScalarValue> _startAt = new List<ScalarValue>();

		// Token: 0x04000A01 RID: 2561
		private readonly List<QueryExpression> _existsFilters = new List<QueryExpression>();

		// Token: 0x04000A02 RID: 2562
		private readonly DefaultContextManager _defaultContextManager;

		// Token: 0x04000A03 RID: 2563
		private readonly bool _suppressDefaultContextFilters;

		// Token: 0x04000A04 RID: 2564
		private readonly bool _includeDirectManyToManyAssociations;

		// Token: 0x04000A05 RID: 2565
		private readonly bool _useConceptualSchema;

		// Token: 0x04000A06 RID: 2566
		private readonly IQueryBuilderExtension _builderExtension;

		// Token: 0x04000A07 RID: 2567
		private QueryBuilder.LimitInfo _postRegroupLimit;

		// Token: 0x04000A08 RID: 2568
		private LimitOperator _topLevelLimit;

		// Token: 0x04000A09 RID: 2569
		private BlankRowBehavior _allowBlankRow;

		// Token: 0x04000A0A RID: 2570
		private IEnumerable<IJoinPredicate> _explicitJoinPredicates;

		// Token: 0x04000A0B RID: 2571
		private GroupFilter _groupFilter;

		// Token: 0x04000A0C RID: 2572
		private FilterCondition _topLevelValueFilter;

		// Token: 0x04000A0D RID: 2573
		private List<QueryBaseDeclarationExpression> _declarations;

		// Token: 0x04000A0E RID: 2574
		private List<QueryParameterDeclarationExpression> _queryParameters;

		// Token: 0x02000341 RID: 833
		internal sealed class GroupInfo
		{
			// Token: 0x06001E77 RID: 7799 RVA: 0x00054957 File Offset: 0x00052B57
			internal GroupInfo(Group group, bool isProjected)
			{
				this._group = group;
				this._isProjected = isProjected;
				this._nextReferenceId = 0;
			}

			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00054974 File Offset: 0x00052B74
			public Group Group
			{
				get
				{
					return this._group;
				}
			}

			// Token: 0x170007FD RID: 2045
			// (get) Token: 0x06001E79 RID: 7801 RVA: 0x0005497C File Offset: 0x00052B7C
			public bool IsProjected
			{
				get
				{
					return this._isProjected;
				}
			}

			// Token: 0x06001E7A RID: 7802 RVA: 0x00054984 File Offset: 0x00052B84
			public GroupReference NewReference(GroupReference.GroupUsage usage)
			{
				Group group = this._group;
				int nextReferenceId = this._nextReferenceId;
				this._nextReferenceId = nextReferenceId + 1;
				return new GroupReference(group, usage, nextReferenceId);
			}

			// Token: 0x06001E7B RID: 7803 RVA: 0x000549AE File Offset: 0x00052BAE
			internal void PromoteToProjected()
			{
				this._isProjected = true;
			}

			// Token: 0x040011C1 RID: 4545
			private readonly Group _group;

			// Token: 0x040011C2 RID: 4546
			private bool _isProjected;

			// Token: 0x040011C3 RID: 4547
			private int _nextReferenceId;
		}

		// Token: 0x02000342 RID: 834
		internal sealed class SortItemInfo
		{
			// Token: 0x06001E7C RID: 7804 RVA: 0x000549B7 File Offset: 0x00052BB7
			internal SortItemInfo(SortItem item, GroupReference groupRef, bool isProjected, bool isRollupSort = false)
				: this(item, new GroupReference[] { groupRef }, isProjected, isRollupSort)
			{
			}

			// Token: 0x06001E7D RID: 7805 RVA: 0x000549CD File Offset: 0x00052BCD
			internal SortItemInfo(SortItem item, IEnumerable<GroupReference> groupRefs, bool isProjected, bool isRollupSort = false)
			{
				this.SortItem = item;
				this.GroupReferences = groupRefs.ToReadOnlyCollection<GroupReference>();
				this.IsProjected = isProjected;
				this.IsRollupSort = isRollupSort;
			}

			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x06001E7E RID: 7806 RVA: 0x000549F7 File Offset: 0x00052BF7
			// (set) Token: 0x06001E7F RID: 7807 RVA: 0x000549FF File Offset: 0x00052BFF
			public SortItem SortItem { get; private set; }

			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00054A08 File Offset: 0x00052C08
			// (set) Token: 0x06001E81 RID: 7809 RVA: 0x00054A10 File Offset: 0x00052C10
			public ReadOnlyCollection<GroupReference> GroupReferences { get; private set; }

			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00054A19 File Offset: 0x00052C19
			// (set) Token: 0x06001E83 RID: 7811 RVA: 0x00054A21 File Offset: 0x00052C21
			public bool IsProjected { get; private set; }

			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06001E84 RID: 7812 RVA: 0x00054A2A File Offset: 0x00052C2A
			// (set) Token: 0x06001E85 RID: 7813 RVA: 0x00054A32 File Offset: 0x00052C32
			public bool IsRollupSort { get; private set; }

			// Token: 0x06001E86 RID: 7814 RVA: 0x00054A3C File Offset: 0x00052C3C
			public bool RefersTo(GroupReference reference, bool useGroupIdentity = false)
			{
				if (useGroupIdentity)
				{
					return this.GroupReferences.Any((GroupReference g) => g.Group == reference.Group);
				}
				return this.GroupReferences.Any((GroupReference g) => g.Equals(reference));
			}

			// Token: 0x06001E87 RID: 7815 RVA: 0x00054A88 File Offset: 0x00052C88
			public bool RefersToAny(IEnumerable<GroupReference> references, bool useGroupIdentity = false)
			{
				return references.Any((GroupReference r) => this.RefersTo(r, useGroupIdentity));
			}
		}

		// Token: 0x02000343 RID: 835
		private sealed class LimitInfo
		{
			// Token: 0x06001E88 RID: 7816 RVA: 0x00054ABB File Offset: 0x00052CBB
			internal LimitInfo(LimitOperator limitOp, IEnumerable<GroupReference> groups, IEnumerable<SortItem> primarySorts)
			{
				this.Operator = limitOp;
				this.Groups = groups.ToReadOnlyCollection<GroupReference>();
				this.PrimarySorts = primarySorts.ToReadOnlyCollection<SortItem>();
			}

			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00054AE2 File Offset: 0x00052CE2
			// (set) Token: 0x06001E8A RID: 7818 RVA: 0x00054AEA File Offset: 0x00052CEA
			public LimitOperator Operator { get; private set; }

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00054AF3 File Offset: 0x00052CF3
			// (set) Token: 0x06001E8C RID: 7820 RVA: 0x00054AFB File Offset: 0x00052CFB
			public ReadOnlyCollection<GroupReference> Groups { get; private set; }

			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00054B04 File Offset: 0x00052D04
			// (set) Token: 0x06001E8E RID: 7822 RVA: 0x00054B0C File Offset: 0x00052D0C
			public ReadOnlyCollection<SortItem> PrimarySorts { get; private set; }

			// Token: 0x06001E8F RID: 7823 RVA: 0x00054B15 File Offset: 0x00052D15
			public bool RefersTo(GroupReference groupRef)
			{
				return this.Groups.Contains(groupRef);
			}
		}

		// Token: 0x02000344 RID: 836
		internal sealed class GroupKeyOrDetail
		{
			// Token: 0x06001E90 RID: 7824 RVA: 0x00054B23 File Offset: 0x00052D23
			internal GroupKeyOrDetail(string name, GroupDetail groupDetail)
			{
				this.Name = ArgumentValidation.CheckNotNullOrEmpty(name, "name");
				this.GroupDetail = groupDetail;
			}

			// Token: 0x17000805 RID: 2053
			// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00054B43 File Offset: 0x00052D43
			// (set) Token: 0x06001E92 RID: 7826 RVA: 0x00054B4B File Offset: 0x00052D4B
			public string Name { get; private set; }

			// Token: 0x17000806 RID: 2054
			// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00054B54 File Offset: 0x00052D54
			// (set) Token: 0x06001E94 RID: 7828 RVA: 0x00054B5C File Offset: 0x00052D5C
			public GroupDetail GroupDetail { get; private set; }
		}

		// Token: 0x02000345 RID: 837
		internal sealed class SortDetail
		{
			// Token: 0x06001E95 RID: 7829 RVA: 0x00054B65 File Offset: 0x00052D65
			internal SortDetail(QueryExpression sortExpression, SortDirection sortDirection, bool calculateInMeasureContext = false, bool autoGenerated = false, GroupReference targetGroup = null)
			{
				this.SortExpression = ArgumentValidation.CheckNotNull<QueryExpression>(sortExpression, "sortExpression");
				this.SortDirection = sortDirection;
				this.CalculateInMeasureContext = calculateInMeasureContext;
				this.AutoGenerated = autoGenerated;
				this.TargetGroup = targetGroup;
			}

			// Token: 0x17000807 RID: 2055
			// (get) Token: 0x06001E96 RID: 7830 RVA: 0x00054B9C File Offset: 0x00052D9C
			// (set) Token: 0x06001E97 RID: 7831 RVA: 0x00054BA4 File Offset: 0x00052DA4
			public QueryExpression SortExpression { get; private set; }

			// Token: 0x17000808 RID: 2056
			// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00054BAD File Offset: 0x00052DAD
			// (set) Token: 0x06001E99 RID: 7833 RVA: 0x00054BB5 File Offset: 0x00052DB5
			public SortDirection SortDirection { get; private set; }

			// Token: 0x17000809 RID: 2057
			// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00054BBE File Offset: 0x00052DBE
			// (set) Token: 0x06001E9B RID: 7835 RVA: 0x00054BC6 File Offset: 0x00052DC6
			public bool CalculateInMeasureContext { get; private set; }

			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06001E9C RID: 7836 RVA: 0x00054BCF File Offset: 0x00052DCF
			// (set) Token: 0x06001E9D RID: 7837 RVA: 0x00054BD7 File Offset: 0x00052DD7
			public bool AutoGenerated { get; private set; }

			// Token: 0x1700080B RID: 2059
			// (get) Token: 0x06001E9E RID: 7838 RVA: 0x00054BE0 File Offset: 0x00052DE0
			// (set) Token: 0x06001E9F RID: 7839 RVA: 0x00054BE8 File Offset: 0x00052DE8
			public GroupReference TargetGroup { get; private set; }

			// Token: 0x06001EA0 RID: 7840 RVA: 0x00054BF4 File Offset: 0x00052DF4
			public QueryBuilder.SortDetail ReverseSortDirection()
			{
				SortDirection sortDirection = ((this.SortDirection == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending);
				return new QueryBuilder.SortDetail(this.SortExpression, sortDirection, this.CalculateInMeasureContext, this.AutoGenerated, this.TargetGroup);
			}
		}
	}
}
