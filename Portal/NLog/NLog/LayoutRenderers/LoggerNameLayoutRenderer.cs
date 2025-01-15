using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D2 RID: 210
	[LayoutRenderer("logger")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class LoggerNameLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00020D7A File Offset: 0x0001EF7A
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x00020D82 File Offset: 0x0001EF82
		[DefaultValue(false)]
		public bool ShortName { get; set; }

		// Token: 0x06000CCC RID: 3276 RVA: 0x00020D8C File Offset: 0x0001EF8C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this.ShortName)
			{
				int num = this.TryGetLastDotForShortName(logEvent);
				if (num >= 0)
				{
					builder.Append(logEvent.LoggerName, num + 1, logEvent.LoggerName.Length - num - 1);
					return;
				}
			}
			builder.Append(logEvent.LoggerName);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00020DDC File Offset: 0x0001EFDC
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			if (this.ShortName)
			{
				int num = this.TryGetLastDotForShortName(logEvent);
				if (num >= 0)
				{
					return logEvent.LoggerName.Substring(num + 1);
				}
			}
			return logEvent.LoggerName ?? string.Empty;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00020E1B File Offset: 0x0001F01B
		private int TryGetLastDotForShortName(LogEventInfo logEvent)
		{
			string loggerName = logEvent.LoggerName;
			if (loggerName == null)
			{
				return -1;
			}
			return loggerName.LastIndexOf('.');
		}
	}
}
