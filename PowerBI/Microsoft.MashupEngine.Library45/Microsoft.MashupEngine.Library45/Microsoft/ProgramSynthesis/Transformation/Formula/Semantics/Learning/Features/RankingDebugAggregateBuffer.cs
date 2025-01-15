using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Features
{
	// Token: 0x020016F7 RID: 5879
	public class RankingDebugAggregateBuffer
	{
		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x0600C407 RID: 50183 RVA: 0x002A3639 File Offset: 0x002A1839
		// (set) Token: 0x0600C408 RID: 50184 RVA: 0x002A3641 File Offset: 0x002A1841
		public double AwardRatio { get; set; }

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x0600C409 RID: 50185 RVA: 0x002A364A File Offset: 0x002A184A
		// (set) Token: 0x0600C40A RID: 50186 RVA: 0x002A3652 File Offset: 0x002A1852
		public double AwardScore { get; set; }

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x0600C40B RID: 50187 RVA: 0x002A365B File Offset: 0x002A185B
		// (set) Token: 0x0600C40C RID: 50188 RVA: 0x002A3663 File Offset: 0x002A1863
		public double MaxScore { get; set; }

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x0600C40D RID: 50189 RVA: 0x002A366C File Offset: 0x002A186C
		// (set) Token: 0x0600C40E RID: 50190 RVA: 0x002A3674 File Offset: 0x002A1874
		public ProgramNode Node { get; set; }

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x0600C40F RID: 50191 RVA: 0x002A367D File Offset: 0x002A187D
		// (set) Token: 0x0600C410 RID: 50192 RVA: 0x002A3685 File Offset: 0x002A1885
		public double PenaltyRatio { get; set; }

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x0600C411 RID: 50193 RVA: 0x002A368E File Offset: 0x002A188E
		// (set) Token: 0x0600C412 RID: 50194 RVA: 0x002A3696 File Offset: 0x002A1896
		public double PenaltyScore { get; set; }

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x0600C413 RID: 50195 RVA: 0x002A369F File Offset: 0x002A189F
		// (set) Token: 0x0600C414 RID: 50196 RVA: 0x002A36A7 File Offset: 0x002A18A7
		public double Score { get; set; }
	}
}
