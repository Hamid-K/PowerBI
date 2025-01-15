using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.SessionIntegrator
{
	// Token: 0x0200076C RID: 1900
	public class SessionIntegratorEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DA9 RID: 15785 RVA: 0x000CFAFE File Offset: 0x000CDCFE
		public SessionIntegratorEventLogContainer()
			: base(SR.SessionIntegratorEventLogSource)
		{
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x000CFB0B File Offset: 0x000CDD0B
		public static void Install()
		{
			EventLogContainer.Install(SR.SessionIntegratorEventLogSource);
		}
	}
}
