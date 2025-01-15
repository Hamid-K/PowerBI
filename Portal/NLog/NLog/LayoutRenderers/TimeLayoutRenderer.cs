using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000EE RID: 238
	[LayoutRenderer("time")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class TimeLayoutRenderer : LayoutRenderer, IRawValue
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00022617 File Offset: 0x00020817
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x0002261F File Offset: 0x0002081F
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x00022628 File Offset: 0x00020828
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00022630 File Offset: 0x00020830
		[DefaultValue(false)]
		public bool Invariant { get; set; }

		// Token: 0x06000D99 RID: 3481 RVA: 0x0002263C File Offset: 0x0002083C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime value = this.GetValue(logEvent);
			string text = ":";
			string text2 = ".";
			if (!this.Invariant)
			{
				CultureInfo culture = base.GetCulture(logEvent, null);
				if (culture != null)
				{
					text = culture.DateTimeFormat.TimeSeparator;
					text2 = culture.NumberFormat.NumberDecimalSeparator;
				}
			}
			builder.Append2DigitsZeroPadded(value.Hour);
			builder.Append(text);
			builder.Append2DigitsZeroPadded(value.Minute);
			builder.Append(text);
			builder.Append2DigitsZeroPadded(value.Second);
			builder.Append(text2);
			builder.Append4DigitsZeroPadded((int)(value.Ticks % 10000000L) / 1000);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000226E3 File Offset: 0x000208E3
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetValue(logEvent);
			return true;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000226F4 File Offset: 0x000208F4
		private DateTime GetValue(LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			return dateTime;
		}
	}
}
