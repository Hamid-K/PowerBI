using System;
using System.Data.Services.Common;
using System.Diagnostics;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000264 RID: 612
	[DebuggerDisplay("EntityPropertyMappingInfo {DefiningType}")]
	internal sealed class EntityPropertyMappingInfo
	{
		// Token: 0x06001320 RID: 4896 RVA: 0x00047B2F File Offset: 0x00045D2F
		internal EntityPropertyMappingInfo(EntityPropertyMappingAttribute attribute, IEdmEntityType definingType, IEdmEntityType actualTypeDeclaringProperty)
		{
			this.attribute = attribute;
			this.definingType = definingType;
			this.actualPropertyType = actualTypeDeclaringProperty;
			this.isSyndicationMapping = this.attribute.TargetSyndicationItem != SyndicationItemProperty.CustomProperty;
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x00047B63 File Offset: 0x00045D63
		internal EntityPropertyMappingAttribute Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x00047B6B File Offset: 0x00045D6B
		internal IEdmEntityType DefiningType
		{
			get
			{
				return this.definingType;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x00047B73 File Offset: 0x00045D73
		internal IEdmEntityType ActualPropertyType
		{
			get
			{
				return this.actualPropertyType;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00047B7B File Offset: 0x00045D7B
		internal EpmSourcePathSegment[] PropertyValuePath
		{
			get
			{
				return this.propertyValuePath;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x00047B83 File Offset: 0x00045D83
		internal bool IsSyndicationMapping
		{
			get
			{
				return this.isSyndicationMapping;
			}
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00047B8B File Offset: 0x00045D8B
		internal void SetPropertyValuePath(EpmSourcePathSegment[] path)
		{
			this.propertyValuePath = path;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00047B94 File Offset: 0x00045D94
		internal bool DefiningTypesAreEqual(EntityPropertyMappingInfo other)
		{
			return this.DefiningType.IsEquivalentTo(other.DefiningType);
		}

		// Token: 0x04000721 RID: 1825
		private readonly EntityPropertyMappingAttribute attribute;

		// Token: 0x04000722 RID: 1826
		private readonly IEdmEntityType definingType;

		// Token: 0x04000723 RID: 1827
		private readonly IEdmEntityType actualPropertyType;

		// Token: 0x04000724 RID: 1828
		private EpmSourcePathSegment[] propertyValuePath;

		// Token: 0x04000725 RID: 1829
		private bool isSyndicationMapping;
	}
}
