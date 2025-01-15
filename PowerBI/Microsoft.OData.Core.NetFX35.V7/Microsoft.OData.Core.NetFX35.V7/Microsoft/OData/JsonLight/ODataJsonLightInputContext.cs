using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200020D RID: 525
	internal sealed class ODataJsonLightInputContext : ODataInputContext
	{
		// Token: 0x06001502 RID: 5378 RVA: 0x0003EAEA File Offset: 0x0003CCEA
		public ODataJsonLightInputContext(ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: this(ODataJsonLightInputContext.CreateTextReader(messageInfo.MessageStream, messageInfo.Encoding), messageInfo, messageReaderSettings)
		{
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0003EB08 File Offset: 0x0003CD08
		internal ODataJsonLightInputContext(TextReader textReader, ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
			: base(ODataFormat.Json, messageInfo, messageReaderSettings)
		{
			try
			{
				this.textReader = textReader;
				IJsonReader jsonReader = ODataJsonLightInputContext.CreateJsonReader(base.Container, this.textReader, messageInfo.MediaType.HasIeee754CompatibleSetToTrue());
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

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0003EBD8 File Offset: 0x0003CDD8
		public JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0003EBE0 File Offset: 0x0003CDE0
		public BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0003EBE8 File Offset: 0x0003CDE8
		public override ODataReader CreateResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, false);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0003EBFB File Offset: 0x0003CDFB
		public override ODataReader CreateResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(navigationSource, expectedResourceType);
			return this.CreateResourceReaderImplementation(navigationSource, expectedResourceType);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0003EC0D File Offset: 0x0003CE0D
		public override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0003EC20 File Offset: 0x0003CE20
		public override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelProperty(expectedPropertyTypeReference);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0003EC44 File Offset: 0x0003CE44
		public override ODataError ReadError()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0003EC5E File Offset: 0x0003CE5E
		public override ODataReader CreateUriParameterResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedResourceType);
			return this.CreateResourceSetReaderImplementation(entitySet, expectedResourceType, true);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0003EC71 File Offset: 0x0003CE71
		public override ODataReader CreateUriParameterResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			return this.CreateResourceReader(navigationSource, expectedResourceType);
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0003EC7B File Offset: 0x0003CE7B
		public override ODataParameterReader CreateParameterReader(IEdmOperation operation)
		{
			this.VerifyCanCreateParameterReader(operation);
			return this.CreateParameterReaderImplementation(operation);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0003EC8C File Offset: 0x0003CE8C
		public IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0003ECAD File Offset: 0x0003CEAD
		internal override ODataDeltaReader CreateDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return this.CreateDeltaReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0003ECC0 File Offset: 0x0003CEC0
		internal override ODataServiceDocument ReadServiceDocument()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0003ECDC File Offset: 0x0003CEDC
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0003ECF8 File Offset: 0x0003CEF8
		internal override ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			this.VerifyCanReadEntityReferenceLink();
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0003ED18 File Offset: 0x0003CF18
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				try
				{
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

		// Token: 0x06001514 RID: 5396 RVA: 0x0003ED64 File Offset: 0x0003CF64
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

		// Token: 0x06001515 RID: 5397 RVA: 0x0003EDA0 File Offset: 0x0003CFA0
		private static IJsonReader CreateJsonReader(IServiceProvider container, TextReader textReader, bool isIeee754Compatible)
		{
			if (container == null)
			{
				return new JsonReader(textReader, isIeee754Compatible);
			}
			IJsonReaderFactory requiredService = container.GetRequiredService<IJsonReaderFactory>();
			return requiredService.CreateJsonReader(textReader, isIeee754Compatible);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0003EDC9 File Offset: 0x0003CFC9
		private void VerifyCanCreateParameterReader(IEdmOperation operation)
		{
			this.VerifyUserModel();
			if (operation == null)
			{
				throw new ArgumentNullException("operation", Strings.ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader("operation"));
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0003EDEC File Offset: 0x0003CFEC
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

		// Token: 0x06001518 RID: 5400 RVA: 0x0003EE59 File Offset: 0x0003D059
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

		// Token: 0x06001519 RID: 5401 RVA: 0x0003EE77 File Offset: 0x0003D077
		private void VerifyCanReadEntityReferenceLink()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0003EE77 File Offset: 0x0003D077
		private void VerifyCanReadProperty()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0003EE87 File Offset: 0x0003D087
		private void VerifyCanDetectPayloadKind()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_PayloadKindDetectionForRequest);
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0003EE9C File Offset: 0x0003D09C
		private void VerifyUserModel()
		{
			if (!base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_ModelRequiredForReading);
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0003EEB6 File Offset: 0x0003D0B6
		private ODataReader CreateResourceSetReaderImplementation(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType, bool readingParameter)
		{
			return new ODataJsonLightReader(this, entitySet, expectedResourceType, true, readingParameter, false, null);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0003EEC4 File Offset: 0x0003D0C4
		private ODataDeltaReader CreateDeltaReaderImplementation(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataJsonLightDeltaReader(this, entitySet, expectedBaseEntityType);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0003EECE File Offset: 0x0003D0CE
		private ODataReader CreateResourceReaderImplementation(IEdmNavigationSource navigationSource, IEdmStructuredType expectedBaseResourceType)
		{
			return new ODataJsonLightReader(this, navigationSource, expectedBaseResourceType, false, false, false, null);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0003EEDC File Offset: 0x0003D0DC
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0003EEE6 File Offset: 0x0003D0E6
		private ODataParameterReader CreateParameterReaderImplementation(IEdmOperation operation)
		{
			return new ODataJsonLightParameterReader(this, operation);
		}

		// Token: 0x04000A1E RID: 2590
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x04000A1F RID: 2591
		private TextReader textReader;

		// Token: 0x04000A20 RID: 2592
		private BufferingJsonReader jsonReader;
	}
}
