using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001B8 RID: 440
	public sealed class SingleResourceCastNode : SingleResourceNode
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x00030A4A File Offset: 0x0002EC4A
		public SingleResourceCastNode(SingleResourceNode source, IEdmStructuredType structuredType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(structuredType, "structuredType");
			this.source = source;
			this.navigationSource = ((source != null) ? source.NavigationSource : null);
			this.structuredTypeReference = structuredType.GetTypeReference();
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00030A83 File Offset: 0x0002EC83
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00030A8B File Offset: 0x0002EC8B
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x00030A93 File Offset: 0x0002EC93
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00030A8B File Offset: 0x0002EC8B
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0002BED3 File Offset: 0x0002A0D3
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleResourceCast;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00030A9B File Offset: 0x0002EC9B
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008EA RID: 2282
		private readonly SingleResourceNode source;

		// Token: 0x040008EB RID: 2283
		private readonly IEdmStructuredTypeReference structuredTypeReference;

		// Token: 0x040008EC RID: 2284
		private readonly IEdmNavigationSource navigationSource;
	}
}
