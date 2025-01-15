using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000200 RID: 512
	internal sealed class ODataJsonLightCollectionReader : ODataCollectionReaderCoreAsync
	{
		// Token: 0x060013CF RID: 5071 RVA: 0x00038B4F File Offset: 0x00036D4F
		internal ODataJsonLightCollectionReader(ODataJsonLightInputContext jsonLightInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, expectedItemTypeReference, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightCollectionDeserializer = new ODataJsonLightCollectionDeserializer(jsonLightInputContext);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x00038B70 File Offset: 0x00036D70
		protected override bool ReadAtStartImplementation()
		{
			PropertyAndAnnotationCollector propertyAndAnnotationCollector = this.jsonLightInputContext.CreatePropertyAndAnnotationCollector();
			this.jsonLightCollectionDeserializer.ReadPayloadStart(ODataPayloadKind.Collection, propertyAndAnnotationCollector, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(propertyAndAnnotationCollector);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00038BA4 File Offset: 0x00036DA4
		protected override bool ReadAtCollectionStartImplementation()
		{
			return this.ReadAtCollectionStartImplementationSynchronously();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00038BAC File Offset: 0x00036DAC
		protected override bool ReadAtValueImplementation()
		{
			return this.ReadAtValueImplementationSynchronously();
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00038BB4 File Offset: 0x00036DB4
		protected override bool ReadAtCollectionEndImplementation()
		{
			return this.ReadAtCollectionEndImplementationSynchronously();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00038BBC File Offset: 0x00036DBC
		private bool ReadAtStartImplementationSynchronously(PropertyAndAnnotationCollector propertyAndAnnotationCollector)
		{
			base.ExpectedItemTypeReference = ReaderValidationUtils.ValidateCollectionContextUriAndGetPayloadItemTypeReference(this.jsonLightCollectionDeserializer.ContextUriParseResult, base.ExpectedItemTypeReference);
			IEdmTypeReference edmTypeReference;
			ODataCollectionStart odataCollectionStart = this.jsonLightCollectionDeserializer.ReadCollectionStart(propertyAndAnnotationCollector, base.IsReadingNestedPayload, base.ExpectedItemTypeReference, out edmTypeReference);
			if (edmTypeReference != null)
			{
				base.ExpectedItemTypeReference = edmTypeReference;
			}
			this.jsonLightCollectionDeserializer.JsonReader.ReadStartArray();
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart);
			return true;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x00038C24 File Offset: 0x00036E24
		private bool ReadAtCollectionStartImplementationSynchronously()
		{
			if (this.jsonLightCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.jsonLightCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x00038C74 File Offset: 0x00036E74
		private bool ReadAtValueImplementationSynchronously()
		{
			if (this.jsonLightCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.jsonLightCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00038CCB File Offset: 0x00036ECB
		private bool ReadAtCollectionEndImplementationSynchronously()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.jsonLightCollectionDeserializer.ReadCollectionEnd(base.IsReadingNestedPayload);
			this.jsonLightCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x040009FC RID: 2556
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x040009FD RID: 2557
		private readonly ODataJsonLightCollectionDeserializer jsonLightCollectionDeserializer;
	}
}
