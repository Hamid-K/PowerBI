using System;

namespace Microsoft.ReportingServices.Extensions
{
	// Token: 0x0200000F RID: 15
	internal interface IEventResolver
	{
		// Token: 0x06000086 RID: 134
		IEventHandler ResolveEvent(string eventType);

		// Token: 0x06000087 RID: 135
		void ItemPlacedInEventQueue();
	}
}
