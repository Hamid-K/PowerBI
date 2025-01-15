using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D5 RID: 213
	[LayoutRenderer("mdc")]
	[ThreadSafe]
	public class MdcLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00020F97 File Offset: 0x0001F197
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x00020F9F File Offset: 0x0001F19F
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00020FA8 File Offset: 0x0001F1A8
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x00020FB0 File Offset: 0x0001F1B0
		public string Format { get; set; }

		// Token: 0x06000CDB RID: 3291 RVA: 0x00020FBC File Offset: 0x0001F1BC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = this.GetValue();
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
			builder.AppendFormattedValue(value, this.Format, formatProvider);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00020FE7 File Offset: 0x0001F1E7
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		private string GetStringValue(LogEventInfo logEvent)
		{
			if (this.Format != "@")
			{
				return FormatHelper.TryFormatToString(this.GetValue(), this.Format, base.GetFormatProvider(logEvent, null));
			}
			return null;
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002101F File Offset: 0x0001F21F
		private object GetValue()
		{
			return MappedDiagnosticsContext.GetObject(this.Item);
		}
	}
}
