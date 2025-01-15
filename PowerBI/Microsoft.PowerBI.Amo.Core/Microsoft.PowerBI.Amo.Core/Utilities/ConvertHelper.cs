using System;
using System.Globalization;

namespace Microsoft.AnalysisServices.Utilities
{
	// Token: 0x02000138 RID: 312
	internal static class ConvertHelper
	{
		// Token: 0x060010B1 RID: 4273 RVA: 0x0003A29C File Offset: 0x0003849C
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

		// Token: 0x060010B2 RID: 4274 RVA: 0x0003A2E6 File Offset: 0x000384E6
		public static string ConvertToCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", value.Substring(0, 1).ToLower(CultureInfo.InvariantCulture), value.Substring(1));
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0003A31A File Offset: 0x0003851A
		public static string ConvertFromCamelCase(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}{1}", value.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture), value.Substring(1));
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0003A350 File Offset: 0x00038550
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
