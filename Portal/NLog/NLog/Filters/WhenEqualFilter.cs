using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000176 RID: 374
	[Filter("whenEqual")]
	public class WhenEqualFilter : LayoutBasedFilter
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x0002CD2D File Offset: 0x0002AF2D
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x0002CD35 File Offset: 0x0002AF35
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x0002CD3E File Offset: 0x0002AF3E
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0002CD46 File Offset: 0x0002AF46
		[RequiredParameter]
		public string CompareTo { get; set; }

		// Token: 0x06001153 RID: 4435 RVA: 0x0002CD50 File Offset: 0x0002AF50
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison stringComparison = (this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (base.Layout.Render(logEvent).Equals(this.CompareTo, stringComparison))
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}
