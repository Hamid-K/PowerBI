using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000083 RID: 131
	internal static class InteractivityPaginationModuleFactory
	{
		// Token: 0x06000789 RID: 1929 RVA: 0x0001BD43 File Offset: 0x00019F43
		internal static IInteractivityPaginationModule CreateInteractivityPaginationModule()
		{
			return (IInteractivityPaginationModule)Activator.CreateInstance("Microsoft.ReportingServices.SPBProcessing", "Microsoft.ReportingServices.Rendering.SPBProcessing.SPBInteractivityProcessing").Unwrap();
		}
	}
}
