using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000AC RID: 172
	internal sealed class GroupMinHashDimAggr
	{
		// Token: 0x060006A9 RID: 1705 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		public GroupMinHashDimAggr(int id, RuleApplConstraints globalRuleConstraints)
		{
			this.m_dimId = id;
			this.m_ruleConstraints = globalRuleConstraints;
			this.m_groupMinHashDimForPos = new GroupMinHashDim[0];
			this.m_validPosList = new int[0];
			this.m_ruleStatusSet = new int[0];
			this.pqArray = new GroupMinHashDimAggr.PQEntry[1];
			this.m_sortArray = new GroupMinHashDimAggr.SortEntry[0];
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001D931 File Offset: 0x0001BB31
		public bool RequiresCapacityIncrease(int maxPos, int maxRules)
		{
			return this.m_groupMinHashDimForPos.Length < maxPos || this.m_ruleStatusSet.Length < maxRules;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001D94C File Offset: 0x0001BB4C
		public void IncreaseCapacity(int maxPos, int maxRules)
		{
			if (this.m_groupMinHashDimForPos.Length < maxPos)
			{
				int num = (int)((double)maxPos * 1.5);
				Array.Resize<GroupMinHashDim>(ref this.m_groupMinHashDimForPos, num);
				Array.Resize<int>(ref this.m_validPosList, num);
				Array.Resize<GroupMinHashDimAggr.PQEntry>(ref this.pqArray, num + 1);
			}
			if (this.m_ruleStatusSet.Length < maxRules)
			{
				Array.Resize<int>(ref this.m_ruleStatusSet, (int)((double)maxRules * 1.5));
				Array.Resize<GroupMinHashDimAggr.SortEntry>(ref this.m_sortArray, (int)((double)maxRules * 1.5));
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
		public void BeginUpdate(double[][] ruleMinHash, int[][] ruleMinHashRep, int[] ruleIdToPos)
		{
			this.m_numValidPos = 0;
			this.m_ruleMinHash = ruleMinHash;
			this.m_ruleMinHashRep = ruleMinHashRep;
			this.m_ruleIdToPos = ruleIdToPos;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		public void AddGroupMinHashDim(int pos, GroupMinHashDim gmhd)
		{
			this.m_groupMinHashDimForPos[pos] = gmhd;
			int[] validPosList = this.m_validPosList;
			int numValidPos = this.m_numValidPos;
			this.m_numValidPos = numValidPos + 1;
			validPosList[numValidPos] = pos;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001DA24 File Offset: 0x0001BC24
		public void EndUpdate()
		{
			for (int i = 0; i < this.m_numValidPos; i++)
			{
				int num = this.m_validPosList[i];
				this.m_groupMinHashDimForPos[num].GetFirst();
				this.pqArray[i + 1].Reset(num, this.m_groupMinHashDimForPos[num].GetCurrentMinHash());
			}
			this.BuildHeap();
			this.m_sortArraySize = 0;
			for (;;)
			{
				int num = this.pqArray[1].pos;
				GroupMinHashDimAggr.SortEntry[] sortArray = this.m_sortArray;
				int sortArraySize = this.m_sortArraySize;
				this.m_sortArraySize = sortArraySize + 1;
				sortArray[sortArraySize].Reset(num, this.m_groupMinHashDimForPos[num].GetCurrentId());
				if (!this.m_groupMinHashDimForPos[num].GetNext())
				{
					break;
				}
				this.pqArray[1].minHash = this.m_groupMinHashDimForPos[num].GetCurrentMinHash();
				this.Heapify(1);
			}
			this.b_posSet = false;
			this.m_curScanIdx = -1;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		public void Reset()
		{
			for (int i = 0; i < this.m_numRuleStatusSet; i++)
			{
				this.m_ruleConstraints.RuleStatus[this.m_ruleStatusSet[i]] = 0;
				this.m_ruleConstraints.FreeRulesAtPos[this.m_ruleIdToPos[this.m_ruleStatusSet[i]]]++;
			}
			this.m_numRuleStatusSet = 0;
			this.m_curScanIdx = -1;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001DB74 File Offset: 0x0001BD74
		public bool GetNext()
		{
			if (this.m_curScanIdx != -1)
			{
				if (!this.b_posSet)
				{
					return false;
				}
				int num = this.m_sortArray[this.m_curScanIdx].ruleId;
				int num2 = this.m_sortArray[this.m_curScanIdx].pos;
				this.m_ruleConstraints.RuleAtPos[num2] = -1;
				this.b_posSet = false;
				if (this.m_ruleConstraints.FreeRulesAtPos[num2] == 1)
				{
					return false;
				}
				this.m_ruleConstraints.RuleStatus[num] = -1;
				int[] ruleStatusSet = this.m_ruleStatusSet;
				int numRuleStatusSet = this.m_numRuleStatusSet;
				this.m_numRuleStatusSet = numRuleStatusSet + 1;
				ruleStatusSet[numRuleStatusSet] = num;
				this.m_ruleConstraints.FreeRulesAtPos[num2]--;
			}
			this.m_curScanIdx++;
			while (this.m_curScanIdx < this.m_sortArraySize)
			{
				int num = this.m_sortArray[this.m_curScanIdx].ruleId;
				int num2 = this.m_sortArray[this.m_curScanIdx].pos;
				if (this.m_ruleConstraints.RuleStatus[num] != -1)
				{
					if (this.m_ruleConstraints.RuleAtPos[num2] == -1)
					{
						this.m_ruleConstraints.RuleAtPos[num2] = num;
						this.b_posSet = true;
						return true;
					}
					if (this.m_ruleConstraints.RuleAtPos[num2] == num)
					{
						return true;
					}
				}
				this.m_curScanIdx++;
			}
			return false;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001DCD3 File Offset: 0x0001BED3
		public int GetCurrentRuleId()
		{
			return this.m_sortArray[this.m_curScanIdx].ruleId;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001DCEB File Offset: 0x0001BEEB
		public int GetCurrentRep()
		{
			return this.m_ruleMinHashRep[this.m_sortArray[this.m_curScanIdx].ruleId][this.m_dimId];
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001DD14 File Offset: 0x0001BF14
		private void BuildHeap()
		{
			for (int i = this.m_numValidPos / 2; i > 0; i--)
			{
				this.Heapify(i);
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001DD3C File Offset: 0x0001BF3C
		private int CompareRep(int n1, int n2)
		{
			int pos = this.pqArray[n1].pos;
			int pos2 = this.pqArray[n2].pos;
			int num;
			if ((num = this.m_ruleConstraints.RuleAtPos[pos]) == -1)
			{
				num = this.m_groupMinHashDimForPos[pos].GetCurrentId();
			}
			int num2;
			if ((num2 = this.m_ruleConstraints.RuleAtPos[pos2]) == -1)
			{
				num2 = this.m_groupMinHashDimForPos[pos2].GetCurrentId();
			}
			return this.m_ruleMinHashRep[num][this.m_dimId] - this.m_ruleMinHashRep[num2][this.m_dimId];
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		private void Heapify(int n)
		{
			int num;
			while ((num = n * 2) <= this.m_numValidPos)
			{
				int num2;
				GroupMinHashDimAggr.PQEntry pqentry;
				if ((num2 = num + 1) > this.m_numValidPos)
				{
					if (this.pqArray[n].minHash > this.pqArray[num].minHash || (this.pqArray[n].minHash == this.pqArray[num].minHash && this.CompareRep(n, num) > 0))
					{
						pqentry = this.pqArray[n];
						this.pqArray[n] = this.pqArray[num];
						this.pqArray[num] = pqentry;
					}
					return;
				}
				int num3 = ((this.pqArray[num2].minHash > this.pqArray[num].minHash || (this.pqArray[num2].minHash == this.pqArray[num].minHash && this.CompareRep(num2, num) > 0)) ? num : num2);
				if (this.pqArray[n].minHash <= this.pqArray[num3].minHash && (this.pqArray[n].minHash != this.pqArray[num3].minHash || this.CompareRep(n, num3) <= 0))
				{
					return;
				}
				pqentry = this.pqArray[n];
				this.pqArray[n] = this.pqArray[num3];
				this.pqArray[num3] = pqentry;
				n = num3;
			}
		}

		// Token: 0x04000266 RID: 614
		private int m_dimId;

		// Token: 0x04000267 RID: 615
		private int[] m_validPosList;

		// Token: 0x04000268 RID: 616
		private int m_numValidPos;

		// Token: 0x04000269 RID: 617
		private GroupMinHashDim[] m_groupMinHashDimForPos;

		// Token: 0x0400026A RID: 618
		private RuleApplConstraints m_ruleConstraints;

		// Token: 0x0400026B RID: 619
		private int[] m_ruleStatusSet;

		// Token: 0x0400026C RID: 620
		private int m_numRuleStatusSet;

		// Token: 0x0400026D RID: 621
		private bool b_posSet;

		// Token: 0x0400026E RID: 622
		private GroupMinHashDimAggr.PQEntry[] pqArray;

		// Token: 0x0400026F RID: 623
		private GroupMinHashDimAggr.SortEntry[] m_sortArray;

		// Token: 0x04000270 RID: 624
		private int m_sortArraySize;

		// Token: 0x04000271 RID: 625
		private int m_curScanIdx;

		// Token: 0x04000272 RID: 626
		private double[][] m_ruleMinHash;

		// Token: 0x04000273 RID: 627
		private int[][] m_ruleMinHashRep;

		// Token: 0x04000274 RID: 628
		private int[] m_ruleIdToPos;

		// Token: 0x0200016E RID: 366
		private struct PQEntry
		{
			// Token: 0x06000CED RID: 3309 RVA: 0x00037583 File Offset: 0x00035783
			public void Reset(int p, double m)
			{
				this.pos = p;
				this.minHash = m;
			}

			// Token: 0x040005D9 RID: 1497
			public int pos;

			// Token: 0x040005DA RID: 1498
			public double minHash;
		}

		// Token: 0x0200016F RID: 367
		private struct SortEntry
		{
			// Token: 0x06000CEE RID: 3310 RVA: 0x00037593 File Offset: 0x00035793
			public void Reset(int p, int r)
			{
				this.pos = p;
				this.ruleId = r;
			}

			// Token: 0x040005DB RID: 1499
			public int pos;

			// Token: 0x040005DC RID: 1500
			public int ruleId;
		}
	}
}
