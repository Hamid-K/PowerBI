using System;
using System.Text;
using NLog.Config;

namespace NLog.Layouts
{
	// Token: 0x020000A9 RID: 169
	[Layout("LayoutWithHeaderAndFooter")]
	[ThreadAgnostic]
	[ThreadSafe]
	[AppDomainFixedOutput]
	public class LayoutWithHeaderAndFooter : Layout
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001C756 File Offset: 0x0001A956
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0001C75E File Offset: 0x0001A95E
		public Layout Layout { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0001C767 File Offset: 0x0001A967
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0001C76F File Offset: 0x0001A96F
		public Layout Header { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0001C778 File Offset: 0x0001A978
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0001C780 File Offset: 0x0001A980
		public Layout Footer { get; set; }

		// Token: 0x06000AEA RID: 2794 RVA: 0x0001C789 File Offset: 0x0001A989
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			return this.Layout.Render(logEvent);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0001C797 File Offset: 0x0001A997
		protected override void RenderFormattedMessage(LogEventInfo logEvent, StringBuilder target)
		{
			this.Layout.RenderAppendBuilder(logEvent, target, false);
		}
	}
}
