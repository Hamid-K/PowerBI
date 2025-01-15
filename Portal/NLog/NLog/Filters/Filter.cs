using System;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000171 RID: 369
	[NLogConfigurationItem]
	public abstract class Filter
	{
		// Token: 0x06001140 RID: 4416 RVA: 0x0002CC79 File Offset: 0x0002AE79
		protected Filter()
		{
			this.Action = FilterResult.Neutral;
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001141 RID: 4417 RVA: 0x0002CC88 File Offset: 0x0002AE88
		// (set) Token: 0x06001142 RID: 4418 RVA: 0x0002CC90 File Offset: 0x0002AE90
		[RequiredParameter]
		public FilterResult Action { get; set; }

		// Token: 0x06001143 RID: 4419 RVA: 0x0002CC99 File Offset: 0x0002AE99
		internal FilterResult GetFilterResult(LogEventInfo logEvent)
		{
			return this.Check(logEvent);
		}

		// Token: 0x06001144 RID: 4420
		protected abstract FilterResult Check(LogEventInfo logEvent);
	}
}
