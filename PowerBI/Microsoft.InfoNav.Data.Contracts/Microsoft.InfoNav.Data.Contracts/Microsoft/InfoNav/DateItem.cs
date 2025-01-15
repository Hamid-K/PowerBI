using System;
using System.ComponentModel;
using System.Text;

namespace Microsoft.InfoNav
{
	// Token: 0x02000064 RID: 100
	[ImmutableObject(true)]
	public sealed class DateItem : IEquatable<DateItem>
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x00005B6A File Offset: 0x00003D6A
		public DateItem(int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null, int? millisecond = null)
		{
			this._year = year;
			this._month = month;
			this._day = day;
			this._hour = hour;
			this._minute = minute;
			this._second = second;
			this._millisecond = millisecond;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public DateItem(DateTime value)
			: this(new int?(value.Year), new int?(value.Month), new int?(value.Day), new int?(value.Hour), new int?(value.Minute), new int?(value.Second), new int?(value.Millisecond))
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005C10 File Offset: 0x00003E10
		public DateItem(TimeSpan value)
		{
			int? num = new int?(value.Hours);
			int? num2 = new int?(value.Minutes);
			int? num3 = new int?(value.Seconds);
			int? num4 = new int?(value.Milliseconds);
			this..ctor(null, null, null, num, num2, num3, num4);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00005C7D File Offset: 0x00003E7D
		public int? Year
		{
			get
			{
				return this._year;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00005C85 File Offset: 0x00003E85
		public int? Month
		{
			get
			{
				return this._month;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00005C8D File Offset: 0x00003E8D
		public int? Day
		{
			get
			{
				return this._day;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005C95 File Offset: 0x00003E95
		public int? Hour
		{
			get
			{
				return this._hour;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00005C9D File Offset: 0x00003E9D
		public int? Minute
		{
			get
			{
				return this._minute;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005CA5 File Offset: 0x00003EA5
		public int? Second
		{
			get
			{
				return this._second;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00005CAD File Offset: 0x00003EAD
		public int? Millisecond
		{
			get
			{
				return this._millisecond;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public override bool Equals(object obj)
		{
			DateItem dateItem = obj as DateItem;
			return this.Equals(dateItem);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005CD4 File Offset: 0x00003ED4
		public bool Equals(DateItem other)
		{
			if (this == other)
			{
				return true;
			}
			if (other != null)
			{
				int? num = other._year;
				int? num2 = this._year;
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					num2 = other._month;
					num = this._month;
					if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
					{
						num = other._day;
						num2 = this._day;
						if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
						{
							num2 = other._hour;
							num = this._hour;
							if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
							{
								num = other._minute;
								num2 = this._minute;
								if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
								{
									num2 = other._second;
									num = this._second;
									if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
									{
										num = other._millisecond;
										num2 = this._millisecond;
										return (num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null));
									}
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00005E50 File Offset: 0x00004050
		public override int GetHashCode()
		{
			int num = Hashing.CombineHash(DateItem.GetHashCode(this._year), DateItem.GetHashCode(this._month), DateItem.GetHashCode(this._day));
			int num2 = Hashing.CombineHash(DateItem.GetHashCode(this._hour), DateItem.GetHashCode(this._minute), DateItem.GetHashCode(this._second), DateItem.GetHashCode(this._millisecond));
			return Hashing.CombineHash(num, num2);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00005EBC File Offset: 0x000040BC
		public override string ToString()
		{
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder(24);
			if (this._year == null)
			{
				stringBuilder.Append("_");
			}
			else
			{
				stringBuilder.AppendFormat("{0:0000}", this._year.Value);
				flag = true;
			}
			if (this._month == null)
			{
				if (flag)
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append("-_");
			}
			else
			{
				stringBuilder.AppendFormat("-{0:00}", this._month.Value);
				flag = true;
			}
			if (this._day == null)
			{
				if (flag)
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append("-_");
			}
			else
			{
				stringBuilder.AppendFormat("-{0:00}", this._day.Value);
				flag = true;
			}
			if (this._hour == null)
			{
				if (flag)
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append(" _");
			}
			else
			{
				if (flag)
				{
					stringBuilder.Append('T');
				}
				stringBuilder.AppendFormat("{0:00}", this._hour.Value);
				flag = true;
			}
			if (this._minute == null)
			{
				if (flag)
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append(":_");
			}
			else
			{
				stringBuilder.AppendFormat(":{0:00}", this._minute.Value);
				flag = true;
			}
			if (this._second == null)
			{
				if (flag)
				{
					return stringBuilder.ToString();
				}
				stringBuilder.Append(":_");
			}
			else
			{
				stringBuilder.AppendFormat(":{0:00}", this._second.Value);
			}
			if (this._millisecond != null)
			{
				stringBuilder.AppendFormat(".{0:000}", this._millisecond.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006096 File Offset: 0x00004296
		public static implicit operator DateItem(DateTime value)
		{
			return new DateItem(value);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000609E File Offset: 0x0000429E
		public static implicit operator DateItem(TimeSpan value)
		{
			return new DateItem(value);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000060A8 File Offset: 0x000042A8
		private static int GetHashCode(int? value)
		{
			if (value != null)
			{
				return value.Value.GetHashCode();
			}
			return -1;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000060D0 File Offset: 0x000042D0
		private static bool IsValid(params int?[] args)
		{
			int num = 0;
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] != null)
				{
					if (num == -1)
					{
						return false;
					}
					num = 1;
				}
				else if (num == 1)
				{
					num = -1;
				}
			}
			return num != 0;
		}

		// Token: 0x04000139 RID: 313
		private readonly int? _year;

		// Token: 0x0400013A RID: 314
		private readonly int? _month;

		// Token: 0x0400013B RID: 315
		private readonly int? _day;

		// Token: 0x0400013C RID: 316
		private readonly int? _hour;

		// Token: 0x0400013D RID: 317
		private readonly int? _minute;

		// Token: 0x0400013E RID: 318
		private readonly int? _second;

		// Token: 0x0400013F RID: 319
		private readonly int? _millisecond;
	}
}
