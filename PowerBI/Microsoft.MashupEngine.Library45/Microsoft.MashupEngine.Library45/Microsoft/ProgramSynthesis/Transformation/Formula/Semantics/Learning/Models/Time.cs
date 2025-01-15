using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models
{
	// Token: 0x020016EE RID: 5870
	public readonly struct Time : IEquatable<Time>
	{
		// Token: 0x0600C3C0 RID: 50112 RVA: 0x002A1B84 File Offset: 0x0029FD84
		public Time(int hour, int minute, int second)
		{
			bool flag = hour < 0 || hour > 23;
			bool flag2 = flag;
			if (!flag2)
			{
				bool flag3 = minute < 0 || minute > 59;
				flag2 = flag3;
			}
			bool flag4 = flag2;
			if (!flag4)
			{
				bool flag3 = second < 0 || second > 59;
				flag4 = flag3;
			}
			if (flag4)
			{
				throw new ArgumentException(string.Format("Invalid time. ({0:00}:{1:00}:{2:00})", hour, minute, second));
			}
			this._timeSpan = new TimeSpan(hour, minute, second);
		}

		// Token: 0x0600C3C1 RID: 50113 RVA: 0x002A1BFF File Offset: 0x0029FDFF
		public Time(TimeSpan timeSpan)
		{
			this = new Time(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		}

		// Token: 0x0600C3C2 RID: 50114 RVA: 0x002A1C1C File Offset: 0x0029FE1C
		public Time(DateTime date)
		{
			this._timeSpan = date - date.Date;
		}

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x0600C3C3 RID: 50115 RVA: 0x002A1C34 File Offset: 0x0029FE34
		public int Hours
		{
			get
			{
				return this._timeSpan.Hours;
			}
		}

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x0600C3C4 RID: 50116 RVA: 0x002A1C50 File Offset: 0x0029FE50
		public int Minutes
		{
			get
			{
				return this._timeSpan.Minutes;
			}
		}

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x0600C3C5 RID: 50117 RVA: 0x002A1C6C File Offset: 0x0029FE6C
		public int Seconds
		{
			get
			{
				return this._timeSpan.Seconds;
			}
		}

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x0600C3C6 RID: 50118 RVA: 0x002A1C88 File Offset: 0x0029FE88
		public double TotalHours
		{
			get
			{
				return this._timeSpan.TotalHours;
			}
		}

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x0600C3C7 RID: 50119 RVA: 0x002A1CA4 File Offset: 0x0029FEA4
		public double TotalMinutes
		{
			get
			{
				return this._timeSpan.TotalMinutes;
			}
		}

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x0600C3C8 RID: 50120 RVA: 0x002A1CC0 File Offset: 0x0029FEC0
		public double TotalSeconds
		{
			get
			{
				return this._timeSpan.TotalSeconds;
			}
		}

		// Token: 0x0600C3C9 RID: 50121 RVA: 0x002A1CDB File Offset: 0x0029FEDB
		public bool Equals(Time other)
		{
			return this._timeSpan == other._timeSpan;
		}

		// Token: 0x0600C3CA RID: 50122 RVA: 0x002A1CF0 File Offset: 0x0029FEF0
		public override bool Equals(object other)
		{
			if (other is Time)
			{
				Time time = (Time)other;
				return this.Equals(time);
			}
			return false;
		}

		// Token: 0x0600C3CB RID: 50123 RVA: 0x002A1D15 File Offset: 0x0029FF15
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600C3CC RID: 50124 RVA: 0x002A1D28 File Offset: 0x0029FF28
		public static bool operator ==(Time left, Time right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600C3CD RID: 50125 RVA: 0x002A1D32 File Offset: 0x0029FF32
		public static bool operator !=(Time left, Time right)
		{
			return !(left == right);
		}

		// Token: 0x0600C3CE RID: 50126 RVA: 0x002A1D40 File Offset: 0x0029FF40
		public override string ToString()
		{
			return this._timeSpan.ToString();
		}

		// Token: 0x0600C3CF RID: 50127 RVA: 0x002A1D64 File Offset: 0x0029FF64
		public double ToOADate()
		{
			return (new DateTime(1899, 12, 30) + this._timeSpan).ToOADate();
		}

		// Token: 0x04004C46 RID: 19526
		private readonly TimeSpan _timeSpan;
	}
}
