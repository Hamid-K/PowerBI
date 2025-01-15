using System;
using Microsoft.OData.Core.Json;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000B3 RID: 179
	internal sealed class JsonMinimalMetadataTypeNameOracle : JsonLightTypeNameOracle
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x00016448 File Offset: 0x00014648
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

		// Token: 0x0600066E RID: 1646 RVA: 0x0001647C File Offset: 0x0001467C
		internal override string GetValueTypeNameForWriting(ODataValue value, IEdmTypeReference typeReferenceFromMetadata, IEdmTypeReference typeReferenceFromValue, bool isOpenProperty)
		{
			SerializationTypeNameAnnotation annotation = value.GetAnnotation<SerializationTypeNameAnnotation>();
			if (annotation != null)
			{
				return annotation.TypeName;
			}
			if (typeReferenceFromValue != null)
			{
				if (typeReferenceFromMetadata != null && typeReferenceFromMetadata.Definition.AsActualType().FullTypeName() != typeReferenceFromValue.FullName())
				{
					return typeReferenceFromValue.FullName();
				}
				if (typeReferenceFromMetadata == null && typeReferenceFromValue.IsComplex() && (typeReferenceFromValue as IEdmComplexTypeReference).ComplexDefinition().BaseType != null)
				{
					return typeReferenceFromValue.FullName();
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
