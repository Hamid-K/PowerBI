using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000270 RID: 624
	internal sealed class ODataVerboseJsonCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x06001395 RID: 5013 RVA: 0x00049874 File Offset: 0x00047A74
		internal ODataVerboseJsonCollectionWriter(ODataVerboseJsonOutputContext verboseJsonOutputContext, IEdmTypeReference itemTypeReference)
			: base(verboseJsonOutputContext, itemTypeReference)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
			this.verboseJsonCollectionSerializer = new ODataVerboseJsonCollectionSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00049896 File Offset: 0x00047A96
		internal ODataVerboseJsonCollectionWriter(ODataVerboseJsonOutputContext verboseJsonOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(verboseJsonOutputContext, expectedItemType, listener)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
			this.verboseJsonCollectionSerializer = new ODataVerboseJsonCollectionSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000498B9 File Offset: 0x00047AB9
		protected override void VerifyNotDisposed()
		{
			this.verboseJsonOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000498C6 File Offset: 0x00047AC6
		protected override void FlushSynchronously()
		{
			this.verboseJsonOutputContext.Flush();
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000498D3 File Offset: 0x00047AD3
		protected override void StartPayload()
		{
			this.verboseJsonCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000498E0 File Offset: 0x00047AE0
		protected override void EndPayload()
		{
			this.verboseJsonCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000498ED File Offset: 0x00047AED
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.verboseJsonCollectionSerializer.WriteCollectionStart();
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x000498FA File Offset: 0x00047AFA
		protected override void EndCollection()
		{
			this.verboseJsonCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00049908 File Offset: 0x00047B08
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.verboseJsonOutputContext.MessageWriterSettings.WriterBehavior);
				this.verboseJsonOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = item as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.verboseJsonCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, base.DuplicatePropertyNamesChecker, base.CollectionValidator);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.verboseJsonCollectionSerializer.WritePrimitiveValue(item, base.CollectionValidator, expectedItemType);
		}

		// Token: 0x0400074C RID: 1868
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;

		// Token: 0x0400074D RID: 1869
		private readonly ODataVerboseJsonCollectionSerializer verboseJsonCollectionSerializer;
	}
}
