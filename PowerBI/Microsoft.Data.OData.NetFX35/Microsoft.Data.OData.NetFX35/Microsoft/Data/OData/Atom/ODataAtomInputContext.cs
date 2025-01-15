using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000228 RID: 552
	internal sealed class ODataAtomInputContext : ODataInputContext
	{
		// Token: 0x06001071 RID: 4209 RVA: 0x0003E05C File Offset: 0x0003C25C
		internal ODataAtomInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, version, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
				this.baseXmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, base.UseServerFormatBehavior && base.Version < ODataVersion.V3, messageReaderSettings.MessageQuotas.MaxNestingDepth, messageReaderSettings.ReaderBehavior.ODataNamespace);
			}
			catch (Exception ex)
			{
				if (ExceptionUtils.IsCatchableExceptionType(ex) && messageStream != null)
				{
					messageStream.Dispose();
				}
				throw;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0003E110 File Offset: 0x0003C310
		internal BufferingXmlReader XmlReader
		{
			get
			{
				return this.xmlReader;
			}
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0003E118 File Offset: 0x0003C318
		internal override ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0003E122 File Offset: 0x0003C322
		internal override ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return this.CreateEntryReaderImplementation(entitySet, expectedEntityType);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003E12C File Offset: 0x0003C32C
		internal override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003E135 File Offset: 0x0003C335
		internal override ODataWorkspace ReadServiceDocument()
		{
			return this.ReadServiceDocumentImplementation();
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003E13D File Offset: 0x0003C33D
		internal override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return this.ReadPropertyImplementation(property, expectedPropertyTypeReference);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0003E147 File Offset: 0x0003C347
		internal override ODataError ReadError()
		{
			return this.ReadErrorImplementation();
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003E14F File Offset: 0x0003C34F
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinksImplementation();
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003E157 File Offset: 0x0003C357
		internal override ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			return this.ReadEntityReferenceLinkImplementation();
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0003E160 File Offset: 0x0003C360
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			ODataAtomPayloadKindDetectionDeserializer odataAtomPayloadKindDetectionDeserializer = new ODataAtomPayloadKindDetectionDeserializer(this);
			return odataAtomPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003E17B File Offset: 0x0003C37B
		internal void InitializeReaderCustomization()
		{
			this.xmlCustomizationReaders = new Stack<BufferingXmlReader>();
			this.xmlCustomizationReaders.Push(this.xmlReader);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003E19C File Offset: 0x0003C39C
		internal void PushCustomReader(XmlReader customXmlReader, Uri xmlBaseUri)
		{
			if (!object.ReferenceEquals(this.xmlReader, customXmlReader))
			{
				BufferingXmlReader bufferingXmlReader = new BufferingXmlReader(customXmlReader, xmlBaseUri, base.MessageReaderSettings.BaseUri, false, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth, base.MessageReaderSettings.ReaderBehavior.ODataNamespace);
				this.xmlCustomizationReaders.Push(bufferingXmlReader);
				this.xmlReader = bufferingXmlReader;
				return;
			}
			this.xmlCustomizationReaders.Push(this.xmlReader);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003E210 File Offset: 0x0003C410
		internal BufferingXmlReader PopCustomReader()
		{
			BufferingXmlReader bufferingXmlReader = this.xmlCustomizationReaders.Pop();
			this.xmlReader = this.xmlCustomizationReaders.Peek();
			return bufferingXmlReader;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0003E23C File Offset: 0x0003C43C
		protected override void DisposeImplementation()
		{
			try
			{
				if (this.baseXmlReader != null)
				{
					this.baseXmlReader.Dispose();
				}
			}
			finally
			{
				this.baseXmlReader = null;
				this.xmlReader = null;
			}
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0003E280 File Offset: 0x0003C480
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataAtomReader(this, entitySet, expectedBaseEntityType, true);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0003E28B File Offset: 0x0003C48B
		private ODataReader CreateEntryReaderImplementation(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			return new ODataAtomReader(this, entitySet, expectedEntityType, false);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x0003E296 File Offset: 0x0003C496
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataAtomCollectionReader(this, expectedItemTypeReference);
		}

		// Token: 0x06001083 RID: 4227 RVA: 0x0003E2A0 File Offset: 0x0003C4A0
		private ODataProperty ReadPropertyImplementation(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			ODataAtomPropertyAndValueDeserializer odataAtomPropertyAndValueDeserializer = new ODataAtomPropertyAndValueDeserializer(this);
			return odataAtomPropertyAndValueDeserializer.ReadTopLevelProperty(property, expectedPropertyTypeReference);
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		private ODataWorkspace ReadServiceDocumentImplementation()
		{
			ODataAtomServiceDocumentDeserializer odataAtomServiceDocumentDeserializer = new ODataAtomServiceDocumentDeserializer(this);
			return odataAtomServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0003E2D8 File Offset: 0x0003C4D8
		private ODataError ReadErrorImplementation()
		{
			ODataAtomErrorDeserializer odataAtomErrorDeserializer = new ODataAtomErrorDeserializer(this);
			return odataAtomErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation()
		{
			ODataAtomEntityReferenceLinkDeserializer odataAtomEntityReferenceLinkDeserializer = new ODataAtomEntityReferenceLinkDeserializer(this);
			return odataAtomEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0003E310 File Offset: 0x0003C510
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation()
		{
			ODataAtomEntityReferenceLinkDeserializer odataAtomEntityReferenceLinkDeserializer = new ODataAtomEntityReferenceLinkDeserializer(this);
			return odataAtomEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x0400065D RID: 1629
		private XmlReader baseXmlReader;

		// Token: 0x0400065E RID: 1630
		private BufferingXmlReader xmlReader;

		// Token: 0x0400065F RID: 1631
		private Stack<BufferingXmlReader> xmlCustomizationReaders;
	}
}
