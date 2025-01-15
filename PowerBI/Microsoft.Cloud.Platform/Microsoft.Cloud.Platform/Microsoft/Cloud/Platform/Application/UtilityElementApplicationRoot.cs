using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003F0 RID: 1008
	public class UtilityElementApplicationRoot<T> : UtilityApplicationRoot<T> where T : UtilityBlock, new()
	{
		// Token: 0x06001F00 RID: 7936 RVA: 0x00074178 File Offset: 0x00072378
		protected override void OnInitialize()
		{
			base.OnInitialize();
			base.AddBlock(new VoidEventsKitFactory());
			base.AddBlock(new ElementInstanceIdProvider());
		}
	}
}
