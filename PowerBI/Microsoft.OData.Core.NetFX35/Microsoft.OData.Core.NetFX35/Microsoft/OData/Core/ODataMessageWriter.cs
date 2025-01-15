using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x02000183 RID: 387
	public sealed class ODataMessageWriter : IDisposable
	{
		// Token: 0x06000E6F RID: 3695 RVA: 0x000330E7 File Offset: 0x000312E7
		public ODataMessageWriter(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000330F1 File Offset: 0x000312F1
		public ODataMessageWriter(IODataRequestMessage requestMessage, ODataMessageWriterSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000330FC File Offset: 0x000312FC
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
			this.settings.ShouldIncludeAnnotation = new Func<string, bool>(AnnotationFilter.CreateInclueAllFilter().Matches);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000331A5 File Offset: 0x000313A5
		public ODataMessageWriter(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000331AF File Offset: 0x000313AF
		public ODataMessageWriter(IODataResponseMessage responseMessage, ODataMessageWriterSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000331BC File Offset: 0x000313BC
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

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003326E File Offset: 0x0003146E
		internal ODataMessageWriterSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003327E File Offset: 0x0003147E
		public ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			this.VerifyCanCreateODataAsyncWriter();
			return this.WriteToOutput<ODataAsynchronousWriter>(ODataPayloadKind.Asynchronous, null, (ODataOutputContext context) => context.CreateODataAsynchronousWriter());
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000332AC File Offset: 0x000314AC
		public ODataWriter CreateODataFeedWriter()
		{
			return this.CreateODataFeedWriter(null, null);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000332B6 File Offset: 0x000314B6
		public ODataWriter CreateODataFeedWriter(IEdmEntitySetBase entitySet)
		{
			return this.CreateODataFeedWriter(entitySet, null);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000332DC File Offset: 0x000314DC
		public ODataWriter CreateODataFeedWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataFeedWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Feed, null, (ODataOutputContext context) => context.CreateODataFeedWriter(entitySet, entityType));
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00033334 File Offset: 0x00031534
		public ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataDeltaWriter();
			return this.WriteToOutput<ODataDeltaWriter>(ODataPayloadKind.Feed, null, (ODataOutputContext context) => context.CreateODataDeltaWriter(entitySet, entityType));
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003336F File Offset: 0x0003156F
		public ODataWriter CreateODataEntryWriter()
		{
			return this.CreateODataEntryWriter(null, null);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00033379 File Offset: 0x00031579
		public ODataWriter CreateODataEntryWriter(IEdmNavigationSource navigationSource)
		{
			return this.CreateODataEntryWriter(navigationSource, null);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000333A0 File Offset: 0x000315A0
		public ODataWriter CreateODataEntryWriter(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryWriter();
			return this.WriteToOutput<ODataWriter>(ODataPayloadKind.Entry, null, (ODataOutputContext context) => context.CreateODataEntryWriter(navigationSource, entityType));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000333DB File Offset: 0x000315DB
		public ODataCollectionWriter CreateODataCollectionWriter()
		{
			return this.CreateODataCollectionWriter(null);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000333FC File Offset: 0x000315FC
		public ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			this.VerifyCanCreateODataCollectionWriter(itemTypeReference);
			return this.WriteToOutput<ODataCollectionWriter>(ODataPayloadKind.Collection, null, (ODataOutputContext context) => context.CreateODataCollectionWriter(itemTypeReference));
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00033444 File Offset: 0x00031644
		public ODataBatchWriter CreateODataBatchWriter()
		{
			this.VerifyCanCreateODataBatchWriter();
			return this.WriteToOutput<ODataBatchWriter>(ODataPayloadKind.Batch, null, (ODataOutputContext context) => context.CreateODataBatchWriter(this.batchBoundary));
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00033478 File Offset: 0x00031678
		public ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterWriter(operation);
			return this.WriteToOutput<ODataParameterWriter>(ODataPayloadKind.Parameter, null, (ODataOutputContext context) => context.CreateODataParameterWriter(operation));
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000334CC File Offset: 0x000316CC
		public void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.VerifyCanWriteServiceDocument(serviceDocument);
			this.WriteToOutput(ODataPayloadKind.ServiceDocument, null, delegate(ODataOutputContext context)
			{
				context.WriteServiceDocument(serviceDocument);
			});
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003351C File Offset: 0x0003171C
		public void WriteProperty(ODataProperty property)
		{
			this.VerifyCanWriteProperty(property);
			this.WriteToOutput(ODataPayloadKind.Property, null, delegate(ODataOutputContext context)
			{
				context.WriteProperty(property);
			});
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00033574 File Offset: 0x00031774
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

		// Token: 0x06000E85 RID: 3717 RVA: 0x00033600 File Offset: 0x00031800
		public void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.VerifyCanWriteEntityReferenceLinks(links);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLinks, null, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLinks(links);
			});
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00033650 File Offset: 0x00031850
		public void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.VerifyCanWriteEntityReferenceLink(link);
			this.WriteToOutput(ODataPayloadKind.EntityReferenceLink, null, delegate(ODataOutputContext context)
			{
				context.WriteEntityReferenceLink(link);
			});
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000336A0 File Offset: 0x000318A0
		public void WriteValue(object value)
		{
			ODataPayloadKind odataPayloadKind = this.VerifyCanWriteValue(value);
			this.WriteToOutput(odataPayloadKind, null, delegate(ODataOutputContext context)
			{
				context.WriteValue(value);
			});
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000336E3 File Offset: 0x000318E3
		public void WriteMetadataDocument()
		{
			this.VerifyCanWriteMetadataDocument();
			this.WriteToOutput(ODataPayloadKind.MetadataDocument, null, delegate(ODataOutputContext context)
			{
				context.WriteMetadataDocument();
			});
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00033711 File Offset: 0x00031911
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00033720 File Offset: 0x00031920
		internal ODataFormat SetHeaders(ODataPayloadKind payloadKind)
		{
			this.writerPayloadKind = payloadKind;
			this.EnsureODataVersion();
			this.EnsureODataFormatAndContentType();
			return this.format;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003373B File Offset: 0x0003193B
		private void SetOrVerifyHeaders(ODataPayloadKind payloadKind)
		{
			this.VerifyPayloadKind(payloadKind);
			if (this.writerPayloadKind == ODataPayloadKind.Unsupported)
			{
				this.SetHeaders(payloadKind);
			}
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003375C File Offset: 0x0003195C
		private void EnsureODataVersion()
		{
			if (this.settings.Version == null)
			{
				this.settings.Version = new ODataVersion?(ODataUtilsInternal.GetODataVersion(this.message, ODataVersion.V4));
				if (string.IsNullOrEmpty(this.message.GetHeader("OData-Version")))
				{
					ODataUtilsInternal.SetODataVersion(this.message, this.settings);
				}
			}
			else
			{
				ODataUtilsInternal.SetODataVersion(this.message, this.settings);
			}
			if (this.settings.WriterBehavior.FormatBehaviorKind != ODataBehaviorKind.Default)
			{
				this.settings.WriterBehavior.UseDefaultFormatBehavior();
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000337F8 File Offset: 0x000319F8
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
				this.format = MediaTypeUtils.GetFormatFromContentType(text, new ODataPayloadKind[] { this.writerPayloadKind }, this.settings.MediaTypeResolver, out this.mediaType, out this.encoding, out odataPayloadKind, out this.batchBoundary);
				if (this.settings.HasJsonPaddingFunction())
				{
					text = MediaTypeUtils.AlterContentTypeForJsonPadding(text);
					this.message.SetHeader("Content-Type", text);
					return;
				}
			}
			else
			{
				this.format = MediaTypeUtils.GetContentTypeFromSettings(this.settings, this.writerPayloadKind, this.settings.MediaTypeResolver, out this.mediaType, out this.encoding);
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

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003393C File Offset: 0x00031B3C
		private void VerifyCanCreateODataAsyncWriter()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_AsyncInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00033957 File Offset: 0x00031B57
		private void VerifyCanCreateODataFeedWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003395F File Offset: 0x00031B5F
		private void VerifyCanCreateODataDeltaWriter()
		{
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_DeltaInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003397A File Offset: 0x00031B7A
		private void VerifyCanCreateODataEntryWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00033982 File Offset: 0x00031B82
		[SuppressMessage("Microsoft.Naming", "CA2204:LiteralsShouldBeSpelledCorrectly", Justification = "Names are correct. String can't be localized after string freeze.")]
		private void VerifyCanCreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			if (itemTypeReference != null && !itemTypeReference.IsPrimitive() && !itemTypeReference.IsComplex() && !itemTypeReference.IsEnum() && !itemTypeReference.IsTypeDefinition())
			{
				throw new ODataException(Strings.ODataMessageWriter_NonCollectionType(itemTypeReference.FullName()));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000339BE File Offset: 0x00031BBE
		private void VerifyCanCreateODataBatchWriter()
		{
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000339C6 File Offset: 0x00031BC6
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

		// Token: 0x06000E95 RID: 3733 RVA: 0x000339FC File Offset: 0x00031BFC
		private void VerifyCanWriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataServiceDocument>(serviceDocument, "serviceDocument");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_ServiceDocumentInRequest);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00033A22 File Offset: 0x00031C22
		private void VerifyCanWriteProperty(ODataProperty property)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataProperty>(property, "property");
			if (property.Value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataMessageWriter_CannotWriteStreamPropertyAsTopLevelProperty(property.Name));
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00033A53 File Offset: 0x00031C53
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

		// Token: 0x06000E98 RID: 3736 RVA: 0x00033A80 File Offset: 0x00031C80
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

		// Token: 0x06000E99 RID: 3737 RVA: 0x00033AD2 File Offset: 0x00031CD2
		private void VerifyCanWriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLinks>(links, "ref");
			if (!this.writingResponse)
			{
				throw new ODataException(Strings.ODataMessageWriter_EntityReferenceLinksInRequestNotAllowed);
			}
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00033AF8 File Offset: 0x00031CF8
		private void VerifyCanWriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntityReferenceLink>(link, "link");
			this.VerifyWriterNotDisposedAndNotUsed();
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00033B0B File Offset: 0x00031D0B
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

		// Token: 0x06000E9C RID: 3740 RVA: 0x00033B2C File Offset: 0x00031D2C
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

		// Token: 0x06000E9D RID: 3741 RVA: 0x00033B5F File Offset: 0x00031D5F
		private void VerifyWriterNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.writeMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageWriter_WriterAlreadyUsed);
			}
			this.writeMethodCalled = true;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00033B81 File Offset: 0x00031D81
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00033B9C File Offset: 0x00031D9C
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

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00033BE0 File Offset: 0x00031DE0
		private void VerifyPayloadKind(ODataPayloadKind payloadKindToWrite)
		{
			if (this.writerPayloadKind != ODataPayloadKind.Unsupported && this.writerPayloadKind != payloadKindToWrite)
			{
				throw new ODataException(Strings.ODataMessageWriter_IncompatiblePayloadKinds(this.writerPayloadKind, payloadKindToWrite));
			}
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00033C14 File Offset: 0x00031E14
		private void WriteToOutput(ODataPayloadKind payloadKind, Action verifyHeaders, Action<ODataOutputContext> writeAction)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders.Invoke();
			}
			this.outputContext = this.format.CreateOutputContext(new ODataMessageInfo
			{
				Encoding = this.encoding,
				GetMessageStream = new Func<Stream>(this.message.GetStream),
				IsResponse = this.writingResponse,
				MediaType = this.mediaType,
				Model = this.model,
				UrlResolver = this.urlResolver
			}, this.settings);
			writeAction.Invoke(this.outputContext);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00033CB0 File Offset: 0x00031EB0
		private TResult WriteToOutput<TResult>(ODataPayloadKind payloadKind, Action verifyHeaders, Func<ODataOutputContext, TResult> writeFunc)
		{
			this.SetOrVerifyHeaders(payloadKind);
			if (verifyHeaders != null)
			{
				verifyHeaders.Invoke();
			}
			this.outputContext = this.format.CreateOutputContext(new ODataMessageInfo
			{
				Encoding = this.encoding,
				GetMessageStream = new Func<Stream>(this.message.GetStream),
				IsResponse = this.writingResponse,
				MediaType = this.mediaType,
				Model = this.model,
				UrlResolver = this.urlResolver
			}, this.settings);
			return writeFunc.Invoke(this.outputContext);
		}

		// Token: 0x04000639 RID: 1593
		private readonly ODataMessage message;

		// Token: 0x0400063A RID: 1594
		private readonly bool writingResponse;

		// Token: 0x0400063B RID: 1595
		private readonly ODataMessageWriterSettings settings;

		// Token: 0x0400063C RID: 1596
		private readonly IEdmModel model;

		// Token: 0x0400063D RID: 1597
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400063E RID: 1598
		private bool writeMethodCalled;

		// Token: 0x0400063F RID: 1599
		private bool isDisposed;

		// Token: 0x04000640 RID: 1600
		private ODataOutputContext outputContext;

		// Token: 0x04000641 RID: 1601
		private ODataPayloadKind writerPayloadKind;

		// Token: 0x04000642 RID: 1602
		private ODataFormat format;

		// Token: 0x04000643 RID: 1603
		private Encoding encoding;

		// Token: 0x04000644 RID: 1604
		private string batchBoundary;

		// Token: 0x04000645 RID: 1605
		private bool writeErrorCalled;

		// Token: 0x04000646 RID: 1606
		private ODataMediaType mediaType;
	}
}
