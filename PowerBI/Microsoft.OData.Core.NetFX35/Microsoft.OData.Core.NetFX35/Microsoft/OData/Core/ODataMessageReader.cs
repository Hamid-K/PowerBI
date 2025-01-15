using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x02000180 RID: 384
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Main entry point for reader functionality")]
	public sealed class ODataMessageReader : IDisposable
	{
		// Token: 0x06000E04 RID: 3588 RVA: 0x00031D02 File Offset: 0x0002FF02
		public ODataMessageReader(IODataRequestMessage requestMessage)
			: this(requestMessage, new ODataMessageReaderSettings())
		{
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00031D10 File Offset: 0x0002FF10
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x00031D1C File Offset: 0x0002FF1C
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
			ODataUtilsInternal.GetODataVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? EdmCoreModel.Instance;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ReaderBehavior);
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00031DE5 File Offset: 0x0002FFE5
		public ODataMessageReader(IODataResponseMessage responseMessage)
			: this(responseMessage, new ODataMessageReaderSettings())
		{
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x00031DF3 File Offset: 0x0002FFF3
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x00031E00 File Offset: 0x00030000
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
			ODataUtilsInternal.GetODataVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? EdmCoreModel.Instance;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ReaderBehavior);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (this.settings.ShouldIncludeAnnotation == null && !string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00031EFB File Offset: 0x000300FB
		internal ODataMessageReaderSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00031F24 File Offset: 0x00030124
		public IEnumerable<ODataPayloadKindDetectionResult> DetectPayloadKind()
		{
			if (this.settings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.ODataServer)
			{
				throw new ODataException(Strings.ODataMessageReader_PayloadKindDetectionInServerMode);
			}
			IEnumerable<ODataPayloadKindDetectionResult> enumerable;
			if (this.TryGetSinglePayloadKindResultFromContentType(out enumerable))
			{
				return enumerable;
			}
			List<ODataPayloadKindDetectionResult> list = new List<ODataPayloadKindDetectionResult>();
			try
			{
				IEnumerable<IGrouping<ODataFormat, ODataPayloadKindDetectionResult>> enumerable2 = Enumerable.GroupBy<ODataPayloadKindDetectionResult, ODataFormat>(enumerable, (ODataPayloadKindDetectionResult kvp) => kvp.Format);
				foreach (IGrouping<ODataFormat, ODataPayloadKindDetectionResult> grouping in enumerable2)
				{
					ODataMessageInfo odataMessageInfo = new ODataMessageInfo
					{
						Encoding = this.encoding,
						GetMessageStream = new Func<Stream>(this.message.GetStream),
						IsResponse = this.readingResponse,
						MediaType = this.contentType,
						Model = this.model
					};
					IEnumerable<ODataPayloadKind> enumerable3 = grouping.Key.DetectPayloadKind(odataMessageInfo, this.settings);
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

		// Token: 0x06000E0C RID: 3596 RVA: 0x00032120 File Offset: 0x00030320
		public ODataAsynchronousReader CreateODataAsynchronousReader()
		{
			this.VerifyCanCreateODataAsynchronousReader();
			return this.ReadFromInput<ODataAsynchronousReader>((ODataInputContext context) => context.CreateAsynchronousReader(), new ODataPayloadKind[] { ODataPayloadKind.Asynchronous });
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00032163 File Offset: 0x00030363
		public ODataReader CreateODataFeedReader()
		{
			return this.CreateODataFeedReader(null, null);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0003216D File Offset: 0x0003036D
		public ODataReader CreateODataFeedReader(IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateODataFeedReader(null, expectedBaseEntityType);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00032194 File Offset: 0x00030394
		public ODataReader CreateODataFeedReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataFeedReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			Func<ODataInputContext, ODataReader> func = (ODataInputContext context) => context.CreateFeedReader(entitySet, expectedBaseEntityType);
			ODataPayloadKind[] array = new ODataPayloadKind[1];
			return this.ReadFromInput<ODataReader>(func, array);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x00032220 File Offset: 0x00030420
		public ODataDeltaReader CreateODataDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataDeltaReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			Func<ODataInputContext, ODataDeltaReader> func = (ODataInputContext context) => context.CreateDeltaReader(entitySet, expectedBaseEntityType);
			ODataPayloadKind[] array = new ODataPayloadKind[1];
			return this.ReadFromInput<ODataDeltaReader>(func, array);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0003228E File Offset: 0x0003048E
		public ODataReader CreateODataEntryReader()
		{
			return this.CreateODataEntryReader(null, null);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x00032298 File Offset: 0x00030498
		public ODataReader CreateODataEntryReader(IEdmEntityType entityType)
		{
			return this.CreateODataEntryReader(null, entityType);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000322C0 File Offset: 0x000304C0
		public ODataReader CreateODataEntryReader(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			this.VerifyCanCreateODataEntryReader(navigationSource, entityType);
			entityType = entityType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateEntryReader(navigationSource, entityType), new ODataPayloadKind[] { ODataPayloadKind.Entry });
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00032332 File Offset: 0x00030532
		public ODataCollectionReader CreateODataCollectionReader()
		{
			return this.CreateODataCollectionReader(null);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00032354 File Offset: 0x00030554
		public ODataCollectionReader CreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInput<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReader(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000323A8 File Offset: 0x000305A8
		public ODataBatchReader CreateODataBatchReader()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInput<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReader(this.batchBoundary), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000323F0 File Offset: 0x000305F0
		public ODataParameterReader CreateODataParameterReader(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterReader(operation);
			return this.ReadFromInput<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReader(operation), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00032440 File Offset: 0x00030640
		public ODataServiceDocument ReadServiceDocument()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInput<ODataServiceDocument>((ODataInputContext context) => context.ReadServiceDocument(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00032482 File Offset: 0x00030682
		public ODataProperty ReadProperty()
		{
			return this.ReadProperty(null);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000324A4 File Offset: 0x000306A4
		public ODataProperty ReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0003250C File Offset: 0x0003070C
		public ODataProperty ReadProperty(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00032558 File Offset: 0x00030758
		public ODataError ReadError()
		{
			this.VerifyCanReadError();
			return this.ReadFromInput<ODataError>((ODataInputContext context) => context.ReadError(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000325A4 File Offset: 0x000307A4
		public ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			this.VerifyCanReadEntityReferenceLinks();
			return this.ReadFromInput<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinks(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000325F0 File Offset: 0x000307F0
		public ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInput<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLink(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00032650 File Offset: 0x00030850
		public object ReadValue(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInput<object>((ODataInputContext context) => context.ReadValue(expectedTypeReference.AsPrimitiveOrNull()), array);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00032694 File Offset: 0x00030894
		public IEdmModel ReadMetadataDocument()
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(null), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000326F0 File Offset: 0x000308F0
		public IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(getReferencedModelReaderFunc), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0003272F File Offset: 0x0003092F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0003273E File Offset: 0x0003093E
		internal ODataFormat GetFormat()
		{
			if (this.format == null)
			{
				throw new ODataException(Strings.ODataMessageReader_GetFormatCalledBeforeReadingStarted);
			}
			return this.format;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0003275C File Offset: 0x0003095C
		private void ProcessContentType(params ODataPayloadKind[] payloadKinds)
		{
			string contentTypeHeader = this.GetContentTypeHeader(payloadKinds);
			this.format = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, payloadKinds, this.settings.MediaTypeResolver, out this.contentType, out this.encoding, out this.readerPayloadKind, out this.batchBoundary);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000327A4 File Offset: 0x000309A4
		private string GetContentTypeHeader(params ODataPayloadKind[] payloadKinds)
		{
			string text = this.message.GetHeader("Content-Type");
			text = ((text == null) ? null : text.Trim());
			if (string.IsNullOrEmpty(text))
			{
				if (this.GetContentLengthHeader() != 0)
				{
					throw new ODataContentTypeException(Strings.ODataMessageReader_NoneOrEmptyContentTypeHeader);
				}
				if (Enumerable.Contains<ODataPayloadKind>(payloadKinds, ODataPayloadKind.Value))
				{
					text = "text/plain";
				}
				else if (Enumerable.Contains<ODataPayloadKind>(payloadKinds, ODataPayloadKind.BinaryValue))
				{
					text = "application/octet-stream";
				}
				else
				{
					text = "application/json";
				}
			}
			return text;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00032814 File Offset: 0x00030A14
		private int GetContentLengthHeader()
		{
			int num = 0;
			int.TryParse(this.message.GetHeader("Content-Length"), ref num);
			return num;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0003283C File Offset: 0x00030A3C
		private void VerifyCanCreateODataFeedReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
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

		// Token: 0x06000E28 RID: 3624 RVA: 0x0003288C File Offset: 0x00030A8C
		private void VerifyCanCreateODataDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_DeltaInRequest);
			}
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

		// Token: 0x06000E29 RID: 3625 RVA: 0x000328F0 File Offset: 0x00030AF0
		private void VerifyCanCreateODataEntryReader(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (navigationSource != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("navigationSource"), "navigationSource");
				}
				if (entityType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("entityType"), "entityType");
				}
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00032940 File Offset: 0x00030B40
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

		// Token: 0x06000E2B RID: 3627 RVA: 0x000329A9 File Offset: 0x00030BA9
		private void VerifyCanCreateODataAsynchronousReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000329B1 File Offset: 0x00030BB1
		private void VerifyCanCreateODataBatchReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x000329B9 File Offset: 0x00030BB9
		private void VerifyCanCreateODataParameterReader(IEdmOperation operation)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ParameterPayloadInResponse);
			}
			if (operation != null && !this.model.IsUserModel())
			{
				throw new ArgumentException(Strings.ODataMessageReader_OperationSpecifiedWithoutMetadata("operation"), "operation");
			}
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000329F9 File Offset: 0x00030BF9
		private void VerifyCanReadServiceDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ServiceDocumentInRequest);
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00032A14 File Offset: 0x00030C14
		private void VerifyCanReadMetadataDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_MetadataDocumentInRequest);
			}
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00032A2F File Offset: 0x00030C2F
		private void VerifyCanReadProperty(IEdmStructuralProperty property)
		{
			if (property == null)
			{
				return;
			}
			this.VerifyCanReadProperty(property.Type);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00032A44 File Offset: 0x00030C44
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

		// Token: 0x06000E32 RID: 3634 RVA: 0x00032AD8 File Offset: 0x00030CD8
		private void VerifyCanReadError()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ErrorPayloadInRequest);
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00032AF3 File Offset: 0x00030CF3
		private void VerifyCanReadEntityReferenceLinks()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00032AFB File Offset: 0x00030CFB
		private void VerifyCanReadEntityReferenceLink()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00032B04 File Offset: 0x00030D04
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
			if (!expectedTypeReference.IsODataPrimitiveTypeKind() && !expectedTypeReference.IsODataTypeDefinitionTypeKind())
			{
				throw new ArgumentException(Strings.ODataMessageReader_ExpectedValueTypeWrongKind(expectedTypeReference.TypeKind().ToString()), "expectedTypeReference");
			}
			if (expectedTypeReference.IsBinary())
			{
				return new ODataPayloadKind[] { ODataPayloadKind.BinaryValue };
			}
			return new ODataPayloadKind[] { ODataPayloadKind.Value };
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00032B7C File Offset: 0x00030D7C
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

		// Token: 0x06000E37 RID: 3639 RVA: 0x00032BD3 File Offset: 0x00030DD3
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00032BF0 File Offset: 0x00030DF0
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

		// Token: 0x06000E39 RID: 3641 RVA: 0x00032C60 File Offset: 0x00030E60
		private T ReadFromInput<T>(Func<ODataInputContext, T> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			this.inputContext = this.format.CreateInputContext(new ODataMessageInfo
			{
				Encoding = this.encoding,
				GetMessageStream = new Func<Stream>(this.message.GetStream),
				IsResponse = this.readingResponse,
				MediaType = this.contentType,
				Model = this.model,
				UrlResolver = this.urlResolver,
				PayloadKind = this.readerPayloadKind
			}, this.settings);
			return readFunc.Invoke(this.inputContext);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00032D14 File Offset: 0x00030F14
		private bool TryGetSinglePayloadKindResultFromContentType(out IEnumerable<ODataPayloadKindDetectionResult> payloadKindResults)
		{
			if (this.message.UseBufferingReadStream == true)
			{
				throw new ODataException(Strings.ODataMessageReader_DetectPayloadKindMultipleTimes);
			}
			string contentTypeHeader = this.GetContentTypeHeader(new ODataPayloadKind[0]);
			IList<ODataPayloadKindDetectionResult> payloadKindsForContentType = MediaTypeUtils.GetPayloadKindsForContentType(contentTypeHeader, this.settings.MediaTypeResolver, out this.contentType, out this.encoding);
			payloadKindResults = Enumerable.Where<ODataPayloadKindDetectionResult>(payloadKindsForContentType, (ODataPayloadKindDetectionResult r) => ODataUtilsInternal.IsPayloadKindSupported(r.PayloadKind, !this.readingResponse));
			if (Enumerable.Count<ODataPayloadKindDetectionResult>(payloadKindResults) > 1)
			{
				this.message.UseBufferingReadStream = new bool?(true);
				return false;
			}
			return true;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00032DAC File Offset: 0x00030FAC
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

		// Token: 0x04000615 RID: 1557
		private readonly ODataMessage message;

		// Token: 0x04000616 RID: 1558
		private readonly bool readingResponse;

		// Token: 0x04000617 RID: 1559
		private readonly ODataMessageReaderSettings settings;

		// Token: 0x04000618 RID: 1560
		private readonly IEdmModel model;

		// Token: 0x04000619 RID: 1561
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400061A RID: 1562
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x0400061B RID: 1563
		private bool readMethodCalled;

		// Token: 0x0400061C RID: 1564
		private bool isDisposed;

		// Token: 0x0400061D RID: 1565
		private ODataInputContext inputContext;

		// Token: 0x0400061E RID: 1566
		private ODataPayloadKind readerPayloadKind;

		// Token: 0x0400061F RID: 1567
		private ODataFormat format;

		// Token: 0x04000620 RID: 1568
		private ODataMediaType contentType;

		// Token: 0x04000621 RID: 1569
		private Encoding encoding;

		// Token: 0x04000622 RID: 1570
		private string batchBoundary;
	}
}
