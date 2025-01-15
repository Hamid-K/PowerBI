using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000D3 RID: 211
	[LayoutRenderer("longdate")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class LongDateLayoutRenderer : LayoutRenderer
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x00020E38 File Offset: 0x0001F038
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x00020E40 File Offset: 0x0001F040
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00020E4C File Offset: 0x0001F04C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			builder.Append4DigitsZeroPadded(dateTime.Year);
			builder.Append('-');
			builder.Append2DigitsZeroPadded(dateTime.Month);
			builder.Append('-');
			builder.Append2DigitsZeroPadded(dateTime.Day);
			builder.Append(' ');
			builder.Append2DigitsZeroPadded(dateTime.Hour);
			builder.Append(':');
			builder.Append2DigitsZeroPadded(dateTime.Minute);
			builder.Append(':');
			builder.Append2DigitsZeroPadded(dateTime.Second);
			builder.Append('.');
			builder.Append4DigitsZeroPadded((int)(dateTime.Ticks % 10000000L) / 1000);
		}
	}
}
