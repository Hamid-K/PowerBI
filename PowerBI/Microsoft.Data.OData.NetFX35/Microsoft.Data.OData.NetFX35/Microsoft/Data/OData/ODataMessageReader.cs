using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000250 RID: 592
	public sealed class ODataMessageReader : IDisposable
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x00044769 File Offset: 0x00042969
		public ODataMessageReader(IODataRequestMessage requestMessage)
			: this(requestMessage, new ODataMessageReaderSettings())
		{
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00044777 File Offset: 0x00042977
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00044784 File Offset: 0x00042984
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.settings = ((settings == null) ? new ODataMessageReaderSettings() : new ODataMessageReaderSettings(settings));
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, false);
			this.readingResponse = false;
			this.message = new ODataRequestMessage(requestMessage, false, this.settings.DisableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.urlResolver = requestMessage as IODataUrlResolver;
			this.version = ODataUtilsInternal.GetDataServiceVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? EdmCoreModel.Instance;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ReaderBehavior, this.version);
			ODataVersionChecker.CheckVersionSupported(this.version, this.settings);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00044869 File Offset: 0x00042A69
		public ODataMessageReader(IODataResponseMessage responseMessage)
			: this(responseMessage, new ODataMessageReaderSettings())
		{
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00044877 File Offset: 0x00042A77
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00044884 File Offset: 0x00042A84
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.settings = ((settings == null) ? new ODataMessageReaderSettings() : new ODataMessageReaderSettings(settings));
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, true);
			this.readingResponse = true;
			this.message = new ODataResponseMessage(responseMessage, false, this.settings.DisableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.urlResolver = responseMessage as IODataUrlResolver;
			this.version = ODataUtilsInternal.GetDataServiceVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? EdmCoreModel.Instance;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ReaderBehavior, this.version);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (this.settings.ShouldIncludeAnnotation == null && !string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
			ODataVersionChecker.CheckVersionSupported(this.version, this.settings);
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004499B File Offset: 0x00042B9B
		internal ODataMessageReaderSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x000449A3 File Offset: 0x00042BA3
		private MediaTypeResolver MediaTypeResolver
		{
			get
			{
				if (this.mediaTypeResolver == null)
				{
					this.mediaTypeResolver = MediaTypeResolver.CreateReaderMediaTypeResolver(this.version, this.settings.ReaderBehavior.FormatBehaviorKind);
				}
				return this.mediaTypeResolver;
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000449FC File Offset: 0x00042BFC
		public IEnumerable<ODataPayloadKindDetectionResult> DetectPayloadKind()
		{
			if (this.settings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionInServerMode);
			}
			IEnumerable<ODataPayloadKindDetectionResult> enumerable;
			if (this.TryGetSinglePayloadKindResultFromContentType(out enumerable))
			{
				return enumerable;
			}
			this.payloadKindDetectionFormatStates = new Dictionary<ODataFormat, object>(ReferenceEqualityComparer<ODataFormat>.Instance);
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			try
			{
				IEnumerable<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerable2 = Enumerable.GroupBy<ODataPayloadKindDetectionResult, ODataFormat>(enumerable, (ODataPayloadKindDetectionResult kvp) => kvp.Format);
				foreach (IGrouping<ODataFormat, ODataPayloadKindDetectionResult> grouping in enumerable2)
				{
					ODataPayloadKindDetectionInfo odataPayloadKindDetectionInfo = new ODataPayloadKindDetectionInfo(this.contentType, this.encoding, this.settings, this.model, Enumerable.Select<ODataPayloadKindDetectionResult, ODataPayloadKind>(grouping, (ODataPayloadKindDetectionResult pkg) => pkg.PayloadKind));
					IEnumerable<ODataPayloadKind> enumerable3 = (this.readingResponse ? grouping.Key.DetectPayloadKind((IODataResponseMessage)this.message, odataPayloadKindDetectionInfo) : grouping.Key.DetectPayloadKind((IODataRequestMessage)this.message, odataPayloadKindDetectionInfo));
					if (enumerable3 != null)
					{
						using (IEnumerator<ODataPayloadKind> enumerator2 = enumerable3.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								ODataPayloadKind kind = enumerator2.Current;
								if (Enumerable.Any<ODataPayloadKindDetectionResult>(enumerable, (ODataPayloadKindDetectionResult pk) => pk.PayloadKind == kind))
								{
									list.Add(new ODataPayloadKindDetectionResult(kind, grouping.Key));
								}
							}
						}
					}
					this.payloadKindDetectionFormatStates.Add(grouping.Key, odataPayloadKindDetectionInfo.PayloadKindDetectionFormatState);
				}
			}
			finally
			{
				this.message.UseBufferingReadStream = new bool?(false);
				this.message.BufferingReadStream.StopBuffering();
			}
			list.Sort(new Comparison<ODataPayloadKindDetectionResult>(this.ComparePayloadKindDetectionResult));
			return list;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00044C28 File Offset: 0x00042E28
		public ODataReader CreateODataFeedReader()
		{
			return this.CreateODataFeedReader(null, null);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00044C32 File Offset: 0x00042E32
		public ODataReader CreateODataFeedReader(IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateODataFeedReader(null, expectedBaseEntityType);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00044C58 File Offset: 0x00042E58
		public ODataReader CreateODataFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataFeedReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			Func<ODataInputContext, ODataReader> func = (ODataInputContext context) => context.CreateFeedReader(entitySet, expectedBaseEntityType);
			ODataPayloadKind[] array = new ODataPayloadKind[1];
			return this.ReadFromInput<ODataReader>(func, array);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00044CC6 File Offset: 0x00042EC6
		public ODataReader CreateODataEntryReader()
		{
			return this.CreateODataEntryReader(null, null);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00044CD0 File Offset: 0x00042ED0
		public ODataReader CreateODataEntryReader(IEdmEntityType entityType)
		{
			return this.CreateODataEntryReader(null, entityType);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00044CF8 File Offset: 0x00042EF8
		public ODataReader CreateODataEntryReader(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryReader(entitySet, entityType);
			entityType = entityType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateEntryReader(entitySet, entityType), new ODataPayloadKind[] { ODataPayloadKind.Entry });
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00044D6A File Offset: 0x00042F6A
		public ODataCollectionReader CreateODataCollectionReader()
		{
			return this.CreateODataCollectionReader(null);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00044D8C File Offset: 0x00042F8C
		public ODataCollectionReader CreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInput<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReader(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00044DE0 File Offset: 0x00042FE0
		public ODataBatchReader CreateODataBatchReader()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInput<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReader(this.batchBoundary), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00044E28 File Offset: 0x00043028
		public ODataParameterReader CreateODataParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateODataParameterReader(functionImport);
			return this.ReadFromInput<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReader(functionImport), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00044E78 File Offset: 0x00043078
		public ODataWorkspace ReadServiceDocument()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInput<ODataWorkspace>((ODataInputContext context) => context.ReadServiceDocument(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00044EBA File Offset: 0x000430BA
		public ODataProperty ReadProperty()
		{
			return this.ReadProperty(null);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00044EDC File Offset: 0x000430DC
		public ODataProperty ReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00044F44 File Offset: 0x00043144
		public ODataProperty ReadProperty(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00044F90 File Offset: 0x00043190
		public ODataError ReadError()
		{
			this.VerifyCanReadError();
			return this.ReadFromInput<ODataError>((ODataInputContext context) => context.ReadError(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00044FD3 File Offset: 0x000431D3
		public ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			return this.ReadEntityReferenceLinks(null);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00044FF4 File Offset: 0x000431F4
		public ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLinks(navigationProperty);
			return this.ReadFromInput<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinks(navigationProperty), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00045038 File Offset: 0x00043238
		public ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			return this.ReadEntityReferenceLink(null);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00045058 File Offset: 0x00043258
		public ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInput<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLink(navigationProperty), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000450B4 File Offset: 0x000432B4
		public object ReadValue(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInput<object>((ODataInputContext context) => context.ReadValue((IEdmPrimitiveTypeReference)expectedTypeReference), array);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000450F8 File Offset: 0x000432F8
		public IEdmModel ReadMetadataDocument()
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0004513B File Offset: 0x0004333B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004514A File Offset: 0x0004334A
		internal ODataFormat GetFormat()
		{
			if (this.format == null)
			{
				throw new ODataException(Strings.ODataMessageReader_GetFormatCalledBeforeReadingStarted);
			}
			return this.format;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00045168 File Offset: 0x00043368
		private void ProcessContentType(params ODataPayloadKind[] payloadKinds)
		{
			string contentTypeHeader = this.GetContentTypeHeader();
			this.format = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, payloadKinds, this.MediaTypeResolver, out this.contentType, out this.encoding, out this.readerPayloadKind, out this.batchBoundary);
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x000451A8 File Offset: 0x000433A8
		private string GetContentTypeHeader()
		{
			string text = this.message.GetHeader("Content-Type");
			text = ((text == null) ? null : text.Trim());
			if (string.IsNullOrEmpty(text))
			{
				throw new ODataContentTypeException(Strings.ODataMessageReader_NoneOrEmptyContentTypeHeader);
			}
			return text;
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x000451E8 File Offset: 0x000433E8
		private void VerifyCanCreateODataFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (expectedBaseEntityType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedBaseEntityType"), "expectedBaseEntityType");
				}
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00045238 File Offset: 0x00043438
		private void VerifyCanCreateODataEntryReader(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (entityType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("entityType"), "entityType");
				}
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00045288 File Offset: 0x00043488
		private void VerifyCanCreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedItemTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedItemTypeReference"), "expectedItemTypeReference");
				}
				if (!expectedItemTypeReference.IsODataPrimitiveTypeKind() && expectedItemTypeReference.TypeKind() != EdmTypeKind.Complex)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedCollectionTypeWrongKind(expectedItemTypeReference.TypeKind().ToString()), "expectedItemTypeReference");
				}
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x000452F1 File Offset: 0x000434F1
		private void VerifyCanCreateODataBatchReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000452FC File Offset: 0x000434FC
		private void VerifyCanCreateODataParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ParameterPayloadInResponse);
			}
			ODataVersionChecker.CheckParameterPayload(this.version);
			if (functionImport != null && !this.model.IsUserModel())
			{
				throw new ArgumentException(Strings.ODataMessageReader_FunctionImportSpecifiedWithoutMetadata("functionImport"), "functionImport");
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00045352 File Offset: 0x00043552
		private void VerifyCanReadServiceDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ServiceDocumentInRequest);
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0004536D File Offset: 0x0004356D
		private void VerifyCanReadMetadataDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_MetadataDocumentInRequest);
			}
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00045388 File Offset: 0x00043588
		private void VerifyCanReadProperty(IEdmStructuralProperty property)
		{
			if (property == null)
			{
				return;
			}
			this.VerifyCanReadProperty(property.Type);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0004539C File Offset: 0x0004359C
		private void VerifyCanReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedPropertyTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedPropertyTypeReference"), "expectedPropertyTypeReference");
				}
				IEdmCollectionType edmCollectionType = expectedPropertyTypeReference.Definition as IEdmCollectionType;
				if (edmCollectionType != null && edmCollectionType.ElementType.IsODataEntityTypeKind())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeEntityCollectionKind, "expectedPropertyTypeReference");
				}
				if (expectedPropertyTypeReference.IsODataEntityTypeKind())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeEntityKind, "expectedPropertyTypeReference");
				}
				if (expectedPropertyTypeReference.IsStream())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedPropertyTypeStream, "expectedPropertyTypeReference");
				}
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00045430 File Offset: 0x00043630
		private void VerifyCanReadError()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ErrorPayloadInRequest);
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0004544C File Offset: 0x0004364C
		private void VerifyCanReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_EntityReferenceLinksInRequestNotAllowed);
			}
			if (navigationProperty != null && !navigationProperty.Type.IsCollection())
			{
				throw new ODataException(Strings.ODataMessageReader_SingletonNavigationPropertyForEntityReferenceLinks(navigationProperty.Name, navigationProperty.DeclaringEntityType().FullName()));
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0004549E File Offset: 0x0004369E
		private void VerifyCanReadEntityReferenceLink()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000454A8 File Offset: 0x000436A8
		private ODataPayloadKind[] VerifyCanReadValue(IEdmTypeReference expectedTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedTypeReference == null)
			{
				return new ODataPayloadKind[]
				{
					ODataPayloadKind.Value,
					ODataPayloadKind.BinaryValue
				};
			}
			if (!expectedTypeReference.IsODataPrimitiveTypeKind())
			{
				throw new ArgumentException(Strings.ODataMessageReader_ExpectedValueTypeWrongKind(expectedTypeReference.TypeKind().ToString()), "expectedTypeReference");
			}
			if (expectedTypeReference.IsBinary())
			{
				return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.Value };
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00045518 File Offset: 0x00043718
		private void VerifyReaderNotDisposedAndNotUsed()
		{
			this.VerifyNotDisposed();
			if (this.readMethodCalled)
			{
				throw new ODataException(Strings.ODataMessageReader_ReaderAlreadyUsed);
			}
			if (this.message.BufferingReadStream != null && this.message.BufferingReadStream.IsBuffering)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionRunning);
			}
			this.readMethodCalled = true;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0004556F File Offset: 0x0004376F
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0004558C File Offset: 0x0004378C
		private void Dispose(bool disposing)
		{
			this.isDisposed = true;
			if (disposing)
			{
				try
				{
					if (this.inputContext != null)
					{
						this.inputContext.Dispose();
					}
				}
				finally
				{
					this.inputContext = null;
				}
				if (!this.settings.DisableMessageStreamDisposal && this.message.BufferingReadStream != null)
				{
					this.message.BufferingReadStream.Dispose();
				}
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x000455FC File Offset: 0x000437FC
		private T ReadFromInput<T>(Func<ODataInputContext, T> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			object obj = null;
			if (this.payloadKindDetectionFormatStates != null)
			{
				this.payloadKindDetectionFormatStates.TryGetValue(this.format, ref obj);
			}
			this.inputContext = this.format.CreateInputContext(this.readerPayloadKind, this.message, this.contentType, this.encoding, this.settings, this.version, this.readingResponse, this.model, this.urlResolver, obj);
			return readFunc.Invoke(this.inputContext);
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00045698 File Offset: 0x00043898
		private bool TryGetSinglePayloadKindResultFromContentType(out IEnumerable<ODataPayloadKindDetectionResult> payloadKindResults)
		{
			if (this.message.UseBufferingReadStream == true)
			{
				throw new ODataException(Strings.ODataMessageReader_DetectPayloadKindMultipleTimes);
			}
			string contentTypeHeader = this.GetContentTypeHeader();
			IList<ODataPayloadKindDetectionResult> payloadKindsForContentType = MediaTypeUtils.GetPayloadKindsForContentType(contentTypeHeader, this.MediaTypeResolver, out this.contentType, out this.encoding);
			payloadKindResults = Enumerable.Where<ODataPayloadKindDetectionResult>(payloadKindsForContentType, (ODataPayloadKindDetectionResult r) => ODataUtilsInternal.IsPayloadKindSupported(r.PayloadKind, !this.readingResponse));
			if (Enumerable.Count<ODataPayloadKindDetectionResult>(payloadKindResults) > 1)
			{
				this.message.UseBufferingReadStream = new bool?(true);
				return false;
			}
			return true;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00045724 File Offset: 0x00043924
		private int ComparePayloadKindDetectionResult(ODataPayloadKindDetectionResult first, ODataPayloadKindDetectionResult second)
		{
			ODataPayloadKind payloadKind = first.PayloadKind;
			ODataPayloadKind payloadKind2 = second.PayloadKind;
			if (payloadKind == payloadKind2)
			{
				return 0;
			}
			if (first.PayloadKind >= second.PayloadKind)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x040006D6 RID: 1750
		private readonly ODataMessage message;

		// Token: 0x040006D7 RID: 1751
		private readonly bool readingResponse;

		// Token: 0x040006D8 RID: 1752
		private readonly ODataMessageReaderSettings settings;

		// Token: 0x040006D9 RID: 1753
		private readonly IEdmModel model;

		// Token: 0x040006DA RID: 1754
		private readonly ODataVersion version;

		// Token: 0x040006DB RID: 1755
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x040006DC RID: 1756
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040006DD RID: 1757
		private bool readMethodCalled;

		// Token: 0x040006DE RID: 1758
		private bool isDisposed;

		// Token: 0x040006DF RID: 1759
		private ODataInputContext inputContext;

		// Token: 0x040006E0 RID: 1760
		private ODataPayloadKind readerPayloadKind;

		// Token: 0x040006E1 RID: 1761
		private ODataFormat format;

		// Token: 0x040006E2 RID: 1762
		private MediaType contentType;

		// Token: 0x040006E3 RID: 1763
		private Encoding encoding;

		// Token: 0x040006E4 RID: 1764
		private string batchBoundary;

		// Token: 0x040006E5 RID: 1765
		private MediaTypeResolver mediaTypeResolver;

		// Token: 0x040006E6 RID: 1766
		private Dictionary<ODataFormat, object> payloadKindDetectionFormatStates;
	}
}
