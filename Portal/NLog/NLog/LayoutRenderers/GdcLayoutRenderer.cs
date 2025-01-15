using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000C7 RID: 199
	[LayoutRenderer("gdc")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class GdcLayoutRenderer : LayoutRenderer, IRawValue, IStringValueRenderer
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0001FBA3 File Offset: 0x0001DDA3
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0001FBAB File Offset: 0x0001DDAB
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0001FBBC File Offset: 0x0001DDBC
		public string Format { get; set; }

		// Token: 0x06000C57 RID: 3159 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = this.GetValue();
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
			builder.AppendFormattedValue(value, this.Format, formatProvider);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetValue();
			return true;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001FBFE File Offset: 0x0001DDFE
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0001FC07 File Offset: 0x0001DE07
		private string GetStringValue(LogEventInfo logEvent)
		{
			if (this.Format != "@")
			{
				return FormatHelper.TryFormatToString(this.GetValue(), this.Format, base.GetFormatProvider(logEvent, null));
			}
			return null;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0001FC36 File Offset: 0x0001DE36
		private object GetValue()
		{
			return GlobalDiagnosticsContext.GetObject(this.Item);
		}
	}
}
