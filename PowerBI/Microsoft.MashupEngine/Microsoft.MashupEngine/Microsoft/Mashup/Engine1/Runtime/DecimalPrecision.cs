using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D4 RID: 5588
	public sealed class DecimalPrecision : Precision
	{
		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x06008C70 RID: 35952 RVA: 0x001D79A5 File Offset: 0x001D5BA5
		public override _ValueComparer Comparer
		{
			get
			{
				return _ValueComparer.LaxDecimal;
			}
		}

		// Token: 0x06008C71 RID: 35953 RVA: 0x001D79AC File Offset: 0x001D5BAC
		public sealed override NumberValue Add(double x, double y)
		{
			return this.Add(NumberValue.New(x), NumberValue.New(y));
		}

		// Token: 0x06008C72 RID: 35954 RVA: 0x001D79C0 File Offset: 0x001D5BC0
		public sealed override NumberValue Subtract(double x, double y)
		{
			return this.Subtract(NumberValue.New(x), NumberValue.New(y));
		}

		// Token: 0x06008C73 RID: 35955 RVA: 0x001D79D4 File Offset: 0x001D5BD4
		public sealed override NumberValue Add(NumberValue x, NumberValue y)
		{
			try
			{
				return NumberValue.New(x.AsDecimal + y.AsDecimal);
			}
			catch (OverflowException)
			{
			}
			return DecimalPrecision.Overflow(x.AsDouble + y.AsDouble);
		}

		// Token: 0x06008C74 RID: 35956 RVA: 0x001D7A24 File Offset: 0x001D5C24
		public sealed override NumberValue Subtract(NumberValue x, NumberValue y)
		{
			try
			{
				return NumberValue.New(x.AsDecimal - y.AsDecimal);
			}
			catch (OverflowException)
			{
			}
			return DecimalPrecision.Overflow(x.AsDouble - y.AsDouble);
		}

		// Token: 0x06008C75 RID: 35957 RVA: 0x001D7A74 File Offset: 0x001D5C74
		public sealed override NumberValue Multiply(NumberValue x, NumberValue y)
		{
			try
			{
				return NumberValue.New(x.AsDecimal * y.AsDecimal);
			}
			catch (OverflowException)
			{
			}
			return DecimalPrecision.Overflow(x.AsDouble * y.AsDouble);
		}

		// Token: 0x06008C76 RID: 35958 RVA: 0x001D7AC4 File Offset: 0x001D5CC4
		public sealed override NumberValue Divide(NumberValue x, NumberValue y)
		{
			try
			{
				return NumberValue.New(x.AsDecimal / y.AsDecimal);
			}
			catch (DivideByZeroException)
			{
			}
			catch (OverflowException)
			{
			}
			return DecimalPrecision.Overflow(x.AsDouble / y.AsDouble);
		}

		// Token: 0x06008C77 RID: 35959 RVA: 0x001D7B20 File Offset: 0x001D5D20
		public sealed override NumberValue Mod(NumberValue x, NumberValue y)
		{
			try
			{
				return NumberValue.New(x.AsDecimal % y.AsDecimal);
			}
			catch (DivideByZeroException)
			{
			}
			catch (OverflowException)
			{
			}
			return DecimalPrecision.Overflow(x.AsDouble % y.AsDouble);
		}

		// Token: 0x06008C78 RID: 35960 RVA: 0x001D7B7C File Offset: 0x001D5D7C
		public sealed override NumberValue IntegerDivide(NumberValue x, NumberValue y)
		{
			try
			{
				decimal asDecimal = x.AsDecimal;
				decimal asDecimal2 = y.AsDecimal;
				return NumberValue.New((asDecimal - asDecimal % asDecimal2) / asDecimal2);
			}
			catch (DivideByZeroException)
			{
			}
			catch (OverflowException)
			{
			}
			double asDouble = x.AsDouble;
			double asDouble2 = y.AsDouble;
			return DecimalPrecision.Overflow((asDouble - asDouble % asDouble2) / asDouble2);
		}

		// Token: 0x06008C79 RID: 35961 RVA: 0x001D7BEC File Offset: 0x001D5DEC
		private static NumberValue Overflow(double value)
		{
			if (double.IsNaN(value))
			{
				return NumberValue.NaN;
			}
			switch (Math.Sign(value))
			{
			case -1:
				return NumberValue.NegativeInfinity;
			case 0:
				return NumberValue.Zero;
			case 1:
				return NumberValue.Infinity;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06008C7A RID: 35962 RVA: 0x001D7C3B File Offset: 0x001D5E3B
		public override Accumulator GetNumberAccumulator()
		{
			return new DefaultAccumulator(Precision.Decimal, NumberValue.Zero);
		}

		// Token: 0x06008C7B RID: 35963 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Acos(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C7C RID: 35964 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Asin(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C7D RID: 35965 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Atan(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C7E RID: 35966 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Atan2(NumberValue y, NumberValue x)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C7F RID: 35967 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Combinations(NumberValue n, NumberValue k)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C80 RID: 35968 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Cos(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C81 RID: 35969 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Cosh(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C82 RID: 35970 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Exp(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C83 RID: 35971 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Log(NumberValue value, NumberValue newBase)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C84 RID: 35972 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Power(NumberValue value, NumberValue power)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C85 RID: 35973 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Sin(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C86 RID: 35974 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Sinh(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C87 RID: 35975 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Sqrt(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C88 RID: 35976 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Tan(NumberValue value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008C89 RID: 35977 RVA: 0x000091AE File Offset: 0x000073AE
		public sealed override NumberValue Tanh(NumberValue value)
		{
			throw new NotImplementedException();
		}
	}
}
