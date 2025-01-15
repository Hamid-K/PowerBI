using System;
using NLog.Config;
using NLog.Internal;

namespace NLog.Time
{
	// Token: 0x02000024 RID: 36
	[NLogConfigurationItem]
	public abstract class TimeSource
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060004AC RID: 1196
		public abstract DateTime Time { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00009183 File Offset: 0x00007383
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000918A File Offset: 0x0000738A
		public static TimeSource Current { get; set; } = new FastLocalTimeSource();

		// Token: 0x060004AF RID: 1199 RVA: 0x00009194 File Offset: 0x00007394
		public override string ToString()
		{
			TimeSourceAttribute customAttribute = base.GetType().GetCustomAttribute<TimeSourceAttribute>();
			if (customAttribute != null)
			{
				return customAttribute.Name + " (time source)";
			}
			return base.GetType().Name;
		}

		// Token: 0x060004B0 RID: 1200
		public abstract DateTime FromSystemTime(DateTime systemTime);
	}
}
