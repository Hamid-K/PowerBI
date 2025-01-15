using System;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x0200008B RID: 139
	internal interface IODataMetadataContext
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000589 RID: 1417
		IEdmModel Model { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600058A RID: 1418
		Uri ServiceBaseUri { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600058B RID: 1419
		Uri MetadataDocumentUri { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600058C RID: 1420
		ODataUri ODataUri { get; }

		// Token: 0x0600058D RID: 1421
		ODataEntityMetadataBuilder GetEntityMetadataBuilderForReader(IODataJsonLightReaderEntryState entryState, bool? useKeyAsSegment);

		// Token: 0x0600058E RID: 1422
		IEdmOperation[] GetBindableOperationsForType(IEdmType bindingType);

		// Token: 0x0600058F RID: 1423
		bool OperationsBoundToEntityTypeMustBeContainerQualified(IEdmEntityType entityType);
	}
}
