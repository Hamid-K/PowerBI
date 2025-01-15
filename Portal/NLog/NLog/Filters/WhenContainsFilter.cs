using System;
using System.ComponentModel;
using NLog.Config;

namespace NLog.Filters
{
	// Token: 0x02000175 RID: 373
	[Filter("whenContains")]
	public class WhenContainsFilter : LayoutBasedFilter
	{
		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0002CCC4 File Offset: 0x0002AEC4
		// (set) Token: 0x0600114A RID: 4426 RVA: 0x0002CCCC File Offset: 0x0002AECC
		[DefaultValue(false)]
		public bool IgnoreCase { get; set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x0002CCD5 File Offset: 0x0002AED5
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x0002CCDD File Offset: 0x0002AEDD
		[RequiredParameter]
		public string Substring { get; set; }

		// Token: 0x0600114D RID: 4429 RVA: 0x0002CCE8 File Offset: 0x0002AEE8
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			StringComparison stringComparison = (this.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			if (base.Layout.Render(logEvent).IndexOf(this.Substring, stringComparison) >= 0)
			{
				return base.Action;
			}
			return FilterResult.Neutral;
		}
	}
}
