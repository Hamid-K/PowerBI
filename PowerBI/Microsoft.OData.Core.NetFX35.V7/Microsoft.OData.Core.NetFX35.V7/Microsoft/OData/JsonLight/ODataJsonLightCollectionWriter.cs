using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000202 RID: 514
	internal sealed class ODataJsonLightCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x00038E15 File Offset: 0x00037015
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference itemTypeReference)
			: base(jsonLightOutputContext, itemTypeReference)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, true);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00038E38 File Offset: 0x00037038
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(jsonLightOutputContext, expectedItemType, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00038E5C File Offset: 0x0003705C
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00038E69 File Offset: 0x00037069
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00038E76 File Offset: 0x00037076
		protected override void StartPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00038E83 File Offset: 0x00037083
		protected override void EndPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00038E90 File Offset: 0x00037090
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.jsonLightCollectionSerializer.WriteCollectionStart(collectionStart, base.ItemTypeReference);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00038EA4 File Offset: 0x000370A4
		protected override void EndCollection()
		{
			this.jsonLightCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00038EB4 File Offset: 0x000370B4
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			if (item == null)
			{
				this.jsonLightOutputContext.WriterValidator.ValidateNullCollectionItem(expectedItemType);
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataEnumValue odataEnumValue = item as ODataEnumValue;
			if (odataEnumValue == null)
			{
				this.jsonLightCollectionSerializer.WritePrimitiveValue(item, expectedItemType);
				return;
			}
			if (odataEnumValue.Value == null)
			{
				this.jsonLightCollectionSerializer.WriteNullValue();
				return;
			}
			this.jsonLightCollectionSerializer.WritePrimitiveValue(odataEnumValue.Value, EdmCoreModel.Instance.GetString(true));
		}

		// Token: 0x040009FF RID: 2559
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000A00 RID: 2560
		private readonly ODataJsonLightCollectionSerializer jsonLightCollectionSerializer;
	}
}
