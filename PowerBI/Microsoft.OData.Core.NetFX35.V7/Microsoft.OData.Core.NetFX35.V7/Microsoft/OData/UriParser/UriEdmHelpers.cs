using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001BC RID: 444
	internal static class UriEdmHelpers
	{
		// Token: 0x0600118C RID: 4492 RVA: 0x00030BB6 File Offset: 0x0002EDB6
		public static IEdmSchemaType FindTypeFromModel(IEdmModel model, string qualifiedName, ODataUriResolver resolver)
		{
			return resolver.ResolveType(model, qualifiedName);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00030BC0 File Offset: 0x0002EDC0
		public static IEdmEnumType FindEnumTypeFromModel(IEdmModel model, string qualifiedName)
		{
			return UriEdmHelpers.FindTypeFromModel(model, qualifiedName, ODataUriResolver.GetUriResolver(null)) as IEdmEnumType;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00030BE4 File Offset: 0x0002EDE4
		public static void CheckRelatedTo(IEdmType parentType, IEdmType childType)
		{
			if (!UriEdmHelpers.IsRelatedTo(parentType, childType))
			{
				string text = ((parentType != null) ? parentType.FullTypeName() : "<null>");
				throw new ODataException(Strings.MetadataBinder_HierarchyNotFollowed(childType.FullTypeName(), text));
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00030C1D File Offset: 0x0002EE1D
		public static bool IsRelatedTo(IEdmType first, IEdmType second)
		{
			return second.IsOrInheritsFrom(first) || first.IsOrInheritsFrom(second);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00030C34 File Offset: 0x0002EE34
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

		// Token: 0x06001191 RID: 4497 RVA: 0x00030CAC File Offset: 0x0002EEAC
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

		// Token: 0x06001192 RID: 4498 RVA: 0x00030D10 File Offset: 0x0002EF10
		public static bool IsStructuredCollection(this IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(type, "type");
			IEdmCollectionTypeReference edmCollectionTypeReference = type as IEdmCollectionTypeReference;
			return edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsStructured();
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00030D40 File Offset: 0x0002EF40
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

		// Token: 0x06001194 RID: 4500 RVA: 0x00030D6F File Offset: 0x0002EF6F
		public static bool IsBindingTypeValid(IEdmType bindingType)
		{
			return bindingType == null || bindingType.IsEntityOrEntityCollectionType() || bindingType.IsODataComplexTypeKind();
		}
	}
}
