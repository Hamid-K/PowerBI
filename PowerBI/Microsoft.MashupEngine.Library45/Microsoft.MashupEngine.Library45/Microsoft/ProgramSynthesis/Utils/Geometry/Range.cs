using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005E6 RID: 1510
	public struct Range<TUnit> : IEquatable<Range<TUnit>> where TUnit : BoundsUnit
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0005CB78 File Offset: 0x0005AD78
		public readonly int Min { get; }

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0005CB80 File Offset: 0x0005AD80
		public readonly int Max { get; }

		// Token: 0x060020A5 RID: 8357 RVA: 0x0005CB88 File Offset: 0x0005AD88
		public Range(int min, int max)
		{
			if (min > max)
			{
				throw new ArgumentException("min must be less than or equal to max.");
			}
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x170005B3 RID: 1459
		public int this[Derivative derivative]
		{
			get
			{
				if (derivative != Derivative.Decreasing)
				{
					return this.Max;
				}
				return this.Min;
			}
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0005CBBA File Offset: 0x0005ADBA
		public int Size()
		{
			return this.Max - this.Min + 1;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0005CBCB File Offset: 0x0005ADCB
		public double Center()
		{
			return (double)(this.Max + this.Min) / 2.0;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0005CBE5 File Offset: 0x0005ADE5
		public bool Overlaps(Range<TUnit> other)
		{
			return Math.Min(this.Max, other.Max) >= Math.Max(this.Min, other.Min);
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0005CC10 File Offset: 0x0005AE10
		public bool Contains(Range<TUnit> other)
		{
			return this.Min <= other.Min && other.Max <= this.Max;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0005CC35 File Offset: 0x0005AE35
		public bool Contains(Ranges<TUnit> other)
		{
			return other.ContainedBy(this);
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0005CC43 File Offset: 0x0005AE43
		public bool Contains(int line)
		{
			return this.Min <= line && line <= this.Max;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0005CC5C File Offset: 0x0005AE5C
		public bool IsAfter(Range<TUnit> other, bool includingIntersection = false)
		{
			return this.IsAfter(includingIntersection ? other.Min : other.Max);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0005CC77 File Offset: 0x0005AE77
		public bool IsAfter(int line)
		{
			return this.Min > line;
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0005CC82 File Offset: 0x0005AE82
		public bool IsBefore(Range<TUnit> other, bool includingIntersection = false)
		{
			return this.IsBefore(includingIntersection ? other.Max : other.Min);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0005CC9D File Offset: 0x0005AE9D
		public bool IsBefore(int line)
		{
			return this.Max < line;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0005CCA8 File Offset: 0x0005AEA8
		public Range<TUnit> Join(Range<TUnit> other)
		{
			return new Range<TUnit>(Math.Min(this.Min, other.Min), Math.Max(this.Max, other.Max));
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0005CCD4 File Offset: 0x0005AED4
		public static Range<TUnit> Join(IEnumerable<Range<TUnit>> ranges)
		{
			int num = int.MaxValue;
			int num2 = int.MinValue;
			foreach (Range<TUnit> range in ranges)
			{
				num = Math.Min(num, range.Min);
				num2 = Math.Max(num2, range.Max);
			}
			return new Range<TUnit>(num, num2);
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0005CD44 File Offset: 0x0005AF44
		public static Optional<Range<TUnit>> MaybeIntersect(IEnumerable<Range<TUnit>> ranges)
		{
			int min = int.MinValue;
			int max = int.MaxValue;
			bool flag = false;
			foreach (Range<TUnit> range in ranges)
			{
				flag = true;
				min = Math.Max(min, range.Min);
				max = Math.Min(max, range.Max);
				if (min > max)
				{
					return Optional<Range<TUnit>>.Nothing;
				}
			}
			return flag.Then(() => Range<TUnit>.MaybeCreate(min, max));
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0005CE08 File Offset: 0x0005B008
		public Optional<Range<TUnit>> BetweenInclusive(Range<TUnit> other)
		{
			if (this.Max < other.Min)
			{
				return new Range<TUnit>(this.Max, other.Min).Some<Range<TUnit>>();
			}
			if (other.Max < this.Min)
			{
				return new Range<TUnit>(other.Max, this.Min).Some<Range<TUnit>>();
			}
			return Optional<Range<TUnit>>.Nothing;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0005CE68 File Offset: 0x0005B068
		public Optional<Range<TUnit>> BetweenExclusive(Range<TUnit> other)
		{
			if (this.Max + 1 < other.Min - 1)
			{
				return new Range<TUnit>(this.Max + 1, other.Min - 1).Some<Range<TUnit>>();
			}
			if (other.Max + 1 < this.Min - 1)
			{
				return new Range<TUnit>(other.Max + 1, this.Min - 1).Some<Range<TUnit>>();
			}
			return Optional<Range<TUnit>>.Nothing;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0005CED8 File Offset: 0x0005B0D8
		public Optional<Range<TUnit>> Intersect(Range<TUnit> other)
		{
			int num = Math.Max(this.Min, other.Min);
			int num2 = Math.Min(this.Max, other.Max);
			if (num > num2)
			{
				return Optional<Range<TUnit>>.Nothing;
			}
			return new Range<TUnit>(num, num2).Some<Range<TUnit>>();
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0005CF21 File Offset: 0x0005B121
		public int Distance(Range<TUnit> other)
		{
			if (this.Overlaps(other))
			{
				return 0;
			}
			return Math.Max(this.Min, other.Min) - Math.Min(this.Max, other.Max);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0005CF53 File Offset: 0x0005B153
		public bool IsAdjacentToBelow(Range<TUnit> other)
		{
			return this.Max == other.Min - 1;
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0005CF66 File Offset: 0x0005B166
		public bool IsAdjacentToAbove(Range<TUnit> other)
		{
			return this.Min == other.Max + 1;
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x0005CF79 File Offset: 0x0005B179
		public bool IsAdjacentTo(Range<TUnit> other)
		{
			return this.IsAdjacentToBelow(other) || this.IsAdjacentToAbove(other);
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x0005CF90 File Offset: 0x0005B190
		public IEnumerable<Range<TUnit>> Subtract(Range<TUnit> other)
		{
			List<Optional<Range<TUnit>>> list = new List<Optional<Range<TUnit>>>();
			if (!this.Overlaps(other))
			{
				return this.Yield<Range<TUnit>>();
			}
			if (this.Contains(other.Min))
			{
				list.Add(Range<TUnit>.MaybeCreate(this.Min, other.Min - 1));
			}
			if (this.Contains(other.Max))
			{
				list.Add(Range<TUnit>.MaybeCreate(other.Max + 1, this.Max));
			}
			return list.SelectValues<Range<TUnit>>();
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0005D010 File Offset: 0x0005B210
		public IReadOnlyList<Range<TUnit>> Subtract(IEnumerable<Range<TUnit>> others)
		{
			List<Range<TUnit>> list = new List<Range<TUnit>> { this };
			using (IEnumerator<Range<TUnit>> enumerator = others.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Range<TUnit> other = enumerator.Current;
					if (other.Overlaps(this))
					{
						list = list.SelectMany((Range<TUnit> r) => r.Subtract(other)).ToList<Range<TUnit>>();
					}
				}
			}
			return list;
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0005D09C File Offset: 0x0005B29C
		public Range<TUnit> Expand(int amount)
		{
			return new Range<TUnit>(this.Min - amount, this.Max + amount);
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0005D0B3 File Offset: 0x0005B2B3
		public Range<TUnit> Expand(int amount, Derivative side)
		{
			if (side != Derivative.Decreasing)
			{
				return new Range<TUnit>(this.Min, this.Max + amount);
			}
			return new Range<TUnit>(this.Min - amount, this.Max);
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0005D0E0 File Offset: 0x0005B2E0
		public IEnumerable<int> AsEnumerable
		{
			get
			{
				return Enumerable.Range(this.Min, this.Size());
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0005D0F3 File Offset: 0x0005B2F3
		public IEnumerable<int> AsReverseEnumerable
		{
			get
			{
				int num;
				for (int i = this.Max; i >= this.Min; i = num - 1)
				{
					yield return i;
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0005D108 File Offset: 0x0005B308
		public bool Equals(Range<TUnit> other)
		{
			return this.Min == other.Min && this.Max == other.Max;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x0005D12C File Offset: 0x0005B32C
		public override bool Equals(object obj)
		{
			if (obj is Range<TUnit>)
			{
				Range<TUnit> range = (Range<TUnit>)obj;
				return this.Equals(range);
			}
			return false;
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0005D154 File Offset: 0x0005B354
		public override int GetHashCode()
		{
			return (1537547080 * -1521134295 + this.Min.GetHashCode()) * -1521134295 + this.Max.GetHashCode();
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x0005D190 File Offset: 0x0005B390
		public static Optional<Range<TUnit>> MaybeCreate(int min, int max)
		{
			if (min <= max)
			{
				return new Range<TUnit>(min, max).Some<Range<TUnit>>();
			}
			return Optional<Range<TUnit>>.Nothing;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0005D1A8 File Offset: 0x0005B3A8
		public static Range<TUnit> CreateUnordered(int a, int b)
		{
			if (a >= b)
			{
				return new Range<TUnit>(b, a);
			}
			return new Range<TUnit>(a, b);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0005D1BD File Offset: 0x0005B3BD
		public static Range<TUnit> CreateAround(int pos, int radius)
		{
			return new Range<TUnit>(pos - radius, pos + radius);
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0005D1CA File Offset: 0x0005B3CA
		public static Range<TUnit> CreateAt(int pos)
		{
			return new Range<TUnit>(pos, pos);
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x0005D1D3 File Offset: 0x0005B3D3
		public static Range<TUnit> Create(Record<int, int> record)
		{
			return new Range<TUnit>(record.Item1, record.Item2);
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x0005D1E6 File Offset: 0x0005B3E6
		public static bool operator ==(Range<TUnit> left, Range<TUnit> right)
		{
			return left.Equals(right);
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0005D1F0 File Offset: 0x0005B3F0
		public static bool operator !=(Range<TUnit> left, Range<TUnit> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0005D1FD File Offset: 0x0005B3FD
		public override string ToString()
		{
			return string.Format("[{0}, {1}]", this.Min, this.Max);
		}
	}
}
