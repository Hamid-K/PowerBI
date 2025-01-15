using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A7 RID: 423
	public sealed class SingleResourceCastNode : SingleResourceNode
	{
		// Token: 0x06001424 RID: 5156 RVA: 0x0003B46C File Offset: 0x0003966C
		public SingleResourceCastNode(SingleResourceNode source, IEdmStructuredType structuredType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmStructuredType>(structuredType, "structuredType");
			this.source = source;
			this.navigationSource = ((source != null) ? source.NavigationSource : null);
			this.structuredTypeReference = structuredType.GetTypeReference();
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0003B4A5 File Offset: 0x000396A5
		public SingleResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0003B4AD File Offset: 0x000396AD
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0003B4B5 File Offset: 0x000396B5
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0003B4AD File Offset: 0x000396AD
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.structuredTypeReference;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0003B4BD File Offset: 0x000396BD
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleResourceCast;
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0003B4C1 File Offset: 0x000396C1
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008DB RID: 2267
		private readonly SingleResourceNode source;

		// Token: 0x040008DC RID: 2268
		private readonly IEdmStructuredTypeReference structuredTypeReference;

		// Token: 0x040008DD RID: 2269
		private readonly IEdmNavigationSource navigationSource;
	}
}
