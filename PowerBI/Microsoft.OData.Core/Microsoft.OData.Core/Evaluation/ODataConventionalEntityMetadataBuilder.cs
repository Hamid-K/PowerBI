using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000261 RID: 609
	internal sealed class ODataConventionalEntityMetadataBuilder : ODataConventionalResourceMetadataBuilder
	{
		// Token: 0x06001B68 RID: 7016 RVA: 0x0005469E File Offset: 0x0005289E
		internal ODataConventionalEntityMetadataBuilder(IODataResourceMetadataContext resourceMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
			: base(resourceMetadataContext, metadataContext, uriBuilder)
		{
			this.isResourceEnd = true;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000546B0 File Offset: 0x000528B0
		private Uri ComputedId
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedId;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000546C0 File Offset: 0x000528C0
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

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000546F4 File Offset: 0x000528F4
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

		// Token: 0x06001B6C RID: 7020 RVA: 0x0005478C File Offset: 0x0005298C
		public override Uri GetCanonicalUrl()
		{
			return this.GetId();
		}

		// Token: 0x06001B6D RID: 7021 RVA: 0x00054794 File Offset: 0x00052994
		public override Uri GetEditUrl()
		{
			return this.GetEditLink();
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0005479C File Offset: 0x0005299C
		public override Uri GetReadUrl()
		{
			return this.GetReadLink();
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000547A4 File Offset: 0x000529A4
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

		// Token: 0x06001B70 RID: 7024 RVA: 0x00054820 File Offset: 0x00052A20
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

		// Token: 0x06001B71 RID: 7025 RVA: 0x00054874 File Offset: 0x00052A74
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

		// Token: 0x06001B72 RID: 7026 RVA: 0x000548B4 File Offset: 0x00052AB4
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

		// Token: 0x06001B73 RID: 7027 RVA: 0x000549AC File Offset: 0x00052BAC
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

		// Token: 0x06001B74 RID: 7028 RVA: 0x00054A14 File Offset: 0x00052C14
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (!this.isResourceEnd)
			{
				return nonComputedProperties;
			}
			return ODataUtilsInternal.ConcatEnumerables<ODataProperty>(nonComputedProperties, this.GetComputedStreamProperties(nonComputedProperties));
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00054A2D File Offset: 0x00052C2D
		internal override void StartResource()
		{
			this.isResourceEnd = false;
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x00054A36 File Offset: 0x00052C36
		internal override void EndResource()
		{
			this.isResourceEnd = true;
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x00054A3F File Offset: 0x00052C3F
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataAction> GetActions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataAction>(this.ResourceMetadataContext.Resource.NonComputedActions, this.MissingOperationGenerator.GetComputedActions());
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x00054A61 File Offset: 0x00052C61
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataFunction>(this.ResourceMetadataContext.Resource.NonComputedFunctions, this.MissingOperationGenerator.GetComputedFunctions());
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x00054A83 File Offset: 0x00052C83
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasNestedResourceInfoUrl)
			{
				return this.UriBuilder.BuildNavigationLinkUri(this.GetReadLink(), navigationPropertyName);
			}
			return navigationLinkUrl;
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x00054AA7 File Offset: 0x00052CA7
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasAssociationLinkUrl)
			{
				return this.UriBuilder.BuildAssociationLinkUri(this.GetReadLink(), navigationPropertyName);
			}
			return associationLinkUrl;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00054ACC File Offset: 0x00052CCC
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

		// Token: 0x06001B7C RID: 7036 RVA: 0x00053DA7 File Offset: 0x00051FA7
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00054B23 File Offset: 0x00052D23
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			id = (this.ResourceMetadataContext.Resource.IsTransient ? null : this.GetId());
			return true;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x00054B44 File Offset: 0x00052D44
		private Uri ComputeEditLink()
		{
			Uri uri = (this.ResourceMetadataContext.Resource.HasNonComputedId ? this.ResourceMetadataContext.Resource.NonComputedId : this.ComputedId);
			if (this.ResourceMetadataContext.ActualResourceTypeName != this.ResourceMetadataContext.TypeContext.NavigationSourceEntityTypeName)
			{
				uri = this.UriBuilder.AppendTypeSegment(uri, this.ResourceMetadataContext.ActualResourceTypeName);
			}
			return uri;
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x00054BB8 File Offset: 0x00052DB8
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

		// Token: 0x06001B80 RID: 7040 RVA: 0x00054C38 File Offset: 0x00052E38
		private Uri ComputeId()
		{
			Uri uri = this.UriBuilder.BuildBaseUri();
			uri = this.UriBuilder.BuildEntitySetUri(uri, this.ResourceMetadataContext.TypeContext.NavigationSourceName);
			return this.UriBuilder.BuildEntityInstanceUri(uri, this.ComputedKeyProperties, this.ResourceMetadataContext.ActualResourceTypeName);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x00054C90 File Offset: 0x00052E90
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

		// Token: 0x06001B82 RID: 7042 RVA: 0x00054D44 File Offset: 0x00052F44
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

		// Token: 0x06001B83 RID: 7043 RVA: 0x00054DFC File Offset: 0x00052FFC
		private Uri ComputeIdForSingleton()
		{
			Uri uri = this.UriBuilder.BuildBaseUri();
			return this.UriBuilder.BuildEntitySetUri(uri, this.ResourceMetadataContext.TypeContext.NavigationSourceName);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x00054E34 File Offset: 0x00053034
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

		// Token: 0x06001B85 RID: 7045 RVA: 0x00054F20 File Offset: 0x00053120
		private Uri GetContainingEntitySetUri(Uri baseUri, ODataUri odataUri)
		{
			ODataPath path = odataUri.Path;
			List<ODataPathSegment> list = path.ToList<ODataPathSegment>();
			ODataPathSegment odataPathSegment = list.Last<ODataPathSegment>();
			while (!(odataPathSegment is NavigationPropertySegment) && !(odataPathSegment is OperationSegment))
			{
				list.Remove(odataPathSegment);
				odataPathSegment = list.Last<ODataPathSegment>();
			}
			list.Remove(odataPathSegment);
			ODataPathSegment odataPathSegment2 = list.Last<ODataPathSegment>();
			while (odataPathSegment2 is TypeSegment)
			{
				ODataPathSegment odataPathSegment3 = list[list.Count - 2];
				IEdmStructuredType edmStructuredType = odataPathSegment3.TargetEdmType as IEdmStructuredType;
				if (edmStructuredType == null || edmStructuredType.FindProperty(odataPathSegment.Identifier) == null)
				{
					break;
				}
				list.Remove(odataPathSegment2);
				odataPathSegment2 = list.Last<ODataPathSegment>();
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
					uri = this.UriBuilder.BuildEntityInstanceUri(uri, keySegment.Keys.ToList<KeyValuePair<string, object>>(), keySegment.EdmType.FullTypeName());
				}
			}
			return uri;
		}

		// Token: 0x04000B7C RID: 2940
		private Uri computedEditLink;

		// Token: 0x04000B7D RID: 2941
		private Uri computedReadLink;

		// Token: 0x04000B7E RID: 2942
		private string computedETag;

		// Token: 0x04000B7F RID: 2943
		private bool etagComputed;

		// Token: 0x04000B80 RID: 2944
		private Uri computedId;

		// Token: 0x04000B81 RID: 2945
		private ODataStreamReferenceValue computedMediaResource;

		// Token: 0x04000B82 RID: 2946
		private List<ODataProperty> computedStreamProperties;

		// Token: 0x04000B83 RID: 2947
		private bool isResourceEnd;

		// Token: 0x04000B84 RID: 2948
		private ODataMissingOperationGenerator missingOperationGenerator;

		// Token: 0x04000B85 RID: 2949
		private ICollection<KeyValuePair<string, object>> computedKeyProperties;
	}
}
