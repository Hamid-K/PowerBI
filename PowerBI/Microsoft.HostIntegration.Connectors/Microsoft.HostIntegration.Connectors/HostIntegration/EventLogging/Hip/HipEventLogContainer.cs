using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.Hip
{
	// Token: 0x02000770 RID: 1904
	public class HipEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DB1 RID: 15793 RVA: 0x000CFB62 File Offset: 0x000CDD62
		public HipEventLogContainer()
			: base(SR.HipEventLogSource)
		{
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000CFB6F File Offset: 0x000CDD6F
		public static void Install()
		{
			EventLogContainer.Install(SR.HipEventLogSource);
		}
	}
}
