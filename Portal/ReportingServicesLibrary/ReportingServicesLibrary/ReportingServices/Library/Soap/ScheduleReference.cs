using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000335 RID: 821
	public class ScheduleReference : ScheduleDefinitionOrReference
	{
		// Token: 0x06001B81 RID: 7041 RVA: 0x0006FE0C File Offset: 0x0006E00C
		internal static ScheduleReference TaskToThis(Task task)
		{
			return new ScheduleReference
			{
				ScheduleID = task.ID.ToString(),
				Definition = ScheduleDefinition.TaskToThis(task)
			};
		}

		// Token: 0x04000B17 RID: 2839
		public string ScheduleID;

		// Token: 0x04000B18 RID: 2840
		public ScheduleDefinition Definition;
	}
}
