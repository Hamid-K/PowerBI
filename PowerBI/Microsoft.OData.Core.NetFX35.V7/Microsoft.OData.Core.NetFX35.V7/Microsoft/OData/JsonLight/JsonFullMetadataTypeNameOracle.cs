using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x020001F5 RID: 501
	internal sealed class JsonFullMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06001399 RID: 5017 RVA: 0x000383BC File Offset: 0x000365BC
		internal override string GetResourceSetTypeNameForForWriting(string expectedResourceTypeName, ODataResourceSet resourceSet, bool isUndeclared)
		{
			if (resourceSet.TypeAnnotation != null)
			{
				return resourceSet.TypeAnnotation.TypeName;
			}
			if (isUndeclared)
			{
				return resourceSet.TypeName;
			}
			return null;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000383DD File Offset: 0x000365DD
		internal override string GetResourceTypeNameForWriting(string expectedTypeName, ODataResource resource, bool isUndeclared = false)
		{
			if (resource.TypeAnnotation != null)
			{
				return resource.TypeAnnotation.TypeName;
			}
			return resource.TypeName;
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x000383FC File Offset: 0x000365FC
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

		// Token: 0x0600139C RID: 5020 RVA: 0x0003843C File Offset: 0x0003663C
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
