using System;
using System.Diagnostics;

namespace Microsoft.MachineLearning.Internal.Utilities
{
	// Token: 0x020004DA RID: 1242
	public sealed class TimerScope : IDisposable
	{
		// Token: 0x06001969 RID: 6505 RVA: 0x0008FA49 File Offset: 0x0008DC49
		public TimerScope(IChannel ch)
		{
			this._ch = ch;
			this._watch = Stopwatch.StartNew();
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0008FA64 File Offset: 0x0008DC64
		public void Dispose()
		{
			this._watch.Stop();
			double num = (double)this._watch.ElapsedMilliseconds / 1000.0;
			if (num > 99.0)
			{
				num = Math.Round(num);
			}
			this._ch.Info("{0}\t Time elapsed(s): {1}\n\n", new object[]
			{
				DateTime.Now,
				num
			});
		}

		// Token: 0x04000F50 RID: 3920
		private readonly IChannel _ch;

		// Token: 0x04000F51 RID: 3921
		private readonly Stopwatch _watch;
	}
}
