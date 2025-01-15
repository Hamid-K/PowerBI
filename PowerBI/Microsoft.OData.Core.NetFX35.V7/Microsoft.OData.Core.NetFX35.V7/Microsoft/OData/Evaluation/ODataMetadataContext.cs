using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200022B RID: 555
	internal sealed class ODataMetadataContext : IODataMetadataContext
	{
		// Token: 0x0600169C RID: 5788 RVA: 0x00045593 File Offset: 0x00043793
		public ODataMetadataContext(bool isResponse, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri)
			: this(isResponse, null, EdmTypeWriterResolver.Instance, model, metadataDocumentUri, odataUri)
		{
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000455A8 File Offset: 0x000437A8
		public ODataMetadataContext(bool isResponse, Func<IEdmStructuredType, bool> operationsBoundToStructuredTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri)
		{
			this.isResponse = isResponse;
			this.operationsBoundToStructuredTypeMustBeContainerQualified = operationsBoundToStructuredTypeMustBeContainerQualified ?? new Func<IEdmStructuredType, bool>(EdmLibraryExtensions.OperationsBoundToStructuredTypeMustBeContainerQualified);
			this.edmTypeResolver = edmTypeResolver;
			this.model = model;
			this.metadataDocumentUri = metadataDocumentUri;
			this.bindableOperationsCache = new Dictionary<IEdmType, IEdmOperation[]>(ReferenceEqualityComparer<IEdmType>.Instance);
			this.odataUri = odataUri;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00045608 File Offset: 0x00043808
		public ODataMetadataContext(bool isResponse, Func<IEdmStructuredType, bool> operationsBoundToEntityTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri, JsonLightMetadataLevel metadataLevel)
			: this(isResponse, operationsBoundToEntityTypeMustBeContainerQualified, edmTypeResolver, model, metadataDocumentUri, odataUri)
		{
			this.metadataLevel = metadataLevel;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00045621 File Offset: 0x00043821
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0004562C File Offset: 0x0004382C
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

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0004565C File Offset: 0x0004385C
		public Uri MetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightResourceMetadataContext_MetadataAnnotationMustBeInPayload("odata.context"));
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00045682 File Offset: 0x00043882
		public ODataUri ODataUri
		{
			get
			{
				return this.odataUri;
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x0004568C File Offset: 0x0004388C
		public ODataResourceMetadataBuilder GetResourceMetadataBuilderForReader(IODataJsonLightReaderResourceState resourceState, bool useKeyAsSegment)
		{
			if (resourceState.MetadataBuilder == null)
			{
				ODataResource resource = resourceState.Resource;
				if (this.isResponse)
				{
					ODataTypeAnnotation typeAnnotation = resource.TypeAnnotation;
					IEdmStructuredType edmStructuredType = null;
					if (typeAnnotation != null)
					{
						if (typeAnnotation.Type != null)
						{
							edmStructuredType = typeAnnotation.Type as IEdmStructuredType;
						}
						else if (typeAnnotation.TypeName != null)
						{
							edmStructuredType = this.model.FindType(typeAnnotation.TypeName) as IEdmStructuredType;
						}
					}
					if (edmStructuredType == null)
					{
						edmStructuredType = resourceState.ResourceType;
					}
					IEdmNavigationSource navigationSource = resourceState.NavigationSource;
					IEdmEntityType elementType = this.edmTypeResolver.GetElementType(navigationSource);
					IODataResourceTypeContext iodataResourceTypeContext = ODataResourceTypeContext.Create(null, navigationSource, elementType, resourceState.ResourceTypeFromMetadata ?? resourceState.ResourceType, true);
					IODataResourceMetadataContext iodataResourceMetadataContext = ODataResourceMetadataContext.Create(resource, iodataResourceTypeContext, null, edmStructuredType, this, resourceState.SelectedProperties);
					ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(this.ServiceBaseUri, useKeyAsSegment ? ODataUrlKeyDelimiter.Slash : ODataUrlKeyDelimiter.Parentheses);
					if (edmStructuredType.IsODataEntityTypeKind())
					{
						resourceState.MetadataBuilder = new ODataConventionalEntityMetadataBuilder(iodataResourceMetadataContext, this, odataConventionalUriBuilder);
					}
					else
					{
						resourceState.MetadataBuilder = new ODataConventionalResourceMetadataBuilder(iodataResourceMetadataContext, this, odataConventionalUriBuilder);
					}
				}
				else
				{
					resourceState.MetadataBuilder = new NoOpResourceMetadataBuilder(resource);
				}
			}
			return resourceState.MetadataBuilder;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000457A0 File Offset: 0x000439A0
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

		// Token: 0x060016A5 RID: 5797 RVA: 0x000457DE File Offset: 0x000439DE
		public bool OperationsBoundToStructuredTypeMustBeContainerQualified(IEdmStructuredType structuredType)
		{
			return this.operationsBoundToStructuredTypeMustBeContainerQualified.Invoke(structuredType);
		}

		// Token: 0x04000A68 RID: 2664
		private readonly IEdmModel model;

		// Token: 0x04000A69 RID: 2665
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000A6A RID: 2666
		private readonly Dictionary<IEdmType, IEdmOperation[]> bindableOperationsCache;

		// Token: 0x04000A6B RID: 2667
		private readonly bool isResponse;

		// Token: 0x04000A6C RID: 2668
		private readonly Func<IEdmStructuredType, bool> operationsBoundToStructuredTypeMustBeContainerQualified;

		// Token: 0x04000A6D RID: 2669
		private readonly Uri metadataDocumentUri;

		// Token: 0x04000A6E RID: 2670
		private readonly ODataUri odataUri;

		// Token: 0x04000A6F RID: 2671
		private Uri serviceBaseUri;

		// Token: 0x04000A70 RID: 2672
		private JsonLightMetadataLevel metadataLevel;
	}
}
