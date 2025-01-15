using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002B9 RID: 697
	public class TimeToRun
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00041505 File Offset: 0x0003F705
		// (set) Token: 0x060012C9 RID: 4809 RVA: 0x0004150D File Offset: 0x0003F70D
		public bool IsNow { get; private set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00041516 File Offset: 0x0003F716
		public DateTime When
		{
			get
			{
				return this.m_when;
			}
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0004151E File Offset: 0x0003F71E
		public TimeToRun(bool isNow, DateTime when)
		{
			this.IsNow = isNow;
			this.m_when = when;
		}

		// Token: 0x040006F8 RID: 1784
		public static readonly DateTime Infinite = DateTime.MaxValue;

		// Token: 0x040006FA RID: 1786
		private DateTime m_when;
	}
}
