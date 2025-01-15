using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200019A RID: 410
	internal sealed class ODataJsonLightParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x00029264 File Offset: 0x00027464
		internal ODataJsonLightParameterWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmFunctionImport functionImport)
			: base(jsonLightOutputContext, functionImport)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightValueSerializer = new ODataJsonLightValueSerializer(this.jsonLightOutputContext);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00029286 File Offset: 0x00027486
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00029293 File Offset: 0x00027493
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000292A0 File Offset: 0x000274A0
		protected override void StartPayload()
		{
			this.jsonLightValueSerializer.WritePayloadStart();
			this.jsonLightOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000292BD File Offset: 0x000274BD
		protected override void EndPayload()
		{
			this.jsonLightOutputContext.JsonWriter.EndObjectScope();
			this.jsonLightValueSerializer.WritePayloadEnd();
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000292DC File Offset: 0x000274DC
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
			this.jsonLightValueSerializer.WritePrimitiveValue(parameterValue, expectedTypeReference);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00029347 File Offset: 0x00027547
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.jsonLightOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataJsonLightCollectionWriter(this.jsonLightOutputContext, expectedItemType, this);
		}

		// Token: 0x0400043E RID: 1086
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x0400043F RID: 1087
		private readonly ODataJsonLightValueSerializer jsonLightValueSerializer;
	}
}
