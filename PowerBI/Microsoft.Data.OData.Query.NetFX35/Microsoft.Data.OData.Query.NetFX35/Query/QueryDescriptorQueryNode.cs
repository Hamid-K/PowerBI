using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x0200004C RID: 76
	public sealed class QueryDescriptorQueryNode : QueryNode
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A8CD File Offset: 0x00008ACD
		public override QueryNodeKind Kind
		{
			get
			{
				return QueryNodeKind.QueryDescriptor;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		public QueryNode Query { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000A8E1 File Offset: 0x00008AE1
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000A8E9 File Offset: 0x00008AE9
		public IEnumerable<QueryNode> CustomQueryOptions { get; set; }

		// Token: 0x060001DE RID: 478 RVA: 0x0000A8F2 File Offset: 0x00008AF2
		public static QueryDescriptorQueryNode ParseUri(Uri queryUri, Uri serviceBaseUri, IEdmModel model)
		{
			return QueryDescriptorQueryNode.ParseUri(queryUri, serviceBaseUri, model, 800);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A904 File Offset: 0x00008B04
		public static QueryDescriptorQueryNode ParseUri(Uri queryUri, Uri serviceBaseUri, IEdmModel model, int maxDepth)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			QueryDescriptorQueryToken queryDescriptorQueryToken = QueryDescriptorQueryToken.ParseUri(queryUri, serviceBaseUri, maxDepth);
			MetadataBinder metadataBinder = new MetadataBinder(model);
			return metadataBinder.BindQuery(queryDescriptorQueryToken);
		}

		// Token: 0x040001C5 RID: 453
		private const int DefaultMaxDepth = 800;
	}
}
