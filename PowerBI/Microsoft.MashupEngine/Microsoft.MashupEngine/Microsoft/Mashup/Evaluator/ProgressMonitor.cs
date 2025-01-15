using System;
using System.Threading;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D0C RID: 7436
	internal class ProgressMonitor : IDisposable
	{
		// Token: 0x0600B97B RID: 47483 RVA: 0x00259404 File Offset: 0x00257604
		public ProgressMonitor(IProgressService2 progressService, IProgress progress)
		{
			this.progressService = progressService;
			this.progress = progress;
			this.syncRoot = new object();
			this.timer = new Timer(new TimerCallback(this.OnTimer), null, TimeSpan.Zero, TimeSpan.FromSeconds(1.0));
		}

		// Token: 0x0600B97C RID: 47484 RVA: 0x0025945C File Offset: 0x0025765C
		private void OnTimer(object obj)
		{
			object obj2 = this.syncRoot;
			lock (obj2)
			{
				if (!this.done)
				{
					this.Update();
				}
			}
		}

		// Token: 0x0600B97D RID: 47485 RVA: 0x002594A4 File Offset: 0x002576A4
		private void Update()
		{
			if (this.progressService != null)
			{
				this.progressService.RecordRowCount(this.progress.Rows, this.progress.ExceptionRows);
			}
		}

		// Token: 0x0600B97E RID: 47486 RVA: 0x002594D0 File Offset: 0x002576D0
		public void Dispose()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.done = true;
				this.Update();
			}
			this.timer.Dispose();
		}

		// Token: 0x04005E69 RID: 24169
		private readonly IProgressService2 progressService;

		// Token: 0x04005E6A RID: 24170
		private readonly IProgress progress;

		// Token: 0x04005E6B RID: 24171
		private readonly object syncRoot;

		// Token: 0x04005E6C RID: 24172
		private readonly Timer timer;

		// Token: 0x04005E6D RID: 24173
		private bool done;
	}
}
