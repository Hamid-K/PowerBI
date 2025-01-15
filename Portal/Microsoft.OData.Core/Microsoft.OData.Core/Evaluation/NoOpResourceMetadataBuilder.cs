using System;
using System.Collections.Generic;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000260 RID: 608
	internal sealed class NoOpResourceMetadataBuilder : ODataResourceMetadataBuilder
	{
		// Token: 0x06001B5C RID: 7004 RVA: 0x00054601 File Offset: 0x00052801
		internal NoOpResourceMetadataBuilder(ODataResourceBase resource)
		{
			this.resource = resource;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x00054610 File Offset: 0x00052810
		internal override Uri GetEditLink()
		{
			return this.resource.NonComputedEditLink;
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0005461D File Offset: 0x0005281D
		internal override Uri GetReadLink()
		{
			return this.resource.NonComputedReadLink;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0005462A File Offset: 0x0005282A
		internal override Uri GetId()
		{
			if (!this.resource.IsTransient)
			{
				return this.resource.NonComputedId;
			}
			return null;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00054646 File Offset: 0x00052846
		internal override string GetETag()
		{
			return this.resource.NonComputedETag;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00054653 File Offset: 0x00052853
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			return this.resource.NonComputedMediaResource;
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x00011BDC File Offset: 0x0000FDDC
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x00054660 File Offset: 0x00052860
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.resource.NonComputedActions;
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x0005466D File Offset: 0x0005286D
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.resource.NonComputedFunctions;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00053DE2 File Offset: 0x00051FE2
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			return navigationLinkUrl;
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00053DE2 File Offset: 0x00051FE2
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			return associationLinkUrl;
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x0005467A File Offset: 0x0005287A
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

		// Token: 0x04000B7B RID: 2939
		private readonly ODataResourceBase resource;
	}
}
