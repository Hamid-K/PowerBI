using System;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000289 RID: 649
	public sealed class ODataMessageWriter : IDisposable
	{
		// Token: 0x06001472 RID: 5234 RVA: 0x0004B61C File Offset: 0x0004981C
		public ODataMessageWriter(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0004B626 File Offset: 0x00049826
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0004B634 File Offset: 0x00049834
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings, IEdmModel model)
		{
			this.writerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.settings = ((settings == null) ? new ODataMessageWriterSettings() : new ODataMessageWriterSettings(settings));
			this.writingResponse = false;
			this.urlResolver = requestMessage as IODataUrlResolver;
			this.model = model ?? EdmCoreModel.Instance;
			WriterValidationUtils.ValidateMessageWriterSettings(this.settings, this.writingResponse);
			this.message = new ODataRequestMessage(requestMessage, true, this.settings.DisableMessageStreamDisposal, -1L);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0004B6C1 File Offset: 0x000498C1
		public ODataMessageWriter(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0004B6CB File Offset: 0x000498CB
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0004B6D8 File Offset: 0x000498D8
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings, IEdmModel model)
		{
			this.writerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.settings = ((settings == null) ? new ODataMessageWriterSettings() : new ODataMessageWriterSettings(settings));
			this.writingResponse = true;
			this.urlResolver = responseMessage as IODataUrlResolver;
			this.model = model ?? EdmCoreModel.Instance;
			WriterValidationUtils.ValidateMessageWriterSettings(this.settings, this.writingResponse);
			this.message = new ODataResponseMessage(responseMessage, true, this.settings.DisableMessageStreamDisposal, -1L);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (!string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0004B78A File Offset: 0x0004998A
		internal ODataMessageWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004B794 File Offset: 0x00049994
		private MediaTypeResolver MediaTypeResolver
		{
			get
			{
				if (this.mediaTypeResolver == null)
				{
					this.mediaTypeResolver = MediaTypeResolver.GetWriterMediaTypeResolver(this.settings.Version.Value);
				}
				return this.mediaTypeResolver;
			}
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0004B7CD File Offset: 0x000499CD
		public ODataWriter CreateODataFeedWriter()
		{
			return this.CreateODataFeedWriter(null, null);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0004B7D7 File Offset: 0x000499D7
		public ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet)
		{
			return this.CreateODataFeedWriter(entitySet, null);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0004B800 File Offset: 0x00049A00
		public ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataFeedWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Feed, null, (ODataOutputContext context) => context.CreateODataFeedWriter(entitySet, entityType));
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0004B83B File Offset: 0x00049A3B
		public ODataWriter CreateODataEntryWriter()
		{
			return this.CreateODataEntryWriter(null, null);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0004B845 File Offset: 0x00049A45
		public ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet)
		{
			return this.CreateODataEntryWriter(entitySet, null);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0004B86C File Offset: 0x00049A6C
		public ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Entry, null, (ODataOutputContext context) => context.CreateODataEntryWriter(entitySet, entityType));
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0004B8A7 File Offset: 0x00049AA7
		public ODataCollectionWriter CreateODataCollectionWriter()
		{
			return this.CreateODataCollectionWriter(null);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0004B8C8 File Offset: 0x00049AC8
		public ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutput<ODataCollectionWriter>(ODataPayloadKind.Collection, null, (ODataOutputContext context) => context.CreateODataCollectionWriter(itemTypeReference));
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0004B910 File Offset: 0x00049B10
		public ODataBatchWriter CreateODataBatchWriter()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutput<ODataBatchWriter>(ODataPayloadKind.Batch, null, (ODataOutputContext context) => context.CreateODataBatchWriter(this.batchBoundary));
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0004B944 File Offset: 0x00049B44
		public ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateODataParameterWriter(functionImport);
			return this.WriteToOutput<ODataParameterWriter>(ODataPayloadKind.Parameter, new Action(this.VerifyODataParameterWriterHeaders), (ODataOutputContext context) => context.CreateODataParameterWriter(functionImport));
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0004B9A0 File Offset: 0x00049BA0
		public void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.VerifyCanWriteServiceDocument(defaultWorkspace);
			this.WriteToOutput(ODataPayloadKind.ServiceDocument, null, delegate(ODataOutputContext context)
			{
				context.WriteServiceDocument(defaultWorkspace);
			});
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0004B9F0 File Offset: 0x00049BF0
		public void WriteProperty(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			this.WriteToOutput(ODataPayloadKind.Property, null, delegate(ODataOutputContext context)
			{
				context.WriteProperty(property);
			});
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0004BA48 File Offset: 0x00049C48
		public void WriteError(ODataError error, bool includeDebugInformation)
		{
			if (this.outputContext == null)
			{
				this.VerifyCanWriteTopLevelError(error);
				this.WriteToOutput(ODataPayloadKind.Error, null, delegate(ODataOutputContext context)
				{
					context.WriteError(error, includeDebugInformation);
				});
				return;
			}
			this.VerifyCanWriteInStreamError(error);
			this.outputContext.WriteInStreamError(error, includeDebugInformation);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0004BABD File Offset: 0x00049CBD
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.WriteEntityReferenceLinks(links, null, null);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0004BB00 File Offset: 0x00049D00
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanWriteEntityReferenceLinks(links, navigationProperty);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLinks, delegate
			{
				this.VerifyEntityReferenceLinksHeaders(links);
			}, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links, entitySet, navigationProperty);
			});
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0004BB60 File Offset: 0x00049D60
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.WriteEntityReferenceLink(link, null, null);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0004BB90 File Offset: 0x00049D90
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLink, null, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link, entitySet, navigationProperty);
			});
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		public void WriteValue(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			this.WriteToOutput(odataPayloadKind, null, delegate(ODataOutputContext context)
			{
				context.WriteValue(value);
			});
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0004BC33 File Offset: 0x00049E33
		public void WriteMetadataDocument()
		{
			this.VerifyCanWriteMetadataDocument();
			this.WriteToOutput(ODataPayloadKind.MetadataDocument, new Action(this.VerifyMetadataDocumentHeaders), delegate(ODataOutputContext context)
			{
				context.WriteMetadataDocument();
			});
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0004BC6C File Offset: 0x00049E6C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0004BC7B File Offset: 0x00049E7B
		internal ODataFormat SetHeaders(ODataPayloadKind payloadKind)
		{
			this.writerPayloadKind = payloadKind;
			this.EnsureODataVersion();
			this.EnsureODataFormatAndContentType();
			return this.format;
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0004BC96 File Offset: 0x00049E96
		private void SetOrVerifyHeaders(ODataPayloadKind payloadKind)
		{
			this.VerifyPayloadKind(payloadKind);
			if (this.writerPayloadKind == ODataPayloadKind.Unsupported)
			{
				this.SetHeaders(payloadKind);
			}
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0004BCB4 File Offset: 0x00049EB4
		private void EnsureODataVersion()
		{
			if (this.settings.Version == null)
			{
				this.settings.Version = new ODataVersion?(ODataUtilsInternal.GetDataServiceVersion(this.message, ODataVersion.V3));
			}
			else
			{
				ODataUtilsInternal.SetDataServiceVersion(this.message, this.settings);
			}
			if (this.settings.Version >= ODataVersion.V3 && this.settings.WriterBehavior.FormatBehaviorKind != ODataBehaviorKind.Default)
			{
				this.settings.WriterBehavior.UseDefaultFormatBehavior();
			}
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0004BD4C File Offset: 0x00049F4C
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
				this.format = MediaTypeUtils.GetFormatFromContentType(text, new ODataPayloadKind[] { this.writerPayloadKind }, this.MediaTypeResolver, out this.mediaType, out this.encoding, out odataPayloadKind, out this.batchBoundary);
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
					this.message.SetHeader("Content-Type", text);
					return;
				}
			}
			else
			{
				this.format = MediaTypeUtils.GetContentTypeFromSettings(this.settings, this.writerPayloadKind, this.MediaTypeResolver, out this.mediaType, out this.encoding);
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

		// Token: 0x06001492 RID: 5266 RVA: 0x0004BE86 File Offset: 0x0004A086
		private void VerifyCanCreateODataFeedWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0004BE8E File Offset: 0x0004A08E
		private void VerifyCanCreateODataEntryWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0004BE96 File Offset: 0x0004A096
		private void VerifyCanCreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			if (itemTypeReference != null && !itemTypeReference.IsPrimitive() && !itemTypeReference.IsComplex())
			{
				throw new ODataException(Strings.ODataMessageWriter_NonCollectionType(itemTypeReference.ODataFullName()));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0004BEC2 File Offset: 0x0004A0C2
		private void VerifyCanCreateODataBatchWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0004BECA File Offset: 0x0004A0CA
		private void VerifyCanCreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			if (this.writingResponse)
			{
				throw new ODataException(Strings.ODataParameterWriter_CannotCreateParameterWriterOnResponseMessage);
			}
			if (functionImport != null && !this.model.IsUserModel())
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotSpecifyFunctionImportWithoutModel);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0004BF00 File Offset: 0x0004A100
		private void VerifyODataParameterWriterHeaders()
		{
			ODataVersionChecker.CheckParameterPayload(this.settings.Version.Value);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0004BF25 File Offset: 0x0004A125
		private void VerifyCanWriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataWorkspace>(defaultWorkspace, "defaultWorkspace");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ServiceDocumentInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0004BF4B File Offset: 0x0004A14B
		private void VerifyCanWriteProperty(ODataProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			if (property.Value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(property.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0004BF7C File Offset: 0x0004A17C
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

		// Token: 0x0600149B RID: 5275 RVA: 0x0004BFAC File Offset: 0x0004A1AC
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

		// Token: 0x0600149C RID: 5276 RVA: 0x0004C000 File Offset: 0x0004A200
		private void VerifyCanWriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmNavigationProperty navigationProperty)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "links");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed);
			}
			if (navigationProperty != null && navigationProperty.Type != null && !navigationProperty.Type.IsCollection())
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksWithSingletonNavPropNotAllowed(navigationProperty.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0004C05C File Offset: 0x0004A25C
		private void VerifyEntityReferenceLinksHeaders(ODataEntityReferenceLinks links)
		{
			if (links.Count != null)
			{
				ODataVersionChecker.CheckCount(this.settings.Version.Value);
			}
			if (links.NextPageLink != null)
			{
				ODataVersionChecker.CheckNextLink(this.settings.Version.Value);
			}
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0004C0B7 File Offset: 0x0004A2B7
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0004C0CA File Offset: 0x0004A2CA
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

		// Token: 0x060014A0 RID: 5280 RVA: 0x0004C0EB File Offset: 0x0004A2EB
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

		// Token: 0x060014A1 RID: 5281 RVA: 0x0004C120 File Offset: 0x0004A320
		private void VerifyMetadataDocumentHeaders()
		{
			Version version = this.model.GetDataServiceVersion();
			if (version == null)
			{
				version = this.settings.Version.Value.ToDataServiceVersion();
				this.model.SetDataServiceVersion(version);
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0004C167 File Offset: 0x0004A367
		private void VerifyWriterNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.writeMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriterAlreadyUsed);
			}
			this.writeMethodCalled = true;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0004C189 File Offset: 0x0004A389
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0004C1A4 File Offset: 0x0004A3A4
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

		// Token: 0x060014A5 RID: 5285 RVA: 0x0004C1E8 File Offset: 0x0004A3E8
		private void VerifyPayloadKind(ODataPayloadKind payloadKindToWrite)
		{
			if (this.writerPayloadKind != ODataPayloadKind.Unsupported && this.writerPayloadKind != payloadKindToWrite)
			{
				throw new ODataException(Strings.ODataMessageWriter_IncompatiblePayloadKinds(this.writerPayloadKind, payloadKindToWrite));
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0004C21C File Offset: 0x0004A41C
		private void WriteToOutput(ODataPayloadKind payloadKind, Action verifyHeaders, Action<ODataOutputContext> writeAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders.Invoke();
			}
			this.outputContext = this.format.CreateOutputContext(this.message, this.mediaType, this.encoding, this.settings, this.writingResponse, this.model, this.urlResolver);
			writeAction.Invoke(this.outputContext);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0004C280 File Offset: 0x0004A480
		private TResult WriteToOutput<TResult>(ODataPayloadKind payloadKind, Action verifyHeaders, Func<ODataOutputContext, TResult> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders.Invoke();
			}
			this.outputContext = this.format.CreateOutputContext(this.message, this.mediaType, this.encoding, this.settings, this.writingResponse, this.model, this.urlResolver);
			return writeFunc.Invoke(this.outputContext);
		}

		// Token: 0x04000847 RID: 2119
		private readonly ODataMessage message;

		// Token: 0x04000848 RID: 2120
		private readonly bool writingResponse;

		// Token: 0x04000849 RID: 2121
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x0400084A RID: 2122
		private readonly IEdmModel model;

		// Token: 0x0400084B RID: 2123
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400084C RID: 2124
		private bool writeMethodCalled;

		// Token: 0x0400084D RID: 2125
		private bool isDisposed;

		// Token: 0x0400084E RID: 2126
		private ODataOutputContext outputContext;

		// Token: 0x0400084F RID: 2127
		private ODataPayloadKind writerPayloadKind;

		// Token: 0x04000850 RID: 2128
		private ODataFormat format;

		// Token: 0x04000851 RID: 2129
		private Encoding encoding;

		// Token: 0x04000852 RID: 2130
		private string batchBoundary;

		// Token: 0x04000853 RID: 2131
		private bool writeErrorCalled;

		// Token: 0x04000854 RID: 2132
		private MediaTypeResolver mediaTypeResolver;

		// Token: 0x04000855 RID: 2133
		private MediaType mediaType;
	}
}
