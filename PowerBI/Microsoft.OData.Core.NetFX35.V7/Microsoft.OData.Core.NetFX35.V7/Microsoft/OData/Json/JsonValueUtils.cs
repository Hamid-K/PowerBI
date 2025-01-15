using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E8 RID: 488
	internal static class JsonValueUtils
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x0003728A File Offset: 0x0003548A
		internal static void WriteValue(TextWriter writer, bool value)
		{
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000372A1 File Offset: 0x000354A1
		internal static void WriteValue(TextWriter writer, int value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000372B5 File Offset: 0x000354B5
		internal static void WriteValue(TextWriter writer, float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(JsonValueUtils.ODataNumberFormatInfo));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000372E6 File Offset: 0x000354E6
		internal static void WriteValue(TextWriter writer, short value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000372FA File Offset: 0x000354FA
		internal static void WriteValue(TextWriter writer, long value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00037310 File Offset: 0x00035510
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

		// Token: 0x06001324 RID: 4900 RVA: 0x0003735F File Offset: 0x0003555F
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00037374 File Offset: 0x00035574
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00037388 File Offset: 0x00035588
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.OData.Json.JsonValueUtils.WriteQuoted(System.IO.TextWriter,System.String)", Justification = "Constant defined by the JSON spec.")]
		internal static void WriteValue(TextWriter writer, DateTimeOffset value, ODataJsonDateTimeFormat dateTimeFormat)
		{
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

		// Token: 0x06001327 RID: 4903 RVA: 0x00037409 File Offset: 0x00035609
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			JsonValueUtils.WriteQuoted(writer, EdmValueWriter.DurationAsXml(value));
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00037417 File Offset: 0x00035617
		internal static void WriteValue(TextWriter writer, TimeOfDay value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0003742C File Offset: 0x0003562C
		internal static void WriteValue(TextWriter writer, Date value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00037441 File Offset: 0x00035641
		internal static void WriteValue(TextWriter writer, byte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00037455 File Offset: 0x00035655
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00037469 File Offset: 0x00035669
		internal static void WriteValue(TextWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00037481 File Offset: 0x00035681
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

		// Token: 0x0600132E RID: 4910 RVA: 0x000374B0 File Offset: 0x000356B0
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

		// Token: 0x0600132F RID: 4911 RVA: 0x00037533 File Offset: 0x00035733
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00037544 File Offset: 0x00035744
		private static NumberFormatInfo InitializeODataNumberFormatInfo()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
			numberFormatInfo.PositiveInfinitySymbol = JsonValueUtils.ODataJsonPositiveInfinitySymbol;
			numberFormatInfo.NegativeInfinitySymbol = JsonValueUtils.ODataJsonNegativeInfinitySymbol;
			return numberFormatInfo;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0003757D File Offset: 0x0003577D
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00037596 File Offset: 0x00035796
		private static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x000375A8 File Offset: 0x000357A8
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

		// Token: 0x040009BE RID: 2494
		internal static readonly string ODataJsonPositiveInfinitySymbol = "INF";

		// Token: 0x040009BF RID: 2495
		internal static readonly string ODataJsonNegativeInfinitySymbol = "-INF";

		// Token: 0x040009C0 RID: 2496
		internal static readonly NumberFormatInfo ODataNumberFormatInfo = JsonValueUtils.InitializeODataNumberFormatInfo();

		// Token: 0x040009C1 RID: 2497
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, 1).Ticks;

		// Token: 0x040009C2 RID: 2498
		private static readonly char[] DoubleIndicatingCharacters = new char[] { '.', 'e', 'E' };

		// Token: 0x040009C3 RID: 2499
		private static readonly string[] SpecialCharToEscapedStringMap = JsonValueUtils.CreateSpecialCharToEscapedStringMap();
	}
}
