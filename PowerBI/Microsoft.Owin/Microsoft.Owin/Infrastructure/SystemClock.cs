using System;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x0200003D RID: 61
	public class SystemClock : ISystemClock
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000245 RID: 581 RVA: 0x000064B8 File Offset: 0x000046B8
		public DateTimeOffset UtcNow
		{
			get
			{
				DateTimeOffset utcNow = DateTimeOffset.UtcNow;
				return utcNow.AddMilliseconds((double)(-(double)utcNow.Millisecond));
			}
		}
	}
}
