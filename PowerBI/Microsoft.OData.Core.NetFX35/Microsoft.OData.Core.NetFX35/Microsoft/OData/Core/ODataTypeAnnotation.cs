using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x020001A0 RID: 416
	internal sealed class ODataTypeAnnotation
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x00035AF8 File Offset: 0x00033CF8
		public ODataTypeAnnotation(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmEntityType>(entityType, "entityType");
			this.navigationSource = navigationSource;
			this.type = entityType.ToTypeReference(true);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00035B1F File Offset: 0x00033D1F
		public ODataTypeAnnotation(IEdmComplexTypeReference complexType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmComplexTypeReference>(complexType, "complexType");
			this.type = complexType;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00035B39 File Offset: 0x00033D39
		public ODataTypeAnnotation(IEdmCollectionTypeReference collectionType)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmCollectionTypeReference>(collectionType, "collectionType");
			this.type = collectionType;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00035B53 File Offset: 0x00033D53
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00035B5B File Offset: 0x00033D5B
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x040006D4 RID: 1748
		private readonly IEdmTypeReference type;

		// Token: 0x040006D5 RID: 1749
		private readonly IEdmNavigationSource navigationSource;
	}
}
