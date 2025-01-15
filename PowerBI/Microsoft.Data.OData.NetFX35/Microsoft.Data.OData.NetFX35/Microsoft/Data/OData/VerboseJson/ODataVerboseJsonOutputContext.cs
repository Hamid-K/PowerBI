using System;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001C8 RID: 456
	internal sealed class ODataVerboseJsonOutputContext : ODataJsonOutputContextBase
	{
		// Token: 0x06000D5F RID: 3423 RVA: 0x0002FE45 File Offset: 0x0002E045
		internal ODataVerboseJsonOutputContext(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, textWriter, messageWriterSettings, model)
		{
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002FE60 File Offset: 0x0002E060
		internal ODataVerboseJsonOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageStream, encoding, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x0002FE8B File Offset: 0x0002E08B
		internal AtomAndVerboseJsonTypeNameOracle TypeNameOracle
		{
			get
			{
				return this.typeNameOracle;
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002FE93 File Offset: 0x0002E093
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002FEA3 File Offset: 0x0002E0A3
		internal override ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002FEAD File Offset: 0x0002E0AD
		internal override ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002FEB7 File Offset: 0x0002E0B7
		internal override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002FEC0 File Offset: 0x0002E0C0
		internal override ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			return this.CreateODataParameterWriterImplementation(functionImport);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002FEC9 File Offset: 0x0002E0C9
		internal override void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.WriteServiceDocumentImplementation(defaultWorkspace);
			base.Flush();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002FED8 File Offset: 0x0002E0D8
		internal override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			base.Flush();
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002FEE7 File Offset: 0x0002E0E7
		internal override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002FEF7 File Offset: 0x0002E0F7
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			base.Flush();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002FF06 File Offset: 0x0002E106
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			base.Flush();
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002FF18 File Offset: 0x0002E118
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataVerboseJsonWriter odataVerboseJsonWriter = new ODataVerboseJsonWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataVerboseJsonWriter;
			return odataVerboseJsonWriter;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002FF38 File Offset: 0x0002E138
		private ODataWriter CreateODataEntryWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataVerboseJsonWriter odataVerboseJsonWriter = new ODataVerboseJsonWriter(this, entitySet, entityType, false);
			this.outputInStreamErrorListener = odataVerboseJsonWriter;
			return odataVerboseJsonWriter;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002FF58 File Offset: 0x0002E158
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataVerboseJsonCollectionWriter odataVerboseJsonCollectionWriter = new ODataVerboseJsonCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataVerboseJsonCollectionWriter;
			return odataVerboseJsonCollectionWriter;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002FF78 File Offset: 0x0002E178
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmFunctionImport functionImport)
		{
			ODataVerboseJsonParameterWriter odataVerboseJsonParameterWriter = new ODataVerboseJsonParameterWriter(this, functionImport);
			this.outputInStreamErrorListener = odataVerboseJsonParameterWriter;
			return odataVerboseJsonParameterWriter;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002FF95 File Offset: 0x0002E195
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			ODataJsonWriterUtils.WriteError(base.JsonWriter, null, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, false);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002FFCC File Offset: 0x0002E1CC
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataVerboseJsonPropertyAndValueSerializer odataVerboseJsonPropertyAndValueSerializer = new ODataVerboseJsonPropertyAndValueSerializer(this);
			odataVerboseJsonPropertyAndValueSerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002FFE8 File Offset: 0x0002E1E8
		private void WriteServiceDocumentImplementation(ODataWorkspace defaultWorkspace)
		{
			ODataVerboseJsonServiceDocumentSerializer odataVerboseJsonServiceDocumentSerializer = new ODataVerboseJsonServiceDocumentSerializer(this);
			odataVerboseJsonServiceDocumentSerializer.WriteServiceDocument(defaultWorkspace);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00030004 File Offset: 0x0002E204
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataVerboseJsonSerializer odataVerboseJsonSerializer = new ODataVerboseJsonSerializer(this);
			odataVerboseJsonSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00030020 File Offset: 0x0002E220
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataVerboseJsonEntityReferenceLinkSerializer odataVerboseJsonEntityReferenceLinkSerializer = new ODataVerboseJsonEntityReferenceLinkSerializer(this);
			odataVerboseJsonEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0003003C File Offset: 0x0002E23C
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataVerboseJsonEntityReferenceLinkSerializer odataVerboseJsonEntityReferenceLinkSerializer = new ODataVerboseJsonEntityReferenceLinkSerializer(this);
			odataVerboseJsonEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x040004AF RID: 1199
		private readonly AtomAndVerboseJsonTypeNameOracle typeNameOracle = new AtomAndVerboseJsonTypeNameOracle();
	}
}
