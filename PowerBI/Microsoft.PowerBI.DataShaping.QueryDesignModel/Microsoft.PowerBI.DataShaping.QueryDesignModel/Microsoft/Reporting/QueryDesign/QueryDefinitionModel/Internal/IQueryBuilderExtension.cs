using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x0200010A RID: 266
	internal interface IQueryBuilderExtension
	{
		// Token: 0x06000F82 RID: 3970
		void AddAutomaticGroupSorts(List<QueryBuilder.SortItemInfo> sorting, List<QueryBuilder.GroupInfo> groups, Func<SortItem, QueryBuilder.SortItemInfo> createSorting, Func<Group, QueryExpression, bool, bool, QueryBuilder.GroupKeyOrDetail> addOrReuseGroupDetail, GroupReference groupRef, bool omitProjection);

		// Token: 0x06000F83 RID: 3971
		void AddOrderBySort(List<QueryBuilder.SortItemInfo> sortItems, List<QueryBuilder.GroupInfo> groups, Func<SortItem, QueryBuilder.SortItemInfo> createItemInfo, Func<Group, QueryExpression, bool, bool, QueryBuilder.GroupKeyOrDetail> addOrReuseGroupDetail, GroupReference groupRef, QueryExpression queryExpr, SortDirection sortDir, bool calculateInMeasureContext, IEnumerable<GroupReference> additionalGroups);
	}
}
