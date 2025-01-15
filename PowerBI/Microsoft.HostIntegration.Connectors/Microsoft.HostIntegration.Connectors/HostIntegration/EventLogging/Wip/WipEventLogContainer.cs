using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.Wip
{
	// Token: 0x0200076B RID: 1899
	public class WipEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DA7 RID: 15783 RVA: 0x000CFAE5 File Offset: 0x000CDCE5
		public WipEventLogContainer()
			: base(SR.WipEventLogSource)
		{
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000CFAF2 File Offset: 0x000CDCF2
		public static void Install()
		{
			EventLogContainer.Install(SR.WipEventLogSource);
		}
	}
}
