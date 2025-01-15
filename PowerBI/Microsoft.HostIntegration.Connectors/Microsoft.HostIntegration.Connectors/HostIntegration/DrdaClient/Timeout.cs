using System;
using System.Timers;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A3D RID: 2621
	internal class Timeout
	{
		// Token: 0x060051EC RID: 20972 RVA: 0x0014E1A8 File Offset: 0x0014C3A8
		internal Timeout(double interval, TimeoutCallback callback, object state)
		{
			this._expireOn = DateTime.Now.Add(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(interval)));
			this._callback = callback;
			this._state = state;
			if (interval <= 0.0)
			{
				return;
			}
			this._timer = new Timer(interval);
			this._timer.Elapsed += this.TimeElapsed;
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x060051ED RID: 20973 RVA: 0x0014E21B File Offset: 0x0014C41B
		// (set) Token: 0x060051EE RID: 20974 RVA: 0x0014E23C File Offset: 0x0014C43C
		internal double Interval
		{
			get
			{
				if (this._timer == null)
				{
					return 0.0;
				}
				return this._timer.Interval;
			}
			set
			{
				if (this._timer != null && this._timer.Interval != value)
				{
					bool enabled = this._timer.Enabled;
					this._timer.Enabled = false;
					if (value == 0.0)
					{
						this._timer.Interval = Convert.ToDouble(int.MaxValue);
					}
					else
					{
						this._timer.Interval = value;
					}
					this._expireOn = DateTime.Now.Add(new TimeSpan(0, 0, 0, 0, Convert.ToInt32(this._timer.Interval)));
					this._timer.Enabled = enabled;
				}
			}
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x0014E2E1 File Offset: 0x0014C4E1
		internal void Start()
		{
			if (this._timer != null)
			{
				this._timer.Start();
			}
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0014E2F6 File Offset: 0x0014C4F6
		internal void Stop()
		{
			if (this._timer != null)
			{
				this._timer.Stop();
			}
		}

		// Token: 0x060051F1 RID: 20977 RVA: 0x0014E30C File Offset: 0x0014C50C
		internal void Dispose()
		{
			lock (this)
			{
				if (this._timer != null)
				{
					this._timer.Stop();
					this._timer.Dispose();
					this._timer = null;
				}
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x060051F2 RID: 20978 RVA: 0x0014E368 File Offset: 0x0014C568
		internal DateTime ExpiresOn
		{
			get
			{
				return this._expireOn;
			}
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x0014E370 File Offset: 0x0014C570
		private void TimeElapsed(object sender, ElapsedEventArgs e)
		{
			this._timer.Enabled = false;
			if (this._callback != null)
			{
				this._callback(this._state);
			}
		}

		// Token: 0x0400406D RID: 16493
		private TimeoutCallback _callback;

		// Token: 0x0400406E RID: 16494
		private Timer _timer;

		// Token: 0x0400406F RID: 16495
		private DateTime _expireOn;

		// Token: 0x04004070 RID: 16496
		private object _state;
	}
}
