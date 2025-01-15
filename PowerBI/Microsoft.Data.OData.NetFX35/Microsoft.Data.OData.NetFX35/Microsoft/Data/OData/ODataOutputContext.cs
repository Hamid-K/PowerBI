using System;
using System.Diagnostics;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020000FA RID: 250
	internal abstract class ODataOutputContext : IDisposable
	{
		// Token: 0x06000661 RID: 1633 RVA: 0x000172B4 File Offset: 0x000154B4
		protected ODataOutputContext(ODataFormat format, ODataMessageWriterSettings messageWriterSettings, bool writingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			this.format = format;
			this.messageWriterSettings = messageWriterSettings;
			this.writingResponse = writingResponse;
			this.synchronous = synchronous;
			this.model = model ?? EdmCoreModel.Instance;
			this.urlResolver = urlResolver;
			this.edmTypeResolver = EdmTypeWriterResolver.Instance;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001731E File Offset: 0x0001551E
		internal ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.messageWriterSettings;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00017328 File Offset: 0x00015528
		internal ODataVersion Version
		{
			get
			{
				return this.messageWriterSettings.Version.Value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00017348 File Offset: 0x00015548
		internal bool WritingResponse
		{
			get
			{
				return this.writingResponse;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x00017350 File Offset: 0x00015550
		internal bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00017358 File Offset: 0x00015558
		internal IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00017360 File Offset: 0x00015560
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.urlResolver;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00017368 File Offset: 0x00015568
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00017370 File Offset: 0x00015570
		protected internal bool UseClientFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00017385 File Offset: 0x00015585
		protected internal bool UseServerFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001739A File Offset: 0x0001559A
		protected internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x000173AF File Offset: 0x000155AF
		protected internal bool UseServerApiBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000173C4 File Offset: 0x000155C4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000173D3 File Offset: 0x000155D3
		internal virtual void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x000173DD File Offset: 0x000155DD
		internal virtual ODataWriter CreateODataFeedWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000173E6 File Offset: 0x000155E6
		internal virtual ODataWriter CreateODataEntryWriter(IEdmEntitySet entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000173EF File Offset: 0x000155EF
		internal virtual ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000173F8 File Offset: 0x000155F8
		internal virtual ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00017402 File Offset: 0x00015602
		internal virtual ODataParameterWriter CreateODataParameterWriter(IEdmFunctionImport functionImport)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001740C File Offset: 0x0001560C
		internal virtual void WriteServiceDocument(ODataWorkspace defaultWorkspace)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00017415 File Offset: 0x00015615
		internal virtual void WriteProperty(ODataProperty property)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001741E File Offset: 0x0001561E
		internal virtual void WriteError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00017428 File Offset: 0x00015628
		internal virtual void WriteEntityReferenceLinks(ODataEntityReferenceLinks links, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00017431 File Offset: 0x00015631
		internal virtual void WriteEntityReferenceLink(ODataEntityReferenceLink link, IEdmEntitySet entitySet, IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001743A File Offset: 0x0001563A
		internal virtual void WriteValue(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00017443 File Offset: 0x00015643
		internal virtual void WriteMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001744D File Offset: 0x0001564D
		[Conditional("DEBUG")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001744F File Offset: 0x0001564F
		[Conditional("DEBUG")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00017451 File Offset: 0x00015651
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00017453 File Offset: 0x00015653
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataOutputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x04000289 RID: 649
		private readonly ODataFormat format;

		// Token: 0x0400028A RID: 650
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x0400028B RID: 651
		private readonly bool writingResponse;

		// Token: 0x0400028C RID: 652
		private readonly bool synchronous;

		// Token: 0x0400028D RID: 653
		private readonly IEdmModel model;

		// Token: 0x0400028E RID: 654
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x0400028F RID: 655
		private readonly EdmTypeResolver edmTypeResolver;
	}
}
