using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.HostFiles
{
	// Token: 0x0200076F RID: 1903
	public class HostFilesEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DAF RID: 15791 RVA: 0x000CFB49 File Offset: 0x000CDD49
		public HostFilesEventLogContainer()
			: base(SR.HostFilesEventLogSource)
		{
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000CFB56 File Offset: 0x000CDD56
		public static void Install()
		{
			EventLogContainer.Install(SR.HostFilesEventLogSource);
		}
	}
}
