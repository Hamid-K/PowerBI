using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x02000051 RID: 81
	public abstract class ODataOutputContext : IDisposable
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000C294 File Offset: 0x0000A494
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
			this.payloadValueConverter = this.model.GetPayloadValueConverter();
			this.writerValidator = ValidatorFactory.CreateWriterValidator(messageWriterSettings.EnableFullValidation);
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000C320 File Offset: 0x0000A520
		public ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.messageWriterSettings;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000312 RID: 786 RVA: 0x0000C328 File Offset: 0x0000A528
		public bool WritingResponse
		{
			get
			{
				return this.writingResponse;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000C330 File Offset: 0x0000A530
		public bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000C338 File Offset: 0x0000A538
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000C340 File Offset: 0x0000A540
		public IODataUrlResolver UrlResolver
		{
			get
			{
				return this.urlResolver;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000C348 File Offset: 0x0000A548
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000C350 File Offset: 0x0000A550
		internal ODataPayloadValueConverter PayloadValueConverter
		{
			get
			{
				return this.payloadValueConverter;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000C358 File Offset: 0x0000A558
		internal IWriterValidator WriterValidator
		{
			get
			{
				return this.writerValidator;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000C360 File Offset: 0x0000A560
		internal virtual ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.OnDemand;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000C363 File Offset: 0x0000A563
		protected internal bool UseClientFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000C378 File Offset: 0x0000A578
		protected internal bool UseServerFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.ODataServer;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000C38D File Offset: 0x0000A58D
		protected internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.FormatBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000C3A2 File Offset: 0x0000A5A2
		protected internal bool UseServerApiBehavior
		{
			get
			{
				return this.messageWriterSettings.WriterBehavior.ApiBehaviorKind == ODataBehaviorKind.ODataServer;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000C3B7 File Offset: 0x0000A5B7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C3C6 File Offset: 0x0000A5C6
		public virtual ODataWriter CreateODataFeedWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000C3CF File Offset: 0x0000A5CF
		public virtual ODataWriter CreateODataEntryWriter(IEdmNavigationSource navigationSource, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		public virtual ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
		public virtual ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000C3EB File Offset: 0x0000A5EB
		public virtual void WriteProperty(ODataProperty property)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		public virtual void WriteError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		internal virtual void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000C408 File Offset: 0x0000A608
		internal virtual ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000C412 File Offset: 0x0000A612
		internal virtual ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C41B File Offset: 0x0000A61B
		internal virtual ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C425 File Offset: 0x0000A625
		internal virtual void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000C42E File Offset: 0x0000A62E
		internal virtual void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000C437 File Offset: 0x0000A637
		internal virtual void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000C440 File Offset: 0x0000A640
		internal virtual void WriteValue(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000C449 File Offset: 0x0000A649
		internal virtual void WriteMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C453 File Offset: 0x0000A653
		internal ODataContextUriBuilder CreateContextUriBuilder()
		{
			return ODataContextUriBuilder.Create(this.messageWriterSettings.MetadataDocumentUri, this.writingResponse && this.ContextUrlLevel != ODataContextUrlLevel.None);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C47C File Offset: 0x0000A67C
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		[Conditional("DEBUG")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C47E File Offset: 0x0000A67E
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C480 File Offset: 0x0000A680
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C482 File Offset: 0x0000A682
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataOutputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x0400018E RID: 398
		private readonly ODataFormat format;

		// Token: 0x0400018F RID: 399
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x04000190 RID: 400
		private readonly bool writingResponse;

		// Token: 0x04000191 RID: 401
		private readonly bool synchronous;

		// Token: 0x04000192 RID: 402
		private readonly IEdmModel model;

		// Token: 0x04000193 RID: 403
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x04000194 RID: 404
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000195 RID: 405
		private readonly ODataPayloadValueConverter payloadValueConverter;

		// Token: 0x04000196 RID: 406
		private readonly IWriterValidator writerValidator;
	}
}
