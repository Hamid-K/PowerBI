using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.Design.Serialization
{
	// Token: 0x02000398 RID: 920
	internal class XmlCustomFormatter
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x0007C528 File Offset: 0x0007A728
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0007C535 File Offset: 0x0007A735
		internal static string FromTime(DateTime value)
		{
			return XmlConvert.ToString(value, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0007C542 File Offset: 0x0007A742
		internal static string FromDateTime(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0007C54F File Offset: 0x0007A74F
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0007C557 File Offset: 0x0007A757
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.DecodeName(nmToken);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0007C55F File Offset: 0x0007A75F
		internal static string FromByteArrayBase64(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			return Convert.ToBase64String(value);
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0007C56C File Offset: 0x0007A76C
		internal static string FromEnum(long val, string[] vals, long[] ids)
		{
			long num = val;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == 0L)
				{
					num2 = i;
				}
				else
				{
					if (val == 0L)
					{
						break;
					}
					if ((ids[i] & num) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(vals[i]);
						val &= ~ids[i];
					}
				}
			}
			if (val != 0L)
			{
				return num.ToString(CultureInfo.InvariantCulture);
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0007C5F9 File Offset: 0x0007A7F9
		internal static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x0007C606 File Offset: 0x0007A806
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x0007C60F File Offset: 0x0007A80F
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x0007C61C File Offset: 0x0007A81C
		internal static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allTimeFormats);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0007C629 File Offset: 0x0007A829
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x0007C631 File Offset: 0x0007A831
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.EncodeNmToken(value);
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x0007C639 File Offset: 0x0007A839
		internal static byte[] ToByteArrayBase64(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return new byte[0];
			}
			return Convert.FromBase64String(value);
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x0007C660 File Offset: 0x0007A860
		internal static long ToEnum(string val, Hashtable vals, string typeName)
		{
			long num = 0L;
			string[] array = val.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = vals[array[i]];
				if (obj == null)
				{
					throw new Exception("UnknownConstant");
				}
				num |= (long)obj;
			}
			return num;
		}

		// Token: 0x04000CD8 RID: 3288
		private static string[] allDateTimeFormats = new string[]
		{
			"yyyy", "---dd", "--MM-dd", "yyyy-MM", "yyyy-MM-dd", "HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff",
			"HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz", "HH:mm:ss.ffzzzzzz", "HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz", "HH:mm:ss.ffffffzzzzzz",
			"HH:mm:ss.fffffffzzzzzz", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.f", "yyyy-MM-ddTHH:mm:ss.ff", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.ffff", "yyyy-MM-ddTHH:mm:ss.fffff", "yyyy-MM-ddTHH:mm:ss.ffffff", "yyyy-MM-ddTHH:mm:ss.fffffff", "yyyy-MM-ddTHH:mm:sszzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz"
		};

		// Token: 0x04000CD9 RID: 3289
		private static string[] allDateFormats = new string[] { "yyyy-MM-dd" };

		// Token: 0x04000CDA RID: 3290
		private static string[] allTimeFormats = new string[]
		{
			"HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz", "HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz", "HH:mm:ss.ffffffzzzzzz", "HH:mm:ss.fffffffzzzzzz"
		};
	}
}
