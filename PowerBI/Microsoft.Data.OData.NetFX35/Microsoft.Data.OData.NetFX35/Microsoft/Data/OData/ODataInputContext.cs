using System;
using System.Diagnostics;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000193 RID: 403
	internal abstract class ODataInputContext : IDisposable
	{
		// Token: 0x06000B3E RID: 2878 RVA: 0x00028138 File Offset: 0x00026338
		protected ODataInputContext(ODataFormat format, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool readingResponse, bool synchronous, IEdmModel model, IODataUrlResolver urlResolver)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			this.format = format;
			this.messageReaderSettings = messageReaderSettings;
			this.version = version;
			this.readingResponse = readingResponse;
			this.synchronous = synchronous;
			this.model = model;
			this.urlResolver = urlResolver;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.Model, this.MessageReaderSettings.ReaderBehavior, this.Version);
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x000281B8 File Offset: 0x000263B8
		internal ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000281C0 File Offset: 0x000263C0
		internal ODataVersion Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x000281C8 File Offset: 0x000263C8
		internal bool ReadingResponse
		{
			get
			{
				return this.readingResponse;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x000281D0 File Offset: 0x000263D0
		internal bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000281D8 File Offset: 0x000263D8
		internal IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000281E0 File Offset: 0x000263E0
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x000281E8 File Offset: 0x000263E8
		internal IODataUrlResolver UrlResolver
		{
			get
			{
				return this.urlResolver;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x000281F0 File Offset: 0x000263F0
		protected internal bool UseClientFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00028205 File Offset: 0x00026405
		protected internal bool UseServerFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0002821A File Offset: 0x0002641A
		protected internal bool UseDefaultFormatBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.FormatBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0002822F File Offset: 0x0002642F
		protected internal bool UseClientApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesClient;
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00028244 File Offset: 0x00026444
		protected internal bool UseServerApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.WcfDataServicesServer;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x00028259 File Offset: 0x00026459
		protected internal bool UseDefaultApiBehavior
		{
			get
			{
				return this.messageReaderSettings.ReaderBehavior.ApiBehaviorKind == ODataBehaviorKind.Default;
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002826E File Offset: 0x0002646E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002827D File Offset: 0x0002647D
		internal virtual ODataReader CreateFeedReader(IEdmEntitySet entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Feed);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00028286 File Offset: 0x00026486
		internal virtual ODataReader CreateEntryReader(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Entry);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002828F File Offset: 0x0002648F
		internal virtual ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00028298 File Offset: 0x00026498
		internal virtual ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000282A2 File Offset: 0x000264A2
		internal virtual ODataParameterReader CreateParameterReader(IEdmFunctionImport functionImport)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000282AC File Offset: 0x000264AC
		internal virtual ODataWorkspace ReadServiceDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000282B5 File Offset: 0x000264B5
		internal virtual IEdmModel ReadMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000282BF File Offset: 0x000264BF
		internal virtual ODataProperty ReadProperty(IEdmStructuralProperty property, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000282C8 File Offset: 0x000264C8
		internal virtual ODataError ReadError()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000282D2 File Offset: 0x000264D2
		internal virtual ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000282DB File Offset: 0x000264DB
		internal virtual ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000282E4 File Offset: 0x000264E4
		internal virtual object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000282ED File Offset: 0x000264ED
		internal void VerifyNotDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00028308 File Offset: 0x00026508
		[Conditional("DEBUG")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0002830A File Offset: 0x0002650A
		[Conditional("DEBUG")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0002830C File Offset: 0x0002650C
		internal DuplicatePropertyNamesChecker CreateDuplicatePropertyNamesChecker()
		{
			return new DuplicatePropertyNamesChecker(this.MessageReaderSettings.ReaderBehavior.AllowDuplicatePropertyNames, this.ReadingResponse);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00028329 File Offset: 0x00026529
		internal Uri ResolveUri(Uri baseUri, Uri payloadUri)
		{
			if (this.UrlResolver != null)
			{
				return this.UrlResolver.ResolveUrl(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000B5E RID: 2910
		protected abstract void DisposeImplementation();

		// Token: 0x06000B5F RID: 2911 RVA: 0x00028342 File Offset: 0x00026542
		private void Dispose(bool disposing)
		{
			this.disposed = true;
			if (disposing)
			{
				this.DisposeImplementation();
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00028354 File Offset: 0x00026554
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataInputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x04000420 RID: 1056
		private readonly ODataFormat format;

		// Token: 0x04000421 RID: 1057
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x04000422 RID: 1058
		private readonly ODataVersion version;

		// Token: 0x04000423 RID: 1059
		private readonly bool readingResponse;

		// Token: 0x04000424 RID: 1060
		private readonly bool synchronous;

		// Token: 0x04000425 RID: 1061
		private readonly IODataUrlResolver urlResolver;

		// Token: 0x04000426 RID: 1062
		private readonly IEdmModel model;

		// Token: 0x04000427 RID: 1063
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000428 RID: 1064
		private bool disposed;
	}
}
