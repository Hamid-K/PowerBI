using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000109 RID: 265
	internal sealed class ODataMetadataContext : IODataMetadataContext
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0001829C File Offset: 0x0001649C
		public ODataMetadataContext(bool isResponse, IEdmModel model, Uri metadataDocumentUri)
			: this(isResponse, null, EdmTypeWriterResolver.Instance, model, metadataDocumentUri)
		{
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x000182B0 File Offset: 0x000164B0
		public ODataMetadataContext(bool isResponse, Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri)
		{
			this.isResponse = isResponse;
			this.operationsBoundToEntityTypeMustBeContainerQualified = operationsBoundToEntityTypeMustBeContainerQualified ?? new Func<IEdmEntityType, bool>(EdmLibraryExtensions.OperationsBoundToEntityTypeMustBeContainerQualified);
			this.edmTypeResolver = edmTypeResolver;
			this.model = model;
			this.metadataDocumentUri = metadataDocumentUri;
			this.alwaysBindableOperationsCache = new Dictionary<IEdmType, IEdmFunctionImport[]>(ReferenceEqualityComparer<IEdmType>.Instance);
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x00018308 File Offset: 0x00016508
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00018310 File Offset: 0x00016510
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x00018340 File Offset: 0x00016540
		public Uri MetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightEntryMetadataContext_MetadataAnnotationMustBeInPayload("odata.metadata"));
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00018368 File Offset: 0x00016568
		public ODataEntityMetadataBuilder GetEntityMetadataBuilderForReader(IODataJsonLightReaderEntryState entryState)
		{
			if (entryState.MetadataBuilder == null)
			{
				ODataEntry entry = entryState.Entry;
				if (this.isResponse)
				{
					ODataTypeAnnotation annotation = entry.GetAnnotation<ODataTypeAnnotation>();
					IEdmEntitySet entitySet = annotation.EntitySet;
					IEdmEntityType elementType = this.edmTypeResolver.GetElementType(entitySet);
					IODataFeedAndEntryTypeContext iodataFeedAndEntryTypeContext = ODataFeedAndEntryTypeContext.Create(null, entitySet, elementType, entryState.EntityType, this.model, true);
					IODataEntryMetadataContext iodataEntryMetadataContext = ODataEntryMetadataContext.Create(entry, iodataFeedAndEntryTypeContext, null, (IEdmEntityType)entry.GetEdmType().Definition, this, entryState.SelectedProperties);
					UrlConvention urlConvention = UrlConvention.ForUserSettingAndTypeContext(default(bool?), iodataFeedAndEntryTypeContext);
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

		// Token: 0x06000713 RID: 1811 RVA: 0x0001842C File Offset: 0x0001662C
		public IEdmFunctionImport[] GetAlwaysBindableOperationsForType(IEdmType bindingType)
		{
			IEdmFunctionImport[] array;
			if (!this.alwaysBindableOperationsCache.TryGetValue(bindingType, ref array))
			{
				array = MetadataUtils.CalculateAlwaysBindableOperationsForType(bindingType, this.model, this.edmTypeResolver);
				this.alwaysBindableOperationsCache.Add(bindingType, array);
			}
			return array;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001846A File Offset: 0x0001666A
		public bool OperationsBoundToEntityTypeMustBeContainerQualified(IEdmEntityType entityType)
		{
			return this.operationsBoundToEntityTypeMustBeContainerQualified.Invoke(entityType);
		}

		// Token: 0x040002B7 RID: 695
		private readonly IEdmModel model;

		// Token: 0x040002B8 RID: 696
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x040002B9 RID: 697
		private readonly Dictionary<IEdmType, IEdmFunctionImport[]> alwaysBindableOperationsCache;

		// Token: 0x040002BA RID: 698
		private readonly bool isResponse;

		// Token: 0x040002BB RID: 699
		private readonly Func<IEdmEntityType, bool> operationsBoundToEntityTypeMustBeContainerQualified;

		// Token: 0x040002BC RID: 700
		private readonly Uri metadataDocumentUri;

		// Token: 0x040002BD RID: 701
		private Uri serviceBaseUri;
	}
}
