using System;
using System.Collections;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000256 RID: 598
	internal sealed class SQLScheduleParameters : ArrayList
	{
		// Token: 0x17000641 RID: 1601
		public SQLScheduleParameter this[int index]
		{
			get
			{
				return base[index] as SQLScheduleParameter;
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000566E8 File Offset: 0x000548E8
		private void Assert(bool condition, string message)
		{
			RSTrace.ScheduleTracer.Assert(condition, message);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000566F8 File Offset: 0x000548F8
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

		// Token: 0x060015D0 RID: 5584 RVA: 0x00056768 File Offset: 0x00054968
		public void AddFrequencyInterval(int interval)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_interval",
				Type = SqlDbType.Int,
				Value = interval
			});
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000567A4 File Offset: 0x000549A4
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

		// Token: 0x060015D2 RID: 5586 RVA: 0x000567F8 File Offset: 0x000549F8
		public void Add_Frequency_SubDay_Interval(int interval)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_subday_interval",
				Type = SqlDbType.Int,
				Value = interval
			});
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00056834 File Offset: 0x00054A34
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

		// Token: 0x060015D4 RID: 5588 RVA: 0x00056890 File Offset: 0x00054A90
		public void Add_Frequency_Recurrence_factor(int factor)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@freq_recurrence_factor",
				Type = SqlDbType.Int,
				Value = factor
			});
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x000568CC File Offset: 0x00054ACC
		public void Add_Active_Start_Date(DateTime date)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_start_date",
				Type = SqlDbType.Int,
				Value = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00056910 File Offset: 0x00054B10
		public void Add_Active_End_Date(DateTime date)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_end_date",
				Type = SqlDbType.Int,
				Value = date.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00056954 File Offset: 0x00054B54
		public void Add_Active_Start_Time(DateTime time)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_start_time",
				Type = SqlDbType.Int,
				Value = time.ToString("HHmmss", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00056998 File Offset: 0x00054B98
		public void Add_Active_End_Time(DateTime time)
		{
			this.Add(new SQLScheduleParameter
			{
				Name = "@active_end_time",
				Type = SqlDbType.Int,
				Value = time.ToString("HHmmss", CultureInfo.InvariantCulture)
			});
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x000569DC File Offset: 0x00054BDC
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
