using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000183 RID: 387
	internal sealed class ODataJsonLightCollectionReader : ODataCollectionReaderCoreAsync
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00023AEB File Offset: 0x00021CEB
		internal ODataJsonLightCollectionReader(ODataJsonLightInputContext jsonLightInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, expectedItemTypeReference, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightCollectionDeserializer = new ODataJsonLightCollectionDeserializer(jsonLightInputContext);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00023B0C File Offset: 0x00021D0C
		protected override bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			this.jsonLightCollectionDeserializer.ReadPayloadStart(ODataPayloadKind.Collection, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00023B40 File Offset: 0x00021D40
		protected override bool ReadAtCollectionStartImplementation()
		{
			return this.ReadAtCollectionStartImplementationSynchronously();
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00023B48 File Offset: 0x00021D48
		protected override bool ReadAtValueImplementation()
		{
			return this.ReadAtValueImplementationSynchronously();
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00023B50 File Offset: 0x00021D50
		protected override bool ReadAtCollectionEndImplementation()
		{
			return this.ReadAtCollectionEndImplementationSynchronously();
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00023B58 File Offset: 0x00021D58
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			base.ExpectedItemTypeReference = ReaderValidationUtils.ValidateCollectionMetadataUriAndGetPayloadItemTypeReference(this.jsonLightCollectionDeserializer.MetadataUriParseResult, base.ExpectedItemTypeReference);
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

		// Token: 0x06000A9B RID: 2715 RVA: 0x00023BC0 File Offset: 0x00021DC0
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

		// Token: 0x06000A9C RID: 2716 RVA: 0x00023C10 File Offset: 0x00021E10
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

		// Token: 0x06000A9D RID: 2717 RVA: 0x00023C67 File Offset: 0x00021E67
		private bool ReadAtCollectionEndImplementationSynchronously()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.jsonLightCollectionDeserializer.ReadCollectionEnd(base.IsReadingNestedPayload);
			this.jsonLightCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x04000402 RID: 1026
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000403 RID: 1027
		private readonly ODataJsonLightCollectionDeserializer jsonLightCollectionDeserializer;
	}
}
