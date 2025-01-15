using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000069 RID: 105
	public abstract class ODataInputContext : IDisposable
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000A460 File Offset: 0x00008660
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
			this.odataSimplifiedOptions = ODataSimplifiedOptions.GetODataSimplifiedOptions(this.container);
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000A52B File Offset: 0x0000872B
		public ODataMessageReaderSettings MessageReaderSettings
		{
			get
			{
				return this.messageReaderSettings;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000A533 File Offset: 0x00008733
		public bool ReadingResponse
		{
			get
			{
				return this.readingResponse;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000356 RID: 854 RVA: 0x0000A53B File Offset: 0x0000873B
		public bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000A543 File Offset: 0x00008743
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000A54B File Offset: 0x0000874B
		public IODataPayloadUriConverter PayloadUriConverter
		{
			get
			{
				return this.payloadUriConverter;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000A553 File Offset: 0x00008753
		internal IServiceProvider Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000A55B File Offset: 0x0000875B
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000A563 File Offset: 0x00008763
		internal ODataPayloadValueConverter PayloadValueConverter
		{
			get
			{
				return this.payloadValueConverter;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000A56B File Offset: 0x0000876B
		internal ODataSimplifiedOptions ODataSimplifiedOptions
		{
			get
			{
				return this.odataSimplifiedOptions;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A573 File Offset: 0x00008773
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

		// Token: 0x0600035E RID: 862 RVA: 0x0000A592 File Offset: 0x00008792
		public virtual ODataReader CreateResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A59B File Offset: 0x0000879B
		public virtual ODataReader CreateResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A5A4 File Offset: 0x000087A4
		public virtual ODataCollectionReader CreateCollectionReader(IEdmTypeReference expectedItemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A5AD File Offset: 0x000087AD
		public virtual ODataProperty ReadProperty(IEdmStructuralProperty edmStructuralProperty, IEdmTypeReference expectedPropertyTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000A5B6 File Offset: 0x000087B6
		public virtual ODataError ReadError()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public virtual ODataReader CreateUriParameterResourceReader(IEdmNavigationSource navigationSource, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A592 File Offset: 0x00008792
		public virtual ODataReader CreateUriParameterResourceSetReader(IEdmEntitySetBase entitySet, IEdmStructuredType expectedResourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A5C0 File Offset: 0x000087C0
		public virtual ODataParameterReader CreateParameterReader(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Parameter);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A5CA File Offset: 0x000087CA
		internal virtual ODataAsynchronousReader CreateAsynchronousReader()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A592 File Offset: 0x00008792
		internal virtual ODataDeltaReader CreateDeltaReader(IEdmEntitySetBase entitySet, IEdmEntityType expectedBaseEntityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A5D4 File Offset: 0x000087D4
		internal virtual ODataBatchReader CreateBatchReader(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A5DE File Offset: 0x000087DE
		internal virtual ODataServiceDocument ReadServiceDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A5E7 File Offset: 0x000087E7
		internal virtual IEdmModel ReadMetadataDocument(Func<Uri, XmlReader> getReferencedModelReaderFunc)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A5F1 File Offset: 0x000087F1
		internal virtual ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000A5FA File Offset: 0x000087FA
		internal virtual ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000A603 File Offset: 0x00008803
		internal virtual object ReadValue(IEdmPrimitiveTypeReference expectedPrimitiveTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000A60C File Offset: 0x0000880C
		internal void VerifyNotDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000A627 File Offset: 0x00008827
		internal PropertyAndAnnotationCollector CreatePropertyAndAnnotationCollector()
		{
			return this.messageReaderSettings.Validator.CreatePropertyAndAnnotationCollector();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000A639 File Offset: 0x00008839
		internal Uri ResolveUri(Uri baseUri, Uri payloadUri)
		{
			if (this.PayloadUriConverter != null)
			{
				return this.PayloadUriConverter.ConvertPayloadUri(baseUri, payloadUri);
			}
			return null;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000250D File Offset: 0x0000070D
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000A652 File Offset: 0x00008852
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataInputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x040001C7 RID: 455
		private readonly ODataFormat format;

		// Token: 0x040001C8 RID: 456
		private readonly ODataMessageReaderSettings messageReaderSettings;

		// Token: 0x040001C9 RID: 457
		private readonly bool readingResponse;

		// Token: 0x040001CA RID: 458
		private readonly bool synchronous;

		// Token: 0x040001CB RID: 459
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x040001CC RID: 460
		private readonly IServiceProvider container;

		// Token: 0x040001CD RID: 461
		private readonly IEdmModel model;

		// Token: 0x040001CE RID: 462
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040001CF RID: 463
		private readonly ODataPayloadValueConverter payloadValueConverter;

		// Token: 0x040001D0 RID: 464
		private readonly ODataSimplifiedOptions odataSimplifiedOptions;

		// Token: 0x040001D1 RID: 465
		private bool disposed;
	}
}
