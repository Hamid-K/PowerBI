using System;
using System.Diagnostics;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200001D RID: 29
	public sealed class TimeMeter : IDisposable
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003FBD File Offset: 0x000021BD
		public TimeMeter(MeterCollector.MeterFactory reportTo)
			: this(reportTo.Data)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003FCB File Offset: 0x000021CB
		public TimeMeter(MeterCollector.MeterData reportTo)
		{
			this.stopWatch = Stopwatch.StartNew();
			this._reportTo = reportTo;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003FE5 File Offset: 0x000021E5
		public void Dispose()
		{
			this.stopWatch.Stop();
			this._reportTo.AddTicks(this.stopWatch.ElapsedTicks);
		}

		// Token: 0x04000079 RID: 121
		private readonly Stopwatch stopWatch;

		// Token: 0x0400007A RID: 122
		private readonly MeterCollector.MeterData _reportTo;
	}
}
