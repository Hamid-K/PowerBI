using System;

namespace Microsoft.DataShaping.Common.DaxComparer
{
	// Token: 0x02000026 RID: 38
	internal static class DaxComparerUtils
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00004B77 File Offset: 0x00002D77
		internal static DataTypeCode GetDataTypeCode(object value)
		{
			if (value == null)
			{
				return DataTypeCode.Empty;
			}
			return DaxComparerUtils.GetDataTypeCode(value, Type.GetTypeCode(value.GetType()));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004B90 File Offset: 0x00002D90
		internal static DataTypeCode GetDataTypeCode(object value, TypeCode typeCode)
		{
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				return DataTypeCode.Empty;
			case TypeCode.Object:
				if (value is TimeSpan)
				{
					return DataTypeCode.TimeSpan;
				}
				if (value is DateTimeOffset)
				{
					return DataTypeCode.DateTimeOffset;
				}
				if (value is byte[])
				{
					return DataTypeCode.ByteArray;
				}
				break;
			case TypeCode.Boolean:
				return DataTypeCode.Boolean;
			case TypeCode.Char:
				return DataTypeCode.Char;
			case TypeCode.SByte:
				return DataTypeCode.SByte;
			case TypeCode.Byte:
				return DataTypeCode.Byte;
			case TypeCode.Int16:
				return DataTypeCode.Int16;
			case TypeCode.UInt16:
				return DataTypeCode.UInt16;
			case TypeCode.Int32:
				return DataTypeCode.Int32;
			case TypeCode.UInt32:
				return DataTypeCode.UInt32;
			case TypeCode.Int64:
				return DataTypeCode.Int64;
			case TypeCode.UInt64:
				return DataTypeCode.UInt64;
			case TypeCode.Single:
				return DataTypeCode.Single;
			case TypeCode.Double:
				return DataTypeCode.Double;
			case TypeCode.Decimal:
				return DataTypeCode.Decimal;
			case TypeCode.DateTime:
				return DataTypeCode.DateTime;
			case TypeCode.String:
				return DataTypeCode.String;
			}
			return DataTypeCode.Unknown;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004C3C File Offset: 0x00002E3C
		internal static Type GetNumericDateTypeFromDataTypeCode(DataTypeCode typeCode)
		{
			switch (typeCode)
			{
			case DataTypeCode.Int64:
				return typeof(long);
			case DataTypeCode.Double:
				return typeof(double);
			case DataTypeCode.Decimal:
				return typeof(decimal);
			case DataTypeCode.DateTime:
				return typeof(DateTime);
			}
			return null;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004C94 File Offset: 0x00002E94
		internal static DataTypeCode GetCommonVariantConversionType(DataTypeCode x, DataTypeCode y)
		{
			if ((y == DataTypeCode.Double && DaxComparerUtils.IsComparableToReal(x)) || (x == DataTypeCode.Double && DaxComparerUtils.IsComparableToReal(y)))
			{
				return DataTypeCode.Double;
			}
			if ((y == DataTypeCode.Decimal && DaxComparerUtils.IsComparableToCurrency(x)) || (x == DataTypeCode.Decimal && DaxComparerUtils.IsComparableToCurrency(y)))
			{
				return DataTypeCode.Decimal;
			}
			if ((y == DataTypeCode.DateTime && DaxComparerUtils.IsNumericVariant(x)) || (x == DataTypeCode.DateTime && DaxComparerUtils.IsNumericVariant(y)))
			{
				return DataTypeCode.Double;
			}
			if ((y == DataTypeCode.Int64 && x == DataTypeCode.Int32) || (x == DataTypeCode.Int64 && y == DataTypeCode.Int32))
			{
				return DataTypeCode.Int64;
			}
			return DataTypeCode.Unknown;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004D0B File Offset: 0x00002F0B
		private static bool IsComparableToReal(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode - DataTypeCode.Decimal <= 1;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004D1D File Offset: 0x00002F1D
		private static bool IsComparableToCurrency(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode == DataTypeCode.Decimal;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004D2D File Offset: 0x00002F2D
		internal static bool IsNumericVariant(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode - DataTypeCode.Double <= 1;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004D3F File Offset: 0x00002F3F
		internal static bool IsNumericDateVariant(DataTypeCode typeCode)
		{
			return typeCode == DataTypeCode.Empty || typeCode - DataTypeCode.Int32 <= 1 || typeCode - DataTypeCode.Double <= 2;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004D54 File Offset: 0x00002F54
		internal static bool IsNonNumericVariant(DataTypeCode typeCode)
		{
			return typeCode == DataTypeCode.Boolean || typeCode == DataTypeCode.String;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004D64 File Offset: 0x00002F64
		internal static bool IsNumericLessThanZero(object value)
		{
			if (value is int)
			{
				return (int)value < 0;
			}
			if (value is double)
			{
				return (double)value < 0.0;
			}
			if (value is float)
			{
				return (float)value < 0f;
			}
			if (value is decimal)
			{
				return (decimal)value < 0m;
			}
			if (value is short)
			{
				return (short)value < 0;
			}
			if (value is long)
			{
				return (long)value < 0L;
			}
			if (value is ushort)
			{
				return (ushort)value < 0;
			}
			if (value is uint)
			{
				return (uint)value < 0U;
			}
			if (value is ulong)
			{
				return (ulong)value < 0UL;
			}
			if (value is byte)
			{
				return (byte)value < 0;
			}
			return value is sbyte && (sbyte)value < 0;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004E4D File Offset: 0x0000304D
		internal static bool IsLessThanReal(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode == DataTypeCode.Decimal;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004E5D File Offset: 0x0000305D
		internal static bool IsLessThanCurrency(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004E68 File Offset: 0x00003068
		internal static bool IsLessThanInt64(DataTypeCode typeCode)
		{
			return typeCode == DataTypeCode.Int32;
		}
	}
}
