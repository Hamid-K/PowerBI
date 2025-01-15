using System;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200032E RID: 814
	internal class LightWeightEventMonitorBased
	{
		// Token: 0x06001D57 RID: 7511 RVA: 0x000587BA File Offset: 0x000569BA
		internal LightWeightEventMonitorBased()
			: this(false)
		{
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000587C3 File Offset: 0x000569C3
		internal LightWeightEventMonitorBased(bool initialState)
		{
			this._signal = initialState;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000587DD File Offset: 0x000569DD
		public bool WaitOne()
		{
			return this.WaitOne(-1);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000587E6 File Offset: 0x000569E6
		public bool WaitOne(TimeSpan timeout)
		{
			return this.WaitOne((int)timeout.TotalMilliseconds);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000587F8 File Offset: 0x000569F8
		public bool WaitOne(int millisecondsTimeout)
		{
			if (!this._signal)
			{
				lock (this._lockObject)
				{
					if (this._signal)
					{
						return true;
					}
					return Monitor.Wait(this._lockObject, millisecondsTimeout);
				}
				return true;
			}
			return true;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00058858 File Offset: 0x00056A58
		public void Set()
		{
			lock (this._lockObject)
			{
				this._signal = true;
				Monitor.Pulse(this._lockObject);
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x000588A4 File Offset: 0x00056AA4
		public void Reset()
		{
			lock (this._lockObject)
			{
				this._signal = false;
			}
		}

		// Token: 0x0400103F RID: 4159
		private object _lockObject = new object();

		// Token: 0x04001040 RID: 4160
		private bool _signal;
	}
}
