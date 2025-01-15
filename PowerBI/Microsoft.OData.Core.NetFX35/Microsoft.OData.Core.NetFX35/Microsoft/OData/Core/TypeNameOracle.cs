using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x0200000B RID: 11
	internal class TypeNameOracle
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002684 File Offset: 0x00000884
		internal static IEdmType ResolveAndValidateTypeName(IEdmModel model, string typeName, EdmTypeKind expectedTypeKind, IWriterValidator writerValidator)
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
				writerValidator.ValidateTypeKind(edmType.TypeKind, expectedTypeKind, edmType);
				return edmType;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026EC File Offset: 0x000008EC
		internal static IEdmTypeReference ResolveAndValidateTypeForPrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			return EdmLibraryExtensions.GetPrimitiveTypeReference(primitiveValue.Value.GetType());
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026FE File Offset: 0x000008FE
		internal static IEdmTypeReference ResolveAndValidateTypeForEnumValue(IEdmModel model, ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			TypeNameOracle.ValidateIfTypeNameMissing(enumValue.TypeName, model, isOpenPropertyType);
			return null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002710 File Offset: 0x00000910
		internal static IEdmTypeReference ResolveAndValidateTypeForComplexValue(IEdmModel model, IEdmTypeReference typeReferenceFromMetadata, ODataComplexValue complexValue, bool isOpenPropertyType, IWriterValidator writerValidator)
		{
			string typeName = complexValue.TypeName;
			TypeNameOracle.ValidateIfTypeNameMissing(typeName, model, isOpenPropertyType);
			IEdmType edmType = ((typeName == null) ? null : TypeNameOracle.ResolveAndValidateTypeName(model, typeName, EdmTypeKind.Complex, writerValidator));
			if (typeReferenceFromMetadata != null)
			{
				writerValidator.ValidateTypeKind(EdmTypeKind.Complex, typeReferenceFromMetadata.TypeKind(), edmType);
			}
			return TypeNameOracle.ResolveTypeFromMetadataAndValue(typeReferenceFromMetadata, (edmType == null) ? null : edmType.ToTypeReference(), writerValidator);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002768 File Offset: 0x00000968
		internal static IEdmTypeReference ResolveAndValidateTypeForCollectionValue(IEdmModel model, IEdmTypeReference typeReferenceFromMetadata, ODataCollectionValue collectionValue, bool isOpenPropertyType, IWriterValidator writerValidator)
		{
			string typeName = collectionValue.TypeName;
			TypeNameOracle.ValidateIfTypeNameMissing(typeName, model, isOpenPropertyType);
			IEdmType edmType = ((typeName == null) ? null : TypeNameOracle.ResolveAndValidateTypeName(model, typeName, EdmTypeKind.Collection, writerValidator));
			if (typeReferenceFromMetadata != null)
			{
				writerValidator.ValidateTypeKind(EdmTypeKind.Collection, typeReferenceFromMetadata.TypeKind(), edmType);
			}
			IEdmTypeReference edmTypeReference = TypeNameOracle.ResolveTypeFromMetadataAndValue(typeReferenceFromMetadata, (edmType == null) ? null : edmType.ToTypeReference(), writerValidator);
			if (edmTypeReference != null)
			{
				if (typeReferenceFromMetadata != null)
				{
					edmTypeReference = typeReferenceFromMetadata;
				}
				edmTypeReference = writerValidator.ValidateCollectionType(edmTypeReference);
			}
			return edmTypeReference;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000027D0 File Offset: 0x000009D0
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
				ODataComplexValue odataComplexValue = value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					return odataComplexValue.TypeName;
				}
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

		// Token: 0x06000035 RID: 53 RVA: 0x0000286D File Offset: 0x00000A6D
		private static void ValidateIfTypeNameMissing(string typeName, IEdmModel model, bool isOpenPropertyType)
		{
			if (typeName == null && model.IsUserModel() && isOpenPropertyType)
			{
				throw new ODataException(Strings.WriterValidationUtils_MissingTypeNameWithMetadata);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002888 File Offset: 0x00000A88
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
