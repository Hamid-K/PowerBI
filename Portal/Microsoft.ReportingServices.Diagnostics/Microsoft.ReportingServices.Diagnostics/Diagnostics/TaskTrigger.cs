using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000076 RID: 118
	internal class TaskTrigger
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000FEE5 File Offset: 0x0000E0E5
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000FEED File Offset: 0x0000E0ED
		public BaseTriggerData TriggerData
		{
			get
			{
				return this.m_triggerData;
			}
			set
			{
				this.m_triggerData = value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000FEF6 File Offset: 0x0000E0F6
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0000FF00 File Offset: 0x0000E100
		public DateTime StartDate
		{
			get
			{
				return this.m_startDate;
			}
			set
			{
				this.m_startDate = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0, 0, DateTimeKind.Local);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000FF3E File Offset: 0x0000E13E
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0000FF46 File Offset: 0x0000E146
		public DateTime EndDate
		{
			get
			{
				return this.m_endDate;
			}
			set
			{
				this.m_endDate = new DateTime(value.Year, value.Month, value.Day);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000FF68 File Offset: 0x0000E168
		public RecurrenceType RecurrenceType
		{
			get
			{
				RecurrenceType recurrenceType = RecurrenceType.Once;
				if (this.TriggerData is Daily)
				{
					recurrenceType = RecurrenceType.Daily;
				}
				else if (this.TriggerData is Minutes)
				{
					recurrenceType = RecurrenceType.Minutes;
				}
				else if (this.TriggerData is Weekly)
				{
					recurrenceType = RecurrenceType.Weekly;
				}
				else if (this.TriggerData is Monthly)
				{
					recurrenceType = RecurrenceType.MonthlyDate;
				}
				else if (this.TriggerData is MonthlyDOW)
				{
					recurrenceType = RecurrenceType.MonthlyDOW;
				}
				return recurrenceType;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000FFCB File Offset: 0x0000E1CB
		public string ScheduleDescription
		{
			get
			{
				return this.ComputeDescription(0);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
		public string ComputeDescription(int utcOffsetInMinutes = 0)
		{
			ScheduleDescription scheduleDescription = new ScheduleDescription(CultureInfo.CurrentCulture);
			DateTime dateTime = this.StartDate;
			DateTime dateTime2 = this.EndDate;
			if (utcOffsetInMinutes != 0)
			{
				TimeSpan timeSpan = TimeSpan.FromMinutes((double)utcOffsetInMinutes);
				if (dateTime > DateTime.MinValue)
				{
					dateTime = dateTime.ToUniversalTime() - timeSpan;
				}
				if (dateTime2 > DateTime.MinValue)
				{
					dateTime2 = dateTime2.ToUniversalTime() - timeSpan;
				}
			}
			switch (this.RecurrenceType)
			{
			case RecurrenceType.Once:
				return scheduleDescription.OnceDescription(dateTime, dateTime2);
			case RecurrenceType.Minutes:
			{
				Minutes minutes = (Minutes)this.TriggerData;
				return scheduleDescription.MinutesDescription(dateTime, dateTime2, minutes.MinutesInterval);
			}
			case RecurrenceType.Daily:
			{
				Daily daily = (Daily)this.TriggerData;
				return scheduleDescription.DailyDescription(dateTime, dateTime2, daily.DaysInterval);
			}
			case RecurrenceType.Weekly:
			{
				Weekly weekly = (Weekly)this.TriggerData;
				return scheduleDescription.WeeklyDescription(dateTime, dateTime2, weekly.DaysOfWeek, weekly.WeeksInterval);
			}
			case RecurrenceType.MonthlyDate:
			{
				Monthly monthly = (Monthly)this.TriggerData;
				return scheduleDescription.MonthlyDescription(dateTime, dateTime2, Monthly.GetDayRange(monthly.DaysOfMonth), monthly.Months);
			}
			case RecurrenceType.MonthlyDOW:
			{
				MonthlyDOW monthlyDOW = (MonthlyDOW)this.TriggerData;
				return scheduleDescription.MonthlyDOWDescription(dateTime, dateTime2, monthlyDOW.DaysOfWeek, (WeeksOfMonth)monthlyDOW.Week, monthlyDOW.Months);
			}
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00010129 File Offset: 0x0000E329
		public void SetToOnce()
		{
			this.TriggerData = null;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010132 File Offset: 0x0000E332
		public void SetToMinutes(int minutesInterval)
		{
			if (minutesInterval < 1)
			{
				throw new InvalidElementException("MinutesInterval");
			}
			this.TriggerData = new Minutes(minutesInterval);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001014F File Offset: 0x0000E34F
		public void SetToDaily(long daysInterval)
		{
			if (daysInterval < 1L)
			{
				throw new InvalidElementException("DaysInterval");
			}
			this.TriggerData = new Daily(daysInterval);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001016D File Offset: 0x0000E36D
		public void SetToWeekly(long weeksInterval, uint daysOfWeek)
		{
			if (weeksInterval <= 0L)
			{
				throw new InvalidElementException("WeeksInterval");
			}
			if (daysOfWeek == 0U)
			{
				throw new InvalidElementException("DaysOfWeek");
			}
			this.TriggerData = new Weekly(weeksInterval, daysOfWeek);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001019A File Offset: 0x0000E39A
		public void SetToMonthly(uint daysOfMonth, uint months)
		{
			if (months == 0U)
			{
				throw new InvalidElementException("Days");
			}
			this.TriggerData = new Monthly(daysOfMonth, months);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000101B7 File Offset: 0x0000E3B7
		public void SetToMonthlyDOW(uint week, uint daysOfWeek, uint months)
		{
			if (week == 0U)
			{
				throw new InvalidElementException("WhichWeek");
			}
			if (months == 0U)
			{
				throw new InvalidElementException("MonthsOfYear");
			}
			this.TriggerData = new MonthlyDOW(week, daysOfWeek, months);
		}

		// Token: 0x04000365 RID: 869
		private DateTime m_startDate = DateTime.MinValue;

		// Token: 0x04000366 RID: 870
		private DateTime m_endDate = DateTime.MinValue;

		// Token: 0x04000367 RID: 871
		private BaseTriggerData m_triggerData;
	}
}
