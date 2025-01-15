using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CF RID: 207
	[LayoutRenderer("level")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class LevelLayoutRenderer : LayoutRenderer, IRawValue, IStringValueRenderer
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000202F7 File Offset: 0x0001E4F7
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x000202FF File Offset: 0x0001E4FF
		[DefaultValue(LevelFormat.Name)]
		public LevelFormat Format { get; set; }

		// Token: 0x06000C96 RID: 3222 RVA: 0x00020308 File Offset: 0x0001E508
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			LogLevel value = LevelLayoutRenderer.GetValue(logEvent);
			switch (this.Format)
			{
			case LevelFormat.Name:
				builder.Append(value.ToString());
				return;
			case LevelFormat.FirstCharacter:
				builder.Append(value.ToString()[0]);
				return;
			case LevelFormat.Ordinal:
				builder.AppendInvariant(value.Ordinal);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00020364 File Offset: 0x0001E564
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = LevelLayoutRenderer.GetValue(logEvent);
			return true;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002036F File Offset: 0x0001E56F
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			if (this.Format != LevelFormat.Name)
			{
				return null;
			}
			return LevelLayoutRenderer.GetValue(logEvent).ToString();
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00020386 File Offset: 0x0001E586
		private static LogLevel GetValue(LogEventInfo logEvent)
		{
			return logEvent.Level;
		}
	}
}
