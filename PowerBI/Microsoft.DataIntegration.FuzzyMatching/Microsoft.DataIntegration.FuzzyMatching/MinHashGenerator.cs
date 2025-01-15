using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A7 RID: 167
	[Serializable]
	internal sealed class MinHashGenerator
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001CFF3 File Offset: 0x0001B1F3
		public int Dimensions
		{
			get
			{
				return this.m_numDimensions;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0001CFFB File Offset: 0x0001B1FB
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0001D003 File Offset: 0x0001B203
		public int Seed { get; private set; }

		// Token: 0x06000685 RID: 1669 RVA: 0x0001D00C File Offset: 0x0001B20C
		public MinHashGenerator(int numDims, int seed)
		{
			this.m_numDimensions = numDims;
			this.Seed = seed;
			this.MINHASHFN = new double[1000004];
			this.InitializeBasicHashFunctions(seed);
			this.PreComputeMinHashFunction();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001D040 File Offset: 0x0001B240
		private void InitializeBasicHashFunctions(int seed)
		{
			Random random = new Random(seed);
			this.m_a = new ulong[this.m_numDimensions];
			this.m_b = new ulong[this.m_numDimensions];
			for (int i = 0; i < this.m_numDimensions; i++)
			{
				this.m_a[i] = (ulong)((long)random.Next(1000003));
				this.m_b[i] = (ulong)((long)random.Next(1000003));
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		private void PreComputeMinHashFunction()
		{
			int num = 1;
			while ((long)num <= 1000003L)
			{
				this.MINHASHFN[num] = MinHashGenerator.LogPRIME - Math.Log((double)num);
				num++;
			}
			this.MINHASHFN[0] = MinHashGenerator.LogPRIME;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
		private double GetMinHash(int dim, int clusterToken, int weight)
		{
			int num = 1 + (int)((this.m_a[dim] * (ulong)((long)clusterToken) + this.m_b[dim]) % 1000003UL);
			return this.MINHASHFN[num] / (double)weight;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001D12C File Offset: 0x0001B32C
		public void GetMinHash(int clusterToken, int weight, double[] minHashVector)
		{
			for (int i = 0; i < this.m_numDimensions; i++)
			{
				minHashVector[i] = this.GetMinHash(i, clusterToken, weight);
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001D158 File Offset: 0x0001B358
		public void GetMinHash(int[] tokenSeq, int[] tokenWeights, int tokenSeqLen, double[] minHashVector, int[] minHashTokenVector)
		{
			long num = (long)tokenSeq[0] % 1000003L;
			for (int i = 0; i < this.m_numDimensions; i++)
			{
				minHashVector[i] = this.GetMinHash(i, tokenSeq[0], tokenWeights[0]);
				minHashTokenVector[i] = tokenSeq[0];
			}
			for (int j = 1; j < tokenSeqLen; j++)
			{
				long num2 = (long)tokenSeq[j] % 1000003L;
				for (int k = 0; k < this.m_numDimensions; k++)
				{
					double minHash = this.GetMinHash(k, tokenSeq[j], tokenWeights[j]);
					if (minHash < minHashVector[k])
					{
						minHashVector[k] = minHash;
						minHashTokenVector[k] = tokenSeq[j];
					}
					else if (minHash == minHashVector[k] && tokenSeq[j] < minHashTokenVector[k])
					{
						minHashTokenVector[k] = tokenSeq[j];
					}
				}
			}
		}

		// Token: 0x04000251 RID: 593
		private const uint PRIME = 1000003U;

		// Token: 0x04000252 RID: 594
		private static readonly double LogPRIME = Math.Log(1000003.0);

		// Token: 0x04000253 RID: 595
		private ulong[] m_a;

		// Token: 0x04000254 RID: 596
		private ulong[] m_b;

		// Token: 0x04000255 RID: 597
		private double[] MINHASHFN;

		// Token: 0x04000256 RID: 598
		private int m_numDimensions;
	}
}
