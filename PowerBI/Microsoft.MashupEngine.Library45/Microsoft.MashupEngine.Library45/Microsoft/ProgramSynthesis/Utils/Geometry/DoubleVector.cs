using System;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005F6 RID: 1526
	public class DoubleVector<TUnit> : AxisAligned<double> where TUnit : BoundsUnit
	{
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0005E31E File Offset: 0x0005C51E
		public double X
		{
			get
			{
				return base.Horizontal;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0005E326 File Offset: 0x0005C526
		public double Y
		{
			get
			{
				return base.Vertical;
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0005E32E File Offset: 0x0005C52E
		public DoubleVector(double x, double y)
			: base(x, y)
		{
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0005E338 File Offset: 0x0005C538
		public DoubleVector(Vector<TUnit> vector)
			: base((double)vector.X, (double)vector.Y)
		{
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0005E34E File Offset: 0x0005C54E
		public DoubleVector(Func<Axis, double> generator)
			: base(generator)
		{
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0005E358 File Offset: 0x0005C558
		public DoubleVector(Direction direction, double amount)
			: base(direction.IsHorizontal() ? (amount * (double)direction.Derivative().Value()) : 0.0, direction.IsVertical() ? (amount * (double)direction.Derivative().Value()) : 0.0)
		{
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0005E3AD File Offset: 0x0005C5AD
		public DoubleVector(Ordinal ordinal, double amount)
			: base((double)ordinal.Horizontal().Derivative().Value() * amount, (double)ordinal.Vertical().Derivative().Value() * amount)
		{
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0005E3DB File Offset: 0x0005C5DB
		public Vector<TUnit> RoundToVector()
		{
			return new Vector<TUnit>((Axis axis) => (int)Math.Round(base[axis]));
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0005E3EE File Offset: 0x0005C5EE
		public double DistanceTo(DoubleVector<TUnit> other)
		{
			return (this - other).Length;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x0005E3FC File Offset: 0x0005C5FC
		public double Length
		{
			get
			{
				return Math.Sqrt(this.X * this.X + this.Y * this.Y);
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0005E41E File Offset: 0x0005C61E
		public static DoubleVector<TUnit>operator +(DoubleVector<TUnit> a, DoubleVector<TUnit> b)
		{
			return new DoubleVector<TUnit>(a.X + b.X, a.Y + b.Y);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0005E43F File Offset: 0x0005C63F
		public static DoubleVector<TUnit>operator -(DoubleVector<TUnit> a, DoubleVector<TUnit> b)
		{
			return new DoubleVector<TUnit>(a.X - b.X, a.Y - b.Y);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0005E460 File Offset: 0x0005C660
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.X, this.Y);
		}

		// Token: 0x04000FC0 RID: 4032
		public static readonly DoubleVector<TUnit> Zero = new DoubleVector<TUnit>(0.0, 0.0);
	}
}
