using System;
using System.Collections.Generic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Semantic
{
	// Token: 0x02000241 RID: 577
	internal sealed class KeyLookupNode : SingleEntityNode
	{
		// Token: 0x060014A1 RID: 5281 RVA: 0x00049B8C File Offset: 0x00047D8C
		public KeyLookupNode(EntityCollectionNode source, IEnumerable<KeyPropertyValue> keyPropertyValues)
		{
			ExceptionUtils.CheckArgumentNotNull<EntityCollectionNode>(source, "source");
			this.source = source;
			this.navigationSource = source.NavigationSource;
			this.entityTypeReference = source.EntityItemType;
			this.keyPropertyValues = keyPropertyValues;
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00049BC5 File Offset: 0x00047DC5
		public EntityCollectionNode Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00049BCD File Offset: 0x00047DCD
		public IEnumerable<KeyPropertyValue> KeyPropertyValues
		{
			get
			{
				return this.keyPropertyValues;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00049BD5 File Offset: 0x00047DD5
		public override IEdmTypeReference TypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x00049BDD File Offset: 0x00047DDD
		public override IEdmEntityTypeReference EntityTypeReference
		{
			get
			{
				return this.entityTypeReference;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x00049BE5 File Offset: 0x00047DE5
		public override IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00049BED File Offset: 0x00047DED
		internal override InternalQueryNodeKind InternalKind
		{
			get
			{
				return InternalQueryNodeKind.KeyLookup;
			}
		}

		// Token: 0x040008A9 RID: 2217
		private readonly EntityCollectionNode source;

		// Token: 0x040008AA RID: 2218
		private readonly IEdmNavigationSource navigationSource;

		// Token: 0x040008AB RID: 2219
		private readonly IEdmEntityTypeReference entityTypeReference;

		// Token: 0x040008AC RID: 2220
		private readonly IEnumerable<KeyPropertyValue> keyPropertyValues;
	}
}
