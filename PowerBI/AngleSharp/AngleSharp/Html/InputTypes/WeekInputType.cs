using System;
using System.Globalization;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000E0 RID: 224
	internal class WeekInputType : BaseInputType
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0002F70D File Offset: 0x0002D90D
		public WeekInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00031000 File Offset: 0x0002F200
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			DateTime? dateTime = WeekInputType.ConvertFromWeek(value);
			if (dateTime != null)
			{
				DateTime? dateTime2 = WeekInputType.ConvertFromWeek(base.Input.Minimum);
				DateTime? dateTime3 = WeekInputType.ConvertFromWeek(base.Input.Maximum);
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

		// Token: 0x06000682 RID: 1666 RVA: 0x00031110 File Offset: 0x0002F310
		public override double? ConvertToNumber(string value)
		{
			DateTime? dateTime = WeekInputType.ConvertFromWeek(value);
			if (dateTime != null)
			{
				return new double?(dateTime.Value.Subtract(BaseInputType.UnixEpoch).TotalMilliseconds);
			}
			return null;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00031158 File Offset: 0x0002F358
		public override string ConvertFromNumber(double value)
		{
			DateTime dateTime = BaseInputType.UnixEpoch.AddMilliseconds(value);
			return this.ConvertFromDate(dateTime);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0003117B File Offset: 0x0002F37B
		public override DateTime? ConvertToDate(string value)
		{
			return WeekInputType.ConvertFromWeek(value);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00031184 File Offset: 0x0002F384
		public override string ConvertFromDate(DateTime value)
		{
			int weekOfYear = BaseInputType.GetWeekOfYear(value);
			return string.Format(CultureInfo.InvariantCulture, "{0:0000}-W{1:00}", new object[] { value.Year, weekOfYear });
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000311C8 File Offset: 0x0002F3C8
		public override void DoStep(int n)
		{
			DateTime? dateTime = WeekInputType.ConvertFromWeek(base.Input.Value);
			if (dateTime != null)
			{
				DateTime dateTime2 = dateTime.Value.AddMilliseconds(base.GetStep() * (double)n);
				DateTime? dateTime3 = WeekInputType.ConvertFromWeek(base.Input.Minimum);
				DateTime? dateTime4 = WeekInputType.ConvertFromWeek(base.Input.Maximum);
				if ((dateTime3 == null || dateTime3.Value <= dateTime2) && (dateTime4 == null || dateTime4.Value >= dateTime2))
				{
					base.Input.ValueAsDate = new DateTime?(dateTime2);
				}
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0003126C File Offset: 0x0002F46C
		protected override double GetDefaultStepBase()
		{
			return -259200000.0;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected override double GetDefaultStep()
		{
			return 1.0;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00031277 File Offset: 0x0002F477
		protected override double GetStepScaleFactor()
		{
			return 604800000.0;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00031284 File Offset: 0x0002F484
		protected static DateTime? ConvertFromWeek(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int num = BaseInputType.FetchDigits(value);
				if (WeekInputType.IsLegalPosition(value, num))
				{
					int num2 = int.Parse(value.Substring(0, num));
					int num3 = int.Parse(value.Substring(num + 2)) - 1;
					if (BaseInputType.IsLegalWeek(num3, num2))
					{
						DateTime dateTime = new DateTime(num2, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
						DayOfWeek dayOfWeek = dateTime.DayOfWeek;
						if (dayOfWeek == DayOfWeek.Sunday)
						{
							dateTime = dateTime.AddDays(-6.0);
						}
						else if (dayOfWeek > DayOfWeek.Monday)
						{
							dateTime = dateTime.AddDays((double)(DayOfWeek.Monday - dayOfWeek));
						}
						return new DateTime?(dateTime.AddDays((double)(7 * num3)));
					}
				}
			}
			return null;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00031330 File Offset: 0x0002F530
		private static bool IsLegalPosition(string value, int position)
		{
			return position >= 4 && position == value.Length - 4 && value[position] == '-' && value[position + 1] == 'W' && value[position + 2].IsDigit() && value[position + 3].IsDigit();
		}
	}
}
