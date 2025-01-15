using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;
using Microsoft.InfoNav.DataShapeQueryGeneration.DSQ;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000043 RID: 67
	internal sealed class DataShapeGenerationResult
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000A5D3 File Offset: 0x000087D3
		internal DataShapeGenerationResult(DataShape dataShape, QueryBindingDescriptor bindingDescriptor, DataShapeGenerationErrorContext errorContext, IFederatedConceptualSchema federatedConceptualSchema, IntermediateDataShapeTableSchema internalSchema, IntermediateDataShapeReferenceSchema testOnlyInternalDsqReferenceSchema)
		{
			this.DataShape = dataShape;
			this.BindingDescriptor = bindingDescriptor;
			this.ErrorContext = errorContext;
			this.FederatedConceptualSchema = federatedConceptualSchema;
			this.InternalSchema = internalSchema;
			this.TestOnlyInternalDsqReferenceSchema = testOnlyInternalDsqReferenceSchema;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A608 File Offset: 0x00008808
		internal DataShape DataShape { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A610 File Offset: 0x00008810
		internal QueryBindingDescriptor BindingDescriptor { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A618 File Offset: 0x00008818
		internal DataShapeGenerationErrorContext ErrorContext { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A620 File Offset: 0x00008820
		internal IFederatedConceptualSchema FederatedConceptualSchema { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A628 File Offset: 0x00008828
		internal IntermediateDataShapeTableSchema InternalSchema { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A630 File Offset: 0x00008830
		internal IntermediateDataShapeReferenceSchema TestOnlyInternalDsqReferenceSchema { get; }
	}
}
