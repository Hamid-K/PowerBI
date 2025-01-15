using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x0200004F RID: 79
	public abstract class ODataInputContext : IDisposable
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000BE48 File Offset: 0x0000A048
		protected ODataInputContext(ODataFormat format, ODataMessageReaderSettings messageReaderSettings, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			this.format = format;
			this.messageReaderSettings = messageReaderSettings;
			this.readingResponse = readingResponse;
			this.synchronous = synchronous;
			this.model = model ?? EdmCoreModel.Instance;
			this.urlResolver = urlResolver;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.Model, this.MessageReaderSettings.ReaderBehavior);
			this.payloadValueConverter = this.model.GetPayloadValueConverter();
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		public bool ReadingResponse
		{
			get
			{
				return this.readingResponse;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000BEEC File Offset: 0x0000A0EC
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000BEF4 File Offset: 0x0000A0F4
		public IODataUrlResolver UrlResolver
		{
			get
			{
				return this.urlResolver;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000BF04 File Offset: 0x0000A104
		internal ODataPayloadValueConverter PayloadValueConverter
		{
			get
			{
				return this.payloadValueConverter;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000BF0C File Offset: 0x0000A10C
		protected internal bool UseServerFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.ODataServer;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000BF21 File Offset: 0x0000A121
		protected internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000BF36 File Offset: 0x0000A136
		protected internal bool UseClientApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000BF4B File Offset: 0x0000A14B
		protected internal bool UseServerApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.ODataServer;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000BF60 File Offset: 0x0000A160
		protected internal bool UseDefaultApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000BF75 File Offset: 0x0000A175
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.disposed = true;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000BF94 File Offset: 0x0000A194
		public virtual ODataReader CreateFeedReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000BF9D File Offset: 0x0000A19D
		public virtual ODataReader CreateEntryReader(IEdmNavigationSource navigationSource, IEdmEntityType expectedEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000BFA6 File Offset: 0x0000A1A6
		public virtual ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000BFAF File Offset: 0x0000A1AF
		public virtual ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public virtual ODataError ReadError()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000BFC2 File Offset: 0x0000A1C2
		public virtual ODataParameterReader CreateParameterReader(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		internal virtual ODataAsynchronousReader CreateAsynchronousReader()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000BFD6 File Offset: 0x0000A1D6
		internal virtual ODataDeltaReader CreateDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000BFDF File Offset: 0x0000A1DF
		internal virtual ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000BFE9 File Offset: 0x0000A1E9
		internal virtual ODataServiceDocument ReadServiceDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000BFF2 File Offset: 0x0000A1F2
		internal virtual IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		internal virtual ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C005 File Offset: 0x0000A205
		internal virtual ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C00E File Offset: 0x0000A20E
		internal virtual object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000C017 File Offset: 0x0000A217
		internal void VerifyNotDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C032 File Offset: 0x0000A232
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C034 File Offset: 0x0000A234
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C036 File Offset: 0x0000A236
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return new DuplicatePropertyNamesChecker(this.MessageReaderSettings.ReaderBehavior.AllowDuplicatePropertyNames, this.ReadingResponse, !this.messageReaderSettings.EnableFullValidation);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C061 File Offset: 0x0000A261
		internal Uri ResolveUri(Uri baseUri, Uri payloadUri)
		{
			if (this.UrlResolver != null)
			{
				return this.UrlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C07A File Offset: 0x0000A27A
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C07C File Offset: 0x0000A27C
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataInputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x04000183 RID: 387
		private readonly ODataFormat format;

		// Token: 0x04000184 RID: 388
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x04000185 RID: 389
		private readonly bool readingResponse;

		// Token: 0x04000186 RID: 390
		private readonly bool synchronous;

		// Token: 0x04000187 RID: 391
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x04000188 RID: 392
		private readonly IEdmModel model;

		// Token: 0x04000189 RID: 393
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x0400018A RID: 394
		private readonly ODataPayloadValueConverter payloadValueConverter;

		// Token: 0x0400018B RID: 395
		private bool disposed;
	}
}
