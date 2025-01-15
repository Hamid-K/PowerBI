using System;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200047E RID: 1150
	internal static class SapHanaValueFormatter
	{
		// Token: 0x06002629 RID: 9769 RVA: 0x0006E77F File Offset: 0x0006C97F
		public static string FormatBoolean(LogicalValue value)
		{
			if (!value.AsBoolean)
			{
				return "FALSE";
			}
			return "TRUE";
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0006E794 File Offset: 0x0006C994
		public static string FormatInteger(NumberValue value)
		{
			return value.AsInteger64.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0006E7B4 File Offset: 0x0006C9B4
		public static string FormatDouble(NumberValue value)
		{
			double asDouble = value.AsDouble;
			if (double.IsInfinity(asDouble) || double.IsNaN(asDouble))
			{
				throw new NotSupportedException();
			}
			return asDouble.ToString("E15", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0006E7F0 File Offset: 0x0006C9F0
		public static string FormatDate(DateValue value)
		{
			return value.AsClrDateTime.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0006E818 File Offset: 0x0006CA18
		public static string FormatDateTime(DateTimeValue value)
		{
			return value.AsClrDateTime.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0006E840 File Offset: 0x0006CA40
		public static string FormatTime(TimeValue value)
		{
			TimeSpan asClrTimeSpan = value.AsClrTimeSpan;
			return string.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}:{2:D2}", asClrTimeSpan.Hours, asClrTimeSpan.Minutes, asClrTimeSpan.Seconds);
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0006E888 File Offset: 0x0006CA88
		public static string FormatBinary(BinaryValue value)
		{
			byte[] asBytes = value.AsBytes;
			char[] array = new char[asBytes.Length * 2];
			for (int i = 0; i < asBytes.Length; i++)
			{
				byte b = asBytes[i];
				array[i * 2] = SapHanaValueFormatter.ToHexChar(b >> 4);
				array[i * 2 + 1] = SapHanaValueFormatter.ToHexChar((int)(b & 15));
			}
			return new string(array);
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x0006E8DC File Offset: 0x0006CADC
		public static string FormatParameterArguments(Value value, bool quoteStrings = false)
		{
			switch (value.Kind)
			{
			case ValueKind.Time:
				return SapHanaValueFormatter.EscapeParameterArgument(SapHanaValueFormatter.FormatTime(value.AsTime));
			case ValueKind.Date:
				return SapHanaValueFormatter.EscapeParameterArgument(SapHanaValueFormatter.FormatDate(value.AsDate));
			case ValueKind.DateTime:
				return SapHanaValueFormatter.EscapeParameterArgument(SapHanaValueFormatter.FormatDateTime(value.AsDateTime));
			case ValueKind.Number:
			{
				NumberValue asNumber = value.AsNumber;
				if (asNumber.IsInteger64)
				{
					return SapHanaValueFormatter.EscapeParameterArgument(SapHanaValueFormatter.FormatInteger(asNumber));
				}
				return SapHanaValueFormatter.EscapeParameterArgument(SapHanaValueFormatter.FormatDouble(asNumber));
			}
			case ValueKind.Logical:
				return SapHanaValueFormatter.EscapeParameterArgument(SapHanaValueFormatter.FormatBoolean(value.AsLogical));
			case ValueKind.Text:
			{
				string text = "''";
				string text2 = SapHanaValueFormatter.EscapeParameterArgument(value.AsString);
				if (!quoteStrings)
				{
					return text2;
				}
				return text + text2 + text;
			}
			case ValueKind.List:
			{
				ListValue asList = value.AsList;
				return string.Join(", ", asList.Select((IValueReference v) => SapHanaValueFormatter.FormatParameterArguments(v.Value, true)).ToArray<string>());
			}
			}
			throw ValueException.NotImplemented<Message1>(Strings.SapHanaUnsupportedParameterValueKind(value.Kind));
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x0006EA03 File Offset: 0x0006CC03
		private static string EscapeParameterArgument(string unescaped)
		{
			if (unescaped != null)
			{
				return unescaped.Replace("\\", "\\\\").Replace("'", "\\''");
			}
			return null;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x0006EA29 File Offset: 0x0006CC29
		private static char ToHexChar(int i)
		{
			if (i <= 9)
			{
				return (char)(48 + i);
			}
			return (char)(65 + i - 10);
		}
	}
}
