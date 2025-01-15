using System;
using System.Data;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000259 RID: 601
	internal sealed class SqlScheduleHelper
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x00056AEA File Offset: 0x00054CEA
		public SqlScheduleHelper(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00056AF9 File Offset: 0x00054CF9
		public Task Task
		{
			get
			{
				return this.m_task;
			}
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00056B04 File Offset: 0x00054D04
		public void PopulateSchedule(IDataReader reader)
		{
			this.m_task.Trigger = new TaskTrigger();
			string text = this.m_task.ID.ToString().ToUpper(CultureInfo.InvariantCulture);
			bool flag = true;
			while (reader.Read())
			{
				int @int = reader.GetInt32(3);
				int int2 = reader.GetInt32(4);
				int int3 = reader.GetInt32(5);
				int int4 = reader.GetInt32(6);
				int int5 = reader.GetInt32(7);
				int int6 = reader.GetInt32(8);
				int int7 = reader.GetInt32(9);
				int int8 = reader.GetInt32(10);
				int int9 = reader.GetInt32(11);
				reader.GetInt32(12);
				if (flag)
				{
					flag = false;
					DateTime sqlTime = SqlScheduleHelper.GetSqlTime(int9);
					DateTime dateTime = SqlScheduleHelper.GetSqlDays(int7);
					DateTime dateTime2 = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, sqlTime.Hour, sqlTime.Minute, sqlTime.Second);
					DateTime minValue = DateTime.MinValue;
					if (int8 != 99991231)
					{
						dateTime = SqlScheduleHelper.GetSqlDays(int8);
						minValue = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
					}
					this.m_task.Trigger.StartDate = dateTime2;
					this.m_task.Trigger.EndDate = minValue;
					if (int3 != 1 && @int != 1 && @int != 4 && int2 != 1)
					{
						throw new InvalidSqlAgentJobException(text);
					}
					SqlScheduleHelper.Frequency_Type frequency_Type = (SqlScheduleHelper.Frequency_Type)@int;
					if (frequency_Type <= SqlScheduleHelper.Frequency_Type.Daily)
					{
						if (frequency_Type == SqlScheduleHelper.Frequency_Type.Once)
						{
							this.m_task.Trigger.SetToOnce();
							continue;
						}
						if (frequency_Type == SqlScheduleHelper.Frequency_Type.Daily)
						{
							if (int3 == 1)
							{
								this.m_task.Trigger.SetToDaily((long)int2);
								continue;
							}
							this.m_task.Trigger.SetToMinutes(int4);
							continue;
						}
					}
					else
					{
						if (frequency_Type == SqlScheduleHelper.Frequency_Type.Weekly)
						{
							this.m_task.Trigger.SetToWeekly((long)int6, (uint)int2);
							continue;
						}
						if (frequency_Type == SqlScheduleHelper.Frequency_Type.Monthly)
						{
							uint dayBit = SqlScheduleHelper.GetDayBit(int2);
							uint num = SqlScheduleHelper.ConvertToMonths(SqlScheduleHelper.GetSqlDays(int7).Month);
							this.m_task.Trigger.SetToMonthly(dayBit, num);
							continue;
						}
						if (frequency_Type == SqlScheduleHelper.Frequency_Type.MonthlyDOW)
						{
							SqlScheduleHelper.Frequency_Relative_Interval frequency_Relative_Interval = (SqlScheduleHelper.Frequency_Relative_Interval)int5;
							uint num2;
							switch (frequency_Relative_Interval)
							{
							case SqlScheduleHelper.Frequency_Relative_Interval.First:
								num2 = 1U;
								break;
							case SqlScheduleHelper.Frequency_Relative_Interval.Second:
								num2 = 2U;
								break;
							case (SqlScheduleHelper.Frequency_Relative_Interval)3:
								goto IL_0266;
							case SqlScheduleHelper.Frequency_Relative_Interval.Third:
								num2 = 3U;
								break;
							default:
								if (frequency_Relative_Interval != SqlScheduleHelper.Frequency_Relative_Interval.Fourth)
								{
									if (frequency_Relative_Interval != SqlScheduleHelper.Frequency_Relative_Interval.Last)
									{
										goto IL_0266;
									}
									num2 = 5U;
								}
								else
								{
									num2 = 4U;
								}
								break;
							}
							uint num3 = (uint)Math.Pow(2.0, (double)(int2 - 1));
							uint num4 = SqlScheduleHelper.ConvertToMonths(SqlScheduleHelper.GetSqlDays(int7).Month);
							this.m_task.Trigger.SetToMonthlyDOW(num2, num3, num4);
							continue;
							IL_0266:
							throw new InvalidSqlAgentJobException(text, "invalid freq_relative_interval in FillTriggerFromSql");
						}
					}
					throw new InvalidSqlAgentJobException(text, "invalid TriggerType in FillTriggerFromSql");
				}
				else
				{
					if (@int != 16 && @int != 32)
					{
						throw new InvalidSqlAgentJobException(text);
					}
					if (this.m_task.Trigger.RecurrenceType == RecurrenceType.MonthlyDate && @int == 16)
					{
						DateTime sqlDays = SqlScheduleHelper.GetSqlDays(int7);
						Monthly monthly = (Monthly)this.m_task.Trigger.TriggerData;
						uint dayBit2 = SqlScheduleHelper.GetDayBit(int2);
						monthly.DaysOfMonth |= dayBit2;
						monthly.Months |= SqlScheduleHelper.ConvertToMonths(sqlDays.Month);
					}
					else
					{
						if (this.m_task.Trigger.RecurrenceType != RecurrenceType.MonthlyDOW || @int != 32)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						MonthlyDOW monthlyDOW = (MonthlyDOW)this.m_task.Trigger.TriggerData;
						if (int5 == 1 && monthlyDOW.Week != 1U)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						if (int5 == 2 && monthlyDOW.Week != 2U)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						if (int5 == 4 && monthlyDOW.Week != 3U)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						if (int5 == 8 && monthlyDOW.Week != 4U)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						if (int5 == 16 && monthlyDOW.Week != 5U)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						if (int2 > 7)
						{
							throw new InvalidSqlAgentJobException(text);
						}
						uint num5 = (uint)Math.Pow(2.0, (double)(int2 - 1));
						monthlyDOW.DaysOfWeek |= num5;
						DateTime sqlDays2 = SqlScheduleHelper.GetSqlDays(int7);
						monthlyDOW.Months |= SqlScheduleHelper.ConvertToMonths(sqlDays2.Month);
					}
				}
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00056F60 File Offset: 0x00055160
		public static uint GetDayBit(int day)
		{
			RSTrace.ScheduleTracer.Assert(day <= 31 && day > 0, "Invalid day int GetDayBit");
			uint num = 1U;
			for (int i = 1; i < day; i++)
			{
				num <<= 1;
			}
			return num;
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00056F9B File Offset: 0x0005519B
		public static uint ConvertToMonths(int month)
		{
			RSTrace.ScheduleTracer.Assert(month >= 1 && month <= 12);
			return (uint)Math.Pow(2.0, (double)(month - 1));
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00056FC9 File Offset: 0x000551C9
		public static DateTime GetSqlDays(int date)
		{
			return DateTime.ParseExact(date.ToString(CultureInfo.InvariantCulture), "yyyyMMdd", CultureInfo.InvariantCulture);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00056FE8 File Offset: 0x000551E8
		public static DateTime GetSqlTime(int time)
		{
			string text = time.ToString(CultureInfo.InvariantCulture);
			if (text.Length < 6)
			{
				text = text.PadLeft(6, '0');
			}
			return DateTime.ParseExact(text, "HHmmss", null);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00057024 File Offset: 0x00055224
		public SQLScheduleSet GetSQLScheduleSet()
		{
			SQLScheduleSet sqlscheduleSet = new SQLScheduleSet();
			SQLScheduleParameters sqlscheduleParameters = new SQLScheduleParameters();
			if (this.m_task.Trigger.RecurrenceType != RecurrenceType.MonthlyDate && this.m_task.Trigger.RecurrenceType != RecurrenceType.MonthlyDOW)
			{
				sqlscheduleParameters.AddFrequencyType(this.m_task.SQlFreqType);
				sqlscheduleParameters.Add_Active_Start_Date(this.m_task.Trigger.StartDate);
				sqlscheduleParameters.Add_Active_Start_Time(this.m_task.Trigger.StartDate);
				if (this.m_task.Trigger.EndDate != DateTime.MinValue)
				{
					sqlscheduleParameters.Add_Active_End_Date(this.m_task.Trigger.EndDate);
				}
				switch (this.m_task.Trigger.RecurrenceType)
				{
				case RecurrenceType.Once:
					sqlscheduleParameters.AddFrequency_SubDay_Type(1);
					break;
				case RecurrenceType.Minutes:
				{
					Minutes minutes = (Minutes)this.m_task.Trigger.TriggerData;
					sqlscheduleParameters.AddFrequencyInterval(1);
					sqlscheduleParameters.AddFrequency_SubDay_Type(4);
					sqlscheduleParameters.Add_Frequency_SubDay_Interval(minutes.MinutesInterval);
					sqlscheduleParameters.Add_Active_End_Time(this.m_task.Trigger.StartDate.AddMinutes(-1.0));
					break;
				}
				case RecurrenceType.Daily:
				{
					Daily daily = (Daily)this.m_task.Trigger.TriggerData;
					sqlscheduleParameters.AddFrequencyInterval((int)daily.DaysInterval);
					sqlscheduleParameters.AddFrequency_SubDay_Type(1);
					break;
				}
				case RecurrenceType.Weekly:
				{
					Weekly weekly = (Weekly)this.m_task.Trigger.TriggerData;
					sqlscheduleParameters.AddFrequencyInterval((int)weekly.DaysOfWeek);
					sqlscheduleParameters.AddFrequency_SubDay_Type(1);
					sqlscheduleParameters.Add_Frequency_Recurrence_factor((int)weekly.WeeksInterval);
					break;
				}
				}
				sqlscheduleSet.Add(sqlscheduleParameters);
			}
			else if (this.m_task.Trigger.RecurrenceType == RecurrenceType.MonthlyDate)
			{
				Monthly monthly = (Monthly)this.m_task.Trigger.TriggerData;
				Months months = (Months)monthly.Months;
				bool isEveryMonth = monthly.IsEveryMonth;
				int num = 0;
				for (uint num2 = 1U; num2 <= 2048U; num2 *= 2U)
				{
					num++;
					if (num2 != 1U && isEveryMonth)
					{
						break;
					}
					if (((long)months & (long)((ulong)num2)) != 0L)
					{
						DateTime dateTime = this.m_task.Trigger.StartDate;
						if (!(this.m_task.Trigger.EndDate != DateTime.MinValue) || !(dateTime > this.m_task.Trigger.EndDate))
						{
							uint daysOfMonth = monthly.DaysOfMonth;
							uint num3 = 1U;
							int num4 = 0;
							for (int i = 1; i < 33; i++)
							{
								if ((num3 & daysOfMonth) > 0U)
								{
									num4 = i;
									if (!isEveryMonth)
									{
										DateTime dateTime2 = ((this.m_task.Trigger.StartDate < DateTime.Now) ? DateTime.Now : this.m_task.Trigger.StartDate);
										if (num < dateTime2.Month || (num == dateTime2.Month && i < dateTime2.Day))
										{
											dateTime = new DateTime(dateTime2.Year + 1, num, 1);
											if (29 != num4 || 2 != num)
											{
												goto IL_0331;
											}
											try
											{
												new DateTime(dateTime.Year, dateTime.Month, num4);
												goto IL_0331;
											}
											catch (ArgumentOutOfRangeException)
											{
												num4 = 28;
												goto IL_0331;
											}
										}
										dateTime = sqlscheduleParameters.GetStartMonthDate((Months)num2, dateTime2);
									}
									IL_0331:
									if (this.m_task.Trigger.EndDate == DateTime.MinValue || dateTime <= this.m_task.Trigger.EndDate)
									{
										sqlscheduleParameters.AddFrequencyType(this.m_task.SQlFreqType);
										sqlscheduleParameters.AddFrequency_SubDay_Type(1);
										if (isEveryMonth)
										{
											sqlscheduleParameters.Add_Frequency_Recurrence_factor(1);
										}
										else
										{
											sqlscheduleParameters.Add_Frequency_Recurrence_factor(12);
										}
										sqlscheduleParameters.AddFrequencyInterval(num4);
										sqlscheduleParameters.Add_Active_Start_Date(dateTime);
										sqlscheduleParameters.Add_Active_Start_Time(this.m_task.Trigger.StartDate);
										if (this.m_task.Trigger.EndDate != DateTime.MinValue)
										{
											sqlscheduleParameters.Add_Active_End_Date(this.m_task.Trigger.EndDate);
										}
										sqlscheduleSet.Add(sqlscheduleParameters);
										sqlscheduleParameters = new SQLScheduleParameters();
									}
								}
								num3 <<= 1;
							}
						}
					}
				}
			}
			else
			{
				RSTrace.ScheduleTracer.Assert(this.m_task.Trigger.RecurrenceType == RecurrenceType.MonthlyDOW);
				MonthlyDOW monthlyDOW = (MonthlyDOW)this.m_task.Trigger.TriggerData;
				Months months2 = (Months)monthlyDOW.Months;
				bool isEveryMonth2 = monthlyDOW.IsEveryMonth;
				uint num5 = 1U;
				while (num5 <= 2048U && (num5 == 1U || !isEveryMonth2))
				{
					if (((long)months2 & (long)((ulong)num5)) != 0L)
					{
						DateTime dateTime3 = this.m_task.Trigger.StartDate;
						if (!isEveryMonth2)
						{
							dateTime3 = sqlscheduleParameters.GetStartMonthDate((Months)num5, this.m_task.Trigger.StartDate);
						}
						if (!(this.m_task.Trigger.EndDate != DateTime.MinValue) || !(dateTime3 > this.m_task.Trigger.EndDate))
						{
							uint daysOfWeek = monthlyDOW.DaysOfWeek;
							uint num6 = 1U;
							for (int j = 1; j <= 64; j *= 2)
							{
								if ((num6 & daysOfWeek) > 0U)
								{
									sqlscheduleParameters.AddFrequencyType(this.m_task.SQlFreqType);
									if (j <= 8)
									{
										switch (j)
										{
										case 1:
											sqlscheduleParameters.AddFrequencyInterval(1);
											break;
										case 2:
											sqlscheduleParameters.AddFrequencyInterval(2);
											break;
										case 3:
											break;
										case 4:
											sqlscheduleParameters.AddFrequencyInterval(3);
											break;
										default:
											if (j == 8)
											{
												sqlscheduleParameters.AddFrequencyInterval(4);
											}
											break;
										}
									}
									else if (j != 16)
									{
										if (j != 32)
										{
											if (j == 64)
											{
												sqlscheduleParameters.AddFrequencyInterval(7);
											}
										}
										else
										{
											sqlscheduleParameters.AddFrequencyInterval(6);
										}
									}
									else
									{
										sqlscheduleParameters.AddFrequencyInterval(5);
									}
									sqlscheduleParameters.AddFrequency_SubDay_Type(1);
									switch (monthlyDOW.Week)
									{
									case 1U:
										sqlscheduleParameters.Add_Frequency_Relative_Interval(1);
										break;
									case 2U:
										sqlscheduleParameters.Add_Frequency_Relative_Interval(2);
										break;
									case 3U:
										sqlscheduleParameters.Add_Frequency_Relative_Interval(4);
										break;
									case 4U:
										sqlscheduleParameters.Add_Frequency_Relative_Interval(8);
										break;
									case 5U:
										sqlscheduleParameters.Add_Frequency_Relative_Interval(16);
										break;
									}
									if (isEveryMonth2)
									{
										sqlscheduleParameters.Add_Frequency_Recurrence_factor(1);
									}
									else
									{
										sqlscheduleParameters.Add_Frequency_Recurrence_factor(12);
									}
									sqlscheduleParameters.Add_Active_Start_Date(dateTime3);
									sqlscheduleParameters.Add_Active_Start_Time(this.m_task.Trigger.StartDate);
									if (this.m_task.Trigger.EndDate != DateTime.MinValue)
									{
										sqlscheduleParameters.Add_Active_End_Date(this.m_task.Trigger.EndDate);
									}
									sqlscheduleSet.Add(sqlscheduleParameters);
									sqlscheduleParameters = new SQLScheduleParameters();
								}
								num6 <<= 1;
							}
						}
					}
					num5 *= 2U;
				}
			}
			return sqlscheduleSet;
		}

		// Token: 0x040007FE RID: 2046
		private Task m_task;

		// Token: 0x040007FF RID: 2047
		public const int _SqlMaxDate = 99991231;

		// Token: 0x020004B8 RID: 1208
		private enum PopulateProjection
		{
			// Token: 0x040010E1 RID: 4321
			Frequency_Type = 3,
			// Token: 0x040010E2 RID: 4322
			Frequency_Interval,
			// Token: 0x040010E3 RID: 4323
			Frequency_SubDay_Type,
			// Token: 0x040010E4 RID: 4324
			Frequency_SubDay_Interval,
			// Token: 0x040010E5 RID: 4325
			Frequency_Relative_Interval,
			// Token: 0x040010E6 RID: 4326
			Frequency_Recurrence_Factor,
			// Token: 0x040010E7 RID: 4327
			Active_Start_Date,
			// Token: 0x040010E8 RID: 4328
			Active_End_Date,
			// Token: 0x040010E9 RID: 4329
			Active_Start_Time,
			// Token: 0x040010EA RID: 4330
			Active_End_Time
		}

		// Token: 0x020004B9 RID: 1209
		private enum Frequency_Type
		{
			// Token: 0x040010EC RID: 4332
			Once = 1,
			// Token: 0x040010ED RID: 4333
			Daily = 4,
			// Token: 0x040010EE RID: 4334
			Weekly = 8,
			// Token: 0x040010EF RID: 4335
			Monthly = 16,
			// Token: 0x040010F0 RID: 4336
			MonthlyDOW = 32
		}

		// Token: 0x020004BA RID: 1210
		private enum Frequency_SubDay_Type
		{
			// Token: 0x040010F2 RID: 4338
			AtSpecifiedTime = 1,
			// Token: 0x040010F3 RID: 4339
			Minutes = 4
		}

		// Token: 0x020004BB RID: 1211
		private enum Frequency_Relative_Interval
		{
			// Token: 0x040010F5 RID: 4341
			First = 1,
			// Token: 0x040010F6 RID: 4342
			Second,
			// Token: 0x040010F7 RID: 4343
			Third = 4,
			// Token: 0x040010F8 RID: 4344
			Fourth = 8,
			// Token: 0x040010F9 RID: 4345
			Last = 16
		}
	}
}
