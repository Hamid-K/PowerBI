using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000247 RID: 583
	internal sealed class ODataJsonLightOutputContext : ODataOutputContext
	{
		// Token: 0x060019A5 RID: 6565 RVA: 0x0004BEFC File Offset: 0x0004A0FC
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
				this.jsonWriter = ODataJsonLightOutputContext.CreateJsonWriter(base.Container, this.textWriter, messageInfo.MediaType.HasIeee754CompatibleSetToTrue(), messageWriterSettings);
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

		// Token: 0x060019A6 RID: 6566 RVA: 0x0004BFD8 File Offset: 0x0004A1D8
		internal ODataJsonLightOutputContext(TextWriter textWriter, ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
			: base(ODataFormat.Json, messageInfo, messageWriterSettings)
		{
			this.textWriter = textWriter;
			this.jsonWriter = ODataJsonLightOutputContext.CreateJsonWriter(messageInfo.Container, textWriter, true, messageWriterSettings);
			this.metadataLevel = new JsonMinimalMetadataLevel();
			this.propertyCacheHandler = new PropertyCacheHandler();
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0004C018 File Offset: 0x0004A218
		public IJsonWriter JsonWriter
		{
			get
			{
				return this.jsonWriter;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0004C020 File Offset: 0x0004A220
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

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0004C041 File Offset: 0x0004A241
		public JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0004C049 File Offset: 0x0004A249
		public PropertyCacheHandler PropertyCacheHandler
		{
			get
			{
				return this.propertyCacheHandler;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0004C054 File Offset: 0x0004A254
		internal bool OmitODataPrefix
		{
			get
			{
				return base.ODataSimplifiedOptions.GetOmitODataPrefix(base.MessageWriterSettings.Version ?? ODataVersion.V4);
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0004C08B File Offset: 0x0004A28B
		public override ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, false, false);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0004C098 File Offset: 0x0004A298
		public override Task<ODataWriter> CreateODataResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, false, false));
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0004C0D1 File Offset: 0x0004A2D1
		public override ODataWriter CreateODataDeltaResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, false, true);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0004C0E0 File Offset: 0x0004A2E0
		public override Task<ODataWriter> CreateODataDeltaResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, false, true));
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0004C119 File Offset: 0x0004A319
		public override ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceWriterImplementation(navigationSource, resourceType);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0004C124 File Offset: 0x0004A324
		public override Task<ODataWriter> CreateODataResourceWriterAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataResourceWriterImplementation(navigationSource, resourceType));
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0004C15D File Offset: 0x0004A35D
		public override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0004C168 File Offset: 0x0004A368
		public override Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionWriter>(() => this.CreateODataCollectionWriterImplementation(itemTypeReference));
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0004C19A File Offset: 0x0004A39A
		public override ODataWriter CreateODataUriParameterResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceWriter(navigationSource, resourceType);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0004C1A4 File Offset: 0x0004A3A4
		public override Task<ODataWriter> CreateODataUriParameterResourceWriterAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceWriterAsync(navigationSource, resourceType);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0004C1AE File Offset: 0x0004A3AE
		public override ODataWriter CreateODataUriParameterResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, true, false);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		public override Task<ODataWriter> CreateODataUriParameterResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataWriter>(() => this.CreateODataResourceSetWriterImplementation(entitySet, resourceType, true, false));
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0004C1F5 File Offset: 0x0004A3F5
		public override ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			return this.CreateODataParameterWriterImplementation(operation);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0004C200 File Offset: 0x0004A400
		public override Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmOperation operation)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataParameterWriter>(() => this.CreateODataParameterWriterImplementation(operation));
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0004C232 File Offset: 0x0004A432
		public override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			this.Flush();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0004C244 File Offset: 0x0004A444
		public override Task WritePropertyAsync(ODataProperty property)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WritePropertyImplementation(property);
				return this.FlushAsync();
			});
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0004C276 File Offset: 0x0004A476
		public override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0004C288 File Offset: 0x0004A488
		public override Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0004C2C1 File Offset: 0x0004A4C1
		public void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0004C2DC File Offset: 0x0004A4DC
		public void Flush()
		{
			this.jsonWriter.Flush();
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0004C2E9 File Offset: 0x0004A4E9
		public Task FlushAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.jsonWriter.Flush();
				return this.asynchronousOutputStream.FlushAsync();
			}).FollowOnSuccessWithTask((Task asyncBufferedStreamFlushTask) => this.messageOutputStream.FlushAsync());
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0004C30D File Offset: 0x0004A50D
		internal void FlushBuffers()
		{
			if (this.asynchronousOutputStream != null)
			{
				this.asynchronousOutputStream.FlushSync();
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0004C322 File Offset: 0x0004A522
		internal Task FlushBuffersAsync()
		{
			if (this.asynchronousOutputStream != null)
			{
				return this.asynchronousOutputStream.FlushAsync();
			}
			return TaskUtils.CompletedTask;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0004C33D File Offset: 0x0004A53D
		internal Stream GetOutputStream()
		{
			if (!base.Synchronous)
			{
				return this.asynchronousOutputStream;
			}
			return this.messageOutputStream;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0004C354 File Offset: 0x0004A554
		internal override ODataBatchWriter CreateODataBatchWriter()
		{
			return this.CreateODataBatchWriterImplementation();
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0004C35C File Offset: 0x0004A55C
		internal override Task<ODataBatchWriter> CreateODataBatchWriterAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchWriter>(() => this.CreateODataBatchWriterImplementation());
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0004C36F File Offset: 0x0004A56F
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0004C380 File Offset: 0x0004A580
		internal override Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteInStreamErrorImplementation(error, includeDebugInformation);
				return this.FlushAsync();
			});
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0004C3B9 File Offset: 0x0004A5B9
		internal override ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataDeltaWriterImplementation(entitySet, entityType);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0004C3C4 File Offset: 0x0004A5C4
		internal override Task<ODataDeltaWriter> CreateODataDeltaWriterAsync(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataDeltaWriter>(() => this.CreateODataDeltaWriterImplementation(entitySet, entityType));
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0004C3FD File Offset: 0x0004A5FD
		internal override void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.WriteServiceDocumentImplementation(serviceDocument);
			this.Flush();
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0004C40C File Offset: 0x0004A60C
		internal override Task WriteServiceDocumentAsync(ODataServiceDocument serviceDocument)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteServiceDocumentImplementation(serviceDocument);
				return this.FlushAsync();
			});
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0004C43E File Offset: 0x0004A63E
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			this.Flush();
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0004C450 File Offset: 0x0004A650
		internal override Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinksImplementation(links);
				return this.FlushAsync();
			});
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0004C482 File Offset: 0x0004A682
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			this.Flush();
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0004C494 File Offset: 0x0004A694
		internal override Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link)
		{
			return TaskUtils.GetTaskForSynchronousOperationReturningTask(delegate
			{
				this.WriteEntityReferenceLinkImplementation(link);
				return this.FlushAsync();
			});
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		[SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "textWriter", Justification = "We don't dispose the jsonWriter or textWriter, instead we dispose the underlying stream directly.")]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.messageOutputStream != null)
				{
					this.jsonWriter.Flush();
					JsonWriter jsonWriter = this.jsonWriter as JsonWriter;
					if (jsonWriter != null)
					{
						jsonWriter.Dispose();
					}
					if (this.asynchronousOutputStream != null)
					{
						this.asynchronousOutputStream.FlushSync();
						this.asynchronousOutputStream.Dispose();
					}
					this.messageOutputStream.Dispose();
				}
				if (this.BinaryValueStream != null)
				{
					this.BinaryValueStream.Flush();
					this.BinaryValueStream.Dispose();
				}
				if (this.StringWriter != null)
				{
					this.StringWriter.Flush();
					this.StringWriter.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.asynchronousOutputStream = null;
				this.BinaryValueStream = null;
				this.textWriter = null;
				this.jsonWriter = null;
				this.StringWriter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
		private static IJsonWriter CreateJsonWriter(IServiceProvider container, TextWriter textWriter, bool isIeee754Compatible, ODataMessageWriterSettings writerSettings)
		{
			IJsonWriter jsonWriter;
			if (container == null)
			{
				jsonWriter = new JsonWriter(textWriter, isIeee754Compatible);
			}
			else
			{
				IJsonWriterFactory requiredService = container.GetRequiredService<IJsonWriterFactory>();
				jsonWriter = requiredService.CreateJsonWriter(textWriter, isIeee754Compatible);
			}
			JsonWriter jsonWriter2 = jsonWriter as JsonWriter;
			if (jsonWriter2 != null && writerSettings.ArrayPool != null)
			{
				jsonWriter2.ArrayPool = writerSettings.ArrayPool;
			}
			return jsonWriter;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0004C5F4 File Offset: 0x0004A7F4
		private ODataWriter CreateODataResourceSetWriterImplementation(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType, bool writingParameter, bool writingDelta)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, entitySet, resourceType, true, writingParameter, writingDelta, null);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0004C618 File Offset: 0x0004A818
		private ODataDeltaWriter CreateODataDeltaWriterImplementation(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			ODataJsonLightDeltaWriter odataJsonLightDeltaWriter = new ODataJsonLightDeltaWriter(this, entitySet, entityType);
			this.outputInStreamErrorListener = odataJsonLightDeltaWriter;
			return odataJsonLightDeltaWriter;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0004C638 File Offset: 0x0004A838
		private ODataWriter CreateODataResourceWriterImplementation(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			ODataJsonLightWriter odataJsonLightWriter = new ODataJsonLightWriter(this, navigationSource, resourceType, false, false, false, null);
			this.outputInStreamErrorListener = odataJsonLightWriter;
			return odataJsonLightWriter;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0004C65C File Offset: 0x0004A85C
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataJsonLightCollectionWriter odataJsonLightCollectionWriter = new ODataJsonLightCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataJsonLightCollectionWriter;
			return odataJsonLightCollectionWriter;
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0004C67C File Offset: 0x0004A87C
		private ODataParameterWriter CreateODataParameterWriterImplementation(IEdmOperation operation)
		{
			ODataJsonLightParameterWriter odataJsonLightParameterWriter = new ODataJsonLightParameterWriter(this, operation);
			this.outputInStreamErrorListener = odataJsonLightParameterWriter;
			return odataJsonLightParameterWriter;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0004C69C File Offset: 0x0004A89C
		private ODataBatchWriter CreateODataBatchWriterImplementation()
		{
			ODataBatchWriter odataBatchWriter = new ODataJsonLightBatchWriter(this);
			this.outputInStreamErrorListener = odataBatchWriter;
			return odataBatchWriter;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0004C6B8 File Offset: 0x0004A8B8
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			JsonLightInstanceAnnotationWriter jsonLightInstanceAnnotationWriter = new JsonLightInstanceAnnotationWriter(new ODataJsonLightValueSerializer(this, false), this.TypeNameOracle);
			ODataJsonWriterUtils.WriteError(this.JsonWriter, new Action<IEnumerable<ODataInstanceAnnotation>>(jsonLightInstanceAnnotationWriter.WriteInstanceAnnotationsForError), error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth, true);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0004C718 File Offset: 0x0004A918
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataJsonLightPropertySerializer odataJsonLightPropertySerializer = new ODataJsonLightPropertySerializer(this, true);
			odataJsonLightPropertySerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0004C734 File Offset: 0x0004A934
		private void WriteServiceDocumentImplementation(ODataServiceDocument serviceDocument)
		{
			ODataJsonLightServiceDocumentSerializer odataJsonLightServiceDocumentSerializer = new ODataJsonLightServiceDocumentSerializer(this);
			odataJsonLightServiceDocumentSerializer.WriteServiceDocument(serviceDocument);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0004C750 File Offset: 0x0004A950
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataJsonLightSerializer odataJsonLightSerializer = new ODataJsonLightSerializer(this, false);
			odataJsonLightSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0004C770 File Offset: 0x0004A970
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0004C78C File Offset: 0x0004A98C
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataJsonLightEntityReferenceLinkSerializer odataJsonLightEntityReferenceLinkSerializer = new ODataJsonLightEntityReferenceLinkSerializer(this);
			odataJsonLightEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x04000B3C RID: 2876
		internal MemoryStream BinaryValueStream;

		// Token: 0x04000B3D RID: 2877
		internal StringWriter StringWriter;

		// Token: 0x04000B3E RID: 2878
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x04000B3F RID: 2879
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;

		// Token: 0x04000B40 RID: 2880
		private Stream messageOutputStream;

		// Token: 0x04000B41 RID: 2881
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x04000B42 RID: 2882
		private TextWriter textWriter;

		// Token: 0x04000B43 RID: 2883
		private IJsonWriter jsonWriter;

		// Token: 0x04000B44 RID: 2884
		private JsonLightTypeNameOracle typeNameOracle;

		// Token: 0x04000B45 RID: 2885
		private PropertyCacheHandler propertyCacheHandler;
	}
}
