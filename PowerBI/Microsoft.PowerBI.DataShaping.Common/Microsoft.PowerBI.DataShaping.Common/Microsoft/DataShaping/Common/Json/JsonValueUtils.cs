using System;
using System.Globalization;
using System.IO;
using System.Xml;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.DataShaping.Common.Json
{
	// Token: 0x0200001F RID: 31
	internal static class JsonValueUtils
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00004147 File Offset: 0x00002347
		internal static void WriteBoolean(TextWriter writer, bool value)
		{
			writer.Write(JsonValueUtils.ToString(value));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004155 File Offset: 0x00002355
		internal static string ToString(bool value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000415D File Offset: 0x0000235D
		internal static void WriteString(TextWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004175 File Offset: 0x00002375
		internal static void WriteJsonEncodedString(TextWriter writer, string value, bool shouldBeQuoted)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			if (shouldBeQuoted)
			{
				writer.Write('"');
			}
			writer.Write(value);
			if (shouldBeQuoted)
			{
				writer.Write('"');
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000041A3 File Offset: 0x000023A3
		internal static void WriteInt(TextWriter writer, int value)
		{
			writer.Write(JsonValueUtils.ToString(value));
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000041B1 File Offset: 0x000023B1
		internal static string ToString(int value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000041B9 File Offset: 0x000023B9
		internal static void WriteLong(TextWriter writer, long value)
		{
			writer.Write(JsonValueUtils.ToString(value));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000041C7 File Offset: 0x000023C7
		internal static string ToString(long value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000041CF File Offset: 0x000023CF
		internal static void WriteDouble(TextWriter writer, double value)
		{
			writer.Write(JsonValueUtils.ToString(value));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000041DD File Offset: 0x000023DD
		internal static string ToString(double value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000041E5 File Offset: 0x000023E5
		internal static void WriteDecimal(TextWriter writer, decimal value)
		{
			writer.Write(JsonValueUtils.ToString(value));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000041F3 File Offset: 0x000023F3
		internal static string ToString(decimal value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000041FB File Offset: 0x000023FB
		internal static void WriteDateTimeOffset(TextWriter writer, DateTimeOffset value)
		{
			writer.Write(JsonValueUtils.ToString(value));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004209 File Offset: 0x00002409
		internal static string ToString(DateTimeOffset value)
		{
			return XmlConvert.ToString(value);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004211 File Offset: 0x00002411
		internal static string ToTypeEncodedString(object value)
		{
			return PrimitiveValueEncoding.ToTypeEncodedString(value);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004219 File Offset: 0x00002419
		internal static string ToSimpleEncodedString(object value)
		{
			return PrimitiveValueEncoding.ToSimpleEncodedString(value);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004224 File Offset: 0x00002424
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString)
		{
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
						goto IL_012A;
					case '\t':
						writer.Write("\\t");
						goto IL_012A;
					case '\n':
						writer.Write("\\n");
						goto IL_012A;
					case '\v':
						break;
					case '\f':
						writer.Write("\\f");
						goto IL_012A;
					case '\r':
						writer.Write("\\r");
						goto IL_012A;
					default:
						if (c == '"')
						{
							writer.Write("\\\"");
							goto IL_012A;
						}
						if (c == '\\')
						{
							writer.Write("\\\\");
							goto IL_012A;
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
				IL_012A:;
			}
			if (num2 > 0)
			{
				writer.Write(inputString.Substring(num, num2));
			}
			writer.Write('"');
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004385 File Offset: 0x00002585
		private static string EscapeChar(char c)
		{
			return string.Format(CultureInfo.InvariantCulture, "\\u{0:x4}", (int)c);
		}
	}
}
