using System;
using System.Threading.Tasks;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200024A RID: 586
	internal sealed class ODataJsonLightParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x060019F5 RID: 6645 RVA: 0x0004CAB2 File Offset: 0x0004ACB2
		internal ODataJsonLightParameterWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmOperation operation)
			: base(jsonLightOutputContext, operation)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0004CAD5 File Offset: 0x0004ACD5
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0004CAE2 File Offset: 0x0004ACE2
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0004CAEF File Offset: 0x0004ACEF
		protected override Task FlushAsynchronously()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0004CAFC File Offset: 0x0004ACFC
		protected override void StartPayload()
		{
			this.jsonLightValueSerializer.WritePayloadStart();
			this.jsonLightOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0004CB19 File Offset: 0x0004AD19
		protected override void EndPayload()
		{
			this.jsonLightOutputContext.JsonWriter.EndObjectScope();
			this.jsonLightValueSerializer.WritePayloadEnd();
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0004CB38 File Offset: 0x0004AD38
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

		// Token: 0x060019FC RID: 6652 RVA: 0x0004CB90 File Offset: 0x0004AD90
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightCollectionWriter(this.jsonLightOutputContext, expectedItemType, this);
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
		protected override ODataWriter CreateFormatResourceWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightWriter(this.jsonLightOutputContext, null, null, false, true, false, this);
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x0004CBD4 File Offset: 0x0004ADD4
		protected override ODataWriter CreateFormatResourceSetWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightWriter(this.jsonLightOutputContext, null, null, true, true, false, this);
		}

		// Token: 0x04000B4B RID: 2891
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000B4C RID: 2892
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
