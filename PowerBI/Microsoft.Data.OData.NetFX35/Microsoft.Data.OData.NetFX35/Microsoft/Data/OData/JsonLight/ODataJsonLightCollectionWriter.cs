using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018A RID: 394
	internal sealed class ODataJsonLightCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x0002440A File Offset: 0x0002260A
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference itemTypeReference)
			: base(jsonLightOutputContext, itemTypeReference)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, true);
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0002442D File Offset: 0x0002262D
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(jsonLightOutputContext, expectedItemType, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00024451 File Offset: 0x00022651
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002445E File Offset: 0x0002265E
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0002446B File Offset: 0x0002266B
		protected override void StartPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00024478 File Offset: 0x00022678
		protected override void EndPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00024485 File Offset: 0x00022685
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.jsonLightCollectionSerializer.WriteCollectionStart(collectionStart, base.ItemTypeReference);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00024499 File Offset: 0x00022699
		protected override void EndCollection()
		{
			this.jsonLightCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000244A8 File Offset: 0x000226A8
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.jsonLightOutputContext.MessageWriterSettings.WriterBehavior);
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = item as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.jsonLightCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, false, base.DuplicatePropertyNamesChecker);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.jsonLightCollectionSerializer.WritePrimitiveValue(item, expectedItemType);
		}

		// Token: 0x04000414 RID: 1044
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000415 RID: 1045
		private readonly ODataJsonLightCollectionSerializer jsonLightCollectionSerializer;
	}
}
