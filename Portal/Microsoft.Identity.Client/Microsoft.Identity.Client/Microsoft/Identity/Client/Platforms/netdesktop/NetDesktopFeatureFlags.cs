using System;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.Platforms.netdesktop
{
	// Token: 0x02000186 RID: 390
	internal class NetDesktopFeatureFlags : IFeatureFlags
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0003F9FD File Offset: 0x0003DBFD
		public bool IsFociEnabled
		{
			get
			{
				return true;
			}
		}
	}
}
