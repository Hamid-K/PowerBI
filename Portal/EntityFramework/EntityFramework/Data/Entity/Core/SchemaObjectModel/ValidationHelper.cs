using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000326 RID: 806
	internal static class ValidationHelper
	{
		// Token: 0x06002665 RID: 9829 RVA: 0x0006E968 File Offset: 0x0006CB68
		internal static void ValidateFacets(SchemaElement element, SchemaType type, TypeUsageBuilder typeUsageBuilder)
		{
			if (type != null)
			{
				SchemaEnumType schemaEnumType = type as SchemaEnumType;
				if (schemaEnumType != null)
				{
					typeUsageBuilder.ValidateEnumFacets(schemaEnumType);
					return;
				}
				if (!(type is ScalarType) && typeUsageBuilder.HasUserDefinedFacets)
				{
					element.AddError(ErrorCode.FacetOnNonScalarType, EdmSchemaErrorSeverity.Error, Strings.FacetsOnNonScalarType(type.FQName));
					return;
				}
			}
			else if (typeUsageBuilder.HasUserDefinedFacets)
			{
				element.AddError(ErrorCode.IncorrectlyPlacedFacet, EdmSchemaErrorSeverity.Error, Strings.FacetDeclarationRequiresTypeAttribute);
			}
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0006E9CB File Offset: 0x0006CBCB
		internal static void ValidateTypeDeclaration(SchemaElement element, SchemaType type, SchemaElement typeSubElement)
		{
			if (type == null && typeSubElement == null)
			{
				element.AddError(ErrorCode.TypeNotDeclared, EdmSchemaErrorSeverity.Error, Strings.TypeMustBeDeclared);
			}
			if (type != null && typeSubElement != null)
			{
				element.AddError(ErrorCode.TypeDeclaredAsAttributeAndElement, EdmSchemaErrorSeverity.Error, Strings.TypeDeclaredAsAttributeAndElement);
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0006E9FB File Offset: 0x0006CBFB
		internal static void ValidateRefType(SchemaElement element, SchemaType type)
		{
			if (type != null && !(type is SchemaEntityType))
			{
				element.AddError(ErrorCode.ReferenceToNonEntityType, EdmSchemaErrorSeverity.Error, Strings.ReferenceToNonEntityType(type.FQName));
			}
		}
	}
}
