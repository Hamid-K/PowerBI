using System;
using System.Text;
using NLog.Config;
using NLog.Time;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000DA RID: 218
	[LayoutRenderer("ndlctiming")]
	[ThreadSafe]
	public class NdlcTimingLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002149D File Offset: 0x0001F69D
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x000214A5 File Offset: 0x0001F6A5
		public bool CurrentScope { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x000214AE File Offset: 0x0001F6AE
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x000214B6 File Offset: 0x0001F6B6
		public bool ScopeBeginTime { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x000214BF File Offset: 0x0001F6BF
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x000214C7 File Offset: 0x0001F6C7
		public string Format { get; set; }

		// Token: 0x06000D0A RID: 3338 RVA: 0x000214D0 File Offset: 0x0001F6D0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = (this.CurrentScope ? NestedDiagnosticsLogicalContext.PeekTopScopeBeginTime() : NestedDiagnosticsLogicalContext.PeekBottomScopeBeginTime());
			if (dateTime != DateTime.MinValue)
			{
				if (this.ScopeBeginTime)
				{
					IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
					builder.Append(TimeSource.Current.FromSystemTime(dateTime).ToString(this.Format, formatProvider));
					return;
				}
				TimeSpan timeSpan = ((dateTime != DateTime.MinValue) ? (DateTime.UtcNow - dateTime) : TimeSpan.Zero);
				if (timeSpan < TimeSpan.Zero)
				{
					timeSpan = TimeSpan.Zero;
				}
				IFormatProvider formatProvider2 = base.GetFormatProvider(logEvent, null);
				builder.Append(timeSpan.ToString(this.Format, formatProvider2));
			}
		}
	}
}
