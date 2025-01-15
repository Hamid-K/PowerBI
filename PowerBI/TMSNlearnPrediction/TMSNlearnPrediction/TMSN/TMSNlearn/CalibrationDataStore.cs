using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.MachineLearning;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.TMSN.TMSNlearn
{
	// Token: 0x020004D3 RID: 1235
	public sealed class CalibrationDataStore : IEnumerable<CalibrationDataStore.DataItem>, IEnumerable
	{
		// Token: 0x06001955 RID: 6485 RVA: 0x0008F2E6 File Offset: 0x0008D4E6
		public CalibrationDataStore()
			: this(1000000)
		{
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0008F2F4 File Offset: 0x0008D4F4
		public CalibrationDataStore(int capacity)
		{
			Contracts.CheckParam(capacity > 0, "capacity", "must be positive");
			this._capacity = capacity;
			this._data = new CalibrationDataStore.DataItem[Math.Min(4, capacity)];
			this._random = new Random(Interlocked.Increment(ref CalibrationDataStore._randSeed) - 1);
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x0008F370 File Offset: 0x0008D570
		public IEnumerator<CalibrationDataStore.DataItem> GetEnumerator()
		{
			if (!this._dataSorted)
			{
				Comparer<CalibrationDataStore.DataItem> comparer = Comparer<CalibrationDataStore.DataItem>.Create((CalibrationDataStore.DataItem x, CalibrationDataStore.DataItem y) => x.Score.CompareTo(y.Score));
				Array.Sort<CalibrationDataStore.DataItem>(this._data, 0, Math.Min(this._itemsSeen, this._capacity), comparer);
				this._dataSorted = true;
			}
			return this._data.Take(this._itemsSeen).GetEnumerator();
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0008F3E3 File Offset: 0x0008D5E3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0008F3EC File Offset: 0x0008D5EC
		public void AddToStore(float score, bool isPositive, float weight)
		{
			if (weight == 0f || float.IsNaN(score))
			{
				return;
			}
			int num = this._itemsSeen++;
			if (this._itemsSeen <= this._capacity)
			{
				Utils.EnsureSize<CalibrationDataStore.DataItem>(ref this._data, this._itemsSeen, this._capacity, true);
			}
			else
			{
				num = this._random.Next(this._itemsSeen);
				if (num >= this._capacity)
				{
					return;
				}
			}
			this._data[num] = new CalibrationDataStore.DataItem(isPositive, weight, score);
		}

		// Token: 0x04000F2F RID: 3887
		private int _itemsSeen;

		// Token: 0x04000F30 RID: 3888
		private readonly Random _random;

		// Token: 0x04000F31 RID: 3889
		private static int _randSeed;

		// Token: 0x04000F32 RID: 3890
		private readonly int _capacity;

		// Token: 0x04000F33 RID: 3891
		private CalibrationDataStore.DataItem[] _data;

		// Token: 0x04000F34 RID: 3892
		private bool _dataSorted;

		// Token: 0x020004D4 RID: 1236
		public struct DataItem
		{
			// Token: 0x0600195B RID: 6491 RVA: 0x0008F47A File Offset: 0x0008D67A
			public DataItem(bool target, float weight, float score)
			{
				this.Target = target;
				this.Weight = weight;
				this.Score = score;
			}

			// Token: 0x04000F36 RID: 3894
			public readonly bool Target;

			// Token: 0x04000F37 RID: 3895
			public readonly float Weight;

			// Token: 0x04000F38 RID: 3896
			public readonly float Score;
		}
	}
}
