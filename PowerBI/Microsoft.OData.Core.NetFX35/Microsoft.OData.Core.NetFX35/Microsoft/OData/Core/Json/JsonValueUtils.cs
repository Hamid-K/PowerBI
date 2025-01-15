using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000117 RID: 279
	internal static class JsonValueUtils
	{
		// Token: 0x06000A79 RID: 2681 RVA: 0x00026558 File Offset: 0x00024758
		static JsonValueUtils()
		{
			JsonValueUtils.ODataNumberFormatInfo.PositiveInfinitySymbol = JsonValueUtils.ODataJsonPositiveInfinitySymbol;
			JsonValueUtils.ODataNumberFormatInfo.NegativeInfinitySymbol = JsonValueUtils.ODataJsonNegativeInfinitySymbol;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000265ED File Offset: 0x000247ED
		internal static void WriteValue(TextWriter writer, bool value)
		{
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00026604 File Offset: 0x00024804
		internal static void WriteValue(TextWriter writer, int value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00026618 File Offset: 0x00024818
		internal static void WriteValue(TextWriter writer, float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(JsonValueUtils.ODataNumberFormatInfo));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00026649 File Offset: 0x00024849
		internal static void WriteValue(TextWriter writer, short value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002665D File Offset: 0x0002485D
		internal static void WriteValue(TextWriter writer, long value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00026674 File Offset: 0x00024874
		internal static void WriteValue(TextWriter writer, double value)
		{
			if (JsonSharedUtils.IsDoubleValueSerializedAsString(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(JsonValueUtils.ODataNumberFormatInfo));
				return;
			}
			string text = XmlConvert.ToString(value);
			writer.Write(text);
			if (text.IndexOfAny(JsonValueUtils.DoubleIndicatingCharacters) < 0)
			{
				writer.Write(".0");
			}
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000266C3 File Offset: 0x000248C3
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000266D8 File Offset: 0x000248D8
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000266EC File Offset: 0x000248EC
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.OData.Core.Json.JsonValueUtils.WriteQuoted(System.IO.TextWriter,System.String)", Justification = "Constant defined by the JSON spec.")]
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

		// Token: 0x06000A83 RID: 2691 RVA: 0x00026780 File Offset: 0x00024980
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			JsonValueUtils.WriteQuoted(writer, EdmValueWriter.DurationAsXml(value));
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0002678E File Offset: 0x0002498E
		internal static void WriteValue(TextWriter writer, TimeOfDay value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x000267A3 File Offset: 0x000249A3
		internal static void WriteValue(TextWriter writer, Date value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000267B8 File Offset: 0x000249B8
		internal static void WriteValue(TextWriter writer, byte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x000267CC File Offset: 0x000249CC
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000267E0 File Offset: 0x000249E0
		internal static void WriteValue(TextWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000267F8 File Offset: 0x000249F8
		internal static void WriteValue(TextWriter writer, byte[] value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			writer.Write('"');
			writer.Write(Convert.ToBase64String(value));
			writer.Write('"');
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00026828 File Offset: 0x00024A28
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString)
		{
			writer.Write('"');
			int num = 0;
			int length = inputString.Length;
			int num2;
			for (int i = 0; i < length; i++)
			{
				char c = inputString.get_Chars(i);
				if (JsonValueUtils.SpecialCharToEscapedStringMap[(int)c] != null)
				{
					num2 = i - num;
					if (num2 > 0)
					{
						writer.Write(inputString.Substring(num, num2));
					}
					writer.Write(JsonValueUtils.SpecialCharToEscapedStringMap[(int)c]);
					num = i + 1;
				}
			}
			num2 = length - num;
			if (num2 > 0)
			{
				writer.Write(inputString.Substring(num, num2));
			}
			writer.Write('"');
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000268AB File Offset: 0x00024AAB
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x000268BB File Offset: 0x00024ABB
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000268D4 File Offset: 0x00024AD4
		private static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000268E4 File Offset: 0x00024AE4
		private static string[] CreateSpecialCharToEscapedStringMap()
		{
			string[] array = new string[65536];
			for (int i = 0; i <= 65535; i++)
			{
				if (i < 32 || i > 127)
				{
					array[i] = string.Format(CultureInfo.InvariantCulture, "\\u{0:x4}", new object[] { i });
				}
				else
				{
					array[i] = null;
				}
			}
			array[13] = "\\r";
			array[9] = "\\t";
			array[34] = "\\\"";
			array[92] = "\\\\";
			array[10] = "\\n";
			array[8] = "\\b";
			array[12] = "\\f";
			return array;
		}

		// Token: 0x04000440 RID: 1088
		internal static readonly string ODataJsonPositiveInfinitySymbol = "INF";

		// Token: 0x04000441 RID: 1089
		internal static readonly string ODataJsonNegativeInfinitySymbol = "-INF";

		// Token: 0x04000442 RID: 1090
		internal static readonly NumberFormatInfo ODataNumberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

		// Token: 0x04000443 RID: 1091
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, 1).Ticks;

		// Token: 0x04000444 RID: 1092
		private static readonly char[] DoubleIndicatingCharacters = new char[] { '.', 'e', 'E' };

		// Token: 0x04000445 RID: 1093
		private static readonly string[] SpecialCharToEscapedStringMap = JsonValueUtils.CreateSpecialCharToEscapedStringMap();
	}
}
