using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E42 RID: 7746
	public sealed class NotifyingStreamSource : IStreamSource, IDisposable
	{
		// Token: 0x0600BE75 RID: 48757 RVA: 0x00268410 File Offset: 0x00266610
		public NotifyingStreamSource(IStreamSource streamSource, Action callback)
		{
			this.streamSource = streamSource;
			this.callback = callback;
		}

		// Token: 0x17002ED9 RID: 11993
		// (get) Token: 0x0600BE76 RID: 48758 RVA: 0x00268426 File Offset: 0x00266626
		public Stream Stream
		{
			get
			{
				return this.streamSource.Stream;
			}
		}

		// Token: 0x0600BE77 RID: 48759 RVA: 0x00268433 File Offset: 0x00266633
		public void Dispose()
		{
			if (this.callback != null)
			{
				Action action = this.callback;
				this.callback = null;
				action();
			}
			if (this.streamSource != null)
			{
				this.streamSource.Dispose();
				this.streamSource = null;
			}
		}

		// Token: 0x04006100 RID: 24832
		private IStreamSource streamSource;

		// Token: 0x04006101 RID: 24833
		private Action callback;
	}
}
