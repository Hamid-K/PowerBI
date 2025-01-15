using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005F5 RID: 1525
	public class Vector<TUnit> : AxisAligned<int>, IEquatable<Vector<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x0005E132 File Offset: 0x0005C332
		public int X
		{
			get
			{
				return base.Horizontal;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002120 RID: 8480 RVA: 0x0005E13A File Offset: 0x0005C33A
		public int Y
		{
			get
			{
				return base.Vertical;
			}
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0005E142 File Offset: 0x0005C342
		public Vector(int x, int y)
			: base(x, y)
		{
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0005E14C File Offset: 0x0005C34C
		public Vector(Func<Axis, int> generator)
			: base(generator)
		{
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0005E155 File Offset: 0x0005C355
		public Vector(Direction direction, int amount)
			: base(direction.IsHorizontal() ? (amount * direction.Derivative().Value()) : 0, direction.IsVertical() ? (amount * direction.Derivative().Value()) : 0)
		{
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0005E18D File Offset: 0x0005C38D
		public Vector(Ordinal ordinal, int amount)
			: base(ordinal.Horizontal().Derivative().Value() * amount, ordinal.Vertical().Derivative().Value() * amount)
		{
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0005E1B9 File Offset: 0x0005C3B9
		public bool Equals(Vector<TUnit> other)
		{
			return other != null && this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0005E1E2 File Offset: 0x0005C3E2
		public static Vector<TUnit>operator +(Vector<TUnit> a, Vector<TUnit> b)
		{
			return new Vector<TUnit>(a.X + b.X, a.Y + b.Y);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0005E203 File Offset: 0x0005C403
		public static Vector<TUnit>operator -(Vector<TUnit> a, Vector<TUnit> b)
		{
			return new Vector<TUnit>(a.X - b.X, a.Y - b.Y);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0005E224 File Offset: 0x0005C424
		public static bool operator ==(Vector<TUnit> vector1, Vector<TUnit> vector2)
		{
			return EqualityComparer<Vector<TUnit>>.Default.Equals(vector1, vector2);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0005E232 File Offset: 0x0005C432
		public static bool operator !=(Vector<TUnit> vector1, Vector<TUnit> vector2)
		{
			return !(vector1 == vector2);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0005E23E File Offset: 0x0005C43E
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.X, this.Y);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0005E260 File Offset: 0x0005C460
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Vector<TUnit>);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0005E270 File Offset: 0x0005C470
		public override int GetHashCode()
		{
			return (1861411795 * -1521134295 + this.X.GetHashCode()) * -1521134295 + this.Y.GetHashCode();
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0005E2AC File Offset: 0x0005C4AC
		public double Distance(Vector<PixelUnit> other)
		{
			object obj = ((this.X == other.X) ? 0 : (Math.Abs(this.X - other.X) + 1));
			int num = ((this.Y == other.Y) ? 0 : (Math.Abs(this.Y - other.Y) + 1));
			object obj2 = obj;
			return Math.Sqrt(obj2 * obj2 + num * num);
		}

		// Token: 0x04000FBF RID: 4031
		public static readonly Vector<TUnit> Zero = new Vector<TUnit>(0, 0);
	}
}
