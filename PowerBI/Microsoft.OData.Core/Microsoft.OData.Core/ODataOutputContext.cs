using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000A1 RID: 161
	public abstract class ODataOutputContext : IDisposable
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00010868 File Offset: 0x0000EA68
		protected ODataOutputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageWriterSettings messageWriterSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageWriterSettings>(messageWriterSettings, "messageWriterSettings");
			this.format = format;
			this.messageWriterSettings = messageWriterSettings;
			this.writingResponse = messageInfo.IsResponse;
			this.synchronous = !messageInfo.IsAsync;
			this.model = messageInfo.Model ?? EdmCoreModel.Instance;
			this.payloadUriConverter = messageInfo.PayloadUriConverter;
			this.container = messageInfo.Container;
			this.edmTypeResolver = EdmTypeWriterResolver.Instance;
			this.payloadValueConverter = ODataPayloadValueConverter.GetPayloadValueConverter(this.container);
			this.writerValidator = messageWriterSettings.Validator;
			this.odataSimplifiedOptions = ODataSimplifiedOptions.GetODataSimplifiedOptions(this.container, messageWriterSettings.Version);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00010928 File Offset: 0x0000EB28
		public ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.messageWriterSettings;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00010930 File Offset: 0x0000EB30
		public bool WritingResponse
		{
			get
			{
				return this.writingResponse;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00010938 File Offset: 0x0000EB38
		public bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00010940 File Offset: 0x0000EB40
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00010948 File Offset: 0x0000EB48
		public IODataPayloadUriConverter PayloadUriConverter
		{
			get
			{
				return this.payloadUriConverter;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00010950 File Offset: 0x0000EB50
		internal IServiceProvider Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00010958 File Offset: 0x0000EB58
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00010960 File Offset: 0x0000EB60
		internal ODataPayloadValueConverter PayloadValueConverter
		{
			get
			{
				return this.payloadValueConverter;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00010968 File Offset: 0x0000EB68
		internal IWriterValidator WriterValidator
		{
			get
			{
				return this.writerValidator;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00010970 File Offset: 0x0000EB70
		internal ODataSimplifiedOptions ODataSimplifiedOptions
		{
			get
			{
				return this.odataSimplifiedOptions;
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00010978 File Offset: 0x0000EB78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00010987 File Offset: 0x0000EB87
		public virtual ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00010987 File Offset: 0x0000EB87
		public virtual Task<ODataWriter> CreateODataResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmStructuredType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00010987 File Offset: 0x0000EB87
		public virtual ODataWriter CreateODataDeltaResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00010987 File Offset: 0x0000EB87
		public virtual Task<ODataWriter> CreateODataDeltaResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmStructuredType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00010990 File Offset: 0x0000EB90
		public virtual ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00010990 File Offset: 0x0000EB90
		public virtual Task<ODataWriter> CreateODataResourceWriterAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00010999 File Offset: 0x0000EB99
		public virtual ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00010999 File Offset: 0x0000EB99
		public virtual Task<ODataCollectionWriter> CreateODataCollectionWriterAsync(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00010990 File Offset: 0x0000EB90
		public virtual ODataWriter CreateODataUriParameterResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00010990 File Offset: 0x0000EB90
		public virtual Task<ODataWriter> CreateODataUriParameterResourceWriterAsync(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00010987 File Offset: 0x0000EB87
		public virtual ODataWriter CreateODataUriParameterResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00010987 File Offset: 0x0000EB87
		public virtual Task<ODataWriter> CreateODataUriParameterResourceSetWriterAsync(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000109A2 File Offset: 0x0000EBA2
		public virtual ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000109A2 File Offset: 0x0000EBA2
		public virtual Task<ODataParameterWriter> CreateODataParameterWriterAsync(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x000109AC File Offset: 0x0000EBAC
		public virtual void WriteProperty(ODataProperty odataProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000109AC File Offset: 0x0000EBAC
		public virtual Task WritePropertyAsync(ODataProperty odataProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000109A2 File Offset: 0x0000EBA2
		public virtual void WriteError(ODataError odataError, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000109A2 File Offset: 0x0000EBA2
		public virtual Task WriteErrorAsync(ODataError odataError, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000109A2 File Offset: 0x0000EBA2
		internal virtual void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000109A2 File Offset: 0x0000EBA2
		internal virtual Task WriteInStreamErrorAsync(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x000109B5 File Offset: 0x0000EBB5
		internal virtual ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000109B5 File Offset: 0x0000EBB5
		internal virtual Task<ODataAsynchronousWriter> CreateODataAsynchronousWriterAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00010987 File Offset: 0x0000EB87
		internal virtual ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00010987 File Offset: 0x0000EB87
		internal virtual Task<ODataDeltaWriter> CreateODataDeltaWriterAsync(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000109BF File Offset: 0x0000EBBF
		internal virtual ODataBatchWriter CreateODataBatchWriter()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000109BF File Offset: 0x0000EBBF
		internal virtual Task<ODataBatchWriter> CreateODataBatchWriterAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000109C9 File Offset: 0x0000EBC9
		internal virtual void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000109C9 File Offset: 0x0000EBC9
		internal virtual Task WriteServiceDocumentAsync(ODataServiceDocument serviceDocument)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000109D2 File Offset: 0x0000EBD2
		internal virtual void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000109D2 File Offset: 0x0000EBD2
		internal virtual Task WriteEntityReferenceLinksAsync(ODataEntityReferenceLinks links)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000109DB File Offset: 0x0000EBDB
		internal virtual void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000109DB File Offset: 0x0000EBDB
		internal virtual Task WriteEntityReferenceLinkAsync(ODataEntityReferenceLink link)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000109E4 File Offset: 0x0000EBE4
		internal virtual void WriteValue(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000109E4 File Offset: 0x0000EBE4
		internal virtual Task WriteValueAsync(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x000109ED File Offset: 0x0000EBED
		internal virtual void WriteMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000239D File Offset: 0x0000059D
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000109F7 File Offset: 0x0000EBF7
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataOutputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x040002AA RID: 682
		private readonly ODataFormat format;

		// Token: 0x040002AB RID: 683
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x040002AC RID: 684
		private readonly bool writingResponse;

		// Token: 0x040002AD RID: 685
		private readonly bool synchronous;

		// Token: 0x040002AE RID: 686
		private readonly IEdmModel model;

		// Token: 0x040002AF RID: 687
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x040002B0 RID: 688
		private readonly IServiceProvider container;

		// Token: 0x040002B1 RID: 689
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040002B2 RID: 690
		private readonly ODataPayloadValueConverter payloadValueConverter;

		// Token: 0x040002B3 RID: 691
		private readonly IWriterValidator writerValidator;

		// Token: 0x040002B4 RID: 692
		private readonly ODataSimplifiedOptions odataSimplifiedOptions;
	}
}
