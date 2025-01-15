using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BC RID: 188
	[LayoutRenderer("date")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class DateLayoutRenderer : LayoutRenderer, IRawValue, IStringValueRenderer
	{
		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001EC2B File Offset: 0x0001CE2B
		public DateLayoutRenderer()
		{
			this.Format = "yyyy/MM/dd HH:mm:ss.fff";
			this.Culture = CultureInfo.InvariantCulture;
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0001EC5E File Offset: 0x0001CE5E
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0001EC66 File Offset: 0x0001CE66
		public CultureInfo Culture { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0001EC6F File Offset: 0x0001CE6F
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0001EC78 File Offset: 0x0001CE78
		[DefaultParameter]
		public string Format
		{
			get
			{
				return this._format;
			}
			set
			{
				this._format = value;
				if (DateLayoutRenderer.IsLowTimeResolutionLayout(this._format))
				{
					this._cachedDateFormatted = new DateLayoutRenderer.CachedDateFormatted(DateTime.MaxValue, string.Empty);
					return;
				}
				this._cachedDateFormatted = new DateLayoutRenderer.CachedDateFormatted(DateTime.MinValue, string.Empty);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0001ECC4 File Offset: 0x0001CEC4
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0001ECCC File Offset: 0x0001CECC
		[DefaultValue(false)]
		public bool UniversalTime { get; set; }

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001ECD5 File Offset: 0x0001CED5
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.GetStringValue(logEvent));
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001ECE5 File Offset: 0x0001CEE5
		bool IRawValue.TryGetRawValue(LogEventInfo logEvent, out object value)
		{
			value = this.GetDate(logEvent);
			return true;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0001ECF6 File Offset: 0x0001CEF6
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue(logEvent);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0001ED00 File Offset: 0x0001CF00
		private string GetStringValue(LogEventInfo logEvent)
		{
			IFormatProvider formatProvider = base.GetFormatProvider(logEvent, this.Culture);
			DateTime date = this.GetDate(logEvent);
			DateLayoutRenderer.CachedDateFormatted cachedDateFormatted = this._cachedDateFormatted;
			if (formatProvider != CultureInfo.InvariantCulture || cachedDateFormatted.Date == DateTime.MinValue)
			{
				cachedDateFormatted = null;
			}
			else if (cachedDateFormatted.Date == date.Date.AddHours((double)date.Hour))
			{
				return cachedDateFormatted.FormattedDate;
			}
			string text = date.ToString(this._format, formatProvider);
			if (cachedDateFormatted != null)
			{
				this._cachedDateFormatted = new DateLayoutRenderer.CachedDateFormatted(date.Date.AddHours((double)date.Hour), text);
			}
			return text;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0001EDAC File Offset: 0x0001CFAC
		private DateTime GetDate(LogEventInfo logEvent)
		{
			DateTime dateTime = logEvent.TimeStamp;
			if (this.UniversalTime)
			{
				dateTime = dateTime.ToUniversalTime();
			}
			return dateTime;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		private static bool IsLowTimeResolutionLayout(string dateTimeFormat)
		{
			foreach (char c in dateTimeFormat)
			{
				if (char.IsLetter(c) && "YyMDdHh".IndexOf(c) < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040002EB RID: 747
		private string _format;

		// Token: 0x040002EC RID: 748
		private const string _lowTimeResolutionChars = "YyMDdHh";

		// Token: 0x040002ED RID: 749
		private DateLayoutRenderer.CachedDateFormatted _cachedDateFormatted = new DateLayoutRenderer.CachedDateFormatted(DateTime.MinValue, string.Empty);

		// Token: 0x0200025A RID: 602
		private class CachedDateFormatted
		{
			// Token: 0x060015ED RID: 5613 RVA: 0x00039D0C File Offset: 0x00037F0C
			public CachedDateFormatted(DateTime date, string formattedDate)
			{
				this.Date = date;
				this.FormattedDate = formattedDate;
			}

			// Token: 0x04000683 RID: 1667
			public readonly DateTime Date;

			// Token: 0x04000684 RID: 1668
			public readonly string FormattedDate;
		}
	}
}
