using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Vocabularies;
using Microsoft.Spatial;

namespace Microsoft.OData.Client
{
	// Token: 0x02000009 RID: 9
	internal static class EdmValueUtils
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002F10 File Offset: 0x00001110
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

		// Token: 0x0600002E RID: 46 RVA: 0x00003060 File Offset: 0x00001260
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

		// Token: 0x0600002F RID: 47 RVA: 0x00003188 File Offset: 0x00001388
		private static object ConvertFloatingValue(IEdmFloatingValue floatingValue, EdmPrimitiveTypeKind primitiveKind)
		{
			double value = floatingValue.Value;
			if (primitiveKind == EdmPrimitiveTypeKind.Single)
			{
				return Convert.ToSingle(value);
			}
			return value;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000031B4 File Offset: 0x000013B4
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

		// Token: 0x06000031 RID: 49 RVA: 0x0000321C File Offset: 0x0000141C
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
			IEdmDelayedValue edmDelayedValue;
			if (EdmValueUtils.TryConvertClientSpecificPrimitiveValue(primitiveValue, type, out edmDelayedValue))
			{
				return edmDelayedValue;
			}
			throw new ODataException(Strings.EdmValueUtils_UnsupportedPrimitiveType(primitiveValue.GetType().FullName));
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003324 File Offset: 0x00001524
		private static bool TryConvertClientSpecificPrimitiveValue(object primitiveValue, IEdmPrimitiveTypeReference type, out IEdmDelayedValue convertedValue)
		{
			byte[] array;
			if (ClientConvert.TryConvertBinaryToByteArray(primitiveValue, out array))
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Binary);
				convertedValue = new EdmBinaryConstant((IEdmBinaryTypeReference)type, array);
				return true;
			}
			PrimitiveType primitiveType;
			if (PrimitiveType.TryGetPrimitiveType(primitiveValue.GetType(), out primitiveType))
			{
				type = EdmValueUtils.EnsurePrimitiveType(type, primitiveType.PrimitiveKind);
				if (primitiveType.PrimitiveKind == EdmPrimitiveTypeKind.String)
				{
					convertedValue = new EdmStringConstant((IEdmStringTypeReference)type, primitiveType.TypeConverter.ToString(primitiveValue));
					return true;
				}
			}
			convertedValue = null;
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000339C File Offset: 0x0000159C
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
