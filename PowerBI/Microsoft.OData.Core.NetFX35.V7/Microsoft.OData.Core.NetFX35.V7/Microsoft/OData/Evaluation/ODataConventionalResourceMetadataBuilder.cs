using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000226 RID: 550
	internal class ODataConventionalResourceMetadataBuilder : ODataResourceMetadataBuilder
	{
		// Token: 0x06001647 RID: 5703 RVA: 0x00044ACD File Offset: 0x00042CCD
		internal ODataConventionalResourceMetadataBuilder(IODataResourceMetadataContext resourceMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
		{
			this.ResourceMetadataContext = resourceMetadataContext;
			this.UriBuilder = uriBuilder;
			this.MetadataContext = metadataContext;
			this.ProcessedNestedResourceInfos = new HashSet<string>(StringComparer.Ordinal);
			this.resource = resourceMetadataContext.Resource;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x00044B06 File Offset: 0x00042D06
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00044B0E File Offset: 0x00042D0E
		internal ODataUri ODataUri { get; set; }

		// Token: 0x0600164A RID: 5706 RVA: 0x00044B18 File Offset: 0x00042D18
		public virtual Uri GetEditUrl()
		{
			if (this.editUrl != null)
			{
				return this.editUrl;
			}
			if (this.resource.HasNonComputedEditLink)
			{
				return this.editUrl = this.resource.NonComputedEditLink;
			}
			Uri uri = this.GetCanonicalUrl();
			if (uri != null)
			{
				this.editUrl = uri;
			}
			else
			{
				ODataConventionalResourceMetadataBuilder odataConventionalResourceMetadataBuilder = base.ParentMetadataBuilder as ODataConventionalResourceMetadataBuilder;
				if (base.NameAsProperty != null && odataConventionalResourceMetadataBuilder != null && odataConventionalResourceMetadataBuilder.GetEditUrl() != null)
				{
					if (odataConventionalResourceMetadataBuilder.IsFromCollection && !(odataConventionalResourceMetadataBuilder is ODataConventionalEntityMetadataBuilder))
					{
						return this.editUrl = null;
					}
					this.editUrl = new Uri(odataConventionalResourceMetadataBuilder.GetEditUrl() + "/" + base.NameAsProperty, 0);
				}
			}
			if (this.editUrl != null && this.ResourceMetadataContext.ActualResourceTypeName != this.ResourceMetadataContext.TypeContext.ExpectedResourceTypeName)
			{
				this.editUrl = this.UriBuilder.AppendTypeSegment(this.editUrl, this.ResourceMetadataContext.ActualResourceTypeName);
			}
			return this.editUrl;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x00044C30 File Offset: 0x00042E30
		public virtual Uri GetReadUrl()
		{
			if (this.readUrl != null)
			{
				return this.readUrl;
			}
			if (this.resource.HasNonComputedReadLink)
			{
				return this.readUrl = this.resource.NonComputedReadLink;
			}
			Uri uri = this.GetEditUrl();
			if (uri != null)
			{
				return this.readUrl = uri;
			}
			ODataConventionalResourceMetadataBuilder odataConventionalResourceMetadataBuilder = base.ParentMetadataBuilder as ODataConventionalResourceMetadataBuilder;
			if (base.NameAsProperty != null && odataConventionalResourceMetadataBuilder != null && odataConventionalResourceMetadataBuilder.GetReadUrl() != null)
			{
				if (odataConventionalResourceMetadataBuilder.IsFromCollection && !(odataConventionalResourceMetadataBuilder is ODataConventionalEntityMetadataBuilder))
				{
					return this.readUrl = null;
				}
				this.readUrl = new Uri(odataConventionalResourceMetadataBuilder.GetReadUrl() + "/" + base.NameAsProperty, 0);
				if (this.ResourceMetadataContext.ActualResourceTypeName != this.ResourceMetadataContext.TypeContext.ExpectedResourceTypeName)
				{
					this.readUrl = this.UriBuilder.AppendTypeSegment(this.readUrl, this.ResourceMetadataContext.ActualResourceTypeName);
				}
			}
			return this.readUrl;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00044D44 File Offset: 0x00042F44
		public virtual Uri GetCanonicalUrl()
		{
			if (this.canonicalUrl != null)
			{
				return this.canonicalUrl;
			}
			if (this.resource.HasNonComputedId)
			{
				return this.canonicalUrl = this.resource.NonComputedId;
			}
			ODataConventionalResourceMetadataBuilder odataConventionalResourceMetadataBuilder = base.ParentMetadataBuilder as ODataConventionalResourceMetadataBuilder;
			if (base.NameAsProperty != null && odataConventionalResourceMetadataBuilder != null && odataConventionalResourceMetadataBuilder.GetCanonicalUrl() != null)
			{
				if (odataConventionalResourceMetadataBuilder.IsFromCollection && !(odataConventionalResourceMetadataBuilder is ODataConventionalEntityMetadataBuilder))
				{
					return this.canonicalUrl = null;
				}
				this.canonicalUrl = odataConventionalResourceMetadataBuilder.GetCanonicalUrl();
				IODataResourceTypeContext typeContext = odataConventionalResourceMetadataBuilder.ResourceMetadataContext.TypeContext;
				if (odataConventionalResourceMetadataBuilder.ResourceMetadataContext.ActualResourceTypeName != typeContext.ExpectedResourceTypeName)
				{
					ODataResourceTypeContext.ODataResourceTypeContextWithModel odataResourceTypeContextWithModel = typeContext as ODataResourceTypeContext.ODataResourceTypeContextWithModel;
					if (odataResourceTypeContextWithModel == null || odataResourceTypeContextWithModel.ExpectedResourceType.FindProperty(base.NameAsProperty) == null)
					{
						this.canonicalUrl = this.UriBuilder.AppendTypeSegment(this.canonicalUrl, odataConventionalResourceMetadataBuilder.ResourceMetadataContext.ActualResourceTypeName);
					}
				}
				this.canonicalUrl = new Uri(this.canonicalUrl + "/" + base.NameAsProperty, 0);
			}
			else if (this.ODataUri != null && this.ODataUri.Path.Count != 0)
			{
				this.canonicalUrl = this.ODataUri.BuildUri(ODataUrlKeyDelimiter.Parentheses);
			}
			return this.canonicalUrl;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0000250D File Offset: 0x0000070D
		internal virtual void StartResource()
		{
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0000250D File Offset: 0x0000070D
		internal virtual void EndResource()
		{
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00044E9A File Offset: 0x0004309A
		internal override Uri GetEditLink()
		{
			return this.resource.NonComputedEditLink;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00044EA7 File Offset: 0x000430A7
		internal override Uri GetReadLink()
		{
			return this.resource.NonComputedReadLink;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00044EB4 File Offset: 0x000430B4
		internal override Uri GetId()
		{
			if (!this.resource.IsTransient)
			{
				return this.resource.NonComputedId;
			}
			return null;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00044ED0 File Offset: 0x000430D0
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			if (this.resource.IsTransient)
			{
				id = null;
				return true;
			}
			id = this.GetId();
			return id != null;
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00044EF4 File Offset: 0x000430F4
		internal override string GetETag()
		{
			return this.resource.NonComputedETag;
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00044F01 File Offset: 0x00043101
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.resource.NonComputedActions;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00044F0E File Offset: 0x0004310E
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.resource.NonComputedFunctions;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00044F1B File Offset: 0x0004311B
		internal override void MarkNestedResourceInfoProcessed(string navigationPropertyName)
		{
			this.ProcessedNestedResourceInfos.Add(navigationPropertyName);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00044F2C File Offset: 0x0004312C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override ODataJsonLightReaderNestedResourceInfo GetNextUnprocessedNavigationLink()
		{
			if (this.unprocessedNavigationLinks == null)
			{
				this.unprocessedNavigationLinks = Enumerable.Select<IEdmNavigationProperty, ODataJsonLightReaderNestedResourceInfo>(Enumerable.Where<IEdmNavigationProperty>(this.ResourceMetadataContext.SelectedNavigationProperties, (IEdmNavigationProperty p) => !this.ProcessedNestedResourceInfos.Contains(p.Name)), new Func<IEdmNavigationProperty, ODataJsonLightReaderNestedResourceInfo>(ODataJsonLightReaderNestedResourceInfo.CreateProjectedNestedResourceInfo)).GetEnumerator();
			}
			if (this.unprocessedNavigationLinks.MoveNext())
			{
				return this.unprocessedNavigationLinks.Current;
			}
			return null;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00044F94 File Offset: 0x00043194
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = this.GetReadUrl();
			if (uri == null || base.IsFromCollection)
			{
				return null;
			}
			if (!hasNestedResourceInfoUrl)
			{
				return this.UriBuilder.BuildNavigationLinkUri(uri, navigationPropertyName);
			}
			return navigationLinkUrl;
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00044FD8 File Offset: 0x000431D8
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = this.GetReadUrl();
			if (uri == null || base.IsFromCollection)
			{
				return null;
			}
			if (!hasAssociationLinkUrl)
			{
				return this.UriBuilder.BuildAssociationLinkUri(uri, navigationPropertyName);
			}
			return associationLinkUrl;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0004501C File Offset: 0x0004321C
		internal override Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			Uri uri;
			if (string.IsNullOrEmpty(bindingParameterTypeName) || this.ResourceMetadataContext.Resource.NonComputedEditLink != null)
			{
				uri = this.GetEditLink();
			}
			else
			{
				uri = this.GetId();
			}
			return this.UriBuilder.BuildOperationTargetUri(uri, operationName, bindingParameterTypeName, parameterNames);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00045073 File Offset: 0x00043273
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x04000A51 RID: 2641
		public readonly IODataResourceMetadataContext ResourceMetadataContext;

		// Token: 0x04000A52 RID: 2642
		protected readonly ODataUriBuilder UriBuilder;

		// Token: 0x04000A53 RID: 2643
		protected readonly IODataMetadataContext MetadataContext;

		// Token: 0x04000A54 RID: 2644
		protected readonly HashSet<string> ProcessedNestedResourceInfos;

		// Token: 0x04000A55 RID: 2645
		private IEnumerator<ODataJsonLightReaderNestedResourceInfo> unprocessedNavigationLinks;

		// Token: 0x04000A56 RID: 2646
		private Uri readUrl;

		// Token: 0x04000A57 RID: 2647
		private Uri editUrl;

		// Token: 0x04000A58 RID: 2648
		private Uri canonicalUrl;

		// Token: 0x04000A59 RID: 2649
		private ODataResource resource;
	}
}
