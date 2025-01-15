using System;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000AF RID: 175
	internal sealed class JsonFullMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x000162FC File Offset: 0x000144FC
		internal override string GetEntryTypeNameForWriting(string expectedTypeName, ODataEntry entry)
		{
			SerializationTypeNameAnnotation annotation = entry.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			return entry.TypeName;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00016320 File Offset: 0x00014520
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			if (typeReferenceFromValue != null && typeReferenceFromValue.IsPrimitive() && JsonSharedUtils.ValueTypeMatchesJsonType((ODataPrimitiveValue)value, typeReferenceFromValue.AsPrimitive()))
			{
				return null;
			}
			return TypeNameOracle.GetTypeNameFromValue(value);
		}
	}
}
