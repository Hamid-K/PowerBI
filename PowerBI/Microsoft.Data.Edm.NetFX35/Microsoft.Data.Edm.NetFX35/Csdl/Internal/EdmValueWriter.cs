using System;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Csdl.Internal
{
	// Token: 0x02000007 RID: 7
	internal static class EdmValueWriter
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002640 File Offset: 0x00000840
		internal static string PrimitiveValueAsXml(IEdmPrimitiveValue v)
		{
			switch (v.ValueKind)
			{
			case EdmValueKind.Binary:
				return EdmValueWriter.BinaryAsXml(((IEdmBinaryValue)v).Value);
			case EdmValueKind.Boolean:
				return EdmValueWriter.BooleanAsXml(((IEdmBooleanValue)v).Value);
			case EdmValueKind.DateTimeOffset:
				return EdmValueWriter.DateTimeOffsetAsXml(((IEdmDateTimeOffsetValue)v).Value);
			case EdmValueKind.DateTime:
				return EdmValueWriter.DateTimeAsXml(((IEdmDateTimeValue)v).Value);
			case EdmValueKind.Decimal:
				return EdmValueWriter.DecimalAsXml(((IEdmDecimalValue)v).Value);
			case EdmValueKind.Floating:
				return EdmValueWriter.FloatAsXml(((IEdmFloatingValue)v).Value);
			case EdmValueKind.Guid:
				return EdmValueWriter.GuidAsXml(((IEdmGuidValue)v).Value);
			case EdmValueKind.Integer:
				return EdmValueWriter.LongAsXml(((IEdmIntegerValue)v).Value);
			case EdmValueKind.String:
				return EdmValueWriter.StringAsXml(((IEdmStringValue)v).Value);
			case EdmValueKind.Time:
				return EdmValueWriter.TimeAsXml(((IEdmTimeValue)v).Value);
			}
			throw new NotSupportedException(Strings.ValueWriter_NonSerializableValue(v.ValueKind));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002756 File Offset: 0x00000956
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000275C File Offset: 0x0000095C
		internal static string BinaryAsXml(byte[] binary)
		{
			char[] array = new char[binary.Length * 2];
			for (int i = 0; i < binary.Length; i++)
			{
				array[i << 1] = EdmValueWriter.Hex[binary[i] >> 4];
				array[(i << 1) | 1] = EdmValueWriter.Hex[(int)(binary[i] & 15)];
			}
			return new string(array);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027AB File Offset: 0x000009AB
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027B3 File Offset: 0x000009B3
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027C1 File Offset: 0x000009C1
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027C9 File Offset: 0x000009C9
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000027D7 File Offset: 0x000009D7
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027DF File Offset: 0x000009DF
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027E7 File Offset: 0x000009E7
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027EF File Offset: 0x000009EF
		internal static string DateTimeAsXml(DateTime d)
		{
			return PlatformHelper.ConvertDateTimeToString(d);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000027F8 File Offset: 0x000009F8
		internal static string TimeAsXml(TimeSpan d)
		{
			return string.Concat(new string[]
			{
				d.Hours.ToString("00", CultureInfo.InvariantCulture),
				":",
				d.Minutes.ToString("00", CultureInfo.InvariantCulture),
				":",
				d.Seconds.ToString("00", CultureInfo.InvariantCulture),
				".",
				d.Milliseconds.ToString("000", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000289B File Offset: 0x00000A9B
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028A3 File Offset: 0x00000AA3
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x0400000A RID: 10
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
