using System;
using System.Globalization;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000D3 RID: 211
	internal class DateInputType : BaseInputType
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0002F70D File Offset: 0x0002D90D
		public DateInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0002F7FC File Offset: 0x0002D9FC
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			DateTime? dateTime = DateInputType.ConvertFromDate(value);
			if (dateTime != null)
			{
				DateTime? dateTime2 = DateInputType.ConvertFromDate(base.Input.Minimum);
				DateTime? dateTime3 = DateInputType.ConvertFromDate(base.Input.Maximum);
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

		// Token: 0x0600062A RID: 1578 RVA: 0x0002F90C File Offset: 0x0002DB0C
		public override double? ConvertToNumber(string value)
		{
			DateTime? dateTime = DateInputType.ConvertFromDate(value);
			if (dateTime != null)
			{
				return new double?(dateTime.Value.Subtract(BaseInputType.UnixEpoch).TotalMilliseconds);
			}
			return null;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0002F954 File Offset: 0x0002DB54
		public override string ConvertFromNumber(double value)
		{
			DateTime dateTime = BaseInputType.UnixEpoch.AddMilliseconds(value);
			return this.ConvertFromDate(dateTime);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0002F977 File Offset: 0x0002DB77
		public override DateTime? ConvertToDate(string value)
		{
			return DateInputType.ConvertFromDate(value);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0002F980 File Offset: 0x0002DB80
		public override string ConvertFromDate(DateTime value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:0000}-{1:00}-{2:00}", new object[] { value.Year, value.Month, value.Day });
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0002F9D0 File Offset: 0x0002DBD0
		public override void DoStep(int n)
		{
			DateTime? dateTime = DateInputType.ConvertFromDate(base.Input.Value);
			if (dateTime != null)
			{
				DateTime dateTime2 = dateTime.Value.AddMilliseconds(base.GetStep() * (double)n);
				DateTime? dateTime3 = DateInputType.ConvertFromDate(base.Input.Minimum);
				DateTime? dateTime4 = DateInputType.ConvertFromDate(base.Input.Maximum);
				if ((dateTime3 == null || dateTime3.Value <= dateTime2) && (dateTime4 == null || dateTime4.Value >= dateTime2))
				{
					base.Input.ValueAsDate = new DateTime?(dateTime2);
				}
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected override double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected override double GetDefaultStep()
		{
			return 1.0;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0002FA74 File Offset: 0x0002DC74
		protected override double GetStepScaleFactor()
		{
			return 86400000.0;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002FA80 File Offset: 0x0002DC80
		protected static DateTime? ConvertFromDate(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int num = BaseInputType.FetchDigits(value);
				if (DateInputType.IsLegalPosition(value, num))
				{
					int num2 = int.Parse(value.Substring(0, num));
					int num3 = int.Parse(value.Substring(num + 1, 2));
					int num4 = int.Parse(value.Substring(num + 4, 2));
					if (BaseInputType.IsLegalDay(num4, num3, num2))
					{
						return new DateTime?(new DateTime(num2, num3, num4, 0, 0, 0, 0, DateTimeKind.Utc));
					}
				}
			}
			return null;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002FAFC File Offset: 0x0002DCFC
		private static bool IsLegalPosition(string value, int position)
		{
			return position >= 4 && position == value.Length - 6 && value[position] == '-' && value[position + 1].IsDigit() && value[position + 2].IsDigit() && value[position + 3] == '-' && value[position + 4].IsDigit() && value[position + 5].IsDigit();
		}
	}
}
