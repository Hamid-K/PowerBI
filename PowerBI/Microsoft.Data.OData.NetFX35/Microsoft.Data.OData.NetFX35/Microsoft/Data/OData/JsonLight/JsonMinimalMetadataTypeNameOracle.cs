using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000111 RID: 273
	internal sealed class JsonMinimalMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00018988 File Offset: 0x00016B88
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			string typeName = entry.TypeName;
			if (expectedTypeName != typeName)
			{
				return typeName;
			}
			return null;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x000189BC File Offset: 0x00016BBC
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			if (typeReferenceFromValue != null)
			{
				if (typeReferenceFromMetadata != null && typeReferenceFromMetadata.ODataFullName() != typeReferenceFromValue.ODataFullName())
				{
					return typeReferenceFromValue.ODataFullName();
				}
				if (typeReferenceFromValue.IsPrimitive() && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, typeReferenceFromValue.AsPrimitive()))
				{
					return null;
				}
			}
			if (!isOpenProperty)
			{
				return null;
			}
			return TypeNameOracle.GetTypeNameFromValue(value);
		}
	}
}
