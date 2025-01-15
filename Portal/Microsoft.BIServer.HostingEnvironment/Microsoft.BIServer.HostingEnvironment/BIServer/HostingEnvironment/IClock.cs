using System;
using System.Threading.Tasks;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200000B RID: 11
	public interface IClock
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55
		DateTime Now { get; }

		// Token: 0x06000038 RID: 56
		Task<TimeSpan> WaitAsync(TimeSpan timeSpan);
	}
}
