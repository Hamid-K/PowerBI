using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200008F RID: 143
	public abstract class ODataInputContext : IDisposable
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0000C800 File Offset: 0x0000AA00
		protected ODataInputContext(ODataFormat format, ODataMessageInfo messageInfo, ODataMessageReaderSettings messageReaderSettings)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFormat>(format, "format");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageInfo>(messageInfo, "messageInfo");
			ExceptionUtils.CheckArgumentNotNull<ODataMessageReaderSettings>(messageReaderSettings, "messageReaderSettings");
			this.format = format;
			this.messageReaderSettings = messageReaderSettings;
			this.readingResponse = messageInfo.IsResponse;
			this.synchronous = !messageInfo.IsAsync;
			this.model = messageInfo.Model ?? EdmCoreModel.Instance;
			this.payloadUriConverter = messageInfo.PayloadUriConverter;
			this.container = messageInfo.Container;
			this.edmTypeResolver = new EdmTypeReaderResolver(this.Model, this.MessageReaderSettings.ClientCustomTypeResolver);
			this.payloadValueConverter = ODataPayloadValueConverter.GetPayloadValueConverter(this.container);
			this.odataSimplifiedOptions = ODataSimplifiedOptions.GetODataSimplifiedOptions(this.container, messageReaderSettings.Version);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000C8D1 File Offset: 0x0000AAD1
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000C8D9 File Offset: 0x0000AAD9
		public bool ReadingResponse
		{
			get
			{
				return this.readingResponse;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000C8E1 File Offset: 0x0000AAE1
		public bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000C8E9 File Offset: 0x0000AAE9
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000C8F1 File Offset: 0x0000AAF1
		public IODataPayloadUriConverter PayloadUriConverter
		{
			get
			{
				return this.payloadUriConverter;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000C8F9 File Offset: 0x0000AAF9
		internal IServiceProvider Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000C901 File Offset: 0x0000AB01
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000C909 File Offset: 0x0000AB09
		internal ODataPayloadValueConverter PayloadValueConverter
		{
			get
			{
				return this.payloadValueConverter;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000C911 File Offset: 0x0000AB11
		internal ODataSimplifiedOptions ODataSimplifiedOptions
		{
			get
			{
				return this.odataSimplifiedOptions;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000C919 File Offset: 0x0000AB19
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

		// Token: 0x06000509 RID: 1289 RVA: 0x0000C938 File Offset: 0x0000AB38
		public virtual ODataReader CreateResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000C938 File Offset: 0x0000AB38
		public virtual Task<ODataReader> CreateResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000C938 File Offset: 0x0000AB38
		public virtual ODataReader CreateDeltaResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000C938 File Offset: 0x0000AB38
		public virtual Task<ODataReader> CreateDeltaResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000C941 File Offset: 0x0000AB41
		public virtual ODataReader CreateResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000C941 File Offset: 0x0000AB41
		public virtual Task<ODataReader> CreateResourceReaderAsync(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public virtual ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public virtual Task<ODataCollectionReader> CreateCollectionReaderAsync(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000C953 File Offset: 0x0000AB53
		public virtual ODataProperty ReadProperty(IEdmStructuralProperty edmStructuralProperty, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000C953 File Offset: 0x0000AB53
		public virtual Task<ODataProperty> ReadPropertyAsync(IEdmStructuralProperty edmStructuralProperty, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public virtual ODataError ReadError()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000C95C File Offset: 0x0000AB5C
		public virtual Task<ODataError> ReadErrorAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000C966 File Offset: 0x0000AB66
		public virtual ODataReader CreateUriParameterResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000C966 File Offset: 0x0000AB66
		public virtual Task<ODataReader> CreateUriParameterResourceReaderAsync(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000C938 File Offset: 0x0000AB38
		public virtual ODataReader CreateUriParameterResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000C938 File Offset: 0x0000AB38
		public virtual Task<ODataReader> CreateUriParameterResourceSetReaderAsync(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000C966 File Offset: 0x0000AB66
		public virtual ODataParameterReader CreateParameterReader(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000C966 File Offset: 0x0000AB66
		public virtual Task<ODataParameterReader> CreateParameterReaderAsync(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0000C970 File Offset: 0x0000AB70
		internal virtual ODataAsynchronousReader CreateAsynchronousReader()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0000C970 File Offset: 0x0000AB70
		internal virtual Task<ODataAsynchronousReader> CreateAsynchronousReaderAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0000C938 File Offset: 0x0000AB38
		internal virtual ODataDeltaReader CreateDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0000C938 File Offset: 0x0000AB38
		internal virtual Task<ODataDeltaReader> CreateDeltaReaderAsync(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0000C97A File Offset: 0x0000AB7A
		internal virtual ODataBatchReader CreateBatchReader()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0000C97A File Offset: 0x0000AB7A
		internal virtual Task<ODataBatchReader> CreateBatchReaderAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0000C984 File Offset: 0x0000AB84
		internal virtual ODataServiceDocument ReadServiceDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0000C984 File Offset: 0x0000AB84
		internal virtual Task<ODataServiceDocument> ReadServiceDocumentAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000C98D File Offset: 0x0000AB8D
		internal virtual IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000C997 File Offset: 0x0000AB97
		internal virtual ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000C997 File Offset: 0x0000AB97
		internal virtual Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		internal virtual ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		internal virtual Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0000C9A9 File Offset: 0x0000ABA9
		internal virtual object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0000C9A9 File Offset: 0x0000ABA9
		internal virtual Task<object> ReadValueAsync(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0000C9B2 File Offset: 0x0000ABB2
		internal void VerifyNotDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0000C9CD File Offset: 0x0000ABCD
		internal PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector()
		{
			return this.messageReaderSettings.Validator.CreatePropertyAndAnnotationCollector();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0000C9DF File Offset: 0x0000ABDF
		internal Uri ResolveUri(Uri baseUri, Uri payloadUri)
		{
			if (this.PayloadUriConverter != null)
			{
				return this.PayloadUriConverter.ConvertPayloadUri(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0000239D File Offset: 0x0000059D
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataInputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x04000228 RID: 552
		private readonly ODataFormat format;

		// Token: 0x04000229 RID: 553
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x0400022A RID: 554
		private readonly bool readingResponse;

		// Token: 0x0400022B RID: 555
		private readonly bool synchronous;

		// Token: 0x0400022C RID: 556
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x0400022D RID: 557
		private readonly IServiceProvider container;

		// Token: 0x0400022E RID: 558
		private readonly IEdmModel model;

		// Token: 0x0400022F RID: 559
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000230 RID: 560
		private readonly ODataPayloadValueConverter payloadValueConverter;

		// Token: 0x04000231 RID: 561
		private readonly ODataSimplifiedOptions odataSimplifiedOptions;

		// Token: 0x04000232 RID: 562
		private bool disposed;
	}
}
