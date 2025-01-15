using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000089 RID: 137
	internal static class Utils
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x00013301 File Offset: 0x00011501
		public static bool IsNullOrWhiteSpace(this string value)
		{
			return value == null || value.All(new Func<char, bool>(char.IsWhiteSpace));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001331C File Offset: 0x0001151C
		public static void CopyDictionary<TValue>(IDictionary<string, TValue> source, IDictionary<string, TValue> target)
		{
			foreach (KeyValuePair<string, TValue> keyValuePair in source)
			{
				if (!string.IsNullOrEmpty(keyValuePair.Key) && !target.ContainsKey(keyValuePair.Key))
				{
					target[keyValuePair.Key] = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00013390 File Offset: 0x00011590
		public static string PopulateRequiredStringValue(string value, string parameterName, string telemetryType)
		{
			if (string.IsNullOrEmpty(value))
			{
				CoreEventSource.Log.PopulateRequiredStringWithValue(parameterName, telemetryType, "Incorrect");
				return "n/a";
			}
			return value;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000133B4 File Offset: 0x000115B4
		public static TimeSpan ValidateDuration(string value)
		{
			TimeSpan timeSpan;
			if (!TimeSpan.TryParse(value, CultureInfo.InvariantCulture, out timeSpan))
			{
				CoreEventSource.Log.TelemetryIncorrectDuration("Incorrect");
				return TimeSpan.Zero;
			}
			return timeSpan;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000133E8 File Offset: 0x000115E8
		public static double SanitizeNanAndInfinity(double value)
		{
			bool flag;
			return Utils.SanitizeNanAndInfinity(value, out flag);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000133FD File Offset: 0x000115FD
		public static double SanitizeNanAndInfinity(double value, out bool valueChanged)
		{
			valueChanged = false;
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				value = 0.0;
				valueChanged = true;
			}
			return value;
		}
	}
}
