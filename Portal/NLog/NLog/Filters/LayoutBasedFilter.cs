using System;
using NLog.Config;
using NLog.Layouts;

namespace NLog.Filters
{
	// Token: 0x02000174 RID: 372
	public abstract class LayoutBasedFilter : Filter
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0002CCB3 File Offset: 0x0002AEB3
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x0002CCBB File Offset: 0x0002AEBB
		[RequiredParameter]
		public Layout Layout { get; set; }
	}
}
