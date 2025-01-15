using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000106 RID: 262
	internal sealed class ListEventsAction : RSSoapAction<ListEventsActionParameters>
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x00027BE8 File Offset: 0x00025DE8
		internal ListEventsAction(RSService service)
			: base("ListEventsAction", service)
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00027BF6 File Offset: 0x00025DF6
		internal override void PerformActionNow()
		{
			base.ActionParameters.Events = this.ListEvents();
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00027C0C File Offset: 0x00025E0C
		private Microsoft.ReportingServices.Library.Soap.Event[] ListEvents()
		{
			Microsoft.ReportingServices.Library.Soap.Event[] array = new Microsoft.ReportingServices.Library.Soap.Event[Globals.Configuration.EventTypes.Count];
			int num = 0;
			foreach (EventExtension eventExtension in Globals.Configuration.EventTypes.Values)
			{
				Microsoft.ReportingServices.Library.Soap.Event @event = new Microsoft.ReportingServices.Library.Soap.Event();
				@event.Type = eventExtension.EventType;
				array[num++] = @event;
			}
			return array;
		}
	}
}
