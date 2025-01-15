using System;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000B2 RID: 178
	internal sealed class QueryGroupValueProjectionVisitor : IQueryGroupValueVisitor<QueryGroupValue>
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x00018C29 File Offset: 0x00016E29
		internal QueryGroupValueProjectionVisitor(Action<ProjectedDsqExpression> callback)
		{
			this._callback = callback;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00018C38 File Offset: 0x00016E38
		public QueryGroupValue Visit(QueryGroupSingleValue value)
		{
			this._callback(value.ProjectedExpression);
			return value;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00018C4C File Offset: 0x00016E4C
		public QueryGroupValue Visit(QueryGroupIntervalValue value)
		{
			this._callback(value.MinExpression);
			this._callback(value.MaxExpression);
			return value;
		}

		// Token: 0x0400037B RID: 891
		private readonly Action<ProjectedDsqExpression> _callback;
	}
}
