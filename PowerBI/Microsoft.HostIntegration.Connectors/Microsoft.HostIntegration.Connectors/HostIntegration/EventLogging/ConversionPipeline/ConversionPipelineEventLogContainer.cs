using System;
using Microsoft.HostIntegration.StrictResources.EventLoggingContainers;

namespace Microsoft.HostIntegration.EventLogging.ConversionPipeline
{
	// Token: 0x02000773 RID: 1907
	public class ConversionPipelineEventLogContainer : EventLogContainer
	{
		// Token: 0x06003DB7 RID: 15799 RVA: 0x000CFBAD File Offset: 0x000CDDAD
		public ConversionPipelineEventLogContainer()
			: base(SR.ConversionPipelineEventLogSource)
		{
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000CFBBA File Offset: 0x000CDDBA
		public static void Install()
		{
			EventLogContainer.Install(SR.ConversionPipelineEventLogSource);
		}
	}
}
