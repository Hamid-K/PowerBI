using System;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D6 RID: 214
	[LayoutRenderer("mdlc")]
	[ThreadSafe]
	public class MdlcLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x00021034 File Offset: 0x0001F234
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x0002103C File Offset: 0x0001F23C
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00021045 File Offset: 0x0001F245
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0002104D File Offset: 0x0001F24D
		public string Format { get; set; }

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00021058 File Offset: 0x0001F258
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			object value = this.GetValue();
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, null);
			builder.AppendFormattedValue(value, this.Format, formatProvider);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00021083 File Offset: 0x0001F283
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002108C File Offset: 0x0001F28C
		private string GetStringValue(LogEventInfo logEvent)
		{
			if (this.Format != "@")
			{
				return FormatHelper.TryFormatToString(this.GetValue(), this.Format, base.GetFormatProvider(logEvent, null));
			}
			return null;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x000210BB File Offset: 0x0001F2BB
		private object GetValue()
		{
			return MappedDiagnosticsLogicalContext.GetObject(this.Item);
		}
	}
}
