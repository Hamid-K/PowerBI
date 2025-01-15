using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E2 RID: 226
	[LayoutRenderer("processtime")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class ProcessTimeLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00021B30 File Offset: 0x0001FD30
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00021B38 File Offset: 0x0001FD38
		[DefaultValue(false)]
		public bool Invariant { get; set; }

		// Token: 0x06000D3E RID: 3390 RVA: 0x00021B44 File Offset: 0x0001FD44
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			TimeSpan value = ProcessTimeLayoutRenderer.GetValue(logEvent);
			CultureInfo cultureInfo = (this.Invariant ? null : base.GetCulture(logEvent, null));
			ProcessTimeLayoutRenderer.WritetTimestamp(builder, value, cultureInfo);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00021B74 File Offset: 0x0001FD74
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = ProcessTimeLayoutRenderer.GetValue(logEvent);
			return true;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00021B84 File Offset: 0x0001FD84
		internal static void WritetTimestamp(StringBuilder builder, TimeSpan ts, CultureInfo culture)
		{
			string text = ":";
			string text2 = ".";
			if (culture != null)
			{
				text = culture.DateTimeFormat.TimeSeparator;
				text2 = culture.NumberFormat.NumberDecimalSeparator;
			}
			builder.Append2DigitsZeroPadded(ts.Hours);
			builder.Append(text);
			builder.Append2DigitsZeroPadded(ts.Minutes);
			builder.Append(text);
			builder.Append2DigitsZeroPadded(ts.Seconds);
			builder.Append(text2);
			int milliseconds = ts.Milliseconds;
			if (milliseconds < 100)
			{
				builder.Append('0');
				if (milliseconds < 10)
				{
					builder.Append('0');
					if (milliseconds < 0)
					{
						builder.Append('0');
						return;
					}
				}
			}
			builder.AppendInvariant(milliseconds);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00021C30 File Offset: 0x0001FE30
		private static TimeSpan GetValue(LogEventInfo logEvent)
		{
			return logEvent.TimeStamp.ToUniversalTime() - LogEventInfo.ZeroDate;
		}
	}
}
