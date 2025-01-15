using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Parsers;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Metadata
{
	// Token: 0x020001ED RID: 493
	internal static class UriEdmHelpers
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x00040F82 File Offset: 0x0003F182
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Uri Parser does not need to go through the ODL behavior knob.")]
		public static IEdmSchemaType FindTypeFromModel(IEdmModel model, string qualifiedName, ODataUriResolver resolver)
		{
			if (resolver == null)
			{
				resolver = ODataUriResolver.Default;
			}
			return resolver.ResolveType(model, qualifiedName);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00040F98 File Offset: 0x0003F198
		public static IEdmEnumType FindEnumTypeFromModel(IEdmModel model, string qualifiedName)
		{
			return UriEdmHelpers.FindTypeFromModel(model, qualifiedName, ODataUriResolver.Default) as IEdmEnumType;
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00040FB8 File Offset: 0x0003F1B8
		public static void CheckRelatedTo(IEdmType parentType, IEdmType childType)
		{
			if (!UriEdmHelpers.IsRelatedTo(parentType, childType))
			{
				string text = ((parentType != null) ? parentType.FullTypeName() : "<null>");
				throw new ODataException(Strings.MetadataBinder_HierarchyNotFollowed(childType.FullTypeName(), text));
			}
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00040FF1 File Offset: 0x0003F1F1
		public static bool IsRelatedTo(IEdmType first, IEdmType second)
		{
			return second.IsOrInheritsFrom(first) || first.IsOrInheritsFrom(second);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00041008 File Offset: 0x0003F208
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

		// Token: 0x060011F0 RID: 4592 RVA: 0x00041080 File Offset: 0x0003F280
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

		// Token: 0x060011F1 RID: 4593 RVA: 0x000410E4 File Offset: 0x0003F2E4
		public static bool IsEntityCollection(this IEdmTypeReference type)
		{
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(type, "type");
			IEdmCollectionTypeReference edmCollectionTypeReference = type as IEdmCollectionTypeReference;
			return edmCollectionTypeReference != null && edmCollectionTypeReference.ElementType().IsEntity();
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00041113 File Offset: 0x0003F313
		public static bool IsBindingTypeValid(IEdmType bindingType)
		{
			return bindingType == null || bindingType.IsEntityOrEntityCollectionType() || bindingType.IsODataComplexTypeKind();
		}
	}
}
