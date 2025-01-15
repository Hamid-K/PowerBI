using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x020005ED RID: 1517
	public class Ranges<TUnit> : IReadOnlyList<Range<TUnit>>, IReadOnlyCollection<Range<TUnit>>, IEnumerable<Range<TUnit>>, IEnumerable where TUnit : BoundsUnit
	{
		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x0005D66B File Offset: 0x0005B86B
		public int Count
		{
			get
			{
				return this._ranges.Count;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x0005D678 File Offset: 0x0005B878
		public static Ranges<TUnit> Empty { get; } = new Ranges<TUnit>(Enumerable.Empty<Range<TUnit>>(), true);

		// Token: 0x170005BC RID: 1468
		public Range<TUnit> this[int index]
		{
			get
			{
				return this._ranges[index];
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0005D68D File Offset: 0x0005B88D
		public Range<TUnit>? JoinOfRanges
		{
			get
			{
				return this._entireRange;
			}
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0005D698 File Offset: 0x0005B898
		private Ranges(IEnumerable<Range<TUnit>> ranges, bool sorted)
		{
			if (!sorted)
			{
				List<Range<TUnit>> list = new List<Range<TUnit>>();
				foreach (Range<TUnit> range in ranges.OrderBy((Range<TUnit> r) => r.Min))
				{
					if (list.Count == 0)
					{
						list.Add(range);
					}
					else
					{
						Range<TUnit> range2 = list.Last<Range<TUnit>>();
						if (range2.Max >= range.Min - 1)
						{
							list[list.Count - 1] = range2.Join(range);
						}
						else
						{
							list.Add(range);
						}
					}
				}
				this._ranges = list;
			}
			else
			{
				this._ranges = ranges.ToList<Range<TUnit>>();
			}
			if (this._ranges.Any<Range<TUnit>>())
			{
				this._entireRange = new Range<TUnit>?(new Range<TUnit>(this._ranges[0].Min, this._ranges.Last<Range<TUnit>>().Max));
			}
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0005D7B4 File Offset: 0x0005B9B4
		public Ranges(IEnumerable<int> xs, bool sorted = false)
		{
			List<Range<TUnit>> list = new List<Range<TUnit>>();
			int? num = null;
			int? num2 = null;
			IEnumerable<int> enumerable2;
			if (!sorted)
			{
				IEnumerable<int> enumerable = xs.OrderBy((int x) => x);
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = xs;
			}
			foreach (int num3 in enumerable2)
			{
				if (num == null)
				{
					num2 = (num = new int?(num3));
				}
				else
				{
					int? num4 = num2;
					int num5 = num3 - 1;
					if ((num4.GetValueOrDefault() == num5) & (num4 != null))
					{
						num2 = new int?(num3);
					}
					else
					{
						list.Add(new Range<TUnit>(num.Value, num2.Value));
						num2 = (num = new int?(num3));
					}
				}
			}
			if (num != null)
			{
				list.Add(new Range<TUnit>(num.Value, num2.Value));
			}
			this._ranges = list;
			if (this._ranges.Any<Range<TUnit>>())
			{
				this._entireRange = new Range<TUnit>?(new Range<TUnit>(this._ranges[0].Min, this._ranges.Last<Range<TUnit>>().Max));
			}
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0005D910 File Offset: 0x0005BB10
		public Ranges(IEnumerable<bool> xs)
			: this(xs.Enumerate<bool>().Where2((int i, bool include) => include).Select2((int i, bool _) => i), true)
		{
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0005D972 File Offset: 0x0005BB72
		public Ranges(IEnumerable<Range<TUnit>> ranges)
			: this(ranges, false)
		{
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0005D97C File Offset: 0x0005BB7C
		public Ranges(Range<TUnit> range)
		{
			this._ranges = new Range<TUnit>[] { range };
			this._entireRange = new Range<TUnit>?(range);
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0005D9A4 File Offset: 0x0005BBA4
		public IEnumerator<Range<TUnit>> GetEnumerator()
		{
			return this._ranges.GetEnumerator();
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0005D9B1 File Offset: 0x0005BBB1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0005D9BC File Offset: 0x0005BBBC
		public Ranges<TUnit> Subtract(Range<TUnit> other)
		{
			return new Ranges<TUnit>(this.SelectMany((Range<TUnit> r) => r.Subtract(other)), true);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0005D9F0 File Offset: 0x0005BBF0
		public Ranges<TUnit> Subtract(IEnumerable<Range<TUnit>> other)
		{
			Ranges<TUnit> ranges = this;
			foreach (Range<TUnit> range in other)
			{
				if (ranges._entireRange == null)
				{
					return ranges;
				}
				if (ranges._entireRange.Value.Contains(range))
				{
					ranges = ranges.Subtract(range);
				}
			}
			return ranges;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0005DA68 File Offset: 0x0005BC68
		public Ranges<TUnit> Intersect(Range<TUnit> other)
		{
			if (this._entireRange == null)
			{
				return this;
			}
			if (!this._entireRange.Value.Intersect(other).HasValue)
			{
				return Ranges<TUnit>.Empty;
			}
			return new Ranges<TUnit>(this.SelectMany((Range<TUnit> r) => r.Intersect(other)), true);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0005DAD4 File Offset: 0x0005BCD4
		public Ranges<TUnit> Intersect(Ranges<TUnit> other)
		{
			if (this._entireRange == null)
			{
				return this;
			}
			if (other._entireRange == null)
			{
				return other;
			}
			if (!this._entireRange.Value.Intersect(other._entireRange.Value).HasValue)
			{
				return Ranges<TUnit>.Empty;
			}
			return new Ranges<TUnit>(this.SelectMany(new Func<Range<TUnit>, IEnumerable<Range<TUnit>>>(other.Intersect)), true);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0005DB45 File Offset: 0x0005BD45
		public Ranges<TUnit> Join(IEnumerable<Range<TUnit>> other)
		{
			return new Ranges<TUnit>(this.Concat(other));
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0005DB53 File Offset: 0x0005BD53
		public Ranges<TUnit> Join(Range<TUnit> other)
		{
			return this.Join(new Range<TUnit>[] { other });
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0005DB6C File Offset: 0x0005BD6C
		public bool Overlaps(Range<TUnit> range)
		{
			if (this._entireRange == null || !this._entireRange.Value.Overlaps(range))
			{
				return false;
			}
			foreach (Range<TUnit> range2 in this._ranges)
			{
				if (range2.Max >= range.Min)
				{
					if (range2.Min > range.Max)
					{
						return false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0005DC04 File Offset: 0x0005BE04
		public bool Overlaps(Ranges<TUnit> ranges)
		{
			return ranges._entireRange != null && this.Overlaps(ranges._entireRange.Value) && ranges.Any(new Func<Range<TUnit>, bool>(this.Overlaps));
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0005DC3C File Offset: 0x0005BE3C
		public bool Contains(Range<TUnit> range)
		{
			if (this._entireRange == null || !this._entireRange.Value.Contains(range))
			{
				return false;
			}
			foreach (Range<TUnit> range2 in this._ranges)
			{
				if (range2.Max >= range.Min)
				{
					return range2.Contains(range);
				}
			}
			return false;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0005DCC8 File Offset: 0x0005BEC8
		public bool Contains(int x)
		{
			if (this._entireRange == null || !this._entireRange.Value.Contains(x))
			{
				return false;
			}
			foreach (Range<TUnit> range in this._ranges)
			{
				if (range.Max >= x)
				{
					return range.Contains(x);
				}
			}
			return false;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0005DD4C File Offset: 0x0005BF4C
		public bool Contains(Ranges<TUnit> ranges)
		{
			return ranges._entireRange == null || (this._entireRange != null && this._entireRange.Value.Contains(ranges._entireRange.Value) && ranges.All(new Func<Range<TUnit>, bool>(this.Contains)));
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0005DDA9 File Offset: 0x0005BFA9
		public bool ContainedBy(Range<TUnit> range)
		{
			return this._entireRange == null || range.Contains(this._entireRange.Value);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0005DDCC File Offset: 0x0005BFCC
		public Optional<int> MinDistance(Ranges<TUnit> other)
		{
			Range<TUnit>? range = this._entireRange;
			if (range != null)
			{
				Range<TUnit> valueOrDefault = range.GetValueOrDefault();
				range = other._entireRange;
				if (range != null)
				{
					Range<TUnit> valueOrDefault2 = range.GetValueOrDefault();
					if (!valueOrDefault.Intersect(valueOrDefault2).HasValue)
					{
						return valueOrDefault.Distance(valueOrDefault2).Some<int>();
					}
					int num = int.MaxValue;
					int num2 = 0;
					int num3 = 0;
					while (num2 < this.Count && num3 < other.Count)
					{
						num = Math.Min(num, this[num2].Distance(other[num3]));
						if (num == 0)
						{
							break;
						}
						if (this[num2].Min < other[num3].Min)
						{
							num2++;
						}
						else
						{
							num3++;
						}
					}
					return num.Some<int>();
				}
			}
			return Optional<int>.Nothing;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0005DEB0 File Offset: 0x0005C0B0
		public override string ToString()
		{
			return string.Join<Range<TUnit>>(", ", this._ranges);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0005DEC2 File Offset: 0x0005C0C2
		public override int GetHashCode()
		{
			return this._ranges.OrderDependentHashCode<Range<TUnit>>();
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0005DED0 File Offset: 0x0005C0D0
		public override bool Equals(object obj)
		{
			Ranges<TUnit> ranges = obj as Ranges<TUnit>;
			return ranges != null && ranges._ranges.SequenceEqual(this._ranges);
		}

		// Token: 0x04000FB1 RID: 4017
		private readonly Range<TUnit>? _entireRange;

		// Token: 0x04000FB2 RID: 4018
		private readonly IReadOnlyList<Range<TUnit>> _ranges;
	}
}
