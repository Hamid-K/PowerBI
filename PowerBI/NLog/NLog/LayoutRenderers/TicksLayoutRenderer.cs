using System;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000ED RID: 237
	[LayoutRenderer("ticks")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class TicksLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x000225BC File Offset: 0x000207BC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(TicksLayoutRenderer.GetValue(logEvent).ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000225E3 File Offset: 0x000207E3
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = TicksLayoutRenderer.GetValue(logEvent);
			return true;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000225F4 File Offset: 0x000207F4
		private static long GetValue(LogEventInfo logEvent)
		{
			return logEvent.TimeStamp.Ticks;
		}
	}
}
