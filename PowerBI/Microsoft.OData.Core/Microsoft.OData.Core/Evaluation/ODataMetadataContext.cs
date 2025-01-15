using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000266 RID: 614
	internal sealed class ODataMetadataContext : IODataMetadataContext
	{
		// Token: 0x06001BC6 RID: 7110 RVA: 0x0005554F File Offset: 0x0005374F
		public ODataMetadataContext(bool isResponse, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri)
			: this(isResponse, null, EdmTypeWriterResolver.Instance, model, metadataDocumentUri, odataUri)
		{
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00055564 File Offset: 0x00053764
		public ODataMetadataContext(bool isResponse, Func<IEdmStructuredType, bool> operationsBoundToStructuredTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri)
		{
			this.isResponse = isResponse;
			this.operationsBoundToStructuredTypeMustBeContainerQualified = operationsBoundToStructuredTypeMustBeContainerQualified ?? new Func<IEdmStructuredType, bool>(EdmLibraryExtensions.OperationsBoundToStructuredTypeMustBeContainerQualified);
			this.edmTypeResolver = edmTypeResolver;
			this.model = model;
			this.metadataDocumentUri = metadataDocumentUri;
			this.bindableOperationsCache = new Dictionary<IEdmType, IList<IEdmOperation>>(ReferenceEqualityComparer<IEdmType>.Instance);
			this.odataUri = odataUri;
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x000555C4 File Offset: 0x000537C4
		public ODataMetadataContext(bool isResponse, Func<IEdmStructuredType, bool> operationsBoundToEntityTypeMustBeContainerQualified, EdmTypeResolver edmTypeResolver, IEdmModel model, Uri metadataDocumentUri, ODataUri odataUri, JsonLightMetadataLevel metadataLevel)
			: this(isResponse, operationsBoundToEntityTypeMustBeContainerQualified, edmTypeResolver, model, metadataDocumentUri, odataUri)
		{
			this.metadataLevel = metadataLevel;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000555DD File Offset: 0x000537DD
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000555E8 File Offset: 0x000537E8
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

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001BCB RID: 7115 RVA: 0x00055618 File Offset: 0x00053818
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0005563E File Offset: 0x0005383E
		public ODataUri ODataUri
		{
			get
			{
				return this.odataUri;
			}
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00055648 File Offset: 0x00053848
		public ODataResourceMetadataBuilder GetResourceMetadataBuilderForReader(IODataJsonLightReaderResourceState resourceState, bool useKeyAsSegment, bool isDelta = false)
		{
			if (resourceState.MetadataBuilder == null)
			{
				ODataResourceBase resource = resourceState.Resource;
				if (this.isResponse && !isDelta)
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
					IODataResourceMetadataContext iodataResourceMetadataContext = ODataResourceMetadataContext.Create(resource, iodataResourceTypeContext, null, edmStructuredType, this, resourceState.SelectedProperties, null);
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

		// Token: 0x06001BCE RID: 7118 RVA: 0x00055764 File Offset: 0x00053964
		public IEnumerable<IEdmOperation> GetBindableOperationsForType(IEdmType bindingType)
		{
			IList<IEdmOperation> list;
			if (!this.bindableOperationsCache.TryGetValue(bindingType, out list))
			{
				list = MetadataUtils.CalculateBindableOperationsForType(bindingType, this.model, this.edmTypeResolver);
				this.bindableOperationsCache.Add(bindingType, list);
			}
			return list;
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x000557A2 File Offset: 0x000539A2
		public bool OperationsBoundToStructuredTypeMustBeContainerQualified(IEdmStructuredType structuredType)
		{
			return this.operationsBoundToStructuredTypeMustBeContainerQualified(structuredType);
		}

		// Token: 0x04000B93 RID: 2963
		private readonly IEdmModel model;

		// Token: 0x04000B94 RID: 2964
		private readonly EdmTypeResolver edmTypeResolver;

		// Token: 0x04000B95 RID: 2965
		private readonly Dictionary<IEdmType, IList<IEdmOperation>> bindableOperationsCache;

		// Token: 0x04000B96 RID: 2966
		private readonly bool isResponse;

		// Token: 0x04000B97 RID: 2967
		private readonly Func<IEdmStructuredType, bool> operationsBoundToStructuredTypeMustBeContainerQualified;

		// Token: 0x04000B98 RID: 2968
		private readonly Uri metadataDocumentUri;

		// Token: 0x04000B99 RID: 2969
		private readonly ODataUri odataUri;

		// Token: 0x04000B9A RID: 2970
		private Uri serviceBaseUri;

		// Token: 0x04000B9B RID: 2971
		private JsonLightMetadataLevel metadataLevel;
	}
}
