using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000012 RID: 18
	internal static class JsonValueUtils
	{
		// Token: 0x06000075 RID: 117 RVA: 0x0000334B File Offset: 0x0000154B
		internal static void WriteValue(TextWriter writer, bool value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003367 File Offset: 0x00001567
		internal static void WriteValue(TextWriter writer, int value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003380 File Offset: 0x00001580
		internal static void WriteValue(TextWriter writer, char value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000338E File Offset: 0x0000158E
		internal static void WriteValue(TextWriter writer, float value)
		{
			DebugUtils.CheckNoExternalCallers();
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033C5 File Offset: 0x000015C5
		internal static void WriteValue(TextWriter writer, short value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033DE File Offset: 0x000015DE
		internal static void WriteValue(TextWriter writer, long value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033F7 File Offset: 0x000015F7
		internal static void WriteValue(TextWriter writer, double value)
		{
			DebugUtils.CheckNoExternalCallers();
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000342E File Offset: 0x0000162E
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003448 File Offset: 0x00001648
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003464 File Offset: 0x00001664
		internal static void WriteValue(TextWriter writer, DateTime value, ODataJsonDateTimeFormat dateTimeFormat)
		{
			DebugUtils.CheckNoExternalCallers();
			if (dateTimeFormat != ODataJsonDateTimeFormat.ODataDateTime)
			{
				if (dateTimeFormat == ODataJsonDateTimeFormat.ISO8601DateTime)
				{
					string text = PlatformHelper.ConvertDateTimeToString(value);
					JsonValueUtils.WriteQuoted(writer, text);
					return;
				}
			}
			else
			{
				value = JsonValueUtils.GetUniversalDate(value);
				string text2 = string.Format(CultureInfo.InvariantCulture, "\\/Date({0})\\/", JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks));
				JsonValueUtils.WriteQuoted(writer, text2);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000034BC File Offset: 0x000016BC
		internal static void WriteValue(TextWriter writer, DateTimeOffset value, ODataJsonDateTimeFormat dateTimeFormat)
		{
			DebugUtils.CheckNoExternalCallers();
			int num = (int)value.Offset.TotalMinutes;
			if (dateTimeFormat != ODataJsonDateTimeFormat.ODataDateTime)
			{
				if (dateTimeFormat == ODataJsonDateTimeFormat.ISO8601DateTime)
				{
					string text = XmlConvert.ToString(value);
					JsonValueUtils.WriteQuoted(writer, text);
					return;
				}
			}
			else
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, "\\/Date({0}{1}{2:D4})\\/", JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks), (num >= 0) ? "+" : string.Empty, num);
				JsonValueUtils.WriteQuoted(writer, text2);
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003533 File Offset: 0x00001733
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, XmlConvert.ToString(value));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003546 File Offset: 0x00001746
		internal static void WriteValue(TextWriter writer, byte value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000355F File Offset: 0x0000175F
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003578 File Offset: 0x00001778
		internal static void WriteValue(TextWriter writer, string value, bool escapeUnicode = true)
		{
			DebugUtils.CheckNoExternalCallers();
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value, escapeUnicode);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003598 File Offset: 0x00001798
		internal static void WriteValue(TextWriter writer, byte[] value)
		{
			DebugUtils.CheckNoExternalCallers();
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			writer.Write('"');
			writer.Write("base64");
			writer.Write(JsonValueUtils.EscapeChar('\''));
			writer.Write(Convert.ToBase64String(value));
			writer.Write(JsonValueUtils.EscapeChar('\''));
			writer.Write('"');
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000035FC File Offset: 0x000017FC
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString, bool escapeUnicode = true)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write('"');
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < inputString.Length; i++)
			{
				char c = inputString[i];
				bool flag = escapeUnicode && (c > '\u007f' || c == '%' || c == '&' || c == '\'' || c == '(' || c == ')' || c == '+' || c == '/' || c == '<' || c == '>');
				if (c == '"' || c == '\\' || c == '/' || c == '\b' || c == '\f' || c == '\n' || c == '\r' || c == '\t' || c < ' ' || flag)
				{
					writer.Write(inputString.Substring(num, num2));
					num = i + 1;
					num2 = 0;
					switch (c)
					{
					case '\b':
						writer.Write("\\b");
						goto IL_0147;
					case '\t':
						writer.Write("\\t");
						goto IL_0147;
					case '\n':
						writer.Write("\\n");
						goto IL_0147;
					case '\v':
						break;
					case '\f':
						writer.Write("\\f");
						goto IL_0147;
					case '\r':
						writer.Write("\\r");
						goto IL_0147;
					default:
						if (c == '"')
						{
							writer.Write("\\\"");
							goto IL_0147;
						}
						if (c == '\\')
						{
							writer.Write("\\\\");
							goto IL_0147;
						}
						break;
					}
					string text = JsonValueUtils.EscapeChar(c);
					writer.Write(text);
				}
				else
				{
					num2++;
				}
				IL_0147:;
			}
			if (num2 > 0)
			{
				writer.Write(inputString.Substring(num, num2));
			}
			writer.Write('"');
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000377A File Offset: 0x0000197A
		private static string EscapeChar(char c)
		{
			return string.Format(CultureInfo.InvariantCulture, "\\u{0:x4}", (int)c);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003791 File Offset: 0x00001991
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			DebugUtils.CheckNoExternalCallers();
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000037A6 File Offset: 0x000019A6
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000037BF File Offset: 0x000019BF
		internal static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000037D0 File Offset: 0x000019D0
		internal static DateTime GetUniversalDate(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Utc);
				break;
			case DateTimeKind.Local:
				value = value.ToUniversalTime();
				break;
			}
			return value;
		}

		// Token: 0x04000079 RID: 121
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
	}
}
