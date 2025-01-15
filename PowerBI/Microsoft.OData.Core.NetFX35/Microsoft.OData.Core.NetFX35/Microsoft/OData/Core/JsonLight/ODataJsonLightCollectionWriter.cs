using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000C0 RID: 192
	internal sealed class ODataJsonLightCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x060006E5 RID: 1765 RVA: 0x00018C30 File Offset: 0x00016E30
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference itemTypeReference)
			: base(jsonLightOutputContext, itemTypeReference)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, true);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00018C53 File Offset: 0x00016E53
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(jsonLightOutputContext, expectedItemType, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00018C77 File Offset: 0x00016E77
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00018C84 File Offset: 0x00016E84
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00018C91 File Offset: 0x00016E91
		protected override void StartPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00018C9E File Offset: 0x00016E9E
		protected override void EndPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00018CAB File Offset: 0x00016EAB
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.jsonLightCollectionSerializer.WriteCollectionStart(collectionStart, base.ItemTypeReference);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00018CBF File Offset: 0x00016EBF
		protected override void EndCollection()
		{
			this.jsonLightCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00018CCC File Offset: 0x00016ECC
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
			ODataEnumValue odataEnumValue;
			if ((odataEnumValue = item as ODataEnumValue) == null)
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

		// Token: 0x0400032D RID: 813
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x0400032E RID: 814
		private readonly ODataJsonLightCollectionSerializer jsonLightCollectionSerializer;
	}
}
