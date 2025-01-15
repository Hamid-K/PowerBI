using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.MqClient
{
	// Token: 0x0200076D RID: 1901
	public class MqClientEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DAB RID: 15787 RVA: 0x000CFB17 File Offset: 0x000CDD17
		public MqClientEventLogContainer()
			: base(SR.MqClientEventLogSource)
		{
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000CFB24 File Offset: 0x000CDD24
		public static void Install()
		{
			EventLogContainer.Install(SR.MqClientEventLogSource);
		}
	}
}
