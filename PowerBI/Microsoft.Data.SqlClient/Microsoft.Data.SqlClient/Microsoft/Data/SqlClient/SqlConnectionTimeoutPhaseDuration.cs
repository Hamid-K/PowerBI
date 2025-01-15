using System;
using System.Diagnostics;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000070 RID: 112
	internal class SqlConnectionTimeoutPhaseDuration
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0001B2B8 File Offset: 0x000194B8
		internal void StartCapture()
		{
			this._swDuration.Start();
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0001B2C5 File Offset: 0x000194C5
		internal void StopCapture()
		{
			if (this._swDuration.IsRunning)
			{
				this._swDuration.Stop();
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001B2DF File Offset: 0x000194DF
		internal long GetMilliSecondDuration()
		{
			return this._swDuration.ElapsedMilliseconds;
		}

		// Token: 0x0400020D RID: 525
		private Stopwatch _swDuration = new Stopwatch();
	}
}
