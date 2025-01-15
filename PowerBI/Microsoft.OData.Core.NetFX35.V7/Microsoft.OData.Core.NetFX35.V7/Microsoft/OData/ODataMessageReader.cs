using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000071 RID: 113
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Main resource point for reader functionality")]
	public sealed class ODataMessageReader : IDisposable
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000AAFF File Offset: 0x00008CFF
		public ODataMessageReader(IODataRequestMessage requestMessage)
			: this(requestMessage, null)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000AB09 File Offset: 0x00008D09
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings)
			: this(requestMessage, settings, null)
		{
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000AB14 File Offset: 0x00008D14
		public ODataMessageReader(IODataRequestMessage requestMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataRequestMessage>(requestMessage, "requestMessage");
			this.container = ODataMessageReader.GetContainer<IODataRequestMessage>(requestMessage);
			this.settings = ODataMessageReaderSettings.CreateReaderSettings(this.container, settings);
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, false);
			this.readingResponse = false;
			this.message = new ODataRequestMessage(requestMessage, false, this.settings.EnableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.payloadUriConverter = requestMessage as IODataPayloadUriConverter;
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.container);
			ODataUtilsInternal.GetODataVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? ODataMessageReader.GetModel(this.container);
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ClientCustomTypeResolver);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000ABFD File Offset: 0x00008DFD
		public ODataMessageReader(IODataResponseMessage responseMessage)
			: this(responseMessage, null)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000AC07 File Offset: 0x00008E07
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings)
			: this(responseMessage, settings, null)
		{
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000AC14 File Offset: 0x00008E14
		public ODataMessageReader(IODataResponseMessage responseMessage, ODataMessageReaderSettings settings, IEdmModel model)
		{
			this.readerPayloadKind = ODataPayloadKind.Unsupported;
			base..ctor();
			ExceptionUtils.CheckArgumentNotNull<IODataResponseMessage>(responseMessage, "responseMessage");
			this.container = ODataMessageReader.GetContainer<IODataResponseMessage>(responseMessage);
			this.settings = ODataMessageReaderSettings.CreateReaderSettings(this.container, settings);
			ReaderValidationUtils.ValidateMessageReaderSettings(this.settings, true);
			this.readingResponse = true;
			this.message = new ODataResponseMessage(responseMessage, false, this.settings.EnableMessageStreamDisposal, this.settings.MessageQuotas.MaxReceivedMessageSize);
			this.payloadUriConverter = responseMessage as IODataPayloadUriConverter;
			this.mediaTypeResolver = ODataMediaTypeResolver.GetMediaTypeResolver(this.container);
			ODataUtilsInternal.GetODataVersion(this.message, this.settings.MaxProtocolVersion);
			this.model = model ?? ODataMessageReader.GetModel(this.container);
			this.edmTypeResolver = new EdmTypeReaderResolver(this.model, this.settings.ClientCustomTypeResolver);
			string annotationFilter = responseMessage.PreferenceAppliedHeader().AnnotationFilter;
			if (this.settings.ShouldIncludeAnnotation == null && !string.IsNullOrEmpty(annotationFilter))
			{
				this.settings.ShouldIncludeAnnotation = ODataUtils.CreateAnnotationFilter(annotationFilter);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000AD2F File Offset: 0x00008F2F
		internal ODataMessageReaderSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000AD38 File Offset: 0x00008F38
		public IEnumerable<ODataPayloadKindDetectionResult> DetectPayloadKind()
		{
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
					ODataMessageInfo orCreateMessageInfo = this.GetOrCreateMessageInfo(this.message.GetStream(), false);
					IEnumerable<ODataPayloadKind> enumerable3 = grouping.Key.DetectPayloadKind(orCreateMessageInfo, this.settings);
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

		// Token: 0x060003B4 RID: 948 RVA: 0x0000AE98 File Offset: 0x00009098
		public ODataAsynchronousReader CreateODataAsynchronousReader()
		{
			this.VerifyCanCreateODataAsynchronousReader();
			return this.ReadFromInput<ODataAsynchronousReader>((ODataInputContext context) => context.CreateAsynchronousReader(), new ODataPayloadKind[] { ODataPayloadKind.Asynchronous });
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000AED0 File Offset: 0x000090D0
		public ODataReader CreateODataResourceSetReader()
		{
			return this.CreateODataResourceSetReader(null, null);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000AEDA File Offset: 0x000090DA
		public ODataReader CreateODataResourceSetReader(IEdmStructuredType expectedResourceType)
		{
			return this.CreateODataResourceSetReader(null, expectedResourceType);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public ODataReader CreateODataResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateResourceSetReader(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000AF50 File Offset: 0x00009150
		public ODataDeltaReader CreateODataDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataDeltaReader(entitySet, expectedBaseEntityType);
			expectedBaseEntityType = expectedBaseEntityType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataDeltaReader>((ODataInputContext context) => context.CreateDeltaReader(entitySet, expectedBaseEntityType), new ODataPayloadKind[1]);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000AFBC File Offset: 0x000091BC
		public ODataReader CreateODataResourceReader()
		{
			return this.CreateODataResourceReader(null, null);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000AFC6 File Offset: 0x000091C6
		public ODataReader CreateODataResourceReader(IEdmStructuredType resourceType)
		{
			return this.CreateODataResourceReader(null, resourceType);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public ODataReader CreateODataResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyCanCreateODataResourceReader(navigationSource, resourceType);
			resourceType = resourceType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateResourceReader(navigationSource, resourceType), new ODataPayloadKind[] { ODataPayloadKind.Resource });
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B040 File Offset: 0x00009240
		public ODataCollectionReader CreateODataCollectionReader()
		{
			return this.CreateODataCollectionReader(null);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B04C File Offset: 0x0000924C
		public ODataCollectionReader CreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateODataCollectionReader(expectedItemTypeReference);
			return this.ReadFromInput<ODataCollectionReader>((ODataInputContext context) => context.CreateCollectionReader(expectedItemTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Collection });
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000B08E File Offset: 0x0000928E
		public ODataBatchReader CreateODataBatchReader()
		{
			this.VerifyCanCreateODataBatchReader();
			return this.ReadFromInput<ODataBatchReader>((ODataInputContext context) => context.CreateBatchReader(this.batchBoundary), new ODataPayloadKind[] { ODataPayloadKind.Batch });
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B0B4 File Offset: 0x000092B4
		public ODataReader CreateODataUriParameterResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceReader(navigationSource, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(navigationSource);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateUriParameterResourceReader(navigationSource, expectedResourceType), new ODataPayloadKind[] { ODataPayloadKind.Resource });
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B124 File Offset: 0x00009324
		public ODataReader CreateODataUriParameterResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataResourceSetReader(entitySet, expectedResourceType);
			expectedResourceType = expectedResourceType ?? this.edmTypeResolver.GetElementType(entitySet);
			return this.ReadFromInput<ODataReader>((ODataInputContext context) => context.CreateUriParameterResourceSetReader(entitySet, expectedResourceType), new ODataPayloadKind[1]);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B190 File Offset: 0x00009390
		public ODataParameterReader CreateODataParameterReader(IEdmOperation operation)
		{
			this.VerifyCanCreateODataParameterReader(operation);
			return this.ReadFromInput<ODataParameterReader>((ODataInputContext context) => context.CreateParameterReader(operation), new ODataPayloadKind[] { ODataPayloadKind.Parameter });
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B1D3 File Offset: 0x000093D3
		public ODataServiceDocument ReadServiceDocument()
		{
			this.VerifyCanReadServiceDocument();
			return this.ReadFromInput<ODataServiceDocument>((ODataInputContext context) => context.ReadServiceDocument(), new ODataPayloadKind[] { ODataPayloadKind.ServiceDocument });
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B20A File Offset: 0x0000940A
		public ODataProperty ReadProperty()
		{
			return this.ReadProperty(null);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B214 File Offset: 0x00009414
		public ODataProperty ReadProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty(expectedPropertyTypeReference);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(null, expectedPropertyTypeReference), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000B258 File Offset: 0x00009458
		public ODataProperty ReadProperty(IEdmStructuralProperty property)
		{
			this.VerifyCanReadProperty(property);
			return this.ReadFromInput<ODataProperty>((ODataInputContext context) => context.ReadProperty(property, property.Type), new ODataPayloadKind[] { ODataPayloadKind.Property });
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B29A File Offset: 0x0000949A
		public ODataError ReadError()
		{
			this.VerifyCanReadError();
			return this.ReadFromInput<ODataError>((ODataInputContext context) => context.ReadError(), new ODataPayloadKind[] { ODataPayloadKind.Error });
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B2D2 File Offset: 0x000094D2
		public ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			this.VerifyCanReadEntityReferenceLinks();
			return this.ReadFromInput<ODataEntityReferenceLinks>((ODataInputContext context) => context.ReadEntityReferenceLinks(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLinks });
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B309 File Offset: 0x00009509
		public ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			this.VerifyCanReadEntityReferenceLink();
			return this.ReadFromInput<ODataEntityReferenceLink>((ODataInputContext context) => context.ReadEntityReferenceLink(), new ODataPayloadKind[] { ODataPayloadKind.EntityReferenceLink });
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B340 File Offset: 0x00009540
		public object ReadValue(IEdmTypeReference expectedTypeReference)
		{
			ODataPayloadKind[] array = this.VerifyCanReadValue(expectedTypeReference);
			return this.ReadFromInput<object>((ODataInputContext context) => context.ReadValue(expectedTypeReference.AsPrimitiveOrNull()), array);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B37A File Offset: 0x0000957A
		public IEdmModel ReadMetadataDocument()
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(null), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000B3B4 File Offset: 0x000095B4
		public IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			this.VerifyCanReadMetadataDocument();
			return this.ReadFromInput<IEdmModel>((ODataInputContext context) => context.ReadMetadataDocument(getReferencedModelReaderFunc), new ODataPayloadKind[] { ODataPayloadKind.MetadataDocument });
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000B3F1 File Offset: 0x000095F1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000B400 File Offset: 0x00009600
		internal ODataFormat GetFormat()
		{
			if (this.format == null)
			{
				throw new ODataException(Strings.ODataMessageReader_GetFormatCalledBeforeReadingStarted);
			}
			return this.format;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000B41B File Offset: 0x0000961B
		private static IServiceProvider GetContainer<T>(T message) where T : class
		{
			return null;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000B41E File Offset: 0x0000961E
		private static IEdmModel GetModel(IServiceProvider container)
		{
			return EdmCoreModel.Instance;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000B428 File Offset: 0x00009628
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
				this.messageInfo.IsResponse = this.readingResponse;
				this.messageInfo.IsAsync = isAsync;
				this.messageInfo.MediaType = this.contentType;
				this.messageInfo.Model = this.model;
				this.messageInfo.PayloadUriConverter = this.payloadUriConverter;
				this.messageInfo.Container = this.container;
				this.messageInfo.MessageStream = messageStream;
				this.messageInfo.PayloadKind = this.readerPayloadKind;
			}
			return this.messageInfo;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000B4FC File Offset: 0x000096FC
		private void ProcessContentType(params ODataPayloadKind[] payloadKinds)
		{
			string contentTypeHeader = this.GetContentTypeHeader(payloadKinds);
			this.format = MediaTypeUtils.GetFormatFromContentType(contentTypeHeader, payloadKinds, this.mediaTypeResolver, out this.contentType, out this.encoding, out this.readerPayloadKind, out this.batchBoundary);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B53C File Offset: 0x0000973C
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

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B5AC File Offset: 0x000097AC
		private int GetContentLengthHeader()
		{
			int num = 0;
			int.TryParse(this.message.GetHeader("Content-Length"), ref num);
			return num;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B5D4 File Offset: 0x000097D4
		private void VerifyCanCreateODataResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedBaseResourceType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (entitySet != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("entitySet"), "entitySet");
				}
				if (expectedBaseResourceType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedBaseEntityType"), "expectedBaseEntityType");
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B624 File Offset: 0x00009824
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

		// Token: 0x060003D6 RID: 982 RVA: 0x0000B688 File Offset: 0x00009888
		private void VerifyCanCreateODataResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.model.IsUserModel())
			{
				if (navigationSource != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_EntitySetSpecifiedWithoutMetadata("navigationSource"), "navigationSource");
				}
				if (resourceType != null)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("resourceType"), "resourceType");
				}
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000B6D8 File Offset: 0x000098D8
		private void VerifyCanCreateODataCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (expectedItemTypeReference != null)
			{
				if (!this.model.IsUserModel())
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedTypeSpecifiedWithoutMetadata("expectedItemTypeReference"), "expectedItemTypeReference");
				}
				if (!expectedItemTypeReference.IsODataPrimitiveTypeKind() && expectedItemTypeReference.TypeKind() != EdmTypeKind.Complex && expectedItemTypeReference.TypeKind() != EdmTypeKind.Enum)
				{
					throw new ArgumentException(Strings.ODataMessageReader_ExpectedCollectionTypeWrongKind(expectedItemTypeReference.TypeKind().ToString()), "expectedItemTypeReference");
				}
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000B74E File Offset: 0x0000994E
		private void VerifyCanCreateODataAsynchronousReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B74E File Offset: 0x0000994E
		private void VerifyCanCreateODataBatchReader()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B756 File Offset: 0x00009956
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

		// Token: 0x060003DB RID: 987 RVA: 0x0000B796 File Offset: 0x00009996
		private void VerifyCanReadServiceDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ServiceDocumentInRequest);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000B7B1 File Offset: 0x000099B1
		private void VerifyCanReadMetadataDocument()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_MetadataDocumentInRequest);
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B7CC File Offset: 0x000099CC
		private void VerifyCanReadProperty(IEdmStructuralProperty property)
		{
			if (property == null)
			{
				return;
			}
			this.VerifyCanReadProperty(property.Type);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000B7E0 File Offset: 0x000099E0
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

		// Token: 0x060003DF RID: 991 RVA: 0x0000B874 File Offset: 0x00009A74
		private void VerifyCanReadError()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
			if (!this.readingResponse)
			{
				throw new ODataException(Strings.ODataMessageReader_ErrorPayloadInRequest);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000B74E File Offset: 0x0000994E
		private void VerifyCanReadEntityReferenceLinks()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000B74E File Offset: 0x0000994E
		private void VerifyCanReadEntityReferenceLink()
		{
			this.VerifyReaderNotDisposedAndNotUsed();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000B890 File Offset: 0x00009A90
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

		// Token: 0x060003E3 RID: 995 RVA: 0x0000B908 File Offset: 0x00009B08
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

		// Token: 0x060003E4 RID: 996 RVA: 0x0000B95F File Offset: 0x00009B5F
		private void VerifyNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000B97C File Offset: 0x00009B7C
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
				if (this.settings.EnableMessageStreamDisposal && this.message.BufferingReadStream != null)
				{
					this.message.BufferingReadStream.Dispose();
				}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000B9EC File Offset: 0x00009BEC
		private T ReadFromInput<T>(Func<ODataInputContext, T> readFunc, params ODataPayloadKind[] payloadKinds) where T : class
		{
			this.ProcessContentType(payloadKinds);
			this.inputContext = this.format.CreateInputContext(this.GetOrCreateMessageInfo(this.message.GetStream(), false), this.settings);
			return readFunc.Invoke(this.inputContext);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000BA2C File Offset: 0x00009C2C
		private bool TryGetSinglePayloadKindResultFromContentType(out IEnumerable<ODataPayloadKindDetectionResult> payloadKindResults)
		{
			if (this.message.UseBufferingReadStream == true)
			{
				throw new ODataException(Strings.ODataMessageReader_DetectPayloadKindMultipleTimes);
			}
			string contentTypeHeader = this.GetContentTypeHeader(new ODataPayloadKind[0]);
			IList<ODataPayloadKindDetectionResult> payloadKindsForContentType = MediaTypeUtils.GetPayloadKindsForContentType(contentTypeHeader, this.mediaTypeResolver, out this.contentType, out this.encoding);
			payloadKindResults = Enumerable.Where<ODataPayloadKindDetectionResult>(payloadKindsForContentType, (ODataPayloadKindDetectionResult r) => ODataUtilsInternal.IsPayloadKindSupported(r.PayloadKind, !this.readingResponse));
			if (Enumerable.Count<ODataPayloadKindDetectionResult>(payloadKindResults) > 1)
			{
				this.message.UseBufferingReadStream = new bool?(true);
				return false;
			}
			return true;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000BAC0 File Offset: 0x00009CC0
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

		// Token: 0x040001E9 RID: 489
		private readonly ODataMessage message;

		// Token: 0x040001EA RID: 490
		private readonly bool readingResponse;

		// Token: 0x040001EB RID: 491
		private readonly ODataMessageReaderSettings settings;

		// Token: 0x040001EC RID: 492
		private readonly IEdmModel model;

		// Token: 0x040001ED RID: 493
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x040001EE RID: 494
		private readonly IServiceProvider container;

		// Token: 0x040001EF RID: 495
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040001F0 RID: 496
		private readonly ODataMediaTypeResolver mediaTypeResolver;

		// Token: 0x040001F1 RID: 497
		private bool readMethodCalled;

		// Token: 0x040001F2 RID: 498
		private bool isDisposed;

		// Token: 0x040001F3 RID: 499
		private ODataInputContext inputContext;

		// Token: 0x040001F4 RID: 500
		private ODataPayloadKind readerPayloadKind;

		// Token: 0x040001F5 RID: 501
		private ODataFormat format;

		// Token: 0x040001F6 RID: 502
		private ODataMediaType contentType;

		// Token: 0x040001F7 RID: 503
		private Encoding encoding;

		// Token: 0x040001F8 RID: 504
		private string batchBoundary;

		// Token: 0x040001F9 RID: 505
		private ODataMessageInfo messageInfo;
	}
}
