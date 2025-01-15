using System;
using System.Globalization;
using System.Xml;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x0200003D RID: 61
	internal static class ODataAtomConvert
	{
		// Token: 0x06000235 RID: 565 RVA: 0x0000771F File Offset: 0x0000591F
		internal static string ToString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000772F File Offset: 0x0000592F
		internal static string ToString(byte b)
		{
			return XmlConvert.ToString(b);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007737 File Offset: 0x00005937
		internal static string ToString(decimal d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000773F File Offset: 0x0000593F
		internal static string ToString(DateTimeOffset dateTime)
		{
			return XmlConvert.ToString(dateTime);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007748 File Offset: 0x00005948
		internal static string ToAtomString(DateTimeOffset dateTime)
		{
			if (dateTime.Offset == ODataAtomConvert.zeroOffset)
			{
				return dateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
			}
			return dateTime.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007793 File Offset: 0x00005993
		internal static string ToString(this TimeSpan ts)
		{
			return EdmValueWriter.DurationAsXml(ts);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000779B File Offset: 0x0000599B
		internal static string ToString(this double d)
		{
			return XmlConvert.ToString(d);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000077A3 File Offset: 0x000059A3
		internal static string ToString(this short i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000077AB File Offset: 0x000059AB
		internal static string ToString(this int i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000077B3 File Offset: 0x000059B3
		internal static string ToString(this long i)
		{
			return XmlConvert.ToString(i);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000077BB File Offset: 0x000059BB
		internal static string ToString(this sbyte sb)
		{
			return XmlConvert.ToString(sb);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000077C3 File Offset: 0x000059C3
		internal static string ToString(this byte[] bytes)
		{
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000077CB File Offset: 0x000059CB
		internal static string ToString(this float s)
		{
			return XmlConvert.ToString(s);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000077D3 File Offset: 0x000059D3
		internal static string ToString(this Guid guid)
		{
			return XmlConvert.ToString(guid);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000077DB File Offset: 0x000059DB
		internal static string ToString(Date date)
		{
			return date.ToString();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000077EA File Offset: 0x000059EA
		internal static string ToString(TimeOfDay time)
		{
			return time.ToString();
		}

		// Token: 0x0400013D RID: 317
		private static readonly TimeSpan zeroOffset = new TimeSpan(0, 0, 0);
	}
}
