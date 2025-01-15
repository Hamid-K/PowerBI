using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200022E RID: 558
	internal sealed class JsonFullMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x00046178 File Offset: 0x00044378
		internal override string GetResourceSetTypeNameForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared)
		{
			if (resourceSet.TypeAnnotation != null)
			{
				return resourceSet.TypeAnnotation.TypeName;
			}
			return resourceSet.TypeName;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00046194 File Offset: 0x00044394
		internal override string GetResourceTypeNameForWriting(string expectedTypeName, ODataResourceBase resource, bool isUndeclared = false)
		{
			if (resource.TypeAnnotation != null)
			{
				return resource.TypeAnnotation.TypeName;
			}
			return resource.TypeName;
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x000461B0 File Offset: 0x000443B0
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			string text;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(value, out text))
			{
				return text;
			}
			if (typeReferenceFromValue != null && typeReferenceFromValue.IsPrimitive() && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, typeReferenceFromValue.AsPrimitive()))
			{
				return null;
			}
			return TypeNameOracle.GetTypeNameFromValue(value);
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x000461F0 File Offset: 0x000443F0
		internal override string GetValueTypeNameForWriting(ODataValue value, PropertySerializationInfo propertyInfo, bool isOpenProperty)
		{
			PropertyValueTypeInfo valueType = propertyInfo.ValueType;
			string text;
			if (TypeNameOracle.TryGetTypeNameFromAnnotation(value, out text))
			{
				return text;
			}
			if (valueType.TypeReference != null && valueType.IsPrimitive && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, valueType.PrimitiveTypeKind))
			{
				return null;
			}
			return TypeNameOracle.GetTypeNameFromValue(value);
		}
	}
}
