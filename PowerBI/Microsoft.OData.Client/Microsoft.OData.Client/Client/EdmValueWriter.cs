using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client
{
	// Token: 0x02000008 RID: 8
	internal static class EdmValueWriter
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002DF3 File Offset: 0x00000FF3
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002DF8 File Offset: 0x00000FF8
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

		// Token: 0x0600001F RID: 31 RVA: 0x00002E47 File Offset: 0x00001047
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002E4F File Offset: 0x0000104F
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002E5D File Offset: 0x0000105D
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002E65 File Offset: 0x00001065
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002E73 File Offset: 0x00001073
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002E7B File Offset: 0x0000107B
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002E83 File Offset: 0x00001083
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002E8B File Offset: 0x0000108B
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E94 File Offset: 0x00001094
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002EAC File Offset: 0x000010AC
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002EC8 File Offset: 0x000010C8
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002EE4 File Offset: 0x000010E4
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002EEC File Offset: 0x000010EC
		internal static string UriAsXml(Uri uri)
		{
			return uri.OriginalString;
		}

		// Token: 0x0400001D RID: 29
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
