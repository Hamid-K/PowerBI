using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C0 RID: 192
	[LayoutRenderer("event-context")]
	[MutableUnsafe]
	[Obsolete("Use EventPropertiesLayoutRenderer class instead. Marked obsolete on NLog 2.0")]
	public class EventContextLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x0001F0A0 File Offset: 0x0001D2A0
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x0001F0A8 File Offset: 0x0001D2A8
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.HasProperties && logEvent.Properties.TryGetValue(this.Item, out obj))
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				builder.Append(Convert.ToString(obj, formatProvider));
			}
		}
	}
}
