using System;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x0200032A RID: 810
	public class ScheduleDefinitionOrReference
	{
		// Token: 0x06001B60 RID: 7008 RVA: 0x0006FAA0 File Offset: 0x0006DCA0
		internal static string ThisToXml(ScheduleDefinitionOrReference sdor, out string option)
		{
			if (sdor is NoSchedule)
			{
				option = "No Snapshot";
				return null;
			}
			if (sdor is ScheduleReference)
			{
				option = "Shared Schedule";
				return ((ScheduleReference)sdor).ScheduleID;
			}
			if (sdor is ScheduleDefinition)
			{
				ScheduleDefinition scheduleDefinition = (ScheduleDefinition)sdor;
				option = "Scoped Schedule";
				return ScheduleDefinition.DefinitionToXml(scheduleDefinition);
			}
			option = null;
			return null;
		}
	}
}
