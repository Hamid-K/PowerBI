using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200013A RID: 314
	internal sealed class KeyLookupNode : SingleEntityNode
	{
		// Token: 0x06000E21 RID: 3617 RVA: 0x000295AA File Offset: 0x000277AA
		public KeyLookupNode(CollectionResourceNode source, IEnumerable<KeyPropertyValue> keyPropertyValues)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceNode>(source, "source");
			this.source = source;
			this.navigationSource = source.NavigationSource;
			this.entityTypeReference = source.ItemStructuredType as IEdmEntityTypeReference;
			this.keyPropertyValues = keyPropertyValues;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x000295E9 File Offset: 0x000277E9
		public CollectionResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x000295F1 File Offset: 0x000277F1
		public IEnumerable<KeyPropertyValue> KeyPropertyValues
		{
			get
			{
				return this.keyPropertyValues;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x000295F9 File Offset: 0x000277F9
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000295F9 File Offset: 0x000277F9
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x00029601 File Offset: 0x00027801
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x000295F9 File Offset: 0x000277F9
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00029609 File Offset: 0x00027809
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.KeyLookup;
			}
		}

		// Token: 0x0400075E RID: 1886
		private readonly CollectionResourceNode source;

		// Token: 0x0400075F RID: 1887
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x04000760 RID: 1888
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x04000761 RID: 1889
		private readonly IEnumerable<KeyPropertyValue> keyPropertyValues;
	}
}
