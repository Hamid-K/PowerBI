using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200023B RID: 571
	internal sealed class ODataJsonLightCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x00046C9D File Offset: 0x00044E9D
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference itemTypeReference)
			: base(jsonLightOutputContext, itemTypeReference)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, true);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00046CC0 File Offset: 0x00044EC0
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(jsonLightOutputContext, expectedItemType, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00046CE4 File Offset: 0x00044EE4
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00046CF1 File Offset: 0x00044EF1
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00046CFE File Offset: 0x00044EFE
		protected override Task FlushAsynchronously()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00046D0B File Offset: 0x00044F0B
		protected override void StartPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00046D18 File Offset: 0x00044F18
		protected override void EndPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00046D25 File Offset: 0x00044F25
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.jsonLightCollectionSerializer.WriteCollectionStart(collectionStart, base.ItemTypeReference);
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00046D39 File Offset: 0x00044F39
		protected override void EndCollection()
		{
			this.jsonLightCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00046D48 File Offset: 0x00044F48
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			if (item == null)
			{
				this.jsonLightOutputContext.WriterValidator.ValidateNullCollectionItem(expectedItemType);
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataResourceValue odataResourceValue = item as ODataResourceValue;
			if (odataResourceValue != null)
			{
				this.jsonLightCollectionSerializer.WriteResourceValue(odataResourceValue, expectedItemType, false, base.DuplicatePropertyNameChecker);
				base.DuplicatePropertyNameChecker.Reset();
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

		// Token: 0x04000B1D RID: 2845
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000B1E RID: 2846
		private readonly ODataJsonLightCollectionSerializer jsonLightCollectionSerializer;
	}
}
