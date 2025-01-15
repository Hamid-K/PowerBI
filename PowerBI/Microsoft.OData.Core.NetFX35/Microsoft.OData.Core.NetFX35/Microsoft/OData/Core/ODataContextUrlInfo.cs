using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200015C RID: 348
	internal sealed class ODataContextUrlInfo
	{
		// Token: 0x06000CFF RID: 3327 RVA: 0x000309A6 File Offset: 0x0002EBA6
		private ODataContextUrlInfo()
		{
			this.DeltaKind = ODataDeltaKind.None;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x000309B5 File Offset: 0x0002EBB5
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x000309BD File Offset: 0x0002EBBD
		internal ODataDeltaKind DeltaKind { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000309C6 File Offset: 0x0002EBC6
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x000309CE File Offset: 0x0002EBCE
		internal bool IsUnknownEntitySet { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x000309D8 File Offset: 0x0002EBD8
		internal string NavigationPath
		{
			get
			{
				if (this.IsUnknownEntitySet)
				{
					return null;
				}
				string text = null;
				if (this.isContained && this.odataUri != null && this.odataUri.Path != null)
				{
					ODataPath odataPath = this.odataUri.Path.TrimEndingTypeSegment().TrimEndingKeySegment();
					if (!(odataPath.LastSegment is NavigationPropertySegment))
					{
						throw new ODataException(Strings.ODataContextUriBuilder_ODataPathInvalidForContainedElement(odataPath.ToContextUrlPathString()));
					}
					text = odataPath.ToContextUrlPathString();
				}
				return text ?? this.navigationSource;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x00030A54 File Offset: 0x0002EC54
		internal string NavigationSource
		{
			get
			{
				return this.navigationSource;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x00030A5C File Offset: 0x0002EC5C
		internal string ResourcePath
		{
			get
			{
				if (this.odataUri != null && this.odataUri.Path != null && this.odataUri.Path.IsIndividualProperty())
				{
					return this.odataUri.Path.ToContextUrlPathString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x00030A9B File Offset: 0x0002EC9B
		internal string QueryClause
		{
			get
			{
				if (this.odataUri == null)
				{
					return null;
				}
				if (this.odataUri.Apply != null)
				{
					return ODataContextUrlInfo.CreateApplyUriSegment(this.odataUri.Apply);
				}
				return ODataContextUrlInfo.CreateSelectExpandContextUriSegment(this.odataUri.SelectAndExpand);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x00030AD5 File Offset: 0x0002ECD5
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x00030ADD File Offset: 0x0002ECDD
		internal string TypeName { get; private set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00030AE6 File Offset: 0x0002ECE6
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x00030AEE File Offset: 0x0002ECEE
		internal string TypeCast { get; private set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00030AF7 File Offset: 0x0002ECF7
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00030AFF File Offset: 0x0002ECFF
		internal bool IncludeFragmentItemSelector { get; private set; }

		// Token: 0x06000D0E RID: 3342 RVA: 0x00030B08 File Offset: 0x0002ED08
		internal static ODataContextUrlInfo Create(ODataValue value, ODataUri odataUri = null, IEdmModel model = null)
		{
			return new ODataContextUrlInfo
			{
				TypeName = ODataContextUrlInfo.GetTypeNameForValue(value, model),
				odataUri = odataUri
			};
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00030B30 File Offset: 0x0002ED30
		internal static ODataContextUrlInfo Create(ODataCollectionStartSerializationInfo info, IEdmTypeReference itemTypeReference)
		{
			string text = null;
			if (info != null)
			{
				text = info.CollectionTypeName;
			}
			else if (itemTypeReference != null)
			{
				text = EdmLibraryExtensions.GetCollectionTypeName(itemTypeReference.FullName());
			}
			return new ODataContextUrlInfo
			{
				TypeName = text
			};
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00030B68 File Offset: 0x0002ED68
		internal static ODataContextUrlInfo Create(IEdmNavigationSource navigationSource, string expectedEntityTypeName, bool isSingle, ODataUri odataUri)
		{
			EdmNavigationSourceKind edmNavigationSourceKind = navigationSource.NavigationSourceKind();
			string text = navigationSource.EntityType().FullName();
			return new ODataContextUrlInfo
			{
				isContained = (edmNavigationSourceKind == EdmNavigationSourceKind.ContainedEntitySet),
				IsUnknownEntitySet = (edmNavigationSourceKind == EdmNavigationSourceKind.UnknownEntitySet),
				navigationSource = navigationSource.Name,
				TypeCast = ((text == expectedEntityTypeName) ? null : expectedEntityTypeName),
				TypeName = text,
				IncludeFragmentItemSelector = (isSingle && edmNavigationSourceKind != EdmNavigationSourceKind.Singleton),
				odataUri = odataUri
			};
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00030BE4 File Offset: 0x0002EDE4
		internal static ODataContextUrlInfo Create(ODataFeedAndEntryTypeContext typeContext, bool isSingle, ODataUri odataUri = null)
		{
			return new ODataContextUrlInfo
			{
				isContained = (typeContext.NavigationSourceKind == EdmNavigationSourceKind.ContainedEntitySet),
				IsUnknownEntitySet = (typeContext.NavigationSourceKind == EdmNavigationSourceKind.UnknownEntitySet),
				navigationSource = typeContext.NavigationSourceName,
				TypeCast = ((typeContext.NavigationSourceEntityTypeName == typeContext.ExpectedEntityTypeName) ? null : typeContext.ExpectedEntityTypeName),
				TypeName = typeContext.NavigationSourceFullTypeName,
				IncludeFragmentItemSelector = (isSingle && typeContext.NavigationSourceKind != EdmNavigationSourceKind.Singleton),
				odataUri = odataUri
			};
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00030C70 File Offset: 0x0002EE70
		internal static ODataContextUrlInfo Create(ODataFeedAndEntryTypeContext typeContext, ODataDeltaKind kind, ODataUri odataUri = null)
		{
			ODataContextUrlInfo odataContextUrlInfo = new ODataContextUrlInfo
			{
				isContained = (typeContext.NavigationSourceKind == EdmNavigationSourceKind.ContainedEntitySet),
				IsUnknownEntitySet = (typeContext.NavigationSourceKind == EdmNavigationSourceKind.UnknownEntitySet),
				navigationSource = typeContext.NavigationSourceName,
				TypeCast = ((typeContext.NavigationSourceEntityTypeName == typeContext.ExpectedEntityTypeName) ? null : typeContext.ExpectedEntityTypeName),
				TypeName = typeContext.NavigationSourceEntityTypeName,
				IncludeFragmentItemSelector = (kind == ODataDeltaKind.Entry && typeContext.NavigationSourceKind != EdmNavigationSourceKind.Singleton),
				DeltaKind = kind
			};
			if (typeContext is ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithModel)
			{
				odataContextUrlInfo.odataUri = odataUri;
			}
			return odataContextUrlInfo;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00030D0D File Offset: 0x0002EF0D
		internal bool IsHiddenBy(ODataContextUrlInfo parentContextUrlInfo)
		{
			return parentContextUrlInfo != null && (parentContextUrlInfo.NavigationPath == this.NavigationPath && parentContextUrlInfo.DeltaKind == ODataDeltaKind.Feed && this.DeltaKind == ODataDeltaKind.Entry);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00030D3C File Offset: 0x0002EF3C
		private static string GetTypeNameForValue(ODataValue value, IEdmModel model)
		{
			if (value == null)
			{
				return null;
			}
			if (value.IsNullValue)
			{
				return "Edm.Null";
			}
			SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null && !string.IsNullOrEmpty(annotation.TypeName))
			{
				return annotation.TypeName;
			}
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return odataComplexValue.TypeName;
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return EdmLibraryExtensions.GetCollectionTypeFullName(odataCollectionValue.TypeName);
			}
			ODataEnumValue odataEnumValue = value as ODataEnumValue;
			if (odataEnumValue != null)
			{
				return odataEnumValue.TypeName;
			}
			ODataUntypedValue odataUntypedValue = value as ODataUntypedValue;
			if (odataUntypedValue != null)
			{
				return "Edm.Untyped";
			}
			ODataPrimitiveValue odataPrimitiveValue = value as ODataPrimitiveValue;
			if (odataPrimitiveValue == null)
			{
				throw new ODataException(Strings.ODataContextUriBuilder_StreamValueMustBePropertiesOfODataEntry);
			}
			IEdmTypeDefinitionReference edmTypeDefinitionReference = model.ResolveUIntTypeDefinition(odataPrimitiveValue.Value);
			if (edmTypeDefinitionReference != null)
			{
				return edmTypeDefinitionReference.FullName();
			}
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(odataPrimitiveValue.Value.GetType());
			if (primitiveTypeReference != null)
			{
				return primitiveTypeReference.FullName();
			}
			return null;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00030E16 File Offset: 0x0002F016
		private static string CreateApplyUriSegment(ApplyClause applyClause)
		{
			if (applyClause != null)
			{
				return applyClause.GetContextUri();
			}
			return string.Empty;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00030E28 File Offset: 0x0002F028
		private static string CreateSelectExpandContextUriSegment(SelectExpandClause selectExpandClause)
		{
			if (selectExpandClause != null)
			{
				string text;
				selectExpandClause.Traverse(new Func<string, string, string>(ODataContextUrlInfo.ProcessSubExpand), new Func<IList<string>, IList<string>, string>(ODataContextUrlInfo.CombineSelectAndExpandResult), out text);
				if (!string.IsNullOrEmpty(text))
				{
					return '(' + text + ')';
				}
			}
			return string.Empty;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00030E7C File Offset: 0x0002F07C
		private static string ProcessSubExpand(string expandNode, string subExpand)
		{
			if (!string.IsNullOrEmpty(subExpand))
			{
				return string.Concat(new object[] { expandNode, '(', subExpand, ')' });
			}
			return null;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00030EBC File Offset: 0x0002F0BC
		private static string CombineSelectAndExpandResult(IList<string> selectList, IList<string> expandList)
		{
			string text = string.Empty;
			if (Enumerable.Any<string>(selectList))
			{
				text += string.Join(",", Enumerable.ToArray<string>(selectList));
			}
			if (Enumerable.Any<string>(expandList))
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += ",";
				}
				text += string.Join(",", Enumerable.ToArray<string>(expandList));
			}
			return text;
		}

		// Token: 0x0400059C RID: 1436
		private bool isContained;

		// Token: 0x0400059D RID: 1437
		private string navigationSource;

		// Token: 0x0400059E RID: 1438
		private ODataUri odataUri;
	}
}
