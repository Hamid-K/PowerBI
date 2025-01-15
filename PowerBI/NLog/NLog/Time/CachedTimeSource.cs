using System;

namespace NLog.Time
{
	// Token: 0x02000021 RID: 33
	public abstract class CachedTimeSource : TimeSource
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060004A3 RID: 1187
		protected abstract DateTime FreshTime { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x00009100 File Offset: 0x00007300
		public override DateTime Time
		{
			get
			{
				int tickCount = Environment.TickCount;
				if (tickCount == this._lastTicks)
				{
					return this._lastTime;
				}
				DateTime freshTime = this.FreshTime;
				this._lastTicks = tickCount;
				this._lastTime = freshTime;
				return freshTime;
			}
		}

		// Token: 0x0400004F RID: 79
		private int _lastTicks = -1;

		// Token: 0x04000050 RID: 80
		private DateTime _lastTime = DateTime.MinValue;
	}
}
