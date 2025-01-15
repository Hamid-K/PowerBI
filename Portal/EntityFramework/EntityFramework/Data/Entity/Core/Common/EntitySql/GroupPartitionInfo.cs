using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000656 RID: 1622
	internal sealed class GroupPartitionInfo : GroupAggregateInfo
	{
		// Token: 0x06004DF0 RID: 19952 RVA: 0x001186D9 File Offset: 0x001168D9
		internal GroupPartitionInfo(GroupPartitionExpr groupPartitionExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
			: base(GroupAggregateKind.Partition, groupPartitionExpr, errCtx, containingAggregate, definingScopeRegion)
		{
		}

		// Token: 0x06004DF1 RID: 19953 RVA: 0x001186E7 File Offset: 0x001168E7
		internal void AttachToAstNode(string aggregateName, DbExpression aggregateDefinition)
		{
			base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
			this.AggregateDefinition = aggregateDefinition;
		}

		// Token: 0x04001C43 RID: 7235
		internal DbExpression AggregateDefinition;
	}
}
