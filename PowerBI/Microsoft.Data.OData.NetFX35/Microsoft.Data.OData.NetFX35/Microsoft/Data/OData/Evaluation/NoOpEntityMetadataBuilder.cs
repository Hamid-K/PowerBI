using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000104 RID: 260
	internal sealed class NoOpEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x00017D0B File Offset: 0x00015F0B
		internal NoOpEntityMetadataBuilder(ODataEntry entry)
		{
			this.entry = entry;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00017D1A File Offset: 0x00015F1A
		internal override Uri GetEditLink()
		{
			return this.entry.NonComputedEditLink;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00017D27 File Offset: 0x00015F27
		internal override Uri GetReadLink()
		{
			return this.entry.NonComputedReadLink;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00017D34 File Offset: 0x00015F34
		internal override string GetId()
		{
			return this.entry.NonComputedId;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00017D41 File Offset: 0x00015F41
		internal override string GetETag()
		{
			return this.entry.NonComputedETag;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00017D4E File Offset: 0x00015F4E
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			return this.entry.NonComputedMediaResource;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00017D5B File Offset: 0x00015F5B
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00017D5E File Offset: 0x00015F5E
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.entry.NonComputedActions;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00017D6B File Offset: 0x00015F6B
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.entry.NonComputedFunctions;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00017D78 File Offset: 0x00015F78
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			return navigationLinkUrl;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00017D7B File Offset: 0x00015F7B
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			return associationLinkUrl;
		}

		// Token: 0x040002A6 RID: 678
		private readonly ODataEntry entry;
	}
}
