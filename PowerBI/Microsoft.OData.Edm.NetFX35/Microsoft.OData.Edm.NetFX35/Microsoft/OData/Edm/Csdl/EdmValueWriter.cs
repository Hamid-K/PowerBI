using System;
using System.Xml;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000019 RID: 25
	internal static class EdmValueWriter
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003194 File Offset: 0x00001394
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

		// Token: 0x0600007C RID: 124 RVA: 0x000032C1 File Offset: 0x000014C1
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000032C4 File Offset: 0x000014C4
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

		// Token: 0x0600007E RID: 126 RVA: 0x00003313 File Offset: 0x00001513
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000331B File Offset: 0x0000151B
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003329 File Offset: 0x00001529
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003331 File Offset: 0x00001531
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000333F File Offset: 0x0000153F
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003347 File Offset: 0x00001547
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000334F File Offset: 0x0000154F
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003357 File Offset: 0x00001557
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003360 File Offset: 0x00001560
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003378 File Offset: 0x00001578
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003394 File Offset: 0x00001594
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000033B0 File Offset: 0x000015B0
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x04000029 RID: 41
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
