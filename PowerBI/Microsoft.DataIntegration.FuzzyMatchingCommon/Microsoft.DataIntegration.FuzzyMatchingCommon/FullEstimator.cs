using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class FullEstimator : ISelfJoinEstimator
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000026F1 File Offset: 0x000008F1
		public FullEstimator()
		{
			this.m_ht = new Dictionary<int, int>();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002704 File Offset: 0x00000904
		public void Begin()
		{
			this.m_ht.Clear();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002714 File Offset: 0x00000914
		public void Add(int key)
		{
			int num = 0;
			if (!this.m_ht.TryGetValue(key, ref num))
			{
				this.m_ht.Add(key, 0);
			}
			num++;
			this.m_ht[key] = num;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002751 File Offset: 0x00000951
		public void End()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002754 File Offset: 0x00000954
		public long SelfJoinSize()
		{
			long num = 0L;
			foreach (KeyValuePair<int, int> keyValuePair in this.m_ht)
			{
				int value = keyValuePair.Value;
				num += (long)(value * value);
			}
			return num;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002793 File Offset: 0x00000993
		public void Clear()
		{
			this.m_ht.Clear();
		}

		// Token: 0x04000012 RID: 18
		private Dictionary<int, int> m_ht;
	}
}
