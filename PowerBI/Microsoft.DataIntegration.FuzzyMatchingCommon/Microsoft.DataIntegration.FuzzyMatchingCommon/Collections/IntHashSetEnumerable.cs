using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x0200008B RID: 139
	[Serializable]
	public sealed class IntHashSetEnumerable : IntHashSet, IEnumerable<int>, IEnumerable, IEnumerator<int>, IDisposable, IEnumerator
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x00021FC0 File Offset: 0x000201C0
		public IntHashSetEnumerable(int suggestedCapacity)
			: base(suggestedCapacity)
		{
			this.m_elems = new int[this.m_hashBuckets.Length];
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00021FDC File Offset: 0x000201DC
		public new void Add(int elem)
		{
			int num = Utilities.GetHashCode(elem) & this.m_hashBucketMask;
			if (this.m_hashBuckets[num].ts < this.m_curTimestamp)
			{
				this.m_hashBuckets[num].key = elem;
				this.m_hashBuckets[num].ts = this.m_curTimestamp;
				int[] elems = this.m_elems;
				int numElemsInHashBuckets = this.m_numElemsInHashBuckets;
				this.m_numElemsInHashBuckets = numElemsInHashBuckets + 1;
				elems[numElemsInHashBuckets] = elem;
				return;
			}
			this.m_fallbackDictionary[elem] = 1;
			this.m_bFallbackUsed = true;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00022068 File Offset: 0x00020268
		public new void Clear()
		{
			this.m_curTimestamp++;
			if (this.m_curTimestamp == 2147483647)
			{
				for (int i = 0; i < this.m_hashBuckets.Length; i++)
				{
					this.m_hashBuckets[i].ts = 0;
				}
				this.m_curTimestamp = 1;
			}
			this.m_fallbackDictionary.Clear();
			this.m_numElemsInHashBuckets = 0;
			this.m_bFallbackUsed = false;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000220D5 File Offset: 0x000202D5
		public IEnumerator<int> GetEnumerator()
		{
			this.m_enumIdx = -1;
			if (this.m_bFallbackUsed)
			{
				this.m_fallbackDictionaryEnumerator = this.m_fallbackDictionary.GetEnumerator();
			}
			return this;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000220F8 File Offset: 0x000202F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00022100 File Offset: 0x00020300
		public int Current
		{
			get
			{
				if (this.m_enumIdx < this.m_numElemsInHashBuckets)
				{
					return this.m_elems[this.m_enumIdx];
				}
				KeyValuePair<int, int> keyValuePair = this.m_fallbackDictionaryEnumerator.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0002213C File Offset: 0x0002033C
		public void Dispose()
		{
			this.m_fallbackDictionaryEnumerator.Dispose();
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00022149 File Offset: 0x00020349
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00022158 File Offset: 0x00020358
		public bool MoveNext()
		{
			int num = this.m_enumIdx + 1;
			this.m_enumIdx = num;
			return num < this.m_numElemsInHashBuckets || (this.m_bFallbackUsed && this.m_fallbackDictionaryEnumerator.MoveNext());
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00022195 File Offset: 0x00020395
		public void Reset()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000124 RID: 292
		private int[] m_elems;

		// Token: 0x04000125 RID: 293
		private int m_numElemsInHashBuckets;

		// Token: 0x04000126 RID: 294
		private int m_enumIdx;

		// Token: 0x04000127 RID: 295
		private bool m_bFallbackUsed;

		// Token: 0x04000128 RID: 296
		private Dictionary<int, int>.Enumerator m_fallbackDictionaryEnumerator;
	}
}
