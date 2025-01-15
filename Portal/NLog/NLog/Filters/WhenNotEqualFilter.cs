using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000178 RID: 376
	[Filter("whenNotEqual")]
	public class WhenNotEqualFilter : LayoutBasedFilter
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0002CE05 File Offset: 0x0002B005
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x0002CE0D File Offset: 0x0002B00D
		[RequiredParameter]
		public string CompareTo { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0002CE16 File Offset: 0x0002B016
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x0002CE1E File Offset: 0x0002B01E
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x06001160 RID: 4448 RVA: 0x0002CE28 File Offset: 0x0002B028
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison stringComparison = (this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (!base.Layout.Render(logEvent).Equals(this.CompareTo, stringComparison))
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}
