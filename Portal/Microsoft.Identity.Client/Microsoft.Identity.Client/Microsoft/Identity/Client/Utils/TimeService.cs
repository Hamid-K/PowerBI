using System;

namespace Microsoft.Identity.Client.Utils
{
	// Token: 0x020001D3 RID: 467
	internal class TimeService : ITimeService
	{
		// Token: 0x0600146B RID: 5227 RVA: 0x0004565A File Offset: 0x0004385A
		public DateTime GetUtcNow()
		{
			return DateTime.UtcNow;
		}
	}
}
