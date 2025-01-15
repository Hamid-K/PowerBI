using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000080 RID: 128
	internal sealed class NoOpEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x000127F6 File Offset: 0x000109F6
		internal NoOpEntityMetadataBuilder(ODataEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00012805 File Offset: 0x00010A05
		internal override Uri GetEditLink()
		{
			return this.entry.NonComputedEditLink;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00012812 File Offset: 0x00010A12
		internal override Uri GetReadLink()
		{
			return this.entry.NonComputedReadLink;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0001281F File Offset: 0x00010A1F
		internal override Uri GetId()
		{
			if (!this.entry.IsTransient)
			{
				return this.entry.NonComputedId;
			}
			return null;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001283B File Offset: 0x00010A3B
		internal override string GetETag()
		{
			return this.entry.NonComputedETag;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00012848 File Offset: 0x00010A48
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			return this.entry.NonComputedMediaResource;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00012855 File Offset: 0x00010A55
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00012858 File Offset: 0x00010A58
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.entry.NonComputedActions;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00012865 File Offset: 0x00010A65
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.entry.NonComputedFunctions;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00012872 File Offset: 0x00010A72
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			return navigationLinkUrl;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00012875 File Offset: 0x00010A75
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			return associationLinkUrl;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00012878 File Offset: 0x00010A78
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			if (this.entry.IsTransient)
			{
				id = null;
				return true;
			}
			id = this.GetId();
			return !(id == null);
		}

		// Token: 0x0400022A RID: 554
		private readonly ODataEntry entry;
	}
}
