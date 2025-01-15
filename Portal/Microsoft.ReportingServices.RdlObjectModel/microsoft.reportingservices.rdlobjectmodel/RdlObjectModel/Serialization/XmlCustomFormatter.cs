using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel.Serialization
{
	// Token: 0x020002E8 RID: 744
	internal class XmlCustomFormatter
	{
		// Token: 0x060016D4 RID: 5844 RVA: 0x0003603A File Offset: 0x0003423A
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00036047 File Offset: 0x00034247
		internal static string FromTime(DateTime value)
		{
			return XmlConvert.ToString(value, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00036054 File Offset: 0x00034254
		internal static string FromDateTime(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00036061 File Offset: 0x00034261
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00036069 File Offset: 0x00034269
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.DecodeName(nmToken);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00036071 File Offset: 0x00034271
		internal static string FromByteArrayBase64(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			return Convert.ToBase64String(value);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00036080 File Offset: 0x00034280
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
				return XmlConvert.ToString(num);
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00036107 File Offset: 0x00034307
		internal static DateTime ToDateTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00036114 File Offset: 0x00034314
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x0003611D File Offset: 0x0003431D
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x0003612A File Offset: 0x0003432A
		internal static DateTime ToTime(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allTimeFormats);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00036137 File Offset: 0x00034337
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0003613F File Offset: 0x0003433F
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.EncodeNmToken(value);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00036147 File Offset: 0x00034347
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

		// Token: 0x060016E2 RID: 5858 RVA: 0x0003616C File Offset: 0x0003436C
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

		// Token: 0x04000711 RID: 1809
		private static readonly string[] allDateTimeFormats = new string[]
		{
			"yyyy", "---dd", "--MM-dd", "yyyy-MM", "yyyy-MM-dd", "HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff",
			"HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz", "HH:mm:ss.ffzzzzzz", "HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz", "HH:mm:ss.ffffffzzzzzz",
			"HH:mm:ss.fffffffzzzzzz", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.f", "yyyy-MM-ddTHH:mm:ss.ff", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.ffff", "yyyy-MM-ddTHH:mm:ss.fffff", "yyyy-MM-ddTHH:mm:ss.ffffff", "yyyy-MM-ddTHH:mm:ss.fffffff", "yyyy-MM-ddTHH:mm:sszzzzzz",
			"yyyy-MM-ddTHH:mm:ss.fzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz"
		};

		// Token: 0x04000712 RID: 1810
		private static readonly string[] allDateFormats = new string[] { "yyyy-MM-dd" };

		// Token: 0x04000713 RID: 1811
		private static readonly string[] allTimeFormats = new string[]
		{
			"HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz",
			"HH:mm:ss.ffzzzzzz", "HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz", "HH:mm:ss.ffffffzzzzzz", "HH:mm:ss.fffffffzzzzzz"
		};
	}
}
