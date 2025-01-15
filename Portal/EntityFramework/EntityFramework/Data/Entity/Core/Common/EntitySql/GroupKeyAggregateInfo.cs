using System;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000654 RID: 1620
	internal sealed class GroupKeyAggregateInfo : GroupAggregateInfo
	{
		// Token: 0x06004DEA RID: 19946 RVA: 0x00118685 File Offset: 0x00116885
		internal GroupKeyAggregateInfo(GroupAggregateKind aggregateKind, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
			: base(aggregateKind, null, errCtx, containingAggregate, definingScopeRegion)
		{
		}
	}
}
