using System;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000EC RID: 236
	internal sealed class ODataJsonLightParameterReader : ODataParameterReaderCoreAsync
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x00020BE9 File Offset: 0x0001EDE9
		internal ODataJsonLightParameterReader(ODataJsonLightInputContext jsonLightInputContext, IEdmOperation operation)
			: base(jsonLightInputContext, operation)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightParameterDeserializer = new ODataJsonLightParameterDeserializer(this, jsonLightInputContext);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00020C07 File Offset: 0x0001EE07
		protected override bool ReadAtStartImplementation()
		{
			this.duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			this.jsonLightParameterDeserializer.ReadPayloadStart(ODataPayloadKind.Parameter, this.duplicatePropertyNamesChecker, false, true);
			return this.ReadAtStartImplementationSynchronously();
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00020C35 File Offset: 0x0001EE35
		protected override bool ReadNextParameterImplementation()
		{
			return this.ReadNextParameterImplementationSynchronously();
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00020C3D File Offset: 0x0001EE3D
		protected override ODataReader CreateEntryReader(IEdmEntityType expectedEntityType)
		{
			return this.CreateEntryReaderSynchronously(expectedEntityType);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00020C46 File Offset: 0x0001EE46
		protected override ODataReader CreateFeedReader(IEdmEntityType expectedEntityType)
		{
			return this.CreateFeedReaderSynchronously(expectedEntityType);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00020C4F File Offset: 0x0001EE4F
		protected override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderSynchronously(expectedItemTypeReference);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00020C58 File Offset: 0x0001EE58
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

		// Token: 0x060008F5 RID: 2293 RVA: 0x00020C90 File Offset: 0x0001EE90
		private bool ReadNextParameterImplementationSynchronously()
		{
			base.PopScope(this.State);
			return this.jsonLightParameterDeserializer.ReadNextParameter(this.duplicatePropertyNamesChecker);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00020CAF File Offset: 0x0001EEAF
		private ODataReader CreateEntryReaderSynchronously(IEdmEntityType expectedEntityType)
		{
			return new ODataJsonLightReader(this.jsonLightInputContext, null, expectedEntityType, false, true, false, this);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00020CC2 File Offset: 0x0001EEC2
		private ODataReader CreateFeedReaderSynchronously(IEdmEntityType expectedEntityType)
		{
			return new ODataJsonLightReader(this.jsonLightInputContext, null, expectedEntityType, true, true, false, this);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00020CD5 File Offset: 0x0001EED5
		private ODataCollectionReader CreateCollectionReaderSynchronously(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this.jsonLightInputContext, expectedItemTypeReference, this);
		}

		// Token: 0x040003A1 RID: 929
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x040003A2 RID: 930
		private readonly ODataJsonLightParameterDeserializer jsonLightParameterDeserializer;

		// Token: 0x040003A3 RID: 931
		private DuplicatePropertyNamesChecker duplicatePropertyNamesChecker;
	}
}
