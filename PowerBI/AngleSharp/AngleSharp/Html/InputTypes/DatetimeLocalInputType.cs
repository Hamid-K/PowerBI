using System;
using System.Globalization;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D5 RID: 213
	internal class DatetimeLocalInputType : BaseInputType
	{
		// Token: 0x06000640 RID: 1600 RVA: 0x0002F70D File Offset: 0x0002D90D
		public DatetimeLocalInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00030068 File Offset: 0x0002E268
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			DateTime? dateTime = DatetimeLocalInputType.ConvertFromDateTime(value);
			if (dateTime != null)
			{
				DateTime? dateTime2 = DatetimeLocalInputType.ConvertFromDateTime(base.Input.Minimum);
				DateTime? dateTime3 = DatetimeLocalInputType.ConvertFromDateTime(base.Input.Maximum);
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

		// Token: 0x06000642 RID: 1602 RVA: 0x00030178 File Offset: 0x0002E378
		public override double? ConvertToNumber(string value)
		{
			DateTime? dateTime = DatetimeLocalInputType.ConvertFromDateTime(value);
			if (dateTime != null)
			{
				return new double?(dateTime.Value.ToUniversalTime().Subtract(BaseInputType.UnixEpoch).TotalMilliseconds);
			}
			return null;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000301C8 File Offset: 0x0002E3C8
		public override string ConvertFromNumber(double value)
		{
			DateTime dateTime = BaseInputType.UnixEpoch.AddMilliseconds(value);
			return this.ConvertFromDate(dateTime);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000301EB File Offset: 0x0002E3EB
		public override DateTime? ConvertToDate(string value)
		{
			return DatetimeLocalInputType.ConvertFromDateTime(value);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000301F4 File Offset: 0x0002E3F4
		public override string ConvertFromDate(DateTime value)
		{
			value = value.ToLocalTime();
			string text = string.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", new object[] { value.Year, value.Month, value.Day });
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00},{3:000}", new object[] { value.Hour, value.Minute, value.Second, value.Millisecond });
			return text + "T" + text2;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000302AC File Offset: 0x0002E4AC
		public override void DoStep(int n)
		{
			DateTime? dateTime = DatetimeLocalInputType.ConvertFromDateTime(base.Input.Value);
			if (dateTime != null)
			{
				DateTime dateTime2 = dateTime.Value.AddMilliseconds(base.GetStep() * (double)n);
				DateTime? dateTime3 = DatetimeLocalInputType.ConvertFromDateTime(base.Input.Minimum);
				DateTime? dateTime4 = DatetimeLocalInputType.ConvertFromDateTime(base.Input.Maximum);
				if ((dateTime3 == null || dateTime3.Value <= dateTime2) && (dateTime4 == null || dateTime4.Value >= dateTime2))
				{
					base.Input.ValueAsDate = new DateTime?(dateTime2);
				}
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected override double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0002FE4C File Offset: 0x0002E04C
		protected override double GetDefaultStep()
		{
			return 60.0;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002FE57 File Offset: 0x0002E057
		protected override double GetStepScaleFactor()
		{
			return 1000.0;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00030350 File Offset: 0x0002E550
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
						num++;
						TimeSpan? timeSpan = BaseInputType.ToTime(value, ref num);
						DateTime dateTime = new DateTime(num2, num3, num4, 0, 0, 0, DateTimeKind.Local);
						if (timeSpan != null)
						{
							dateTime = dateTime.Add(timeSpan.Value);
							if (num == value.Length)
							{
								return new DateTime?(dateTime);
							}
						}
					}
				}
			}
			return null;
		}
	}
}
