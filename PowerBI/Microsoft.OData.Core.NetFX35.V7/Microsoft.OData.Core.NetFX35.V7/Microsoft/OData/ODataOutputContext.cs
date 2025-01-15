using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x0200007C RID: 124
	public abstract class ODataOutputContext : IDisposable
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000D428 File Offset: 0x0000B628
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
			this.odataSimplifiedOptions = ODataSimplifiedOptions.GetODataSimplifiedOptions(this.container);
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000D4E2 File Offset: 0x0000B6E2
		public ODataMessageWriterSettings MessageWriterSettings
		{
			get
			{
				return this.messageWriterSettings;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000D4EA File Offset: 0x0000B6EA
		public bool WritingResponse
		{
			get
			{
				return this.writingResponse;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000D4F2 File Offset: 0x0000B6F2
		public bool Synchronous
		{
			get
			{
				return this.synchronous;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000D4FA File Offset: 0x0000B6FA
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000D502 File Offset: 0x0000B702
		public IODataPayloadUriConverter PayloadUriConverter
		{
			get
			{
				return this.payloadUriConverter;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x0000D50A File Offset: 0x0000B70A
		internal IServiceProvider Container
		{
			get
			{
				return this.container;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000D512 File Offset: 0x0000B712
		internal EdmTypeResolver EdmTypeResolver
		{
			get
			{
				return this.edmTypeResolver;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0000D51A File Offset: 0x0000B71A
		internal ODataPayloadValueConverter PayloadValueConverter
		{
			get
			{
				return this.payloadValueConverter;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000D522 File Offset: 0x0000B722
		internal IWriterValidator WriterValidator
		{
			get
			{
				return this.writerValidator;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000D52A File Offset: 0x0000B72A
		internal ODataSimplifiedOptions ODataSimplifiedOptions
		{
			get
			{
				return this.odataSimplifiedOptions;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00002503 File Offset: 0x00000703
		internal virtual ODataContextUrlLevel ContextUrlLevel
		{
			get
			{
				return ODataContextUrlLevel.OnDemand;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000D532 File Offset: 0x0000B732
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000D541 File Offset: 0x0000B741
		public virtual ODataWriter CreateODataResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000D54A File Offset: 0x0000B74A
		public virtual ODataWriter CreateODataResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D553 File Offset: 0x0000B753
		public virtual ODataCollectionWriter CreateODataCollectionWriter(IEdmTypeReference itemTypeReference)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Collection);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000D54A File Offset: 0x0000B74A
		public virtual ODataWriter CreateODataUriParameterResourceWriter(IEdmNavigationSource navigationSource, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Resource);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000D541 File Offset: 0x0000B741
		public virtual ODataWriter CreateODataUriParameterResourceSetWriter(IEdmEntitySetBase entitySet, IEdmStructuredType resourceType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000D55C File Offset: 0x0000B75C
		public virtual ODataParameterWriter CreateODataParameterWriter(IEdmOperation operation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000D566 File Offset: 0x0000B766
		public virtual void WriteProperty(ODataProperty odataProperty)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Property);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000D55C File Offset: 0x0000B75C
		public virtual void WriteError(ODataError odataError, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000D55C File Offset: 0x0000B75C
		internal virtual void WriteInStreamError(ODataError error, bool includeDebugInformation)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Error);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000D56F File Offset: 0x0000B76F
		internal virtual ODataAsynchronousWriter CreateODataAsynchronousWriter()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Asynchronous);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000D541 File Offset: 0x0000B741
		internal virtual ODataDeltaWriter CreateODataDeltaWriter(IEdmEntitySetBase entitySet, IEdmEntityType entityType)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ResourceSet);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D579 File Offset: 0x0000B779
		internal virtual ODataBatchWriter CreateODataBatchWriter(string batchBoundary)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Batch);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D583 File Offset: 0x0000B783
		internal virtual void WriteServiceDocument(ODataServiceDocument serviceDocument)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.ServiceDocument);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D58C File Offset: 0x0000B78C
		internal virtual void WriteEntityReferenceLinks(ODataEntityReferenceLinks links)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLinks);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D595 File Offset: 0x0000B795
		internal virtual void WriteEntityReferenceLink(ODataEntityReferenceLink link)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.EntityReferenceLink);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000D59E File Offset: 0x0000B79E
		internal virtual void WriteValue(object value)
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.Value);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000D5A7 File Offset: 0x0000B7A7
		internal virtual void WriteMetadataDocument()
		{
			throw this.CreatePayloadKindNotSupportedException(ODataPayloadKind.MetadataDocument);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000D5B1 File Offset: 0x0000B7B1
		internal ODataContextUriBuilder CreateContextUriBuilder()
		{
			return ODataContextUriBuilder.Create(this.messageWriterSettings.MetadataDocumentUri, this.writingResponse && this.ContextUrlLevel > ODataContextUrlLevel.None);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertSynchronous()
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Needs to access this in debug only.")]
		internal void AssertAsynchronous()
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000250D File Offset: 0x0000070D
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000D5D7 File Offset: 0x0000B7D7
		private ODataException CreatePayloadKindNotSupportedException(ODataPayloadKind payloadKind)
		{
			return new ODataException(Strings.ODataOutputContext_UnsupportedPayloadKindForFormat(this.format.ToString(), payloadKind.ToString()));
		}

		// Token: 0x04000244 RID: 580
		private readonly ODataFormat format;

		// Token: 0x04000245 RID: 581
		private readonly ODataMessageWriterSettings messageWriterSettings;

		// Token: 0x04000246 RID: 582
		private readonly bool writingResponse;

		// Token: 0x04000247 RID: 583
		private readonly bool synchronous;

		// Token: 0x04000248 RID: 584
		private readonly IEdmModel model;

		// Token: 0x04000249 RID: 585
		private readonly IODataPayloadUriConverter payloadUriConverter;

		// Token: 0x0400024A RID: 586
		private readonly IServiceProvider container;

		// Token: 0x0400024B RID: 587
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x0400024C RID: 588
		private readonly ODataPayloadValueConverter payloadValueConverter;

		// Token: 0x0400024D RID: 589
		private readonly IWriterValidator writerValidator;

		// Token: 0x0400024E RID: 590
		private readonly ODataSimplifiedOptions odataSimplifiedOptions;
	}
}
