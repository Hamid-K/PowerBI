using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class SketchEstimator : ISelfJoinEstimator
	{
		// Token: 0x0600007A RID: 122 RVA: 0x000027A0 File Offset: 0x000009A0
		public SketchEstimator()
		{
			this.m_nHashFunctions = 250;
			this.m_hashSum = new int[this.m_nHashFunctions];
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000027C4 File Offset: 0x000009C4
		public void Begin()
		{
			for (int i = 0; i < this.m_nHashFunctions; i++)
			{
				this.m_hashSum[i] = 0;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000027EC File Offset: 0x000009EC
		public void Add(int key)
		{
			for (int i = 0; i < this.m_nHashFunctions; i++)
			{
				this.m_hashSum[i] += this.Hash(i, key);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002822 File Offset: 0x00000A22
		public void End()
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002824 File Offset: 0x00000A24
		public long SelfJoinSize()
		{
			double[] array = new double[5];
			for (int i = 0; i < 5; i++)
			{
				long num = 0L;
				for (int j = 0; j < 50; j++)
				{
					int num2 = i * 50 + j;
					num += (long)this.m_hashSum[num2] * (long)this.m_hashSum[num2];
				}
				array[i] = (double)num / 50.0;
			}
			Array.Sort<double>(array);
			return Convert.ToInt64(Math.Round(array[2]));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002896 File Offset: 0x00000A96
		public void Clear()
		{
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002898 File Offset: 0x00000A98
		private int Hash(int i, int key)
		{
			int num = i / 16;
			int num2 = i % 16;
			int hashCode = Utilities.GetHashCode(key + num);
			if (this.GetBit(num2, hashCode) == 0)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000028C5 File Offset: 0x00000AC5
		private int GetBit(int i, int key)
		{
			key >>= i;
			return key % 2;
		}

		// Token: 0x04000013 RID: 19
		private const int s1 = 50;

		// Token: 0x04000014 RID: 20
		private const int s2 = 5;

		// Token: 0x04000015 RID: 21
		private int m_nHashFunctions;

		// Token: 0x04000016 RID: 22
		private int[] m_hashSum;
	}
}
