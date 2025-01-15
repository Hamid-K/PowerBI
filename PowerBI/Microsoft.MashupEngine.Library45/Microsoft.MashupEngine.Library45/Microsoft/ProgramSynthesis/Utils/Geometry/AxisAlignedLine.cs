using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005A5 RID: 1445
	public struct AxisAlignedLine<TUnit> : IBounded<TUnit>, IEquatable<AxisAlignedLine<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x0005A2C9 File Offset: 0x000584C9
		public readonly Axis Axis { get; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0005A2D1 File Offset: 0x000584D1
		public readonly Range<TUnit> Range { get; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0005A2D9 File Offset: 0x000584D9
		public readonly int Position { get; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0005A2E4 File Offset: 0x000584E4
		public bool IsTrivial
		{
			get
			{
				return this.Range.Min == this.Range.Max;
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0005A30F File Offset: 0x0005850F
		public AxisAlignedLine(Axis axis, Range<TUnit> range, int position)
		{
			this.Axis = axis;
			this.Range = range;
			this.Position = position;
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0005A326 File Offset: 0x00058526
		public Bounds<TUnit> Bounds
		{
			get
			{
				return new Bounds<TUnit>(delegate(Axis axis)
				{
					if (axis != this.Axis)
					{
						return new Range<TUnit>(this.Position, this.Position);
					}
					return this.Range;
				});
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x0005A34C File Offset: 0x0005854C
		public IEnumerable<Vector<TUnit>> AsEnumerable
		{
			get
			{
				return this.Range.AsEnumerable.Select((int pos) => new Vector<TUnit>(delegate(Axis a)
				{
					if (a != this.Axis)
					{
						return this.Position;
					}
					return pos;
				}));
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0005A38C File Offset: 0x0005858C
		public bool Equals(AxisAlignedLine<TUnit> other)
		{
			return this.Axis == other.Axis && this.Range.Equals(other.Range) && this.Position == other.Position;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0005A3D0 File Offset: 0x000585D0
		public override bool Equals(object obj)
		{
			return obj != null && obj is AxisAlignedLine<TUnit> && this.Equals((AxisAlignedLine<TUnit>)obj);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0005A3F0 File Offset: 0x000585F0
		public override int GetHashCode()
		{
			return (int)((((this.Axis * (Axis)397) ^ (Axis)this.Range.GetHashCode()) * (Axis)397) ^ (Axis)this.Position);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0005A42B File Offset: 0x0005862B
		public static bool operator ==(AxisAlignedLine<TUnit> left, AxisAlignedLine<TUnit> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0005A435 File Offset: 0x00058635
		public static bool operator !=(AxisAlignedLine<TUnit> left, AxisAlignedLine<TUnit> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0005A442 File Offset: 0x00058642
		public override string ToString()
		{
			return string.Format("Line(Axis={0}, Position={1}, Range={2})", this.Axis, this.Position, this.Range);
		}
	}
}
