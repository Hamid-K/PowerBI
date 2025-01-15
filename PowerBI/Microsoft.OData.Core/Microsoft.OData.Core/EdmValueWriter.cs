using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x020000ED RID: 237
	internal static class EdmValueWriter
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001D267 File Offset: 0x0001B467
		internal static string StringAsXml(string s)
		{
			return s;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001D26C File Offset: 0x0001B46C
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

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001D2BB File Offset: 0x0001B4BB
		internal static string BooleanAsXml(bool b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001D2C3 File Offset: 0x0001B4C3
		internal static string BooleanAsXml(bool? b)
		{
			return EdmValueWriter.BooleanAsXml(b.Value);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00004494 File Offset: 0x00002694
		internal static string IntAsXml(int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001D2D1 File Offset: 0x0001B4D1
		internal static string IntAsXml(int? i)
		{
			return EdmValueWriter.IntAsXml(i.Value);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000449C File Offset: 0x0000269C
		internal static string LongAsXml(long l)
		{
			return XmlConvert.ToString(l);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000448C File Offset: 0x0000268C
		internal static string FloatAsXml(double f)
		{
			return XmlConvert.ToString(f);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x00004474 File Offset: 0x00002674
		internal static string DecimalAsXml(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0001D2DF File Offset: 0x0001B4DF
		internal static string DurationAsXml(TimeSpan d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		internal static string DateTimeOffsetAsXml(DateTimeOffset d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0001D300 File Offset: 0x0001B500
		internal static string DateAsXml(Date d)
		{
			return d.ToString();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0001D31C File Offset: 0x0001B51C
		internal static string TimeOfDayAsXml(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000044BC File Offset: 0x000026BC
		internal static string GuidAsXml(Guid g)
		{
			return XmlConvert.ToString(g);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0001D338 File Offset: 0x0001B538
		internal static string UriAsXml(Uri uri)
		{
			return uri.OriginalString;
		}

		// Token: 0x040003DD RID: 989
		private static char[] Hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};
	}
}
