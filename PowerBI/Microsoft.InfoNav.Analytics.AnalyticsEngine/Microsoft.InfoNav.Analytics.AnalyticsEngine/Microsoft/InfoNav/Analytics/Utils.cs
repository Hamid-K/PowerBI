using System;
using System.Data;
using System.Globalization;
using System.Text;
using Microsoft.InfoNav.Analytics.Clustering;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000021 RID: 33
	internal static class Utils
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000369F File Offset: 0x0000189F
		internal static bool IsDate(this ForecastParameterUnit unit)
		{
			return unit == ForecastParameterUnit.Day || unit == ForecastParameterUnit.Hour || unit == ForecastParameterUnit.Minute || unit == ForecastParameterUnit.Month || unit == ForecastParameterUnit.Quarter || unit == ForecastParameterUnit.Second || unit == ForecastParameterUnit.Year;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036C0 File Offset: 0x000018C0
		internal static string ToString(this Exception e, int levelLimit = 3)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(e.Message + "stack:" + e.StackTrace.ToString());
			int num = 0;
			while (e.InnerException != null && num < levelLimit)
			{
				e = e.InnerException;
				stringBuilder.Append(string.Concat(new string[]
				{
					"inner:",
					e.Message,
					"stack:",
					e.StackTrace.ToString(),
					","
				}));
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003758 File Offset: 0x00001958
		internal static bool IsAnyInfinity(float value1, float value2, float value3)
		{
			return float.IsInfinity(value1) || float.IsInfinity(value2) || float.IsInfinity(value3);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003772 File Offset: 0x00001972
		internal static bool IsAnyNan(float value1, float value2, float value3)
		{
			return float.IsNaN(value1) || float.IsNaN(value2) || float.IsNaN(value3);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000378C File Offset: 0x0000198C
		internal static bool IsAnyInfinity(double value1, double value2, double value3)
		{
			return double.IsInfinity(value1) || double.IsInfinity(value2) || double.IsInfinity(value3);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000037A6 File Offset: 0x000019A6
		internal static bool IsAnyNan(double value1, double value2, double value3)
		{
			return double.IsNaN(value1) || double.IsNaN(value2) || double.IsNaN(value3);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000037C0 File Offset: 0x000019C0
		internal static bool TryConvertToInt32(this object value, out int intValue)
		{
			intValue = 0;
			if (value == null)
			{
				return false;
			}
			bool flag;
			try
			{
				intValue = Convert.ToInt32(value);
				flag = true;
			}
			catch (OverflowException)
			{
				flag = false;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000381C File Offset: 0x00001A1C
		internal static bool TryConvertToFloat(this object value, out float floatValue)
		{
			floatValue = 0f;
			if (value == null)
			{
				return false;
			}
			bool flag;
			try
			{
				floatValue = Convert.ToSingle(value);
				flag = !float.IsNaN(floatValue) && !float.IsInfinity(floatValue);
			}
			catch (OverflowException)
			{
				flag = false;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003890 File Offset: 0x00001A90
		internal static bool TryConvertNumericToDouble(this object value, DbType dataType, out double doubleValue)
		{
			doubleValue = 0.0;
			if (value == null)
			{
				return false;
			}
			if (dataType <= DbType.Double)
			{
				if (dataType == DbType.Currency)
				{
					doubleValue = (double)((decimal)value);
					return true;
				}
				if (dataType == DbType.Double)
				{
					doubleValue = (double)value;
					return true;
				}
			}
			else
			{
				if (dataType == DbType.Int64)
				{
					doubleValue = (double)((long)value);
					return true;
				}
				if (dataType == DbType.Object)
				{
					if (value is long || value is decimal || value is double)
					{
						doubleValue = Convert.ToDouble(value, CultureInfo.InvariantCulture);
						return true;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003916 File Offset: 0x00001B16
		internal static bool IsStrictlyFinite(this double value)
		{
			return value > double.MinValue && value < double.MaxValue;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003932 File Offset: 0x00001B32
		internal static bool IsNumeric(this DbType dataType)
		{
			return dataType == DbType.Currency || dataType == DbType.Double || dataType - DbType.Int64 <= 1;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003946 File Offset: 0x00001B46
		internal static void ThrowExtensionException(string errorString, Exception innerException = null)
		{
			if (innerException == null)
			{
				throw new TransformException(errorString);
			}
			throw new TransformException(errorString, innerException);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003959 File Offset: 0x00001B59
		internal static void ThrowExtensionException(ClusteringErrorType error, Exception innerException = null)
		{
			Utils.ThrowExtensionException(error.ToErrorCode(), innerException);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003967 File Offset: 0x00001B67
		internal static void ThrowExtensionException(ClusteringErrorType error)
		{
			Utils.ThrowExtensionException(error.ToErrorCode(), null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003975 File Offset: 0x00001B75
		internal static string ToErrorCode(this ClusteringErrorType error)
		{
			return StringUtil.FormatInvariant("{0}.{1}", error.GetType().Name, error.ToString());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000039A0 File Offset: 0x00001BA0
		internal static DateTime AddMonthEndAware(DateTime baseDate, int months)
		{
			if (baseDate.Day != DateTime.DaysInMonth(baseDate.Year, baseDate.Month))
			{
				return baseDate.AddMonths(months);
			}
			return baseDate.AddDays(1.0).AddMonths(months).AddDays(-1.0);
		}
	}
}
