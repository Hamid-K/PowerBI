using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005B0 RID: 1456
	[JsonObject(MemberSerialization.OptIn)]
	public struct Bounds<TUnit> : IEquatable<Bounds<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x0005AA57 File Offset: 0x00058C57
		public readonly Range<TUnit> Horizontal { get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0005AA5F File Offset: 0x00058C5F
		public readonly Range<TUnit> Vertical { get; }

		// Token: 0x06001F9C RID: 8092 RVA: 0x0005AA67 File Offset: 0x00058C67
		public Bounds(Range<TUnit> horizontal, Range<TUnit> vertical)
		{
			this.Horizontal = horizontal;
			this.Vertical = vertical;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x0005AA77 File Offset: 0x00058C77
		public Bounds(IReadOnlyDictionary<Axis, Range<TUnit>> ranges)
		{
			this = new Bounds<TUnit>(ranges[Axis.Horizontal], ranges[Axis.Vertical]);
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x0005AA8D File Offset: 0x00058C8D
		[JsonConstructor]
		public Bounds(int left, int right, int top, int bottom)
		{
			this = new Bounds<TUnit>(new Range<TUnit>(left, right), new Range<TUnit>(top, bottom));
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0005AAA4 File Offset: 0x00058CA4
		public Bounds(IReadOnlyDictionary<Direction, int> bounds)
		{
			this = new Bounds<TUnit>(bounds[Direction.Left], bounds[Direction.Right], bounds[Direction.Up], bounds[Direction.Down]);
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0005AAC8 File Offset: 0x00058CC8
		public Bounds(Func<Direction, int> generator)
		{
			this = new Bounds<TUnit>(generator(Direction.Left), generator(Direction.Right), generator(Direction.Up), generator(Direction.Down));
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0005AAEC File Offset: 0x00058CEC
		public Bounds(Func<Axis, Range<TUnit>> generator)
		{
			this = new Bounds<TUnit>(generator(Axis.Horizontal), generator(Axis.Vertical));
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0005AB04 File Offset: 0x00058D04
		public Bounds(Vector<TUnit> corner1, Vector<TUnit> corner2)
		{
			this = new Bounds<TUnit>(Math.Min(corner1.X, corner2.X), Math.Max(corner1.X, corner2.X), Math.Min(corner1.Y, corner2.Y), Math.Max(corner1.Y, corner2.Y));
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0005AB5C File Offset: 0x00058D5C
		[JsonProperty]
		public int Left
		{
			get
			{
				return this.Horizontal.Min;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0005AB78 File Offset: 0x00058D78
		[JsonProperty]
		public int Right
		{
			get
			{
				return this.Horizontal.Max;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0005AB94 File Offset: 0x00058D94
		[JsonProperty]
		public int Top
		{
			get
			{
				return this.Vertical.Min;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001FA6 RID: 8102 RVA: 0x0005ABB0 File Offset: 0x00058DB0
		[JsonProperty]
		public int Bottom
		{
			get
			{
				return this.Vertical.Max;
			}
		}

		// Token: 0x1700058F RID: 1423
		public int this[Direction dir]
		{
			get
			{
				return this.BoundInDirection(dir);
			}
		}

		// Token: 0x17000590 RID: 1424
		public Range<TUnit> this[Axis axis]
		{
			get
			{
				return this.RangeAlongAxis(axis);
			}
		}

		// Token: 0x17000591 RID: 1425
		public Vector<TUnit> this[Ordinal dir]
		{
			get
			{
				return this.Corner(dir);
			}
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		public override string ToString()
		{
			return string.Format("left: {0}, ", this.Left) + string.Format("right: {0}, ", this.Right) + string.Format("top: {0}, ", this.Top) + string.Format("bottom: {0}", this.Bottom);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x0005AC4E File Offset: 0x00058E4E
		public Vector<TUnit> Corner(Ordinal ordinal)
		{
			return new Vector<TUnit>(this[ordinal.Horizontal()], this[ordinal.Vertical()]);
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x0005AC70 File Offset: 0x00058E70
		public int Width()
		{
			return this.RangeAlongAxis(Axis.Horizontal).Size();
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0005AC8C File Offset: 0x00058E8C
		public int Height()
		{
			return this.RangeAlongAxis(Axis.Vertical).Size();
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0005ACA8 File Offset: 0x00058EA8
		public int Area()
		{
			return this.Width() * this.Height();
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0005ACB7 File Offset: 0x00058EB7
		public double AspectRatio()
		{
			return (double)this.Width() / (double)this.Height();
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0005ACC8 File Offset: 0x00058EC8
		public bool Contains(Bounds<TUnit> other)
		{
			return this.Vertical.Contains(other.Vertical) && this.Horizontal.Contains(other.Horizontal);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0005AD04 File Offset: 0x00058F04
		public bool Contains(int line, Axis axis)
		{
			return this.RangeAlongAxis(axis).Contains(line);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x0005AD21 File Offset: 0x00058F21
		public bool Contains(Bounds<TUnit> other, Axis axis)
		{
			return this.Contains(other.RangeAlongAxis(axis), axis);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x0005AD34 File Offset: 0x00058F34
		public bool Contains(Range<TUnit> range, Axis axis)
		{
			return this.RangeAlongAxis(axis).Contains(range);
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0005AD54 File Offset: 0x00058F54
		public bool Contains(Vector<TUnit> vector)
		{
			return this.Vertical.Contains(vector.Y) && this.Horizontal.Contains(vector.X);
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0005AD8D File Offset: 0x00058F8D
		public bool IsSubsetOf(Bounds<TUnit> other)
		{
			return other.Contains(this);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0005AD9C File Offset: 0x00058F9C
		public bool IsSubsetOf(Range<TUnit> range, Axis axis)
		{
			return range.Contains(this.RangeAlongAxis(axis));
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0005ADAC File Offset: 0x00058FAC
		public bool Overlaps(Bounds<TUnit> other)
		{
			return this.Vertical.Overlaps(other.Vertical) && this.Horizontal.Overlaps(other.Horizontal);
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0005ADE8 File Offset: 0x00058FE8
		public bool Overlaps(Bounds<TUnit> other, Axis axis)
		{
			return this.RangeAlongAxis(axis).Overlaps(other.RangeAlongAxis(axis));
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0005AE0C File Offset: 0x0005900C
		public bool Overlaps(Range<TUnit> range, Axis axis)
		{
			return this.RangeAlongAxis(axis).Overlaps(range);
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0005AE2C File Offset: 0x0005902C
		public bool IsAfter(Bounds<TUnit> other, Axis axis, bool includingIntersection = false)
		{
			return this.RangeAlongAxis(axis).IsAfter(other.RangeAlongAxis(axis), includingIntersection);
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0005AE54 File Offset: 0x00059054
		public bool IsAfter(int line, Axis axis)
		{
			return this.RangeAlongAxis(axis).IsAfter(line);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0005AE74 File Offset: 0x00059074
		public bool IsBefore(Bounds<TUnit> other, Axis axis, bool includingIntersection = false)
		{
			return this.RangeAlongAxis(axis).IsBefore(other.RangeAlongAxis(axis), includingIntersection);
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x0005AE9C File Offset: 0x0005909C
		public bool IsBefore(int line, Axis axis)
		{
			return this.RangeAlongAxis(axis).IsBefore(line);
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0005AEBC File Offset: 0x000590BC
		public int BoundInDirection(Direction dir)
		{
			switch (dir)
			{
			case Direction.Up:
				return this.Top;
			case Direction.Right:
				return this.Right;
			case Direction.Down:
				return this.Bottom;
			case Direction.Left:
				return this.Left;
			default:
				throw new ArgumentException("dir", string.Format("Invalid Direction: {0}", dir));
			}
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0005AF17 File Offset: 0x00059117
		public Range<TUnit> RangeAlongAxis(Axis axis)
		{
			if (axis == Axis.Horizontal)
			{
				return this.Horizontal;
			}
			return this.Vertical;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0005AF2C File Offset: 0x0005912C
		public double CenterAlongAxis(Axis axis)
		{
			return this.RangeAlongAxis(axis).Center();
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0005AF48 File Offset: 0x00059148
		public DoubleVector<PixelUnit> Center
		{
			get
			{
				return new DoubleVector<PixelUnit>(new Func<Axis, double>(this.CenterAlongAxis));
			}
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0005AF68 File Offset: 0x00059168
		public Bounds<TUnit> Extend(Direction direction, int amount = 1)
		{
			switch (direction)
			{
			case Direction.Up:
				return new Bounds<TUnit>(this.Left, this.Right, this.Top - amount, this.Bottom);
			case Direction.Right:
				return new Bounds<TUnit>(this.Left, this.Right + amount, this.Top, this.Bottom);
			case Direction.Down:
				return new Bounds<TUnit>(this.Left, this.Right, this.Top, this.Bottom + amount);
			case Direction.Left:
				return new Bounds<TUnit>(this.Left - amount, this.Right, this.Top, this.Bottom);
			default:
				throw new ArgumentException("direction", string.Format("Invalid Direction: {0}", direction));
			}
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0005B02C File Offset: 0x0005922C
		public Bounds<TUnit> Extend(Axis axis, int amount = 1)
		{
			if (axis == Axis.Horizontal)
			{
				return new Bounds<TUnit>(this.Left - amount, this.Right + amount, this.Top, this.Bottom);
			}
			if (axis == Axis.Vertical)
			{
				return new Bounds<TUnit>(this.Left, this.Right, this.Top - amount, this.Bottom + amount);
			}
			throw new ArgumentException("axis", string.Format("Invalid Axis: {0}", axis));
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0005B09E File Offset: 0x0005929E
		public Bounds<TUnit> Extend(int amount = 1)
		{
			return new Bounds<TUnit>(this.Left - amount, this.Right + amount, this.Top - amount, this.Bottom + amount);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0005B0C5 File Offset: 0x000592C5
		public Bounds<TUnit> Join(Bounds<TUnit> other)
		{
			return Bounds<TUnit>.Join(new Bounds<TUnit>[] { this, other });
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0005B0E8 File Offset: 0x000592E8
		public static Bounds<TUnit> Join(IEnumerable<Bounds<TUnit>> bounds)
		{
			Optional<Bounds<TUnit>> optional = Bounds<TUnit>.MaybeJoin(bounds);
			if (!optional.HasValue)
			{
				throw new ArgumentException("Argument bounds must not be empty.");
			}
			return optional.Value;
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0005B118 File Offset: 0x00059318
		public static Optional<Bounds<TUnit>> MaybeJoin(IEnumerable<Bounds<TUnit>> bounds)
		{
			bool flag = false;
			int left = int.MaxValue;
			int right = int.MinValue;
			int top = int.MaxValue;
			int bottom = int.MinValue;
			foreach (Bounds<TUnit> bounds2 in bounds)
			{
				flag = true;
				left = Math.Min(left, bounds2.Left);
				right = Math.Max(right, bounds2.Right);
				top = Math.Min(top, bounds2.Top);
				bottom = Math.Max(bottom, bounds2.Bottom);
			}
			return flag.Then(() => new Bounds<TUnit>(left, right, top, bottom));
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0005B204 File Offset: 0x00059404
		public Optional<Bounds<TUnit>> Intersect(Bounds<TUnit> other)
		{
			return Bounds<TUnit>.Intersection(new Bounds<TUnit>[] { this, other });
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0005B228 File Offset: 0x00059428
		public static Optional<Bounds<TUnit>> Intersection(IEnumerable<Bounds<TUnit>> bounds)
		{
			int left = int.MinValue;
			int right = int.MaxValue;
			int top = int.MinValue;
			int bottom = int.MaxValue;
			foreach (Bounds<TUnit> bounds2 in bounds)
			{
				left = Math.Max(left, bounds2.Left);
				right = Math.Min(right, bounds2.Right);
				top = Math.Max(top, bounds2.Top);
				bottom = Math.Min(bottom, bounds2.Bottom);
			}
			return (left <= right && top <= bottom).Then(() => new Bounds<TUnit>(left, right, top, bottom));
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0005B330 File Offset: 0x00059530
		public static Optional<Bounds<TUnit>> MaybeBetween(Bounds<TUnit> first, Bounds<TUnit> second)
		{
			Func<Range<TUnit>, Optional<Bounds<TUnit>>> <>9__3;
			return (from horizontalOverlap in first.Horizontal.Intersect(second.Horizontal)
				select from betweenVertical in first.Vertical.BetweenExclusive(second.Vertical)
					select new Bounds<TUnit>(horizontalOverlap, betweenVertical)).OrElseCompute(delegate
			{
				Optional<Range<TUnit>> optional = first.Vertical.Intersect(second.Vertical);
				Func<Range<TUnit>, Optional<Bounds<TUnit>>> func;
				if ((func = <>9__3) == null)
				{
					func = (<>9__3 = (Range<TUnit> verticalOverlap) => from betweenHorizontal in first.Horizontal.BetweenExclusive(second.Horizontal)
						select new Bounds<TUnit>(betweenHorizontal, verticalOverlap));
				}
				return optional.Select(func);
			});
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0005B394 File Offset: 0x00059594
		public static Optional<Bounds<TUnit>> MaybeBetweenCenters(Axis axis, Bounds<TUnit> first, Bounds<TUnit> second)
		{
			Axis axis2 = axis.Perpendicular();
			return from overlap in first[axis2].Intersect(second[axis2])
				select new Bounds<TUnit>(delegate(Axis a)
				{
					if (a != axis)
					{
						return overlap;
					}
					return Range<TUnit>.CreateUnordered((int)first[axis].Center(), (int)second[axis].Center());
				});
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0005B3FC File Offset: 0x000595FC
		public Optional<Bounds<TUnit>> MaybeJoinExact(Bounds<TUnit> other)
		{
			foreach (Axis axis in AxisUtilities.Axes)
			{
				Axis axis2 = axis.Perpendicular();
				if (this[axis] == other[axis] && this[axis2].Distance(other[axis2]) <= 1)
				{
					return this.Join(other).Some<Bounds<TUnit>>();
				}
			}
			return Optional<Bounds<TUnit>>.Nothing;
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0005B490 File Offset: 0x00059690
		public IEnumerable<Bounds<TUnit>> Subtract(Bounds<TUnit> other)
		{
			if (!this.Overlaps(other))
			{
				yield return ref this;
				yield break;
			}
			if (other.Contains(ref this))
			{
				yield break;
			}
			foreach (Range<TUnit> range in this.Vertical.Subtract(other.Vertical))
			{
				Range<TUnit> range2 = range;
				yield return new Bounds<TUnit>(this.Horizontal, range2);
			}
			IEnumerator<Range<TUnit>> enumerator = null;
			Range<TUnit> verticalOverlap = this.Vertical.Intersect(other.Vertical).Value;
			foreach (Range<TUnit> range3 in this.Horizontal.Subtract(other.Horizontal))
			{
				Range<TUnit> range2 = verticalOverlap;
				yield return new Bounds<TUnit>(range3, range2);
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0005B4AC File Offset: 0x000596AC
		public double DistanceTo(Bounds<TUnit> other)
		{
			AxisAligned<int> axisAligned = new AxisAligned<int>((Axis axis) => this[axis].Distance(other[axis]));
			int num = 0;
			foreach (Axis axis2 in AxisUtilities.Axes)
			{
				int num2 = axisAligned[axis2];
				if (num2 == 0)
				{
					return (double)axisAligned[axis2.Perpendicular()];
				}
				num += num2 * num2;
			}
			return Math.Sqrt((double)num);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0005B54C File Offset: 0x0005974C
		public bool OnBoundary(int x, int y)
		{
			if (x == this.Left || x == this.Right)
			{
				return this.Vertical.Contains(y);
			}
			return (y == this.Top || y == this.Bottom) && this.Horizontal.Contains(x);
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x0005B59E File Offset: 0x0005979E
		public Directed<AxisAlignedLine<TUnit>> Edges
		{
			get
			{
				return new Directed<AxisAlignedLine<TUnit>>(new Func<Direction, AxisAlignedLine<TUnit>>(this.EdgeInDirection));
			}
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0005B5BC File Offset: 0x000597BC
		public AxisAlignedLine<TUnit> EdgeInDirection(Direction dir)
		{
			switch (dir)
			{
			case Direction.Up:
				return new AxisAlignedLine<TUnit>(Axis.Horizontal, this.Horizontal, this.Top);
			case Direction.Right:
				return new AxisAlignedLine<TUnit>(Axis.Vertical, this.Vertical, this.Right);
			case Direction.Down:
				return new AxisAlignedLine<TUnit>(Axis.Horizontal, this.Horizontal, this.Bottom);
			case Direction.Left:
				return new AxisAlignedLine<TUnit>(Axis.Vertical, this.Vertical, this.Left);
			default:
				throw new ArgumentException(string.Format("Unknown direction: {0}", dir));
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x0005B644 File Offset: 0x00059844
		public IEnumerable<Vector<TUnit>> AsEnumerableColumnMajor
		{
			get
			{
				int top = this.Top;
				int height = this.Height();
				return Enumerable.Range(this.Left, this.Width()).SelectMany((int x) => from y in Enumerable.Range(top, height)
					select new Vector<TUnit>(x, y));
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0005B694 File Offset: 0x00059894
		public IEnumerable<Vector<TUnit>> AsEnumerableRowMajor
		{
			get
			{
				int left = this.Left;
				int width = this.Width();
				return Enumerable.Range(this.Top, this.Height()).SelectMany((int y) => from x in Enumerable.Range(left, width)
					select new Vector<TUnit>(x, y));
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x0005B6E4 File Offset: 0x000598E4
		public IEnumerable<Vector<TUnit>> AsEnumerableColumnMajorDecreasing
		{
			get
			{
				int top = this.Top;
				int height = this.Height();
				return Enumerable.Range(this.Left, this.Width()).Reverse<int>().SelectMany((int x) => from y in Enumerable.Range(top, height)
					select new Vector<TUnit>(x, y));
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0005B738 File Offset: 0x00059938
		public IEnumerable<Vector<TUnit>> AsEnumerableRowMajorDecreasing
		{
			get
			{
				int left = this.Left;
				int width = this.Width();
				return Enumerable.Range(this.Top, this.Height()).Reverse<int>().SelectMany((int y) => from x in Enumerable.Range(left, width)
					select new Vector<TUnit>(x, y));
			}
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0005B78A File Offset: 0x0005998A
		public IEnumerable<Vector<TUnit>> AsEnumerable(Axis majorAxis = Axis.Vertical, Derivative derivative = Derivative.Increasing)
		{
			if (majorAxis != Axis.Vertical)
			{
				if (derivative != Derivative.Increasing)
				{
					return this.AsEnumerableRowMajorDecreasing;
				}
				return this.AsEnumerableRowMajor;
			}
			else
			{
				if (derivative != Derivative.Increasing)
				{
					return this.AsEnumerableColumnMajorDecreasing;
				}
				return this.AsEnumerableColumnMajor;
			}
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0005B7B3 File Offset: 0x000599B3
		public Bounds<TUnit> With(Direction newValueDir, int value)
		{
			return new Bounds<TUnit>(delegate(Direction dir)
			{
				if (dir != newValueDir)
				{
					return this[dir];
				}
				return value;
			});
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0005B7E4 File Offset: 0x000599E4
		public Optional<Bounds<TUnit>> MaybeWith(Direction newValueDir, int value)
		{
			if (!this[newValueDir.AlignedAxis()].Contains(value))
			{
				return Optional<Bounds<TUnit>>.Nothing;
			}
			Bounds<TUnit> self = this;
			return new Bounds<TUnit>(delegate(Direction dir)
			{
				if (dir != newValueDir)
				{
					return self[dir];
				}
				return value;
			}).Some<Bounds<TUnit>>();
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0005B84E File Offset: 0x00059A4E
		public Bounds<TUnit> With(Axis newValueAxis, Range<TUnit> value)
		{
			return new Bounds<TUnit>(delegate(Axis axis)
			{
				if (axis != newValueAxis)
				{
					return this[axis];
				}
				return value;
			});
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0005B880 File Offset: 0x00059A80
		public bool Equals(Bounds<TUnit> other)
		{
			return this.Horizontal.Equals(other.Horizontal) && this.Vertical.Equals(other.Vertical);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0005B8BC File Offset: 0x00059ABC
		public override bool Equals(object obj)
		{
			if (obj is Bounds<TUnit>)
			{
				Bounds<TUnit> bounds = (Bounds<TUnit>)obj;
				return this.Equals(bounds);
			}
			return false;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		public override int GetHashCode()
		{
			return (1942786966 * -1521134295 + this.Horizontal.GetHashCode()) * -1521134295 + this.Vertical.GetHashCode();
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0005B92C File Offset: 0x00059B2C
		public static bool operator ==(Bounds<TUnit> left, Bounds<TUnit> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0005B936 File Offset: 0x00059B36
		public static bool operator !=(Bounds<TUnit> left, Bounds<TUnit> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0005B943 File Offset: 0x00059B43
		public static Bounds<TUnit>operator +(Bounds<TUnit> a, Vector<TUnit> b)
		{
			return new Bounds<TUnit>(a.Left + b.X, a.Right + b.X, a.Top + b.Y, a.Bottom + b.Y);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0005B982 File Offset: 0x00059B82
		public static Bounds<TUnit>operator -(Bounds<TUnit> a, Vector<TUnit> b)
		{
			return new Bounds<TUnit>(a.Left - b.X, a.Right - b.X, a.Top - b.Y, a.Bottom - b.Y);
		}

		// Token: 0x04000F39 RID: 3897
		public static readonly Bounds<TUnit> Zero = new Bounds<TUnit>(0, 0, 0, 0);
	}
}
