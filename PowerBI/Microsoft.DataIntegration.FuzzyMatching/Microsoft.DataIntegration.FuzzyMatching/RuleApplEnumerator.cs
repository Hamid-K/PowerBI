using System;
using System.Collections;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class RuleApplEnumerator<T> : IDeserializationCallback where T : ITransformationMatch
	{
		// Token: 0x06000ABE RID: 2750 RVA: 0x000303BB File Offset: 0x0002E5BB
		public RuleApplEnumerator()
		{
			this.InitTransientMembers();
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000303C9 File Offset: 0x0002E5C9
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.InitTransientMembers();
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000303D1 File Offset: 0x0002E5D1
		private void InitTransientMembers()
		{
			this.m_appliedRulesStack = new IndexableStack<int>();
			this.m_isAffected = new BitArray(1);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000303EA File Offset: 0x0002E5EA
		internal void Reset(TokenSequence tokenSequence, IndexedSubList<T> tranMatchSubList)
		{
			this.m_tranMatchSubList = tranMatchSubList;
			this.m_isAffected.Length = tokenSequence.Count;
			this.m_isAffected.SetAll(false);
			this.m_appliedRulesStack.Clear();
			this.m_appliedRulesStack.Push(-1);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00030428 File Offset: 0x0002E628
		public bool GetNextRuleApplication()
		{
			if (this.m_appliedRulesStack.Count == 0)
			{
				return false;
			}
			int num = this.m_appliedRulesStack.Pop();
			if (num != -1)
			{
				T t = this.m_tranMatchSubList[num];
				int num2 = t.Position;
				for (;;)
				{
					int num3 = num2;
					Transformation transformation = t.Transformation;
					if (num3 >= transformation.From.Count + t.Position)
					{
						break;
					}
					this.m_isAffected[num2] = false;
					num2++;
				}
			}
			for (num = this.m_tranMatchSubList.GetSuccessorOverlap(num); num != -1; num = this.m_tranMatchSubList.GetSuccessorNonOverlap(num))
			{
				this.m_appliedRulesStack.Push(num);
				T t2 = this.m_tranMatchSubList[num];
				int num4 = t2.Position;
				for (;;)
				{
					int num5 = num4;
					int position = t2.Position;
					Transformation transformation = t2.Transformation;
					if (num5 >= position + transformation.From.Count)
					{
						break;
					}
					this.m_isAffected[num4] = true;
					num4++;
				}
			}
			return true;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x00030539 File Offset: 0x0002E739
		public int NumRulesApplied
		{
			get
			{
				return this.m_appliedRulesStack.Count;
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00030546 File Offset: 0x0002E746
		public int GetAppliedRuleIndex(int idx)
		{
			return this.m_appliedRulesStack[idx];
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00030554 File Offset: 0x0002E754
		public T GetAppliedRule(int idx)
		{
			return this.m_tranMatchSubList[this.m_appliedRulesStack[idx]];
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0003056D File Offset: 0x0002E76D
		public T GetAppliedRule(int idx, out int tranMatchSourceIndex)
		{
			tranMatchSourceIndex = this.m_tranMatchSubList.GetTranMatchSourceIndex(this.m_appliedRulesStack[idx]);
			return this.m_tranMatchSubList[this.m_appliedRulesStack[idx]];
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0003059F File Offset: 0x0002E79F
		public bool IsAffected(int pos)
		{
			return this.m_isAffected[pos];
		}

		// Token: 0x0400040D RID: 1037
		private const int InitialBitVectorSize = 1;

		// Token: 0x0400040E RID: 1038
		private const int EmptyMatch = -1;

		// Token: 0x0400040F RID: 1039
		[NonSerialized]
		private IndexedSubList<T> m_tranMatchSubList;

		// Token: 0x04000410 RID: 1040
		[NonSerialized]
		private IndexableStack<int> m_appliedRulesStack;

		// Token: 0x04000411 RID: 1041
		[NonSerialized]
		private BitArray m_isAffected;
	}
}
