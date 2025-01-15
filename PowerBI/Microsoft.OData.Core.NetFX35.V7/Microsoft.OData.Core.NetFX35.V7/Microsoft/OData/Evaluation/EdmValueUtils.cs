using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.Spatial;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000221 RID: 545
	internal static class EdmValueUtils
	{
		// Token: 0x0600161B RID: 5659 RVA: 0x00044214 File Offset: 0x00042414
		internal static IEdmDelayedValue ConvertPrimitiveValue(object primitiveValue, IEdmPrimitiveTypeReference type)
		{
			if (primitiveValue is bool)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Boolean);
				return new EdmBooleanConstant(type, (bool)primitiveValue);
			}
			if (primitiveValue is byte)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Byte);
				return new EdmIntegerConstant(type, (long)((ulong)((byte)primitiveValue)));
			}
			if (primitiveValue is sbyte)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.SByte);
				return new EdmIntegerConstant(type, (long)((sbyte)primitiveValue));
			}
			if (primitiveValue is short)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int16);
				return new EdmIntegerConstant(type, (long)((short)primitiveValue));
			}
			if (primitiveValue is int)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int32);
				return new EdmIntegerConstant(type, (long)((int)primitiveValue));
			}
			if (primitiveValue is long)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int64);
				return new EdmIntegerConstant(type, (long)primitiveValue);
			}
			if (primitiveValue is decimal)
			{
				IEdmDecimalTypeReference edmDecimalTypeReference = (IEdmDecimalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Decimal);
				return new EdmDecimalConstant(edmDecimalTypeReference, (decimal)primitiveValue);
			}
			if (primitiveValue is float)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Single);
				return new EdmFloatingConstant(type, (double)((float)primitiveValue));
			}
			if (primitiveValue is double)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Double);
				return new EdmFloatingConstant(type, (double)primitiveValue);
			}
			string text = primitiveValue as string;
			if (text != null)
			{
				IEdmStringTypeReference edmStringTypeReference = (IEdmStringTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.String);
				return new EdmStringConstant(edmStringTypeReference, text);
			}
			return EdmValueUtils.ConvertPrimitiveValueWithoutTypeCode(primitiveValue, type);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00044364 File Offset: 0x00042564
		internal static object ToClrValue(this IEdmPrimitiveValue edmValue)
		{
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmValue.Type.PrimitiveKind();
			switch (edmValue.ValueKind)
			{
			case EdmValueKind.Binary:
				return ((IEdmBinaryValue)edmValue).Value;
			case EdmValueKind.Boolean:
				return ((IEdmBooleanValue)edmValue).Value;
			case EdmValueKind.DateTimeOffset:
				return ((IEdmDateTimeOffsetValue)edmValue).Value;
			case EdmValueKind.Decimal:
				return ((IEdmDecimalValue)edmValue).Value;
			case EdmValueKind.Floating:
				return EdmValueUtils.ConvertFloatingValue((IEdmFloatingValue)edmValue, edmPrimitiveTypeKind);
			case EdmValueKind.Guid:
				return ((IEdmGuidValue)edmValue).Value;
			case EdmValueKind.Integer:
				return EdmValueUtils.ConvertIntegerValue((IEdmIntegerValue)edmValue, edmPrimitiveTypeKind);
			case EdmValueKind.String:
				return ((IEdmStringValue)edmValue).Value;
			case EdmValueKind.Duration:
				return ((IEdmDurationValue)edmValue).Value;
			case EdmValueKind.Date:
				return ((IEdmDateValue)edmValue).Value;
			case EdmValueKind.TimeOfDay:
				return ((IEdmTimeOfDayValue)edmValue).Value;
			}
			throw new ODataException(Strings.EdmValueUtils_CannotConvertTypeToClrValue(edmValue.ValueKind));
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0004448B File Offset: 0x0004268B
		internal static bool TryGetStreamProperty(IEdmStructuredValue entityInstance, string streamPropertyName, out IEdmProperty streamProperty)
		{
			streamProperty = null;
			if (streamPropertyName != null)
			{
				streamProperty = entityInstance.Type.AsEntity().FindProperty(streamPropertyName);
				if (streamProperty == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000444B0 File Offset: 0x000426B0
		internal static object GetPrimitivePropertyClrValue(this IEdmStructuredValue structuredValue, string propertyName)
		{
			IEdmStructuredTypeReference edmStructuredTypeReference = structuredValue.Type.AsStructured();
			IEdmPropertyValue edmPropertyValue = structuredValue.FindPropertyValue(propertyName);
			if (edmPropertyValue == null)
			{
				throw new ODataException(Strings.EdmValueUtils_PropertyDoesntExist(edmStructuredTypeReference.FullName(), propertyName));
			}
			if (edmPropertyValue.Value.ValueKind == EdmValueKind.Null)
			{
				return null;
			}
			IEdmPrimitiveValue edmPrimitiveValue = edmPropertyValue.Value as IEdmPrimitiveValue;
			if (edmPrimitiveValue == null)
			{
				throw new ODataException(Strings.EdmValueUtils_NonPrimitiveValue(edmPropertyValue.Name, edmStructuredTypeReference.FullName()));
			}
			return edmPrimitiveValue.ToClrValue();
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x00044524 File Offset: 0x00042724
		private static object ConvertFloatingValue(IEdmFloatingValue floatingValue, EdmPrimitiveTypeKind primitiveKind)
		{
			double value = floatingValue.Value;
			if (primitiveKind == EdmPrimitiveTypeKind.Single)
			{
				return Convert.ToSingle(value);
			}
			return value;
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00044550 File Offset: 0x00042750
		private static object ConvertIntegerValue(IEdmIntegerValue integerValue, EdmPrimitiveTypeKind primitiveKind)
		{
			long value = integerValue.Value;
			if (primitiveKind != EdmPrimitiveTypeKind.Byte)
			{
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Int16:
					return Convert.ToInt16(value);
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32(value);
				case EdmPrimitiveTypeKind.SByte:
					return Convert.ToSByte(value);
				}
				return value;
			}
			return Convert.ToByte(value);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x000445B8 File Offset: 0x000427B8
		private static IEdmDelayedValue ConvertPrimitiveValueWithoutTypeCode(object primitiveValue, IEdmPrimitiveTypeReference type)
		{
			byte[] array = primitiveValue as byte[];
			if (array != null)
			{
				IEdmBinaryTypeReference edmBinaryTypeReference = (IEdmBinaryTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Binary);
				return new EdmBinaryConstant(edmBinaryTypeReference, array);
			}
			if (primitiveValue is Date)
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Date);
				return new EdmDateConstant(edmPrimitiveTypeReference, (Date)primitiveValue);
			}
			if (primitiveValue is DateTimeOffset)
			{
				IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.DateTimeOffset);
				return new EdmDateTimeOffsetConstant(edmTemporalTypeReference, (DateTimeOffset)primitiveValue);
			}
			if (primitiveValue is Guid)
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Guid);
				return new EdmGuidConstant(type, (Guid)primitiveValue);
			}
			if (primitiveValue is TimeOfDay)
			{
				IEdmTemporalTypeReference edmTemporalTypeReference2 = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.TimeOfDay);
				return new EdmTimeOfDayConstant(edmTemporalTypeReference2, (TimeOfDay)primitiveValue);
			}
			if (primitiveValue is TimeSpan)
			{
				IEdmTemporalTypeReference edmTemporalTypeReference3 = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Duration);
				return new EdmDurationConstant(edmTemporalTypeReference3, (TimeSpan)primitiveValue);
			}
			if (primitiveValue is ISpatial)
			{
				throw new NotImplementedException();
			}
			throw new ODataException(Strings.EdmValueUtils_UnsupportedPrimitiveType(primitiveValue.GetType().FullName));
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x000446B0 File Offset: 0x000428B0
		private static IEdmPrimitiveTypeReference EnsurePrimitiveType(IEdmPrimitiveTypeReference type, EdmPrimitiveTypeKind primitiveKindFromValue)
		{
			if (type == null)
			{
				type = EdmCoreModel.Instance.GetPrimitive(primitiveKindFromValue, true);
			}
			else
			{
				EdmPrimitiveTypeKind primitiveKind = type.PrimitiveDefinition().PrimitiveKind;
				if (primitiveKind != primitiveKindFromValue)
				{
					string text = type.FullName();
					if (text == null)
					{
						throw new ODataException(Strings.EdmValueUtils_IncorrectPrimitiveTypeKindNoTypeName(primitiveKind.ToString(), primitiveKindFromValue.ToString()));
					}
					throw new ODataException(Strings.EdmValueUtils_IncorrectPrimitiveTypeKind(text, primitiveKindFromValue.ToString(), primitiveKind.ToString()));
				}
			}
			return type;
		}
	}
}
