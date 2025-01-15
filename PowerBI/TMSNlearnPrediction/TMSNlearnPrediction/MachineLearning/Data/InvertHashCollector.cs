using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001BA RID: 442
	internal sealed class InvertHashCollector<T>
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x00034D70 File Offset: 0x00032F70
		public InvertHashCollector(int slots, int maxCount, ValueMapper<T, StringBuilder> mapper, IEqualityComparer<T> comparer, ValueMapper<T, T> copier = null)
		{
			this._slots = slots;
			this._maxCount = maxCount;
			this._stringifyMapper = mapper;
			this._comparer = new InvertHashCollector<T>.PairEqualityComparer(comparer);
			this._slotToValueSet = new Dictionary<int, HashSet<InvertHashCollector<T>.Pair>>();
			ValueMapper<T, T> valueMapper = copier;
			if (copier == null)
			{
				valueMapper = delegate(ref T src, ref T dst)
				{
					dst = src;
				};
			}
			this._copier = valueMapper;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00034DEC File Offset: 0x00032FEC
		private DvText Textify(ref StringBuilder sb, ref StringBuilder temp, ref char[] cbuffer, ref InvertHashCollector<T>.Pair[] buffer, HashSet<InvertHashCollector<T>.Pair> pairs)
		{
			int count = pairs.Count;
			Utils.EnsureSize<InvertHashCollector<T>.Pair>(ref buffer, count, true);
			pairs.CopyTo(buffer);
			pairs.Clear();
			if (count != 1)
			{
				Array.Sort<InvertHashCollector<T>.Pair>(buffer, 0, count, Comparer<InvertHashCollector<T>.Pair>.Create((InvertHashCollector<T>.Pair x, InvertHashCollector<T>.Pair y) => x.Order - y.Order));
				if (sb == null)
				{
					sb = new StringBuilder();
				}
				sb.Append('{');
				for (int i = 0; i < count; i++)
				{
					InvertHashCollector<T>.Pair pair = buffer[i];
					if (i > 0)
					{
						sb.Append(',');
					}
					T value = pair.Value;
					this._stringifyMapper.Invoke(ref value, ref temp);
					InvertHashUtils.AppendToEnd(temp, sb, ref cbuffer);
				}
				sb.Append('}');
				DvText dvText;
				dvText..ctor(sb.ToString());
				sb.Clear();
				return dvText;
			}
			T value2 = buffer[0].Value;
			this._stringifyMapper.Invoke(ref value2, ref temp);
			if (Utils.Size(temp) <= 0)
			{
				return DvText.Empty;
			}
			return new DvText(temp.ToString());
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00034F0C File Offset: 0x0003310C
		public VBuffer<DvText> GetMetadata()
		{
			int count = this._slotToValueSet.Count;
			StringBuilder stringBuilder = null;
			StringBuilder stringBuilder2 = null;
			InvertHashCollector<T>.Pair[] array = null;
			char[] array2 = null;
			bool flag = count <= this._slots / 2;
			if (flag)
			{
				int[] array3 = new int[count];
				DvText[] array4 = new DvText[count];
				int num = 0;
				foreach (KeyValuePair<int, HashSet<InvertHashCollector<T>.Pair>> keyValuePair in this._slotToValueSet)
				{
					array3[num] = keyValuePair.Key;
					array4[num++] = this.Textify(ref stringBuilder, ref stringBuilder2, ref array2, ref array, keyValuePair.Value);
				}
				Array.Sort<int, DvText>(array3, array4);
				return new VBuffer<DvText>(this._slots, count, array4, array3);
			}
			DvText[] array5 = new DvText[this._slots];
			foreach (KeyValuePair<int, HashSet<InvertHashCollector<T>.Pair>> keyValuePair2 in this._slotToValueSet)
			{
				array5[keyValuePair2.Key] = this.Textify(ref stringBuilder, ref stringBuilder2, ref array2, ref array, keyValuePair2.Value);
			}
			return new VBuffer<DvText>(array5.Length, array5, null);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00035068 File Offset: 0x00033268
		public void Add(int dstSlot, ValueGetter<T> getter, ref T key)
		{
			HashSet<InvertHashCollector<T>.Pair> hashSet;
			if (this._slotToValueSet.TryGetValue(dstSlot, out hashSet))
			{
				if (hashSet.Count >= this._maxCount)
				{
					return;
				}
			}
			else
			{
				hashSet = (this._slotToValueSet[dstSlot] = new HashSet<InvertHashCollector<T>.Pair>(this._comparer));
			}
			getter.Invoke(ref key);
			hashSet.Add(new InvertHashCollector<T>.Pair(key, hashSet.Count));
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000350D0 File Offset: 0x000332D0
		public void Add(int dstSlot, T key)
		{
			HashSet<InvertHashCollector<T>.Pair> hashSet;
			if (this._slotToValueSet.TryGetValue(dstSlot, out hashSet))
			{
				if (hashSet.Count >= this._maxCount)
				{
					return;
				}
			}
			else
			{
				hashSet = (this._slotToValueSet[dstSlot] = new HashSet<InvertHashCollector<T>.Pair>(this._comparer));
			}
			T t = default(T);
			this._copier.Invoke(ref key, ref t);
			hashSet.Add(new InvertHashCollector<T>.Pair(t, hashSet.Count));
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00035141 File Offset: 0x00033341
		public void Add(uint hash, ValueGetter<T> getter, ref T key)
		{
			if (hash == 0U)
			{
				return;
			}
			this.Add((int)(hash - 1U), getter, ref key);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00035152 File Offset: 0x00033352
		public void Add(uint hash, T key)
		{
			if (hash == 0U)
			{
				return;
			}
			this.Add((int)(hash - 1U), key);
		}

		// Token: 0x04000517 RID: 1303
		private readonly int _maxCount;

		// Token: 0x04000518 RID: 1304
		private readonly int _slots;

		// Token: 0x04000519 RID: 1305
		private readonly ValueMapper<T, StringBuilder> _stringifyMapper;

		// Token: 0x0400051A RID: 1306
		private readonly Dictionary<int, HashSet<InvertHashCollector<T>.Pair>> _slotToValueSet;

		// Token: 0x0400051B RID: 1307
		private readonly IEqualityComparer<InvertHashCollector<T>.Pair> _comparer;

		// Token: 0x0400051C RID: 1308
		private readonly ValueMapper<T, T> _copier;

		// Token: 0x020001BB RID: 443
		private struct Pair
		{
			// Token: 0x060009EE RID: 2542 RVA: 0x00035162 File Offset: 0x00033362
			public Pair(T value, int order)
			{
				this.Value = value;
				this.Order = order;
			}

			// Token: 0x0400051F RID: 1311
			public readonly T Value;

			// Token: 0x04000520 RID: 1312
			public readonly int Order;
		}

		// Token: 0x020001BC RID: 444
		private sealed class PairEqualityComparer : IEqualityComparer<InvertHashCollector<T>.Pair>
		{
			// Token: 0x060009EF RID: 2543 RVA: 0x00035172 File Offset: 0x00033372
			public PairEqualityComparer(IEqualityComparer<T> tComparer)
			{
				this._tComparer = tComparer;
			}

			// Token: 0x060009F0 RID: 2544 RVA: 0x00035181 File Offset: 0x00033381
			public bool Equals(InvertHashCollector<T>.Pair x, InvertHashCollector<T>.Pair y)
			{
				return this._tComparer.Equals(x.Value, y.Value);
			}

			// Token: 0x060009F1 RID: 2545 RVA: 0x0003519C File Offset: 0x0003339C
			public int GetHashCode(InvertHashCollector<T>.Pair obj)
			{
				return this._tComparer.GetHashCode(obj.Value);
			}

			// Token: 0x04000521 RID: 1313
			private readonly IEqualityComparer<T> _tComparer;
		}
	}
}
