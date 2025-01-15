using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000246 RID: 582
	internal sealed class ODataJsonLightInputContext : ODataInputContext
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x0004B76F File Offset: 0x0004996F
		public ODataJsonLightInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: this(ODataJsonLightInputContext.CreateTextReader(messageInfo.MessageStream, messageInfo.Encoding), messageInfo, messageReaderSettings)
		{
			this.stream = messageInfo.MessageStream;
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0004B798 File Offset: 0x00049998
		internal ODataJsonLightInputContext(TextReader textReader, ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: base(ODataFormat.Json, messageInfo, messageReaderSettings)
		{
			try
			{
				this.textReader = textReader;
				IJsonReader jsonReader = ODataJsonLightInputContext.CreateJsonReader(base.Container, this.textReader, messageInfo.MediaType.HasIeee754CompatibleSetToTrue());
				if (messageReaderSettings.ArrayPool != null)
				{
					JsonReader jsonReader2 = jsonReader as JsonReader;
					if (jsonReader2 != null && jsonReader2.ArrayPool == null)
					{
						jsonReader2.ArrayPool = messageReaderSettings.ArrayPool;
					}
				}
				if (messageInfo.MediaType.HasStreamingSetToTrue())
				{
					this.jsonReader = new BufferingJsonReader(jsonReader, "error", messageReaderSettings.MessageQuotas.MaxNestingDepth);
				}
				else
				{
					this.jsonReader = new ReorderingJsonReader(jsonReader, messageReaderSettings.MessageQuotas.MaxNestingDepth);
				}
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && this.textReader != null)
				{
					this.textReader.Dispose();
				}
				throw;
			}
			this.metadataLevel = JsonLightMetadataLevel.Create(messageInfo.MediaType, null, base.Model, base.ReadingResponse);
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0004B88C File Offset: 0x00049A8C
		public JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x0004B894 File Offset: 0x00049A94
		public BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x0004B89C File Offset: 0x00049A9C
		internal Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x0004B8A4 File Offset: 0x00049AA4
		internal bool OptionalODataPrefix
		{
			get
			{
				return !(base.MessageReaderSettings.Version == ODataVersion.V4) || base.ODataSimplifiedOptions.EnableReadingODataAnnotationWithoutPrefix;
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0004B8E2 File Offset: 0x00049AE2
		public override ODataReader CreateResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, false, false);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0004B8F8 File Offset: 0x00049AF8
		public override Task<ODataReader> CreateResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, false, false));
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0004B943 File Offset: 0x00049B43
		public override ODataReader CreateDeltaResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, false, true);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0004B958 File Offset: 0x00049B58
		public override Task<ODataReader> CreateDeltaResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, false, true));
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0004B9A3 File Offset: 0x00049BA3
		public override ODataReader CreateResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(navigationSource, expectedResourceType);
			return this.CreateResourceReaderImplementation(navigationSource, expectedResourceType);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0004B9B8 File Offset: 0x00049BB8
		public override Task<ODataReader> CreateResourceReaderAsync(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(navigationSource, expectedResourceType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateResourceReaderImplementation(navigationSource, expectedResourceType));
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0004BA03 File Offset: 0x00049C03
		public override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0004BA14 File Offset: 0x00049C14
		public override Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return TaskUtils.GetTaskForSynchronousOperation<ODataCollectionReader>(() => this.CreateCollectionReaderImplementation(expectedItemTypeReference));
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0004BA54 File Offset: 0x00049C54
		public override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelProperty(expectedPropertyTypeReference);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0004BA78 File Offset: 0x00049C78
		public override Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelPropertyAsync(expectedPropertyTypeReference);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0004BA9C File Offset: 0x00049C9C
		public override ODataError ReadError()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		public override Task<ODataError> ReadErrorAsync()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelErrorAsync();
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x0004BAD2 File Offset: 0x00049CD2
		public override ODataReader CreateUriParameterResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, true, false);
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x0004BAE8 File Offset: 0x00049CE8
		public override Task<ODataReader> CreateUriParameterResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataReader>(() => this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, true, false));
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0004BB33 File Offset: 0x00049D33
		public override ODataReader CreateUriParameterResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceReader(navigationSource, expectedResourceType);
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0004BB3D File Offset: 0x00049D3D
		public override Task<ODataReader> CreateUriParameterResourceReaderAsync(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceReaderAsync(navigationSource, expectedResourceType);
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0004BB47 File Offset: 0x00049D47
		public override ODataParameterReader CreateParameterReader(IEdmOperation operation)
		{
			this.VerifyCanCreateParameterReader(operation);
			return this.CreateParameterReaderImplementation(operation);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x0004BB58 File Offset: 0x00049D58
		public override Task<ODataParameterReader> CreateParameterReaderAsync(IEdmOperation operation)
		{
			this.VerifyCanCreateParameterReader(operation);
			return TaskUtils.GetTaskForSynchronousOperation<ODataParameterReader>(() => this.CreateParameterReaderImplementation(operation));
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x0004BB98 File Offset: 0x00049D98
		public IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x0004BBBC File Offset: 0x00049DBC
		public Task<IEnumerable<ODataPayloadKind>> DetectPayloadKindAsync(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKindAsync(detectionInfo);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x0004BBDD File Offset: 0x00049DDD
		internal override ODataDeltaReader CreateDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return this.CreateDeltaReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x0004BBF0 File Offset: 0x00049DF0
		internal override Task<ODataDeltaReader> CreateDeltaReaderAsync(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return TaskUtils.GetTaskForSynchronousOperation<ODataDeltaReader>(() => this.CreateDeltaReaderImplementation(entitySet, expectedBaseEntityType));
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x0004BC3B File Offset: 0x00049E3B
		internal override ODataBatchReader CreateBatchReader()
		{
			return this.CreateBatchReaderImplementation(true);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x0004BC44 File Offset: 0x00049E44
		internal override Task<ODataBatchReader> CreateBatchReaderAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<ODataBatchReader>(() => this.CreateBatchReaderImplementation(false));
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x0004BC58 File Offset: 0x00049E58
		internal override ODataServiceDocument ReadServiceDocument()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0004BC74 File Offset: 0x00049E74
		internal override Task<ODataServiceDocument> ReadServiceDocumentAsync()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocumentAsync();
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0004BC90 File Offset: 0x00049E90
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0004BCAC File Offset: 0x00049EAC
		internal override Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync()
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinksAsync();
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0004BCC8 File Offset: 0x00049EC8
		internal override ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			this.VerifyCanReadEntityReferenceLink();
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0004BCE8 File Offset: 0x00049EE8
		internal override Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync()
		{
			this.VerifyCanReadEntityReferenceLink();
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinkAsync();
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0004BD08 File Offset: 0x00049F08
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
					this.stream = null;
					if (this.textReader != null)
					{
						this.textReader.Dispose();
					}
				}
				finally
				{
					this.textReader = null;
					this.jsonReader = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x0004BD5C File Offset: 0x00049F5C
		private static TextReader CreateTextReader(Stream messageStream, Encoding encoding)
		{
			TextReader textReader;
			try
			{
				textReader = new StreamReader(messageStream, encoding);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
			return textReader;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0004BD98 File Offset: 0x00049F98
		private static IJsonReader CreateJsonReader(IServiceProvider container, TextReader textReader, bool isIeee754Compatible)
		{
			if (container == null)
			{
				return new JsonReader(textReader, isIeee754Compatible);
			}
			IJsonReaderFactory requiredService = container.GetRequiredService<IJsonReaderFactory>();
			return requiredService.CreateJsonReader(textReader, isIeee754Compatible);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0004BDC1 File Offset: 0x00049FC1
		private void VerifyCanCreateParameterReader(IEdmOperation operation)
		{
			this.VerifyUserModel();
			if (operation == null)
			{
				throw new ArgumentNullException("operation", Strings.ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader("operation"));
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x0004BDE4 File Offset: 0x00049FE4
		private void VerifyCanCreateODataReader(IEdmNavigationSource navigationSource, IEdmStructuredType structuredType)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (navigationSource == null && structuredType != null && structuredType.IsODataEntityTypeKind())
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_NoEntitySetForRequest);
				}
			}
			IEdmEntityType elementType = base.EdmTypeResolver.GetElementType(navigationSource);
			if (navigationSource != null && structuredType != null && !structuredType.IsOrInheritsFrom(elementType))
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(structuredType.FullTypeName(), elementType.FullName(), navigationSource.FullNavigationSourceName()));
			}
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0004BE51 File Offset: 0x0004A051
		private void VerifyCanCreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (expectedItemTypeReference == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_ItemTypeRequiredForCollectionReaderInRequests);
				}
			}
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0004BE6F File Offset: 0x0004A06F
		private void VerifyCanReadEntityReferenceLink()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0004BE6F File Offset: 0x0004A06F
		private void VerifyCanReadProperty()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0004BE7F File Offset: 0x0004A07F
		private void VerifyCanDetectPayloadKind()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_PayloadKindDetectionForRequest);
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0004BE94 File Offset: 0x0004A094
		private void VerifyUserModel()
		{
			if (!base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_ModelRequiredForReading);
			}
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0004BEAE File Offset: 0x0004A0AE
		private ODataReader CreateResourceSetReaderImplementation(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType, bool readingParameter, bool readingDelta)
		{
			return new ODataJsonLightReader(this, entitySet, expectedResourceType, true, readingParameter, readingDelta, null);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0004BEBD File Offset: 0x0004A0BD
		private ODataDeltaReader CreateDeltaReaderImplementation(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataJsonLightDeltaReader(this, entitySet, expectedBaseEntityType);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0004BEC7 File Offset: 0x0004A0C7
		private ODataReader CreateResourceReaderImplementation(IEdmNavigationSource navigationSource, IEdmStructuredType expectedBaseResourceType)
		{
			return new ODataJsonLightReader(this, navigationSource, expectedBaseResourceType, false, false, false, null);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0004BED5 File Offset: 0x0004A0D5
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0004BEDF File Offset: 0x0004A0DF
		private ODataParameterReader CreateParameterReaderImplementation(IEdmOperation operation)
		{
			return new ODataJsonLightParameterReader(this, operation);
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0004BEE8 File Offset: 0x0004A0E8
		private ODataBatchReader CreateBatchReaderImplementation(bool synchronous)
		{
			return new ODataJsonLightBatchReader(this, synchronous);
		}

		// Token: 0x04000B38 RID: 2872
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x04000B39 RID: 2873
		private TextReader textReader;

		// Token: 0x04000B3A RID: 2874
		private BufferingJsonReader jsonReader;

		// Token: 0x04000B3B RID: 2875
		private Stream stream;
	}
}
