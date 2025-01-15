using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000196 RID: 406
	internal sealed class ODataJsonLightParameterReader : ODataParameterReaderCoreAsync
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x000289DD File Offset: 0x00026BDD
		internal ODataJsonLightParameterReader(ODataJsonLightInputContext jsonLightInputContext, IEdmFunctionImport functionImport)
			: base(jsonLightInputContext, functionImport)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightParameterDeserializer = new ODataJsonLightParameterDeserializer(this, jsonLightInputContext);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000289FB File Offset: 0x00026BFB
		protected override bool ReadAtStartImplementation()
		{
			this.duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			this.jsonLightParameterDeserializer.ReadPayloadStart(ODataPayloadKind.Parameter, this.duplicatePropertyNamesChecker, false, true);
			return this.ReadAtStartImplementationSynchronously();
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00028A29 File Offset: 0x00026C29
		protected override bool ReadNextParameterImplementation()
		{
			return this.ReadNextParameterImplementationSynchronously();
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00028A31 File Offset: 0x00026C31
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderSynchronously(expectedItemTypeReference);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00028A3A File Offset: 0x00026C3A
		private bool ReadAtStartImplementationSynchronously()
		{
			if (this.jsonLightInputContext.JsonReader.NodeType == JsonNodeType.EndOfInput)
			{
				base.PopScope(ODataParameterReaderState.Start);
				base.EnterScope(ODataParameterReaderState.Completed, null, null);
				return false;
			}
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.duplicatePropertyNamesChecker);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00028A72 File Offset: 0x00026C72
		private bool ReadNextParameterImplementationSynchronously()
		{
			base.PopScope(this.State);
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.duplicatePropertyNamesChecker);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00028A91 File Offset: 0x00026C91
		private ODataCollectionReader CreateCollectionReaderSynchronously(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this.jsonLightInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x0400042E RID: 1070
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x0400042F RID: 1071
		private readonly ODataJsonLightParameterDeserializer jsonLightParameterDeserializer;

		// Token: 0x04000430 RID: 1072
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
