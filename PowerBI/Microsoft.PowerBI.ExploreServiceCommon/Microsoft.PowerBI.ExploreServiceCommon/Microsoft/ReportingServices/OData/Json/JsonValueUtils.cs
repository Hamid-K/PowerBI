using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000015 RID: 21
	internal static class JsonValueUtils
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000328B File Offset: 0x0000148B
		internal static void WriteValue(TextWriter writer, bool value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032A7 File Offset: 0x000014A7
		internal static void WriteValue(TextWriter writer, int value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000032C0 File Offset: 0x000014C0
		internal static void WriteValue(TextWriter writer, char value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032CE File Offset: 0x000014CE
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

		// Token: 0x06000078 RID: 120 RVA: 0x00003305 File Offset: 0x00001505
		internal static void WriteValue(TextWriter writer, short value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000331E File Offset: 0x0000151E
		internal static void WriteValue(TextWriter writer, long value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003337 File Offset: 0x00001537
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

		// Token: 0x0600007B RID: 123 RVA: 0x0000336E File Offset: 0x0000156E
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003388 File Offset: 0x00001588
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000033A4 File Offset: 0x000015A4
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
				string text2 = string.Format(CultureInfo.InvariantCulture, "\\/Date({0})\\/", new object[] { JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks) });
				JsonValueUtils.WriteQuoted(writer, text2);
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003408 File Offset: 0x00001608
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
				string text2 = string.Format(CultureInfo.InvariantCulture, "\\/Date({0}{1}{2:D4})\\/", new object[]
				{
					JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks),
					(num >= 0) ? "+" : string.Empty,
					num
				});
				JsonValueUtils.WriteQuoted(writer, text2);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000348E File Offset: 0x0000168E
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			DebugUtils.CheckNoExternalCallers();
			JsonValueUtils.WriteQuoted(writer, XmlConvert.ToString(value));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000034A1 File Offset: 0x000016A1
		internal static void WriteValue(TextWriter writer, byte value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000034BA File Offset: 0x000016BA
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000034D3 File Offset: 0x000016D3
		internal static void WriteValue(TextWriter writer, string value)
		{
			DebugUtils.CheckNoExternalCallers();
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000034F0 File Offset: 0x000016F0
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

		// Token: 0x06000084 RID: 132 RVA: 0x00003554 File Offset: 0x00001754
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString)
		{
			DebugUtils.CheckNoExternalCallers();
			writer.Write('"');
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < inputString.Length; i++)
			{
				char c = inputString[i];
				if (c == '\r' || c == '\t' || c == '"' || c == '\\' || c == '\n' || c < ' ' || c > '\u007f' || c == '\b' || c == '\f' || c == '%' || c == '&' || c == '\'' || c == '(' || c == ')' || c == '+' || c == '/' || c == '<' || c == '>')
				{
					writer.Write(inputString.Substring(num, num2));
					num = i + 1;
					num2 = 0;
					switch (c)
					{
					case '\b':
						writer.Write("\\b");
						goto IL_012F;
					case '\t':
						writer.Write("\\t");
						goto IL_012F;
					case '\n':
						writer.Write("\\n");
						goto IL_012F;
					case '\v':
						break;
					case '\f':
						writer.Write("\\f");
						goto IL_012F;
					case '\r':
						writer.Write("\\r");
						goto IL_012F;
					default:
						if (c == '"')
						{
							writer.Write("\\\"");
							goto IL_012F;
						}
						if (c == '\\')
						{
							writer.Write("\\\\");
							goto IL_012F;
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
				IL_012F:;
			}
			if (num2 > 0)
			{
				writer.Write(inputString.Substring(num, num2));
			}
			writer.Write('"');
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000036BA File Offset: 0x000018BA
		private static string EscapeChar(char c)
		{
			return string.Format(CultureInfo.InvariantCulture, "\\u{0:x4}", new object[] { (int)c });
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000036DA File Offset: 0x000018DA
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			DebugUtils.CheckNoExternalCallers();
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000036EF File Offset: 0x000018EF
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003708 File Offset: 0x00001908
		internal static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003718 File Offset: 0x00001918
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

		// Token: 0x0400009D RID: 157
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
	}
}
