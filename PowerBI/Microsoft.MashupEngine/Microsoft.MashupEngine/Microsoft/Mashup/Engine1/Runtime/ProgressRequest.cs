using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D9 RID: 5593
	public class ProgressRequest : IDisposable
	{
		// Token: 0x06008CB3 RID: 36019 RVA: 0x001D7F2B File Offset: 0x001D612B
		public ProgressRequest(IHostProgress hostProgress)
		{
			this.hostProgress = hostProgress;
			this.hostProgress.StartRequest();
		}

		// Token: 0x06008CB4 RID: 36020 RVA: 0x001D7F45 File Offset: 0x001D6145
		public void Dispose()
		{
			this.hostProgress.StopRequest();
		}

		// Token: 0x04004CBF RID: 19647
		private IHostProgress hostProgress;
	}
}
