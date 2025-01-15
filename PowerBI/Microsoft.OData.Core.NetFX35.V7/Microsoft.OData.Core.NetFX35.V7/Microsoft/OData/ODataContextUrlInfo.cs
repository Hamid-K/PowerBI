using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData
{
	// Token: 0x0200004A RID: 74
	internal sealed class ODataContextUrlInfo
	{
		// Token: 0x06000261 RID: 609 RVA: 0x000093C0 File Offset: 0x000075C0
		private ODataContextUrlInfo()
		{
			this.DeltaKind = ODataDeltaKind.None;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000093CF File Offset: 0x000075CF
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000093D7 File Offset: 0x000075D7
		internal ODataDeltaKind DeltaKind { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000093E0 File Offset: 0x000075E0
		// (set) Token: 0x06000265 RID: 613 RVA: 0x000093E8 File Offset: 0x000075E8
		internal bool IsUnknownEntitySet { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000093F1 File Offset: 0x000075F1
		// (set) Token: 0x06000267 RID: 615 RVA: 0x000093F9 File Offset: 0x000075F9
		internal bool HasNavigationSourceInfo { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00009402 File Offset: 0x00007602
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000940A File Offset: 0x0000760A
		internal string NavigationPath { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00009413 File Offset: 0x00007613
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000941B File Offset: 0x0000761B
		internal string NavigationSource { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00009424 File Offset: 0x00007624
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000942C File Offset: 0x0000762C
		internal string ResourcePath { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00009435 File Offset: 0x00007635
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000943D File Offset: 0x0000763D
		internal bool? IsUndeclared { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009446 File Offset: 0x00007646
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000944E File Offset: 0x0000764E
		internal string QueryClause { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00009457 File Offset: 0x00007657
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000945F File Offset: 0x0000765F
		internal string TypeName { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000274 RID: 628 RVA: 0x00009468 File Offset: 0x00007668
		// (set) Token: 0x06000275 RID: 629 RVA: 0x00009470 File Offset: 0x00007670
		internal string TypeCast { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00009479 File Offset: 0x00007679
		// (set) Token: 0x06000277 RID: 631 RVA: 0x00009481 File Offset: 0x00007681
		internal bool IncludeFragmentItemSelector { get; private set; }

		// Token: 0x06000278 RID: 632 RVA: 0x0000948A File Offset: 0x0000768A
		internal static ODataContextUrlInfo Create(ODataValue value, ODataUri odataUri = null, IEdmModel model = null)
		{
			return new ODataContextUrlInfo
			{
				TypeName = ODataContextUrlInfo.GetTypeNameForValue(value, model),
				ResourcePath = ODataContextUrlInfo.ComputeResourcePath(odataUri),
				QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri),
				IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri)
			};
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000094C4 File Offset: 0x000076C4
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

		// Token: 0x0600027A RID: 634 RVA: 0x000094FC File Offset: 0x000076FC
		internal static ODataContextUrlInfo Create(IEdmNavigationSource navigationSource, string expectedEntityTypeName, bool isSingle, ODataUri odataUri)
		{
			EdmNavigationSourceKind edmNavigationSourceKind = navigationSource.NavigationSourceKind();
			string text = navigationSource.EntityType().FullName();
			return new ODataContextUrlInfo
			{
				IsUnknownEntitySet = (edmNavigationSourceKind == EdmNavigationSourceKind.UnknownEntitySet),
				NavigationSource = navigationSource.Name,
				TypeCast = ((text == expectedEntityTypeName) ? null : expectedEntityTypeName),
				TypeName = text,
				IncludeFragmentItemSelector = (isSingle && edmNavigationSourceKind != EdmNavigationSourceKind.Singleton),
				NavigationPath = ODataContextUrlInfo.ComputeNavigationPath(edmNavigationSourceKind, odataUri, navigationSource.Name),
				ResourcePath = ODataContextUrlInfo.ComputeResourcePath(odataUri),
				QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri),
				IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri)
			};
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000959C File Offset: 0x0000779C
		internal static ODataContextUrlInfo Create(ODataResourceTypeContext typeContext, bool isSingle, ODataUri odataUri = null)
		{
			bool flag = typeContext.NavigationSourceKind != EdmNavigationSourceKind.None || !string.IsNullOrEmpty(typeContext.NavigationSourceName);
			string text = (flag ? typeContext.NavigationSourceFullTypeName : ((typeContext.ExpectedResourceTypeName == null) ? null : (isSingle ? typeContext.ExpectedResourceTypeName : EdmLibraryExtensions.GetCollectionTypeName(typeContext.ExpectedResourceTypeName))));
			return new ODataContextUrlInfo
			{
				HasNavigationSourceInfo = flag,
				IsUnknownEntitySet = (typeContext.NavigationSourceKind == EdmNavigationSourceKind.UnknownEntitySet),
				NavigationSource = typeContext.NavigationSourceName,
				TypeCast = ((typeContext.NavigationSourceEntityTypeName == null || typeContext.ExpectedResourceTypeName == null || typeContext.ExpectedResourceType is IEdmComplexType || typeContext.NavigationSourceEntityTypeName == typeContext.ExpectedResourceTypeName) ? null : typeContext.ExpectedResourceTypeName),
				TypeName = text,
				IncludeFragmentItemSelector = (isSingle && typeContext.NavigationSourceKind != EdmNavigationSourceKind.Singleton),
				NavigationPath = ODataContextUrlInfo.ComputeNavigationPath(typeContext.NavigationSourceKind, odataUri, typeContext.NavigationSourceName),
				ResourcePath = ODataContextUrlInfo.ComputeResourcePath(odataUri),
				QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri),
				IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri)
			};
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000096B4 File Offset: 0x000078B4
		internal static ODataContextUrlInfo Create(ODataResourceTypeContext typeContext, ODataDeltaKind kind, ODataUri odataUri = null)
		{
			ODataContextUrlInfo odataContextUrlInfo = new ODataContextUrlInfo
			{
				IsUnknownEntitySet = (typeContext.NavigationSourceKind == EdmNavigationSourceKind.UnknownEntitySet),
				NavigationSource = typeContext.NavigationSourceName,
				TypeCast = ((typeContext.NavigationSourceEntityTypeName == typeContext.ExpectedResourceTypeName) ? null : typeContext.ExpectedResourceTypeName),
				TypeName = typeContext.NavigationSourceEntityTypeName,
				IncludeFragmentItemSelector = (kind == ODataDeltaKind.Resource && typeContext.NavigationSourceKind != EdmNavigationSourceKind.Singleton),
				DeltaKind = kind,
				NavigationPath = ODataContextUrlInfo.ComputeNavigationPath(typeContext.NavigationSourceKind, null, typeContext.NavigationSourceName)
			};
			if (typeContext is ODataResourceTypeContext.ODataResourceTypeContextWithModel)
			{
				odataContextUrlInfo.NavigationPath = ODataContextUrlInfo.ComputeNavigationPath(typeContext.NavigationSourceKind, odataUri, typeContext.NavigationSourceName);
				odataContextUrlInfo.ResourcePath = ODataContextUrlInfo.ComputeResourcePath(odataUri);
				odataContextUrlInfo.QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri);
				odataContextUrlInfo.IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri);
			}
			return odataContextUrlInfo;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000978D File Offset: 0x0000798D
		internal bool IsHiddenBy(ODataContextUrlInfo parentContextUrlInfo)
		{
			return parentContextUrlInfo != null && (parentContextUrlInfo.NavigationPath == this.NavigationPath && parentContextUrlInfo.DeltaKind == ODataDeltaKind.ResourceSet && this.DeltaKind == ODataDeltaKind.Resource);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000097BC File Offset: 0x000079BC
		private static string ComputeNavigationPath(EdmNavigationSourceKind kind, ODataUri odataUri, string navigationSource)
		{
			bool flag = kind == EdmNavigationSourceKind.ContainedEntitySet;
			bool flag2 = kind == EdmNavigationSourceKind.UnknownEntitySet;
			if (flag2)
			{
				return null;
			}
			string text = null;
			if (flag && odataUri != null && odataUri.Path != null)
			{
				ODataPath odataPath = odataUri.Path.TrimEndingTypeSegment().TrimEndingKeySegment();
				if (!(odataPath.LastSegment is NavigationPropertySegment) && !(odataPath.LastSegment is OperationSegment))
				{
					throw new ODataException(Strings.ODataContextUriBuilder_ODataPathInvalidForContainedElement(odataPath.ToContextUrlPathString()));
				}
				text = odataPath.ToContextUrlPathString();
			}
			return text ?? navigationSource;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00009831 File Offset: 0x00007A31
		private static string ComputeResourcePath(ODataUri odataUri)
		{
			if (odataUri != null && odataUri.Path != null && odataUri.Path.IsIndividualProperty())
			{
				return odataUri.Path.ToContextUrlPathString();
			}
			return string.Empty;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000985C File Offset: 0x00007A5C
		private static string ComputeQueryClause(ODataUri odataUri)
		{
			if (odataUri == null)
			{
				return null;
			}
			if (odataUri.Apply != null)
			{
				return ODataContextUrlInfo.CreateApplyUriSegment(odataUri.Apply);
			}
			return ODataContextUrlInfo.CreateSelectExpandContextUriSegment(odataUri.SelectAndExpand);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00009884 File Offset: 0x00007A84
		private static bool? ComputeIfIsUndeclared(ODataUri odataUri)
		{
			if (odataUri != null && odataUri.Path != null)
			{
				return new bool?(odataUri.Path.IsUndeclared());
			}
			return default(bool?);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000098B8 File Offset: 0x00007AB8
		private static string GetTypeNameForValue(ODataValue value, IEdmModel model)
		{
			if (value == null)
			{
				return null;
			}
			if (value.TypeAnnotation != null && !string.IsNullOrEmpty(value.TypeAnnotation.TypeName))
			{
				return value.TypeAnnotation.TypeName;
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
				throw new ODataException(Strings.ODataContextUriBuilder_StreamValueMustBePropertiesOfODataResource);
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

		// Token: 0x06000283 RID: 643 RVA: 0x00009975 File Offset: 0x00007B75
		private static string CreateApplyUriSegment(ApplyClause applyClause)
		{
			if (applyClause != null)
			{
				return applyClause.GetContextUri();
			}
			return string.Empty;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00009988 File Offset: 0x00007B88
		private static string CreateSelectExpandContextUriSegment(SelectExpandClause selectExpandClause)
		{
			if (selectExpandClause != null)
			{
				string text;
				selectExpandClause.Traverse(new Func<string, string, string>(ODataContextUrlInfo.ProcessSubExpand), new Func<IList<string>, IList<string>, string>(ODataContextUrlInfo.CombineSelectAndExpandResult), out text);
				if (!string.IsNullOrEmpty(text))
				{
					return "(" + text + ")";
				}
			}
			return string.Empty;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000099D6 File Offset: 0x00007BD6
		private static string ProcessSubExpand(string expandNode, string subExpand)
		{
			if (!string.IsNullOrEmpty(subExpand))
			{
				return expandNode + "(" + subExpand + ")";
			}
			return null;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000099F4 File Offset: 0x00007BF4
		private static string CombineSelectAndExpandResult(IList<string> selectList, IList<string> expandList)
		{
			string text = string.Empty;
			if (Enumerable.Any<string>(selectList))
			{
				foreach (string text2 in expandList)
				{
					string text3 = text2.Substring(0, text2.IndexOf('('));
					selectList.Remove(text3);
				}
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
	}
}
