using System;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D5 RID: 469
	internal sealed class SearchBinder
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x0003D6A9 File Offset: 0x0003B8A9
		internal SearchBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003D6B8 File Offset: 0x0003B8B8
		internal SearchClause BindSearch(QueryToken search)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryToken>(search, "filter");
			QueryNode queryNode = this.bindMethod(search);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			return new SearchClause(singleValueNode);
		}

		// Token: 0x0400078B RID: 1931
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
