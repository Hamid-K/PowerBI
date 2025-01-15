using System;

namespace NLog.Time
{
	// Token: 0x02000020 RID: 32
	[TimeSource("AccurateUTC")]
	public class AccurateUtcTimeSource : TimeSource
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000090E6 File Offset: 0x000072E6
		public override DateTime Time
		{
			get
			{
				return DateTime.UtcNow;
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000090ED File Offset: 0x000072ED
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToUniversalTime();
		}
	}
}
