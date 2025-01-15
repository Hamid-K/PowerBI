using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000E5 RID: 229
	internal sealed class ODataJsonLightOutputContext : ODataJsonOutputContextBase
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		internal ODataJsonLightOutputContext(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, textWriter, messageWriterSettings, model)
		{
			this.metadataLevel = new JsonMinimalMetadataLevel();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001FFFC File Offset: 0x0001E1FC
		internal ODataJsonLightOutputContext(ODataFormat format, Stream messageStream, ODataMediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageStream, encoding, messageWriterSettings, writingResponse, synchronous, mediaType.HasIeee754CompatibleSetToTrue(), model, urlResolver)
		{
			Uri metadataDocumentUri = messageWriterSettings.MetadataDocumentUri;
			this.metadataLevel = JsonLightMetadataLevel.Create(mediaType, metadataDocumentUri, model, writingResponse);
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002003C File Offset: 0x0001E23C
		internal JsonLightTypeNameOracle TypeNameOracle
		{
			get
			{
				if (this.typeNameOracle == null)
				{
					this.typeNameOracle = this.MetadataLevel.GetTypeNameOracle(base.MessageWriterSettings.AutoComputePayloadMetadataInJson);
				}
				return this.typeNameOracle;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00020068 File Offset: 0x0001E268
		internal JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00020070 File Offset: 0x0001E270
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return this.metadataLevel.ContextUrlLevel;
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002007D File Offset: 0x0001E27D
		public override ODataWriter CreateODataFeedWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00020087 File Offset: 0x0001E287
		public override ODataWriter CreateODataEntryWriter(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(navigationSource, entityType);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00020091 File Offset: 0x0001E291
		public override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002009A File Offset: 0x0001E29A
		public override ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			return this.CreateODataParameterWriterImplementation(operation);
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x000200A3 File Offset: 0x0001E2A3
		public override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			base.Flush();
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x000200B2 File Offset: 0x0001E2B2
		public override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x000200C2 File Offset: 0x0001E2C2
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x000200D2 File Offset: 0x0001E2D2
		internal override ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataDeltaWriterImplementation(entitySet, entityType);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000200DC File Offset: 0x0001E2DC
		internal override void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.WriteServiceDocumentImplementation(serviceDocument);
			base.Flush();
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000200EB File Offset: 0x0001E2EB
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			base.Flush();
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000200FA File Offset: 0x0001E2FA
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			base.Flush();
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0002010C File Offset: 0x0001E30C
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, entityType, true, false, false, null);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00020130 File Offset: 0x0001E330
		private ODataDeltaWriter CreateODataDeltaWriterImplementation(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightDeltaWriter odataJsonLightDeltaWriter = new ODataJsonLightDeltaWriter(this, entitySet, entityType);
			this.outputInStreamErrorListener = odataJsonLightDeltaWriter;
			return odataJsonLightDeltaWriter;
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00020150 File Offset: 0x0001E350
		private ODataWriter CreateODataEntryWriterImplementation(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, navigationSource, entityType, false, false, false, null);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00020174 File Offset: 0x0001E374
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataJsonLightCollectionWriter odataJsonLightCollectionWriter = new ODataJsonLightCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataJsonLightCollectionWriter;
			return odataJsonLightCollectionWriter;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00020194 File Offset: 0x0001E394
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmOperation operation)
		{
			ODataJsonLightParameterWriter odataJsonLightParameterWriter = new ODataJsonLightParameterWriter(this, operation);
			this.outputInStreamErrorListener = odataJsonLightParameterWriter;
			return odataJsonLightParameterWriter;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000201B4 File Offset: 0x0001E3B4
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			JsonLightInstanceAnnotationWriter jsonLightInstanceAnnotationWriter = new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(this, false), this.TypeNameOracle);
			ODataJsonWriterUtils.WriteError(base.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(jsonLightInstanceAnnotationWriter.WriteInstanceAnnotationsForError), error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00020214 File Offset: 0x0001E414
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataJsonLightPropertySerializer odataJsonLightPropertySerializer = new ODataJsonLightPropertySerializer(this, true);
			odataJsonLightPropertySerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00020230 File Offset: 0x0001E430
		private void WriteServiceDocumentImplementation(ODataServiceDocument serviceDocument)
		{
			ODataJsonLightServiceDocumentSerializer odataJsonLightServiceDocumentSerializer = new ODataJsonLightServiceDocumentSerializer(this);
			odataJsonLightServiceDocumentSerializer.WriteServiceDocument(serviceDocument);
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0002024C File Offset: 0x0001E44C
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataJsonLightSerializer odataJsonLightSerializer = new ODataJsonLightSerializer(this, false);
			odataJsonLightSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0002026C File Offset: 0x0001E46C
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00020288 File Offset: 0x0001E488
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x04000390 RID: 912
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x04000391 RID: 913
		private JsonLightTypeNameOracle typeNameOracle;
	}
}
