using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	internal sealed class RuleGroup
	{
		// Token: 0x0600068C RID: 1676 RVA: 0x0001D214 File Offset: 0x0001B414
		public RuleGroup(int id, int numDims)
		{
			this.m_numDimensions = numDims;
			this.m_minHashDims = new GroupMinHashDim[numDims];
			for (int i = 0; i < numDims; i++)
			{
				this.m_minHashDims[i] = new GroupMinHashDim();
			}
			this.Id = id;
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0001D25A File Offset: 0x0001B45A
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001D262 File Offset: 0x0001B462
		public int Id { get; private set; }

		// Token: 0x0600068F RID: 1679 RVA: 0x0001D26C File Offset: 0x0001B46C
		public void BeginUpdate()
		{
			GroupMinHashDim[] minHashDims = this.m_minHashDims;
			for (int i = 0; i < minHashDims.Length; i++)
			{
				minHashDims[i].BeginUpdate();
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001D298 File Offset: 0x0001B498
		public void EndUpdate()
		{
			GroupMinHashDim[] minHashDims = this.m_minHashDims;
			for (int i = 0; i < minHashDims.Length; i++)
			{
				minHashDims[i].EndUpdate();
			}
		}

		// Token: 0x17000151 RID: 337
		public GroupMinHashDim this[int dim]
		{
			get
			{
				return this.m_minHashDims[dim];
			}
		}

		// Token: 0x04000258 RID: 600
		private int m_numDimensions;

		// Token: 0x04000259 RID: 601
		private GroupMinHashDim[] m_minHashDims;
	}
}
