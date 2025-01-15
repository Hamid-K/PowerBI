using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.TableBuilders
{
	// Token: 0x020001CC RID: 460
	internal sealed class ExistsFilterTableBuilder
	{
		// Token: 0x0600102C RID: 4140 RVA: 0x00042EA4 File Offset: 0x000410A4
		internal static PlanOperation BuildExistsFilterTable(IReadOnlyList<PlanGroupByMember> primaryGroupByMembers, IReadOnlyList<PlanGroupByMember> secondaryGroupByMembers, IReadOnlyList<PlanGroupByDataTransformColumn> groupingTransformColumns, ExistsFilterCondition existsFilter, IEnumerable<PlanOperation> contextTables, PlanGroupAndJoinPredicateBehavior predicateBehavior)
		{
			IEnumerable<PlanGroupByMember> enumerable = primaryGroupByMembers.Select((PlanGroupByMember member) => new PlanGroupByMember(member.Member, false, true, null));
			IEnumerable<PlanGroupByMember> enumerable2 = secondaryGroupByMembers.Select((PlanGroupByMember member) => new PlanGroupByMember(member.Member, false, true, null));
			BatchGroupAndJoinBuilder batchGroupAndJoinBuilder = new BatchGroupAndJoinBuilder(true, true);
			batchGroupAndJoinBuilder.AddToPrimaryGroupingBucket(enumerable.Concat(enumerable2));
			batchGroupAndJoinBuilder.AddGroupingTransformColumns(groupingTransformColumns);
			batchGroupAndJoinBuilder.AddContextTables(contextTables);
			batchGroupAndJoinBuilder.AddExistsFilters(existsFilter);
			batchGroupAndJoinBuilder.PredicateBehavior = predicateBehavior;
			return batchGroupAndJoinBuilder.ToPlanOperation(null);
		}
	}
}
