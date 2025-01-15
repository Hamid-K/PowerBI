using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.DrdaAr
{
	// Token: 0x02000772 RID: 1906
	public class DrdaArEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DB5 RID: 15797 RVA: 0x000CFB94 File Offset: 0x000CDD94
		public DrdaArEventLogContainer()
			: base(SR.DrdaArEventLogSource)
		{
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000CFBA1 File Offset: 0x000CDDA1
		public static void Install()
		{
			EventLogContainer.Install(SR.DrdaArEventLogSource);
		}
	}
}
