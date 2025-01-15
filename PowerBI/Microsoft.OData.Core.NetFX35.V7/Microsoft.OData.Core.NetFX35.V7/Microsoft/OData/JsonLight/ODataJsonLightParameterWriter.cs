using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000211 RID: 529
	internal sealed class ODataJsonLightParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x06001553 RID: 5459 RVA: 0x0003F558 File Offset: 0x0003D758
		internal ODataJsonLightParameterWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmOperation operation)
			: base(jsonLightOutputContext, operation)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0003F57B File Offset: 0x0003D77B
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0003F588 File Offset: 0x0003D788
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0003F595 File Offset: 0x0003D795
		protected override void StartPayload()
		{
			this.jsonLightValueSerializer.WritePayloadStart();
			this.jsonLightOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0003F5B2 File Offset: 0x0003D7B2
		protected override void EndPayload()
		{
			this.jsonLightOutputContext.JsonWriter.EndObjectScope();
			this.jsonLightValueSerializer.WritePayloadEnd();
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0003F5D0 File Offset: 0x0003D7D0
		protected override void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			if (parameterValue == null)
			{
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataEnumValue odataEnumValue = parameterValue as ODataEnumValue;
			if (odataEnumValue != null)
			{
				this.jsonLightValueSerializer.WriteEnumValue(odataEnumValue, expectedTypeReference);
				return;
			}
			this.jsonLightValueSerializer.WritePrimitiveValue(parameterValue, expectedTypeReference);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x0003F628 File Offset: 0x0003D828
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightCollectionWriter(this.jsonLightOutputContext, expectedItemType, this);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0003F648 File Offset: 0x0003D848
		protected override ODataWriter CreateFormatResourceWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightWriter(this.jsonLightOutputContext, null, null, false, true, false, this);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0003F66C File Offset: 0x0003D86C
		protected override ODataWriter CreateFormatResourceSetWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightWriter(this.jsonLightOutputContext, null, null, true, true, false, this);
		}

		// Token: 0x04000A2E RID: 2606
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000A2F RID: 2607
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
