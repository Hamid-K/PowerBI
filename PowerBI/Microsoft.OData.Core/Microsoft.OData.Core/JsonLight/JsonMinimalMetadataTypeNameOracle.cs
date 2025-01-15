using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000234 RID: 564
	internal sealed class JsonMinimalMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x00046414 File Offset: 0x00044614
		internal override string GetResourceSetTypeNameForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared)
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

		// Token: 0x06001886 RID: 6278 RVA: 0x0004645C File Offset: 0x0004465C
		internal override string GetResourceTypeNameForWriting(string expectedTypeName, ODataResourceBase resource, bool isUndeclared = false)
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

		// Token: 0x06001887 RID: 6279 RVA: 0x00046494 File Offset: 0x00044694
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
				if (typeReferenceFromMetadata == null && typeReferenceFromValue.IsStructured() && (typeReferenceFromValue as IEdmStructuredTypeReference).StructuredDefinition().BaseType != null)
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

		// Token: 0x06001888 RID: 6280 RVA: 0x00046524 File Offset: 0x00044724
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
