using System;
using System.Globalization;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000DE RID: 222
	internal class TimeInputType : BaseInputType
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x0002F70D File Offset: 0x0002D90D
		public TimeInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00030C3C File Offset: 0x0002EE3C
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			DateTime? dateTime = TimeInputType.ConvertFromTime(value);
			if (dateTime != null)
			{
				DateTime? dateTime2 = TimeInputType.ConvertFromTime(base.Input.Minimum);
				DateTime? dateTime3 = TimeInputType.ConvertFromTime(base.Input.Maximum);
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

		// Token: 0x06000674 RID: 1652 RVA: 0x00030D4C File Offset: 0x0002EF4C
		public override double? ConvertToNumber(string value)
		{
			DateTime? dateTime = TimeInputType.ConvertFromTime(value);
			if (dateTime != null)
			{
				return new double?(dateTime.Value.Subtract(default(DateTime)).TotalMilliseconds);
			}
			return null;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00030D9C File Offset: 0x0002EF9C
		public override string ConvertFromNumber(double value)
		{
			DateTime dateTime = default(DateTime).AddMilliseconds(value);
			return this.ConvertFromDate(dateTime);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		public override DateTime? ConvertToDate(string value)
		{
			DateTime? dateTime = TimeInputType.ConvertFromTime(value);
			if (dateTime != null)
			{
				return new DateTime?(BaseInputType.UnixEpoch.Add(dateTime.Value.Subtract(default(DateTime))));
			}
			return null;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00030E18 File Offset: 0x0002F018
		public override string ConvertFromDate(DateTime value)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00},{3:000}", new object[] { value.Hour, value.Minute, value.Second, value.Millisecond });
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00030E78 File Offset: 0x0002F078
		public override void DoStep(int n)
		{
			DateTime? dateTime = TimeInputType.ConvertFromTime(base.Input.Value);
			if (dateTime != null)
			{
				DateTime dateTime2 = dateTime.Value.AddMilliseconds(base.GetStep() * (double)n);
				DateTime? dateTime3 = TimeInputType.ConvertFromTime(base.Input.Minimum);
				DateTime? dateTime4 = TimeInputType.ConvertFromTime(base.Input.Maximum);
				if ((dateTime3 == null || dateTime3.Value <= dateTime2) && (dateTime4 == null || dateTime4.Value >= dateTime2))
				{
					base.Input.ValueAsDate = new DateTime?(dateTime2);
				}
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected override double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0002FE4C File Offset: 0x0002E04C
		protected override double GetDefaultStep()
		{
			return 60.0;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0002FE57 File Offset: 0x0002E057
		protected override double GetStepScaleFactor()
		{
			return 1000.0;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00030F1C File Offset: 0x0002F11C
		protected static DateTime? ConvertFromTime(string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				int num = 0;
				TimeSpan? timeSpan = BaseInputType.ToTime(value, ref num);
				if (timeSpan != null && num == value.Length)
				{
					return new DateTime?(new DateTime(0L, DateTimeKind.Utc).Add(timeSpan.Value));
				}
			}
			return null;
		}
	}
}
