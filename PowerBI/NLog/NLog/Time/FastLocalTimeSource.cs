using System;

namespace NLog.Time
{
	// Token: 0x02000022 RID: 34
	[TimeSource("FastLocal")]
	public class FastLocalTimeSource : CachedTimeSource
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x00009153 File Offset: 0x00007353
		protected override DateTime FreshTime
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000915A File Offset: 0x0000735A
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToLocalTime();
		}
	}
}
