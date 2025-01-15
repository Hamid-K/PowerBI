using System;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000035 RID: 53
	public interface ISystemClock
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000204 RID: 516
		DateTimeOffset UtcNow { get; }
	}
}
