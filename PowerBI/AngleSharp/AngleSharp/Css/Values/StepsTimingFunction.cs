using System;

namespace AngleSharp.Css.Values
{
	// Token: 0x0200012A RID: 298
	public sealed class StepsTimingFunction : ITimingFunction
	{
		// Token: 0x06000983 RID: 2435 RVA: 0x0003F0FD File Offset: 0x0003D2FD
		public StepsTimingFunction(int intervals, bool start = false)
		{
			this._intervals = Math.Max(1, intervals);
			this._start = start;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0003F119 File Offset: 0x0003D319
		public int Intervals
		{
			get
			{
				return this._intervals;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0003F121 File Offset: 0x0003D321
		public bool IsStart
		{
			get
			{
				return this._start;
			}
		}

		// Token: 0x040008DD RID: 2269
		private readonly int _intervals;

		// Token: 0x040008DE RID: 2270
		private readonly bool _start;
	}
}
