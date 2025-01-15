using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000F0 RID: 240
	internal sealed class ODataJsonLightParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x000216AC File Offset: 0x0001F8AC
		internal ODataJsonLightParameterWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmOperation operation)
			: base(jsonLightOutputContext, operation)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000216CF File Offset: 0x0001F8CF
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000216DC File Offset: 0x0001F8DC
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000216E9 File Offset: 0x0001F8E9
		protected override void StartPayload()
		{
			this.jsonLightValueSerializer.WritePayloadStart();
			this.jsonLightOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00021706 File Offset: 0x0001F906
		protected override void EndPayload()
		{
			this.jsonLightOutputContext.JsonWriter.EndObjectScope();
			this.jsonLightValueSerializer.WritePayloadEnd();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00021724 File Offset: 0x0001F924
		protected override void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			if (parameterValue == null)
			{
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = parameterValue as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.jsonLightValueSerializer.WriteComplexValue(odataComplexValue, expectedTypeReference, false, false, base.DuplicatePropertyNamesChecker);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			ODataEnumValue odataEnumValue;
			if ((odataEnumValue = parameterValue as ODataEnumValue) != null)
			{
				this.jsonLightValueSerializer.WriteEnumValue(odataEnumValue, expectedTypeReference);
				return;
			}
			this.jsonLightValueSerializer.WritePrimitiveValue(parameterValue, expectedTypeReference);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000217A9 File Offset: 0x0001F9A9
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightCollectionWriter(this.jsonLightOutputContext, expectedItemType, this);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x000217C9 File Offset: 0x0001F9C9
		protected override ODataWriter CreateFormatEntryWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightWriter(this.jsonLightOutputContext, null, null, false, true, false, this);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000217ED File Offset: 0x0001F9ED
		protected override ODataWriter CreateFormatFeedWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightWriter(this.jsonLightOutputContext, null, null, true, true, false, this);
		}

		// Token: 0x040003B1 RID: 945
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x040003B2 RID: 946
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
