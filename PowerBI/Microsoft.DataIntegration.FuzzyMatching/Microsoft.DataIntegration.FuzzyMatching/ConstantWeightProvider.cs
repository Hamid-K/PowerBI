using System;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000116 RID: 278
	[Serializable]
	public sealed class ConstantWeightProvider : ITokenWeightProvider
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x000332BF File Offset: 0x000314BF
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x000332C7 File Offset: 0x000314C7
		public int Weight { get; set; }

		// Token: 0x06000B9A RID: 2970 RVA: 0x000332D0 File Offset: 0x000314D0
		public ConstantWeightProvider()
		{
			this.Weight = 1;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000332DF File Offset: 0x000314DF
		public ConstantWeightProvider(int weight)
		{
			this.Weight = weight;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000332EE File Offset: 0x000314EE
		public int GetWeight(ITokenIdProvider tokenIdProvider, int token)
		{
			return this.Weight;
		}
	}
}
