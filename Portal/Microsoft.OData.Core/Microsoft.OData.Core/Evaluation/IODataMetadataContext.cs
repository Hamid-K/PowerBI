using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000265 RID: 613
	internal interface IODataMetadataContext
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001BBF RID: 7103
		IEdmModel Model { get; }

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001BC0 RID: 7104
		Uri ServiceBaseUri { get; }

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001BC1 RID: 7105
		Uri MetadataDocumentUri { get; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001BC2 RID: 7106
		ODataUri ODataUri { get; }

		// Token: 0x06001BC3 RID: 7107
		ODataResourceMetadataBuilder GetResourceMetadataBuilderForReader(IODataJsonLightReaderResourceState resourceState, bool useKeyAsSegment, bool isDelta);

		// Token: 0x06001BC4 RID: 7108
		IEnumerable<IEdmOperation> GetBindableOperationsForType(IEdmType bindingType);

		// Token: 0x06001BC5 RID: 7109
		bool OperationsBoundToStructuredTypeMustBeContainerQualified(IEdmStructuredType structuredType);
	}
}
