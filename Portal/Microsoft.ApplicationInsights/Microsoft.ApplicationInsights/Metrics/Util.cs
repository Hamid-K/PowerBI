using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x0200002B RID: 43
	internal static class Util
	{
		// Token: 0x0600016F RID: 367 RVA: 0x00008964 File Offset: 0x00006B64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateNotNull(object value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name ?? "specified parameter");
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00008979 File Offset: 0x00006B79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateNotNullOrEmpty(string value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name ?? "specified parameter");
			}
			if (value.Length == 0)
			{
				throw new ArgumentException((name ?? "specified parameter") + " may not be empty.");
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000089B0 File Offset: 0x00006BB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateNotNullOrWhitespace(string value, string name)
		{
			Util.ValidateNotNullOrEmpty(value, name);
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException((name ?? "specified parameter") + " may not be whitespace only.");
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000089DC File Offset: 0x00006BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double EnsureConcreteValue(double x)
		{
			if (x < -1.7976931348623157E+308)
			{
				return double.MinValue;
			}
			if (x > 1.7976931348623157E+308)
			{
				return double.MaxValue;
			}
			if (!double.IsNaN(x))
			{
				return x;
			}
			return 0.0;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00008A28 File Offset: 0x00006C28
		public static double RoundAndValidateValue(double value)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException("Cannot process the specified value. A non-negative whole number was expected, but the specified value is Double.NaN. Have you specified the correct metric configuration?");
			}
			if (value < -1E-06)
			{
				throw new ArgumentException("Cannot process the specified value. A non-negative whole number was expected, but the specified value is a negative double value (" + value + "). Have you specified the correct metric configuration?");
			}
			double num = Math.Round(value);
			if (num > 4294967295.0)
			{
				throw new ArgumentException("Cannot process the specified value. A non-negative whole number was expected, but the specified value is larger than the maximum accepted value (" + value + "). Have you specified the correct metric configuration?");
			}
			if (Math.Abs(value - num) > 1E-06)
			{
				throw new ArgumentException("Cannot process the specified value. A non-negative whole number was expected, but the specified value is a double value that does not equal to a whole number (" + value + "). Have you specified the correct metric configuration?");
			}
			return num;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00008ACC File Offset: 0x00006CCC
		public static double ConvertToDoubleValue(object metricValue)
		{
			if (metricValue == null)
			{
				return double.NaN;
			}
			if (metricValue is sbyte)
			{
				return (double)((sbyte)metricValue);
			}
			if (metricValue is byte)
			{
				return (double)((byte)metricValue);
			}
			if (metricValue is short)
			{
				return (double)((short)metricValue);
			}
			if (metricValue is ushort)
			{
				return (double)((ushort)metricValue);
			}
			if (metricValue is int)
			{
				return (double)((int)metricValue);
			}
			if (metricValue is uint)
			{
				return (uint)metricValue;
			}
			if (metricValue is long)
			{
				return (double)((long)metricValue);
			}
			if (metricValue is ulong)
			{
				return (ulong)metricValue;
			}
			if (metricValue is float)
			{
				return (double)((float)metricValue);
			}
			if (metricValue is double)
			{
				return (double)metricValue;
			}
			string text = metricValue as string;
			if (text == null)
			{
				throw new ArgumentException("Cannot process the specified value. A numeric value was expected, but the specified metricValue is of type " + metricValue.GetType().FullName + ". Have you specified the correct metric configuration?");
			}
			double num;
			if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
			{
				return num;
			}
			throw new ArgumentException("Cannot process the specified value. A numeric value was expected, but the specified metricValue is a String that cannot be parsed into a number (\"" + metricValue + "\"). Have you specified the correct metric configuration?");
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00008BDC File Offset: 0x00006DDC
		public static DateTimeOffset RoundDownToMinute(DateTimeOffset dto)
		{
			return new DateTimeOffset(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, 0, 0, dto.Offset);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00008C0F File Offset: 0x00006E0F
		public static DateTimeOffset RoundDownToSecond(DateTimeOffset dto)
		{
			return new DateTimeOffset(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, dto.Second, 0, dto.Offset);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00008C48 File Offset: 0x00006E48
		public static bool FilterWillConsume(IMetricSeriesFilter seriesFilter, MetricSeries series, out IMetricValueFilter valueFilter)
		{
			valueFilter = null;
			bool flag;
			try
			{
				flag = seriesFilter == null || seriesFilter.WillConsume(series, out valueFilter);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00008C80 File Offset: 0x00006E80
		public static bool FilterWillConsume(IMetricValueFilter valueFilter, MetricSeries series, double metricValue)
		{
			bool flag;
			try
			{
				flag = valueFilter == null || valueFilter.WillConsume(series, metricValue);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008CB4 File Offset: 0x00006EB4
		public static bool FilterWillConsume(IMetricValueFilter valueFilter, MetricSeries series, object metricValue)
		{
			bool flag;
			try
			{
				flag = valueFilter == null || valueFilter.WillConsume(series, metricValue);
			}
			catch
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public static int CombineHashCodes(int hash1)
		{
			return 17 * 23 + hash1;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00008CF1 File Offset: 0x00006EF1
		public static int CombineHashCodes(int hash1, int hash2)
		{
			return (17 * 23 + hash1) * 23 + hash2;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008CFF File Offset: 0x00006EFF
		public static int CombineHashCodes(int hash1, int hash2, int hash3, int hash4)
		{
			return (((17 * 23 + hash1) * 23 + hash2) * 23 + hash3) * 23 + hash4;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00008D18 File Offset: 0x00006F18
		public static int CombineHashCodes(int[] arr)
		{
			if (arr == null)
			{
				return 0;
			}
			int num = 17;
			for (int i = 0; i < arr.Length; i++)
			{
				num = num * 23 + arr[i];
			}
			return num;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00008D48 File Offset: 0x00006F48
		public static void CopyTelemetryContext(TelemetryContext source, TelemetryContext target)
		{
			Util.ValidateNotNull(source, "source");
			Util.ValidateNotNull(target, "target");
			target.Initialize(source, null);
			Utils.CopyDictionary<string>(source.Properties, target.Properties);
			if (source.GlobalPropertiesValue != null)
			{
				Utils.CopyDictionary<string>(source.GlobalProperties, target.GlobalProperties);
			}
			if (source.InstrumentationKey != null)
			{
				target.InstrumentationKey = source.InstrumentationKey;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00008DB4 File Offset: 0x00006FB4
		public static void StampSdkVersionToContext(ITelemetry aggregate)
		{
			InternalContext internalContext;
			if (aggregate == null)
			{
				internalContext = null;
			}
			else
			{
				TelemetryContext context = aggregate.Context;
				internalContext = ((context != null) ? context.GetInternalContext() : null);
			}
			InternalContext internalContext2 = internalContext;
			if (internalContext2 == null)
			{
				return;
			}
			internalContext2.SdkVersion = Util.sdkVersionMoniker;
		}

		// Token: 0x040000AB RID: 171
		public const string NullString = "null";

		// Token: 0x040000AC RID: 172
		private const double MicroOne = 1E-06;

		// Token: 0x040000AD RID: 173
		private const string FallbackParameterName = "specified parameter";

		// Token: 0x040000AE RID: 174
		private static string sdkVersionMoniker = SdkVersionUtils.GetSdkVersion("m-agg2:");
	}
}
