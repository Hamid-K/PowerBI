using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.JsonLight;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x0200022E RID: 558
	internal sealed class ODataConventionalEntityMetadataBuilder : ODataConventionalResourceMetadataBuilder
	{
		// Token: 0x060016B5 RID: 5813 RVA: 0x00045AC8 File Offset: 0x00043CC8
		internal ODataConventionalEntityMetadataBuilder(IODataResourceMetadataContext resourceMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
			: base(resourceMetadataContext, metadataContext, uriBuilder)
		{
			this.isResourceEnd = true;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00045ADA File Offset: 0x00043CDA
		private Uri ComputedId
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedId;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00045AE8 File Offset: 0x00043CE8
		private ODataMissingOperationGenerator MissingOperationGenerator
		{
			get
			{
				ODataMissingOperationGenerator odataMissingOperationGenerator;
				if ((odataMissingOperationGenerator = this.missingOperationGenerator) == null)
				{
					odataMissingOperationGenerator = (this.missingOperationGenerator = new ODataMissingOperationGenerator(this.ResourceMetadataContext, this.MetadataContext));
				}
				return odataMissingOperationGenerator;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x00045B1C File Offset: 0x00043D1C
		private ICollection<KeyValuePair<string, object>> ComputedKeyProperties
		{
			get
			{
				if (this.computedKeyProperties == null)
				{
					this.computedKeyProperties = new List<KeyValuePair<string, object>>();
					foreach (KeyValuePair<string, object> keyValuePair in this.ResourceMetadataContext.KeyProperties)
					{
						object obj = this.MetadataContext.Model.ConvertToUnderlyingTypeIfUIntValue(keyValuePair.Value, null);
						this.computedKeyProperties.Add(new KeyValuePair<string, object>(keyValuePair.Key, obj));
					}
				}
				return this.computedKeyProperties;
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00045BB4 File Offset: 0x00043DB4
		public override Uri GetCanonicalUrl()
		{
			return this.GetId();
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00045BBC File Offset: 0x00043DBC
		public override Uri GetEditUrl()
		{
			return this.GetEditLink();
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00045BC4 File Offset: 0x00043DC4
		public override Uri GetReadUrl()
		{
			return this.GetReadLink();
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00045BCC File Offset: 0x00043DCC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override Uri GetEditLink()
		{
			if (this.ResourceMetadataContext.Resource.HasNonComputedEditLink)
			{
				return this.ResourceMetadataContext.Resource.NonComputedEditLink;
			}
			if (this.ResourceMetadataContext.Resource.IsTransient || this.ResourceMetadataContext.Resource.HasNonComputedReadLink)
			{
				return null;
			}
			if (this.computedEditLink != null)
			{
				return this.computedEditLink;
			}
			return this.computedEditLink = this.ComputeEditLink();
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00045C48 File Offset: 0x00043E48
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override Uri GetReadLink()
		{
			if (this.ResourceMetadataContext.Resource.HasNonComputedReadLink)
			{
				return this.ResourceMetadataContext.Resource.NonComputedReadLink;
			}
			if (this.computedReadLink != null)
			{
				return this.computedReadLink;
			}
			return this.computedReadLink = this.GetEditLink();
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00045C9C File Offset: 0x00043E9C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override Uri GetId()
		{
			if (this.ResourceMetadataContext.Resource.HasNonComputedId)
			{
				return this.ResourceMetadataContext.Resource.NonComputedId;
			}
			if (!this.ResourceMetadataContext.Resource.IsTransient)
			{
				return this.ComputedId;
			}
			return null;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00045CDC File Offset: 0x00043EDC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override string GetETag()
		{
			if (this.ResourceMetadataContext.Resource.HasNonComputedETag)
			{
				return this.ResourceMetadataContext.Resource.NonComputedETag;
			}
			if (!this.etagComputed)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, object> keyValuePair in this.ResourceMetadataContext.ETagProperties)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					else
					{
						stringBuilder.Append("W/\"");
					}
					string text;
					if (keyValuePair.Value == null)
					{
						text = "null";
					}
					else
					{
						text = LiteralFormatter.ForConstants.Format(keyValuePair.Value);
					}
					stringBuilder.Append(text);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('"');
					this.computedETag = stringBuilder.ToString();
				}
				this.etagComputed = true;
			}
			return this.computedETag;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00045DD4 File Offset: 0x00043FD4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			if (this.ResourceMetadataContext.Resource.NonComputedMediaResource != null)
			{
				return this.ResourceMetadataContext.Resource.NonComputedMediaResource;
			}
			if (this.computedMediaResource == null && this.ResourceMetadataContext.TypeContext.IsMediaLinkEntry)
			{
				this.computedMediaResource = new ODataStreamReferenceValue();
				this.computedMediaResource.SetMetadataBuilder(this, null);
			}
			return this.computedMediaResource;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00045E3C File Offset: 0x0004403C
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (!this.isResourceEnd)
			{
				return nonComputedProperties;
			}
			return ODataUtilsInternal.ConcatEnumerables<ODataProperty>(nonComputedProperties, this.GetComputedStreamProperties(nonComputedProperties));
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00045E55 File Offset: 0x00044055
		internal override void StartResource()
		{
			this.isResourceEnd = false;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00045E5E File Offset: 0x0004405E
		internal override void EndResource()
		{
			this.isResourceEnd = true;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00045E67 File Offset: 0x00044067
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataAction> GetActions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataAction>(this.ResourceMetadataContext.Resource.NonComputedActions, this.MissingOperationGenerator.GetComputedActions());
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00045E89 File Offset: 0x00044089
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataFunction>(this.ResourceMetadataContext.Resource.NonComputedFunctions, this.MissingOperationGenerator.GetComputedFunctions());
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00044F1B File Offset: 0x0004311B
		internal override void MarkNestedResourceInfoProcessed(string navigationPropertyName)
		{
			this.ProcessedNestedResourceInfos.Add(navigationPropertyName);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00045EAC File Offset: 0x000440AC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override ODataJsonLightReaderNestedResourceInfo GetNextUnprocessedNavigationLink()
		{
			if (this.unprocessedNestedResourceInfos == null)
			{
				this.unprocessedNestedResourceInfos = Enumerable.Select<IEdmNavigationProperty, ODataJsonLightReaderNestedResourceInfo>(Enumerable.Where<IEdmNavigationProperty>(this.ResourceMetadataContext.SelectedNavigationProperties, (IEdmNavigationProperty p) => !this.ProcessedNestedResourceInfos.Contains(p.Name)), new Func<IEdmNavigationProperty, ODataJsonLightReaderNestedResourceInfo>(ODataJsonLightReaderNestedResourceInfo.CreateProjectedNestedResourceInfo)).GetEnumerator();
			}
			if (this.unprocessedNestedResourceInfos.MoveNext())
			{
				return this.unprocessedNestedResourceInfos.Current;
			}
			return null;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00045F13 File Offset: 0x00044113
		internal override Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.UriBuilder.BuildStreamEditLinkUri(this.GetEditLink(), streamPropertyName);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00045F32 File Offset: 0x00044132
		internal override Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.UriBuilder.BuildStreamReadLinkUri(this.GetReadLink(), streamPropertyName);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00045F51 File Offset: 0x00044151
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasNestedResourceInfoUrl)
			{
				return this.UriBuilder.BuildNavigationLinkUri(this.GetReadLink(), navigationPropertyName);
			}
			return navigationLinkUrl;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00045F75 File Offset: 0x00044175
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasAssociationLinkUrl)
			{
				return this.UriBuilder.BuildAssociationLinkUri(this.GetReadLink(), navigationPropertyName);
			}
			return associationLinkUrl;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00045F9C File Offset: 0x0004419C
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

		// Token: 0x060016CD RID: 5837 RVA: 0x00045073 File Offset: 0x00043273
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00045FF3 File Offset: 0x000441F3
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			id = (this.ResourceMetadataContext.Resource.IsTransient ? null : this.GetId());
			return true;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00046014 File Offset: 0x00044214
		private Uri ComputeEditLink()
		{
			Uri uri = (this.ResourceMetadataContext.Resource.HasNonComputedId ? this.ResourceMetadataContext.Resource.NonComputedId : this.ComputedId);
			if (this.ResourceMetadataContext.ActualResourceTypeName != this.ResourceMetadataContext.TypeContext.NavigationSourceEntityTypeName)
			{
				uri = this.UriBuilder.AppendTypeSegment(uri, this.ResourceMetadataContext.ActualResourceTypeName);
			}
			return uri;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00046088 File Offset: 0x00044288
		private void ComputeAndCacheId()
		{
			if (this.computedId != null)
			{
				return;
			}
			Uri uri;
			switch (this.ResourceMetadataContext.TypeContext.NavigationSourceKind)
			{
			case EdmNavigationSourceKind.Singleton:
				uri = this.ComputeIdForSingleton();
				break;
			case EdmNavigationSourceKind.ContainedEntitySet:
				uri = this.ComputeIdForContainment();
				break;
			case EdmNavigationSourceKind.UnknownEntitySet:
				throw new ODataException(Strings.ODataMetadataBuilder_UnknownEntitySet(this.ResourceMetadataContext.TypeContext.NavigationSourceName));
			default:
				uri = this.ComputeId();
				break;
			}
			this.computedId = uri;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00046108 File Offset: 0x00044308
		private Uri ComputeId()
		{
			Uri uri = this.UriBuilder.BuildBaseUri();
			uri = this.UriBuilder.BuildEntitySetUri(uri, this.ResourceMetadataContext.TypeContext.NavigationSourceName);
			return this.UriBuilder.BuildEntityInstanceUri(uri, this.ComputedKeyProperties, this.ResourceMetadataContext.ActualResourceTypeName);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00046160 File Offset: 0x00044360
		private Uri ComputeIdForContainment()
		{
			Uri uri;
			if (!this.TryComputeIdFromParent(out uri))
			{
				uri = this.UriBuilder.BuildBaseUri();
				ODataUri odataUri = base.ODataUri ?? this.MetadataContext.ODataUri;
				if (odataUri == null || odataUri.Path == null || odataUri.Path.Count == 0)
				{
					throw new ODataException(Strings.ODataMetadataBuilder_MissingParentIdOrContextUrl);
				}
				uri = this.GetContainingEntitySetUri(uri, odataUri);
			}
			uri = this.UriBuilder.BuildEntitySetUri(uri, this.ResourceMetadataContext.TypeContext.NavigationSourceName);
			if (this.ResourceMetadataContext.TypeContext.IsFromCollection)
			{
				uri = this.UriBuilder.BuildEntityInstanceUri(uri, this.ComputedKeyProperties, this.ResourceMetadataContext.ActualResourceTypeName);
			}
			return uri;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00046214 File Offset: 0x00044414
		private bool TryComputeIdFromParent(out Uri uri)
		{
			try
			{
				ODataConventionalResourceMetadataBuilder odataConventionalResourceMetadataBuilder = base.ParentMetadataBuilder as ODataConventionalResourceMetadataBuilder;
				if (odataConventionalResourceMetadataBuilder != null && odataConventionalResourceMetadataBuilder != this)
				{
					uri = odataConventionalResourceMetadataBuilder.GetCanonicalUrl();
					if (uri != null)
					{
						IODataResourceTypeContext typeContext = odataConventionalResourceMetadataBuilder.ResourceMetadataContext.TypeContext;
						if (odataConventionalResourceMetadataBuilder.ResourceMetadataContext.ActualResourceTypeName != typeContext.ExpectedResourceTypeName)
						{
							ODataResourceTypeContext.ODataResourceTypeContextWithModel odataResourceTypeContextWithModel = typeContext as ODataResourceTypeContext.ODataResourceTypeContextWithModel;
							if (odataResourceTypeContextWithModel == null || odataResourceTypeContextWithModel.ExpectedResourceType.FindProperty(this.ResourceMetadataContext.TypeContext.NavigationSourceName) == null)
							{
								uri = new Uri(UriUtils.EnsureTaillingSlash(uri), odataConventionalResourceMetadataBuilder.ResourceMetadataContext.ActualResourceTypeName);
							}
						}
						return true;
					}
				}
			}
			catch (ODataException)
			{
			}
			uri = null;
			return false;
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x000462CC File Offset: 0x000444CC
		private Uri ComputeIdForSingleton()
		{
			Uri uri = this.UriBuilder.BuildBaseUri();
			return this.UriBuilder.BuildEntitySetUri(uri, this.ResourceMetadataContext.TypeContext.NavigationSourceName);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00046304 File Offset: 0x00044504
		private IEnumerable<ODataProperty> GetComputedStreamProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (this.computedStreamProperties == null)
			{
				IDictionary<string, IEdmStructuralProperty> selectedStreamProperties = this.ResourceMetadataContext.SelectedStreamProperties;
				if (nonComputedProperties != null)
				{
					foreach (ODataProperty odataProperty in nonComputedProperties)
					{
						selectedStreamProperties.Remove(odataProperty.Name);
					}
				}
				this.computedStreamProperties = new List<ODataProperty>();
				if (selectedStreamProperties.Count > 0)
				{
					foreach (string text in selectedStreamProperties.Keys)
					{
						ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
						odataStreamReferenceValue.SetMetadataBuilder(this, text);
						this.computedStreamProperties.Add(new ODataProperty
						{
							Name = text,
							Value = odataStreamReferenceValue
						});
					}
				}
			}
			return this.computedStreamProperties;
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x000463F0 File Offset: 0x000445F0
		private Uri GetContainingEntitySetUri(Uri baseUri, ODataUri odataUri)
		{
			ODataPath path = odataUri.Path;
			List<ODataPathSegment> list = Enumerable.ToList<ODataPathSegment>(path);
			ODataPathSegment odataPathSegment = Enumerable.Last<ODataPathSegment>(list);
			while (!(odataPathSegment is NavigationPropertySegment) && !(odataPathSegment is OperationSegment))
			{
				list.Remove(odataPathSegment);
				odataPathSegment = Enumerable.Last<ODataPathSegment>(list);
			}
			list.Remove(odataPathSegment);
			ODataPathSegment odataPathSegment2 = Enumerable.Last<ODataPathSegment>(list);
			while (odataPathSegment2 is TypeSegment)
			{
				ODataPathSegment odataPathSegment3 = list[list.Count - 2];
				IEdmStructuredType edmStructuredType = odataPathSegment3.TargetEdmType as IEdmStructuredType;
				if (edmStructuredType == null || edmStructuredType.FindProperty(odataPathSegment.Identifier) == null)
				{
					break;
				}
				list.Remove(odataPathSegment2);
				odataPathSegment2 = Enumerable.Last<ODataPathSegment>(list);
			}
			Uri uri = baseUri;
			foreach (ODataPathSegment odataPathSegment4 in list)
			{
				KeySegment keySegment = odataPathSegment4 as KeySegment;
				if (keySegment == null)
				{
					uri = this.UriBuilder.BuildEntitySetUri(uri, odataPathSegment4.Identifier);
				}
				else
				{
					uri = this.UriBuilder.BuildEntityInstanceUri(uri, Enumerable.ToList<KeyValuePair<string, object>>(keySegment.Keys), keySegment.EdmType.FullTypeName());
				}
			}
			return uri;
		}

		// Token: 0x04000A75 RID: 2677
		private Uri computedEditLink;

		// Token: 0x04000A76 RID: 2678
		private Uri computedReadLink;

		// Token: 0x04000A77 RID: 2679
		private string computedETag;

		// Token: 0x04000A78 RID: 2680
		private bool etagComputed;

		// Token: 0x04000A79 RID: 2681
		private Uri computedId;

		// Token: 0x04000A7A RID: 2682
		private ODataStreamReferenceValue computedMediaResource;

		// Token: 0x04000A7B RID: 2683
		private List<ODataProperty> computedStreamProperties;

		// Token: 0x04000A7C RID: 2684
		private bool isResourceEnd;

		// Token: 0x04000A7D RID: 2685
		private IEnumerator<ODataJsonLightReaderNestedResourceInfo> unprocessedNestedResourceInfos;

		// Token: 0x04000A7E RID: 2686
		private ODataMissingOperationGenerator missingOperationGenerator;

		// Token: 0x04000A7F RID: 2687
		private ICollection<KeyValuePair<string, object>> computedKeyProperties;
	}
}
