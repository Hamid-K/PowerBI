using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000E3 RID: 227
	internal sealed class ODataJsonLightInputContext : ODataInputContext
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x0001FA98 File Offset: 0x0001DC98
		internal ODataJsonLightInputContext(ODataFormat format, Stream messageStream, ODataMediaType contentType, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: this(format, ODataJsonLightInputContext.CreateTextReaderForMessageStreamConstructor(messageStream, encoding), contentType, messageReaderSettings, readingResponse, synchronous, model, urlResolver)
		{
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0001FAC0 File Offset: 0x0001DCC0
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal ODataJsonLightInputContext(ODataFormat format, TextReader reader, ODataMediaType contentType, ODataMessageReaderSettings messageReaderSettings, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, readingResponse, synchronous, model, urlResolver)
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
					this.jsonReader = new BufferingJsonReader(this.textReader, "error", messageReaderSettings.MessageQuotas.MaxNestingDepth, ODataFormat.Json, contentType.HasIeee754CompatibleSetToTrue());
				}
				else
				{
					this.jsonReader = new ReorderingJsonReader(this.textReader, messageReaderSettings.MessageQuotas.MaxNestingDepth, contentType.HasIeee754CompatibleSetToTrue());
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
			this.metadataLevel = JsonLightMetadataLevel.Create(contentType, null, model, readingResponse);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		internal JsonLightMetadataLevel MetadataLevel
		{
			get
			{
				return this.metadataLevel;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001FBAC File Offset: 0x0001DDAC
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		public override ODataReader CreateFeedReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001FBC6 File Offset: 0x0001DDC6
		public override ODataReader CreateEntryReader(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			this.VerifyCanCreateODataReader(navigationSource, expectedEntityType);
			return this.CreateEntryReaderImplementation(navigationSource, expectedEntityType);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		public override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			this.VerifyCanCreateCollectionReader(expectedItemTypeReference);
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		public override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			this.VerifyCanReadProperty();
			ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(this);
			return odataJsonLightPropertyAndValueDeserializer.ReadTopLevelProperty(expectedPropertyTypeReference);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001FC0C File Offset: 0x0001DE0C
		public override ODataError ReadError()
		{
			ODataJsonLightErrorDeserializer odataJsonLightErrorDeserializer = new ODataJsonLightErrorDeserializer(this);
			return odataJsonLightErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001FC26 File Offset: 0x0001DE26
		public override ODataParameterReader CreateParameterReader(IEdmOperation operation)
		{
			this.VerifyCanCreateParameterReader(operation);
			return this.CreateParameterReaderImplementation(operation);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001FC36 File Offset: 0x0001DE36
		internal override ODataDeltaReader CreateDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			this.VerifyCanCreateODataReader(entitySet, expectedBaseEntityType);
			return this.CreateDeltaReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001FC48 File Offset: 0x0001DE48
		internal override ODataServiceDocument ReadServiceDocument()
		{
			ODataJsonLightServiceDocumentDeserializer odataJsonLightServiceDocumentDeserializer = new ODataJsonLightServiceDocumentDeserializer(this);
			return odataJsonLightServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001FC64 File Offset: 0x0001DE64
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001FC80 File Offset: 0x0001DE80
		internal override ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			this.VerifyCanReadEntityReferenceLink();
			ODataJsonLightEntityReferenceLinkDeserializer odataJsonLightEntityReferenceLinkDeserializer = new ODataJsonLightEntityReferenceLinkDeserializer(this);
			return odataJsonLightEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001FCA0 File Offset: 0x0001DEA0
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			this.VerifyCanDetectPayloadKind();
			ODataJsonLightPayloadKindDetectionDeserializer odataJsonLightPayloadKindDetectionDeserializer = new ODataJsonLightPayloadKindDetectionDeserializer(this);
			return odataJsonLightPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001FCC4 File Offset: 0x0001DEC4
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

		// Token: 0x06000893 RID: 2195 RVA: 0x0001FD10 File Offset: 0x0001DF10
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
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

		// Token: 0x06000894 RID: 2196 RVA: 0x0001FD4C File Offset: 0x0001DF4C
		private void VerifyCanCreateParameterReader(IEdmOperation operation)
		{
			this.VerifyUserModel();
			if (operation == null)
			{
				throw new ArgumentNullException("operation", Strings.ODataJsonLightInputContext_OperationCannotBeNullForCreateParameterReader("operation"));
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001FD6C File Offset: 0x0001DF6C
		private void VerifyCanCreateODataReader(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
				if (navigationSource == null)
				{
					throw new ODataException(Strings.ODataJsonLightInputContext_NoEntitySetForRequest);
				}
			}
			IEdmEntityType elementType = base.EdmTypeResolver.GetElementType(navigationSource);
			if (navigationSource != null && entityType != null && !entityType.IsOrInheritsFrom(elementType))
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_EntityTypeMustBeCompatibleWithEntitySetBaseType(entityType.FullName(), elementType.FullName(), navigationSource.FullNavigationSourceName()));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001FDCE File Offset: 0x0001DFCE
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

		// Token: 0x06000897 RID: 2199 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		private void VerifyCanReadEntityReferenceLink()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001FDFC File Offset: 0x0001DFFC
		private void VerifyCanReadProperty()
		{
			if (!base.ReadingResponse)
			{
				this.VerifyUserModel();
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001FE0C File Offset: 0x0001E00C
		private void VerifyCanDetectPayloadKind()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_PayloadKindDetectionForRequest);
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001FE21 File Offset: 0x0001E021
		private void VerifyUserModel()
		{
			if (!base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonLightInputContext_ModelRequiredForReading);
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001FE3B File Offset: 0x0001E03B
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataJsonLightReader(this, entitySet, expectedBaseEntityType, true, false, false, null);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001FE49 File Offset: 0x0001E049
		private ODataDeltaReader CreateDeltaReaderImplementation(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataJsonLightDeltaReader(this, entitySet, expectedBaseEntityType);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001FE53 File Offset: 0x0001E053
		private ODataReader CreateEntryReaderImplementation(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			return new ODataJsonLightReader(this, navigationSource, expectedEntityType, false, false, false, null);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001FE61 File Offset: 0x0001E061
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataJsonLightCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001FE6B File Offset: 0x0001E06B
		private ODataParameterReader CreateParameterReaderImplementation(IEdmOperation operation)
		{
			return new ODataJsonLightParameterReader(this, operation);
		}

		// Token: 0x04000388 RID: 904
		private readonly JsonLightMetadataLevel metadataLevel;

		// Token: 0x04000389 RID: 905
		private TextReader textReader;

		// Token: 0x0400038A RID: 906
		private BufferingJsonReader jsonReader;
	}
}
