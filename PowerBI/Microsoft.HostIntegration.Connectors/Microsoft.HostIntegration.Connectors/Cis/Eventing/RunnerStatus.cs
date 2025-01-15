using System;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000499 RID: 1177
	public enum RunnerStatus : byte
	{
		// Token: 0x0400180A RID: 6154
		Healthy = 10,
		// Token: 0x0400180B RID: 6155
		Degraded = 20,
		// Token: 0x0400180C RID: 6156
		Unhealthy = 30,
		// Token: 0x0400180D RID: 6157
		MostlyDead = 40,
		// Token: 0x0400180E RID: 6158
		Dead = 50
	}
}
