using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A6 RID: 678
	internal sealed class Time
	{
		// Token: 0x060017F4 RID: 6132 RVA: 0x00041562 File Offset: 0x00040562
		internal Time()
		{
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0004156C File Offset: 0x0004056C
		internal Time(int minutes)
			: this()
		{
			if (minutes < 0 || minutes >= 1440)
			{
				throw new ArgumentException(Strings.MinutesMustBeBetween0And1439, "minutes");
			}
			this.Hours = minutes / 60;
			this.Minutes = minutes % 60;
			this.Seconds = 0;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000415BB File Offset: 0x000405BB
		internal Time(DateTime dateTime)
		{
			this.Hours = dateTime.Hour;
			this.Minutes = dateTime.Minute;
			this.Seconds = dateTime.Second;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000415EA File Offset: 0x000405EA
		internal Time(int hours, int minutes, int seconds)
			: this()
		{
			this.Hours = hours;
			this.Minutes = minutes;
			this.Seconds = seconds;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00041607 File Offset: 0x00040607
		internal string ToXSTime()
		{
			return string.Format("{0:00}:{1:00}:{2:00}", this.Hours, this.Minutes, this.Seconds);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00041634 File Offset: 0x00040634
		internal int ConvertToMinutes()
		{
			return this.Minutes + this.Hours * 60;
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00041646 File Offset: 0x00040646
		// (set) Token: 0x060017FB RID: 6139 RVA: 0x0004164E File Offset: 0x0004064E
		internal int Hours
		{
			get
			{
				return this.hours;
			}
			set
			{
				if (value >= 0 && value < 24)
				{
					this.hours = value;
					return;
				}
				throw new ArgumentException(Strings.HourMustBeBetween0And23);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00041670 File Offset: 0x00040670
		// (set) Token: 0x060017FD RID: 6141 RVA: 0x00041678 File Offset: 0x00040678
		internal int Minutes
		{
			get
			{
				return this.minutes;
			}
			set
			{
				if (value >= 0 && value < 60)
				{
					this.minutes = value;
					return;
				}
				throw new ArgumentException(Strings.MinuteMustBeBetween0And59);
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0004169A File Offset: 0x0004069A
		// (set) Token: 0x060017FF RID: 6143 RVA: 0x000416A2 File Offset: 0x000406A2
		internal int Seconds
		{
			get
			{
				return this.seconds;
			}
			set
			{
				if (value >= 0 && value < 60)
				{
					this.seconds = value;
					return;
				}
				throw new ArgumentException(Strings.SecondMustBeBetween0And59);
			}
		}

		// Token: 0x04001394 RID: 5012
		private int hours;

		// Token: 0x04001395 RID: 5013
		private int minutes;

		// Token: 0x04001396 RID: 5014
		private int seconds;
	}
}
