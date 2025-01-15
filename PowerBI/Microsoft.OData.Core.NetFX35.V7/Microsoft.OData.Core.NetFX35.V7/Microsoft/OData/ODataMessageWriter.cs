using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000073 RID: 115
	public sealed class ODataMessageWriter : IDisposable
	{
		// Token: 0x0600040E RID: 1038 RVA: 0x0000BDE6 File Offset: 0x00009FE6
		public ODataMessageWriter(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000BDFC File Offset: 0x00009FFC
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

		// Token: 0x06000411 RID: 1041 RVA: 0x0000BEC5 File Offset: 0x0000A0C5
		public ODataMessageWriter(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000BECF File Offset: 0x0000A0CF
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000BEDC File Offset: 0x0000A0DC
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

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000BFAE File Offset: 0x0000A1AE
		internal ODataMessageWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000BFB6 File Offset: 0x0000A1B6
		public ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			this.VerifyCanCreateODataAsyncWriter();
			return this.WriteToOutput<ODataAsynchronousWriter>(ODataPayloadKind.Asynchronous, (ODataOutputContext context) => context.CreateODataAsynchronousWriter());
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BFE5 File Offset: 0x0000A1E5
		public ODataWriter CreateODataResourceSetWriter()
		{
			return this.CreateODataResourceSetWriter(null, null);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000BFEF File Offset: 0x0000A1EF
		public ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet)
		{
			return this.CreateODataResourceSetWriter(entitySet, null);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		public ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataResourceSetWriter(entitySet, resourceType));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C038 File Offset: 0x0000A238
		public ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataDeltaWriter();
			return this.WriteToOutput<ODataDeltaWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataDeltaWriter(entitySet, entityType));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C072 File Offset: 0x0000A272
		public ODataWriter CreateODataResourceWriter()
		{
			return this.CreateODataResourceWriter(null, null);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000C07C File Offset: 0x0000A27C
		public ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource)
		{
			return this.CreateODataResourceWriter(navigationSource, null);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000C088 File Offset: 0x0000A288
		public ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Resource, (ODataOutputContext context) => context.CreateODataResourceWriter(navigationSource, resourceType));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		public ODataWriter CreateODataUriParameterResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Resource, (ODataOutputContext context) => context.CreateODataUriParameterResourceWriter(navigationSource, resourceType));
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000C100 File Offset: 0x0000A300
		public ODataWriter CreateODataUriParameterResourceSetWriter(IEdmEntitySetBase entitySetBase, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceSetWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.ResourceSet, (ODataOutputContext context) => context.CreateODataUriParameterResourceSetWriter(entitySetBase, resourceType));
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000C13A File Offset: 0x0000A33A
		public ODataCollectionWriter CreateODataCollectionWriter()
		{
			return this.CreateODataCollectionWriter(null);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000C144 File Offset: 0x0000A344
		public ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutput<ODataCollectionWriter>(ODataPayloadKind.Collection, (ODataOutputContext context) => context.CreateODataCollectionWriter(itemTypeReference));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000C17D File Offset: 0x0000A37D
		public ODataBatchWriter CreateODataBatchWriter()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutput<ODataBatchWriter>(ODataPayloadKind.Batch, (ODataOutputContext context) => context.CreateODataBatchWriter(this.batchBoundary));
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000C19C File Offset: 0x0000A39C
		public ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterWriter(operation);
			return this.WriteToOutput<ODataParameterWriter>(ODataPayloadKind.Parameter, (ODataOutputContext context) => context.CreateODataParameterWriter(operation));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.VerifyCanWriteServiceDocument(serviceDocument);
			this.WriteToOutput(ODataPayloadKind.ServiceDocument, delegate(ODataOutputContext context)
			{
				context.WriteServiceDocument(serviceDocument);
			});
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000C214 File Offset: 0x0000A414
		public void WriteProperty(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			this.WriteToOutput(ODataPayloadKind.Property, delegate(ODataOutputContext context)
			{
				context.WriteProperty(property);
			});
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000C250 File Offset: 0x0000A450
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

		// Token: 0x06000426 RID: 1062 RVA: 0x0000C2C0 File Offset: 0x0000A4C0
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.VerifyCanWriteEntityReferenceLinks(links);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLinks, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links);
			});
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLink, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link);
			});
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000C338 File Offset: 0x0000A538
		public void WriteValue(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			this.WriteToOutput(odataPayloadKind, delegate(ODataOutputContext context)
			{
				context.WriteValue(value);
			});
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000C372 File Offset: 0x0000A572
		public void WriteMetadataDocument()
		{
			this.VerifyCanWriteMetadataDocument();
			this.WriteToOutput(ODataPayloadKind.MetadataDocument, delegate(ODataOutputContext context)
			{
				context.WriteMetadataDocument();
			});
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000C3A1 File Offset: 0x0000A5A1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000C3B0 File Offset: 0x0000A5B0
		internal ODataFormat SetHeaders(ODataPayloadKind payloadKind)
		{
			this.writerPayloadKind = payloadKind;
			this.EnsureODataVersion();
			this.EnsureODataFormatAndContentType();
			return this.format;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000B41B File Offset: 0x0000961B
		private static IServiceProvider GetContainer<T>(T message) where T : class
		{
			return null;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000B41E File Offset: 0x0000961E
		private static IEdmModel GetModel(IServiceProvider container)
		{
			return EdmCoreModel.Instance;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C3CB File Offset: 0x0000A5CB
		private void SetOrVerifyHeaders(ODataPayloadKind payloadKind)
		{
			this.VerifyPayloadKind(payloadKind);
			if (this.writerPayloadKind == ODataPayloadKind.Unsupported)
			{
				this.SetHeaders(payloadKind);
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C3EC File Offset: 0x0000A5EC
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

		// Token: 0x06000430 RID: 1072 RVA: 0x0000C464 File Offset: 0x0000A664
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
				this.format = MediaTypeUtils.GetFormatFromContentType(text, new ODataPayloadKind[] { this.writerPayloadKind }, this.mediaTypeResolver, out this.mediaType, out this.encoding, out odataPayloadKind, out this.batchBoundary);
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
				if (this.writerPayloadKind == ODataPayloadKind.Batch)
				{
					this.batchBoundary = ODataBatchWriterUtils.CreateBatchBoundary(this.writingResponse);
					text = ODataBatchWriterUtils.CreateMultipartMixedContentType(this.batchBoundary);
				}
				else
				{
					this.batchBoundary = null;
					text = HttpUtils.BuildContentType(this.mediaType, this.encoding);
				}
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
				}
				this.message.SetHeader("Content-Type", text);
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000C59C File Offset: 0x0000A79C
		private void VerifyCanCreateODataAsyncWriter()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_AsyncInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
		private void VerifyCanCreateODataResourceSetWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000C5BF File Offset: 0x0000A7BF
		private void VerifyCanCreateODataDeltaWriter()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_DeltaInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
		private void VerifyCanCreateODataResourceWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000C5DA File Offset: 0x0000A7DA
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		private void VerifyCanCreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			if (itemTypeReference != null && !itemTypeReference.IsPrimitive() && !itemTypeReference.IsComplex() && !itemTypeReference.IsEnum() && !itemTypeReference.IsTypeDefinition())
			{
				throw new ODataException(Strings.ODataMessageWriter_NonCollectionType(itemTypeReference.FullName()));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
		private void VerifyCanCreateODataBatchWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000C616 File Offset: 0x0000A816
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

		// Token: 0x06000438 RID: 1080 RVA: 0x0000C64C File Offset: 0x0000A84C
		private void VerifyCanWriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataServiceDocument>(serviceDocument, "serviceDocument");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ServiceDocumentInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000C673 File Offset: 0x0000A873
		private void VerifyCanWriteProperty(ODataProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			if (property.Value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(property.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000C6A5 File Offset: 0x0000A8A5
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

		// Token: 0x0600043B RID: 1083 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
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

		// Token: 0x0600043C RID: 1084 RVA: 0x0000C727 File Offset: 0x0000A927
		private void VerifyCanWriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "ref");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000C74E File Offset: 0x0000A94E
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000C762 File Offset: 0x0000A962
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

		// Token: 0x0600043F RID: 1087 RVA: 0x0000C783 File Offset: 0x0000A983
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

		// Token: 0x06000440 RID: 1088 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		private void VerifyWriterNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.writeMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriterAlreadyUsed);
			}
			this.writeMethodCalled = true;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
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

		// Token: 0x06000443 RID: 1091 RVA: 0x0000C838 File Offset: 0x0000AA38
		private void VerifyPayloadKind(ODataPayloadKind payloadKindToWrite)
		{
			if (this.writerPayloadKind != ODataPayloadKind.Unsupported && this.writerPayloadKind != payloadKindToWrite)
			{
				throw new ODataException(Strings.ODataMessageWriter_IncompatiblePayloadKinds(this.writerPayloadKind, payloadKindToWrite));
			}
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000C86C File Offset: 0x0000AA6C
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

		// Token: 0x06000445 RID: 1093 RVA: 0x0000C92E File Offset: 0x0000AB2E
		private void WriteToOutput(ODataPayloadKind payloadKind, Action<ODataOutputContext> writeAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			this.outputContext = this.format.CreateOutputContext(this.GetOrCreateMessageInfo(this.message.GetStream(), false), this.settings);
			writeAction.Invoke(this.outputContext);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000C96C File Offset: 0x0000AB6C
		private TResult WriteToOutput<TResult>(ODataPayloadKind payloadKind, Func<ODataOutputContext, TResult> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			this.outputContext = this.format.CreateOutputContext(this.GetOrCreateMessageInfo(this.message.GetStream(), false), this.settings);
			return writeFunc.Invoke(this.outputContext);
		}

		// Token: 0x04000209 RID: 521
		private readonly ODataMessage message;

		// Token: 0x0400020A RID: 522
		private readonly bool writingResponse;

		// Token: 0x0400020B RID: 523
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x0400020C RID: 524
		private readonly IEdmModel model;

		// Token: 0x0400020D RID: 525
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x0400020E RID: 526
		private readonly IServiceProvider container;

		// Token: 0x0400020F RID: 527
		private readonly ODataMediaTypeResolver mediaTypeResolver;

		// Token: 0x04000210 RID: 528
		private bool writeMethodCalled;

		// Token: 0x04000211 RID: 529
		private bool isDisposed;

		// Token: 0x04000212 RID: 530
		private ODataOutputContext outputContext;

		// Token: 0x04000213 RID: 531
		private ODataPayloadKind writerPayloadKind;

		// Token: 0x04000214 RID: 532
		private ODataFormat format;

		// Token: 0x04000215 RID: 533
		private Encoding encoding;

		// Token: 0x04000216 RID: 534
		private string batchBoundary;

		// Token: 0x04000217 RID: 535
		private bool writeErrorCalled;

		// Token: 0x04000218 RID: 536
		private ODataMediaType mediaType;

		// Token: 0x04000219 RID: 537
		private ODataMessageInfo messageInfo;
	}
}
