using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	internal sealed class GroupMinHashDim
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001D40E File Offset: 0x0001B60E
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001D416 File Offset: 0x0001B616
		public int NumEntries { get; private set; }

		// Token: 0x0600069D RID: 1693 RVA: 0x0001D41F File Offset: 0x0001B61F
		public GroupMinHashDim()
		{
			this.entries = new GroupMinHashDim.Entry[2];
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001D433 File Offset: 0x0001B633
		public void BeginUpdate()
		{
			this.NumEntries = 0;
			this.bScanInHeapRegion = true;
			this.heapSize = 1;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001D44C File Offset: 0x0001B64C
		public void AddMinHash(int ruleId, int rep, double minHash)
		{
			if (this.NumEntries == this.entries.Length - 1)
			{
				Array.Resize<GroupMinHashDim.Entry>(ref this.entries, (int)((double)(this.NumEntries + 1) * 1.5));
			}
			int numEntries = this.NumEntries;
			this.NumEntries = numEntries + 1;
			this.entries[this.NumEntries].Reset(ruleId, rep, minHash);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		public void EndUpdate()
		{
			this.heapSize = this.NumEntries;
			for (int i = this.heapSize / 2; i > 0; i--)
			{
				this.Heapify(i);
			}
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001D4E7 File Offset: 0x0001B6E7
		public void GetFirst()
		{
			if (this.heapSize < this.NumEntries)
			{
				this.bScanInHeapRegion = false;
				this.m_scanSortIdx = this.NumEntries;
				return;
			}
			this.bScanInHeapRegion = true;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001D514 File Offset: 0x0001B714
		public bool GetNext()
		{
			if (this.bScanInHeapRegion)
			{
				if (this.heapSize <= 1)
				{
					return false;
				}
				GroupMinHashDim.Entry entry = this.entries[1];
				this.entries[1] = this.entries[this.heapSize];
				this.entries[this.heapSize] = entry;
				this.heapSize--;
				this.Heapify(1);
				return true;
			}
			else
			{
				this.m_scanSortIdx--;
				if (this.m_scanSortIdx > this.heapSize)
				{
					return true;
				}
				this.bScanInHeapRegion = true;
				return this.heapSize > 0;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001D5B5 File Offset: 0x0001B7B5
		public int GetCurrentId()
		{
			return this.entries[this.bScanInHeapRegion ? 1 : this.m_scanSortIdx].ruleId;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001D5D8 File Offset: 0x0001B7D8
		public double GetCurrentMinHash()
		{
			return this.entries[this.bScanInHeapRegion ? 1 : this.m_scanSortIdx].minHash;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001D5FB File Offset: 0x0001B7FB
		public int GetCurrentRep()
		{
			return this.entries[this.bScanInHeapRegion ? 1 : this.m_scanSortIdx].rep;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001D620 File Offset: 0x0001B820
		private void Heapify(int n)
		{
			int num;
			while ((num = n * 2) <= this.heapSize)
			{
				int num2;
				GroupMinHashDim.Entry entry;
				if ((num2 = num + 1) > this.heapSize)
				{
					if (this.entries[n].minHash > this.entries[num].minHash || (this.entries[n].minHash == this.entries[num].minHash && this.entries[n].rep > this.entries[num].rep))
					{
						entry = this.entries[n];
						this.entries[n] = this.entries[num];
						this.entries[num] = entry;
					}
					return;
				}
				int num3 = ((this.entries[num2].minHash > this.entries[num].minHash || (this.entries[num2].minHash == this.entries[num].minHash && this.entries[num2].rep > this.entries[num].rep)) ? num : num2);
				if (this.entries[n].minHash <= this.entries[num3].minHash && (this.entries[n].minHash != this.entries[num3].minHash || this.entries[n].rep <= this.entries[num3].rep))
				{
					return;
				}
				entry = this.entries[n];
				this.entries[n] = this.entries[num3];
				this.entries[num3] = entry;
				n = num3;
			}
		}

		// Token: 0x0400025E RID: 606
		private GroupMinHashDim.Entry[] entries;

		// Token: 0x04000260 RID: 608
		private int heapSize;

		// Token: 0x04000261 RID: 609
		private bool bScanInHeapRegion;

		// Token: 0x04000262 RID: 610
		private int m_scanSortIdx;

		// Token: 0x0200016D RID: 365
		[Serializable]
		private struct Entry
		{
			// Token: 0x06000CEC RID: 3308 RVA: 0x0003756C File Offset: 0x0003576C
			public void Reset(int i, int r, double m)
			{
				this.ruleId = i;
				this.rep = r;
				this.minHash = m;
			}

			// Token: 0x040005D6 RID: 1494
			public int ruleId;

			// Token: 0x040005D7 RID: 1495
			public int rep;

			// Token: 0x040005D8 RID: 1496
			public double minHash;
		}
	}
}
