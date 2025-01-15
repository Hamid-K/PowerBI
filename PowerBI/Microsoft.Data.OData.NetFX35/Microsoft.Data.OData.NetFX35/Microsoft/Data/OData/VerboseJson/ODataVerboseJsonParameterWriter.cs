using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001DD RID: 477
	internal sealed class ODataVerboseJsonParameterWriter : ODataParameterWriterCore
	{
		// Token: 0x06000DF8 RID: 3576 RVA: 0x00031EB2 File Offset: 0x000300B2
		internal ODataVerboseJsonParameterWriter(ODataVerboseJsonOutputContext verboseJsonOutputContext, IEdmFunctionImport functionImport)
			: base(verboseJsonOutputContext, functionImport)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
			this.verboseJsonPropertyAndValueSerializer = new ODataVerboseJsonPropertyAndValueSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00031ED4 File Offset: 0x000300D4
		protected override void VerifyNotDisposed()
		{
			this.verboseJsonOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00031EE1 File Offset: 0x000300E1
		protected override void FlushSynchronously()
		{
			this.verboseJsonOutputContext.Flush();
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00031EEE File Offset: 0x000300EE
		protected override void StartPayload()
		{
			this.verboseJsonPropertyAndValueSerializer.WritePayloadStart();
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00031F0B File Offset: 0x0003010B
		protected override void EndPayload()
		{
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
			this.verboseJsonPropertyAndValueSerializer.WritePayloadEnd();
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00031F28 File Offset: 0x00030128
		protected override void WriteValueParameter(string parameterName, object parameterValue, IEdmTypeReference expectedTypeReference)
		{
			this.verboseJsonOutputContext.JsonWriter.WriteName(parameterName);
			if (parameterValue == null)
			{
				this.verboseJsonOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = parameterValue as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.verboseJsonPropertyAndValueSerializer.WriteComplexValue(odataComplexValue, expectedTypeReference, false, base.DuplicatePropertyNamesChecker, null);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.verboseJsonPropertyAndValueSerializer.WritePrimitiveValue(parameterValue, null, expectedTypeReference);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00031F94 File Offset: 0x00030194
		protected override ODataCollectionWriter CreateFormatCollectionWriter(string parameterName, IEdmTypeReference expectedItemType)
		{
			this.verboseJsonOutputContext.JsonWriter.WriteName(parameterName);
			return new ODataVerboseJsonCollectionWriter(this.verboseJsonOutputContext, expectedItemType, this);
		}

		// Token: 0x0400051D RID: 1309
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;

		// Token: 0x0400051E RID: 1310
		private readonly ODataVerboseJsonPropertyAndValueSerializer verboseJsonPropertyAndValueSerializer;
	}
}
