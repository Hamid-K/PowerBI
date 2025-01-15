using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000238 RID: 568
	internal sealed class ODataVerboseJsonInputContext : ODataInputContext
	{
		// Token: 0x0600113D RID: 4413 RVA: 0x00041A28 File Offset: 0x0003FC28
		internal ODataVerboseJsonInputContext(ODataFormat format, TextReader reader, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
				this.textReader = reader;
				if (base.UseServerFormatBehavior)
				{
					this.jsonReader = new PropertyDeduplicatingJsonReader(this.textReader, messageReaderSettings.MessageQuotas.MaxNestingDepth);
				}
				else
				{
					this.jsonReader = new BufferingJsonReader(this.textReader, "error", messageReaderSettings.MessageQuotas.MaxNestingDepth, ODataFormat.VerboseJson);
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
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00041AD8 File Offset: 0x0003FCD8
		internal ODataVerboseJsonInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: this(format, ODataVerboseJsonInputContext.CreateTextReaderForMessageStreamConstructor(messageStream, encoding), messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600113F RID: 4415 RVA: 0x00041AFF File Offset: 0x0003FCFF
		internal BufferingJsonReader JsonReader
		{
			get
			{
				return this.jsonReader;
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00041B07 File Offset: 0x0003FD07
		internal override ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00041B11 File Offset: 0x0003FD11
		internal override ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return this.CreateEntryReaderImplementation(entitySet, expectedEntityType);
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00041B1B File Offset: 0x0003FD1B
		internal override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00041B24 File Offset: 0x0003FD24
		internal override ODataParameterReader CreateParameterReader(IEdmFunctionImport functionImport)
		{
			ODataVerboseJsonInputContext.VerifyCanCreateParameterReader(functionImport);
			return this.CreateParameterReaderImplementation(functionImport);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00041B33 File Offset: 0x0003FD33
		internal override ODataWorkspace ReadServiceDocument()
		{
			return this.ReadServiceDocumentImplementation();
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00041B3B File Offset: 0x0003FD3B
		internal override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return this.ReadPropertyImplementation(property, expectedPropertyTypeReference);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00041B45 File Offset: 0x0003FD45
		internal override ODataError ReadError()
		{
			return this.ReadErrorImplementation();
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00041B4D File Offset: 0x0003FD4D
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinksImplementation();
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00041B55 File Offset: 0x0003FD55
		internal override ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinkImplementation();
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00041B60 File Offset: 0x0003FD60
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind()
		{
			ODataVerboseJsonPayloadKindDetectionDeserializer odataVerboseJsonPayloadKindDetectionDeserializer = new ODataVerboseJsonPayloadKindDetectionDeserializer(this);
			return odataVerboseJsonPayloadKindDetectionDeserializer.DetectPayloadKind();
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00041B7C File Offset: 0x0003FD7C
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

		// Token: 0x0600114B RID: 4427 RVA: 0x00041BC0 File Offset: 0x0003FDC0
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

		// Token: 0x0600114C RID: 4428 RVA: 0x00041BFC File Offset: 0x0003FDFC
		private static void VerifyCanCreateParameterReader(IEdmFunctionImport functionImport)
		{
			if (functionImport == null)
			{
				throw new ArgumentNullException("functionImport", Strings.ODataJsonInputContext_FunctionImportCannotBeNullForCreateParameterReader("functionImport"));
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00041C16 File Offset: 0x0003FE16
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataVerboseJsonReader(this, entitySet, expectedBaseEntityType, true, null);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00041C22 File Offset: 0x0003FE22
		private ODataReader CreateEntryReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return new ODataVerboseJsonReader(this, entitySet, expectedEntityType, false, null);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00041C2E File Offset: 0x0003FE2E
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataVerboseJsonCollectionReader(this, expectedItemTypeReference, null);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00041C38 File Offset: 0x0003FE38
		private ODataParameterReader CreateParameterReaderImplementation(IEdmFunctionImport functionImport)
		{
			return new ODataVerboseJsonParameterReader(this, functionImport);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00041C44 File Offset: 0x0003FE44
		private ODataProperty ReadPropertyImplementation(IEdmStructuralProperty Property, IEdmTypeReference expectedPropertyTypeReference)
		{
			ODataVerboseJsonPropertyAndValueDeserializer odataVerboseJsonPropertyAndValueDeserializer = new ODataVerboseJsonPropertyAndValueDeserializer(this);
			return odataVerboseJsonPropertyAndValueDeserializer.ReadTopLevelProperty(Property, expectedPropertyTypeReference);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00041C60 File Offset: 0x0003FE60
		private ODataWorkspace ReadServiceDocumentImplementation()
		{
			ODataVerboseJsonServiceDocumentDeserializer odataVerboseJsonServiceDocumentDeserializer = new ODataVerboseJsonServiceDocumentDeserializer(this);
			return odataVerboseJsonServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00041C7C File Offset: 0x0003FE7C
		private ODataError ReadErrorImplementation()
		{
			ODataVerboseJsonErrorDeserializer odataVerboseJsonErrorDeserializer = new ODataVerboseJsonErrorDeserializer(this);
			return odataVerboseJsonErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00041C98 File Offset: 0x0003FE98
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation()
		{
			ODataVerboseJsonEntityReferenceLinkDeserializer odataVerboseJsonEntityReferenceLinkDeserializer = new ODataVerboseJsonEntityReferenceLinkDeserializer(this);
			return odataVerboseJsonEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00041CB4 File Offset: 0x0003FEB4
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation()
		{
			ODataVerboseJsonEntityReferenceLinkDeserializer odataVerboseJsonEntityReferenceLinkDeserializer = new ODataVerboseJsonEntityReferenceLinkDeserializer(this);
			return odataVerboseJsonEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x04000691 RID: 1681
		private TextReader textReader;

		// Token: 0x04000692 RID: 1682
		private BufferingJsonReader jsonReader;
	}
}
