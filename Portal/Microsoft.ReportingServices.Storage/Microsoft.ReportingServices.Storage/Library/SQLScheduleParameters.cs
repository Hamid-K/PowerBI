using System;
using System.Collections;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200001B RID: 27
	internal sealed class SQLScheduleParameters : ArrayList
	{
		// Token: 0x17000042 RID: 66
		public SQLScheduleParameter this[int index]
		{
			get
			{
				return base[index] as SQLScheduleParameter;
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006D14 File Offset: 0x00004F14
		private void Assert(bool condition, string message)
		{
			RSTrace.ScheduleTracer.Assert(condition, message);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006D24 File Offset: 0x00004F24
		public void AddFrequencyType(int type)
		{
			this.Assert(type == 1 || type == 2 || type == 4 || type == 8 || type == 16 || type == 32 || type == 64 || type == 128, "Incorrect frequency type for a SQL schedule");
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_type",
				Type = SqlDbType.Int,
				Value = type
			});
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006D94 File Offset: 0x00004F94
		public void AddFrequencyInterval(int interval)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_interval",
				Type = SqlDbType.Int,
				Value = interval
			});
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public void AddFrequency_SubDay_Type(int type)
		{
			this.Assert(type == 1 || type == 4 || type == 8, "Incorrect frequency sub day type for a SQL schedule");
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_subday_type",
				Type = SqlDbType.Int,
				Value = type
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006E24 File Offset: 0x00005024
		public void Add_Frequency_SubDay_Interval(int interval)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_subday_interval",
				Type = SqlDbType.Int,
				Value = interval
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006E60 File Offset: 0x00005060
		public void Add_Frequency_Relative_Interval(int interval)
		{
			this.Assert(interval == 1 || interval == 2 || interval == 4 || interval == 8 || interval == 16, "Incorrect frequency relative interval for a SQL schedule");
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_relative_interval",
				Type = SqlDbType.Int,
				Value = interval
			});
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006EBC File Offset: 0x000050BC
		public void Add_Frequency_Recurrence_factor(int factor)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_recurrence_factor",
				Type = SqlDbType.Int,
				Value = factor
			});
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006EF8 File Offset: 0x000050F8
		public void Add_Active_Start_Date(DateTime date)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_start_date",
				Type = SqlDbType.Int,
				Value = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006F3C File Offset: 0x0000513C
		public void Add_Active_End_Date(DateTime date)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_end_date",
				Type = SqlDbType.Int,
				Value = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00006F80 File Offset: 0x00005180
		public void Add_Active_Start_Time(DateTime time)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_start_time",
				Type = SqlDbType.Int,
				Value = time.ToString("HHmmss", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006FC4 File Offset: 0x000051C4
		public void Add_Active_End_Time(DateTime time)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_end_time",
				Type = SqlDbType.Int,
				Value = time.ToString("HHmmss", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00007008 File Offset: 0x00005208
		public DateTime GetStartMonthDate(Months month, DateTime date)
		{
			int num = 0;
			DateTime dateTime = DateTime.MinValue;
			if (month <= Months.July)
			{
				if (month <= Months.April)
				{
					switch (month)
					{
					case Months.January:
						num = 1;
						break;
					case Months.February:
						num = 2;
						break;
					case (Months)3:
						break;
					case Months.March:
						num = 3;
						break;
					default:
						if (month == Months.April)
						{
							num = 4;
						}
						break;
					}
				}
				else if (month != Months.May)
				{
					if (month != Months.June)
					{
						if (month == Months.July)
						{
							num = 7;
						}
					}
					else
					{
						num = 6;
					}
				}
				else
				{
					num = 5;
				}
			}
			else if (month <= Months.September)
			{
				if (month != Months.August)
				{
					if (month == Months.September)
					{
						num = 9;
					}
				}
				else
				{
					num = 8;
				}
			}
			else if (month != Months.October)
			{
				if (month != Months.November)
				{
					if (month == Months.December)
					{
						num = 12;
					}
				}
				else
				{
					num = 11;
				}
			}
			else
			{
				num = 10;
			}
			if (num == date.Month)
			{
				dateTime = date;
			}
			else
			{
				dateTime = new DateTime(date.Year, num, 1);
				if (num < date.Month)
				{
					dateTime = dateTime.AddYears(1);
				}
			}
			return dateTime;
		}
	}
}
