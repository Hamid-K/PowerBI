using System;
using System.Xml;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl
{
	// Token: 0x02000143 RID: 323
	internal static class EdmValueWriter
	{
		// Token: 0x060007D0 RID: 2000 RVA: 0x00014B40 File Offset: 0x00012D40
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

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001402B File Offset: 0x0001222B
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00014C70 File Offset: 0x00012E70
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

		// Token: 0x060007D3 RID: 2003 RVA: 0x00014CBF File Offset: 0x00012EBF
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00014CC7 File Offset: 0x00012EC7
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00014CD5 File Offset: 0x00012ED5
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00014CDD File Offset: 0x00012EDD
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00014CEB File Offset: 0x00012EEB
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00014CF3 File Offset: 0x00012EF3
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00014CFB File Offset: 0x00012EFB
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00014D03 File Offset: 0x00012F03
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00014D0C File Offset: 0x00012F0C
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00014D24 File Offset: 0x00012F24
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00014D40 File Offset: 0x00012F40
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00014D5C File Offset: 0x00012F5C
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00014D64 File Offset: 0x00012F64
		internal static string UriAsXml(Uri uri)
		{
			return uri.OriginalString;
		}

		// Token: 0x04000481 RID: 1153
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
