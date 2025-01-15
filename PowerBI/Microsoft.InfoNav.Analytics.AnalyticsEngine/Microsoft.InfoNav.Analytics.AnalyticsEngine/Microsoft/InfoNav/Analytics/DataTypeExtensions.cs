using System;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200000E RID: 14
	internal static class DataTypeExtensions
	{
		// Token: 0x0600001B RID: 27 RVA: 0x0000247F File Offset: 0x0000067F
		internal static bool IsSupportedXAxisType(this DataType t)
		{
			return t == DataType.Int64 || t == DataType.Decimal || t == DataType.Double || t == DataType.DateTime;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002494 File Offset: 0x00000694
		internal static DateTime AddUnits(this DateTime dateTime, ForecastParameterUnit unit, int amount)
		{
			switch (unit)
			{
			case ForecastParameterUnit.Year:
				return dateTime.AddYears(amount);
			case ForecastParameterUnit.Quarter:
				return dateTime.AddMonths(amount * 3);
			case ForecastParameterUnit.Month:
				return dateTime.AddMonths(amount);
			case ForecastParameterUnit.Day:
				return dateTime.AddDays((double)amount);
			case ForecastParameterUnit.Hour:
				return dateTime.AddHours((double)amount);
			case ForecastParameterUnit.Minute:
				return dateTime.AddMinutes((double)amount);
			case ForecastParameterUnit.Second:
				return dateTime.AddSeconds((double)amount);
			default:
				throw new TransformException("Unsupported parameter unit of type " + unit.ToString());
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002528 File Offset: 0x00000728
		internal static double ToDouble(this DateTime date)
		{
			return (date - DataTypeExtensions.Epoch).TotalDays;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002548 File Offset: 0x00000748
		internal static DateTime ToDateTime(this double days)
		{
			return DataTypeExtensions.Epoch.AddDays(days);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002564 File Offset: 0x00000764
		internal static object ConvertTo(this double value, DataType type, ConvertType convertType = ConvertType.Round, double tolerance = 0.0)
		{
			object obj;
			try
			{
				switch (type)
				{
				case DataType.Decimal:
					return (decimal)value;
				case DataType.Double:
					return value;
				case DataType.Int64:
					return value.ConvertToInt64(convertType, tolerance);
				case DataType.Variant:
					return value;
				}
				throw new TransformException("Unsupported data type of type " + type.ToString());
			}
			catch (OverflowException)
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025F4 File Offset: 0x000007F4
		internal static object ConvertTo(this float value, DataType type, ConvertType convertType = ConvertType.Round)
		{
			return ((double)value).ConvertTo(type, convertType, 0.0);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002608 File Offset: 0x00000808
		internal static long ConvertToInt64(this double value, ConvertType convertType = ConvertType.Round, double tolerance = 0.0)
		{
			checked
			{
				switch (convertType)
				{
				case ConvertType.Round:
					return (long)Math.Round(value);
				case ConvertType.LowerBound:
					return (long)Math.Floor(unchecked(value + tolerance));
				case ConvertType.UpperBound:
					return (long)Math.Ceiling(unchecked(value - tolerance));
				default:
					throw new TransformException("Unsupported convert type " + convertType.ToString());
				}
			}
		}

		// Token: 0x0400004E RID: 78
		internal static readonly DateTime Epoch = DateTime.MinValue;
	}
}
