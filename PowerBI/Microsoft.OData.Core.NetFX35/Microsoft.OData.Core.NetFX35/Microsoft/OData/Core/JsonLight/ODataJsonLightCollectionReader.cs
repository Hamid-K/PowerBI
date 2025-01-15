using System;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000BC RID: 188
	internal sealed class ODataJsonLightCollectionReader : ODataCollectionReaderCoreAsync
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x00018201 File Offset: 0x00016401
		internal ODataJsonLightCollectionReader(ODataJsonLightInputContext jsonLightInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, expectedItemTypeReference, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightCollectionDeserializer = new ODataJsonLightCollectionDeserializer(jsonLightInputContext);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00018220 File Offset: 0x00016420
		protected override bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			this.jsonLightCollectionDeserializer.ReadPayloadStart(ODataPayloadKind.Collection, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018254 File Offset: 0x00016454
		protected override bool ReadAtCollectionStartImplementation()
		{
			return this.ReadAtCollectionStartImplementationSynchronously();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001825C File Offset: 0x0001645C
		protected override bool ReadAtValueImplementation()
		{
			return this.ReadAtValueImplementationSynchronously();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00018264 File Offset: 0x00016464
		protected override bool ReadAtCollectionEndImplementation()
		{
			return this.ReadAtCollectionEndImplementationSynchronously();
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001826C File Offset: 0x0001646C
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			base.ExpectedItemTypeReference = ReaderValidationUtils.ValidateCollectionContextUriAndGetPayloadItemTypeReference(this.jsonLightCollectionDeserializer.ContextUriParseResult, base.ExpectedItemTypeReference);
			IEdmTypeReference edmTypeReference;
			ODataCollectionStart odataCollectionStart = this.jsonLightCollectionDeserializer.ReadCollectionStart(duplicatePropertyNamesChecker, base.IsReadingNestedPayload, base.ExpectedItemTypeReference, out edmTypeReference);
			if (edmTypeReference != null)
			{
				base.ExpectedItemTypeReference = edmTypeReference;
			}
			this.jsonLightCollectionDeserializer.JsonReader.ReadStartArray();
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart);
			return true;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000182D4 File Offset: 0x000164D4
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

		// Token: 0x060006C5 RID: 1733 RVA: 0x00018324 File Offset: 0x00016524
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

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001837B File Offset: 0x0001657B
		private bool ReadAtCollectionEndImplementationSynchronously()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.jsonLightCollectionDeserializer.ReadCollectionEnd(base.IsReadingNestedPayload);
			this.jsonLightCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x04000323 RID: 803
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000324 RID: 804
		private readonly ODataJsonLightCollectionDeserializer jsonLightCollectionDeserializer;
	}
}
