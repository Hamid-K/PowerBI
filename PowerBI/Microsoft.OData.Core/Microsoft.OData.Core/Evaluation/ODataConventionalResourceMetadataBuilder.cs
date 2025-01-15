using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200025A RID: 602
	internal class ODataConventionalResourceMetadataBuilder : ODataResourceMetadataBuilder
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x000536F8 File Offset: 0x000518F8
		internal ODataConventionalResourceMetadataBuilder(IODataResourceMetadataContext resourceMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
		{
			this.ResourceMetadataContext = resourceMetadataContext;
			this.UriBuilder = uriBuilder;
			this.MetadataContext = metadataContext;
			this.ProcessedNestedResourceInfos = new HashSet<string>(StringComparer.Ordinal);
			this.ProcessedStreamProperties = new HashSet<string>(StringComparer.Ordinal);
			this.resource = resourceMetadataContext.Resource;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0005374C File Offset: 0x0005194C
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x00053754 File Offset: 0x00051954
		internal ODataUri ODataUri { get; set; }

		// Token: 0x06001B1F RID: 6943 RVA: 0x00053760 File Offset: 0x00051960
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
					this.editUrl = new Uri(odataConventionalResourceMetadataBuilder.GetEditUrl() + "/" + base.NameAsProperty, UriKind.RelativeOrAbsolute);
				}
			}
			if (this.editUrl != null && this.ResourceMetadataContext.ActualResourceTypeName != this.ResourceMetadataContext.TypeContext.ExpectedResourceTypeName)
			{
				this.editUrl = this.UriBuilder.AppendTypeSegment(this.editUrl, this.ResourceMetadataContext.ActualResourceTypeName);
			}
			return this.editUrl;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00053878 File Offset: 0x00051A78
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
				this.readUrl = new Uri(odataConventionalResourceMetadataBuilder.GetReadUrl() + "/" + base.NameAsProperty, UriKind.RelativeOrAbsolute);
				if (this.ResourceMetadataContext.ActualResourceTypeName != this.ResourceMetadataContext.TypeContext.ExpectedResourceTypeName)
				{
					this.readUrl = this.UriBuilder.AppendTypeSegment(this.readUrl, this.ResourceMetadataContext.ActualResourceTypeName);
				}
			}
			return this.readUrl;
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x0005398C File Offset: 0x00051B8C
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
				this.canonicalUrl = new Uri(this.canonicalUrl + "/" + base.NameAsProperty, UriKind.RelativeOrAbsolute);
			}
			else if (this.ODataUri != null && this.ODataUri.Path.Count != 0)
			{
				this.canonicalUrl = this.ODataUri.BuildUri(ODataUrlKeyDelimiter.Parentheses);
			}
			return this.canonicalUrl;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0000239D File Offset: 0x0000059D
		internal virtual void StartResource()
		{
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0000239D File Offset: 0x0000059D
		internal virtual void EndResource()
		{
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00053AE2 File Offset: 0x00051CE2
		internal override Uri GetEditLink()
		{
			return this.resource.NonComputedEditLink;
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00053AEF File Offset: 0x00051CEF
		internal override Uri GetReadLink()
		{
			return this.resource.NonComputedReadLink;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00053AFC File Offset: 0x00051CFC
		internal override Uri GetId()
		{
			if (!this.resource.IsTransient)
			{
				return this.resource.NonComputedId;
			}
			return null;
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x00053B18 File Offset: 0x00051D18
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

		// Token: 0x06001B28 RID: 6952 RVA: 0x00053B3C File Offset: 0x00051D3C
		internal override string GetETag()
		{
			return this.resource.NonComputedETag;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00011BDC File Offset: 0x0000FDDC
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return nonComputedProperties;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x00053B49 File Offset: 0x00051D49
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataAction> GetActions()
		{
			return this.resource.NonComputedActions;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x00053B56 File Offset: 0x00051D56
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return this.resource.NonComputedFunctions;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00053B63 File Offset: 0x00051D63
		internal override void MarkNestedResourceInfoProcessed(string navigationPropertyName)
		{
			this.ProcessedNestedResourceInfos.Add(navigationPropertyName);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x00053B74 File Offset: 0x00051D74
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override ODataJsonLightReaderNestedResourceInfo GetNextUnprocessedNavigationLink()
		{
			if (this.unprocessedNavigationLinks == null)
			{
				this.unprocessedNavigationLinks = this.ResourceMetadataContext.SelectedNavigationProperties.Where((IEdmNavigationProperty p) => !this.ProcessedNestedResourceInfos.Contains(p.Name)).Select(new Func<IEdmNavigationProperty, ODataJsonLightReaderNestedResourceInfo>(ODataJsonLightReaderNestedResourceInfo.CreateProjectedNestedResourceInfo)).GetEnumerator();
			}
			if (this.unprocessedNavigationLinks.MoveNext())
			{
				return this.unprocessedNavigationLinks.Current;
			}
			return null;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x00053BDB File Offset: 0x00051DDB
		internal override void MarkStreamPropertyProcessed(string streamPropertyName)
		{
			this.ProcessedStreamProperties.Add(streamPropertyName);
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x00053BEC File Offset: 0x00051DEC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override ODataProperty GetNextUnprocessedStreamProperty()
		{
			if (this.unprocessedStreamProperties == null)
			{
				this.unprocessedStreamProperties = (from p in this.ResourceMetadataContext.SelectedStreamProperties
					where !this.ProcessedStreamProperties.Contains(p.Key)
					select p.Key).GetEnumerator();
			}
			if (this.unprocessedStreamProperties.MoveNext())
			{
				string text = this.unprocessedStreamProperties.Current;
				ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
				odataStreamReferenceValue.SetMetadataBuilder(this, text);
				return new ODataProperty
				{
					Name = text,
					Value = odataStreamReferenceValue
				};
			}
			return null;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x00053C88 File Offset: 0x00051E88
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

		// Token: 0x06001B31 RID: 6961 RVA: 0x00053CCC File Offset: 0x00051ECC
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

		// Token: 0x06001B32 RID: 6962 RVA: 0x00053D10 File Offset: 0x00051F10
		internal override Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.UriBuilder.BuildStreamEditLinkUri(this.GetEditUrl(), streamPropertyName);
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x00053D2F File Offset: 0x00051F2F
		internal override Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.UriBuilder.BuildStreamReadLinkUri(this.GetReadUrl(), streamPropertyName);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00053D50 File Offset: 0x00051F50
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

		// Token: 0x06001B35 RID: 6965 RVA: 0x00053DA7 File Offset: 0x00051FA7
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x04000B6A RID: 2922
		public readonly IODataResourceMetadataContext ResourceMetadataContext;

		// Token: 0x04000B6B RID: 2923
		protected readonly ODataUriBuilder UriBuilder;

		// Token: 0x04000B6C RID: 2924
		protected readonly IODataMetadataContext MetadataContext;

		// Token: 0x04000B6D RID: 2925
		protected readonly HashSet<string> ProcessedNestedResourceInfos;

		// Token: 0x04000B6E RID: 2926
		protected readonly HashSet<string> ProcessedStreamProperties;

		// Token: 0x04000B6F RID: 2927
		private IEnumerator<ODataJsonLightReaderNestedResourceInfo> unprocessedNavigationLinks;

		// Token: 0x04000B70 RID: 2928
		private IEnumerator<string> unprocessedStreamProperties;

		// Token: 0x04000B71 RID: 2929
		private Uri readUrl;

		// Token: 0x04000B72 RID: 2930
		private Uri editUrl;

		// Token: 0x04000B73 RID: 2931
		private Uri canonicalUrl;

		// Token: 0x04000B74 RID: 2932
		private ODataResourceBase resource;
	}
}
