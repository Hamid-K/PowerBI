using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000108 RID: 264
	internal interface IODataMetadataContext
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000707 RID: 1799
		IEdmModel Model { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000708 RID: 1800
		Uri ServiceBaseUri { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000709 RID: 1801
		Uri MetadataDocumentUri { get; }

		// Token: 0x0600070A RID: 1802
		ODataEntityMetadataBuilder GetEntityMetadataBuilderForReader(IODataJsonLightReaderEntryState entryState);

		// Token: 0x0600070B RID: 1803
		IEdmFunctionImport[] GetAlwaysBindableOperationsForType(IEdmType bindingType);

		// Token: 0x0600070C RID: 1804
		bool OperationsBoundToEntityTypeMustBeContainerQualified(IEdmEntityType entityType);
	}
}
