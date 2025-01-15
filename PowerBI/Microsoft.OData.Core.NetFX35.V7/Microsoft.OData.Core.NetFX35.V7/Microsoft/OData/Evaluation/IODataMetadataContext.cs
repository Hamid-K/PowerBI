using System;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200022A RID: 554
	internal interface IODataMetadataContext
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001695 RID: 5781
		IEdmModel Model { get; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001696 RID: 5782
		Uri ServiceBaseUri { get; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001697 RID: 5783
		Uri MetadataDocumentUri { get; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001698 RID: 5784
		ODataUri ODataUri { get; }

		// Token: 0x06001699 RID: 5785
		ODataResourceMetadataBuilder GetResourceMetadataBuilderForReader(IODataJsonLightReaderResourceState resourceState, bool useKeyAsSegment);

		// Token: 0x0600169A RID: 5786
		IEdmOperation[] GetBindableOperationsForType(IEdmType bindingType);

		// Token: 0x0600169B RID: 5787
		bool OperationsBoundToStructuredTypeMustBeContainerQualified(IEdmStructuredType structuredType);
	}
}
