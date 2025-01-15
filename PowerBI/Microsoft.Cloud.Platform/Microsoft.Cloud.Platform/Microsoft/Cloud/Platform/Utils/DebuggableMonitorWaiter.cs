using System;
using System.Diagnostics;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F7 RID: 759
	public class DebuggableMonitorWaiter
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x00046228 File Offset: 0x00044428
		public DebuggableMonitorWaiter([NotNull] object monitor, int timeout)
		{
			Ensure.ArgNotNull<object>(monitor, "monitor");
			Ensure.ArgSatisfiesCondition("timeout", timeout > 0 || timeout == -1, "timeout cannot be 0 or negative");
			this.m_monitor = monitor;
			this.m_timeout = timeout;
			if (this.m_timeout != -1)
			{
				this.m_sw = Stopwatch.StartNew();
			}
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00046284 File Offset: 0x00044484
		public bool TryWait()
		{
			bool flag = ExtendedDiagnostics.IsDebuggerAttached();
			int num = ((this.m_timeout != -1) ? Math.Max(0, this.m_timeout - (int)this.m_sw.ElapsedMilliseconds) : this.m_timeout);
			if (flag && (num < 0 || num > 10000))
			{
				num = 10000;
			}
			return Monitor.Wait(this.m_monitor, num) || num != 0;
		}

		// Token: 0x040007AE RID: 1966
		private object m_monitor;

		// Token: 0x040007AF RID: 1967
		private int m_timeout;

		// Token: 0x040007B0 RID: 1968
		private Stopwatch m_sw;
	}
}
