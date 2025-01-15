using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000FB RID: 251
	internal sealed class ODataAtomOutputContext : ODataOutputContext
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x00017478 File Offset: 0x00015678
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
				this.xmlRootWriter = ODataAtomWriterUtils.CreateXmlWriter(stream, messageWriterSettings, encoding);
				this.xmlWriter = this.xmlRootWriter;
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

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00017504 File Offset: 0x00015704
		internal XmlWriter XmlWriter
		{
			get
			{
				return this.xmlWriter;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001750C File Offset: 0x0001570C
		internal AtomAndVerboseJsonTypeNameOracle TypeNameOracle
		{
			get
			{
				return this.typeNameOracle;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00017514 File Offset: 0x00015714
		internal void VerifyNotDisposed()
		{
			if (this.messageOutputStream == null)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001752F File Offset: 0x0001572F
		internal void Flush()
		{
			this.xmlWriter.Flush();
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0001753C File Offset: 0x0001573C
		internal override void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			this.WriteInStreamErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0001754C File Offset: 0x0001574C
		internal override ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataFeedWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00017556 File Offset: 0x00015756
		internal override ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			return this.CreateODataEntryWriterImplementation(entitySet, entityType);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00017560 File Offset: 0x00015760
		internal override ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			return this.CreateODataCollectionWriterImplementation(itemTypeReference);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00017569 File Offset: 0x00015769
		internal override void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			this.WriteServiceDocumentImplementation(defaultWorkspace);
			this.Flush();
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00017578 File Offset: 0x00015778
		internal override void WriteProperty(ODataProperty property)
		{
			this.WritePropertyImplementation(property);
			this.Flush();
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00017587 File Offset: 0x00015787
		internal override void WriteError(ODataError error, bool includeDebugInformation)
		{
			this.WriteErrorImplementation(error, includeDebugInformation);
			this.Flush();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00017597 File Offset: 0x00015797
		internal override void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinksImplementation(links);
			this.Flush();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000175A6 File Offset: 0x000157A6
		internal override void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			this.WriteEntityReferenceLinkImplementation(link);
			this.Flush();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x000175B5 File Offset: 0x000157B5
		internal void InitializeWriterCustomization()
		{
			this.xmlCustomizationWriters = new Stack<XmlWriter>();
			this.xmlCustomizationWriters.Push(this.xmlRootWriter);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000175D3 File Offset: 0x000157D3
		internal void PushCustomWriter(XmlWriter customXmlWriter)
		{
			this.xmlCustomizationWriters.Push(customXmlWriter);
			this.xmlWriter = customXmlWriter;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x000175E8 File Offset: 0x000157E8
		internal XmlWriter PopCustomWriter()
		{
			XmlWriter xmlWriter = this.xmlCustomizationWriters.Pop();
			this.xmlWriter = this.xmlCustomizationWriters.Peek();
			return xmlWriter;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00017614 File Offset: 0x00015814
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			try
			{
				if (this.messageOutputStream != null)
				{
					this.xmlRootWriter.Flush();
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
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001768C File Offset: 0x0001588C
		private void WriteInStreamErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			if (this.outputInStreamErrorListener != null)
			{
				this.outputInStreamErrorListener.OnInStreamError();
			}
			ODataAtomWriterUtils.WriteError(this.xmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000176C0 File Offset: 0x000158C0
		private ODataWriter CreateODataFeedWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataAtomWriter odataAtomWriter = new ODataAtomWriter(this, entitySet, entityType, true);
			this.outputInStreamErrorListener = odataAtomWriter;
			return odataAtomWriter;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000176E0 File Offset: 0x000158E0
		private ODataWriter CreateODataEntryWriterImplementation(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			ODataAtomWriter odataAtomWriter = new ODataAtomWriter(this, entitySet, entityType, false);
			this.outputInStreamErrorListener = odataAtomWriter;
			return odataAtomWriter;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00017700 File Offset: 0x00015900
		private ODataCollectionWriter CreateODataCollectionWriterImplementation(IEdmTypeReference itemTypeReference)
		{
			ODataAtomCollectionWriter odataAtomCollectionWriter = new ODataAtomCollectionWriter(this, itemTypeReference);
			this.outputInStreamErrorListener = odataAtomCollectionWriter;
			return odataAtomCollectionWriter;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00017720 File Offset: 0x00015920
		private void WritePropertyImplementation(ODataProperty property)
		{
			ODataAtomPropertyAndValueSerializer odataAtomPropertyAndValueSerializer = new ODataAtomPropertyAndValueSerializer(this);
			odataAtomPropertyAndValueSerializer.WriteTopLevelProperty(property);
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0001773C File Offset: 0x0001593C
		private void WriteServiceDocumentImplementation(ODataWorkspace defaultWorkspace)
		{
			ODataAtomServiceDocumentSerializer odataAtomServiceDocumentSerializer = new ODataAtomServiceDocumentSerializer(this);
			odataAtomServiceDocumentSerializer.WriteServiceDocument(defaultWorkspace);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00017758 File Offset: 0x00015958
		private void WriteErrorImplementation(ODataError error, bool includeDebugInformation)
		{
			ODataAtomSerializer odataAtomSerializer = new ODataAtomSerializer(this);
			odataAtomSerializer.WriteTopLevelError(error, includeDebugInformation);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00017774 File Offset: 0x00015974
		private void WriteEntityReferenceLinksImplementation(ODataEntityReferenceLinks links)
		{
			ODataAtomEntityReferenceLinkSerializer odataAtomEntityReferenceLinkSerializer = new ODataAtomEntityReferenceLinkSerializer(this);
			odataAtomEntityReferenceLinkSerializer.WriteEntityReferenceLinks(links);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00017790 File Offset: 0x00015990
		private void WriteEntityReferenceLinkImplementation(ODataEntityReferenceLink link)
		{
			ODataAtomEntityReferenceLinkSerializer odataAtomEntityReferenceLinkSerializer = new ODataAtomEntityReferenceLinkSerializer(this);
			odataAtomEntityReferenceLinkSerializer.WriteEntityReferenceLink(link);
		}

		// Token: 0x04000290 RID: 656
		private readonly AtomAndVerboseJsonTypeNameOracle typeNameOracle = new AtomAndVerboseJsonTypeNameOracle();

		// Token: 0x04000291 RID: 657
		private Stream messageOutputStream;

		// Token: 0x04000292 RID: 658
		private AsyncBufferedStream asynchronousOutputStream;

		// Token: 0x04000293 RID: 659
		private XmlWriter xmlRootWriter;

		// Token: 0x04000294 RID: 660
		private XmlWriter xmlWriter;

		// Token: 0x04000295 RID: 661
		private Stack<XmlWriter> xmlCustomizationWriters;

		// Token: 0x04000296 RID: 662
		private IODataOutputInStreamErrorListener outputInStreamErrorListener;
	}
}
