using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x0200025F RID: 607
	public sealed class SingleEntityCastNode : SingleEntityNode
	{
		// Token: 0x06001568 RID: 5480 RVA: 0x0004B82C File Offset: 0x00049A2C
		public SingleEntityCastNode(SingleEntityNode source, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.source = source;
			this.navigationSource = ((source != null) ? source.NavigationSource : null);
			this.entityTypeReference = new EdmEntityTypeReference(entityType, false);
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0004B865 File Offset: 0x00049A65
		public SingleEntityNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x0004B86D File Offset: 0x00049A6D
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0004B875 File Offset: 0x00049A75
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x0004B87D File Offset: 0x00049A7D
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0004B885 File Offset: 0x00049A85
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.SingleEntityCast;
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004B889 File Offset: 0x00049A89
		public override T Accept<T>(QueryNodeVisitor<T> visitor)
		{
			ExceptionUtils.CheckArgumentNotNull<QueryNodeVisitor<T>>(visitor, "visitor");
			return visitor.Visit(this);
		}

		// Token: 0x040008E7 RID: 2279
		private readonly SingleEntityNode source;

		// Token: 0x040008E8 RID: 2280
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040008E9 RID: 2281
		private readonly IEdmNavigationSource navigationSource;
	}
}
