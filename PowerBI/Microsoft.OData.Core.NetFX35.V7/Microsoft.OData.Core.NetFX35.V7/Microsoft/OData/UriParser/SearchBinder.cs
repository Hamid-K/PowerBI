using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F2 RID: 242
	internal sealed class SearchBinder
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x0001E7A2 File Offset: 0x0001C9A2
		internal SearchBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001E7B4 File Offset: 0x0001C9B4
		internal SearchClause BindSearch(QueryToken search)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(search, "filter");
			QueryNode queryNode = this.bindMethod(search);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			return new SearchClause(singleValueNode);
		}

		// Token: 0x04000698 RID: 1688
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
