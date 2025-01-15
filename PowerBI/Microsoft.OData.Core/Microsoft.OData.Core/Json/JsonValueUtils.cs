using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.OData.Buffers;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x0200021A RID: 538
	internal static class JsonValueUtils
	{
		// Token: 0x0600178F RID: 6031 RVA: 0x000430C8 File Offset: 0x000412C8
		internal static void WriteValue(TextWriter writer, char value, ODataStringEscapeOption stringEscapeOption)
		{
			if (stringEscapeOption == ODataStringEscapeOption.EscapeNonAscii || value <= '\u007f')
			{
				string text = JsonValueUtils.SpecialCharToEscapedStringMap[(int)value];
				if (text != null)
				{
					writer.Write(text);
					return;
				}
			}
			writer.Write(value);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000430F7 File Offset: 0x000412F7
		internal static void WriteValue(TextWriter writer, bool value)
		{
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0004310E File Offset: 0x0004130E
		internal static void WriteValue(TextWriter writer, int value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00043122 File Offset: 0x00041322
		internal static void WriteValue(TextWriter writer, float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(JsonValueUtils.ODataNumberFormatInfo));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00043153 File Offset: 0x00041353
		internal static void WriteValue(TextWriter writer, short value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00043167 File Offset: 0x00041367
		internal static void WriteValue(TextWriter writer, long value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0004317C File Offset: 0x0004137C
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

		// Token: 0x06001796 RID: 6038 RVA: 0x000431CB File Offset: 0x000413CB
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000431E0 File Offset: 0x000413E0
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000431F4 File Offset: 0x000413F4
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

		// Token: 0x06001799 RID: 6041 RVA: 0x00043275 File Offset: 0x00041475
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			JsonValueUtils.WriteQuoted(writer, EdmValueWriter.DurationAsXml(value));
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00043283 File Offset: 0x00041483
		internal static void WriteValue(TextWriter writer, TimeOfDay value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00043298 File Offset: 0x00041498
		internal static void WriteValue(TextWriter writer, Date value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000432AD File Offset: 0x000414AD
		internal static void WriteValue(TextWriter writer, byte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000432C1 File Offset: 0x000414C1
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000432D5 File Offset: 0x000414D5
		internal static void WriteValue(TextWriter writer, string value, ODataStringEscapeOption stringEscapeOption, ref char[] buffer, ICharArrayPool arrayPool = null)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value, stringEscapeOption, ref buffer, arrayPool);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000432F1 File Offset: 0x000414F1
		internal static void WriteValue(TextWriter writer, byte[] value, ref char[] buffer, ICharArrayPool arrayPool = null)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			writer.Write('"');
			JsonValueUtils.WriteBinaryString(writer, value, ref buffer, arrayPool);
			writer.Write('"');
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0004331C File Offset: 0x0004151C
		internal static void WriteBinaryString(TextWriter writer, byte[] value, ref char[] buffer, ICharArrayPool arrayPool)
		{
			buffer = BufferUtils.InitializeBufferIfRequired(arrayPool, buffer);
			int num = buffer.Length;
			int num2 = num * 3 / 4;
			for (int i = 0; i < value.Length; i += num2)
			{
				int num3 = num2;
				if (i + num3 > value.Length)
				{
					num3 = value.Length - i;
				}
				int num4 = Convert.ToBase64CharArray(value, i, num3, buffer, 0);
				writer.Write(buffer, 0, num4);
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00043374 File Offset: 0x00041574
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString, ODataStringEscapeOption stringEscapeOption, ref char[] buffer, ICharArrayPool bufferPool = null)
		{
			writer.Write('"');
			JsonValueUtils.WriteEscapedJsonStringValue(writer, inputString, stringEscapeOption, ref buffer, bufferPool);
			writer.Write('"');
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00043394 File Offset: 0x00041594
		internal static void WriteEscapedJsonStringValue(TextWriter writer, string inputString, ODataStringEscapeOption stringEscapeOption, ref char[] buffer, ICharArrayPool bufferPool)
		{
			int num;
			if (!JsonValueUtils.CheckIfStringHasSpecialChars(inputString, stringEscapeOption, out num))
			{
				writer.Write(inputString);
				return;
			}
			int length = inputString.Length;
			buffer = BufferUtils.InitializeBufferIfRequired(bufferPool, buffer);
			int num2 = buffer.Length;
			int num3 = 0;
			int i = 0;
			while (i < num)
			{
				int num4 = num - i;
				if (num4 >= num2)
				{
					inputString.CopyTo(i, buffer, 0, num2);
					writer.Write(buffer, 0, num2);
					i += num2;
				}
				else
				{
					inputString.CopyTo(i, buffer, 0, num4);
					num3 = num4;
					i += num4;
				}
			}
			while (i < length)
			{
				num3 = JsonValueUtils.EscapeAndWriteCharToBuffer(writer, inputString[i], buffer, num3, stringEscapeOption);
				i++;
			}
			if (num3 > 0)
			{
				writer.Write(buffer, 0, num3);
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00043448 File Offset: 0x00041648
		internal static void WriteEscapedCharArray(TextWriter writer, char[] inputArray, int inputArrayOffset, int inputArrayCount, ODataStringEscapeOption stringEscapeOption, ref char[] buffer, ICharArrayPool bufferPool)
		{
			int num = 0;
			buffer = BufferUtils.InitializeBufferIfRequired(bufferPool, buffer);
			while (inputArrayOffset < inputArrayCount)
			{
				num = JsonValueUtils.EscapeAndWriteCharToBuffer(writer, inputArray[inputArrayOffset], buffer, num, stringEscapeOption);
				inputArrayOffset++;
			}
			if (num > 0)
			{
				writer.Write(buffer, 0, num);
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0004348E File Offset: 0x0004168E
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x000434A0 File Offset: 0x000416A0
		internal static string GetEscapedJsonString(string inputString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int length = inputString.Length;
			int num2;
			for (int i = 0; i < length; i++)
			{
				char c = inputString[i];
				if (JsonValueUtils.SpecialCharToEscapedStringMap[(int)c] != null)
				{
					num2 = i - num;
					if (num2 > 0)
					{
						stringBuilder.Append(inputString.Substring(num, num2));
					}
					stringBuilder.Append(JsonValueUtils.SpecialCharToEscapedStringMap[(int)c]);
					num = i + 1;
				}
			}
			num2 = length - num;
			if (num2 > 0)
			{
				stringBuilder.Append(inputString.Substring(num, num2));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0004352C File Offset: 0x0004172C
		private static int EscapeAndWriteCharToBuffer(TextWriter writer, char character, char[] buffer, int bufferIndex, ODataStringEscapeOption stringEscapeOption)
		{
			int num = buffer.Length;
			string text = null;
			if (stringEscapeOption == ODataStringEscapeOption.EscapeNonAscii || character <= '\u007f')
			{
				text = JsonValueUtils.SpecialCharToEscapedStringMap[(int)character];
			}
			if (text == null)
			{
				buffer[bufferIndex] = character;
				bufferIndex++;
			}
			else
			{
				int length = text.Length;
				if (bufferIndex + length > num)
				{
					writer.Write(buffer, 0, bufferIndex);
					bufferIndex = 0;
				}
				text.CopyTo(0, buffer, bufferIndex, length);
				bufferIndex += length;
			}
			if (bufferIndex >= num)
			{
				writer.Write(buffer, 0, bufferIndex);
				bufferIndex = 0;
			}
			return bufferIndex;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00043598 File Offset: 0x00041798
		private static bool CheckIfStringHasSpecialChars(string inputString, ODataStringEscapeOption stringEscapeOption, out int firstIndex)
		{
			firstIndex = -1;
			int length = inputString.Length;
			for (int i = 0; i < length; i++)
			{
				char c = inputString[i];
				if ((stringEscapeOption != ODataStringEscapeOption.EscapeOnlyControls || c < '\u007f') && JsonValueUtils.SpecialCharToEscapedStringMap[(int)c] != null)
				{
					firstIndex = i;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x000435DC File Offset: 0x000417DC
		private static NumberFormatInfo InitializeODataNumberFormatInfo()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
			numberFormatInfo.PositiveInfinitySymbol = JsonValueUtils.ODataJsonPositiveInfinitySymbol;
			numberFormatInfo.NegativeInfinitySymbol = JsonValueUtils.ODataJsonNegativeInfinitySymbol;
			return numberFormatInfo;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00043615 File Offset: 0x00041815
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0004362E File Offset: 0x0004182E
		private static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00043640 File Offset: 0x00041840
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

		// Token: 0x04000AA2 RID: 2722
		internal static readonly string ODataJsonPositiveInfinitySymbol = "INF";

		// Token: 0x04000AA3 RID: 2723
		internal static readonly string ODataJsonNegativeInfinitySymbol = "-INF";

		// Token: 0x04000AA4 RID: 2724
		internal static readonly NumberFormatInfo ODataNumberFormatInfo = JsonValueUtils.InitializeODataNumberFormatInfo();

		// Token: 0x04000AA5 RID: 2725
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

		// Token: 0x04000AA6 RID: 2726
		private static readonly char[] DoubleIndicatingCharacters = new char[] { '.', 'e', 'E' };

		// Token: 0x04000AA7 RID: 2727
		private static readonly string[] SpecialCharToEscapedStringMap = JsonValueUtils.CreateSpecialCharToEscapedStringMap();
	}
}
