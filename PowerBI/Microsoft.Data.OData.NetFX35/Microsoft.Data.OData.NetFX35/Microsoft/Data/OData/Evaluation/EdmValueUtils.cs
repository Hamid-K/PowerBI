using System;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Values;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000163 RID: 355
	internal static class EdmValueUtils
	{
		// Token: 0x06000972 RID: 2418 RVA: 0x0001DFE0 File Offset: 0x0001C1E0
		internal static IEdmDelayedValue ConvertPrimitiveValue(object primitiveValue, IEdmPrimitiveTypeReference type)
		{
			switch (PlatformHelper.GetTypeCode(primitiveValue.GetType()))
			{
			case 3:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Boolean);
				return new EdmBooleanConstant(type, (bool)primitiveValue);
			case 5:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.SByte);
				return new EdmIntegerConstant(type, (long)((sbyte)primitiveValue));
			case 6:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Byte);
				return new EdmIntegerConstant(type, (long)((ulong)((byte)primitiveValue)));
			case 7:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int16);
				return new EdmIntegerConstant(type, (long)((short)primitiveValue));
			case 9:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int32);
				return new EdmIntegerConstant(type, (long)((int)primitiveValue));
			case 11:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Int64);
				return new EdmIntegerConstant(type, (long)primitiveValue);
			case 13:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Single);
				return new EdmFloatingConstant(type, (double)((float)primitiveValue));
			case 14:
				type = EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Double);
				return new EdmFloatingConstant(type, (double)primitiveValue);
			case 15:
			{
				IEdmDecimalTypeReference edmDecimalTypeReference = (IEdmDecimalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Decimal);
				return new EdmDecimalConstant(edmDecimalTypeReference, (decimal)primitiveValue);
			}
			case 16:
			{
				IEdmTemporalTypeReference edmTemporalTypeReference = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.DateTime);
				return new EdmDateTimeConstant(edmTemporalTypeReference, (DateTime)primitiveValue);
			}
			case 18:
			{
				IEdmStringTypeReference edmStringTypeReference = (IEdmStringTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.String);
				return new EdmStringConstant(edmStringTypeReference, (string)primitiveValue);
			}
			}
			return EdmValueUtils.ConvertPrimitiveValueWithoutTypeCode(primitiveValue, type);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0001E15C File Offset: 0x0001C35C
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
			case EdmValueKind.DateTime:
				return ((IEdmDateTimeValue)edmValue).Value;
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
			case EdmValueKind.Time:
				return ((IEdmTimeValue)edmValue).Value;
			}
			throw new ODataException(Strings.EdmValueUtils_CannotConvertTypeToClrValue(edmValue.ValueKind));
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0001E26C File Offset: 0x0001C46C
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

		// Token: 0x06000975 RID: 2421 RVA: 0x0001E290 File Offset: 0x0001C490
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

		// Token: 0x06000976 RID: 2422 RVA: 0x0001E304 File Offset: 0x0001C504
		private static object ConvertFloatingValue(IEdmFloatingValue floatingValue, EdmPrimitiveTypeKind primitiveKind)
		{
			double value = floatingValue.Value;
			if (primitiveKind == EdmPrimitiveTypeKind.Single)
			{
				return Convert.ToSingle(value);
			}
			return value;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0001E330 File Offset: 0x0001C530
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

		// Token: 0x06000978 RID: 2424 RVA: 0x0001E39C File Offset: 0x0001C59C
		private static IEdmDelayedValue ConvertPrimitiveValueWithoutTypeCode(object primitiveValue, IEdmPrimitiveTypeReference type)
		{
			byte[] array = primitiveValue as byte[];
			if (array != null)
			{
				IEdmBinaryTypeReference edmBinaryTypeReference = (IEdmBinaryTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Binary);
				return new EdmBinaryConstant(edmBinaryTypeReference, array);
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
			if (primitiveValue is TimeSpan)
			{
				IEdmTemporalTypeReference edmTemporalTypeReference2 = (IEdmTemporalTypeReference)EdmValueUtils.EnsurePrimitiveType(type, EdmPrimitiveTypeKind.Time);
				return new EdmTimeConstant(edmTemporalTypeReference2, (TimeSpan)primitiveValue);
			}
			if (primitiveValue is ISpatial)
			{
				throw new NotImplementedException();
			}
			throw new ODataException(Strings.EdmValueUtils_UnsupportedPrimitiveType(primitiveValue.GetType().FullName));
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001E450 File Offset: 0x0001C650
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
