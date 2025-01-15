using System;

namespace NLog.Time
{
	// Token: 0x02000023 RID: 35
	[TimeSource("FastUTC")]
	public class FastUtcTimeSource : CachedTimeSource
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000916B File Offset: 0x0000736B
		protected override DateTime FreshTime
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00009172 File Offset: 0x00007372
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToUniversalTime();
		}
	}
}
