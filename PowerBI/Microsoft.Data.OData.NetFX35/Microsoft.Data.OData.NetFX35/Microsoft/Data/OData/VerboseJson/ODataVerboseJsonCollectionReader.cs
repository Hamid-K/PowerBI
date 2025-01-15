using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x0200023C RID: 572
	internal sealed class ODataVerboseJsonCollectionReader : ODataCollectionReaderCore
	{
		// Token: 0x06001167 RID: 4455 RVA: 0x00041EBC File Offset: 0x000400BC
		internal ODataVerboseJsonCollectionReader(ODataVerboseJsonInputContext verboseJsonInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(verboseJsonInputContext, expectedItemTypeReference, listener)
		{
			this.verboseJsonInputContext = verboseJsonInputContext;
			this.verboseJsonCollectionDeserializer = new ODataVerboseJsonCollectionDeserializer(verboseJsonInputContext);
			if (!verboseJsonInputContext.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonCollectionReader_ParsingWithoutMetadata);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00041EF2 File Offset: 0x000400F2
		private bool IsResultsWrapperExpected
		{
			get
			{
				return this.verboseJsonInputContext.Version >= ODataVersion.V2 && this.verboseJsonInputContext.ReadingResponse;
			}
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00041F10 File Offset: 0x00040110
		protected override bool ReadAtStartImplementation()
		{
			this.verboseJsonCollectionDeserializer.ReadPayloadStart(base.IsReadingNestedPayload);
			if (this.IsResultsWrapperExpected && this.verboseJsonCollectionDeserializer.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonCollectionReader_CannotReadWrappedCollectionStart(this.verboseJsonCollectionDeserializer.JsonReader.NodeType));
			}
			if (!this.IsResultsWrapperExpected && this.verboseJsonCollectionDeserializer.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonCollectionReader_CannotReadCollectionStart(this.verboseJsonCollectionDeserializer.JsonReader.NodeType));
			}
			ODataCollectionStart odataCollectionStart = this.verboseJsonCollectionDeserializer.ReadCollectionStart(this.IsResultsWrapperExpected);
			this.verboseJsonCollectionDeserializer.JsonReader.ReadStartArray();
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart);
			return true;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00041FD0 File Offset: 0x000401D0
		protected override bool ReadAtCollectionStartImplementation()
		{
			if (this.verboseJsonCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.verboseJsonCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00042020 File Offset: 0x00040220
		protected override bool ReadAtValueImplementation()
		{
			if (this.verboseJsonCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.verboseJsonCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00042077 File Offset: 0x00040277
		protected override bool ReadAtCollectionEndImplementation()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.verboseJsonCollectionDeserializer.ReadCollectionEnd(this.IsResultsWrapperExpected);
			this.verboseJsonCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x04000699 RID: 1689
		private readonly ODataVerboseJsonInputContext verboseJsonInputContext;

		// Token: 0x0400069A RID: 1690
		private readonly ODataVerboseJsonCollectionDeserializer verboseJsonCollectionDeserializer;
	}
}
