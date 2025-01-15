using System;
using System.Globalization;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D4 RID: 212
	internal class DatetimeInputType : BaseInputType
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0002F70D File Offset: 0x0002D90D
		public DatetimeInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0002FB70 File Offset: 0x0002DD70
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			DateTime? dateTime = DatetimeInputType.ConvertFromDateTime(value);
			if (dateTime != null)
			{
				DateTime? dateTime2 = DatetimeInputType.ConvertFromDateTime(base.Input.Minimum);
				DateTime? dateTime3 = DatetimeInputType.ConvertFromDateTime(base.Input.Maximum);
				state.IsRangeUnderflow = dateTime2 != null && dateTime < dateTime2.Value;
				state.IsRangeOverflow = dateTime3 != null && dateTime > dateTime3.Value;
				state.IsValueMissing = false;
				state.IsBadInput = false;
				state.IsStepMismatch = base.IsStepMismatch();
				return;
			}
			state.IsRangeUnderflow = false;
			state.IsRangeOverflow = false;
			state.IsStepMismatch = false;
			state.IsValueMissing = base.Input.IsRequired;
			state.IsBadInput = !string.IsNullOrEmpty(value);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0002FC80 File Offset: 0x0002DE80
		public override double? ConvertToNumber(string value)
		{
			DateTime? dateTime = DatetimeInputType.ConvertFromDateTime(value);
			if (dateTime != null)
			{
				return new double?(dateTime.Value.Subtract(BaseInputType.UnixEpoch).TotalMilliseconds);
			}
			return null;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0002FCC8 File Offset: 0x0002DEC8
		public override string ConvertFromNumber(double value)
		{
			DateTime dateTime = BaseInputType.UnixEpoch.AddMilliseconds(value);
			return this.ConvertFromDate(dateTime);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0002FCEB File Offset: 0x0002DEEB
		public override DateTime? ConvertToDate(string value)
		{
			return DatetimeInputType.ConvertFromDateTime(value);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0002FCF4 File Offset: 0x0002DEF4
		public override string ConvertFromDate(DateTime value)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", new object[] { value.Year, value.Month, value.Day });
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00},{3:000}", new object[] { value.Hour, value.Minute, value.Second, value.Millisecond });
			return text + "T" + text2 + "Z";
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0002FDA8 File Offset: 0x0002DFA8
		public override void DoStep(int n)
		{
			DateTime? dateTime = DatetimeInputType.ConvertFromDateTime(base.Input.Value);
			if (dateTime != null)
			{
				DateTime dateTime2 = dateTime.Value.AddMilliseconds(base.GetStep() * (double)n);
				DateTime? dateTime3 = DatetimeInputType.ConvertFromDateTime(base.Input.Minimum);
				DateTime? dateTime4 = DatetimeInputType.ConvertFromDateTime(base.Input.Maximum);
				if ((dateTime3 == null || dateTime3.Value <= dateTime2) && (dateTime4 == null || dateTime4.Value >= dateTime2))
				{
					base.Input.ValueAsDate = new DateTime?(dateTime2);
				}
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected override double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0002FE4C File Offset: 0x0002E04C
		protected override double GetDefaultStep()
		{
			return 60.0;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0002FE57 File Offset: 0x0002E057
		protected override double GetStepScaleFactor()
		{
			return 1000.0;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002FE64 File Offset: 0x0002E064
		protected static DateTime? ConvertFromDateTime(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int num = BaseInputType.FetchDigits(value);
				if (BaseInputType.PositionIsValidForDateTime(value, num))
				{
					int num2 = int.Parse(value.Substring(0, num));
					int num3 = int.Parse(value.Substring(num + 1, 2));
					int num4 = int.Parse(value.Substring(num + 4, 2));
					num += 6;
					if (BaseInputType.IsLegalDay(num4, num3, num2) && BaseInputType.IsTimeSeparator(value[num]))
					{
						bool flag = value[num++] == ' ';
						TimeSpan? timeSpan = BaseInputType.ToTime(value, ref num);
						DateTime dateTime = new DateTime(num2, num3, num4, 0, 0, 0, DateTimeKind.Utc);
						if (timeSpan != null)
						{
							dateTime = dateTime.Add(timeSpan.Value);
							if (num != value.Length)
							{
								if (value[num] != 'Z')
								{
									if (!DatetimeInputType.IsLegalPosition(value, num))
									{
										return null;
									}
									int num5 = int.Parse(value.Substring(num + 1, 2));
									int num6 = int.Parse(value.Substring(num + 4, 2));
									TimeSpan timeSpan2 = new TimeSpan(num5, num6, 0);
									if (value[num] == '+')
									{
										dateTime = dateTime.Add(timeSpan2);
									}
									else
									{
										if (value[num] != '-')
										{
											return null;
										}
										dateTime = dateTime.Subtract(timeSpan2);
									}
								}
								else
								{
									if (num + 1 != value.Length)
									{
										return null;
									}
									dateTime = dateTime.ToUniversalTime();
								}
								return new DateTime?(dateTime);
							}
							if (flag)
							{
								return null;
							}
							return new DateTime?(dateTime);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00030000 File Offset: 0x0002E200
		private static bool IsLegalPosition(string value, int position)
		{
			return position == value.Length - 6 && value[position + 1].IsDigit() && value[position + 2].IsDigit() && value[position + 3] == ':' && value[position + 4].IsDigit() && value[position + 5].IsDigit();
		}
	}
}
