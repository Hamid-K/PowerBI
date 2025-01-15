using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000099 RID: 153
	public sealed class ODataMessageWriter : IDisposable
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x0000EB22 File Offset: 0x0000CD22
		public ODataMessageWriter(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0000EB38 File Offset: 0x0000CD38
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings, IEdmModel model)
		{
			this.writerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.container = ODataMessageWriter.GetContainer<IODataRequestMessage>(requestMessage);
			this.settings = ODataMessageWriterSettings.CreateWriterSettings(this.container, settings);
			this.writingResponse = false;
			this.payloadUriConverter = requestMessage as IODataPayloadUriConverter;
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.container);
			this.model = model ?? ODataMessageWriter.GetModel(this.container);
			WriterValidationUtils.ValidateMessageWriterSettings(this.settings, this.writingResponse);
			this.message = new ODataRequestMessage(requestMessage, true, this.settings.EnableMessageStreamDisposal, -1L);
			this.settings.ShouldIncludeAnnotation = new Func<string, bool>(AnnotationFilter.CreateInclueAllFilter().Matches);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0000EC01 File Offset: 0x0000CE01
		public ODataMessageWriter(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0000EC0B File Offset: 0x0000CE0B
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings, IEdmModel model)
		{
			this.writerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.container = ODataMessageWriter.GetContainer<IODataResponseMessage>(responseMessage);
			this.settings = ODataMessageWriterSettings.CreateWriterSettings(this.container, settings);
			this.writingResponse = true;
			this.payloadUriConverter = responseMessage as IODataPayloadUriConverter;
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.container);
			this.model = model ?? ODataMessageWriter.GetModel(this.container);
			WriterValidationUtils.ValidateMessageWriterSettings(this.settings, this.writingResponse);
			this.message = new ODataResponseMessage(responseMessage, true, this.settings.EnableMessageStreamDisposal, -1L);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (!string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000ECEA File Offset: 0x0000CEEA
		internal ODataMessageWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0000ECF2 File Offset: 0x0000CEF2
		public ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			this.VerifyCanCreateODataAsyncWriter();
			return this.WriteToOutput<ODataAsynchronousWriter>(ODataPayloadKind.Asynchronous, (ODataOutputContext context) => context.CreateODataAsynchronousWriter());
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0000ED21 File Offset: 0x0000CF21
		public ODataWriter CreateODataResourceSetWriter()
		{
			return this.CreateODataResourceSetWriter(null, null);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0000ED2B File Offset: 0x0000CF2B
		public ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet)
		{
			return this.CreateODataResourceSetWriter(entitySet, null);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0000ED38 File Offset: 0x0000CF38
		public ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataResourceSetWriter(entitySet, resourceType));
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000ED72 File Offset: 0x0000CF72
		public Task<ODataAsynchronousWriter> CreateODataAsynchronousWriterAsync()
		{
			this.VerifyCanCreateODataAsyncWriter();
			return this.WriteToOutputAsync<ODataAsynchronousWriter>(ODataPayloadKind.Asynchronous, (ODataOutputContext context) => context.CreateODataAsynchronousWriterAsync());
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public Task<ODataWriter> CreateODataResourceSetWriterAsync()
		{
			return this.CreateODataResourceSetWriterAsync(null, null);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000EDAB File Offset: 0x0000CFAB
		public Task<ODataWriter> CreateODataResourceSetWriterAsync(IEdmEntitySetBase entitySet)
		{
			return this.CreateODataResourceSetWriterAsync(entitySet, null);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000EDB8 File Offset: 0x0000CFB8
		public Task<ODataWriter> CreateODataResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataResourceSetWriterAsync(entitySet, entityType));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000EDF2 File Offset: 0x0000CFF2
		public ODataWriter CreateODataDeltaResourceSetWriter()
		{
			return this.CreateODataDeltaResourceSetWriter(null, null);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		public ODataWriter CreateODataDeltaResourceSetWriter(IEdmEntitySetBase entitySet)
		{
			return this.CreateODataDeltaResourceSetWriter(entitySet, null);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0000EE08 File Offset: 0x0000D008
		public ODataWriter CreateODataDeltaResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataDeltaResourceSetWriter(entitySet, resourceType));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0000EE42 File Offset: 0x0000D042
		public Task<ODataWriter> CreateODataDeltaResourceSetWriterAsync()
		{
			return this.CreateODataDeltaResourceSetWriterAsync(null, null);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000EE4C File Offset: 0x0000D04C
		public Task<ODataWriter> CreateODataDeltaResourceSetWriterAsync(IEdmEntitySetBase entitySet)
		{
			return this.CreateODataDeltaResourceSetWriterAsync(entitySet, null);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000EE58 File Offset: 0x0000D058
		public Task<ODataWriter> CreateODataDeltaResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataDeltaResourceSetWriterAsync(entitySet, entityType));
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000EE94 File Offset: 0x0000D094
		[Obsolete("Use CreateODataDeltaResourceSetWriter.", false)]
		public ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataDeltaWriter();
			return this.WriteToOutput<ODataDeltaWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataDeltaWriter(entitySet, entityType));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		[Obsolete("Use CreateODataDeltaResourceSetWriterAsync.", false)]
		public Task<ODataDeltaWriter> CreateODataDeltaWriterAsync(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutputAsync<ODataDeltaWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataDeltaWriterAsync(entitySet, entityType));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0000EF0A File Offset: 0x0000D10A
		public ODataWriter CreateODataResourceWriter()
		{
			return this.CreateODataResourceWriter(null, null);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000EF14 File Offset: 0x0000D114
		public ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource)
		{
			return this.CreateODataResourceWriter(navigationSource, null);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0000EF20 File Offset: 0x0000D120
		public ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Resource, (ODataOutputContext context) => context.CreateODataResourceWriter(navigationSource, resourceType));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0000EF5A File Offset: 0x0000D15A
		public Task<ODataWriter> CreateODataResourceWriterAsync()
		{
			return this.CreateODataResourceWriterAsync(null, null);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0000EF64 File Offset: 0x0000D164
		public Task<ODataWriter> CreateODataResourceWriterAsync(IEdmNavigationSource navigationSource)
		{
			return this.CreateODataResourceWriterAsync(navigationSource, null);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0000EF70 File Offset: 0x0000D170
		public Task<ODataWriter> CreateODataResourceWriterAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.Resource, (ODataOutputContext context) => context.CreateODataResourceWriterAsync(navigationSource, resourceType));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		public ODataWriter CreateODataUriParameterResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Resource, (ODataOutputContext context) => context.CreateODataUriParameterResourceWriter(navigationSource, resourceType));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public Task<ODataWriter> CreateODataUriParameterResourceWriterAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.Resource, (ODataOutputContext context) => context.CreateODataUriParameterResourceWriterAsync(navigationSource, resourceType));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000F024 File Offset: 0x0000D224
		public ODataWriter CreateODataUriParameterResourceSetWriter(IEdmEntitySetBase entitySetBase, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataUriParameterResourceSetWriter(entitySetBase, resourceType));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0000F060 File Offset: 0x0000D260
		public Task<ODataWriter> CreateODataUriParameterResourceSetWriterAsync(IEdmEntitySetBase entitySetBase, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutputAsync<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataUriParameterResourceSetWriterAsync(entitySetBase, resourceType));
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0000F09A File Offset: 0x0000D29A
		public ODataCollectionWriter CreateODataCollectionWriter()
		{
			return this.CreateODataCollectionWriter(null);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		public ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutput<ODataCollectionWriter>(ODataPayloadKind.Collection, (ODataOutputContext context) => context.CreateODataCollectionWriter(itemTypeReference));
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000F0DD File Offset: 0x0000D2DD
		public Task<ODataCollectionWriter> CreateODataCollectionWriterAsync()
		{
			return this.CreateODataCollectionWriterAsync(null);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		public Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutputAsync<ODataCollectionWriter>(ODataPayloadKind.Collection, (ODataOutputContext context) => context.CreateODataCollectionWriterAsync(itemTypeReference));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000F121 File Offset: 0x0000D321
		public ODataBatchWriter CreateODataBatchWriter()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutput<ODataBatchWriter>(ODataPayloadKind.Batch, (ODataOutputContext context) => context.CreateODataBatchWriter());
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000F150 File Offset: 0x0000D350
		public Task<ODataBatchWriter> CreateODataBatchWriterAsync()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutputAsync<ODataBatchWriter>(ODataPayloadKind.Batch, (ODataOutputContext context) => context.CreateODataBatchWriterAsync());
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000F180 File Offset: 0x0000D380
		public ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterWriter(operation);
			return this.WriteToOutput<ODataParameterWriter>(ODataPayloadKind.Parameter, (ODataOutputContext context) => context.CreateODataParameterWriter(operation));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterWriter(operation);
			return this.WriteToOutputAsync<ODataParameterWriter>(ODataPayloadKind.Parameter, (ODataOutputContext context) => context.CreateODataParameterWriterAsync(operation));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000F1F8 File Offset: 0x0000D3F8
		public void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.VerifyCanWriteServiceDocument(serviceDocument);
			this.WriteToOutput(ODataPayloadKind.ServiceDocument, delegate(ODataOutputContext context)
			{
				context.WriteServiceDocument(serviceDocument);
			});
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000F234 File Offset: 0x0000D434
		public Task WriteServiceDocumentAsync(ODataServiceDocument serviceDocument)
		{
			this.VerifyCanWriteServiceDocument(serviceDocument);
			return this.WriteToOutputAsync(ODataPayloadKind.ServiceDocument, (ODataOutputContext context) => context.WriteServiceDocumentAsync(serviceDocument));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0000F270 File Offset: 0x0000D470
		public void WriteProperty(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			this.WriteToOutput(ODataPayloadKind.Property, delegate(ODataOutputContext context)
			{
				context.WriteProperty(property);
			});
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		public Task WritePropertyAsync(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			return this.WriteToOutputAsync(ODataPayloadKind.Property, (ODataOutputContext context) => context.WritePropertyAsync(property));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public void WriteError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputContext == null)
			{
				this.VerifyCanWriteTopLevelError(error);
				this.WriteToOutput(ODataPayloadKind.Error, delegate(ODataOutputContext context)
				{
					context.WriteError(error, includeDebugInformation);
				});
				return;
			}
			this.VerifyCanWriteInStreamError(error);
			this.outputContext.WriteInStreamError(error, includeDebugInformation);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000F358 File Offset: 0x0000D558
		public Task WriteErrorAsync(ODataError error, bool includeDebugInformation)
		{
			if (this.outputContext == null)
			{
				this.VerifyCanWriteTopLevelError(error);
				return this.WriteToOutputAsync(ODataPayloadKind.Error, (ODataOutputContext context) => context.WriteErrorAsync(error, includeDebugInformation));
			}
			this.VerifyCanWriteInStreamError(error);
			return this.outputContext.WriteInStreamErrorAsync(error, includeDebugInformation);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.VerifyCanWriteEntityReferenceLinks(links);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLinks, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links);
			});
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000F404 File Offset: 0x0000D604
		public Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links)
		{
			this.VerifyCanWriteEntityReferenceLinks(links);
			return this.WriteToOutputAsync(ODataPayloadKind.EntityReferenceLinks, (ODataOutputContext context) => context.WriteEntityReferenceLinksAsync(links));
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0000F440 File Offset: 0x0000D640
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLink, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link);
			});
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0000F47C File Offset: 0x0000D67C
		public Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			return this.WriteToOutputAsync(ODataPayloadKind.EntityReferenceLink, (ODataOutputContext context) => context.WriteEntityReferenceLinkAsync(link));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
		public void WriteValue(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			this.WriteToOutput(odataPayloadKind, delegate(ODataOutputContext context)
			{
				context.WriteValue(value);
			});
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public Task WriteValueAsync(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			return this.WriteToOutputAsync(odataPayloadKind, (ODataOutputContext context) => context.WriteValueAsync(value));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0000F52E File Offset: 0x0000D72E
		public void WriteMetadataDocument()
		{
			this.VerifyCanWriteMetadataDocument();
			this.WriteToOutput(ODataPayloadKind.MetadataDocument, delegate(ODataOutputContext context)
			{
				context.WriteMetadataDocument();
			});
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0000F55D File Offset: 0x0000D75D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0000F56C File Offset: 0x0000D76C
		internal ODataFormat SetHeaders(ODataPayloadKind payloadKind)
		{
			this.writerPayloadKind = payloadKind;
			this.EnsureODataVersion();
			this.EnsureODataFormatAndContentType();
			return this.format;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0000F588 File Offset: 0x0000D788
		private static IServiceProvider GetContainer<T>(T message) where T : class
		{
			IContainerProvider containerProvider = message as IContainerProvider;
			if (containerProvider != null)
			{
				return containerProvider.Container;
			}
			return null;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0000F5AC File Offset: 0x0000D7AC
		private static IEdmModel GetModel(IServiceProvider container)
		{
			if (container != null)
			{
				return container.GetRequiredService<IEdmModel>();
			}
			return EdmCoreModel.Instance;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000F5CA File Offset: 0x0000D7CA
		private void SetOrVerifyHeaders(ODataPayloadKind payloadKind)
		{
			this.VerifyPayloadKind(payloadKind);
			if (this.writerPayloadKind == ODataPayloadKind.Unsupported)
			{
				this.SetHeaders(payloadKind);
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		private void EnsureODataVersion()
		{
			if (this.settings.Version == null)
			{
				this.settings.Version = new ODataVersion?(ODataUtilsInternal.GetODataVersion(this.message, ODataVersion.V4));
				if (string.IsNullOrEmpty(this.message.GetHeader("OData-Version")))
				{
					ODataUtilsInternal.SetODataVersion(this.message, this.settings);
					return;
				}
			}
			else
			{
				ODataUtilsInternal.SetODataVersion(this.message, this.settings);
			}
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000F660 File Offset: 0x0000D860
		private void EnsureODataFormatAndContentType()
		{
			string text = null;
			if (this.settings.UseFormat == null)
			{
				text = this.message.GetHeader("Content-Type");
				text = ((text == null) ? null : text.Trim());
			}
			if (!string.IsNullOrEmpty(text))
			{
				ODataPayloadKind odataPayloadKind;
				this.format = MediaTypeUtils.GetFormatFromContentType(text, new ODataPayloadKind[] { this.writerPayloadKind }, this.mediaTypeResolver, out this.mediaType, out this.encoding, out odataPayloadKind);
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
					this.message.SetHeader("Content-Type", text);
					return;
				}
			}
			else
			{
				this.format = MediaTypeUtils.GetContentTypeFromSettings(this.settings, this.writerPayloadKind, this.mediaTypeResolver, out this.mediaType, out this.encoding);
				IEnumerable<KeyValuePair<string, string>> enumerable;
				text = this.format.GetContentType(this.mediaType, this.encoding, this.writingResponse, out enumerable);
				if (this.mediaType.Parameters != enumerable)
				{
					this.mediaType = new ODataMediaType(this.mediaType.Type, this.mediaType.SubType, enumerable);
				}
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
				}
				this.message.SetHeader("Content-Type", text);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000F7A0 File Offset: 0x0000D9A0
		private void VerifyCanCreateODataAsyncWriter()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_AsyncInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0000F7BB File Offset: 0x0000D9BB
		private void VerifyCanCreateODataResourceSetWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0000F7C3 File Offset: 0x0000D9C3
		private void VerifyCanCreateODataDeltaWriter()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_DeltaInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0000F7BB File Offset: 0x0000D9BB
		private void VerifyCanCreateODataResourceWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0000F7DE File Offset: 0x0000D9DE
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		private void VerifyCanCreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			if (itemTypeReference != null && !itemTypeReference.IsPrimitive() && !itemTypeReference.IsComplex() && !itemTypeReference.IsEnum() && !itemTypeReference.IsTypeDefinition())
			{
				throw new ODataException(Strings.ODataMessageWriter_NonCollectionType(itemTypeReference.FullName()));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0000F7BB File Offset: 0x0000D9BB
		private void VerifyCanCreateODataBatchWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000F81A File Offset: 0x0000DA1A
		private void VerifyCanCreateODataParameterWriter(IEdmOperation operation)
		{
			if (this.writingResponse)
			{
				throw new ODataException(Strings.ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage);
			}
			if (operation != null && !this.model.IsUserModel())
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotSpecifyOperationWithoutModel);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000F850 File Offset: 0x0000DA50
		private void VerifyCanWriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataServiceDocument>(serviceDocument, "serviceDocument");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ServiceDocumentInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000F877 File Offset: 0x0000DA77
		private void VerifyCanWriteProperty(ODataProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			if (property.Value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(property.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0000F8A9 File Offset: 0x0000DAA9
		private void VerifyCanWriteTopLevelError(ODataError error)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataError>(error, "error");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ErrorPayloadInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
			this.writeErrorCalled = true;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
		private void VerifyCanWriteInStreamError(ODataError error)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataError>(error, "error");
			this.VerifyNotDisposed();
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ErrorPayloadInRequest);
			}
			if (this.writeErrorCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriteErrorAlreadyCalled);
			}
			this.writeErrorCalled = true;
			this.writeMethodCalled = true;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000F92B File Offset: 0x0000DB2B
		private void VerifyCanWriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "ref");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0000F952 File Offset: 0x0000DB52
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0000F966 File Offset: 0x0000DB66
		private ODataPayloadKind VerifyCanWriteValue(object value)
		{
			if (value == null)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteNullInRawFormat);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
			if (!(value is byte[]))
			{
				return ODataPayloadKind.Value;
			}
			return ODataPayloadKind.BinaryValue;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000F987 File Offset: 0x0000DB87
		private void VerifyCanWriteMetadataDocument()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_MetadataDocumentInRequest);
			}
			if (!this.model.IsUserModel())
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteMetadataWithoutModel);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000F9BA File Offset: 0x0000DBBA
		private void VerifyWriterNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.writeMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriterAlreadyUsed);
			}
			this.writeMethodCalled = true;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		private void Dispose(bool disposing)
		{
			this.isDisposed = true;
			if (disposing)
			{
				try
				{
					if (this.outputContext != null)
					{
						this.outputContext.Dispose();
					}
				}
				finally
				{
					this.outputContext = null;
				}
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		private void VerifyPayloadKind(ODataPayloadKind payloadKindToWrite)
		{
			if (this.writerPayloadKind != ODataPayloadKind.Unsupported && this.writerPayloadKind != payloadKindToWrite)
			{
				throw new ODataException(Strings.ODataMessageWriter_IncompatiblePayloadKinds(this.writerPayloadKind, payloadKindToWrite));
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0000FA70 File Offset: 0x0000DC70
		private ODataMessageInfo GetOrCreateMessageInfo(Stream messageStream, bool isAsync)
		{
			if (this.messageInfo == null)
			{
				if (this.container == null)
				{
					this.messageInfo = new ODataMessageInfo();
				}
				else
				{
					this.messageInfo = this.container.GetRequiredService<ODataMessageInfo>();
				}
				this.messageInfo.Encoding = this.encoding;
				this.messageInfo.IsResponse = this.writingResponse;
				this.messageInfo.IsAsync = isAsync;
				this.messageInfo.MediaType = this.mediaType;
				this.messageInfo.Model = this.model;
				this.messageInfo.PayloadUriConverter = this.payloadUriConverter;
				this.messageInfo.Container = this.container;
				this.messageInfo.MessageStream = messageStream;
			}
			return this.messageInfo;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000FB32 File Offset: 0x0000DD32
		private void WriteToOutput(ODataPayloadKind payloadKind, Action<ODataOutputContext> writeAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			this.outputContext = this.format.CreateOutputContext(this.GetOrCreateMessageInfo(this.message.GetStream(), false), this.settings);
			writeAction(this.outputContext);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0000FB70 File Offset: 0x0000DD70
		private TResult WriteToOutput<TResult>(ODataPayloadKind payloadKind, Func<ODataOutputContext, TResult> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			this.outputContext = this.format.CreateOutputContext(this.GetOrCreateMessageInfo(this.message.GetStream(), false), this.settings);
			return writeFunc(this.outputContext);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		private Task WriteToOutputAsync(ODataPayloadKind payloadKind, Func<ODataOutputContext, Task> writeAsyncAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			return this.message.GetStreamAsync().FollowOnSuccessWithTask((Task<Stream> streamTask) => this.format.CreateOutputContextAsync(this.GetOrCreateMessageInfo(streamTask.Result, true), this.settings)).FollowOnSuccessWithTask(delegate(Task<ODataOutputContext> createOutputContextTask)
			{
				this.outputContext = createOutputContextTask.Result;
				return writeAsyncAction(this.outputContext);
			});
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0000FC08 File Offset: 0x0000DE08
		private Task<TResult> WriteToOutputAsync<TResult>(ODataPayloadKind payloadKind, Func<ODataOutputContext, Task<TResult>> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			return this.message.GetStreamAsync().FollowOnSuccessWithTask((Task<Stream> streamTask) => this.format.CreateOutputContextAsync(this.GetOrCreateMessageInfo(streamTask.Result, true), this.settings)).FollowOnSuccessWithTask(delegate(Task<ODataOutputContext> createOutputContextTask)
			{
				this.outputContext = createOutputContextTask.Result;
				return writeFunc(this.outputContext);
			});
		}

		// Token: 0x0400026D RID: 621
		private readonly ODataMessage message;

		// Token: 0x0400026E RID: 622
		private readonly bool writingResponse;

		// Token: 0x0400026F RID: 623
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x04000270 RID: 624
		private readonly IEdmModel model;

		// Token: 0x04000271 RID: 625
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x04000272 RID: 626
		private readonly IServiceProvider container;

		// Token: 0x04000273 RID: 627
		private readonly ODataMediaTypeResolver mediaTypeResolver;

		// Token: 0x04000274 RID: 628
		private bool writeMethodCalled;

		// Token: 0x04000275 RID: 629
		private bool isDisposed;

		// Token: 0x04000276 RID: 630
		private ODataOutputContext outputContext;

		// Token: 0x04000277 RID: 631
		private ODataPayloadKind writerPayloadKind;

		// Token: 0x04000278 RID: 632
		private ODataFormat format;

		// Token: 0x04000279 RID: 633
		private Encoding encoding;

		// Token: 0x0400027A RID: 634
		private bool writeErrorCalled;

		// Token: 0x0400027B RID: 635
		private ODataMediaType mediaType;

		// Token: 0x0400027C RID: 636
		private ODataMessageInfo messageInfo;
	}
}
