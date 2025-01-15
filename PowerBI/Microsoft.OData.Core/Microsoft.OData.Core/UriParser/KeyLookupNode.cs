using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000184 RID: 388
	internal sealed class KeyLookupNode : SingleEntityNode
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00039273 File Offset: 0x00037473
		public KeyLookupNode(CollectionResourceNode source, IEnumerable<KeyPropertyValue> keyPropertyValues)
		{
			ExceptionUtils.CheckArgumentNotNull<CollectionResourceNode>(source, "source");
			this.source = source;
			this.navigationSource = source.NavigationSource;
			this.entityTypeReference = source.ItemStructuredType as IEdmEntityTypeReference;
			this.keyPropertyValues = keyPropertyValues;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000392B2 File Offset: 0x000374B2
		public CollectionResourceNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x000392BA File Offset: 0x000374BA
		public IEnumerable<KeyPropertyValue> KeyPropertyValues
		{
			get
			{
				return this.keyPropertyValues;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x000392C2 File Offset: 0x000374C2
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x000392C2 File Offset: 0x000374C2
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x000392CA File Offset: 0x000374CA
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x000392C2 File Offset: 0x000374C2
		public override IEdmStructuredTypeReference StructuredTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x000392D2 File Offset: 0x000374D2
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.KeyLookup;
			}
		}

		// Token: 0x04000893 RID: 2195
		private readonly CollectionResourceNode source;

		// Token: 0x04000894 RID: 2196
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x04000895 RID: 2197
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x04000896 RID: 2198
		private readonly IEnumerable<KeyPropertyValue> keyPropertyValues;
	}
}
