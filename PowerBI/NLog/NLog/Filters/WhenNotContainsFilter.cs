using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000177 RID: 375
	[Filter("whenNotContains")]
	public class WhenNotContainsFilter : LayoutBasedFilter
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0002CD94 File Offset: 0x0002AF94
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0002CD9C File Offset: 0x0002AF9C
		[RequiredParameter]
		public string Substring { get; set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x0002CDA5 File Offset: 0x0002AFA5
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x0002CDAD File Offset: 0x0002AFAD
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x06001159 RID: 4441 RVA: 0x0002CDB8 File Offset: 0x0002AFB8
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison stringComparison = (this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (base.Layout.Render(logEvent).IndexOf(this.Substring, stringComparison) < 0)
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}
