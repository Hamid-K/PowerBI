using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000194 RID: 404
	internal sealed class ODataJsonLightInputContext : ODataInputContext
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x00028378 File Offset: 0x00026578
		internal ODataJsonLightInputContext(ODataFormat format, Stream messageStream, MediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataJsonLightPayloadKindDetectionState payloadKindDetectionState)
			: this(format, ODataJsonLightInputContext.CreateTextReaderForMessageStreamConstructor(messageStream, encoding), contentType, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver, payloadKindDetectionState)
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000283A4 File Offset: 0x000265A4
		internal ODataJsonLightInputContext(ODataFormat format, TextReader reader, MediaType contentType, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver, ODataJsonLightPayloadKindDetectionState payloadKindDetectionState)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			}
			catch (ArgumentNullException)
			{
				reader.Dispose();
				throw;
			}
			try
			{
				this.textReader = reader;
				if (contentType.HasStreamingSetToTrue())
				{
					this.jsonReader = new BufferingJsonReader(this.textReader, "odata.error", messageReaderSettings.MessageQuotas.MaxNestingDepth, ODataFormat.Json);
				}
				else
				{
					this.jsonReader = new ReorderingJsonReader(this.textReader, messageReaderSettings.MessageQuotas.MaxNestingDepth);
				}
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && reader != null)
				{
					reader.Dispose();
				}
				throw;
			}
			this.payloadKindDetectionState = payloadKindDetectionState;
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x00028478 File Offset: 0x00026678
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00028480 File Offset: 0x00026680
		internal ODataJsonLightPayloadKindDetectionState PayloadKindDetectionState
		{
			get
			{
				return this.payloadKindDetectionState;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00028488 File Offset: 0x00026688
		internal override ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002849A File Offset: 0x0002669A
		internal override ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedEntityType);
			return this.CreateEntryReaderImplementation(entitySet, expectedEntityType);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000284AC File Offset: 0x000266AC
		internal override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000284BC File Offset: 0x000266BC
		internal override ODataParameterReader CreateParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyCanCreateParameterReader(functionImport);
			return this.CreateParameterReaderImplementation(functionImport);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000284CC File Offset: 0x000266CC
		internal override ODataWorkspace ReadServiceDocument()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000284E8 File Offset: 0x000266E8
		internal override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelProperty(expectedPropertyTypeReference);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002850C File Offset: 0x0002670C
		internal override ODataError ReadError()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00028528 File Offset: 0x00026728
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinks(navigationProperty);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00028544 File Offset: 0x00026744
		internal override ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			this.VerifyCanReadEntityReferenceLink(navigationProperty);
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLink(navigationProperty);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00028568 File Offset: 0x00026768
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002858C File Offset: 0x0002678C
		protected override void DisposeImplementation()
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

		// Token: 0x06000B70 RID: 2928 RVA: 0x000285D0 File Offset: 0x000267D0
		private static TextReader CreateTextReaderForMessageStreamConstructor(Stream messageStream, Encoding encoding)
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

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002860C File Offset: 0x0002680C
		private void VerifyCanCreateParameterReader(IEdmFunctionImport functionImport)
		{
			this.VerifyUserModel();
			if (functionImport == null)
			{
				throw new ArgumentNullException("functionImport", Strings.ODataJsonLightInputContext_FunctionImportCannotBeNullForCreateParameterReader("functionImport"));
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002862C File Offset: 0x0002682C
		private void VerifyCanCreateODataReader(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (entitySet == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_NoEntitySetForRequest);
				}
			}
			IEdmEntityType elementType = base.EdmTypeResolver.GetElementType(entitySet);
			if (entitySet != null && entityType != null && !entityType.IsOrInheritsFrom(elementType))
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(entityType.FullName(), elementType.FullName(), entitySet.FullName()));
			}
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002868E File Offset: 0x0002688E
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

		// Token: 0x06000B74 RID: 2932 RVA: 0x000286AC File Offset: 0x000268AC
		private void VerifyCanReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (navigationProperty == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_NavigationPropertyRequiredForReadEntityReferenceLinkInRequests);
				}
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000286CA File Offset: 0x000268CA
		private void VerifyCanReadProperty()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000286DA File Offset: 0x000268DA
		private void VerifyCanDetectPayloadKind()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_PayloadKindDetectionForRequest);
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x000286EF File Offset: 0x000268EF
		private void VerifyUserModel()
		{
			if (!base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_ModelRequiredForReading);
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00028709 File Offset: 0x00026909
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataJsonLightReader(this, entitySet, expectedBaseEntityType, true, null);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00028715 File Offset: 0x00026915
		private ODataReader CreateEntryReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return new ODataJsonLightReader(this, entitySet, expectedEntityType, false, null);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00028721 File Offset: 0x00026921
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002872B File Offset: 0x0002692B
		private ODataParameterReader CreateParameterReaderImplementation(IEdmFunctionImport functionImport)
		{
			return new ODataJsonLightParameterReader(this, functionImport);
		}

		// Token: 0x04000429 RID: 1065
		private readonly ODataJsonLightPayloadKindDetectionState payloadKindDetectionState;

		// Token: 0x0400042A RID: 1066
		private TextReader textReader;

		// Token: 0x0400042B RID: 1067
		private BufferingJsonReader jsonReader;
	}
}
