using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000B8 RID: 184
	internal static class EdmValueWriter
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x00014BAB File Offset: 0x00012DAB
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00014BB0 File Offset: 0x00012DB0
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

		// Token: 0x06000729 RID: 1833 RVA: 0x00014BFF File Offset: 0x00012DFF
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00014C07 File Offset: 0x00012E07
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000F39D File Offset: 0x0000D59D
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00014C15 File Offset: 0x00012E15
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000F3A5 File Offset: 0x0000D5A5
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000F38D File Offset: 0x0000D58D
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000F375 File Offset: 0x0000D575
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00014C23 File Offset: 0x00012E23
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00014C2C File Offset: 0x00012E2C
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00014C44 File Offset: 0x00012E44
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00014C60 File Offset: 0x00012E60
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000F3C5 File Offset: 0x0000D5C5
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00014C7C File Offset: 0x00012E7C
		internal static string UriAsXml(Uri uri)
		{
			return uri.OriginalString;
		}

		// Token: 0x040002FC RID: 764
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
