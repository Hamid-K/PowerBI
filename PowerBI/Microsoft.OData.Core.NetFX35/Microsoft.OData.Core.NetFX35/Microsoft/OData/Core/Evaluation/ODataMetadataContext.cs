using System;
using System.Collections.Generic;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x0200008C RID: 140
	internal sealed class ODataMetadataContext : IODataMetadataContext
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x000145A9 File Offset: 0x000127A9
		public ODataMetadataContext(bool isResponse, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri)
			: this(isResponse, null, EdmTypeWriterResolver.Instance, model, metadataDocumentUri, odataUri)
		{
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x000145BC File Offset: 0x000127BC
		public ODataMetadataContext(bool isResponse, Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri)
		{
			this.isResponse = isResponse;
			this.operationsBoundToEntityTypeMustBeContainerQualified = operationsBoundToEntityTypeMustBeContainerQualified ?? new Func<IEdmEntityType, bool>(EdmLibraryExtensions.OperationsBoundToEntityTypeMustBeContainerQualified);
			this.edmTypeResolver = edmTypeResolver;
			this.model = model;
			this.metadataDocumentUri = metadataDocumentUri;
			this.bindableOperationsCache = new Dictionary<IEdmType, IEdmOperation[]>(ReferenceEqualityComparer<IEdmType>.Instance);
			this.odataUri = odataUri;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001461C File Offset: 0x0001281C
		public ODataMetadataContext(bool isResponse, Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri, JsonLightMetadataLevel metadataLevel)
			: this(isResponse, operationsBoundToEntityTypeMustBeContainerQualified, edmTypeResolver, model, metadataDocumentUri, odataUri)
		{
			this.metadataLevel = metadataLevel;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00014635 File Offset: 0x00012835
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x00014640 File Offset: 0x00012840
		public Uri ServiceBaseUri
		{
			get
			{
				Uri uri;
				if ((uri = this.serviceBaseUri) == null)
				{
					uri = (this.serviceBaseUri = new Uri(this.MetadataDocumentUri, "./"));
				}
				return uri;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00014670 File Offset: 0x00012870
		public Uri MetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload("odata.context"));
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00014696 File Offset: 0x00012896
		public ODataUri ODataUri
		{
			get
			{
				return this.odataUri;
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x000146A0 File Offset: 0x000128A0
		public ODataEntityMetadataBuilder GetEntityMetadataBuilderForReader(IODataJsonLightReaderEntryState entryState, bool? useKeyAsSegment)
		{
			if (entryState.MetadataBuilder == null)
			{
				ODataEntry entry = entryState.Entry;
				if (this.isResponse)
				{
					ODataTypeAnnotation annotation = entry.GetAnnotation<ODataTypeAnnotation>();
					IEdmNavigationSource navigationSource = annotation.NavigationSource;
					IEdmEntityType elementType = this.edmTypeResolver.GetElementType(navigationSource);
					IODataFeedAndEntryTypeContext iodataFeedAndEntryTypeContext = ODataFeedAndEntryTypeContext.Create(null, navigationSource, elementType, entryState.EntityType, this.model, true);
					IODataEntryMetadataContext iodataEntryMetadataContext = ODataEntryMetadataContext.Create(entry, iodataFeedAndEntryTypeContext, null, (IEdmEntityType)entry.GetEdmType().Definition, this, entryState.SelectedProperties);
					UrlConvention urlConvention = UrlConvention.ForUserSettingAndTypeContext(useKeyAsSegment, iodataFeedAndEntryTypeContext);
					ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(this.ServiceBaseUri, urlConvention);
					entryState.MetadataBuilder = new ODataConventionalEntityMetadataBuilder(iodataEntryMetadataContext, this, odataConventionalUriBuilder);
				}
				else
				{
					entryState.MetadataBuilder = new NoOpEntityMetadataBuilder(entry);
				}
			}
			return entryState.MetadataBuilder;
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00014758 File Offset: 0x00012958
		public IEdmOperation[] GetBindableOperationsForType(IEdmType bindingType)
		{
			IEdmOperation[] array;
			if (!this.bindableOperationsCache.TryGetValue(bindingType, ref array))
			{
				array = MetadataUtils.CalculateBindableOperationsForType(bindingType, this.model, this.edmTypeResolver);
				this.bindableOperationsCache.Add(bindingType, array);
			}
			return array;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00014796 File Offset: 0x00012996
		public bool OperationsBoundToEntityTypeMustBeContainerQualified(IEdmEntityType entityType)
		{
			return this.operationsBoundToEntityTypeMustBeContainerQualified.Invoke(entityType);
		}

		// Token: 0x04000255 RID: 597
		private readonly IEdmModel model;

		// Token: 0x04000256 RID: 598
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000257 RID: 599
		private readonly Dictionary<IEdmType, IEdmOperation[]> bindableOperationsCache;

		// Token: 0x04000258 RID: 600
		private readonly bool isResponse;

		// Token: 0x04000259 RID: 601
		private readonly Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified;

		// Token: 0x0400025A RID: 602
		private readonly Uri metadataDocumentUri;

		// Token: 0x0400025B RID: 603
		private readonly ODataUri odataUri;

		// Token: 0x0400025C RID: 604
		private Uri serviceBaseUri;

		// Token: 0x0400025D RID: 605
		private JsonLightMetadataLevel metadataLevel;
	}
}
