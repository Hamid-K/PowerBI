using System;

namespace NLog.Time
{
	// Token: 0x0200001F RID: 31
	[TimeSource("AccurateLocal")]
	public class AccurateLocalTimeSource : TimeSource
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000090CE File Offset: 0x000072CE
		public override DateTime Time
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000090D5 File Offset: 0x000072D5
		public override DateTime FromSystemTime(DateTime systemTime)
		{
			return systemTime.ToLocalTime();
		}
	}
}
