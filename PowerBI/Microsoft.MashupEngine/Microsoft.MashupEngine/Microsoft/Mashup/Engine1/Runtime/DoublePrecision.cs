using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D2 RID: 5586
	public sealed class DoublePrecision : Precision
	{
		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x06008C4F RID: 35919 RVA: 0x001D763A File Offset: 0x001D583A
		public override _ValueComparer Comparer
		{
			get
			{
				return _ValueComparer.LaxDouble;
			}
		}

		// Token: 0x06008C50 RID: 35920 RVA: 0x001D7641 File Offset: 0x001D5841
		public sealed override NumberValue Add(double x, double y)
		{
			return NumberValue.New(x + y);
		}

		// Token: 0x06008C51 RID: 35921 RVA: 0x001D764B File Offset: 0x001D584B
		public sealed override NumberValue Subtract(double x, double y)
		{
			return NumberValue.New(x - y);
		}

		// Token: 0x06008C52 RID: 35922 RVA: 0x001D7655 File Offset: 0x001D5855
		public sealed override NumberValue Add(NumberValue x, NumberValue y)
		{
			return NumberValue.New(x.AsDouble + y.AsDouble);
		}

		// Token: 0x06008C53 RID: 35923 RVA: 0x001D7669 File Offset: 0x001D5869
		public sealed override NumberValue Subtract(NumberValue x, NumberValue y)
		{
			return NumberValue.New(x.AsDouble - y.AsDouble);
		}

		// Token: 0x06008C54 RID: 35924 RVA: 0x001D767D File Offset: 0x001D587D
		public sealed override NumberValue Multiply(NumberValue x, NumberValue y)
		{
			return NumberValue.New(x.AsDouble * y.AsDouble);
		}

		// Token: 0x06008C55 RID: 35925 RVA: 0x001D7691 File Offset: 0x001D5891
		public sealed override NumberValue Divide(NumberValue x, NumberValue y)
		{
			return NumberValue.New(x.AsDouble / y.AsDouble);
		}

		// Token: 0x06008C56 RID: 35926 RVA: 0x001D76A5 File Offset: 0x001D58A5
		public sealed override NumberValue Mod(NumberValue x, NumberValue y)
		{
			return NumberValue.New(x.AsDouble % y.AsDouble);
		}

		// Token: 0x06008C57 RID: 35927 RVA: 0x001D76BC File Offset: 0x001D58BC
		public sealed override NumberValue IntegerDivide(NumberValue x, NumberValue y)
		{
			double asDouble = x.AsDouble;
			double asDouble2 = y.AsDouble;
			return NumberValue.New((asDouble - asDouble % asDouble2) / asDouble2);
		}

		// Token: 0x06008C58 RID: 35928 RVA: 0x001D76E1 File Offset: 0x001D58E1
		public override Accumulator GetNumberAccumulator()
		{
			return new DoublePrecision.DoubleNumberAccumulator();
		}

		// Token: 0x06008C59 RID: 35929 RVA: 0x001D76E8 File Offset: 0x001D58E8
		public sealed override NumberValue Acos(NumberValue value)
		{
			return NumberValue.New(Math.Acos(value.AsDouble));
		}

		// Token: 0x06008C5A RID: 35930 RVA: 0x001D76FA File Offset: 0x001D58FA
		public sealed override NumberValue Asin(NumberValue value)
		{
			return NumberValue.New(Math.Asin(value.AsDouble));
		}

		// Token: 0x06008C5B RID: 35931 RVA: 0x001D770C File Offset: 0x001D590C
		public sealed override NumberValue Atan(NumberValue value)
		{
			return NumberValue.New(Math.Atan(value.AsDouble));
		}

		// Token: 0x06008C5C RID: 35932 RVA: 0x001D771E File Offset: 0x001D591E
		public sealed override NumberValue Atan2(NumberValue y, NumberValue x)
		{
			return NumberValue.New(Math.Atan2(y.AsDouble, x.AsDouble));
		}

		// Token: 0x06008C5D RID: 35933 RVA: 0x001D7736 File Offset: 0x001D5936
		public sealed override NumberValue Combinations(NumberValue n, NumberValue k)
		{
			return NumberValue.New(DoublePrecision.Combinations(n.ToInt64(), k.ToInt64()));
		}

		// Token: 0x06008C5E RID: 35934 RVA: 0x001D7750 File Offset: 0x001D5950
		private static double Combinations(long n, long k)
		{
			if (k > n)
			{
				return 0.0;
			}
			if (n == k || k == 0L)
			{
				return 1.0;
			}
			if (k == 1L)
			{
				return (double)n;
			}
			double num = 1.0;
			k = Math.Min(k, n - k);
			long num2 = 1L;
			long num3 = n - k + 1L;
			while (num2 <= k && !double.IsInfinity(num))
			{
				num = (double)num3 * num / (double)num2;
				num2 += 1L;
				num3 += 1L;
			}
			return Math.Round(num);
		}

		// Token: 0x06008C5F RID: 35935 RVA: 0x001D77C9 File Offset: 0x001D59C9
		public sealed override NumberValue Cos(NumberValue value)
		{
			return NumberValue.New(Math.Cos(value.AsDouble));
		}

		// Token: 0x06008C60 RID: 35936 RVA: 0x001D77DB File Offset: 0x001D59DB
		public sealed override NumberValue Cosh(NumberValue value)
		{
			return NumberValue.New(Math.Cosh(value.AsDouble));
		}

		// Token: 0x06008C61 RID: 35937 RVA: 0x001D77ED File Offset: 0x001D59ED
		public sealed override NumberValue Exp(NumberValue value)
		{
			return NumberValue.New(Math.Exp(value.AsDouble));
		}

		// Token: 0x06008C62 RID: 35938 RVA: 0x001D77FF File Offset: 0x001D59FF
		public sealed override NumberValue Log(NumberValue value, NumberValue newBase)
		{
			if (newBase.Equals(NumberValue.Ten))
			{
				return NumberValue.New(Math.Log10(value.AsDouble));
			}
			return NumberValue.New(Math.Log(value.AsDouble, newBase.AsDouble));
		}

		// Token: 0x06008C63 RID: 35939 RVA: 0x001D7838 File Offset: 0x001D5A38
		public sealed override NumberValue Power(NumberValue number, NumberValue power)
		{
			double asDouble = number.AsNumber.AsDouble;
			double asDouble2 = power.AsNumber.AsDouble;
			if (DoublePrecision.IsIndeterminateForm(asDouble, asDouble2))
			{
				return NumberValue.NaN;
			}
			return NumberValue.New(Math.Pow(asDouble, asDouble2));
		}

		// Token: 0x06008C64 RID: 35940 RVA: 0x001D7878 File Offset: 0x001D5A78
		private static bool IsIndeterminateForm(double number, double power)
		{
			return (number == 0.0 && power == 0.0) || (Math.Abs(number) == 1.0 && double.IsInfinity(power)) || (double.IsInfinity(number) && power == 0.0);
		}

		// Token: 0x06008C65 RID: 35941 RVA: 0x001D78D1 File Offset: 0x001D5AD1
		public sealed override NumberValue Sin(NumberValue value)
		{
			return NumberValue.New(Math.Sin(value.AsDouble));
		}

		// Token: 0x06008C66 RID: 35942 RVA: 0x001D78E3 File Offset: 0x001D5AE3
		public sealed override NumberValue Sinh(NumberValue value)
		{
			return NumberValue.New(Math.Sinh(value.AsDouble));
		}

		// Token: 0x06008C67 RID: 35943 RVA: 0x001D78F5 File Offset: 0x001D5AF5
		public sealed override NumberValue Sqrt(NumberValue value)
		{
			return NumberValue.New(Math.Sqrt(value.AsDouble));
		}

		// Token: 0x06008C68 RID: 35944 RVA: 0x001D7907 File Offset: 0x001D5B07
		public sealed override NumberValue Tan(NumberValue value)
		{
			return NumberValue.New(Math.Tan(value.AsDouble));
		}

		// Token: 0x06008C69 RID: 35945 RVA: 0x001D7919 File Offset: 0x001D5B19
		public sealed override NumberValue Tanh(NumberValue value)
		{
			return NumberValue.New(Math.Tanh(value.AsDouble));
		}

		// Token: 0x020015D3 RID: 5587
		private class DoubleNumberAccumulator : Accumulator
		{
			// Token: 0x06008C6B RID: 35947 RVA: 0x001D7933 File Offset: 0x001D5B33
			public DoubleNumberAccumulator()
			{
				this.value = 0.0;
			}

			// Token: 0x06008C6C RID: 35948 RVA: 0x001D794A File Offset: 0x001D5B4A
			public override void Add(Value value)
			{
				this.value += value.AsNumber.AsDouble;
			}

			// Token: 0x06008C6D RID: 35949 RVA: 0x001D7964 File Offset: 0x001D5B64
			public override void Subtract(Value value)
			{
				this.value -= value.AsNumber.AsDouble;
			}

			// Token: 0x06008C6E RID: 35950 RVA: 0x001D797E File Offset: 0x001D5B7E
			public override void Divide(Value value)
			{
				this.value /= value.AsNumber.AsDouble;
			}

			// Token: 0x06008C6F RID: 35951 RVA: 0x001D7998 File Offset: 0x001D5B98
			public override Value ToValue()
			{
				return NumberValue.New(this.value);
			}

			// Token: 0x04004CB8 RID: 19640
			private double value;
		}
	}
}
