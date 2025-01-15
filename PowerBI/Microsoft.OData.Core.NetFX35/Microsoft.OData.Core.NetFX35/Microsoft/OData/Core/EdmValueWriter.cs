using System;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core
{
	// Token: 0x020002A3 RID: 675
	internal static class EdmValueWriter
	{
		// Token: 0x06001741 RID: 5953 RVA: 0x0004FEEB File Offset: 0x0004E0EB
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0004FEF0 File Offset: 0x0004E0F0
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

		// Token: 0x06001743 RID: 5955 RVA: 0x0004FF3F File Offset: 0x0004E13F
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0004FF47 File Offset: 0x0004E147
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0004FF55 File Offset: 0x0004E155
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0004FF5D File Offset: 0x0004E15D
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0004FF6B File Offset: 0x0004E16B
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0004FF73 File Offset: 0x0004E173
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0004FF7B File Offset: 0x0004E17B
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0004FF83 File Offset: 0x0004E183
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0004FF8C File Offset: 0x0004E18C
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0004FFA4 File Offset: 0x0004E1A4
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0004FFC0 File Offset: 0x0004E1C0
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0004FFDC File Offset: 0x0004E1DC
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x04000A0C RID: 2572
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
