using System;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F1 RID: 497
	internal class EventSourcePlatformLogger : IPlatformLogger
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00046F1E File Offset: 0x0004511E
		internal static MsalEventSource MsalEventSource { get; } = new MsalEventSource();

		// Token: 0x06001547 RID: 5447 RVA: 0x00046F25 File Offset: 0x00045125
		public void Always(string message)
		{
			EventSourcePlatformLogger.MsalEventSource.Information(message);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00046F32 File Offset: 0x00045132
		public void Error(string message)
		{
			EventSourcePlatformLogger.MsalEventSource.Error(message);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00046F3F File Offset: 0x0004513F
		public void Warning(string message)
		{
			EventSourcePlatformLogger.MsalEventSource.Error(message);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00046F4C File Offset: 0x0004514C
		public void Verbose(string message)
		{
			EventSourcePlatformLogger.MsalEventSource.Error(message);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00046F59 File Offset: 0x00045159
		public void Information(string message)
		{
			EventSourcePlatformLogger.MsalEventSource.Error(message);
		}
	}
}
