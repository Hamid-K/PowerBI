using System;
using System.Xml;
using Microsoft.OData.Edm;

namespace Microsoft.OData
{
	// Token: 0x0200002C RID: 44
	internal static class ODataRawValueConverter
	{
		// Token: 0x0600017D RID: 381 RVA: 0x0000445C File Offset: 0x0000265C
		internal static string ToString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000446C File Offset: 0x0000266C
		internal static string ToString(byte b)
		{
			return XmlConvert.ToString((short)b);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004474 File Offset: 0x00002674
		internal static string ToString(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000447C File Offset: 0x0000267C
		internal static string ToString(DateTimeOffset dateTime)
		{
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004484 File Offset: 0x00002684
		internal static string ToString(this TimeSpan ts)
		{
			return EdmValueWriter.DurationAsXml(ts);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000448C File Offset: 0x0000268C
		internal static string ToString(this double d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000446C File Offset: 0x0000266C
		internal static string ToString(this short i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004494 File Offset: 0x00002694
		internal static string ToString(this int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000449C File Offset: 0x0000269C
		internal static string ToString(this long i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000044A4 File Offset: 0x000026A4
		internal static string ToString(this sbyte sb)
		{
			return XmlConvert.ToString(sb);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000044AC File Offset: 0x000026AC
		internal static string ToString(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000044B4 File Offset: 0x000026B4
		internal static string ToString(this float s)
		{
			return XmlConvert.ToString(s);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000044BC File Offset: 0x000026BC
		internal static string ToString(this Guid guid)
		{
			return XmlConvert.ToString(guid);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000044C4 File Offset: 0x000026C4
		internal static string ToString(Date date)
		{
			return date.ToString();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000044D3 File Offset: 0x000026D3
		internal static string ToString(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x0400007E RID: 126
		private const string RawValueTrueLiteral = "true";

		// Token: 0x0400007F RID: 127
		private const string RawValueFalseLiteral = "false";
	}
}
