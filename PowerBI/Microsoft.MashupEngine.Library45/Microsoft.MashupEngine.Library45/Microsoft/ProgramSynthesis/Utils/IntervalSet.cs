using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x0200049A RID: 1178
	public class IntervalSet
	{
		// Token: 0x06001A7B RID: 6779 RVA: 0x0004FD57 File Offset: 0x0004DF57
		public IntervalSet(int universeStart, int universeEnd)
			: this(new Interval(universeStart, universeEnd - universeStart))
		{
			if (universeEnd < universeStart)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("universeEnd cannot be less than universeStart", Array.Empty<object>())));
			}
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x0004FD86 File Offset: 0x0004DF86
		private IntervalSet(Interval universe)
		{
			this.Universe = universe;
			this._coveredIntervals = new List<Interval>();
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		public Interval Universe { get; }

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001A7E RID: 6782 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
		public int UniverseStart
		{
			get
			{
				return this.Universe.Start;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0004FDC4 File Offset: 0x0004DFC4
		public int UniverseEnd
		{
			get
			{
				return this.UniverseStart + this.Universe.Length;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001A80 RID: 6784 RVA: 0x0004FDE8 File Offset: 0x0004DFE8
		public int UniverseLength
		{
			get
			{
				return this.Universe.Length;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x0004FE03 File Offset: 0x0004E003
		public IEnumerable<Interval> UncoveredIntervals
		{
			get
			{
				this._ComputeUncoveredIntervals();
				return this._uncoveredIntervals;
			}
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0004FE14 File Offset: 0x0004E014
		private void _ComputeUncoveredIntervals()
		{
			if (this._uncoveredIntervals != null)
			{
				return;
			}
			if (!this._coveredIntervals.Any<Interval>())
			{
				this._uncoveredIntervals = new List<Interval> { this.Universe }.ToImmutableList<Interval>();
				return;
			}
			List<Interval> list = new List<Interval>();
			IEnumerable<IGrouping<int, Interval>> enumerable = from s in this._coveredIntervals
				group s by s.Start into g
				orderby g.Key
				select g;
			int num = this.UniverseStart;
			foreach (IGrouping<int, Interval> grouping in enumerable)
			{
				int key = grouping.Key;
				int num2 = grouping.Max((Interval s) => s.Length);
				if (key + num2 >= num)
				{
					if (num < key)
					{
						list.Add(new Interval(num, key - num));
					}
					num = key + num2;
				}
			}
			if (num < this.UniverseEnd)
			{
				list.Add(new Interval(num, this.UniverseEnd - num));
			}
			this._uncoveredIntervals = list.ToImmutableList<Interval>();
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x0004FF58 File Offset: 0x0004E158
		public void CoverInterval(Interval interval)
		{
			this._uncoveredIntervals = null;
			this._coveredIntervals.Add(interval);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x0004FF6D File Offset: 0x0004E16D
		public void CoverInterval(int intervalStart, int intervalEnd)
		{
			this.CoverInterval(new Interval(intervalStart, intervalEnd - intervalStart));
		}

		// Token: 0x04000D0F RID: 3343
		private readonly List<Interval> _coveredIntervals;

		// Token: 0x04000D10 RID: 3344
		private ImmutableList<Interval> _uncoveredIntervals;
	}
}
