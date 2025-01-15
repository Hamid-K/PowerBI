using System;
using System.Xml;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000151 RID: 337
	internal static class EdmValueWriter
	{
		// Token: 0x06000873 RID: 2163 RVA: 0x00016778 File Offset: 0x00014978
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
			case EdmValueKind.Duration:
				return EdmValueWriter.DurationAsXml(((IEdmDurationValue)v).Value);
			case EdmValueKind.Date:
				return EdmValueWriter.DateAsXml(((IEdmDateValue)v).Value);
			case EdmValueKind.TimeOfDay:
				return EdmValueWriter.TimeOfDayAsXml(((IEdmTimeOfDayValue)v).Value);
			}
			throw new NotSupportedException(Strings.ValueWriter_NonSerializableValue(v.ValueKind));
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0001250B File Offset: 0x0001070B
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000168A8 File Offset: 0x00014AA8
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

		// Token: 0x06000876 RID: 2166 RVA: 0x000168F7 File Offset: 0x00014AF7
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000168FF File Offset: 0x00014AFF
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001690D File Offset: 0x00014B0D
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00016915 File Offset: 0x00014B15
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00016923 File Offset: 0x00014B23
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001692B File Offset: 0x00014B2B
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00016933 File Offset: 0x00014B33
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001693B File Offset: 0x00014B3B
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00016944 File Offset: 0x00014B44
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001695C File Offset: 0x00014B5C
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00016978 File Offset: 0x00014B78
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00016994 File Offset: 0x00014B94
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0001699C File Offset: 0x00014B9C
		internal static string UriAsXml(Uri uri)
		{
			return uri.OriginalString;
		}

		// Token: 0x040004EA RID: 1258
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
