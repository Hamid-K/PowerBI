using System;
using System.Collections;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000332 RID: 818
	public class Schedule
	{
		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x0006FBE7 File Offset: 0x0006DDE7
		// (set) Token: 0x06001B68 RID: 7016 RVA: 0x0006FBEF File Offset: 0x0006DDEF
		public string ScheduleID { get; set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x0006FBF8 File Offset: 0x0006DDF8
		// (set) Token: 0x06001B6A RID: 7018 RVA: 0x0006FC00 File Offset: 0x0006DE00
		public string Name { get; set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0006FC09 File Offset: 0x0006DE09
		// (set) Token: 0x06001B6C RID: 7020 RVA: 0x0006FC11 File Offset: 0x0006DE11
		public ScheduleDefinition Definition { get; set; }

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0006FC1A File Offset: 0x0006DE1A
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x0006FC22 File Offset: 0x0006DE22
		public string Description { get; set; }

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0006FC2B File Offset: 0x0006DE2B
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x0006FC33 File Offset: 0x0006DE33
		public string Creator { get; set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0006FC3C File Offset: 0x0006DE3C
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x0006FC44 File Offset: 0x0006DE44
		public DateTime NextRunTime { get; set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0006FC4D File Offset: 0x0006DE4D
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0006FC55 File Offset: 0x0006DE55
		[XmlIgnore]
		public bool NextRunTimeSpecified { get; set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0006FC5E File Offset: 0x0006DE5E
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0006FC66 File Offset: 0x0006DE66
		public DateTime LastRunTime { get; set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0006FC6F File Offset: 0x0006DE6F
		// (set) Token: 0x06001B78 RID: 7032 RVA: 0x0006FC77 File Offset: 0x0006DE77
		[XmlIgnore]
		public bool LastRunTimeSpecified { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x0006FC80 File Offset: 0x0006DE80
		// (set) Token: 0x06001B7A RID: 7034 RVA: 0x0006FC88 File Offset: 0x0006DE88
		public bool ReferencesPresent { get; set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0006FC91 File Offset: 0x0006DE91
		// (set) Token: 0x06001B7C RID: 7036 RVA: 0x0006FC99 File Offset: 0x0006DE99
		public ScheduleStateEnum State { get; set; }

		// Token: 0x06001B7D RID: 7037 RVA: 0x0006FCA4 File Offset: 0x0006DEA4
		internal static Schedule TaskToThis(Task schedule)
		{
			Schedule schedule2 = new Schedule();
			schedule2.ScheduleID = schedule.ID.ToString();
			schedule2.Name = schedule.Name;
			schedule2.Definition = ScheduleDefinition.TaskToThis(schedule);
			schedule2.Description = schedule.Trigger.ScheduleDescription;
			schedule2.Creator = schedule.Creator.UserName;
			schedule2.NextRunTime = schedule.NextRunTime;
			schedule2.NextRunTimeSpecified = schedule.NextRunTime != DateTime.MinValue;
			schedule2.LastRunTime = schedule.LastRunTime;
			schedule2.LastRunTimeSpecified = schedule.LastRunTime != DateTime.MinValue;
			if (schedule.IsFailing)
			{
				schedule2.State = ScheduleStateEnum.Failing;
			}
			else if (schedule.IsRunning)
			{
				schedule2.State = ScheduleStateEnum.Running;
			}
			else if (schedule.ScheduleState == TaskState.Ready)
			{
				schedule2.State = ScheduleStateEnum.Ready;
			}
			else if (schedule.ScheduleState == TaskState.Paused)
			{
				schedule2.State = ScheduleStateEnum.Paused;
			}
			else if (schedule.ScheduleState == TaskState.Expired)
			{
				schedule2.State = ScheduleStateEnum.Expired;
			}
			if (schedule.ReportsCount > 0)
			{
				schedule2.ReferencesPresent = true;
			}
			else
			{
				schedule2.ReferencesPresent = false;
			}
			return schedule2;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0006FDC0 File Offset: 0x0006DFC0
		internal static Schedule[] TaskArrayToThisArray(ArrayList schedules)
		{
			if (schedules == null)
			{
				return null;
			}
			Schedule[] array = new Schedule[schedules.Count];
			for (int i = 0; i < schedules.Count; i++)
			{
				array[i] = Schedule.TaskToThis((Task)schedules[i]);
			}
			return array;
		}
	}
}
