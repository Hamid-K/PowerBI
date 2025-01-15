using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000130 RID: 304
	internal sealed class SearchBinder
	{
		// Token: 0x06001029 RID: 4137 RVA: 0x0002A236 File Offset: 0x00028436
		internal SearchBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0002A248 File Offset: 0x00028448
		internal SearchClause BindSearch(QueryToken search)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(search, "filter");
			QueryNode queryNode = this.bindMethod(search);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			return new SearchClause(singleValueNode);
		}

		// Token: 0x040007AB RID: 1963
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
