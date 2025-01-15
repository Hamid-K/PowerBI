using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015D1 RID: 5585
	public abstract class Precision
	{
		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x06008C2A RID: 35882
		public abstract _ValueComparer Comparer { get; }

		// Token: 0x06008C2B RID: 35883 RVA: 0x001D757A File Offset: 0x001D577A
		public Value Add(Value x, Value y)
		{
			return x.Add(y, this);
		}

		// Token: 0x06008C2C RID: 35884 RVA: 0x001D7584 File Offset: 0x001D5784
		public Value Subtract(Value x, Value y)
		{
			return x.Subtract(y, this);
		}

		// Token: 0x06008C2D RID: 35885 RVA: 0x001D758E File Offset: 0x001D578E
		public Value Multiply(Value x, Value y)
		{
			return x.Multiply(y, this);
		}

		// Token: 0x06008C2E RID: 35886 RVA: 0x001D7598 File Offset: 0x001D5798
		public Value Divide(Value x, Value y)
		{
			return x.Divide(y, this);
		}

		// Token: 0x06008C2F RID: 35887 RVA: 0x001D75A2 File Offset: 0x001D57A2
		public Value Mod(Value x, Value y)
		{
			return x.Mod(y, this);
		}

		// Token: 0x06008C30 RID: 35888 RVA: 0x001D75AC File Offset: 0x001D57AC
		public Value IntegerDivide(Value x, Value y)
		{
			return x.IntegerDivide(y, this);
		}

		// Token: 0x06008C31 RID: 35889 RVA: 0x001D75B6 File Offset: 0x001D57B6
		public bool Equals(Value x, Value y)
		{
			return this.Comparer.Equals(x, y);
		}

		// Token: 0x06008C32 RID: 35890 RVA: 0x001D75C5 File Offset: 0x001D57C5
		public int Compare(Value x, Value y)
		{
			return this.Comparer.Compare(x, y);
		}

		// Token: 0x06008C33 RID: 35891
		public abstract NumberValue Add(NumberValue x, NumberValue y);

		// Token: 0x06008C34 RID: 35892
		public abstract NumberValue Subtract(NumberValue x, NumberValue y);

		// Token: 0x06008C35 RID: 35893
		public abstract NumberValue Multiply(NumberValue x, NumberValue y);

		// Token: 0x06008C36 RID: 35894
		public abstract NumberValue Divide(NumberValue x, NumberValue y);

		// Token: 0x06008C37 RID: 35895
		public abstract NumberValue Mod(NumberValue x, NumberValue y);

		// Token: 0x06008C38 RID: 35896
		public abstract NumberValue IntegerDivide(NumberValue x, NumberValue y);

		// Token: 0x06008C39 RID: 35897
		public abstract Accumulator GetNumberAccumulator();

		// Token: 0x06008C3A RID: 35898 RVA: 0x001D75D4 File Offset: 0x001D57D4
		public NumberValue Add(int x, int y)
		{
			long num = (long)x + (long)y;
			int num2 = (int)num;
			if ((long)num2 != num)
			{
				return NumberValue.New(num);
			}
			return NumberValue.New(num2);
		}

		// Token: 0x06008C3B RID: 35899
		public abstract NumberValue Add(double x, double y);

		// Token: 0x06008C3C RID: 35900 RVA: 0x001D75FC File Offset: 0x001D57FC
		public NumberValue Subtract(int x, int y)
		{
			long num = (long)x - (long)y;
			int num2 = (int)num;
			if ((long)num2 != num)
			{
				return NumberValue.New(num);
			}
			return NumberValue.New(num2);
		}

		// Token: 0x06008C3D RID: 35901
		public abstract NumberValue Subtract(double x, double y);

		// Token: 0x06008C3E RID: 35902
		public abstract NumberValue Acos(NumberValue value);

		// Token: 0x06008C3F RID: 35903
		public abstract NumberValue Asin(NumberValue value);

		// Token: 0x06008C40 RID: 35904
		public abstract NumberValue Atan(NumberValue value);

		// Token: 0x06008C41 RID: 35905
		public abstract NumberValue Atan2(NumberValue y, NumberValue x);

		// Token: 0x06008C42 RID: 35906
		public abstract NumberValue Combinations(NumberValue n, NumberValue k);

		// Token: 0x06008C43 RID: 35907
		public abstract NumberValue Cos(NumberValue value);

		// Token: 0x06008C44 RID: 35908
		public abstract NumberValue Cosh(NumberValue value);

		// Token: 0x06008C45 RID: 35909
		public abstract NumberValue Exp(NumberValue value);

		// Token: 0x06008C46 RID: 35910
		public abstract NumberValue Log(NumberValue value, NumberValue newBase);

		// Token: 0x06008C47 RID: 35911
		public abstract NumberValue Power(NumberValue number, NumberValue power);

		// Token: 0x06008C48 RID: 35912
		public abstract NumberValue Sin(NumberValue value);

		// Token: 0x06008C49 RID: 35913
		public abstract NumberValue Sinh(NumberValue value);

		// Token: 0x06008C4A RID: 35914
		public abstract NumberValue Sqrt(NumberValue value);

		// Token: 0x06008C4B RID: 35915
		public abstract NumberValue Tan(NumberValue value);

		// Token: 0x06008C4C RID: 35916
		public abstract NumberValue Tanh(NumberValue value);

		// Token: 0x04004CB6 RID: 19638
		public static readonly Precision Double = new DoublePrecision();

		// Token: 0x04004CB7 RID: 19639
		public static readonly Precision Decimal = new DecimalPrecision();
	}
}
