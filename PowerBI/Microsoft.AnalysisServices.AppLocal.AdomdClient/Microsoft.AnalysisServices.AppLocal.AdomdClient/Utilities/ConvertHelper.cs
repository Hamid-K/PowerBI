using System;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000143 RID: 323
	internal static class ConvertHelper
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x00037998 File Offset: 0x00035B98
		public static bool TryParseBool(string value, bool treatNonZeroIntValueAsTrue, out bool result)
		{
			if (!string.IsNullOrEmpty(value))
			{
				if (bool.TryParse(value, out result))
				{
					return true;
				}
				int num;
				if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					if (num == 0)
					{
						result = false;
						return true;
					}
					result = treatNonZeroIntValueAsTrue || num == 1;
					return true;
				}
			}
			result = false;
			return false;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000379E2 File Offset: 0x00035BE2
		public static string ConvertToCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", value.Substring(0, 1).ToLower(CultureInfo.InvariantCulture), value.Substring(1));
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00037A16 File Offset: 0x00035C16
		public static string ConvertFromCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", value.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture), value.Substring(1));
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x00037A4C File Offset: 0x00035C4C
		public static TEnum ParseRawEnumValue<TEnum>(object rawValue, bool isJson, bool ignoreCase)
		{
			string text;
			if (rawValue is int)
			{
				text = ((int)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is long)
			{
				text = ((long)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is sbyte)
			{
				text = ((sbyte)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is byte)
			{
				text = ((byte)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is short)
			{
				text = ((short)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is ushort)
			{
				text = ((ushort)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is uint)
			{
				text = ((uint)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else if (rawValue is ulong)
			{
				text = ((ulong)rawValue).ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				string text2 = rawValue as string;
				if (text2 != null)
				{
					text = (isJson ? ConvertHelper.ConvertFromCamelCase(text2) : text2);
				}
				else
				{
					text = rawValue.ToString();
				}
			}
			return (TEnum)((object)Enum.Parse(typeof(TEnum), text, ignoreCase));
		}
	}
}
