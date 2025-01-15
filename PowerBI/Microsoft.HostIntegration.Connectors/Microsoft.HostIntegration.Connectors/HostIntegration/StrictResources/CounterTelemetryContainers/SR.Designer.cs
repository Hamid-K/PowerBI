using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.StrictResources.CounterTelemetryContainers
{
	// Token: 0x020005DB RID: 1499
	internal class SR
	{
		// Token: 0x0600340F RID: 13327 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000ADDB0 File Offset: 0x000ABFB0
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.StrictResources.CounterTelemetryContainers.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000ADDDC File Offset: 0x000ABFDC
		// (set) Token: 0x06003412 RID: 13330 RVA: 0x000ADDE3 File Offset: 0x000ABFE3
		internal static CultureInfo Culture
		{
			get
			{
				return SR.resourceCulture;
			}
			set
			{
				SR.resourceCulture = value;
			}
		}

		// Token: 0x04001CC0 RID: 7360
		private static ResourceManager resourceManager;

		// Token: 0x04001CC1 RID: 7361
		private static CultureInfo resourceCulture;
	}
}
