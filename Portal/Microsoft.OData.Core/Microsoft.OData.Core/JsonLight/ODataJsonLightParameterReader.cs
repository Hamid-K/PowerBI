using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000249 RID: 585
	internal sealed class ODataJsonLightParameterReader : ODataParameterReaderCoreAsync
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x0004C8C9 File Offset: 0x0004AAC9
		internal ODataJsonLightParameterReader(ODataJsonLightInputContext jsonLightInputContext, IEdmOperation operation)
			: base(jsonLightInputContext, operation)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightParameterDeserializer = new ODataJsonLightParameterDeserializer(this, jsonLightInputContext);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0004C8E7 File Offset: 0x0004AAE7
		protected override bool ReadAtStartImplementation()
		{
			this.propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			this.jsonLightParameterDeserializer.ReadPayloadStart(ODataPayloadKind.Parameter, this.propertyAndAnnotationCollector, false, true);
			return this.ReadAtStartImplementationSynchronously();
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0004C915 File Offset: 0x0004AB15
		protected override Task<bool> ReadAtStartImplementationAsync()
		{
			this.propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			return this.jsonLightParameterDeserializer.ReadPayloadStartAsync(ODataPayloadKind.Parameter, this.propertyAndAnnotationCollector, false, true).FollowOnSuccessWith((Task t) => this.ReadAtStartImplementationSynchronously());
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0004C94E File Offset: 0x0004AB4E
		protected override bool ReadNextParameterImplementation()
		{
			return this.ReadNextParameterImplementationSynchronously();
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0004C956 File Offset: 0x0004AB56
		protected override Task<bool> ReadNextParameterImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadNextParameterImplementationSynchronously));
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0004C969 File Offset: 0x0004AB69
		protected override ODataReader CreateResourceReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceReaderSynchronously(expectedResourceType);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0004C974 File Offset: 0x0004AB74
		protected override Task<ODataReader> CreateResourceReaderAsync(IEdmStructuredType expectedResourceType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateResourceReaderSynchronously(expectedResourceType));
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0004C9A6 File Offset: 0x0004ABA6
		protected override ODataReader CreateResourceSetReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceSetReaderSynchronously(expectedResourceType);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0004C9B0 File Offset: 0x0004ABB0
		protected override Task<ODataReader> CreateResourceSetReaderAsync(IEdmStructuredType expectedResourceType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateResourceSetReaderSynchronously(expectedResourceType));
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0004C9E2 File Offset: 0x0004ABE2
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderSynchronously(expectedItemTypeReference);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0004C9EC File Offset: 0x0004ABEC
		protected override Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionReader>(() => this.CreateCollectionReaderSynchronously(expectedItemTypeReference));
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0004CA1E File Offset: 0x0004AC1E
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

		// Token: 0x060019F0 RID: 6640 RVA: 0x0004CA56 File Offset: 0x0004AC56
		private bool ReadNextParameterImplementationSynchronously()
		{
			base.PopScope(this.State);
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.propertyAndAnnotationCollector);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0004CA75 File Offset: 0x0004AC75
		private ODataReader CreateResourceReaderSynchronously(IEdmStructuredType expectedResourceType)
		{
			return new ODataJsonLightReader(this.jsonLightInputContext, null, expectedResourceType, false, true, false, this);
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0004CA88 File Offset: 0x0004AC88
		private ODataReader CreateResourceSetReaderSynchronously(IEdmStructuredType expectedResourceType)
		{
			return new ODataJsonLightReader(this.jsonLightInputContext, null, expectedResourceType, true, true, false, this);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0004CA9B File Offset: 0x0004AC9B
		private ODataCollectionReader CreateCollectionReaderSynchronously(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this.jsonLightInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x04000B48 RID: 2888
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000B49 RID: 2889
		private readonly ODataJsonLightParameterDeserializer jsonLightParameterDeserializer;

		// Token: 0x04000B4A RID: 2890
		private PropertyAndAnnotationCollector propertyAndAnnotationCollector;
	}
}
