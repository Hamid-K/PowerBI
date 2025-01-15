using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000281 RID: 641
	internal static class ODataAtomConvert
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x0004A34F File Offset: 0x0004854F
		internal static string ToString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004A35F File Offset: 0x0004855F
		internal static string ToString(byte b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0004A367 File Offset: 0x00048567
		internal static string ToString(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004A36F File Offset: 0x0004856F
		internal static string ToString(this DateTime dt)
		{
			return PlatformHelper.ConvertDateTimeToString(dt);
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004A377 File Offset: 0x00048577
		internal static string ToString(DateTimeOffset dateTime)
		{
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004A380 File Offset: 0x00048580
		internal static string ToAtomString(DateTimeOffset dateTime)
		{
			if (dateTime.Offset == ODataAtomConvert.zeroOffset)
			{
				return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			}
			return dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0004A3CB File Offset: 0x000485CB
		internal static string ToString(this TimeSpan ts)
		{
			return XmlConvert.ToString(ts);
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004A3D3 File Offset: 0x000485D3
		internal static string ToString(this double d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004A3DB File Offset: 0x000485DB
		internal static string ToString(this short i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004A3E3 File Offset: 0x000485E3
		internal static string ToString(this int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004A3EB File Offset: 0x000485EB
		internal static string ToString(this long i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004A3F3 File Offset: 0x000485F3
		internal static string ToString(this sbyte sb)
		{
			return XmlConvert.ToString(sb);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004A3FB File Offset: 0x000485FB
		internal static string ToString(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0004A403 File Offset: 0x00048603
		internal static string ToString(this float s)
		{
			return XmlConvert.ToString(s);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0004A40B File Offset: 0x0004860B
		internal static string ToString(this Guid guid)
		{
			return XmlConvert.ToString(guid);
		}

		// Token: 0x040007C3 RID: 1987
		private static readonly TimeSpan zeroOffset = new TimeSpan(0, 0, 0);
	}
}
