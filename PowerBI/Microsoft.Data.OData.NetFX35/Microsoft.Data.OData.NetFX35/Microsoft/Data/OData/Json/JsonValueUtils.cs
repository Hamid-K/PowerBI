using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x020002A6 RID: 678
	internal static class JsonValueUtils
	{
		// Token: 0x06001594 RID: 5524 RVA: 0x0004E82D File Offset: 0x0004CA2D
		internal static void WriteValue(TextWriter writer, bool value)
		{
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0004E844 File Offset: 0x0004CA44
		internal static void WriteValue(TextWriter writer, int value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0004E858 File Offset: 0x0004CA58
		internal static void WriteValue(TextWriter writer, float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0004E88A File Offset: 0x0004CA8A
		internal static void WriteValue(TextWriter writer, short value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0004E89E File Offset: 0x0004CA9E
		internal static void WriteValue(TextWriter writer, long value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0004E8B4 File Offset: 0x0004CAB4
		internal static void WriteValue(TextWriter writer, double value, bool mustIncludeDecimalPoint)
		{
			if (JsonSharedUtils.IsDoubleValueSerializedAsString(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			string text = XmlConvert.ToString(value);
			writer.Write(text);
			if (mustIncludeDecimalPoint && text.IndexOfAny(JsonValueUtils.DoubleIndicatingCharacters) < 0)
			{
				writer.Write(".0");
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0004E907 File Offset: 0x0004CB07
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0004E91C File Offset: 0x0004CB1C
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0004E930 File Offset: 0x0004CB30
		internal static void WriteValue(TextWriter writer, DateTime value, ODataJsonDateTimeFormat dateTimeFormat)
		{
			switch (dateTimeFormat)
			{
			case ODataJsonDateTimeFormat.ODataDateTime:
			{
				value = JsonValueUtils.GetUniversalDate(value);
				string text = string.Format(CultureInfo.InvariantCulture, "\\/Date({0})\\/", new object[] { JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks) });
				JsonValueUtils.WriteQuoted(writer, text);
				return;
			}
			case ODataJsonDateTimeFormat.ISO8601DateTime:
			{
				string text2 = PlatformHelper.ConvertDateTimeToString(value);
				JsonValueUtils.WriteQuoted(writer, text2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0004E998 File Offset: 0x0004CB98
		internal static void WriteValue(TextWriter writer, DateTimeOffset value, ODataJsonDateTimeFormat dateTimeFormat)
		{
			int num = (int)value.Offset.TotalMinutes;
			switch (dateTimeFormat)
			{
			case ODataJsonDateTimeFormat.ODataDateTime:
			{
				string text = string.Format(CultureInfo.InvariantCulture, "\\/Date({0}{1}{2:D4})\\/", new object[]
				{
					JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks),
					(num >= 0) ? "+" : string.Empty,
					num
				});
				JsonValueUtils.WriteQuoted(writer, text);
				return;
			}
			case ODataJsonDateTimeFormat.ISO8601DateTime:
			{
				string text2 = XmlConvert.ToString(value);
				JsonValueUtils.WriteQuoted(writer, text2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0004EA2C File Offset: 0x0004CC2C
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			JsonValueUtils.WriteQuoted(writer, XmlConvert.ToString(value));
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0004EA3A File Offset: 0x0004CC3A
		internal static void WriteValue(TextWriter writer, byte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0004EA4E File Offset: 0x0004CC4E
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0004EA62 File Offset: 0x0004CC62
		internal static void WriteValue(TextWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x0004EA7C File Offset: 0x0004CC7C
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString)
		{
			writer.Write('"');
			int num = 0;
			int length = inputString.Length;
			int i = 0;
			int num2;
			while (i < length)
			{
				char c = inputString.get_Chars(i);
				char c2 = c;
				string text;
				switch (c2)
				{
				case '\b':
					text = "\\b";
					goto IL_00A9;
				case '\t':
					text = "\\t";
					goto IL_00A9;
				case '\n':
					text = "\\n";
					goto IL_00A9;
				case '\v':
					goto IL_0093;
				case '\f':
					text = "\\f";
					goto IL_00A9;
				case '\r':
					text = "\\r";
					goto IL_00A9;
				default:
					if (c2 == '"')
					{
						text = "\\\"";
						goto IL_00A9;
					}
					if (c2 != '\\')
					{
						goto IL_0093;
					}
					text = "\\\\";
					goto IL_00A9;
				}
				IL_00CB:
				i++;
				continue;
				IL_0093:
				if (c >= ' ' && c <= '\u007f')
				{
					goto IL_00CB;
				}
				text = JsonValueUtils.SpecialCharToEscapedStringMap[(int)c];
				IL_00A9:
				num2 = i - num;
				if (num2 > 0)
				{
					writer.Write(inputString.Substring(num, num2));
				}
				writer.Write(text);
				num = i + 1;
				goto IL_00CB;
			}
			num2 = length - num;
			if (num2 > 0)
			{
				writer.Write(inputString.Substring(num, num2));
			}
			writer.Write('"');
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0004EB7D File Offset: 0x0004CD7D
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0004EB8D File Offset: 0x0004CD8D
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0004EBA6 File Offset: 0x0004CDA6
		private static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0004EBB8 File Offset: 0x0004CDB8
		private static DateTime GetUniversalDate(DateTime value)
		{
			switch (value.Kind)
			{
			case 0:
				value..ctor(value.Ticks, 1);
				break;
			case 2:
				value = value.ToUniversalTime();
				break;
			}
			return value;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004EBFC File Offset: 0x0004CDFC
		private static string[] CreateSpecialCharToEscapedStringMap()
		{
			string[] array = new string[65536];
			for (int i = 0; i <= 65535; i++)
			{
				array[i] = string.Format(CultureInfo.InvariantCulture, "\\u{0:x4}", new object[] { i });
			}
			return array;
		}

		// Token: 0x0400096E RID: 2414
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, 1).Ticks;

		// Token: 0x0400096F RID: 2415
		private static readonly char[] DoubleIndicatingCharacters = new char[] { '.', 'e', 'E' };

		// Token: 0x04000970 RID: 2416
		private static readonly string[] SpecialCharToEscapedStringMap = JsonValueUtils.CreateSpecialCharToEscapedStringMap();
	}
}
