using System;
using System.Globalization;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D9 RID: 217
	internal class MonthInputType : BaseInputType
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x0002F70D File Offset: 0x0002D90D
		public MonthInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x000306BC File Offset: 0x0002E8BC
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			DateTime? dateTime = MonthInputType.ConvertFromMonth(value);
			if (dateTime != null)
			{
				DateTime? dateTime2 = MonthInputType.ConvertFromMonth(base.Input.Minimum);
				DateTime? dateTime3 = MonthInputType.ConvertFromMonth(base.Input.Maximum);
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

		// Token: 0x06000659 RID: 1625 RVA: 0x000307CC File Offset: 0x0002E9CC
		public override double? ConvertToNumber(string value)
		{
			DateTime? dateTime = MonthInputType.ConvertFromMonth(value);
			if (dateTime != null)
			{
				return new double?((double)((dateTime.Value.Year - 1970) * 12 + dateTime.Value.Month - 1));
			}
			return null;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00030824 File Offset: 0x0002EA24
		public override string ConvertFromNumber(double value)
		{
			DateTime dateTime = BaseInputType.UnixEpoch.AddMonths((int)value);
			return this.ConvertFromDate(dateTime);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00030848 File Offset: 0x0002EA48
		public override DateTime? ConvertToDate(string value)
		{
			return MonthInputType.ConvertFromMonth(value);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00030850 File Offset: 0x0002EA50
		public override string ConvertFromDate(DateTime value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}", new object[] { value.Year, value.Month });
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00030888 File Offset: 0x0002EA88
		public override void DoStep(int n)
		{
			DateTime? dateTime = MonthInputType.ConvertFromMonth(base.Input.Value);
			if (dateTime != null)
			{
				DateTime dateTime2 = dateTime.Value.AddMilliseconds(base.GetStep() * (double)n);
				DateTime? dateTime3 = MonthInputType.ConvertFromMonth(base.Input.Minimum);
				DateTime? dateTime4 = MonthInputType.ConvertFromMonth(base.Input.Maximum);
				if ((dateTime3 == null || dateTime3.Value <= dateTime2) && (dateTime4 == null || dateTime4.Value >= dateTime2))
				{
					base.Input.ValueAsDate = new DateTime?(dateTime2);
				}
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected override double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected override double GetDefaultStep()
		{
			return 1.0;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected override double GetStepScaleFactor()
		{
			return 1.0;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0003092C File Offset: 0x0002EB2C
		protected static DateTime? ConvertFromMonth(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int num = BaseInputType.FetchDigits(value);
				if (MonthInputType.IsLegalPosition(value, num))
				{
					int num2 = int.Parse(value.Substring(0, num));
					int num3 = int.Parse(value.Substring(num + 1));
					if (BaseInputType.IsLegalDay(1, num3, num2))
					{
						return new DateTime?(new DateTime(num2, num3, 1, 0, 0, 0, 0, DateTimeKind.Utc));
					}
				}
			}
			return null;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00030994 File Offset: 0x0002EB94
		private static bool IsLegalPosition(string value, int position)
		{
			return position >= 4 && position == value.Length - 3 && value[position] == '-' && value[position + 1].IsDigit() && value[position + 2].IsDigit();
		}
	}
}
