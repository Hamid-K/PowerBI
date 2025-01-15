using System;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics
{
	// Token: 0x02001CD3 RID: 7379
	internal class LearningConfiguration : Tuple<int?, int?>
	{
		// Token: 0x170029DF RID: 10719
		// (get) Token: 0x0600FA2D RID: 64045 RVA: 0x0035333C File Offset: 0x0035153C
		public int? ConditionalBranchLimit
		{
			get
			{
				return base.Item1;
			}
		}

		// Token: 0x170029E0 RID: 10720
		// (get) Token: 0x0600FA2E RID: 64046 RVA: 0x00353344 File Offset: 0x00351544
		public int? ConditionalClusterLimit
		{
			get
			{
				return base.Item2;
			}
		}

		// Token: 0x0600FA2F RID: 64047 RVA: 0x0035334C File Offset: 0x0035154C
		public LearningConfiguration(int? branchLimit, int? clusterLimit)
			: base(branchLimit, clusterLimit)
		{
		}
	}
}
