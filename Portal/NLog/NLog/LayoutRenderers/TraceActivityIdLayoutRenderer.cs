using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EF RID: 239
	[LayoutRenderer("activityid")]
	[ThreadSafe]
	public class TraceActivityIdLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x00022721 File Offset: 0x00020921
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(TraceActivityIdLayoutRenderer.GetStringValue());
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002272F File Offset: 0x0002092F
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return TraceActivityIdLayoutRenderer.GetStringValue();
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00022738 File Offset: 0x00020938
		private static string GetStringValue()
		{
			Guid value = TraceActivityIdLayoutRenderer.GetValue();
			if (!Guid.Empty.Equals(value))
			{
				return value.ToString("D", CultureInfo.InvariantCulture);
			}
			return string.Empty;
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00022772 File Offset: 0x00020972
		private static Guid GetValue()
		{
			return Trace.CorrelationManager.ActivityId;
		}
	}
}
