using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E6 RID: 230
	[LayoutRenderer("shortdate")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class ShortDateLayoutRenderer : LayoutRenderer, IRawValue, IStringValueRenderer
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00022166 File Offset: 0x00020366
		// (set) Token: 0x06000D66 RID: 3430 RVA: 0x0002216E File Offset: 0x0002036E
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x06000D67 RID: 3431 RVA: 0x00022178 File Offset: 0x00020378
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string stringValue = this.GetStringValue(logEvent);
			builder.Append(stringValue);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00022198 File Offset: 0x00020398
		private string GetStringValue(LogEventInfo logEvent)
		{
			DateTime value = this.GetValue(logEvent);
			ShortDateLayoutRenderer.CachedDateFormatted cachedDateFormatted = this._cachedDateFormatted;
			if (cachedDateFormatted.Date != value.Date)
			{
				string text = value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
				cachedDateFormatted = (this._cachedDateFormatted = new ShortDateLayoutRenderer.CachedDateFormatted(value.Date, text));
			}
			return cachedDateFormatted.FormattedDate;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000221F8 File Offset: 0x000203F8
		private DateTime GetValue(LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			return dateTime;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00022220 File Offset: 0x00020420
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetValue(logEvent).Date;
			return true;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00022244 File Offset: 0x00020444
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x0400039B RID: 923
		private ShortDateLayoutRenderer.CachedDateFormatted _cachedDateFormatted = new ShortDateLayoutRenderer.CachedDateFormatted(DateTime.MaxValue, string.Empty);

		// Token: 0x0200025F RID: 607
		private class CachedDateFormatted
		{
			// Token: 0x060015FF RID: 5631 RVA: 0x00039DC2 File Offset: 0x00037FC2
			public CachedDateFormatted(DateTime date, string formattedDate)
			{
				this.Date = date;
				this.FormattedDate = formattedDate;
			}

			// Token: 0x0400068F RID: 1679
			public readonly DateTime Date;

			// Token: 0x04000690 RID: 1680
			public readonly string FormattedDate;
		}
	}
}
