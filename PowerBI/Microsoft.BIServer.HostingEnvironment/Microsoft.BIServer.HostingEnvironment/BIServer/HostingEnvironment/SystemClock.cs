using System;
using System.Threading.Tasks;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200001B RID: 27
	public class SystemClock : IClock
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003D70 File Offset: 0x00001F70
		public DateTime Now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003D78 File Offset: 0x00001F78
		public async Task<TimeSpan> WaitAsync(TimeSpan timeSpan)
		{
			DateTime start = DateTime.Now;
			await Task.Delay(timeSpan);
			return DateTime.Now - start;
		}
	}
}
