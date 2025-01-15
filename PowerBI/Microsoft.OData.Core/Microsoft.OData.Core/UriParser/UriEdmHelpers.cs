using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200011D RID: 285
	internal static class UriEdmHelpers
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x00027154 File Offset: 0x00025354
		public static IEdmSchemaType FindTypeFromModel(IEdmModel model, string qualifiedName, ODataUriResolver resolver)
		{
			return resolver.ResolveType(model, qualifiedName);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00027160 File Offset: 0x00025360
		public static IEdmEnumType FindEnumTypeFromModel(IEdmModel model, string qualifiedName)
		{
			return UriEdmHelpers.FindTypeFromModel(model, qualifiedName, ODataUriResolver.GetUriResolver(null)) as IEdmEnumType;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00027184 File Offset: 0x00025384
		public static void CheckRelatedTo(IEdmType parentType, IEdmType childType)
		{
			if (!UriEdmHelpers.IsRelatedTo(parentType, childType))
			{
				string text = ((parentType != null) ? parentType.FullTypeName() : "<null>");
				throw new ODataException(Strings.MetadataBinder_HierarchyNotFollowed(childType.FullTypeName(), text));
			}
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000271BD File Offset: 0x000253BD
		public static bool IsRelatedTo(IEdmType first, IEdmType second)
		{
			return second.IsOrInheritsFrom(first) || first.IsOrInheritsFrom(second);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000271D4 File Offset: 0x000253D4
		public static IEdmNavigationProperty GetNavigationPropertyFromExpandPath(ODataPath path)
		{
			NavigationPropertySegment navigationPropertySegment = null;
			foreach (ODataPathSegment odataPathSegment in path)
			{
				TypeSegment typeSegment = odataPathSegment as TypeSegment;
				navigationPropertySegment = odataPathSegment as NavigationPropertySegment;
				if (typeSegment == null && navigationPropertySegment == null)
				{
					throw new ODataException(Strings.ExpandItemBinder_TypeSegmentNotFollowedByPath);
				}
			}
			if (navigationPropertySegment == null)
			{
				throw new ODataException(Strings.ExpandItemBinder_TypeSegmentNotFollowedByPath);
			}
			return navigationPropertySegment.NavigationProperty;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002724C File Offset: 0x0002544C
		public static IEdmType GetMostDerivedTypeFromPath(ODataPath path, IEdmType startingType)
		{
			IEdmType edmType = startingType;
			foreach (ODataPathSegment odataPathSegment in path)
			{
				TypeSegment typeSegment = odataPathSegment as TypeSegment;
				if (typeSegment != null && typeSegment.EdmType.IsOrInheritsFrom(edmType))
				{
					edmType = typeSegment.EdmType;
				}
			}
			return edmType;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000272B0 File Offset: 0x000254B0
		public static bool IsStructuredCollection(this IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(type, "type");
			IEdmCollectionTypeReference edmCollectionTypeReference = type as IEdmCollectionTypeReference;
			return edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsStructured();
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000272E0 File Offset: 0x000254E0
		public static IEdmStructuredTypeReference GetTypeReference(this IEdmStructuredType structuredType)
		{
			IEdmEntityType edmEntityType = structuredType as IEdmEntityType;
			IEdmStructuredTypeReference edmStructuredTypeReference;
			if (edmEntityType != null)
			{
				edmStructuredTypeReference = new EdmEntityTypeReference(edmEntityType, false);
			}
			else
			{
				edmStructuredTypeReference = new EdmComplexTypeReference(structuredType as IEdmComplexType, false);
			}
			return edmStructuredTypeReference;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002730F File Offset: 0x0002550F
		public static bool IsBindingTypeValid(IEdmType bindingType)
		{
			return bindingType == null || bindingType.IsEntityOrEntityCollectionType() || bindingType.IsODataComplexTypeKind();
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00027324 File Offset: 0x00025524
		internal static IEdmEnumType FindEnumTypeFromModel(IEdmModel model, string qualifiedName, ODataUriResolver resolver)
		{
			return UriEdmHelpers.FindTypeFromModel(model, qualifiedName, resolver ?? ODataUriResolver.GetUriResolver(null)) as IEdmEnumType;
		}
	}
}
