using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	internal sealed class IndexedSubList<T> where T : ITransformationMatch
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x00032571 File Offset: 0x00030771
		public IndexedSubList()
		{
			this.m_tranMatchList = new T[2];
			this.m_tranMatchSourceIndexes = new int[2];
			this.m_sortNodes = new IndexedSubList<T>.SortNode[2];
			this.m_numTranMatches = 0;
			this.m_firstValidSortNodeIdx = 0;
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000325AC File Offset: 0x000307AC
		public IndexedSubList(IList<T> matchList)
			: this()
		{
			this.BeginListSpecification();
			for (int i = 0; i < matchList.Count; i++)
			{
				this.Add(matchList[i], i);
			}
			this.EndListSpecification();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000325EA File Offset: 0x000307EA
		public void BeginListSpecification()
		{
			this.m_numTranMatches = 0;
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x000325F4 File Offset: 0x000307F4
		public void Add(T tranMatch, int tranMatchSourceIndex)
		{
			if (this.m_numTranMatches == this.m_tranMatchList.Length)
			{
				Array.Resize<T>(ref this.m_tranMatchList, Math.Max(1, (int)((float)this.m_numTranMatches * 2f)));
				Array.Resize<int>(ref this.m_tranMatchSourceIndexes, Math.Max(1, (int)((float)this.m_numTranMatches * 2f)));
			}
			this.m_tranMatchList[this.m_numTranMatches] = tranMatch;
			this.m_tranMatchSourceIndexes[this.m_numTranMatches] = tranMatchSourceIndex;
			this.m_numTranMatches++;
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00032680 File Offset: 0x00030880
		public void EndListSpecification()
		{
			this.InitSuccessorOverlapIds();
			if (this.m_sortNodes.Length < 2 * this.m_numTranMatches)
			{
				Array.Resize<IndexedSubList<T>.SortNode>(ref this.m_sortNodes, this.m_numTranMatches * 2);
			}
			int num = 0;
			for (int i = 0; i < this.m_numTranMatches; i++)
			{
				T t = this.m_tranMatchList[i];
				this.m_sortNodes[num].Position = t.Position;
				this.m_sortNodes[num].IsBegin = true;
				this.m_sortNodes[num].MatchIdx = i;
				num++;
				this.m_sortNodes[num].Position = t.Position + t.Transformation.From.Count;
				this.m_sortNodes[num].IsBegin = false;
				this.m_sortNodes[num].MatchIdx = i;
				num++;
			}
			this.Sort();
			int num2 = 0;
			for (int j = 0; j < this.m_numTranMatches; j++)
			{
			}
			for (int k = 1; k < num; k++)
			{
				if (this.m_sortNodes[k].IsBegin)
				{
					this.m_successorInfo[this.m_sortNodes[num2].MatchIdx].SuccessorOverlapId = this.m_sortNodes[k].MatchIdx;
					for (int l = num2 + 1; l < k; l++)
					{
						this.m_successorInfo[this.m_sortNodes[l].MatchIdx].SuccessorNonOverlapId = this.m_sortNodes[k].MatchIdx;
					}
					num2 = k;
				}
			}
			this.m_firstValidSortNodeIdx = 0;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00032851 File Offset: 0x00030A51
		public void BeginSubListSpecification()
		{
			this.m_tranMatchListIdx = -1;
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003285C File Offset: 0x00030A5C
		public bool GetNext()
		{
			int num = this.m_tranMatchListIdx + 1;
			this.m_tranMatchListIdx = num;
			return num < this.m_numTranMatches;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00032882 File Offset: 0x00030A82
		public int CurrentTranMatchSourceIndex
		{
			get
			{
				return this.m_tranMatchSourceIndexes[this.m_tranMatchListIdx];
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00032891 File Offset: 0x00030A91
		public T Current
		{
			get
			{
				return this.m_tranMatchList[this.m_tranMatchListIdx];
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x000328A4 File Offset: 0x00030AA4
		public void SetCurrentValid()
		{
			this.m_successorInfo[this.m_tranMatchListIdx].IsValid = true;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x000328BD File Offset: 0x00030ABD
		public void SetCurrentInvalid()
		{
			this.m_successorInfo[this.m_tranMatchListIdx].IsValid = false;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x000328D8 File Offset: 0x00030AD8
		public void EndSubListSpecification()
		{
			int num = -1;
			this.InitSuccessorOverlapIds();
			for (int i = 0; i < 2 * this.m_numTranMatches; i++)
			{
				if (this.m_sortNodes[i].IsBegin && this.m_successorInfo[this.m_sortNodes[i].MatchIdx].IsValid)
				{
					if (num == -1)
					{
						this.m_firstValidSortNodeIdx = i;
						num = i;
					}
					else
					{
						this.m_successorInfo[this.m_sortNodes[num].MatchIdx].SuccessorOverlapId = this.m_sortNodes[i].MatchIdx;
						for (int j = num + 1; j < i; j++)
						{
							if (this.m_successorInfo[this.m_sortNodes[j].MatchIdx].IsValid)
							{
								this.m_successorInfo[this.m_sortNodes[j].MatchIdx].SuccessorNonOverlapId = this.m_sortNodes[i].MatchIdx;
							}
						}
						num = i;
					}
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000329EB File Offset: 0x00030BEB
		public int Count
		{
			get
			{
				return this.m_numTranMatches;
			}
		}

		// Token: 0x1700023A RID: 570
		public T this[int index]
		{
			get
			{
				return this.m_tranMatchList[index];
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00032A01 File Offset: 0x00030C01
		public int GetTranMatchSourceIndex(int index)
		{
			return this.m_tranMatchSourceIndexes[index];
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00032A0B File Offset: 0x00030C0B
		public int GetFirstMatch()
		{
			if (this.m_numTranMatches == 0)
			{
				return -1;
			}
			return this.m_sortNodes[this.m_firstValidSortNodeIdx].MatchIdx;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00032A2D File Offset: 0x00030C2D
		public int GetSuccessorOverlap(int tranMatchIndex)
		{
			if (tranMatchIndex == -1)
			{
				return this.GetFirstMatch();
			}
			if (this.m_successorInfo[tranMatchIndex].SuccessorOverlapId == -1)
			{
				return -1;
			}
			return this.m_successorInfo[tranMatchIndex].SuccessorOverlapId;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00032A61 File Offset: 0x00030C61
		public int GetSuccessorNonOverlap(int tranMatchIndex)
		{
			if (tranMatchIndex == -1)
			{
				return this.GetFirstMatch();
			}
			if (this.m_successorInfo[tranMatchIndex].SuccessorNonOverlapId == -1)
			{
				return -1;
			}
			return this.m_successorInfo[tranMatchIndex].SuccessorNonOverlapId;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00032A98 File Offset: 0x00030C98
		private void InitSuccessorOverlapIds()
		{
			if (this.m_successorInfo == null)
			{
				this.m_successorInfo = new IndexedSubList<T>.TransformationMatchSuccessorInfo[this.m_numTranMatches];
			}
			else if (this.m_successorInfo.Length < this.m_numTranMatches)
			{
				Array.Resize<IndexedSubList<T>.TransformationMatchSuccessorInfo>(ref this.m_successorInfo, this.m_numTranMatches);
			}
			for (int i = 0; i < this.m_numTranMatches; i++)
			{
				this.m_successorInfo[i].SuccessorOverlapId = (this.m_successorInfo[i].SuccessorNonOverlapId = -1);
			}
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00032B18 File Offset: 0x00030D18
		private void Sort()
		{
			for (int i = 0; i < this.m_numTranMatches * 2; i++)
			{
				for (int j = i + 1; j < this.m_numTranMatches * 2; j++)
				{
					if (this.m_sortNodes[j].Position < this.m_sortNodes[i].Position)
					{
						int num = this.m_sortNodes[i].Position;
						this.m_sortNodes[i].Position = this.m_sortNodes[j].Position;
						this.m_sortNodes[j].Position = num;
						num = this.m_sortNodes[i].MatchIdx;
						this.m_sortNodes[i].MatchIdx = this.m_sortNodes[j].MatchIdx;
						this.m_sortNodes[j].MatchIdx = num;
						bool isBegin = this.m_sortNodes[i].IsBegin;
						this.m_sortNodes[i].IsBegin = this.m_sortNodes[j].IsBegin;
						this.m_sortNodes[j].IsBegin = isBegin;
					}
					else if (this.m_sortNodes[j].Position == this.m_sortNodes[i].Position && !this.m_sortNodes[j].IsBegin && this.m_sortNodes[i].IsBegin)
					{
						int num = this.m_sortNodes[i].Position;
						this.m_sortNodes[i].Position = this.m_sortNodes[j].Position;
						this.m_sortNodes[j].Position = num;
						num = this.m_sortNodes[i].MatchIdx;
						this.m_sortNodes[i].MatchIdx = this.m_sortNodes[j].MatchIdx;
						this.m_sortNodes[j].MatchIdx = num;
						this.m_sortNodes[j].IsBegin = true;
						this.m_sortNodes[i].IsBegin = false;
					}
				}
			}
		}

		// Token: 0x0400045A RID: 1114
		private const int InitialCapacity = 2;

		// Token: 0x0400045B RID: 1115
		private const float GrowthFactor = 2f;

		// Token: 0x0400045C RID: 1116
		private int m_tranMatchListIdx;

		// Token: 0x0400045D RID: 1117
		private T[] m_tranMatchList;

		// Token: 0x0400045E RID: 1118
		private int[] m_tranMatchSourceIndexes;

		// Token: 0x0400045F RID: 1119
		private IndexedSubList<T>.TransformationMatchSuccessorInfo[] m_successorInfo;

		// Token: 0x04000460 RID: 1120
		private IndexedSubList<T>.SortNode[] m_sortNodes;

		// Token: 0x04000461 RID: 1121
		private int m_numTranMatches;

		// Token: 0x04000462 RID: 1122
		private int m_firstValidSortNodeIdx;

		// Token: 0x020001B0 RID: 432
		[Serializable]
		private struct SortNode
		{
			// Token: 0x04000719 RID: 1817
			public int Position;

			// Token: 0x0400071A RID: 1818
			public bool IsBegin;

			// Token: 0x0400071B RID: 1819
			public int MatchIdx;
		}

		// Token: 0x020001B1 RID: 433
		[Serializable]
		private struct TransformationMatchSuccessorInfo
		{
			// Token: 0x0400071C RID: 1820
			public int SuccessorOverlapId;

			// Token: 0x0400071D RID: 1821
			public int SuccessorNonOverlapId;

			// Token: 0x0400071E RID: 1822
			public bool IsValid;
		}
	}
}
