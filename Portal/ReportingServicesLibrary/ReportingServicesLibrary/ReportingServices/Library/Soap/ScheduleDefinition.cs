using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000336 RID: 822
	public class ScheduleDefinition : ScheduleDefinitionOrReference
	{
		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001B83 RID: 7043 RVA: 0x0006FE44 File Offset: 0x0006E044
		// (set) Token: 0x06001B84 RID: 7044 RVA: 0x0006FE4C File Offset: 0x0006E04C
		public DateTime StartDateTime { get; set; }

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0006FE55 File Offset: 0x0006E055
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0006FE5D File Offset: 0x0006E05D
		public DateTime EndDate { get; set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0006FE66 File Offset: 0x0006E066
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x0006FE6E File Offset: 0x0006E06E
		[XmlIgnore]
		public bool EndDateSpecified { get; set; }

		// Token: 0x06001B89 RID: 7049 RVA: 0x0006FE78 File Offset: 0x0006E078
		internal static ScheduleDefinition TaskToThis(Task task)
		{
			if (task == null)
			{
				return null;
			}
			ScheduleDefinition scheduleDefinition = new ScheduleDefinition();
			scheduleDefinition.StartDateTime = task.StartDateTime;
			scheduleDefinition.EndDateSpecified = false;
			if (task.EndDate != DateTime.MinValue)
			{
				scheduleDefinition.EndDateSpecified = true;
				scheduleDefinition.EndDate = task.EndDate;
			}
			if (task.Trigger.TriggerData is Minutes)
			{
				scheduleDefinition.Item = MinuteRecurrence.TriggerDataToThis((Minutes)task.Trigger.TriggerData);
			}
			else if (task.Trigger.TriggerData is Daily)
			{
				scheduleDefinition.Item = DailyRecurrence.TriggerDataToThis((Daily)task.Trigger.TriggerData);
			}
			else if (task.Trigger.TriggerData is Weekly)
			{
				scheduleDefinition.Item = WeeklyRecurrence.TriggerDataToThis((Weekly)task.Trigger.TriggerData);
			}
			else if (task.Trigger.TriggerData is Monthly)
			{
				scheduleDefinition.Item = MonthlyRecurrence.TriggerDataToThis((Monthly)task.Trigger.TriggerData);
			}
			else if (task.Trigger.TriggerData is MonthlyDOW)
			{
				scheduleDefinition.Item = MonthlyDOWRecurrence.TriggerDataToThis((MonthlyDOW)task.Trigger.TriggerData);
			}
			return scheduleDefinition;
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x0006FFB8 File Offset: 0x0006E1B8
		internal void PopulateTaskWithThis(Task task)
		{
			if (this.StartDateTime == DateTime.MinValue)
			{
				throw new MissingElementException("StartDateTime");
			}
			if (!ScheduleDefinition.DateInValidRange(this.StartDateTime))
			{
				throw new ScheduleDateTimeRangeException();
			}
			task.Trigger.StartDate = this.StartDateTime;
			if (this.EndDateSpecified)
			{
				if (!ScheduleDefinition.DateInValidRange(this.EndDate))
				{
					throw new ScheduleDateTimeRangeException();
				}
				if (this.EndDate < this.StartDateTime)
				{
					throw new InvalidElementException("EndDate");
				}
				task.Trigger.EndDate = this.EndDate;
			}
			if (this.Item == null)
			{
				task.Trigger.SetToOnce();
				return;
			}
			if (this.Item is MinuteRecurrence)
			{
				MinuteRecurrence minuteRecurrence = (MinuteRecurrence)this.Item;
				if (minuteRecurrence.MinutesInterval <= 0)
				{
					throw new InvalidElementException("MinutesInterval");
				}
				task.Trigger.SetToMinutes(minuteRecurrence.MinutesInterval);
				return;
			}
			else if (this.Item is DailyRecurrence)
			{
				DailyRecurrence dailyRecurrence = (DailyRecurrence)this.Item;
				if (dailyRecurrence.DaysInterval <= 0)
				{
					throw new InvalidElementException("DaysInterval");
				}
				task.Trigger.SetToDaily((long)dailyRecurrence.DaysInterval);
				return;
			}
			else if (this.Item is WeeklyRecurrence)
			{
				WeeklyRecurrence weeklyRecurrence = (WeeklyRecurrence)this.Item;
				int num = WeeklyRecurrence.DefaultWeeksInterval;
				if (weeklyRecurrence.WeeksIntervalSpecified)
				{
					num = weeklyRecurrence.WeeksInterval;
				}
				if (!weeklyRecurrence.DaysOfWeek.Monday && !weeklyRecurrence.DaysOfWeek.Tuesday && !weeklyRecurrence.DaysOfWeek.Wednesday && !weeklyRecurrence.DaysOfWeek.Thursday && !weeklyRecurrence.DaysOfWeek.Friday && !weeklyRecurrence.DaysOfWeek.Saturday && !weeklyRecurrence.DaysOfWeek.Sunday)
				{
					throw new MissingElementException("DaysOfWeek");
				}
				task.Trigger.SetToWeekly((long)num, DaysOfWeekSelector.ThisToUint(weeklyRecurrence.DaysOfWeek));
				return;
			}
			else
			{
				if (!(this.Item is MonthlyRecurrence))
				{
					if (this.Item is MonthlyDOWRecurrence)
					{
						MonthlyDOWRecurrence monthlyDOWRecurrence = (MonthlyDOWRecurrence)this.Item;
						bool flag = monthlyDOWRecurrence.DaysOfWeek.Monday || monthlyDOWRecurrence.DaysOfWeek.Tuesday || monthlyDOWRecurrence.DaysOfWeek.Wednesday || monthlyDOWRecurrence.DaysOfWeek.Thursday || monthlyDOWRecurrence.DaysOfWeek.Friday || monthlyDOWRecurrence.DaysOfWeek.Saturday || monthlyDOWRecurrence.DaysOfWeek.Sunday;
						bool flag2 = monthlyDOWRecurrence.MonthsOfYear.January || monthlyDOWRecurrence.MonthsOfYear.February || monthlyDOWRecurrence.MonthsOfYear.March || monthlyDOWRecurrence.MonthsOfYear.April || monthlyDOWRecurrence.MonthsOfYear.May || monthlyDOWRecurrence.MonthsOfYear.June || monthlyDOWRecurrence.MonthsOfYear.July || monthlyDOWRecurrence.MonthsOfYear.August || monthlyDOWRecurrence.MonthsOfYear.September || monthlyDOWRecurrence.MonthsOfYear.October || monthlyDOWRecurrence.MonthsOfYear.November || monthlyDOWRecurrence.MonthsOfYear.December;
						if (!flag)
						{
							throw new MissingElementException("DaysOfWeek");
						}
						if (!flag2)
						{
							throw new MissingElementException("MonthsOfYear");
						}
						task.Trigger.SetToMonthlyDOW((uint)(monthlyDOWRecurrence.WhichWeek + 1), DaysOfWeekSelector.ThisToUint(monthlyDOWRecurrence.DaysOfWeek), (monthlyDOWRecurrence.MonthsOfYear != null) ? monthlyDOWRecurrence.MonthsOfYear.ToUint() : 0U);
					}
					return;
				}
				MonthlyRecurrence monthlyRecurrence = (MonthlyRecurrence)this.Item;
				Months months = (Months)((monthlyRecurrence.MonthsOfYear != null) ? monthlyRecurrence.MonthsOfYear.ToUint() : 0U);
				if (!monthlyRecurrence.MonthsOfYear.January && !monthlyRecurrence.MonthsOfYear.February && !monthlyRecurrence.MonthsOfYear.March && !monthlyRecurrence.MonthsOfYear.April && !monthlyRecurrence.MonthsOfYear.May && !monthlyRecurrence.MonthsOfYear.June && !monthlyRecurrence.MonthsOfYear.July && !monthlyRecurrence.MonthsOfYear.August && !monthlyRecurrence.MonthsOfYear.September && !monthlyRecurrence.MonthsOfYear.October && !monthlyRecurrence.MonthsOfYear.November && !monthlyRecurrence.MonthsOfYear.December)
				{
					throw new MissingElementException("MonthsOfYear");
				}
				task.Trigger.SetToMonthly(Monthly.GetDayBitMap(monthlyRecurrence.Days, months), (uint)months);
				return;
			}
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x0007042C File Offset: 0x0006E62C
		internal static string DefinitionToXml(ScheduleDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			ScheduleDefinition.WriteToXml(definition, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x0007045B File Offset: 0x0006E65B
		internal static ScheduleDefinition XmlToDefinition(string xml)
		{
			if (xml == null)
			{
				return null;
			}
			Task task = new Task(Guid.Empty);
			task.FromXml(xml);
			return ScheduleDefinition.TaskToThis(task);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x00070478 File Offset: 0x0006E678
		internal static void WriteToXml(ScheduleDefinition definition, XmlTextWriter xml)
		{
			if (definition == null)
			{
				return;
			}
			xml.WriteStartElement("ScheduleDefinition");
			xml.WriteElementString("StartDateTime", definition.StartDateTime.ToString(CultureInfo.InvariantCulture));
			if (definition.EndDateSpecified)
			{
				xml.WriteElementString("EndDate", definition.EndDate.ToString("d", CultureInfo.InvariantCulture));
			}
			if (definition.Item is MinuteRecurrence)
			{
				MinuteRecurrence.WriteToXml((MinuteRecurrence)definition.Item, xml);
			}
			else if (definition.Item is DailyRecurrence)
			{
				DailyRecurrence.WriteToXml((DailyRecurrence)definition.Item, xml);
			}
			else if (definition.Item is WeeklyRecurrence)
			{
				WeeklyRecurrence.WriteToXml((WeeklyRecurrence)definition.Item, xml);
			}
			else if (definition.Item is MonthlyRecurrence)
			{
				MonthlyRecurrence.WriteToXml((MonthlyRecurrence)definition.Item, xml);
			}
			else if (definition.Item is MonthlyDOWRecurrence)
			{
				MonthlyDOWRecurrence.WriteToXml((MonthlyDOWRecurrence)definition.Item, xml);
			}
			xml.WriteEndElement();
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00070584 File Offset: 0x0006E784
		internal bool IsValid()
		{
			return this.Item == null || ((!(this.Item is WeeklyRecurrence) || ((WeeklyRecurrence)this.Item).DaysOfWeek != null) && (!(this.Item is MonthlyRecurrence) || ((MonthlyRecurrence)this.Item).MonthsOfYear != null) && (!(this.Item is MonthlyDOWRecurrence) || (((MonthlyDOWRecurrence)this.Item).DaysOfWeek != null && ((MonthlyDOWRecurrence)this.Item).MonthsOfYear != null)));
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x00070611 File Offset: 0x0006E811
		private static bool DateInValidRange(DateTime dt)
		{
			return ScheduleDefinition.minStart <= dt && ScheduleDefinition.maxEnd >= dt;
		}

		// Token: 0x04000B1C RID: 2844
		[XmlElement(typeof(MinuteRecurrence))]
		[XmlElement(typeof(DailyRecurrence))]
		[XmlElement(typeof(WeeklyRecurrence))]
		[XmlElement(typeof(MonthlyRecurrence))]
		[XmlElement(typeof(MonthlyDOWRecurrence))]
		public RecurrencePattern Item;

		// Token: 0x04000B1D RID: 2845
		private static readonly DateTime minStart = new DateTime(1990, 1, 1);

		// Token: 0x04000B1E RID: 2846
		private static readonly DateTime maxEnd = DateTime.MaxValue;
	}
}
