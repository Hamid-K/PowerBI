using System;
using System.Globalization;
using System.Resources;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x0200050A RID: 1290
	internal class SR
	{
		// Token: 0x06002B91 RID: 11153 RVA: 0x00002061 File Offset: 0x00000261
		private SR()
		{
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06002B92 RID: 11154 RVA: 0x0009619C File Offset: 0x0009439C
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (SR.resourceManager == null)
				{
					SR.resourceManager = new ResourceManager("Microsoft.HostIntegration.Common.SR", typeof(SR).Assembly);
				}
				return SR.resourceManager;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000961C8 File Offset: 0x000943C8
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000961CF File Offset: 0x000943CF
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

		// Token: 0x04001B77 RID: 7031
		private static ResourceManager resourceManager;

		// Token: 0x04001B78 RID: 7032
		private static CultureInfo resourceCulture;
	}
}
