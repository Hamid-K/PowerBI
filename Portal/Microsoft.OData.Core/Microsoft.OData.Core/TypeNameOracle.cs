using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x020000D1 RID: 209
	internal class TypeNameOracle
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x00018B20 File Offset: 0x00016D20
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

		// Token: 0x060009B5 RID: 2485 RVA: 0x00018B94 File Offset: 0x00016D94
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

		// Token: 0x060009B6 RID: 2486 RVA: 0x00018BEE File Offset: 0x00016DEE
		internal static IEdmTypeReference ResolveAndValidateTypeForPrimitiveValue(ODataPrimitiveValue primitiveValue)
		{
			return EdmLibraryExtensions.GetPrimitiveTypeReference(primitiveValue.Value.GetType());
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00018C00 File Offset: 0x00016E00
		internal static IEdmTypeReference ResolveAndValidateTypeForEnumValue(IEdmModel model, ODataEnumValue enumValue, bool isOpenPropertyType)
		{
			TypeNameOracle.ValidateIfTypeNameMissing(enumValue.TypeName, model, isOpenPropertyType);
			return null;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00018C10 File Offset: 0x00016E10
		internal static IEdmTypeReference ResolveAndValidateTypeForResourceValue(IEdmModel model, IEdmTypeReference typeReferenceFromMetadata, ODataResourceValue resourceValue, bool isOpenPropertyType, IWriterValidator writerValidator)
		{
			string typeName = resourceValue.TypeName;
			TypeNameOracle.ValidateIfTypeNameMissing(typeName, model, isOpenPropertyType);
			IEdmType edmType = ((typeName == null) ? null : TypeNameOracle.ResolveAndValidateTypeName(model, typeName, EdmTypeKind.Complex, new bool?(true), writerValidator));
			if (typeReferenceFromMetadata != null)
			{
				writerValidator.ValidateTypeKind(EdmTypeKind.Complex, typeReferenceFromMetadata.TypeKind(), new bool?(true), edmType);
			}
			return TypeNameOracle.ResolveTypeFromMetadataAndValue(typeReferenceFromMetadata, (edmType == null) ? null : edmType.ToTypeReference(), writerValidator);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00018C70 File Offset: 0x00016E70
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

		// Token: 0x060009BA RID: 2490 RVA: 0x00018CE0 File Offset: 0x00016EE0
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

		// Token: 0x060009BB RID: 2491 RVA: 0x00018D00 File Offset: 0x00016F00
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
				ODataResourceValue odataResourceValue = value as ODataResourceValue;
				if (odataResourceValue != null)
				{
					return odataResourceValue.TypeName;
				}
				ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
				if (odataCollectionValue != null)
				{
					return EdmLibraryExtensions.GetCollectionTypeFullName(odataCollectionValue.TypeName);
				}
				ODataBinaryStreamValue odataBinaryStreamValue = value as ODataBinaryStreamValue;
				if (odataBinaryStreamValue != null)
				{
					return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Binary, true).FullName();
				}
				ODataStreamReferenceValue odataStreamReferenceValue = value as ODataStreamReferenceValue;
				if (odataStreamReferenceValue != null)
				{
					return EdmCoreModel.Instance.GetPrimitive(EdmPrimitiveTypeKind.Stream, true).FullName();
				}
				IEdmPrimitiveTypeReference primitiveTypeReference2 = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
				if (primitiveTypeReference2 == null)
				{
					throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
				}
				return primitiveTypeReference2.FullName();
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00018DDA File Offset: 0x00016FDA
		private static void ValidateIfTypeNameMissing(string typeName, IEdmModel model, bool isOpenPropertyType)
		{
			if (typeName == null && model.IsUserModel() && isOpenPropertyType)
			{
				throw new ODataException(Strings.WriterValidationUtils_MissingTypeNameWithMetadata);
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00018DF7 File Offset: 0x00016FF7
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
