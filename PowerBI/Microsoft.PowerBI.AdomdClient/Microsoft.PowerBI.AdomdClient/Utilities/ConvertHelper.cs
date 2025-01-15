using System;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient.Utilities
{
	// Token: 0x02000143 RID: 323
	internal static class ConvertHelper
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x00037668 File Offset: 0x00035868
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

		// Token: 0x06001017 RID: 4119 RVA: 0x000376B2 File Offset: 0x000358B2
		public static string ConvertToCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", value.Substring(0, 1).ToLower(CultureInfo.InvariantCulture), value.Substring(1));
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000376E6 File Offset: 0x000358E6
		public static string ConvertFromCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", value.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture), value.Substring(1));
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0003771C File Offset: 0x0003591C
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
