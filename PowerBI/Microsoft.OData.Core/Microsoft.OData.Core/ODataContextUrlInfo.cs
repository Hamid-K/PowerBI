using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.OData
{
	// Token: 0x0200006C RID: 108
	internal sealed class ODataContextUrlInfo
	{
		// Token: 0x060003DA RID: 986 RVA: 0x0000B160 File Offset: 0x00009360
		private ODataContextUrlInfo()
		{
			this.DeltaKind = ODataDeltaKind.None;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000B16F File Offset: 0x0000936F
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000B177 File Offset: 0x00009377
		internal ODataDeltaKind DeltaKind { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000B180 File Offset: 0x00009380
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000B188 File Offset: 0x00009388
		internal bool IsUnknownEntitySet { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000B191 File Offset: 0x00009391
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000B199 File Offset: 0x00009399
		internal bool HasNavigationSourceInfo { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000B1A2 File Offset: 0x000093A2
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000B1AA File Offset: 0x000093AA
		internal string NavigationPath { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000B1B3 File Offset: 0x000093B3
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000B1BB File Offset: 0x000093BB
		internal string NavigationSource { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000B1C4 File Offset: 0x000093C4
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000B1CC File Offset: 0x000093CC
		internal string ResourcePath { get; private set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B1D5 File Offset: 0x000093D5
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000B1DD File Offset: 0x000093DD
		internal bool? IsUndeclared { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000B1E6 File Offset: 0x000093E6
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000B1EE File Offset: 0x000093EE
		internal string QueryClause { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000B1F7 File Offset: 0x000093F7
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000B1FF File Offset: 0x000093FF
		internal string TypeName { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000B208 File Offset: 0x00009408
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000B210 File Offset: 0x00009410
		internal string TypeCast { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B219 File Offset: 0x00009419
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000B221 File Offset: 0x00009421
		internal bool IncludeFragmentItemSelector { get; private set; }

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000B22A File Offset: 0x0000942A
		internal static ODataContextUrlInfo Create(ODataValue value, ODataVersion version, ODataUri odataUri = null, IEdmModel model = null)
		{
			return new ODataContextUrlInfo
			{
				TypeName = ODataContextUrlInfo.GetTypeNameForValue(value, model),
				ResourcePath = ODataContextUrlInfo.ComputeResourcePath(odataUri),
				QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri, version),
				IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri)
			};
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B264 File Offset: 0x00009464
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

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000B29C File Offset: 0x0000949C
		internal static ODataContextUrlInfo Create(IEdmNavigationSource navigationSource, string expectedEntityTypeName, bool isSingle, ODataUri odataUri, ODataVersion version)
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
				QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri, version),
				IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri)
			};
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B340 File Offset: 0x00009540
		internal static ODataContextUrlInfo Create(ODataResourceTypeContext typeContext, ODataVersion version, bool isSingle, ODataUri odataUri = null)
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
				QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri, version),
				IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri)
			};
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000B458 File Offset: 0x00009658
		internal static ODataContextUrlInfo Create(ODataResourceTypeContext typeContext, ODataVersion version, ODataDeltaKind kind, ODataUri odataUri = null)
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
				odataContextUrlInfo.QueryClause = ODataContextUrlInfo.ComputeQueryClause(odataUri, version);
				odataContextUrlInfo.IsUndeclared = ODataContextUrlInfo.ComputeIfIsUndeclared(odataUri);
			}
			return odataContextUrlInfo;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000B532 File Offset: 0x00009732
		internal bool IsHiddenBy(ODataContextUrlInfo parentContextUrlInfo)
		{
			return parentContextUrlInfo != null && (parentContextUrlInfo.NavigationPath == this.NavigationPath && parentContextUrlInfo.DeltaKind == ODataDeltaKind.ResourceSet && this.DeltaKind == ODataDeltaKind.Resource);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000B564 File Offset: 0x00009764
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

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000B5D9 File Offset: 0x000097D9
		private static string ComputeResourcePath(ODataUri odataUri)
		{
			if (odataUri != null && odataUri.Path != null && odataUri.Path.IsIndividualProperty())
			{
				return odataUri.Path.ToContextUrlPathString();
			}
			return string.Empty;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000B604 File Offset: 0x00009804
		private static string ComputeQueryClause(ODataUri odataUri, ODataVersion version)
		{
			if (odataUri != null)
			{
				if (odataUri.SelectAndExpand != null)
				{
					return ODataContextUrlInfo.CreateSelectExpandContextUriSegment(odataUri.SelectAndExpand);
				}
				if (odataUri.Apply != null)
				{
					return ODataContextUrlInfo.CreateApplyUriSegment(odataUri.Apply);
				}
			}
			return null;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000B634 File Offset: 0x00009834
		private static bool? ComputeIfIsUndeclared(ODataUri odataUri)
		{
			if (odataUri != null && odataUri.Path != null)
			{
				return new bool?(odataUri.Path.IsUndeclared());
			}
			return null;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000B668 File Offset: 0x00009868
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
			ODataResourceValue odataResourceValue = value as ODataResourceValue;
			if (odataResourceValue != null)
			{
				return odataResourceValue.TypeName;
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

		// Token: 0x060003FC RID: 1020 RVA: 0x0000B748 File Offset: 0x00009948
		private static string CreateApplyUriSegment(ApplyClause applyClause)
		{
			if (applyClause != null)
			{
				string contextUri = applyClause.GetContextUri();
				if (!string.IsNullOrEmpty(contextUri))
				{
					return "(" + contextUri + ")";
				}
			}
			return string.Empty;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000B780 File Offset: 0x00009980
		private static string CreateSelectExpandContextUriSegment(SelectExpandClause selectExpandClause)
		{
			if (selectExpandClause != null)
			{
				string text;
				selectExpandClause.Traverse(new Func<string, string, string>(ODataContextUrlInfo.ProcessSubExpand), new Func<IList<string>, IList<string>, string>(ODataContextUrlInfo.CombineSelectAndExpandResult), new Func<ApplyClause, string>(ODataContextUrlInfo.ProcessApply), out text);
				if (!string.IsNullOrEmpty(text))
				{
					return "(" + text + ")";
				}
			}
			return string.Empty;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000B7DA File Offset: 0x000099DA
		private static string ProcessApply(ApplyClause applyClause)
		{
			if (applyClause != null)
			{
				return applyClause.GetContextUri();
			}
			return null;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000B7E7 File Offset: 0x000099E7
		private static string ProcessSubExpand(string expandNode, string subExpand)
		{
			return expandNode + "(" + subExpand + ")";
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000B7FC File Offset: 0x000099FC
		private static string CombineSelectAndExpandResult(IList<string> selectList, IList<string> expandList)
		{
			string text = string.Empty;
			if (selectList.Any<string>())
			{
				text += string.Join(",", selectList.ToArray<string>());
			}
			if (expandList.Any<string>())
			{
				if (!string.IsNullOrEmpty(text))
				{
					text += ",";
				}
				text += string.Join(",", expandList.ToArray<string>());
			}
			return text;
		}
	}
}
