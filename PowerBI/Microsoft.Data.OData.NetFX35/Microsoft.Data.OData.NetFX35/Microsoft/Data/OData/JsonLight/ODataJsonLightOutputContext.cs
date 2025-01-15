using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000195 RID: 405
	internal sealed class ODataJsonLightOutputContext : ODataJsonOutputContextBase
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x00028734 File Offset: 0x00026934
		internal ODataJsonLightOutputContext(ODataFormat format, TextWriter textWriter, ODataMessageWriterSettings messageWriterSettings, IEdmModel model)
			: base(format, textWriter, messageWriterSettings, model)
		{
			this.metadataLevel = new JsonMinimalMetadataLevel();
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002874C File Offset: 0x0002694C
		internal ODataJsonLightOutputContext(ODataFormat format, Stream messageStream, MediaType mediaType, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageStream, encoding, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			Uri uri = ((messageWriterSettings.MetadataDocumentUri == null) ? null : messageWriterSettings.MetadataDocumentUri.BaseUri);
			this.metadataLevel = JsonLightMetadataLevel.Create(mediaType, uri, model, writingResponse);
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00028797 File Offset: 0x00026997
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

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x000287C3 File Offset: 0x000269C3
		internal JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000287CB File Offset: 0x000269CB
		internal ODataJsonLightMetadataUriBuilder CreateMetadataUriBuilder()
		{
			return ODataJsonLightMetadataUriBuilder.CreateFromSettings(this.MetadataLevel, base.WritingResponse, base.MessageWriterSettings, base.Model);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000287EA File Offset: 0x000269EA
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000287FA File Offset: 0x000269FA
		internal override ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00028804 File Offset: 0x00026A04
		internal override ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002880E File Offset: 0x00026A0E
		internal override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x00028817 File Offset: 0x00026A17
		internal override ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			return this.CreateODataParameterWriterImplementation(functionImport);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00028820 File Offset: 0x00026A20
		internal override void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.WriteServiceDocumentImplementation(defaultWorkspace);
			base.Flush();
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002882F File Offset: 0x00026A2F
		internal override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			base.Flush();
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002883E File Offset: 0x00026A3E
		internal override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			base.Flush();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002884E File Offset: 0x00026A4E
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinksImplementation(links, entitySet, navigationProperty);
			base.Flush();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002885F File Offset: 0x00026A5F
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinkImplementation(link, entitySet, navigationProperty);
			base.Flush();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00028870 File Offset: 0x00026A70
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00028890 File Offset: 0x00026A90
		private ODataWriter CreateODataEntryWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, entityType, false);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000288B0 File Offset: 0x00026AB0
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataJsonLightCollectionWriter odataJsonLightCollectionWriter = new ODataJsonLightCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataJsonLightCollectionWriter;
			return odataJsonLightCollectionWriter;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000288D0 File Offset: 0x00026AD0
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmFunctionImport functionImport)
		{
			ODataJsonLightParameterWriter odataJsonLightParameterWriter = new ODataJsonLightParameterWriter(this, functionImport);
			this.outputInStreamErrorListener = odataJsonLightParameterWriter;
			return odataJsonLightParameterWriter;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000288F0 File Offset: 0x00026AF0
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			JsonLightInstanceAnnotationWriter jsonLightInstanceAnnotationWriter = new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(this), this.TypeNameOracle);
			ODataJsonWriterUtils.WriteError(base.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(jsonLightInstanceAnnotationWriter.WriteInstanceAnnotations), error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002894C File Offset: 0x00026B4C
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataJsonLightPropertySerializer odataJsonLightPropertySerializer = new ODataJsonLightPropertySerializer(this);
			odataJsonLightPropertySerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00028968 File Offset: 0x00026B68
		private void WriteServiceDocumentImplementation(ODataWorkspace defaultWorkspace)
		{
			ODataJsonLightServiceDocumentSerializer odataJsonLightServiceDocumentSerializer = new ODataJsonLightServiceDocumentSerializer(this);
			odataJsonLightServiceDocumentSerializer.WriteServiceDocument(defaultWorkspace);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00028984 File Offset: 0x00026B84
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataJsonLightSerializer odataJsonLightSerializer = new ODataJsonLightSerializer(this);
			odataJsonLightSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000289A0 File Offset: 0x00026BA0
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links, entitySet, navigationProperty);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000289C0 File Offset: 0x00026BC0
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLink(link, entitySet, navigationProperty);
		}

		// Token: 0x0400042C RID: 1068
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x0400042D RID: 1069
		private JsonLightTypeNameOracle typeNameOracle;
	}
}
