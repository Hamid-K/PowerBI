using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000210 RID: 528
	internal sealed class ODataJsonLightParameterReader : ODataParameterReaderCoreAsync
	{
		// Token: 0x06001548 RID: 5448 RVA: 0x0003F45D File Offset: 0x0003D65D
		internal ODataJsonLightParameterReader(ODataJsonLightInputContext jsonLightInputContext, IEdmOperation operation)
			: base(jsonLightInputContext, operation)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightParameterDeserializer = new ODataJsonLightParameterDeserializer(this, jsonLightInputContext);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0003F47B File Offset: 0x0003D67B
		protected override bool ReadAtStartImplementation()
		{
			this.propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			this.jsonLightParameterDeserializer.ReadPayloadStart(ODataPayloadKind.Parameter, this.propertyAndAnnotationCollector, false, true);
			return this.ReadAtStartImplementationSynchronously();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0003F4A9 File Offset: 0x0003D6A9
		protected override bool ReadNextParameterImplementation()
		{
			return this.ReadNextParameterImplementationSynchronously();
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0003F4B1 File Offset: 0x0003D6B1
		protected override ODataReader CreateResourceReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceReaderSynchronously(expectedResourceType);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0003F4BA File Offset: 0x0003D6BA
		protected override ODataReader CreateResourceSetReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceSetReaderSynchronously(expectedResourceType);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0003F4C3 File Offset: 0x0003D6C3
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderSynchronously(expectedItemTypeReference);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0003F4CC File Offset: 0x0003D6CC
		private bool ReadAtStartImplementationSynchronously()
		{
			if (this.jsonLightInputContext.JsonReader.NodeType == JsonNodeType.EndOfInput)
			{
				base.PopScope(ODataParameterReaderState.Start);
				base.EnterScope(ODataParameterReaderState.Completed, null, null);
				return false;
			}
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.propertyAndAnnotationCollector);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0003F504 File Offset: 0x0003D704
		private bool ReadNextParameterImplementationSynchronously()
		{
			base.PopScope(this.State);
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.propertyAndAnnotationCollector);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0003F523 File Offset: 0x0003D723
		private ODataReader CreateResourceReaderSynchronously(IEdmStructuredType expectedResourceType)
		{
			return new ODataJsonLightReader(this.jsonLightInputContext, null, expectedResourceType, false, true, false, this);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0003F536 File Offset: 0x0003D736
		private ODataReader CreateResourceSetReaderSynchronously(IEdmStructuredType expectedResourceType)
		{
			return new ODataJsonLightReader(this.jsonLightInputContext, null, expectedResourceType, true, true, false, this);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0003F549 File Offset: 0x0003D749
		private ODataCollectionReader CreateCollectionReaderSynchronously(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this.jsonLightInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x04000A2B RID: 2603
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000A2C RID: 2604
		private readonly ODataJsonLightParameterDeserializer jsonLightParameterDeserializer;

		// Token: 0x04000A2D RID: 2605
		private PropertyAndAnnotationCollector propertyAndAnnotationCollector;
	}
}
