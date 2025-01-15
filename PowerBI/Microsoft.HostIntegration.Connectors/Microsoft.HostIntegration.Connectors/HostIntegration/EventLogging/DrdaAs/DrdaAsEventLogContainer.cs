using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.DrdaAs
{
	// Token: 0x02000771 RID: 1905
	public class DrdaAsEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DB3 RID: 15795 RVA: 0x000CFB7B File Offset: 0x000CDD7B
		public DrdaAsEventLogContainer()
			: base(SR.DrdaAsEventLogSource)
		{
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000CFB88 File Offset: 0x000CDD88
		public static void Install()
		{
			EventLogContainer.Install(SR.DrdaAsEventLogSource);
		}
	}
}
