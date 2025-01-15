using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200017C RID: 380
	internal sealed class ODataTypeAnnotation
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x00023165 File Offset: 0x00021365
		public ODataTypeAnnotation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntitySet>(entitySet, "entitySet");
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.entitySet = entitySet;
			this.type = entityType.ToTypeReference(true);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00023197 File Offset: 0x00021397
		public ODataTypeAnnotation(IEdmComplexTypeReference complexType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmComplexTypeReference>(complexType, "complexType");
			this.type = complexType;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000231B1 File Offset: 0x000213B1
		public ODataTypeAnnotation(IEdmCollectionTypeReference collectionType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(collectionType, "collectionType");
			this.type = collectionType;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x000231CB File Offset: 0x000213CB
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x000231D3 File Offset: 0x000213D3
		public IEdmEntitySet EntitySet
		{
			get
			{
				return this.entitySet;
			}
		}

		// Token: 0x040003FB RID: 1019
		private readonly IEdmTypeReference type;

		// Token: 0x040003FC RID: 1020
		private readonly IEdmEntitySet entitySet;
	}
}
