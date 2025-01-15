using System;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CB RID: 203
	[LayoutRenderer("install-context")]
	[ThreadSafe]
	public class InstallContextLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00020029 File Offset: 0x0001E229
		// (set) Token: 0x06000C7A RID: 3194 RVA: 0x00020031 File Offset: 0x0001E231
		[RequiredParameter]
		[DefaultParameter]
		public string Parameter { get; set; }

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002003C File Offset: 0x0001E23C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = this.GetValue(logEvent);
			if (value != null)
			{
				IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
				builder.Append(Convert.ToString(value, formatProvider));
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002006C File Offset: 0x0001E26C
		private object GetValue(LogEventInfo logEvent)
		{
			object obj;
			if (logEvent.Properties.TryGetValue(this.Parameter, out obj))
			{
				return obj;
			}
			return null;
		}
	}
}
