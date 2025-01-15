using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200008E RID: 142
	internal static class ODataRawValueConverter
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x0000F35D File Offset: 0x0000D55D
		internal static string ToString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0000F36D File Offset: 0x0000D56D
		internal static string ToString(byte b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0000F375 File Offset: 0x0000D575
		internal static string ToString(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000F37D File Offset: 0x0000D57D
		internal static string ToString(DateTimeOffset dateTime)
		{
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0000F385 File Offset: 0x0000D585
		internal static string ToString(this TimeSpan ts)
		{
			return EdmValueWriter.DurationAsXml(ts);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0000F38D File Offset: 0x0000D58D
		internal static string ToString(this double d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0000F395 File Offset: 0x0000D595
		internal static string ToString(this short i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000F39D File Offset: 0x0000D59D
		internal static string ToString(this int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000F3A5 File Offset: 0x0000D5A5
		internal static string ToString(this long i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000F3AD File Offset: 0x0000D5AD
		internal static string ToString(this sbyte sb)
		{
			return XmlConvert.ToString(sb);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000F3B5 File Offset: 0x0000D5B5
		internal static string ToString(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000F3BD File Offset: 0x0000D5BD
		internal static string ToString(this float s)
		{
			return XmlConvert.ToString(s);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000F3C5 File Offset: 0x0000D5C5
		internal static string ToString(this Guid guid)
		{
			return XmlConvert.ToString(guid);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000F3CD File Offset: 0x0000D5CD
		internal static string ToString(Date date)
		{
			return date.ToString();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0000F3DC File Offset: 0x0000D5DC
		internal static string ToString(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x040002A3 RID: 675
		private const string RawValueTrueLiteral = "true";

		// Token: 0x040002A4 RID: 676
		private const string RawValueFalseLiteral = "false";
	}
}
