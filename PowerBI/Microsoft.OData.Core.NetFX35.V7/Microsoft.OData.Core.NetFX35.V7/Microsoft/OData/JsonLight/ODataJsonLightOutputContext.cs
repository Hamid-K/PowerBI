using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200020E RID: 526
	internal sealed class ODataJsonLightOutputContext : ODataOutputContext
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x0003EEF0 File Offset: 0x0003D0F0
		public ODataJsonLightOutputContext(ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
			: base(ODataFormat.Json, messageInfo, messageWriterSettings)
		{
			try
			{
				this.messageOutputStream = messageInfo.MessageStream;
				Stream stream;
				if (base.Synchronous)
				{
					stream = this.messageOutputStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(this.messageOutputStream);
					stream = this.asynchronousOutputStream;
				}
				this.textWriter = new StreamWriter(stream, messageInfo.Encoding);
				this.jsonWriter = ODataJsonLightOutputContext.CreateJsonWriter(base.Container, this.textWriter, messageInfo.MediaType.HasIeee754CompatibleSetToTrue());
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex))
				{
					this.messageOutputStream.Dispose();
				}
				throw;
			}
			Uri metadataDocumentUri = messageWriterSettings.MetadataDocumentUri;
			this.metadataLevel = JsonLightMetadataLevel.Create(messageInfo.MediaType, metadataDocumentUri, base.Model, base.WritingResponse);
			this.propertyCacheHandler = new PropertyCacheHandler();
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0003EFCC File Offset: 0x0003D1CC
		internal ODataJsonLightOutputContext(TextWriter textWriter, ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
			: base(ODataFormat.Json, messageInfo, messageWriterSettings)
		{
			this.textWriter = textWriter;
			this.jsonWriter = ODataJsonLightOutputContext.CreateJsonWriter(messageInfo.Container, textWriter, true);
			this.metadataLevel = new JsonMinimalMetadataLevel();
			this.propertyCacheHandler = new PropertyCacheHandler();
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0003F00B File Offset: 0x0003D20B
		public IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x0003F013 File Offset: 0x0003D213
		public JsonLightTypeNameOracle TypeNameOracle
		{
			get
			{
				if (this.typeNameOracle == null)
				{
					this.typeNameOracle = this.MetadataLevel.GetTypeNameOracle();
				}
				return this.typeNameOracle;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0003F034 File Offset: 0x0003D234
		public JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0003F03C File Offset: 0x0003D23C
		public PropertyCacheHandler PropertyCacheHandler
		{
			get
			{
				return this.propertyCacheHandler;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x0003F044 File Offset: 0x0003D244
		internal override ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return this.metadataLevel.ContextUrlLevel;
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0003F051 File Offset: 0x0003D251
		public override ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, false);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0003F05C File Offset: 0x0003D25C
		public override ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceWriterImplementation(navigationSource, resourceType);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0003F066 File Offset: 0x0003D266
		public override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0003F06F File Offset: 0x0003D26F
		public override ODataWriter CreateODataUriParameterResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceWriter(navigationSource, resourceType);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0003F079 File Offset: 0x0003D279
		public override ODataWriter CreateODataUriParameterResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, true);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0003F084 File Offset: 0x0003D284
		public override ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			return this.CreateODataParameterWriterImplementation(operation);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0003F08D File Offset: 0x0003D28D
		public override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			this.Flush();
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0003F09C File Offset: 0x0003D29C
		public override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x0003F0AC File Offset: 0x0003D2AC
		public void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0003F0C7 File Offset: 0x0003D2C7
		public void Flush()
		{
			this.jsonWriter.Flush();
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0003F0D4 File Offset: 0x0003D2D4
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0003F0E4 File Offset: 0x0003D2E4
		internal override ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataDeltaWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0003F0EE File Offset: 0x0003D2EE
		internal override void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.WriteServiceDocumentImplementation(serviceDocument);
			this.Flush();
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0003F0FD File Offset: 0x0003D2FD
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			this.Flush();
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0003F10C File Offset: 0x0003D30C
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			this.Flush();
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0003F11C File Offset: 0x0003D31C
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "textWriter", Justification = "We don't dispose the jsonWriter or textWriter, instead we dispose the underlying stream directly.")]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.messageOutputStream != null)
				{
					this.jsonWriter.Flush();
					if (this.asynchronousOutputStream != null)
					{
						this.asynchronousOutputStream.FlushSync();
						this.asynchronousOutputStream.Dispose();
					}
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.asynchronousOutputStream = null;
				this.textWriter = null;
				this.jsonWriter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0003F19C File Offset: 0x0003D39C
		private static IJsonWriter CreateJsonWriter(IServiceProvider container, TextWriter textWriter, bool isIeee754Compatible)
		{
			if (container == null)
			{
				return new JsonWriter(textWriter, isIeee754Compatible);
			}
			IJsonWriterFactory requiredService = container.GetRequiredService<IJsonWriterFactory>();
			return requiredService.CreateJsonWriter(textWriter, isIeee754Compatible);
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0003F1C8 File Offset: 0x0003D3C8
		private ODataWriter CreateODataResourceSetWriterImplementation(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType, bool writingParameter)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, resourceType, true, writingParameter, false, null);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0003F1EC File Offset: 0x0003D3EC
		private ODataDeltaWriter CreateODataDeltaWriterImplementation(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightDeltaWriter odataJsonLightDeltaWriter = new ODataJsonLightDeltaWriter(this, entitySet, entityType);
			this.outputInStreamErrorListener = odataJsonLightDeltaWriter;
			return odataJsonLightDeltaWriter;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0003F20C File Offset: 0x0003D40C
		private ODataWriter CreateODataResourceWriterImplementation(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, navigationSource, resourceType, false, false, false, null);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0003F230 File Offset: 0x0003D430
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataJsonLightCollectionWriter odataJsonLightCollectionWriter = new ODataJsonLightCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataJsonLightCollectionWriter;
			return odataJsonLightCollectionWriter;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0003F250 File Offset: 0x0003D450
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmOperation operation)
		{
			ODataJsonLightParameterWriter odataJsonLightParameterWriter = new ODataJsonLightParameterWriter(this, operation);
			this.outputInStreamErrorListener = odataJsonLightParameterWriter;
			return odataJsonLightParameterWriter;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0003F270 File Offset: 0x0003D470
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			JsonLightInstanceAnnotationWriter jsonLightInstanceAnnotationWriter = new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(this, false), this.TypeNameOracle);
			ODataJsonWriterUtils.WriteError(this.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(jsonLightInstanceAnnotationWriter.WriteInstanceAnnotationsForError), error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0003F2D0 File Offset: 0x0003D4D0
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataJsonLightPropertySerializer odataJsonLightPropertySerializer = new ODataJsonLightPropertySerializer(this, true);
			odataJsonLightPropertySerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0003F2EC File Offset: 0x0003D4EC
		private void WriteServiceDocumentImplementation(ODataServiceDocument serviceDocument)
		{
			ODataJsonLightServiceDocumentSerializer odataJsonLightServiceDocumentSerializer = new ODataJsonLightServiceDocumentSerializer(this);
			odataJsonLightServiceDocumentSerializer.WriteServiceDocument(serviceDocument);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0003F308 File Offset: 0x0003D508
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataJsonLightSerializer odataJsonLightSerializer = new ODataJsonLightSerializer(this, false);
			odataJsonLightSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0003F328 File Offset: 0x0003D528
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0003F344 File Offset: 0x0003D544
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x04000A21 RID: 2593
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x04000A22 RID: 2594
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x04000A23 RID: 2595
		private Stream messageOutputStream;

		// Token: 0x04000A24 RID: 2596
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x04000A25 RID: 2597
		private TextWriter textWriter;

		// Token: 0x04000A26 RID: 2598
		private IJsonWriter jsonWriter;

		// Token: 0x04000A27 RID: 2599
		private JsonLightTypeNameOracle typeNameOracle;

		// Token: 0x04000A28 RID: 2600
		private PropertyCacheHandler propertyCacheHandler;
	}
}
