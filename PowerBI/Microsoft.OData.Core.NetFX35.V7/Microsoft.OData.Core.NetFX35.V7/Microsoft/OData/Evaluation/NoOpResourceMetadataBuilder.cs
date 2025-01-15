using System;
using System.Collections.Generic;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000225 RID: 549
	internal sealed class NoOpResourceMetadataBuilder : ODataResourceMetadataBuilder
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x00044A2D File Offset: 0x00042C2D
		internal NoOpResourceMetadataBuilder(ODataResource resource)
		{
			this.resource = resource;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00044A3C File Offset: 0x00042C3C
		internal override Uri GetEditLink()
		{
			return this.resource.NonComputedEditLink;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00044A49 File Offset: 0x00042C49
		internal override Uri GetReadLink()
		{
			return this.resource.NonComputedReadLink;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00044A56 File Offset: 0x00042C56
		internal override Uri GetId()
		{
			if (!this.resource.IsTransient)
			{
				return this.resource.NonComputedId;
			}
			return null;
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00044A72 File Offset: 0x00042C72
		internal override string GetETag()
		{
			return this.resource.NonComputedETag;
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00044A7F File Offset: 0x00042C7F
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			return this.resource.NonComputedMediaResource;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00044A8C File Offset: 0x00042C8C
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.resource.NonComputedActions;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00044A99 File Offset: 0x00042C99
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.resource.NonComputedFunctions;
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00044AA6 File Offset: 0x00042CA6
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			return navigationLinkUrl;
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00044AA6 File Offset: 0x00042CA6
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			return associationLinkUrl;
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00044AA9 File Offset: 0x00042CA9
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			if (this.resource.IsTransient)
			{
				id = null;
				return true;
			}
			id = this.GetId();
			return id != null;
		}

		// Token: 0x04000A50 RID: 2640
		private readonly ODataResource resource;
	}
}
