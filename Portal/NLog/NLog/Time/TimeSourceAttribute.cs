using System;
using NLog.Config;

namespace NLog.Time
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TimeSourceAttribute : NameBaseAttribute
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x000091E0 File Offset: 0x000073E0
		public TimeSourceAttribute(string name)
			: base(name)
		{
		}
	}
}
