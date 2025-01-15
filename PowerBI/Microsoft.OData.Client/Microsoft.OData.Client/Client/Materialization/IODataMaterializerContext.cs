using System;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000102 RID: 258
	internal interface IODataMaterializerContext
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000AE0 RID: 2784
		DataServiceContext Context { get; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000AE1 RID: 2785
		UndeclaredPropertyBehavior UndeclaredPropertyBehavior { get; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000AE2 RID: 2786
		ClientEdmModel Model { get; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000AE3 RID: 2787
		DataServiceClientResponsePipelineConfiguration ResponsePipeline { get; }

		// Token: 0x06000AE4 RID: 2788
		ClientTypeAnnotation ResolveTypeForMaterialization(Type expectedType, string readerTypeName);

		// Token: 0x06000AE5 RID: 2789
		IEdmType ResolveExpectedTypeForReading(Type clientClrType);
	}
}
