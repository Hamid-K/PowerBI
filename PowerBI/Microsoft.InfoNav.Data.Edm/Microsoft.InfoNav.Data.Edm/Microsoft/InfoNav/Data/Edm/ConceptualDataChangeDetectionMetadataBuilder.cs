using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200001C RID: 28
	internal static class ConceptualDataChangeDetectionMetadataBuilder
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004786 File Offset: 0x00002986
		internal static ConceptualDataChangeDetectionMetadata BuildChangeDetectionMetadata(ChangeDetectionMetadata changeDetectionMetadata)
		{
			if (changeDetectionMetadata == null)
			{
				return null;
			}
			return new ConceptualDataChangeDetectionMetadata(changeDetectionMetadata.RefreshInterval);
		}

		// Token: 0x0400014A RID: 330
		internal const string ChangeDetectionNameKey = "ChangeDetectionMetadata";
	}
}
