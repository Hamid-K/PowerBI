using System;
using System.Xml.Serialization;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002EE RID: 750
	public class Schedule
	{
		// Token: 0x06001AD8 RID: 6872 RVA: 0x0006C530 File Offset: 0x0006A730
		internal static Schedule[] SoapScheduleToThisArray(Schedule[] schs)
		{
			if (schs == null)
			{
				return null;
			}
			Schedule[] array = new Schedule[schs.Length];
			for (int i = 0; i < schs.Length; i++)
			{
				array[i] = Schedule.SoapScheduleToThis(schs[i]);
			}
			return array;
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x0006C568 File Offset: 0x0006A768
		internal static Schedule SoapScheduleToThis(Schedule sch2000)
		{
			if (sch2000 == null)
			{
				return null;
			}
			return new Schedule
			{
				ScheduleID = sch2000.ScheduleID,
				Name = sch2000.Name,
				Definition = sch2000.Definition,
				Description = sch2000.Description,
				Creator = sch2000.Creator,
				NextRunTime = sch2000.NextRunTime,
				NextRunTimeSpecified = sch2000.NextRunTimeSpecified,
				LastRunTime = sch2000.LastRunTime,
				LastRunTimeSpecified = sch2000.LastRunTimeSpecified,
				ReferencesPresent = sch2000.ReferencesPresent,
				ScheduleStateName = sch2000.State.ToString()
			};
		}

		// Token: 0x040009AC RID: 2476
		public string ScheduleID;

		// Token: 0x040009AD RID: 2477
		public string Name;

		// Token: 0x040009AE RID: 2478
		public ScheduleDefinition Definition;

		// Token: 0x040009AF RID: 2479
		public string Description;

		// Token: 0x040009B0 RID: 2480
		public string Creator;

		// Token: 0x040009B1 RID: 2481
		public DateTime NextRunTime;

		// Token: 0x040009B2 RID: 2482
		[XmlIgnore]
		public bool NextRunTimeSpecified;

		// Token: 0x040009B3 RID: 2483
		public DateTime LastRunTime;

		// Token: 0x040009B4 RID: 2484
		[XmlIgnore]
		public bool LastRunTimeSpecified;

		// Token: 0x040009B5 RID: 2485
		public bool ReferencesPresent;

		// Token: 0x040009B6 RID: 2486
		public string ScheduleStateName;
	}
}
