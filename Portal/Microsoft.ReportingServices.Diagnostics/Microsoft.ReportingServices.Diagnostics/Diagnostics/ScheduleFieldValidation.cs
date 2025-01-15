using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000061 RID: 97
	internal static class ScheduleFieldValidation
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
		public static bool TryGetDayBitMap(string daysString, Months months, CultureInfo culture, out uint days)
		{
			days = 0U;
			int num = 0;
			char[] array = (daysString.Contains(",") ? ",".ToCharArray() : culture.TextInfo.ListSeparator.ToCharArray());
			char[] array2 = "-".ToCharArray();
			string[] array3 = daysString.Split(array);
			if (array3.Length == 0)
			{
				return false;
			}
			string[] array4 = array3;
			for (int i = 0; i < array4.Length; i++)
			{
				string[] array5 = array4[i].Split(array2);
				if (array5.Length == 0 || array5.Length > 2)
				{
					return false;
				}
				int num2 = 0;
				int num3 = 0;
				if (!int.TryParse(array5[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out num2))
				{
					return false;
				}
				if (num2 < 1 || num2 > 31)
				{
					return false;
				}
				num = ((num2 > num) ? num2 : num);
				if (array5.Length == 2)
				{
					if (!int.TryParse(array5[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num3))
					{
						return false;
					}
					if (num3 < 1 || num3 > 31 || num3 < num2)
					{
						return false;
					}
					num = ((num3 > num) ? num3 : num);
				}
				uint num4 = 1U;
				for (int j = 1; j < num2; j++)
				{
					num4 <<= 1;
				}
				days |= num4;
				for (int j = num2 + 1; j <= num3; j++)
				{
					num4 = 1U;
					for (int k = 1; k < j; k++)
					{
						num4 <<= 1;
					}
					days |= num4;
				}
			}
			int l = 1;
			int num5 = 0;
			while (l <= 2048)
			{
				if ((months & (Months)l) == (Months)l && num > ScheduleFieldValidation.m_daysInMonth[num5])
				{
					return false;
				}
				l *= 2;
				num5++;
			}
			return true;
		}

		// Token: 0x040002F6 RID: 758
		private static int[] m_daysInMonth = new int[]
		{
			31, 29, 31, 30, 31, 30, 31, 31, 30, 31,
			30, 31
		};
	}
}
