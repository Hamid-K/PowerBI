using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000138 RID: 312
	internal sealed class ODataConventionalEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x0001A5D4 File Offset: 0x000187D4
		internal ODataConventionalEntityMetadataBuilder(IODataEntryMetadataContext entryMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
		{
			this.entryMetadataContext = entryMetadataContext;
			this.uriBuilder = uriBuilder;
			this.metadataContext = metadataContext;
			this.processedNavigationLinks = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001A601 File Offset: 0x00018801
		private string ComputedId
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedId;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001A60F File Offset: 0x0001880F
		private Uri ComputedEntityInstanceUri
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedEntityInstanceUri;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001A620 File Offset: 0x00018820
		private ODataMissingOperationGenerator MissingOperationGenerator
		{
			get
			{
				ODataMissingOperationGenerator odataMissingOperationGenerator;
				if ((odataMissingOperationGenerator = this.missingOperationGenerator) == null)
				{
					odataMissingOperationGenerator = (this.missingOperationGenerator = new ODataMissingOperationGenerator(this.entryMetadataContext, this.metadataContext));
				}
				return odataMissingOperationGenerator;
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001A654 File Offset: 0x00018854
		internal override Uri GetEditLink()
		{
			Uri nonComputedEditLink;
			if (!this.entryMetadataContext.Entry.HasNonComputedEditLink)
			{
				if ((nonComputedEditLink = this.computedEditLink) == null)
				{
					return this.computedEditLink = this.ComputeEditLink();
				}
			}
			else
			{
				nonComputedEditLink = this.entryMetadataContext.Entry.NonComputedEditLink;
			}
			return nonComputedEditLink;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001A6A0 File Offset: 0x000188A0
		internal override Uri GetReadLink()
		{
			Uri nonComputedReadLink;
			if (!this.entryMetadataContext.Entry.HasNonComputedReadLink)
			{
				if ((nonComputedReadLink = this.computedReadLink) == null)
				{
					return this.computedReadLink = this.GetEditLink();
				}
			}
			else
			{
				nonComputedReadLink = this.entryMetadataContext.Entry.NonComputedReadLink;
			}
			return nonComputedReadLink;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001A6EC File Offset: 0x000188EC
		internal override string GetId()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedId)
			{
				return this.entryMetadataContext.Entry.NonComputedId;
			}
			if (this.entryMetadataContext.Entry.HasNonComputedReadLink)
			{
				return UriUtilsCommon.UriToString(this.entryMetadataContext.Entry.NonComputedReadLink);
			}
			if (this.entryMetadataContext.Entry.NonComputedEditLink != null)
			{
				return UriUtilsCommon.UriToString(this.entryMetadataContext.Entry.NonComputedEditLink);
			}
			return this.ComputedId;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001A778 File Offset: 0x00018978
		internal override string GetETag()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedETag)
			{
				return this.entryMetadataContext.Entry.NonComputedETag;
			}
			if (!this.etagComputed)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, object> keyValuePair in this.entryMetadataContext.ETagProperties)
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

		// Token: 0x0600081B RID: 2075 RVA: 0x0001A870 File Offset: 0x00018A70
		internal override ODataStreamReferenceValue GetMediaResource()
		{
			if (this.entryMetadataContext.Entry.NonComputedMediaResource != null)
			{
				return this.entryMetadataContext.Entry.NonComputedMediaResource;
			}
			if (this.computedMediaResource == null && this.entryMetadataContext.TypeContext.IsMediaLinkEntry)
			{
				this.computedMediaResource = new ODataStreamReferenceValue();
				this.computedMediaResource.SetMetadataBuilder(this, null);
			}
			return this.computedMediaResource;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001A8D8 File Offset: 0x00018AD8
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataProperty>(nonComputedProperties, this.GetComputedStreamProperties(nonComputedProperties));
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001A8E7 File Offset: 0x00018AE7
		internal override IEnumerable<ODataAction> GetActions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataAction>(this.entryMetadataContext.Entry.NonComputedActions, this.MissingOperationGenerator.GetComputedActions());
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001A909 File Offset: 0x00018B09
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataFunction>(this.entryMetadataContext.Entry.NonComputedFunctions, this.MissingOperationGenerator.GetComputedFunctions());
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001A92B File Offset: 0x00018B2B
		internal override void MarkNavigationLinkProcessed(string navigationPropertyName)
		{
			this.processedNavigationLinks.Add(navigationPropertyName);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0001A950 File Offset: 0x00018B50
		internal override ODataJsonLightReaderNavigationLinkInfo GetNextUnprocessedNavigationLink()
		{
			if (this.unprocessedNavigationLinks == null)
			{
				this.unprocessedNavigationLinks = Enumerable.Select<IEdmNavigationProperty, ODataJsonLightReaderNavigationLinkInfo>(Enumerable.Where<IEdmNavigationProperty>(this.entryMetadataContext.SelectedNavigationProperties, (IEdmNavigationProperty p) => !this.processedNavigationLinks.Contains(p.Name)), new Func<IEdmNavigationProperty, ODataJsonLightReaderNavigationLinkInfo>(ODataJsonLightReaderNavigationLinkInfo.CreateProjectedNavigationLinkInfo)).GetEnumerator();
			}
			if (this.unprocessedNavigationLinks.MoveNext())
			{
				return this.unprocessedNavigationLinks.Current;
			}
			return null;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x0001A9BE File Offset: 0x00018BBE
		internal override Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.uriBuilder.BuildStreamEditLinkUri(this.GetEditLink(), streamPropertyName);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001A9DD File Offset: 0x00018BDD
		internal override Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.uriBuilder.BuildStreamReadLinkUri(this.GetReadLink(), streamPropertyName);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001A9FC File Offset: 0x00018BFC
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasNavigationLinkUrl)
			{
				return this.uriBuilder.BuildNavigationLinkUri(this.GetEditLink(), navigationPropertyName);
			}
			return navigationLinkUrl;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001AA20 File Offset: 0x00018C20
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasAssociationLinkUrl)
			{
				return this.uriBuilder.BuildAssociationLinkUri(this.GetEditLink(), navigationPropertyName);
			}
			return associationLinkUrl;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001AA44 File Offset: 0x00018C44
		internal override Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			Uri editLink;
			if (string.IsNullOrEmpty(bindingParameterTypeName) || this.entryMetadataContext.Entry.NonComputedEditLink != null)
			{
				editLink = this.GetEditLink();
			}
			else
			{
				editLink = this.ComputedEntityInstanceUri;
			}
			return this.uriBuilder.BuildOperationTargetUri(editLink, operationName, bindingParameterTypeName);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001AA9A File Offset: 0x00018C9A
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		private Uri ComputeEditLink()
		{
			Uri uri = this.ComputedEntityInstanceUri;
			if (this.entryMetadataContext.ActualEntityTypeName != this.entryMetadataContext.TypeContext.EntitySetElementTypeName)
			{
				uri = this.uriBuilder.AppendTypeSegment(uri, this.entryMetadataContext.ActualEntityTypeName);
			}
			return uri;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0001AAF8 File Offset: 0x00018CF8
		private void ComputeAndCacheId()
		{
			if (this.computedEntityInstanceUri == null)
			{
				Uri uri = this.uriBuilder.BuildBaseUri();
				uri = this.uriBuilder.BuildEntitySetUri(uri, this.entryMetadataContext.TypeContext.EntitySetName);
				uri = this.uriBuilder.BuildEntityInstanceUri(uri, this.entryMetadataContext.KeyProperties, this.entryMetadataContext.ActualEntityTypeName);
				this.computedEntityInstanceUri = uri;
				this.computedId = UriUtilsCommon.UriToString(uri);
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001AB74 File Offset: 0x00018D74
		private IEnumerable<ODataProperty> GetComputedStreamProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (this.computedStreamProperties == null)
			{
				IDictionary<string, IEdmStructuralProperty> selectedStreamProperties = this.entryMetadataContext.SelectedStreamProperties;
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

		// Token: 0x04000320 RID: 800
		private readonly ODataUriBuilder uriBuilder;

		// Token: 0x04000321 RID: 801
		private readonly IODataEntryMetadataContext entryMetadataContext;

		// Token: 0x04000322 RID: 802
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x04000323 RID: 803
		private readonly HashSet<string> processedNavigationLinks;

		// Token: 0x04000324 RID: 804
		private Uri computedEditLink;

		// Token: 0x04000325 RID: 805
		private Uri computedReadLink;

		// Token: 0x04000326 RID: 806
		private string computedETag;

		// Token: 0x04000327 RID: 807
		private bool etagComputed;

		// Token: 0x04000328 RID: 808
		private string computedId;

		// Token: 0x04000329 RID: 809
		private Uri computedEntityInstanceUri;

		// Token: 0x0400032A RID: 810
		private ODataStreamReferenceValue computedMediaResource;

		// Token: 0x0400032B RID: 811
		private List<ODataProperty> computedStreamProperties;

		// Token: 0x0400032C RID: 812
		private IEnumerator<ODataJsonLightReaderNavigationLinkInfo> unprocessedNavigationLinks;

		// Token: 0x0400032D RID: 813
		private ODataMissingOperationGenerator missingOperationGenerator;
	}
}
