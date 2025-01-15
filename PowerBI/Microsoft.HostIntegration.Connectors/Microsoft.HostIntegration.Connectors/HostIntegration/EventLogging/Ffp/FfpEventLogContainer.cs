using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.Ffp
{
	// Token: 0x0200076A RID: 1898
	public class FfpEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DA5 RID: 15781 RVA: 0x000CFACC File Offset: 0x000CDCCC
		public FfpEventLogContainer()
			: base(SR.FfpEventLogSource)
		{
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x000CFAD9 File Offset: 0x000CDCD9
		public static void Install()
		{
			EventLogContainer.Install(SR.FfpEventLogSource);
		}
	}
}
