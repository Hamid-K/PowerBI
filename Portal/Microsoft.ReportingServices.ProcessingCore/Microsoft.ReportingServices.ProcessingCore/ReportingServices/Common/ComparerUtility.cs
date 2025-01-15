using System;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x020005CA RID: 1482
	internal static class ComparerUtility
	{
		// Token: 0x0600538B RID: 21387 RVA: 0x00160450 File Offset: 0x0015E650
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

		// Token: 0x0600538C RID: 21388 RVA: 0x001604A8 File Offset: 0x0015E6A8
		internal static DataTypeCode GetCommonVariantConversionType(DataTypeCode x, DataTypeCode y)
		{
			if ((y == DataTypeCode.Double && ComparerUtility.IsComparableToReal(x)) || (x == DataTypeCode.Double && ComparerUtility.IsComparableToReal(y)))
			{
				return DataTypeCode.Double;
			}
			if ((y == DataTypeCode.Decimal && ComparerUtility.IsComparableToCurrency(x)) || (x == DataTypeCode.Decimal && ComparerUtility.IsComparableToCurrency(y)))
			{
				return DataTypeCode.Decimal;
			}
			if ((y == DataTypeCode.DateTime && ComparerUtility.IsNumericVariant(x)) || (x == DataTypeCode.DateTime && ComparerUtility.IsNumericVariant(y)))
			{
				return DataTypeCode.Double;
			}
			if ((y == DataTypeCode.Int64 && x == DataTypeCode.Int32) || (x == DataTypeCode.Int64 && y == DataTypeCode.Int32))
			{
				return DataTypeCode.Int64;
			}
			return DataTypeCode.Unknown;
		}

		// Token: 0x0600538D RID: 21389 RVA: 0x0016051F File Offset: 0x0015E71F
		private static bool IsComparableToReal(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode - DataTypeCode.Decimal <= 1;
		}

		// Token: 0x0600538E RID: 21390 RVA: 0x00160531 File Offset: 0x0015E731
		private static bool IsComparableToCurrency(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode == DataTypeCode.Decimal;
		}

		// Token: 0x0600538F RID: 21391 RVA: 0x00160541 File Offset: 0x0015E741
		internal static bool IsNumericVariant(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode - DataTypeCode.Double <= 1;
		}

		// Token: 0x06005390 RID: 21392 RVA: 0x00160553 File Offset: 0x0015E753
		internal static bool IsNumericDateVariant(DataTypeCode typeCode)
		{
			return typeCode == DataTypeCode.Empty || typeCode - DataTypeCode.Int32 <= 1 || typeCode - DataTypeCode.Double <= 2;
		}

		// Token: 0x06005391 RID: 21393 RVA: 0x00160568 File Offset: 0x0015E768
		internal static bool IsNonNumericVariant(DataTypeCode typeCode)
		{
			return typeCode == DataTypeCode.Boolean || typeCode == DataTypeCode.String;
		}

		// Token: 0x06005392 RID: 21394 RVA: 0x00160578 File Offset: 0x0015E778
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

		// Token: 0x06005393 RID: 21395 RVA: 0x00160661 File Offset: 0x0015E861
		internal static bool IsLessThanReal(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1 || typeCode == DataTypeCode.Decimal;
		}

		// Token: 0x06005394 RID: 21396 RVA: 0x00160671 File Offset: 0x0015E871
		internal static bool IsLessThanCurrency(DataTypeCode typeCode)
		{
			return typeCode - DataTypeCode.Int32 <= 1;
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x0016067C File Offset: 0x0015E87C
		internal static bool IsLessThanInt64(DataTypeCode typeCode)
		{
			return typeCode == DataTypeCode.Int32;
		}
	}
}
