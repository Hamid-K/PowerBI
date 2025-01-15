using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000B0 RID: 176
	internal class TypeNameOracle
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x00013574 File Offset: 0x00011774
		internal static IEdmType ResolveAndValidateTypeName(IEdmModel model, string typeName, EdmTypeKind expectedTypeKind, bool? expectStructuredType, IWriterValidator writerValidator)
		{
			if (typeName == null)
			{
				if (model.IsUserModel())
				{
					throw new ODataException(Strings.WriterValidationUtils_MissingTypeNameWithMetadata);
				}
				return null;
			}
			else
			{
				if (typeName.Length == 0)
				{
					throw new ODataException(Strings.ValidationUtils_TypeNameMustNotBeEmpty);
				}
				if (!model.IsUserModel())
				{
					return null;
				}
				IEdmType edmType = MetadataUtils.ResolveTypeNameForWrite(model, typeName);
				if (edmType == null)
				{
					throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(typeName));
				}
				if (edmType.TypeKind != EdmTypeKind.Untyped)
				{
					writerValidator.ValidateTypeKind(edmType.TypeKind, expectedTypeKind, expectStructuredType, edmType);
				}
				return edmType;
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x000135E8 File Offset: 0x000117E8
		internal static IEdmStructuredType ResolveAndValidateTypeFromTypeName(IEdmModel model, IEdmStructuredType expectedType, string typeName, IWriterValidator writerValidator)
		{
			if (typeName == null && expectedType != null)
			{
				return expectedType;
			}
			IEdmType edmType = TypeNameOracle.ResolveAndValidateTypeName(model, typeName, EdmTypeKind.None, new bool?(true), writerValidator);
			IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveTypeFromMetadataAndValue(expectedType.ToTypeReference(), (edmType == null) ? null : edmType.ToTypeReference(), writerValidator);
			if (edmTypeReference != null && edmTypeReference.IsUntyped())
			{
				return new EdmUntypedStructuredType();
			}
			if (edmTypeReference != null)
			{
				return edmTypeReference.ToStructuredType();
			}
			return null;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00013642 File Offset: 0x00011842
		internal static IEdmTypeReference ResolveAndValidateTypeForPrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			return EdmLibraryExtensions.GetPrimitiveTypeReference(primitiveValue.Value.GetType());
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00013654 File Offset: 0x00011854
		internal static IEdmTypeReference ResolveAndValidateTypeForEnumValue(IEdmModel model, ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			TypeNameOracle.ValidateIfTypeNameMissing(enumValue.TypeName, model, isOpenPropertyType);
			return null;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00013664 File Offset: 0x00011864
		internal static IEdmTypeReference ResolveAndValidateTypeForCollectionValue(IEdmModel model, IEdmTypeReference typeReferenceFromMetadata, ODataCollectionValue collectionValue, bool isOpenPropertyType, IWriterValidator writerValidator)
		{
			string typeName = collectionValue.TypeName;
			TypeNameOracle.ValidateIfTypeNameMissing(typeName, model, isOpenPropertyType);
			IEdmType edmType = ((typeName == null) ? null : TypeNameOracle.ResolveAndValidateTypeName(model, typeName, EdmTypeKind.Collection, new bool?(false), writerValidator));
			if (typeReferenceFromMetadata != null)
			{
				writerValidator.ValidateTypeKind(EdmTypeKind.Collection, typeReferenceFromMetadata.TypeKind(), new bool?(false), edmType);
			}
			IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveTypeFromMetadataAndValue(typeReferenceFromMetadata, (edmType == null) ? null : edmType.ToTypeReference(), writerValidator);
			if (edmTypeReference != null)
			{
				if (typeReferenceFromMetadata != null)
				{
					edmTypeReference = typeReferenceFromMetadata;
				}
				edmTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
			}
			return edmTypeReference;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000136D4 File Offset: 0x000118D4
		internal static bool TryGetTypeNameFromAnnotation(ODataValue value, out string propertyName)
		{
			if (value.TypeAnnotation != null)
			{
				propertyName = value.TypeAnnotation.TypeName;
				return true;
			}
			propertyName = null;
			return false;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000136F4 File Offset: 0x000118F4
		protected static string GetTypeNameFromValue(object value)
		{
			ODataPrimitiveValue odataPrimitiveValue = value as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(odataPrimitiveValue.Value.GetType());
				if (primitiveTypeReference != null)
				{
					return primitiveTypeReference.FullName();
				}
				return null;
			}
			else
			{
				ODataEnumValue odataEnumValue = value as ODataEnumValue;
				if (odataEnumValue != null)
				{
					return odataEnumValue.TypeName;
				}
				ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					return EdmLibraryExtensions.GetCollectionTypeFullName(odataCollectionValue.TypeName);
				}
				IEdmPrimitiveTypeReference primitiveTypeReference2 = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
				if (primitiveTypeReference2 == null)
				{
					throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
				}
				return primitiveTypeReference2.FullName();
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001377D File Offset: 0x0001197D
		private static void ValidateIfTypeNameMissing(string typeName, IEdmModel model, bool isOpenPropertyType)
		{
			if (typeName == null && model.IsUserModel() && isOpenPropertyType)
			{
				throw new ODataException(Strings.WriterValidationUtils_MissingTypeNameWithMetadata);
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001379A File Offset: 0x0001199A
		private static IEdmTypeReference ResolveTypeFromMetadataAndValue(IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, IWriterValidator writerValidator)
		{
			if (typeReferenceFromMetadata == null)
			{
				return typeReferenceFromValue;
			}
			if (typeReferenceFromValue == null)
			{
				return typeReferenceFromMetadata;
			}
			writerValidator.ValidateTypeReference(typeReferenceFromMetadata, typeReferenceFromValue);
			return typeReferenceFromValue;
		}
	}
}
