using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003EA RID: 1002
	[EventsKit(6378033201498483233L)]
	[Trace(typeof(CommonTrace))]
	public interface IApplicationRootEvents
	{
		// Token: 0x06001ECF RID: 7887
		[Event(624553973825736865L, 1)]
		[PublishedEvent]
		void FireApplicationStateChangeCompletedEvent(BlockState stateBefore, BlockState stateAfter);

		// Token: 0x06001ED0 RID: 7888
		[Event(7693330382521224138L, 2, Level = EventLevel.Error)]
		[PublishedEvent(AlwaysEnabled = true)]
		void FireApplicationStateChangeFailedEvent(BlockState stateBefore, BlockState stateDesired, string block, string description);

		// Token: 0x06001ED1 RID: 7889
		[Event(1884791070976249107L, 3)]
		[PublishedEvent]
		void FireApplicationShutdownRequestedEvent(BlockState currentState, string requestor, int returnCode);

		// Token: 0x06001ED2 RID: 7890
		[Event(440641205129802808L, 4, Level = EventLevel.Error, Format = "Unhandled exception in element '{1}' due to: {0}")]
		[PublishedEvent(AlwaysEnabled = true)]
		[WindowsEventLog(EventLogEntryType.Error, 500)]
		[Logging]
		void FireApplicationUnhandledExceptionEvent(string exceptionText, string elementOrProcessId);

		// Token: 0x06001ED3 RID: 7891
		[Event(2242056975486269322L, 5, Level = EventLevel.Informational)]
		[PublishedEvent]
		[PerformanceCounter(1902796230, "ProcessId", CounterModifier.Set, "processId", CounterType.NumberOfItems)]
		void FireApplicationProcessId(int processId);

		// Token: 0x06001ED4 RID: 7892
		[Event(4444813373239364203L, 6, Level = EventLevel.Informational)]
		[PublishedEvent(AlwaysEnabled = true)]
		[Logging]
		void FireApplicationStartInformationEvent(string machineName, string osVersion, int processId, string processName, string clrVersion, string entryAssemblyFilename, string entryAssemblyName, string mainModuleVersion, string machineNetworkAddresses);

		// Token: 0x06001ED5 RID: 7893
		[Event(8586220386793283224L, 7)]
		[PublishedEvent]
		[Logging]
		void FireApplicationShutdownCompletedEvent(long duration);

		// Token: 0x06001ED6 RID: 7894
		[Event(3484387856948743630L, 8)]
		[PublishedEvent]
		[Logging]
		void FireApplicationStartedEvent(long duration);
	}
}
