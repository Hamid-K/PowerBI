using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000052 RID: 82
	internal sealed class ODataAtomOutputContext : ODataOutputContext
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		[SuppressMessage("DataWeb.Usage", "AC0014", Justification = "Throws every time")]
		internal ODataAtomOutputContext(ODataFormat format, Stream messageStream, Encoding encoding, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
			: base(format, messageWriterSettings, writingResponse, synchronous, model, urlResolver)
		{
			try
			{
				this.messageOutputStream = messageStream;
				Stream stream;
				if (synchronous)
				{
					stream = messageStream;
				}
				else
				{
					this.asynchronousOutputStream = new AsyncBufferedStream(messageStream);
					stream = this.asynchronousOutputStream;
				}
				this.xmlWriter = ODataAtomWriterUtils.CreateXmlWriter(stream, messageWriterSettings, encoding);
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000C524 File Offset: 0x0000A724
		internal XmlWriter XmlWriter
		{
			get
			{
				return this.xmlWriter;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000C52C File Offset: 0x0000A72C
		internal AtomAndVerboseJsonTypeNameOracle TypeNameOracle
		{
			get
			{
				return this.typeNameOracle;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000C534 File Offset: 0x0000A734
		public override ODataWriter CreateODataFeedWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000C53E File Offset: 0x0000A73E
		public override ODataWriter CreateODataEntryWriter(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(navigationSource, entityType);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000C548 File Offset: 0x0000A748
		public override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000C551 File Offset: 0x0000A751
		public override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			this.Flush();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000C560 File Offset: 0x0000A760
		public override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000C570 File Offset: 0x0000A770
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000C58B File Offset: 0x0000A78B
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000C598 File Offset: 0x0000A798
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		internal override void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			this.WriteServiceDocumentImplementation(serviceDocument);
			this.Flush();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			this.Flush();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000C5C6 File Offset: 0x0000A7C6
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			this.Flush();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (this.messageOutputStream != null)
				{
					this.xmlWriter.Flush();
					if (this.asynchronousOutputStream != null)
					{
						this.asynchronousOutputStream.FlushSync();
						this.asynchronousOutputStream.Dispose();
					}
					this.messageOutputStream.Dispose();
				}
			}
			finally
			{
				this.messageOutputStream = null;
				this.asynchronousOutputStream = null;
				this.xmlWriter = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C650 File Offset: 0x0000A850
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			ODataAtomWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C684 File Offset: 0x0000A884
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			ODataAtomWriter odataAtomWriter = new ODataAtomWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataAtomWriter;
			return odataAtomWriter;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		private ODataWriter CreateODataEntryWriterImplementation(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			ODataAtomWriter odataAtomWriter = new ODataAtomWriter(this, navigationSource, entityType, false);
			this.outputInStreamErrorListener = odataAtomWriter;
			return odataAtomWriter;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataAtomCollectionWriter odataAtomCollectionWriter = new ODataAtomCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataAtomCollectionWriter;
			return odataAtomCollectionWriter;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataAtomPropertyAndValueSerializer odataAtomPropertyAndValueSerializer = new ODataAtomPropertyAndValueSerializer(this);
			odataAtomPropertyAndValueSerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C700 File Offset: 0x0000A900
		private void WriteServiceDocumentImplementation(ODataServiceDocument serviceDocument)
		{
			ODataAtomServiceDocumentSerializer odataAtomServiceDocumentSerializer = new ODataAtomServiceDocumentSerializer(this);
			odataAtomServiceDocumentSerializer.WriteServiceDocument(serviceDocument);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C71C File Offset: 0x0000A91C
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataAtomSerializer odataAtomSerializer = new ODataAtomSerializer(this);
			odataAtomSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C738 File Offset: 0x0000A938
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataAtomEntityReferenceLinkSerializer odataAtomEntityReferenceLinkSerializer = new ODataAtomEntityReferenceLinkSerializer(this);
			odataAtomEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C754 File Offset: 0x0000A954
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataAtomEntityReferenceLinkSerializer odataAtomEntityReferenceLinkSerializer = new ODataAtomEntityReferenceLinkSerializer(this);
			odataAtomEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x04000197 RID: 407
		private readonly AtomAndVerboseJsonTypeNameOracle typeNameOracle = new AtomAndVerboseJsonTypeNameOracle();

		// Token: 0x04000198 RID: 408
		private Stream messageOutputStream;

		// Token: 0x04000199 RID: 409
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x0400019A RID: 410
		private XmlWriter xmlWriter;

		// Token: 0x0400019B RID: 411
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;
	}
}
