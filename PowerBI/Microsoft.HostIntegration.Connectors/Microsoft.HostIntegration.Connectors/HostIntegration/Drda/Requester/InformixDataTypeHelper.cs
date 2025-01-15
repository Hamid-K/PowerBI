using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x020009A0 RID: 2464
	public class InformixDataTypeHelper
	{
		// Token: 0x06004C71 RID: 19569 RVA: 0x00131E2C File Offset: 0x0013002C
		public static string GetInformixDatetimeTypeName(int size)
		{
			if (!InformixDataTypeHelper.SupportedDateTimeTypes.Contains(size))
			{
				return null;
			}
			int num = size % 16 + 1;
			int num2 = size % 256 / 16 + 1;
			InformixDateTimeInfo informixDateTimeInfo = null;
			InformixDateTimeInfo informixDateTimeInfo2 = null;
			if (InformixDataTypeHelper.InformixDateTimeParts.TryGetValue(num2, out informixDateTimeInfo) && InformixDataTypeHelper.InformixDateTimeParts.TryGetValue(num, out informixDateTimeInfo2))
			{
				return informixDateTimeInfo.baseName + " TO " + informixDateTimeInfo2.name;
			}
			return null;
		}

		// Token: 0x06004C72 RID: 19570 RVA: 0x00131E98 File Offset: 0x00130098
		public static string GetInformixFormatedDateTimeValue(int size, DateTime dt)
		{
			if (!InformixDataTypeHelper.SupportedDateTimeTypes.Contains(size))
			{
				return null;
			}
			int num = size % 16 + 1;
			int num2 = size % 256 / 16 + 1;
			InformixDateTimeInfo informixDateTimeInfo = null;
			InformixDateTimeInfo informixDateTimeInfo2 = null;
			if (InformixDataTypeHelper.InformixDateTimeParts.TryGetValue((num2 > 12) ? 12 : num2, out informixDateTimeInfo) && InformixDataTypeHelper.InformixDateTimeParts.TryGetValue(num, out informixDateTimeInfo2))
			{
				int num3 = (int)(informixDateTimeInfo2.endSubstr - informixDateTimeInfo.startSubstr + 1);
				string text = "yyyy-MM-dd HH:mm:ss.fffff".Substring((int)informixDateTimeInfo.startSubstr, num3);
				DateTime dateTime = dt;
				return dateTime.ToString(text).Trim();
			}
			return null;
		}

		// Token: 0x04003C56 RID: 15446
		private static readonly Dictionary<int, InformixDateTimeInfo> InformixDateTimeParts = new Dictionary<int, InformixDateTimeInfo>
		{
			{
				1,
				new InformixDateTimeInfo("YEAR", 1, 5, 0, 3)
			},
			{
				3,
				new InformixDateTimeInfo("MONTH", 5, 7, 5, 6)
			},
			{
				5,
				new InformixDateTimeInfo("DAY", 7, 9, 8, 9)
			},
			{
				7,
				new InformixDateTimeInfo("HOUR", 9, 11, 11, 12)
			},
			{
				9,
				new InformixDateTimeInfo("MINUTE", 11, 13, 14, 15)
			},
			{
				11,
				new InformixDateTimeInfo("SECOND", 13, 15, 17, 18)
			},
			{
				12,
				new InformixDateTimeInfo("FRACTION(1)", 15, 16, 19, 20)
			},
			{
				13,
				new InformixDateTimeInfo("FRACTION(2)", 16, 17, 19, 21)
			},
			{
				14,
				new InformixDateTimeInfo("FRACTION(3)", 17, 18, 19, 22)
			},
			{
				15,
				new InformixDateTimeInfo("FRACTION(4)", 18, 19, 19, 23)
			},
			{
				16,
				new InformixDateTimeInfo("FRACTION(5)", 19, 20, 19, 24)
			}
		};

		// Token: 0x04003C57 RID: 15447
		private static readonly HashSet<int> SupportedDateTimeTypes = new HashSet<int>
		{
			1538, 2052, 3080, 3594, 3851, 4108, 4365, 4622, 4879, 1060,
			1128, 1642, 1899, 2156, 2413, 2670, 2927
		};

		// Token: 0x04003C58 RID: 15448
		private const string InformixDateTimeFormatString = "yyyy-MM-dd HH:mm:ss.fffff";
	}
}
