using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001FB RID: 507
	internal sealed class JsonMinimalMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x060013B6 RID: 5046 RVA: 0x00038650 File Offset: 0x00036850
		internal override string GetResourceSetTypeNameForForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared)
		{
			if (resourceSet.TypeAnnotation != null)
			{
				return resourceSet.TypeAnnotation.TypeName;
			}
			string text = ((expectedResourceTypeName == null) ? null : EdmLibraryExtensions.GetCollectionTypeName(expectedResourceTypeName));
			if (text != resourceSet.TypeName || isUndeclared)
			{
				return resourceSet.TypeName;
			}
			return null;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00038698 File Offset: 0x00036898
		internal override string GetResourceTypeNameForWriting(string expectedTypeName, ODataResource resource, bool isUndeclared = false)
		{
			if (resource.TypeAnnotation != null)
			{
				return resource.TypeAnnotation.TypeName;
			}
			string typeName = resource.TypeName;
			if (expectedTypeName != typeName || isUndeclared)
			{
				return typeName;
			}
			return null;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x000386D0 File Offset: 0x000368D0
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			string text = null;
			string text2;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(value, out text2))
			{
				return text2;
			}
			if (typeReferenceFromValue != null)
			{
				text = typeReferenceFromValue.FullName();
				if (typeReferenceFromMetadata != null && typeReferenceFromMetadata.Definition.AsActualType().FullTypeName() != text)
				{
					return text;
				}
				if (typeReferenceFromValue.IsPrimitive() && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, typeReferenceFromValue.AsPrimitive()))
				{
					return null;
				}
				if (typeReferenceFromMetadata == null && typeReferenceFromValue.IsComplex() && (typeReferenceFromValue as IEdmComplexTypeReference).ComplexDefinition().BaseType != null)
				{
					return text;
				}
			}
			if (!isOpenProperty)
			{
				return null;
			}
			if (text == null)
			{
				return TypeNameOracle.GetTypeNameFromValue(value);
			}
			return text;
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00038760 File Offset: 0x00036960
		internal override string GetValueTypeNameForWriting(ODataValue value, PropertySerializationInfo propertyInfo, bool isOpenProperty)
		{
			string text = null;
			PropertyValueTypeInfo valueType = propertyInfo.ValueType;
			PropertyMetadataTypeInfo metadataType = propertyInfo.MetadataType;
			string text2;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(value, out text2))
			{
				return text2;
			}
			if (valueType.TypeReference != null)
			{
				text = valueType.FullName;
				if (metadataType.TypeReference != null && metadataType.FullName != text)
				{
					return text;
				}
				if (valueType.IsPrimitive && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, valueType.PrimitiveTypeKind))
				{
					return null;
				}
				if (metadataType.TypeReference == null && valueType.IsComplex && (valueType.TypeReference as IEdmComplexTypeReference).ComplexDefinition().BaseType != null)
				{
					return text;
				}
			}
			if (!isOpenProperty)
			{
				return null;
			}
			if (text == null)
			{
				return TypeNameOracle.GetTypeNameFromValue(value);
			}
			return text;
		}
	}
}
