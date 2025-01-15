using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.EntitySql.AST;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x0200064F RID: 1615
	internal sealed class FunctionAggregateInfo : GroupAggregateInfo
	{
		// Token: 0x06004DD1 RID: 19921 RVA: 0x00117ECF File Offset: 0x001160CF
		internal FunctionAggregateInfo(MethodExpr methodExpr, ErrorContext errCtx, GroupAggregateInfo containingAggregate, ScopeRegion definingScopeRegion)
			: base(GroupAggregateKind.Function, methodExpr, errCtx, containingAggregate, definingScopeRegion)
		{
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x00117EDD File Offset: 0x001160DD
		internal void AttachToAstNode(string aggregateName, DbAggregate aggregateDefinition)
		{
			base.AttachToAstNode(aggregateName, aggregateDefinition.ResultType);
			this.AggregateDefinition = aggregateDefinition;
		}

		// Token: 0x04001C2B RID: 7211
		internal DbAggregate AggregateDefinition;
	}
}
