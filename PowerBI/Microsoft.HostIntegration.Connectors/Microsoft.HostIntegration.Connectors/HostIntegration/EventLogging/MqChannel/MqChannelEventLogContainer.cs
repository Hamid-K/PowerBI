using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.MqChannel
{
	// Token: 0x0200076E RID: 1902
	public class MqChannelEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DAD RID: 15789 RVA: 0x000CFB30 File Offset: 0x000CDD30
		public MqChannelEventLogContainer()
			: base(SR.MqChannelEventLogSource)
		{
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000CFB3D File Offset: 0x000CDD3D
		public static void Install()
		{
			EventLogContainer.Install(SR.MqChannelEventLogSource);
		}
	}
}
