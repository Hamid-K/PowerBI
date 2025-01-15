using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.OData.Core.JsonLight;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000081 RID: 129
	internal sealed class ODataConventionalEntityMetadataBuilder : ODataEntityMetadataBuilder
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x000128A1 File Offset: 0x00010AA1
		internal ODataConventionalEntityMetadataBuilder(IODataEntryMetadataContext entryMetadataContext, IODataMetadataContext metadataContext, ODataUriBuilder uriBuilder)
		{
			this.entryMetadataContext = entryMetadataContext;
			this.uriBuilder = uriBuilder;
			this.metadataContext = metadataContext;
			this.processedNavigationLinks = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000128CE File Offset: 0x00010ACE
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x000128D6 File Offset: 0x00010AD6
		internal ODataUri ODataUri { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x000128DF File Offset: 0x00010ADF
		private Uri ComputedId
		{
			get
			{
				this.ComputeAndCacheId();
				return this.computedId;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x000128F0 File Offset: 0x00010AF0
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00012924 File Offset: 0x00010B24
		private ICollection<KeyValuePair<string, object>> ComputedKeyProperties
		{
			get
			{
				if (this.computedKeyProperties == null)
				{
					this.computedKeyProperties = new List<KeyValuePair<string, object>>();
					foreach (KeyValuePair<string, object> keyValuePair in this.entryMetadataContext.KeyProperties)
					{
						object obj = this.metadataContext.Model.ConvertToUnderlyingTypeIfUIntValue(keyValuePair.Value, null);
						this.computedKeyProperties.Add(new KeyValuePair<string, object>(keyValuePair.Key, obj));
					}
				}
				return this.computedKeyProperties;
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000129BC File Offset: 0x00010BBC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override Uri GetEditLink()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedEditLink)
			{
				return this.entryMetadataContext.Entry.NonComputedEditLink;
			}
			if (this.entryMetadataContext.Entry.IsTransient || this.entryMetadataContext.Entry.HasNonComputedReadLink)
			{
				return null;
			}
			if (this.computedEditLink != null)
			{
				return this.computedEditLink;
			}
			return this.computedEditLink = this.ComputeEditLink();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00012A38 File Offset: 0x00010C38
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override Uri GetReadLink()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedReadLink)
			{
				return this.entryMetadataContext.Entry.NonComputedReadLink;
			}
			if (this.computedReadLink != null)
			{
				return this.computedReadLink;
			}
			return this.computedReadLink = this.GetEditLink();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00012A8C File Offset: 0x00010C8C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override Uri GetId()
		{
			if (this.entryMetadataContext.Entry.HasNonComputedId)
			{
				return this.entryMetadataContext.Entry.NonComputedId;
			}
			if (!this.entryMetadataContext.Entry.IsTransient)
			{
				return this.ComputedId;
			}
			return null;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00012ACC File Offset: 0x00010CCC
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
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

		// Token: 0x06000527 RID: 1319 RVA: 0x00012BC4 File Offset: 0x00010DC4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
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

		// Token: 0x06000528 RID: 1320 RVA: 0x00012C2C File Offset: 0x00010E2C
		internal override IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataProperty>(nonComputedProperties, this.GetComputedStreamProperties(nonComputedProperties));
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00012C3B File Offset: 0x00010E3B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataAction> GetActions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataAction>(this.entryMetadataContext.Entry.NonComputedActions, this.MissingOperationGenerator.GetComputedActions());
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00012C5D File Offset: 0x00010E5D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal override IEnumerable<ODataFunction> GetFunctions()
		{
			return ODataUtilsInternal.ConcatEnumerables<ODataFunction>(this.entryMetadataContext.Entry.NonComputedFunctions, this.MissingOperationGenerator.GetComputedFunctions());
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00012C7F File Offset: 0x00010E7F
		internal override void MarkNavigationLinkProcessed(string navigationPropertyName)
		{
			this.processedNavigationLinks.Add(navigationPropertyName);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00012CA4 File Offset: 0x00010EA4
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
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

		// Token: 0x0600052D RID: 1325 RVA: 0x00012D12 File Offset: 0x00010F12
		internal override Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.uriBuilder.BuildStreamEditLinkUri(this.GetEditLink(), streamPropertyName);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00012D31 File Offset: 0x00010F31
		internal override Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return this.uriBuilder.BuildStreamReadLinkUri(this.GetReadLink(), streamPropertyName);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00012D50 File Offset: 0x00010F50
		internal override Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasNavigationLinkUrl)
			{
				return this.uriBuilder.BuildNavigationLinkUri(this.GetReadLink(), navigationPropertyName);
			}
			return navigationLinkUrl;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00012D74 File Offset: 0x00010F74
		internal override Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			if (!hasAssociationLinkUrl)
			{
				return this.uriBuilder.BuildAssociationLinkUri(this.GetReadLink(), navigationPropertyName);
			}
			return associationLinkUrl;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00012D98 File Offset: 0x00010F98
		internal override Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			Uri uri;
			if (string.IsNullOrEmpty(bindingParameterTypeName) || this.entryMetadataContext.Entry.NonComputedEditLink != null)
			{
				uri = this.GetEditLink();
			}
			else
			{
				uri = this.GetId();
			}
			return this.uriBuilder.BuildOperationTargetUri(uri, operationName, bindingParameterTypeName, parameterNames);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00012DEF File Offset: 0x00010FEF
		internal override string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return operationName;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00012DFD File Offset: 0x00010FFD
		internal override bool TryGetIdForSerialization(out Uri id)
		{
			id = (this.entryMetadataContext.Entry.IsTransient ? null : this.GetId());
			return true;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00012E20 File Offset: 0x00011020
		private Uri ComputeEditLink()
		{
			Uri uri = (this.entryMetadataContext.Entry.HasNonComputedId ? this.entryMetadataContext.Entry.NonComputedId : this.ComputedId);
			if (this.entryMetadataContext.ActualEntityTypeName != this.entryMetadataContext.TypeContext.NavigationSourceEntityTypeName)
			{
				uri = this.uriBuilder.AppendTypeSegment(uri, this.entryMetadataContext.ActualEntityTypeName);
			}
			return uri;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00012E94 File Offset: 0x00011094
		private void ComputeAndCacheId()
		{
			if (this.computedId != null)
			{
				return;
			}
			Uri uri;
			switch (this.entryMetadataContext.TypeContext.NavigationSourceKind)
			{
			case EdmNavigationSourceKind.Singleton:
				uri = this.ComputeIdForSingleton();
				break;
			case EdmNavigationSourceKind.ContainedEntitySet:
				uri = this.ComputeIdForContainment();
				break;
			case EdmNavigationSourceKind.UnknownEntitySet:
				throw new ODataException(Strings.ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing);
			default:
				uri = this.ComputeId();
				break;
			}
			this.computedId = uri;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00012F04 File Offset: 0x00011104
		private Uri ComputeId()
		{
			Uri uri = this.uriBuilder.BuildBaseUri();
			uri = this.uriBuilder.BuildEntitySetUri(uri, this.entryMetadataContext.TypeContext.NavigationSourceName);
			return this.uriBuilder.BuildEntityInstanceUri(uri, this.ComputedKeyProperties, this.entryMetadataContext.ActualEntityTypeName);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00012F5C File Offset: 0x0001115C
		private Uri ComputeIdForContainment()
		{
			ODataConventionalEntityMetadataBuilder odataConventionalEntityMetadataBuilder = base.ParentMetadataBuilder as ODataConventionalEntityMetadataBuilder;
			Uri uri;
			if (odataConventionalEntityMetadataBuilder != null)
			{
				uri = odataConventionalEntityMetadataBuilder.GetId();
				IODataFeedAndEntryTypeContext typeContext = odataConventionalEntityMetadataBuilder.entryMetadataContext.TypeContext;
				if (typeContext.NavigationSourceEntityTypeName != typeContext.ExpectedEntityTypeName)
				{
					ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithModel odataFeedAndEntryTypeContextWithModel = typeContext as ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithModel;
					if (odataFeedAndEntryTypeContextWithModel == null || odataFeedAndEntryTypeContextWithModel.NavigationSourceEntityType.FindProperty(this.entryMetadataContext.TypeContext.NavigationSourceName) == null)
					{
						uri = new Uri(UriUtils.EnsureTaillingSlash(uri), odataConventionalEntityMetadataBuilder.entryMetadataContext.ActualEntityTypeName);
					}
				}
			}
			else
			{
				uri = this.uriBuilder.BuildBaseUri();
				ODataUri odataUri = this.ODataUri ?? this.metadataContext.ODataUri;
				uri = this.GetContainingEntitySetUri(uri, odataUri);
			}
			uri = this.uriBuilder.BuildEntitySetUri(uri, this.entryMetadataContext.TypeContext.NavigationSourceName);
			if (this.entryMetadataContext.TypeContext.IsFromCollection)
			{
				uri = this.uriBuilder.BuildEntityInstanceUri(uri, this.ComputedKeyProperties, this.entryMetadataContext.ActualEntityTypeName);
			}
			return uri;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001305C File Offset: 0x0001125C
		private Uri ComputeIdForSingleton()
		{
			Uri uri = this.uriBuilder.BuildBaseUri();
			return this.uriBuilder.BuildEntitySetUri(uri, this.entryMetadataContext.TypeContext.NavigationSourceName);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00013094 File Offset: 0x00011294
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

		// Token: 0x0600053A RID: 1338 RVA: 0x00013188 File Offset: 0x00011388
		private Uri GetContainingEntitySetUri(Uri baseUri, ODataUri odataUri)
		{
			if (odataUri == null || odataUri.Path == null)
			{
				throw new ODataException(Strings.ODataMetadataBuilder_MissingODataUri);
			}
			ODataPath path = odataUri.Path;
			List<ODataPathSegment> list = Enumerable.ToList<ODataPathSegment>(path);
			ODataPathSegment odataPathSegment = Enumerable.Last<ODataPathSegment>(list);
			while (!(odataPathSegment is NavigationPropertySegment))
			{
				list.Remove(odataPathSegment);
				odataPathSegment = Enumerable.Last<ODataPathSegment>(list);
			}
			list.Remove(odataPathSegment);
			ODataPathSegment odataPathSegment2 = Enumerable.Last<ODataPathSegment>(list);
			while (odataPathSegment2 is TypeSegment)
			{
				ODataPathSegment odataPathSegment3 = list[list.Count - 2];
				IEdmEntityType edmEntityType = odataPathSegment3.TargetEdmType as IEdmEntityType;
				if (edmEntityType == null || edmEntityType.FindProperty(odataPathSegment.Identifier) == null)
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
					uri = this.uriBuilder.BuildEntitySetUri(uri, odataPathSegment4.Identifier);
				}
				else
				{
					uri = this.uriBuilder.BuildEntityInstanceUri(uri, Enumerable.ToList<KeyValuePair<string, object>>(keySegment.Keys), keySegment.EdmType.FullTypeName());
				}
			}
			return uri;
		}

		// Token: 0x0400022B RID: 555
		private readonly ODataUriBuilder uriBuilder;

		// Token: 0x0400022C RID: 556
		private readonly IODataEntryMetadataContext entryMetadataContext;

		// Token: 0x0400022D RID: 557
		private readonly IODataMetadataContext metadataContext;

		// Token: 0x0400022E RID: 558
		private readonly HashSet<string> processedNavigationLinks;

		// Token: 0x0400022F RID: 559
		private Uri computedEditLink;

		// Token: 0x04000230 RID: 560
		private Uri computedReadLink;

		// Token: 0x04000231 RID: 561
		private string computedETag;

		// Token: 0x04000232 RID: 562
		private bool etagComputed;

		// Token: 0x04000233 RID: 563
		private Uri computedId;

		// Token: 0x04000234 RID: 564
		private ODataStreamReferenceValue computedMediaResource;

		// Token: 0x04000235 RID: 565
		private List<ODataProperty> computedStreamProperties;

		// Token: 0x04000236 RID: 566
		private IEnumerator<ODataJsonLightReaderNavigationLinkInfo> unprocessedNavigationLinks;

		// Token: 0x04000237 RID: 567
		private ODataMissingOperationGenerator missingOperationGenerator;

		// Token: 0x04000238 RID: 568
		private ICollection<KeyValuePair<string, object>> computedKeyProperties;
	}
}
