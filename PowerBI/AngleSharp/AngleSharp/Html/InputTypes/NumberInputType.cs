using System;
using System.Globalization;
using AngleSharp.Dom.Html;

namespace AngleSharp.Html.InputTypes
{
	// Token: 0x020000DA RID: 218
	internal class NumberInputType : BaseInputType
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0002F70D File Offset: 0x0002D90D
		public NumberInputType(IHtmlInputElement input, string name)
			: base(input, name, true)
		{
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000309D0 File Offset: 0x0002EBD0
		public override double? ConvertToNumber(string value)
		{
			return BaseInputType.ToNumber(value);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000309D8 File Offset: 0x0002EBD8
		public override string ConvertFromNumber(double value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000309E8 File Offset: 0x0002EBE8
		public override void Check(ValidityState state)
		{
			string value = base.Input.Value;
			double? num = this.ConvertToNumber(value);
			state.Reset();
			if (num != null)
			{
				double? num2 = this.ConvertToNumber(base.Input.Minimum);
				double? num3 = this.ConvertToNumber(base.Input.Maximum);
				state.IsRangeUnderflow = num2 != null && num < num2.Value;
				state.IsRangeOverflow = num3 != null && num > num3.Value;
				state.IsValueMissing = false;
				state.IsStepMismatch = base.IsStepMismatch();
				return;
			}
			state.IsRangeUnderflow = false;
			state.IsRangeOverflow = false;
			state.IsValueMissing = base.Input.IsRequired;
			state.IsStepMismatch = false;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00030AE0 File Offset: 0x0002ECE0
		public override void DoStep(int n)
		{
			double? num = BaseInputType.ToNumber(base.Input.Value);
			if (num != null)
			{
				double num2 = num.Value + base.GetStep() * (double)n;
				double? num3 = BaseInputType.ToNumber(base.Input.Minimum);
				double? num4 = BaseInputType.ToNumber(base.Input.Maximum);
				if ((num3 == null || num3.Value <= num2) && (num4 == null || num4.Value >= num2))
				{
					base.Input.ValueAsNumber = num2;
				}
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0002F2B1 File Offset: 0x0002D4B1
		protected override double GetDefaultStepBase()
		{
			return 0.0;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected override double GetDefaultStep()
		{
			return 1.0;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		protected override double GetStepScaleFactor()
		{
			return 1.0;
		}
	}
}
