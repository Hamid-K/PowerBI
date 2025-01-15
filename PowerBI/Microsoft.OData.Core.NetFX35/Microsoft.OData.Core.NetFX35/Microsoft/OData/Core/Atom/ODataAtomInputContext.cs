using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000050 RID: 80
	internal sealed class ODataAtomInputContext : ODataInputContext
	{
		// Token: 0x060002FC RID: 764 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal ODataAtomInputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageReaderSettings messageReaderSettings, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageReaderSettings, readingResponse, synchronous, model, urlResolver)
		{
			try
			{
				ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
				ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
				this.baseXmlReader = ODataAtomReaderUtils.CreateXmlReader(messageStream, encoding, messageReaderSettings);
				this.xmlReader = new BufferingXmlReader(this.baseXmlReader, null, messageReaderSettings.BaseUri, false, messageReaderSettings.MessageQuotas.MaxNestingDepth);
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

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000C130 File Offset: 0x0000A330
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Not yet implemented.")]
		internal BufferingXmlReader XmlReader
		{
			get
			{
				return this.xmlReader;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C138 File Offset: 0x0000A338
		public override ODataReader CreateFeedReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return this.CreateFeedReaderImplementation(entitySet, expectedBaseEntityType);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C142 File Offset: 0x0000A342
		public override ODataReader CreateEntryReader(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			return this.CreateEntryReaderImplementation(navigationSource, expectedEntityType);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C14C File Offset: 0x0000A34C
		public override ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			return this.CreateCollectionReaderImplementation(expectedItemTypeReference);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C155 File Offset: 0x0000A355
		public override ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			return this.ReadPropertyImplementation(property, expectedPropertyTypeReference);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C15F File Offset: 0x0000A35F
		public override ODataError ReadError()
		{
			return this.ReadErrorImplementation();
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C167 File Offset: 0x0000A367
		internal override ODataServiceDocument ReadServiceDocument()
		{
			return this.ReadServiceDocumentImplementation();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C16F File Offset: 0x0000A36F
		internal override ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			return this.ReadEntityReferenceLinksImplementation();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C177 File Offset: 0x0000A377
		internal override ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			return this.ReadEntityReferenceLinkImplementation();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C180 File Offset: 0x0000A380
		internal IEnumerable<ODataPayloadKind> DetectPayloadKind(ODataPayloadKindDetectionInfo detectionInfo)
		{
			ODataAtomPayloadKindDetectionDeserializer odataAtomPayloadKindDetectionDeserializer = new ODataAtomPayloadKindDetectionDeserializer(this);
			return odataAtomPayloadKindDetectionDeserializer.DetectPayloadKind(detectionInfo);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C19C File Offset: 0x0000A39C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
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
			base.Dispose(disposing);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C1E8 File Offset: 0x0000A3E8
		private ODataReader CreateFeedReaderImplementation(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			return new ODataAtomReader(this, entitySet, expectedBaseEntityType, true);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C1F3 File Offset: 0x0000A3F3
		private ODataReader CreateEntryReaderImplementation(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			return new ODataAtomReader(this, navigationSource, expectedEntityType, false);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C1FE File Offset: 0x0000A3FE
		private ODataCollectionReader CreateCollectionReaderImplementation(IEdmTypeReference expectedItemTypeReference)
		{
			return new ODataAtomCollectionReader(this, expectedItemTypeReference);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C208 File Offset: 0x0000A408
		private ODataProperty ReadPropertyImplementation(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			ODataAtomPropertyAndValueDeserializer odataAtomPropertyAndValueDeserializer = new ODataAtomPropertyAndValueDeserializer(this);
			return odataAtomPropertyAndValueDeserializer.ReadTopLevelProperty(property, expectedPropertyTypeReference);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000C224 File Offset: 0x0000A424
		private ODataServiceDocument ReadServiceDocumentImplementation()
		{
			ODataAtomServiceDocumentDeserializer odataAtomServiceDocumentDeserializer = new ODataAtomServiceDocumentDeserializer(this);
			return odataAtomServiceDocumentDeserializer.ReadServiceDocument();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000C240 File Offset: 0x0000A440
		private ODataError ReadErrorImplementation()
		{
			ODataAtomErrorDeserializer odataAtomErrorDeserializer = new ODataAtomErrorDeserializer(this);
			return odataAtomErrorDeserializer.ReadTopLevelError();
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000C25C File Offset: 0x0000A45C
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation()
		{
			ODataAtomEntityReferenceLinkDeserializer odataAtomEntityReferenceLinkDeserializer = new ODataAtomEntityReferenceLinkDeserializer(this);
			return odataAtomEntityReferenceLinkDeserializer.ReadEntityReferenceLinks();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C278 File Offset: 0x0000A478
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation()
		{
			ODataAtomEntityReferenceLinkDeserializer odataAtomEntityReferenceLinkDeserializer = new ODataAtomEntityReferenceLinkDeserializer(this);
			return odataAtomEntityReferenceLinkDeserializer.ReadEntityReferenceLink();
		}

		// Token: 0x0400018C RID: 396
		private XmlReader baseXmlReader;

		// Token: 0x0400018D RID: 397
		private BufferingXmlReader xmlReader;
	}
}
